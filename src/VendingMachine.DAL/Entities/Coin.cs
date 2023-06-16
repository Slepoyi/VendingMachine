namespace VendingMachine.DAL.Entities
{
    public class Coin
    {
        public CoinValue Value { get; set; }
        public bool IsAccepted { get; set; } = true;
        public int Quantity { get; set; }
    }
}
