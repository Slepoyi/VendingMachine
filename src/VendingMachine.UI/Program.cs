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

var conn = builder.Configuration.GetConnectionString("LocalConnection");
var dir = Environment.CurrentDirectory;
if (conn.Contains("%CONTENTROOTPATH%"))
{
    conn = conn.Replace("%CONTENTROOTPATH%", dir);
}

builder.Services.AddDbContext<EfDbContext>(options =>
{
    options.UseSqlServer(conn);
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
