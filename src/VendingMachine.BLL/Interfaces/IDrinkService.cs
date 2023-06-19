using BLL.Dtos;

namespace BLL.Interfaces
{
    public interface IDrinkService
    {
        IEnumerable<DrinkDto> Drinks { get; }

        Task<DrinkDto?> FindDrinkAsync(int id);

        Task AddDrinkAsync(DrinkDto drinkDto);

        Task UpdateDrinkAsync(DrinkDto drinkDto);

        Task<DrinkDto?> DeleteDrinkAsync(int id);
    }
}
