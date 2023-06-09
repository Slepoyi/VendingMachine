using VendingMachine.DAL.Context;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Services
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly EfDbContext _efDbContext;

        public DrinkRepository(EfDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public IQueryable<Drink> Drinks => _efDbContext.Drinks;

        public async Task AddDrinkAsync(Drink drink)
        {
            await _efDbContext.Drinks.AddAsync(drink);
            await _efDbContext.SaveChangesAsync();
        }

        public async Task UpdateDrinkAsync(Drink drink)
        {
            _efDbContext.Drinks.Update(drink);
            await _efDbContext.SaveChangesAsync();
        }

        public async Task DeleteDrinkAsync(Drink drink)
        {
            _efDbContext.Drinks.Remove(drink);
            await _efDbContext.SaveChangesAsync();
        }
    }
}
