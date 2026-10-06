using Microsoft.Data.Sqlite;

namespace CatalogoVideojuegos.Data
{
    public class DatabaseInitializer
    {
        private readonly string connectionString;

        public DatabaseInitializer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inicializar()
        {
            using var conexion = new SqliteConnection(connectionString);
            conexion.Open();

            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS Desarrolladores (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Pais TEXT NOT NULL,
                    AnioFundacion INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Videojuegos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Genero TEXT NOT NULL,
                    Precio REAL NOT NULL,
                    AnioLanzamiento INTEGER NOT NULL,
                    IdDesarrollador INTEGER NOT NULL,
                    FOREIGN KEY (IdDesarrollador)
                        REFERENCES Desarrolladores(Id)
                );
            ";

            comando.ExecuteNonQuery();
        }
    }
}