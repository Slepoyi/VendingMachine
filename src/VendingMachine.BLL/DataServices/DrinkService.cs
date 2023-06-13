using BLL.Dtos;
using BLL.Extensions;
using BLL.Interfaces;
using VendingMachine.DAL.Interfaces;

namespace VendingMachine.BLL.DataServices
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;

        public DrinkService(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }

        public IEnumerable<DrinkDto> Drinks
            => _drinkRepository.Drinks.AsEnumerable().ToDrinkDtoEnumerable();

        public async Task AddDrinkAsync(DrinkDto drinkDto)
        {
            var drink = drinkDto.ToDrink();
            await _drinkRepository.AddDrinkAsync(drink);
        }

        public async Task<bool> UpdateDrinkAsync(DrinkDto drinkDto)
        {
            var existingDrink = _drinkRepository.FindDrinkAsync(drinkDto.Id);
            if (existingDrink == null)
                return false;
            else
            {
                await _drinkRepository.UpdateDrinkAsync(drinkDto.ToDrink());
                return true;
            }
        }

        public async Task<DrinkDto?> DeleteDrinkAsync(int id)
        {
            var existingDrink = await _drinkRepository.FindDrinkAsync(id);
            if (existingDrink == null)
                return null;
            else
            {
                await _drinkRepository.DeleteDrinkAsync(existingDrink);
                return existingDrink.ToDrinkDto();
            }
        }
    }
}
