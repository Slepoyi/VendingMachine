using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IDrinkRepository
    {
        IQueryable<Drink> Drinks { get; }

        Task AddDrinkAsync(Drink drink);

        Task UpdateDrinkAsync(Drink drink);

        Task DeleteDrinkAsync(Drink drink);
    }
}
