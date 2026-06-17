using System.Text.Json.Serialization;
namespace CatalogoVideojuegos.Models
{
    public class Desarrollador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public int AnioFundacion { get; set; }

        // Propiedad de navegación (un desarrollador tiene muchos videojuegos)
        [JsonIgnore]
        public List<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();
    }
}