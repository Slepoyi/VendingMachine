using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Interfaces
{
    public interface IDrinkRepository
    {
        IQueryable<Drink> Drinks { get; }

        Task<Drink?> FindDrinkAsync(int id);

        Task AddDrinkAsync(Drink drink);

        Task UpdateDrinkAsync(Drink drink);

        Task DeleteDrinkAsync(Drink drink);
    }
}
