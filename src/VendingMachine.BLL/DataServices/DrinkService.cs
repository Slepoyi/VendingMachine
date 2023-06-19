using BLL.Dtos;
using BLL.Extensions;
using BLL.Interfaces;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.DataServices
{
    public class DrinkService : IDrinkService
    {
        private readonly IRepository<Drink, int> _drinkRepository;

        public DrinkService(IRepository<Drink, int> drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }

        public IEnumerable<DrinkDto> Drinks
            => _drinkRepository.Entities.ToDrinkDtoEnumerable();

        public async Task<DrinkDto?> FindDrinkAsync(int id)
        {
            var res = await _drinkRepository.FindAsync(id);
            if (res is null) return null;

            return res.ToDrinkDto();
        }

        public async Task AddDrinkAsync(DrinkDto drinkDto)
        {
            var drink = drinkDto.ToDrink();
            await _drinkRepository.AddAsync(drink);
        }

        public async Task UpdateDrinkAsync(DrinkDto drinkDto)
        {
            await _drinkRepository.UpdateAsync(drinkDto.ToDrink());
        }

        public async Task<DrinkDto?> DeleteDrinkAsync(int id)
        {
            var existingDrink = await _drinkRepository.FindAsync(id);
            if (existingDrink == null) return null;

            await _drinkRepository.DeleteAsync(existingDrink);
            return existingDrink.ToDrinkDto();
        }
    }
}
