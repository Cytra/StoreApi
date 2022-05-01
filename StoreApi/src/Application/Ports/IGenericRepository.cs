using System.Linq.Expressions;
using Application.Entities;
using Application.Models.Pagination;

namespace Application.Ports;

public interface IGenericRepository<TEntity> where TEntity : EntityBase
{
    Task<List<TEntity>> Get(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params string[] includeProperties);

    Task<PagedList<TEntity>> GetPaged(int page, int rowsPerPage, Expression<Func<TEntity, bool>> filter = null,
        params string[] includeProperties);

    Task<TEntity> GetById(object id);
    Task Insert(TEntity entity, bool save);
    Task Delete(object id, bool save);
    Task Delete(TEntity entityToDelete, bool save);
    Task Update(TEntity entityToUpdate, bool save);
    Task UpdateEntities<T>(IEnumerable<T> entities, bool save);
    Task Insert<T>(IEnumerable<T> entities, bool save);
    Task Update<T>(IEnumerable<T> entities, bool save);
}