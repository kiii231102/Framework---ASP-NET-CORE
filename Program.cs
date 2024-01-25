using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Musclegym.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusclegymContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MusclegymContext");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'MusclegymContext' not found.");
    }
    options.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Awal}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "member",
    pattern: "{controller=Home}/{action=Member}");

app.Run();
