using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;

namespace CatalogoVideojuegos.Services;

public class DesarrolladorService : IDesarrolladorService
{
    private readonly List<Desarrollador> desarrolladores = new();

    public List<Desarrollador> ObtenerTodos()
    {
        return desarrolladores;
    }

    public Desarrollador? ObtenerPorId(int id)
    {
        return desarrolladores.FirstOrDefault(d => d.Id == id);
    }

    public Desarrollador Crear(DesarrolladorDTO dto)
    {
        var desarrollador = new Desarrollador
        {
            Id = desarrolladores.Count + 1,
            Nombre = dto.Nombre,
            Pais = dto.Pais,
            AnioFundacion = dto.AñoFundacion
        };

        desarrolladores.Add(desarrollador);

        return desarrollador;
    }

    public bool Actualizar(int id, DesarrolladorDTO dto)
    {
        var desarrollador = desarrolladores.FirstOrDefault(d => d.Id == id);

        if (desarrollador == null)
            return false;

        desarrollador.Nombre = dto.Nombre;
        desarrollador.Pais = dto.Pais;
        desarrollador.AnioFundacion = dto.AñoFundacion;

        return true;
    }

    public bool Eliminar(int id)
    {
        var desarrollador = desarrolladores.FirstOrDefault(d => d.Id == id);

        if (desarrollador == null)
            return false;

        desarrolladores.Remove(desarrollador);

        return true;
    }
}