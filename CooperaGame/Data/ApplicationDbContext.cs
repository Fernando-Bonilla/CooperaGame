using CooperaGame.Models;
using Microsoft.EntityFrameworkCore;

namespace CooperaGame.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        // Aca agregar las tablas a mapear
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Recoleccion> Recolecciones {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partida>()
                .HasKey(p => p.Id);            


            modelBuilder.Entity<Jugador>()
                .HasKey(j => j.Id);            


            modelBuilder.Entity<Recoleccion>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Recoleccion>()
                .HasOne(r => r.Partida)
                .WithMany(p => p.Recoleccion) // List en Partida
                .HasForeignKey(r => r.PartidaId); // FK de Partida en Recol.

            modelBuilder.Entity<Recoleccion>()
                .HasOne(r => r.Jugador)
                .WithMany(j => j.Recoleccion)
                .HasForeignKey(r => r.JugadorId);
        }

    }
}
