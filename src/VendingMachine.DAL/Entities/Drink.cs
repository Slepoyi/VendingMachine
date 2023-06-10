using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.DAL.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Amount { get; set; }
        public byte[]? Photo { get; set; }
    }
}
