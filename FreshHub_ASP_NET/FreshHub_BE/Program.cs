using FluentValidation;
using FreshHub_BE.Data;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Extensions;
using FreshHub_BE.Models;
using FreshHub_BE.Services.CartRepository;
using FreshHub_BE.Services.CategoryRepository;
using FreshHub_BE.Services.LoginService;
using FreshHub_BE.Services.OrderService;
using FreshHub_BE.Services.ProductRepository;
using FreshHub_BE.Services.Registration;
using FreshHub_BE.Services.TokenService;
using FreshHub_BE.Services.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { WebRootPath = "wwwroot"});
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    //if (builder.Environment.IsProduction())
    //{
    //    opt.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(8, 0))); //
    //}
   // else
    {
        opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("FreshHub_BE"));
    }

});

builder.Services.Configure<IdentityOptions>(opt => { 
        opt.Password.RequireDigit = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireNonAlphanumeric = false;
    
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
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("ModeratorRole", policy => policy.RequireRole("Moderator"));
    opt.AddPolicy("ModeratorRole", policy => policy.RequireRole("Admin"));
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
app.UseDefaultFiles();
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
    // context.Database.EnsureDeleted();
    context.Database.Migrate();
    await Seed.SeedCategory(context);
    await Seed.SeedRole(services.GetRequiredService<RoleManager<Role>>());
    await Seed.SeedUsers(services.GetRequiredService<UserManager<User>>());
    await Seed.SeedOrderStatus(context);

}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
app.Run();

public partial class Program { };
