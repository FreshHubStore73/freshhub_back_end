using FreshHub_BE.Data;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.ProductRepository;
using FreshHub_BE.Services.UserRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

//app.UseCors(builder => builder.AllowAnyHeader()
//                              .AllowAnyMethod()
//                              .AllowCredentials()
//                              );
app.MapControllers();
using var scoped = app.Services.CreateScope();
var services = scoped.ServiceProvider;
try
{

    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedCategory(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
app.Run();
