using System.ComponentModel.DataAnnotations;

namespace BLL.Dtos
{
    public class DrinkDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Amount { get; set; }
        public byte[]? Photo { get; set; }
    }
}
