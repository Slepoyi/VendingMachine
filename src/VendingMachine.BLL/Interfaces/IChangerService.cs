using BLL.Dtos;

namespace VendingMachine.BLL.Interfaces
{
    public interface IChangerService
    {
        Task<IEnumerable<CoinDto>> GetChangeAsync(int remainingMoney);
    }
}
