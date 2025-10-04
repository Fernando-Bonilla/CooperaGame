using CooperaGame.Data;
using Microsoft.EntityFrameworkCore;
using CooperaGame.Models;

namespace CooperaGame.Services
{
    public class EstadisticasService
    {
        private readonly ApplicationDbContext _context;
        public EstadisticasService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<EstadisticaJugador>>obtenerEstadisticasDeLaPartida(int partidaId)
        {
            // Primero busco todos los Ids de jugadores en esa partida
            List<int> jugadoresId = await _context.Recolecciones
                .Where(recol => recol.PartidaId == partidaId)                
                .Select(recol => recol.JugadorId)
                .Distinct()
                .ToListAsync();

            List<EstadisticaJugador> jugadoresEstadisticas = new List<EstadisticaJugador>();

            foreach(int id in jugadoresId)
            {
                EstadisticaJugador estadisticaJugador = new EstadisticaJugador();
                estadisticaJugador.Nombre = await _context.Jugadores
                    .Where(jug => jug.Id == id)
                    .Select(jug => jug.Nombre)
                    .FirstOrDefaultAsync() ?? "Algo vino mal";

                estadisticaJugador.CantMaderaRecolectada = await _context.Recolecciones
                    .CountAsync(recol => recol.PartidaId == partidaId && 
                    recol.JugadorId == id && 
                    recol.Recurso == "wood");

                estadisticaJugador.CantPiedraRecolectada = await _context.Recolecciones
                    .CountAsync(recol => recol.PartidaId == partidaId &&
                    recol.JugadorId == id &&
                    recol.Recurso == "stone");

                estadisticaJugador.CantComidaRecolectada = await _context.Recolecciones
                   .CountAsync(recol => recol.PartidaId == partidaId &&
                   recol.JugadorId == id &&
                   recol.Recurso == "food");

                estadisticaJugador.TotalRecursosRecolectados = estadisticaJugador.CantMaderaRecolectada + estadisticaJugador.CantComidaRecolectada + estadisticaJugador.CantPiedraRecolectada;

                jugadoresEstadisticas.Add(estadisticaJugador);
            }

            return jugadoresEstadisticas;
            
        }

        public async Task<TimeSpan> obtenerDuracionPartida(int partidaId)
        {
            DateTime? primerRegistro = await _context.Recolecciones
                .Where(rec => rec.PartidaId == partidaId)
                .Select(rec => rec.Fecha)
                .FirstOrDefaultAsync();

            DateTime? ultimoRegistro = await _context.Recolecciones
                .Where(rec => rec.PartidaId == partidaId)
                .OrderByDescending(rec => rec.Fecha)
                .Select(rec => rec.Fecha)
                .FirstOrDefaultAsync();

            TimeSpan duracionPartida = TimeSpan.Zero;
            if (ultimoRegistro == null || primerRegistro == null)
            {
                return duracionPartida;
            }
            else
            {
                return duracionPartida = (TimeSpan)(ultimoRegistro - primerRegistro);
            }             

        }

        // clase anidada para poder pasar la lista EstadisticaJugador estadisticaJugador = new EstadisticaJugador() con las propiedades que necesitamos mostrar en la view
        public class EstadisticaJugador()
        {
            public string Nombre {  get; set; } = string.Empty;
            public int CantMaderaRecolectada { get; set; } = 0;
            public int CantPiedraRecolectada { get; set; } = 0;
            public int CantComidaRecolectada { get; set; } = 0;
            public int TotalRecursosRecolectados { get; set; } = 0;

        }
    }
}
