using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PYP_Book.Application.ServiceRegistration;
using PYP_Book.Domain.Entities;
using PYP_Book.Infrastructure.Data;
using PYP_Book.Infrastructure.ServiceRegistration;
using System.Text;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddFluentValidationAutoValidation(configuration => configuration.DisableDataAnnotationsValidation = false)
//    .AddFluentValidationClientsideAdapters();
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;

    opt.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm_.";
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthentication((opt) =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer((configuration) =>
{
    configuration.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issue"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomMiddlewares();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
