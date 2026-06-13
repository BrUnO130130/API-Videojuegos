using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
public interface IDesarrolladorService
{
    List<Desarrollador> ObtenerTodos();
    Desarrollador? ObtenerPorId(int id);
    Desarrollador Crear(DesarrolladorDTO dto);
    bool Actualizar(int id, DesarrolladorDTO dto);
    bool Eliminar(int id);
}