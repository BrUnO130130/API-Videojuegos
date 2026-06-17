namespace CatalogoVideojuegos.DTOs
{
    public class VideojuegoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int AnioLanzamiento { get; set; }

        // Clave foránea hacia Desarrollador
        public int IdDesarrollador { get; set; }

    }
}