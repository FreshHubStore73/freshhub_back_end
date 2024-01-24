using FreshHub_BE.Data;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.ProductRepository;
using FreshHub_BE.Services.Registration;
using FreshHub_BE.Services.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddTransient<IRegistrationService, RegistrationService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),

        ValidateIssuer = false,

        ValidateAudience = false,
    };
});
builder.Services.AddAuthorization();


builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
//app.UseAuthentication();
//app.UseAuthorization();


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
