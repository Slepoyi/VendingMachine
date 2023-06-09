using System.ComponentModel.DataAnnotations;

namespace VendingMachine.DAL.Entities
{
    public class Coin
    {
        [Key]
        public CoinValue Value { get; set; }
        public uint Quantity { get; set; }
    }
}
