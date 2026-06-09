namespace CatalogoVideojuegos.Models;
public interface IVideojuego{
    
    String ObternerTodos();
    String ObternerPorId(int id);
    Videojuego Crear(Videojuego videojuego);
    Boolean Actualizar();
    Boolean Eliminar(int id);

}