using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;

namespace CatalogoVideojuegos.Services;

public interface IVideojuegoService
{
    Task<List<Desarrollador>> ObtenerTodosAsync();

    Task<Desarrollador?> ObtenerPorIdAsync(int id);

    Task<Desarrollador> CrearAsync(VideojuegoDTO dto);

    Task<bool> ActualizarAsync(int id, VideojuegoDTO dto);

    Task<bool> EliminarAsync(int id);
}
