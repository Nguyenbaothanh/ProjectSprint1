using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping_Tutorial.Models;
using Shopping_Tutorial.Repository;

var builder = WebApplication.CreateBuilder(args);
// Connect Db
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
});
// Add services to the container.
builder.Services.AddControllersWithViews();


// save cache
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
//Khai bao Identity
builder.Services.AddIdentity<AppUserModel, IdentityRole>()
	.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

var app = builder.Build();

app.UseSession();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{Controller=Product}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Seeding Data
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();

SeedData.SeedingData(context);

app.Run();
