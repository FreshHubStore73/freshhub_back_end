using FluentValidation;
using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Extensions;
using FreshHub_BE.Models;
using FreshHub_BE.Services.CartRepository;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.LoginService;
using FreshHub_BE.Services.ProductRepository;
using FreshHub_BE.Services.Registration;
using FreshHub_BE.Services.TokenService;
using FreshHub_BE.Services.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddSwaggerGen(x =>
{
    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }        
    };
    x.AddSecurityDefinition("Bearer", securitySchema);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securitySchema, new []{"Bearer"}
        }
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IRegistrationService, RegistrationService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginModel>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("ModeratorRole", policy => policy.RequireRole("Moderator"));
});

builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<AppDbContext>();

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


builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();
app.UseStaticFiles();
app.UseExceptionMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();
using var scoped = app.Services.CreateScope();
var services = scoped.ServiceProvider;
try
{

    var context = services.GetRequiredService<AppDbContext>();


    await Seed.SeedCategory(context);
    await Seed.SeedRole(services.GetRequiredService<RoleManager<Role>>());
    await Seed.SeedUsers(services.GetRequiredService<UserManager<User>>());

}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
app.Run();
