// Define el contrato del servicio de videojuegos - qué operaciones ofrece.
// No implementa la lógica - esa tarea corresponde a VideojuegoService.
// El Controller depende de esta interfaz, no de la clase concreta.
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