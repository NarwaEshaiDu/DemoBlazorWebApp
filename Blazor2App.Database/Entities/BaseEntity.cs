using System.ComponentModel.DataAnnotations;

namespace Blazor2App.Database.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
    }
}
