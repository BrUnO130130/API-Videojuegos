namespace CatalogoVideojuegos.Models;
public interface IDesarrollador{
    
    String ObternerTodos();
    String ObternerPorId(int id);
    Desarrollador Crear(Desarrollador desarrollador);
    Boolean Actualizar();
    Boolean Eliminar(int id);

}