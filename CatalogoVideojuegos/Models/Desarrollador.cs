// Representa una empresa/persona desarrolladora.
// La relación es uno-a-muchos - un desarrollador puede tener varios juegos.
using System.Text.Json.Serialization;
namespace CatalogoVideojuegos.Models
{
    public class Desarrollador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public int AnioFundacion { get; set; }
// Propiedad de navegación - relación uno-a-muchos (un desarrollador puede tener varios juegos).
// Evita serializar la lista al JSON y posibles referencias circulares.)
        [JsonIgnore]
        public List<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();
    }
}