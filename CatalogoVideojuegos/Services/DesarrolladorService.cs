using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using CatalogoVideojuegos.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogoVideojuegos.Services;

public class DesarrolladorService : IDesarrolladorService
{
private readonly AppDbContext _context;

public DesarrolladorService(AppDbContext context)
{
    _context = context;
}

public async Task<List<Desarrollador>> ObtenerTodosAsync()
{
    return await _context.Desarrolladores.ToListAsync();
}

public async Task<Desarrollador?> ObtenerPorIdAsync(int id)
{
    return await _context.Desarrolladores.FindAsync(id);
}
public async Task<Desarrollador> CrearAsync(DesarrolladorDTO dto)
{
    var desarrollador = new Desarrollador
    {
        Nombre = dto.Nombre,
        Pais = dto.Pais,
        AnioFundacion = dto.AnioFundacion
    };

    _context.Desarrolladores.Add(desarrollador);

    await _context.SaveChangesAsync();

    return desarrollador;
}

public async Task<bool> ActualizarAsync(int id, DesarrolladorDTO dto)
{
    var desarrollador = await _context.Desarrolladores.FindAsync(id);

    if (desarrollador == null)
    {
        return false;
    }

    desarrollador.Nombre = dto.Nombre;
    desarrollador.Pais = dto.Pais;
    desarrollador.AnioFundacion = dto.AnioFundacion;

    await _context.SaveChangesAsync();

    return true;
}
public async Task<bool> EliminarAsync(int id)
{
    var desarrollador = await _context.Desarrolladores.FindAsync(id);

    if (desarrollador == null)
    {
        return false;
    }

    _context.Desarrolladores.Remove(desarrollador);

    await _context.SaveChangesAsync();

    return true;
}

}
