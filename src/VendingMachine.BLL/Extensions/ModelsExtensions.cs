using BLL.Dtos;
using VendingMachine.DAL.Entities;

namespace BLL.Extensions
{
    public static class ModelsExtensions
    {
        public static CoinDto ToCoinDto(this Coin coin)
        {
            return new CoinDto
            {
                Value = coin.Value,
                IsAccepted = coin.IsAccepted,
                Quantity = coin.Quantity
            };
        }

        public static Coin ToCoin(this CoinDto coinDto)
        {
            return new Coin
            {
                Value = coinDto.Value,
                IsAccepted = coinDto.IsAccepted,
                Quantity = coinDto.Quantity
            };
        }

        public static DrinkDto ToDrinkDto(this Drink drink)
        {
            return new DrinkDto
            {
                Id = drink.Id,
                Name = drink.Name,
                Price = drink.Price,
                Amount = drink.Amount,
                Photo = drink.Photo
            };
        }

        public static Drink ToDrink(this DrinkDto drinkDto)
        {
            return new Drink
            {
                Id = drinkDto.Id,
                Name = drinkDto.Name,
                Price = drinkDto.Price,
                Amount = drinkDto.Amount,
                Photo = drinkDto.Photo
            };
        }

        public static IEnumerable<CoinDto> ToCoinDtoEnumerable(this IEnumerable<Coin> coinEnumerable)
        {
            foreach (var coin in coinEnumerable)
                yield return coin.ToCoinDto();
        }

        public static IEnumerable<DrinkDto> ToDrinkDtoEnumerable(this IEnumerable<Drink> drinkEnumerable)
        {
            foreach (var drink in drinkEnumerable)
                yield return drink.ToDrinkDto();
        }
    }
}
