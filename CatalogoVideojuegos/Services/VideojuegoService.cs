using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using CatalogoVideojuegos.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogoVideojuegos.Services;

public class VideojuegoService : IVideojuegoService
{
    private readonly AppDbContext _context;

    public VideojuegoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Videojuego>> ObtenerTodosAsync()
    {
        return await _context.Videojuegos.ToListAsync();
    }

    public async Task<Videojuego?> ObtenerPorIdAsync(int id)
    {
        return await _context.Videojuegos.FindAsync(id);
    }

    public async Task<Videojuego> CrearAsync(VideojuegoDTO dto)
    {
        var videojuego = new Videojuego
        {
            Nombre = dto.Nombre,
            Genero = dto.Genero,
            Precio = dto.Precio,
            AnioLanzamiento = dto.AnioLanzamiento,
            IdDesarrollador = dto.IdDesarrollador
        };

        _context.Videojuegos.Add(videojuego);
        await _context.SaveChangesAsync();

        return videojuego;
    }

    public async Task<bool> ActualizarAsync(int id, VideojuegoDTO dto)
    {
        var videojuego = await _context.Videojuegos.FindAsync(id);

        if (videojuego == null)
        {
            return false;
        }

        videojuego.Nombre = dto.Nombre;
        videojuego.Genero = dto.Genero;
        videojuego.Precio = dto.Precio;
        videojuego.AnioLanzamiento = dto.AnioLanzamiento;
        videojuego.IdDesarrollador = dto.IdDesarrollador;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var videojuego = await _context.Videojuegos.FindAsync(id);

        if (videojuego == null)
        {
            return false;
        }

        _context.Videojuegos.Remove(videojuego);
        await _context.SaveChangesAsync();

        return true;
    }
}
