using System;
using System.ComponentModel.DataAnnotations;

namespace StoreApi.Entities
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public Guid Key { get; set; }
        public DateTimeOffset InsertTime { get; set; }
    }
}