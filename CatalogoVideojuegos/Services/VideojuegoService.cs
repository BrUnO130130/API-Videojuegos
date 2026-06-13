using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;

namespace CatalogoVideojuegos.Services;

public class VideojuegoService : IVideojuegoService
{
    private readonly List<Videojuego> videojuegos = new();

    public List<Videojuego> ObtenerTodos()
    {
        return videojuegos;
    }

    public Videojuego? ObtenerPorId(int id)
    {
        return videojuegos.FirstOrDefault(v => v.Id == id);
    }

    public Videojuego Crear(VideojuegoDTO dto)
    {
        var videojuego = new Videojuego
        {
            Id = videojuegos.Count + 1,
            Nombre = dto.Nombre,
            Genero = dto.Genero,
            Precio = dto.Precio,
            AnioLanzamiento = dto.AñoLanzamiento,
            IdDesarrollador = dto.IdDesarrollador
        };

        videojuegos.Add(videojuego);

        return videojuego;
    }

    public bool Actualizar(int id, VideojuegoDTO dto)
    {
        var videojuego = videojuegos.FirstOrDefault(v => v.Id == id);

        if (videojuego == null)
            return false;

        videojuego.Nombre = dto.Nombre;
        videojuego.Genero = dto.Genero;
        videojuego.Precio = dto.Precio;
        videojuego.AnioLanzamiento = dto.AñoLanzamiento;
        videojuego.IdDesarrollador = dto.IdDesarrollador;

        return true;
    }

    public bool Eliminar(int id)
    {
        var videojuego = videojuegos.FirstOrDefault(v => v.Id == id);

        if (videojuego == null)
            return false;

        videojuegos.Remove(videojuego);
        return true;
    }
}