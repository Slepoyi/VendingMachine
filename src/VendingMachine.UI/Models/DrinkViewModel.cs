using System.ComponentModel.DataAnnotations;

namespace VendingMachine.UI.Models
{
    public class DrinkViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
        public byte[]? Photo { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
