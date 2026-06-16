using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;

public interface IVideojuegoService
{
    List<Videojuego> ObtenerTodos();
    Videojuego? ObtenerPorId(int id);
    Videojuego Crear(VideojuegoDTO dto);
    bool Actualizar(int id, VideojuegoDTO dto);
    bool Eliminar(int id);
}