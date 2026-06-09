using Microsoft.Data.Sqlite;

string rutaBaseDeDatos = "videojuegos.db";
using var conexion = new SqliteConnection($"Data Source={rutaBaseDeDatos}");
conexion.Open();

var crearTabla = conexion.CreateCommand();
crearTabla.CommandText = @"
CREATE TABLE IF NOT EXISTS Videojuegos (
Id INTEGER PRIMARY KEY AUTOINCREMENT,
Nombre TEXT NOT NULL,
Genero TEXT NOT NULL,
Precio REAL NOT NULL,
Añolanzamiento INTEGER NOT NULL,
    FOREIGN KEY (IdDesarrollador)
        REFERENCES Desarrolladores(Id)
)
";

crearTabla.ExecuteNonQuery();