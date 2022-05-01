using System.Linq.Expressions;
using Application.Entities;
using Application.Models.Pagination;
using Application.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
{
    internal DatabaseContext context;
    internal DbSet<TEntity> dbSet;

    public GenericRepository(DatabaseContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params string[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null) query = query.Where(filter);

        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);

        if (orderBy != null) return await orderBy(query).ToListAsync();
        return await query.ToListAsync();
    }

    public virtual async Task<PagedList<TEntity>> GetPaged(int page, int rowsPerPage,
        Expression<Func<TEntity, bool>> filter = null, params string[] includeProperties)
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null) query = query.Where(filter);
        foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
        var total = await query.CountAsync();
        query = query.OrderByDescending(x => x.Id).Skip((page - 1) * rowsPerPage)
            .Take(rowsPerPage);
        return new PagedList<TEntity>
        {
            Items = query.ToList(),
            Paging = new PagingModel { Page = page, PageSize = rowsPerPage, TotalItems = total }
        };
    }


    public virtual async Task<TEntity> GetById(object id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task Insert(TEntity entity, bool save)
    {
        dbSet.Add(entity);
        if (save) await Save();
    }

    public virtual async Task Delete(object id, bool save)
    {
        var entityToDelete = dbSet.Find(id);
        await Delete(entityToDelete, save);
    }

    public virtual async Task Delete(TEntity entityToDelete, bool save)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached) dbSet.Attach(entityToDelete);
        dbSet.Remove(entityToDelete);
        if (save) await Save();
    }

    public virtual async Task Update(TEntity entityToUpdate, bool save)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
        if (save) await Save();
    }

    public async Task UpdateEntities<T>(IEnumerable<T> entities, bool save)
    {
        foreach (var entity in entities) context.Update(entity);
        if (save) await Save();
    }

    public async Task Insert<T>(IEnumerable<T> entities, bool save)
    {
        foreach (var entity in entities) context.Add(entity);
        if (save) await Save();
    }

    public async Task Update<T>(IEnumerable<T> entities, bool save)
    {
        foreach (var entity in entities) context.Update(entity);
        if (save) await Save();
    }

    public async Task Save()
    {
        await context.SaveChangesAsync();
    }
}