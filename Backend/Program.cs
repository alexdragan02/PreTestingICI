using Backend.Data;
using Backend.Models;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configurare conexiune PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

// ✅ Adăugare Identity pentru `User` (în loc de `IdentityUser`)
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ✅ Configurare autentificare cu cookie-uri
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/auth/login";  
    options.LogoutPath = "/api/auth/logout";
    options.AccessDeniedPath = "/api/auth/access-denied";
});

// ✅ Configurare CORS pentru a permite accesul frontend-ului Vue
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Adaugă URL-ul frontend-ului
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// ✅ Adăugăm dependințele Repository și Service
builder.Services.AddScoped<ICasaDeMarcatRepository, CasaDeMarcatRepository>();
builder.Services.AddScoped<ICasaDeMarcatService, CasaDeMarcatService>();
builder.Services.AddScoped<IMsjRepository, MsjRepository>();
builder.Services.AddScoped<IMsjService, MsjService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Activează CORS
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
