using CatalogoVideojuegos.DTOs;
using CatalogoVideojuegos.Models;
using Microsoft.Data.Sqlite;

namespace CatalogoVideojuegos.Services;

public class DesarrolladorService : IDesarrolladorService
{
    private readonly string connectionString = "Data Source=videojuegos.db";

public List<Desarrollador> ObtenerTodos()
{
    var desarrolladores = new List<Desarrollador>();

    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = "SELECT * FROM Desarrolladores";

    using var reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        desarrolladores.Add(new Desarrollador
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Pais = reader.GetString(2),
            AnioFundacion = reader.GetInt32(3)
        });
    }

    return desarrolladores;
}

public Desarrollador? ObtenerPorId(int id)
{
    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
        SELECT *
        FROM Desarrolladores
        WHERE Id = $id";

    cmd.Parameters.AddWithValue("$id", id);

    using var reader = cmd.ExecuteReader();

    if (reader.Read())
    {
        return new Desarrollador
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Pais = reader.GetString(2),
            AnioFundacion = reader.GetInt32(3)
        };
    }

    return null;
}

public Desarrollador Crear(DesarrolladorDTO dto)
{
    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
        INSERT INTO Desarrolladores
        (Nombre, Pais, AnioFundacion)
        VALUES
        ($nombre, $pais, $anioFundacion)";

    cmd.Parameters.AddWithValue("$nombre", dto.Nombre);
    cmd.Parameters.AddWithValue("$pais", dto.Pais);
    cmd.Parameters.AddWithValue("$anioFundacion", dto.AnioFundacion);

    cmd.ExecuteNonQuery();

    return new Desarrollador
    {
        Nombre = dto.Nombre,
        Pais = dto.Pais,
        AnioFundacion = dto.AnioFundacion
    };
}

public bool Actualizar(int id, DesarrolladorDTO dto)
{
    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
        UPDATE Desarrolladores
        SET Nombre = $nombre,
            Pais = $pais,
            AnioFundacion = $anioFundacion
        WHERE Id = $id";

    cmd.Parameters.AddWithValue("$nombre", dto.Nombre);
    cmd.Parameters.AddWithValue("$pais", dto.Pais);
    cmd.Parameters.AddWithValue("$anioFundacion", dto.AnioFundacion);
    cmd.Parameters.AddWithValue("$id", id);

    return cmd.ExecuteNonQuery() > 0;
}

public bool Eliminar(int id)
{
    using var conexion = new SqliteConnection(connectionString);

    conexion.Open();

    var cmd = conexion.CreateCommand();

    cmd.CommandText = @"
        DELETE FROM Desarrolladores
        WHERE Id = $id";

    cmd.Parameters.AddWithValue("$id", id);

    return cmd.ExecuteNonQuery() > 0;
}
}