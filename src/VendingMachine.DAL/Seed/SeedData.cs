using VendingMachine.DAL.Entities;

namespace VendingMachine.DAL.Seed
{
    public class SeedData
    {
        private const string _currentFolder = "C:\\Users\\Павел\\Desktop\\VendingMachine\\src\\VendingMachine.DAL\\Seed\\";

        public static List<Coin> Coins { get; } = new()
        {
            new Coin
            {
                Value = CoinValue.One,
                IsAccepted = true,
                Quantity = 500
            },
            new Coin
            {
                Value = CoinValue.Two,
                IsAccepted = true,
                Quantity = 250
            },
            new Coin
            {
                Value = CoinValue.Five,
                IsAccepted = true,
                Quantity = 100
            },
            new Coin
            {
                Value = CoinValue.Ten,
                IsAccepted = true,
                Quantity = 50
            },
        };

        public static List<Drink> Drinks { get; } = new()
        {
            new Drink
            {
                Id = 1,
                Name = "Green Tea",
                Price = 22,
                Amount = 20,
                //Photo = File.ReadAllBytes(_currentFolder + "GreenTea.jpeg")
            },
            new Drink
            {
                Id = 2,
                Name = "Black Tea",
                Price = 22,
                Amount = 20,
                //Photo = File.ReadAllBytes(_currentFolder + "BlackTea.jpg")
            },
            new Drink
            {
                Id = 3,
                Name = "Coffee",
                Price = 30,
                Amount = 20,
                //Photo = File.ReadAllBytes(_currentFolder + "Coffee.jpg")
            },
            new Drink
            {
                Id = 4,
                Name = "Water",
                Price = 5,
                Amount = 100,
                //Photo = File.ReadAllBytes(_currentFolder + "Water.jpg")
            },
            new Drink
            {
                Id = 5,
                Name = "Soda",
                Price = 15,
                Amount = 40,
                //Photo = File.ReadAllBytes(_currentFolder + "Soda.jpg")
            },
            new Drink
            {
                Id = 6,
                Name = "Orange Soda",
                Price = 15,
                Amount = 40,
                //Photo = File.ReadAllBytes(_currentFolder + "OrangeSoda.jpg")
            },
            new Drink
            {
                Id = 7,
                Name = "Orange Juice",
                Price = 18,
                Amount = 40,
                //Photo = File.ReadAllBytes(_currentFolder + "OrangeJuice.jpg")
            },
            new Drink
            {
                Id = 8,
                Name = "Apple Juice",
                Price = 14,
                Amount = 40,
                //Photo = File.ReadAllBytes(_currentFolder + "AppleJuice.jpeg")
            },
        };
    }
}
