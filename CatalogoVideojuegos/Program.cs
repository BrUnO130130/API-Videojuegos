using Microsoft.Data.Sqlite;

string rutaBaseDeDatos = "videojuegos.db";
using var conexion = new SqliteConnection($"Data Source={rutaBaseDeDatos}");
conexion.Open();

var crearTabla = conexion.CreateCommand();
crearTabla.CommandText = @"
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
crearTabla.ExecuteNonQuery();
var insertar = conexion.CreateCommand();

