using Employe.Data;  
using Microsoft.EntityFrameworkCore;
using Employe.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EmployeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddControllersWithViews();

builder.Services.AddSession();

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

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  
app.Run();