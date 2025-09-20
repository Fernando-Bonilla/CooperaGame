using CooperaGame.Models;
using Microsoft.AspNetCore.Mvc;
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
        //public DbSet<Jugador> Jugadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partida>()
                .HasKey(p => p.Id);
        }

    }
}
