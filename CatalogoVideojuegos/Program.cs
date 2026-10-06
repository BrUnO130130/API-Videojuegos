using CatalogoVideojuegos.Data;
using CatalogoVideojuegos.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IVideojuegoService, VideojuegoService>();
builder.Services.AddScoped<IDesarrolladorService, DesarrolladorService>();

var app = builder.Build();

var initializer = new DatabaseInitializer("Data Source=videojuegos.db");
initializer.Inicializar();

app.MapControllers();

app.Run();