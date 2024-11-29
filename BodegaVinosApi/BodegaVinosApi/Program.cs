using Data.Context;
using Data.Repository;
using Data.Repository.Interfaces;
using Data.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios para el contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar el DbContext con SQLite
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DBConnectionString"),
        b => b.MigrationsAssembly("BodegaVinosApi") // Asegura que las migraciones se generen en el proyecto principal
    ));


// Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
            builder.Configuration["Authentication:SecretForKey"]))
    };
});

// Registrar repositorios e interfaces
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWineRepository, WineRepository>();
builder.Services.AddScoped<ICataRepository, CataRepository>();

// Registrar servicios e interfaces
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWineService, WineService>();
builder.Services.AddScoped<ICataService, CataService>();

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware de autenticación
app.UseAuthentication(); // JWT

// Middleware de autorización
app.UseAuthorization();

app.MapControllers();

app.Run();
