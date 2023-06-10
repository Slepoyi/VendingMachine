using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface IDrinkService
    {
        IEnumerable<DrinkDto> Drinks { get; }

        Task AddDrinkAsync(DrinkDto drinkDto);

        Task<bool> UpdateDrinkAsync(DrinkDto drinkDto);

        Task<DrinkDto?> DeleteDrinkAsync(int id);
    }
}
