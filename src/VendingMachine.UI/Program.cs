using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VendingMachine.BLL.ChangerServices;
using VendingMachine.BLL.DataServices;
using VendingMachine.BLL.Interfaces;
using VendingMachine.DAL.Context;
using VendingMachine.DAL.Interfaces;
using VendingMachine.DAL.Repositories;
using VendingMachine.UI.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EfDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DesktopConnection"));
});

builder.Services.AddControllersWithViews();

builder.Services.Configure<SecretOptions>(builder.Configuration.GetSection(SecretOptions.Section));



builder.Services.AddScoped<ICoinRepository, CoinRepository>();
builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();

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
    pattern: "{controller=drinks}/{action=getdrinks}/{id?}");

app.Run();
