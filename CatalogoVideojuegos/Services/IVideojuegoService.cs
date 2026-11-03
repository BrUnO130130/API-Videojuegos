using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;

namespace CatalogoVideojuegos.Services;

public interface IVideojuegoService
{
    Task<List<Videojuego>> ObtenerTodosAsync();

    Task<Videojuego?> ObtenerPorIdAsync(int id);

    Task<Videojuego> CrearAsync(VideojuegoDTO dto);

    Task<bool> ActualizarAsync(int id, VideojuegoDTO dto);

    Task<bool> EliminarAsync(int id);
}