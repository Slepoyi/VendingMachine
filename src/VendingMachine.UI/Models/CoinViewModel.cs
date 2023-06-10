using System.ComponentModel.DataAnnotations;

namespace VendingMachine.UI.Models
{
    public class CoinViewModel
    {
        [Key]
        public CoinValue Value { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
