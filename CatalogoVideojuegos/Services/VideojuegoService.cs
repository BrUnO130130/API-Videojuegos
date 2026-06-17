using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using Microsoft.Data.Sqlite;

namespace CatalogoVideojuegos.Services;

public class VideojuegoService : IVideojuegoService
{
    private readonly string connectionString = "Data Source=videojuegos.db";

public List<Videojuego> ObtenerTodos()
{
    var videojuegos = new List<Videojuego>();

    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = "SELECT * FROM Videojuegos";

    using var reader = cmd.ExecuteReader();

    while(reader.Read())
    {
        videojuegos.Add(new Videojuego
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Genero = reader.GetString(2),
            Precio = reader.GetDecimal(3),
            AnioLanzamiento = reader.GetInt32(4),
            IdDesarrollador = reader.GetInt32(5)
        });
    }

    return videojuegos;
}

    public Videojuego? ObtenerPorId(int id)
{
    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
        SELECT *
        FROM Videojuegos
        WHERE Id = $id";

    cmd.Parameters.AddWithValue("$id", id);

    using var reader = cmd.ExecuteReader();

    if (reader.Read())
    {
        return new Videojuego
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Genero = reader.GetString(2),
            Precio = Convert.ToDecimal(reader.GetDouble(3)),
            AnioLanzamiento = reader.GetInt32(4),
            IdDesarrollador = reader.GetInt32(5)
        };
    }

    return null;
}

    public Videojuego Crear(VideojuegoDTO dto)
    {
        var videojuego = new Videojuego
        {
            Nombre = dto.Nombre,
            Genero = dto.Genero,
            Precio = dto.Precio,
            AnioLanzamiento = dto.AnioLanzamiento,
            IdDesarrollador = dto.IdDesarrollador
        };

        using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
    INSERT INTO Videojuegos
    (Nombre, Genero, Precio, AnioLanzamiento, IdDesarrollador)
    VALUES
    ($nombre, $genero, $precio, $anio, $idDesarrollador)";
        
    cmd.Parameters.AddWithValue("$nombre", dto.Nombre);
    cmd.Parameters.AddWithValue("$genero", dto.Genero);
    cmd.Parameters.AddWithValue("$precio", dto.Precio);
    cmd.Parameters.AddWithValue("$anio", dto.AnioLanzamiento);
    cmd.Parameters.AddWithValue("$idDesarrollador", dto.IdDesarrollador);

    cmd.ExecuteNonQuery();

        return videojuego;
    }

  public bool Actualizar(int id, VideojuegoDTO dto)
{
    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
        UPDATE Videojuegos
        SET Nombre = $nombre,
            Genero = $genero,
            Precio = $precio,
            AnioLanzamiento = $anio,
            IdDesarrollador = $idDesarrollador
        WHERE Id = $id";

    cmd.Parameters.AddWithValue("$nombre", dto.Nombre);
    cmd.Parameters.AddWithValue("$genero", dto.Genero);
    cmd.Parameters.AddWithValue("$precio", dto.Precio);
    cmd.Parameters.AddWithValue("$anio", dto.AnioLanzamiento);
    cmd.Parameters.AddWithValue("$idDesarrollador", dto.IdDesarrollador);
    cmd.Parameters.AddWithValue("$id", id);

    int filasAfectadas = cmd.ExecuteNonQuery();

    return filasAfectadas > 0;
}

public bool Eliminar(int id)
{
    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
        DELETE FROM Videojuegos
        WHERE Id = $id";

    cmd.Parameters.AddWithValue("$id", id);

    int filasAfectadas = cmd.ExecuteNonQuery();

    return filasAfectadas > 0;
}
}