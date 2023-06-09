using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DAL.Entities
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public uint Amount { get; set; }
    }
}
