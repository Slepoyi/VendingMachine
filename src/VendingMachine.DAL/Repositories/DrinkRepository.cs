using Microsoft.EntityFrameworkCore;
using VendingMachine.DAL.Context;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.DAL.Repositories
{
    public class DrinkRepository : IRepository<Drink, int>
    {
        private readonly EfDbContext _efDbContext;

        public DrinkRepository(EfDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public IQueryable<Drink> Entities => _efDbContext.Drinks.AsNoTracking();

        public async Task<Drink?> FindAsync(int id)
            => await _efDbContext.Drinks.FindAsync(id);

        public async Task AddAsync(Drink drink)
        {
            await _efDbContext.Drinks.AddAsync(drink);
            await _efDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Drink drink)
        {
            var attachedEntity = _efDbContext.ChangeTracker
                .Entries<Drink>()
                .FirstOrDefault(e => e.Entity.Id == drink.Id);

            if (attachedEntity is not null)
                _efDbContext.Entry(attachedEntity.Entity).State = EntityState.Detached;

            _efDbContext.Entry(drink).State = EntityState.Modified;

            await _efDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Drink drink)
        {
            _efDbContext.Drinks.Remove(drink);
            await _efDbContext.SaveChangesAsync();
        }
    }
}
