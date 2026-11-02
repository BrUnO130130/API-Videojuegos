using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;

namespace CatalogoVideojuegos.Services;

public interface IDesarrolladorService
{
    Task<List<Desarrollador>> ObtenerTodosAsync();

    Task<Desarrollador?> ObtenerPorIdAsync(int id);

    Task<Desarrollador> CrearAsync(DesarrolladorDTO dto);

    Task<bool> ActualizarAsync(int id, DesarrolladorDTO dto);

    Task<bool> EliminarAsync(int id);
}
