using BLL.Dtos;
using VendingMachine.UI.Models;

namespace VendingMachine.UI.Extensions
{
    public static class ModelsExtensions
    {
        public static DrinkViewModel ToDrinkViewModel(this DrinkDto drinkDto)
        {
            string? imgUrl = null;
            if (drinkDto.Photo is not null)
            {
                var imreBase64Data = Convert.ToBase64String(drinkDto.Photo);
                imgUrl = string.Format("data:image/png;base64,{0}", imreBase64Data);
            }
            return new DrinkViewModel
            {
                Id = drinkDto.Id,
                Name = drinkDto.Name,
                Price = drinkDto.Price,
                Amount = drinkDto.Amount,
                Photo = drinkDto.Photo,
                PhotoUrl = imgUrl
            };
        }

        public static DrinkDto ToDrinkDto(this DrinkEditModel drinkEditModel)
        {
            byte[]? photo = null;
            if (drinkEditModel.PhotoFile is not null)
            {
                if (drinkEditModel.PhotoFile.Length > 0)
                {
                    using var ms = new MemoryStream();
                    drinkEditModel.PhotoFile.CopyTo(ms);
                    photo = ms.ToArray();
                }
            }
            return new DrinkDto
            {
                Id = drinkEditModel.Id,
                Name = drinkEditModel.Name,
                Price = drinkEditModel.Price,
                Amount = drinkEditModel.Amount,
                Photo = photo
            };
        }

        public static DrinkEditModel ToDrinkEditModel(this DrinkDto drinkDto)
        {
            return new DrinkEditModel
            {
                Id = drinkDto.Id,
                Name = drinkDto.Name,
                Price = drinkDto.Price,
                Amount = drinkDto.Amount,
                Photo = drinkDto.Photo
            };
        }

        public static DrinkDto ToDrinkDto(this DrinkViewModel drinkViewModel)
        {
            return new DrinkDto
            {
                Id = drinkViewModel.Id,
                Name = drinkViewModel.Name,
                Price = drinkViewModel.Price,
                Amount = drinkViewModel.Amount,
                Photo = drinkViewModel.Photo
            };
        }

        public static CoinViewModel ToCoinViewModel(this CoinDto coinDto)
        {
            return new CoinViewModel
            {
                Value = coinDto.Value,
                IsAccepted = coinDto.IsAccepted,
                Quantity = coinDto.Quantity
            };
        }

        public static CoinDto ToCoinDto(this CoinViewModel coinViewModel)
        {
            return new CoinDto
            {
                Value = coinViewModel.Value,
                IsAccepted = coinViewModel.IsAccepted,
                Quantity = coinViewModel.Quantity
            };
        }

        public static IEnumerable<DrinkViewModel> ToDrinkViewModelEnumerable(this IEnumerable<DrinkDto> drinkDtoEnumerable)
        {
            foreach (var drink in drinkDtoEnumerable)
            {
                yield return drink.ToDrinkViewModel();
            }
        }

        public static IEnumerable<CoinViewModel> ToCoinViewModelEnumerable(this IEnumerable<CoinDto> coinDtoEnumerable)
        {
            foreach (var coin in coinDtoEnumerable)
            {
                yield return coin.ToCoinViewModel();
            }
        }
    }
}
