using Microsoft.EntityFrameworkCore;
using SoalDeveloper.Models;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan DB Context
builder.Services.AddDbContext<FinanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FinanceConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Kontrak}/{action=Index}/{id?}");

app.Run();
