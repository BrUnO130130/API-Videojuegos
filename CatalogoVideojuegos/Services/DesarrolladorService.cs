// Implementa la lógica CRUD para desarrolladores.
// Trabaja con _context.Desarrolladores y traduce los DTO a entidades.
using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using CatalogoVideojuegos.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogoVideojuegos.Services;

public class DesarrolladorService : IDesarrolladorService
{
// Contexto de EF Core utilizado para operar sobre la tabla Desarrolladores.
private readonly AppDbContext _context;

// Constructor: recibe el contexto automáticamente mediante inyección de dependencias.
public DesarrolladorService(AppDbContext context)
{
    _context = context;
}
// READ: devuelve todos los desarrolladores de la base de datos.
public async Task<List<Desarrollador>> ObtenerTodosAsync()
{
    return await _context.Desarrolladores.ToListAsync();
}
// READ por ID: busca por clave primaria - puede devolver null.
public async Task<Desarrollador?> ObtenerPorIdAsync(int id)
{
    return await _context.Desarrolladores.FindAsync(id);
}
// CREATE: pasa los datos del DTO a una entidad y la guarda.
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
// UPDATE: busca el desarrollador y actualiza sus propiedades - false significa que no existe.
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
// DELETE: elimina el desarrollador encontrado - retorna false si no existe.
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
