// Representa un videojuego y sus columnas principales en la base de datos.
// EF Core utiliza esta clase para materializar registros como objetos C#.
using System.Text.Json.Serialization;
namespace CatalogoVideojuegos.Models
{
    public class Videojuego
    {
        // Clave primaria, normalmente generada automáticamente por SQLite.
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int AnioLanzamiento { get; set; }

        // Clave foránea hacia Desarrollador
        // Clave foránea: indica qué desarrollador creó este videojuego.
        public int IdDesarrollador { get; set; }

        // Propiedad de navegación
        // Evita que esta relación se incluya en el JSON de respuesta (previene ciclos).
        [JsonIgnore]
         // Propiedad de navegación de EF Core: permite acceder al objeto relacionado.
        public Desarrollador? Desarrollador { get; set; }
    }
}