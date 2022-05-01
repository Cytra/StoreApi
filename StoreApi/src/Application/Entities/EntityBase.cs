using System.ComponentModel.DataAnnotations;

namespace Application.Entities;

public class EntityBase
{
    [Key] public int Id { get; set; }
    public Guid Key { get; set; }
    public DateTimeOffset InsertTime { get; set; }
}