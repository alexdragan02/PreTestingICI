using Backend.Data;
using Backend.Repositories;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Permitem CORS pentru frontend-ul Vue.js
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueClient",
        policy => policy.WithOrigins("http://localhost:5173") // Portul Vue.js
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// 2️⃣ Configurăm PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

// 3️⃣ Înregistrăm repository-urile
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// 4️⃣ Înregistrăm serviciile
builder.Services.AddScoped<ClientService>();

// 5️⃣ Adăugăm serviciile necesare pentru API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6️⃣ Activăm CORS
app.UseCors("AllowVueClient");

// 7️⃣ Configurăm OpenAPI (Swagger) doar în dezvoltare
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8️⃣ Rutele API (Minimal API și Controller API)
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Activează API-urile bazate pe Controller

// 9️⃣ Aplicăm migrațiile la baza de date automat
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Aplică migrațiile
}

// 🔟 Pornim aplicația
app.Run();
