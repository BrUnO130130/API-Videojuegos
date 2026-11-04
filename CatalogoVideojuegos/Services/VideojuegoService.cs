// Implementa la lógica CRUD para videojuegos.
// Recibe AppDbContext por inyección de dependencias y usa EF Core
// para consultar, agregar, modificar y eliminar registros.
using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using CatalogoVideojuegos.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogoVideojuegos.Services;

public class VideojuegoService : IVideojuegoService
{
 // Contexto de EF Core: permite acceder a las tablas y guardar cambios.
    private readonly AppDbContext _context;
// Constructor: recibe AppDbContext por inyección de dependencias.
    public VideojuegoService(AppDbContext context)
    {
        _context = context;
    }
// READ: obtiene todos los videojuegos - ToListAsync ejecuta la consulta de forma asincrónica.
    public async Task<List<Videojuego>> ObtenerTodosAsync()
    {
        return await _context.Videojuegos.ToListAsync();
    }
// READ por ID: FindAsync busca por clave primaria - devuelve null si no existe.
    public async Task<Videojuego?> ObtenerPorIdAsync(int id)
    {
        return await _context.Videojuegos.FindAsync(id);
    }
// CREATE: convierte el DTO recibido en una entidad Videojuego.
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
// Add marca la entidad como nueva en el contexto - SaveChangesAsync la persiste en SQLite.
        _context.Videojuegos.Add(videojuego);
        await _context.SaveChangesAsync();

        return videojuego;
    }
// UPDATE: busca el registro - devuelve false si no existe y true al guardar la modificación.
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
// DELETE: busca la entidad - Remove la marca para eliminar y SaveChangesAsync confirma el cambio.
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
