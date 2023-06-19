using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using VendingMachine.BLL.ChangerServices;
using VendingMachine.BLL.DataServices;
using VendingMachine.BLL.Interfaces;
using VendingMachine.DAL.Context;
using VendingMachine.DAL.Entities;
using VendingMachine.DAL.Interfaces;
using VendingMachine.DAL.Repositories;
using VendingMachine.UI.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EfDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DesktopConnection"));
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllersWithViews();

builder.Services.Configure<SecretOptions>(builder.Configuration.GetSection(SecretOptions.Section));

builder.Services.AddScoped<IRepository<Coin, CoinValue>, CoinRepository>();
builder.Services.AddScoped<IRepository<Drink, int>, DrinkRepository>();

builder.Services.AddScoped<ICoinService, CoinService>();
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<IChangerService, GreedyChangerService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Main}/{id?}");

app.Run();
