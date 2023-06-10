using BLL.Dtos;
using VendingMachine.UI.Models;

namespace VendingMachine.UI.Extensions
{
    public static class ModelsExtensions
    {
        public static DrinkViewModel ToDrinkViewModel(this DrinkDto drinkDto)
        {
            string? imgUrl = null;
            if (drinkDto.Photo != null)
            {
                var imreBase64Data = Convert.ToBase64String(drinkDto?.Photo);
                imgUrl = string.Format("data:image/png;base64,{0}", imreBase64Data);
            }
            return new DrinkViewModel
            {
                Id = drinkDto.Id,
                Name = drinkDto.Name,
                Price = drinkDto.Price,
                Amount = drinkDto.Amount,
                PhotoUrl = imgUrl
            };
        }

        public static IEnumerable<DrinkViewModel> ToDrinkViewModelEnumerable(this IEnumerable<DrinkDto> drinkDtoEnumerable)
        {
            foreach (var drink in drinkDtoEnumerable)
            {
                yield return drink.ToDrinkViewModel();
            }
        }
    }
}
