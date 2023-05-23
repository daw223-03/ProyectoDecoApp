using DecoApp4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Rotativa.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddWkhtmltopdf();
builder.Services.AddDbContext<DecoappContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//IWebHostEnvironment env = app.Environment;
//Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa"); Para windows en fisico
Rotativa.AspNetCore.RotativaConfiguration.Setup("/usr/local/bin", string.Empty);


app.Run();
