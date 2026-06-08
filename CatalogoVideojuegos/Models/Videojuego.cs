namespace CatalogoVideojuegos.Models
{
    public class Videojuego
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int AñoLanzamiento { get; set; }

        // Clave foránea hacia Desarrollador
        public int IdDesarrollador { get; set; }

        // Propiedad de navegación
        public Desarrollador? Desarrollador { get; set; }
    }
}