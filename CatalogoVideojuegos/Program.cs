using CatalogoVideojuegos.Context;
using CatalogoVideojuegos.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=videojuegos.db"));

builder.Services.AddScoped<IVideojuegoService, VideojuegoService>();
builder.Services.AddScoped<IDesarrolladorService, DesarrolladorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();