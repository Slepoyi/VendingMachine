namespace VendingMachine.BLL.Interfaces
{
    public interface IChangerService
    {
        Task<Dictionary<CoinValue, int>> GetChangeAsync(int remainingMoney);
    }
}
