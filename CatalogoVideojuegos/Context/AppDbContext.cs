using Microsoft.EntityFrameworkCore;
using CatalogoVideojuegos.Models;

namespace CatalogoVideojuegos.Context;

public class AppDbContext : DbContext
{
    public DbSet<Desarrollador> Desarrolladores { get; set; }
    public DbSet<Videojuego> Videojuegos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Videojuego>()
            .HasOne(v => v.Desarrollador)
            .WithMany(d => d.Videojuegos)
            .HasForeignKey(v => v.IdDesarrollador);
    }
}