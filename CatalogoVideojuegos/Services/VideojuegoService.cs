using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using Microsoft.Data.Sqlite;

namespace CatalogoVideojuegos.Services;

public class VideojuegoService : IVideojuegoService
{
    private readonly string connectionString = "Data Source=videojuegos.db";

    private SqliteConnection CrearConexion()
    {
        return new SqliteConnection(connectionString);
    }

    private static Videojuego MapearVideojuego(SqliteDataReader reader)
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

    public List<Videojuego> ObtenerTodos()
    {
        var videojuegos = new List<Videojuego>();

        using var conexion = CrearConexion();
        conexion.Open();

        var cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM Videojuegos";

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            videojuegos.Add(MapearVideojuego(reader));
        }

        return videojuegos;
    }

    public Videojuego? ObtenerPorId(int id)
    {
        using var conexion = CrearConexion();
        conexion.Open();

        var cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM Videojuegos WHERE Id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        using var reader = cmd.ExecuteReader();

        return reader.Read() ? MapearVideojuego(reader) : null;
    }

    public Videojuego Crear(VideojuegoDTO dto)
    {
        using var conexion = CrearConexion();
        conexion.Open();

        var cmd = conexion.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO Videojuegos
            (Nombre, Genero, Precio, AnioLanzamiento, IdDesarrollador)
            VALUES
            ($nombre, $genero, $precio, $anio, $idDesarrollador);
            SELECT last_insert_rowid();";

        cmd.Parameters.AddWithValue("$nombre", dto.Nombre);
        cmd.Parameters.AddWithValue("$genero", dto.Genero);
        cmd.Parameters.AddWithValue("$precio", dto.Precio);
        cmd.Parameters.AddWithValue("$anio", dto.AnioLanzamiento);
        cmd.Parameters.AddWithValue("$idDesarrollador", dto.IdDesarrollador);

        var idGenerado = (long)cmd.ExecuteScalar()!;

        return new Videojuego
        {
            Id = (int)idGenerado,
            Nombre = dto.Nombre,
            Genero = dto.Genero,
            Precio = dto.Precio,
            AnioLanzamiento = dto.AnioLanzamiento,
            IdDesarrollador = dto.IdDesarrollador
        };
    }

    public bool Actualizar(int id, VideojuegoDTO dto)
    {
        using var conexion = CrearConexion();
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

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int id)
    {
        using var conexion = CrearConexion();
        conexion.Open();

        var cmd = conexion.CreateCommand();
        cmd.CommandText = "DELETE FROM Videojuegos WHERE Id = $id";
        cmd.Parameters.AddWithValue("$id", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}