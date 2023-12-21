using System.ComponentModel.DataAnnotations;

namespace Blazor2App.Shared.Models.Registration
{
    public class RegistrationModel
    {
        [Required]
        public string EventId { get; set; } = null!;

        [Required]
        public string MemberId { get; set; } = null!;

        [Required]
        public decimal Payment { get; set; }
    }
}
