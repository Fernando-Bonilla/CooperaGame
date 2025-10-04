using CooperaGame.Models;
using CooperaGame.Services;
using static CooperaGame.Services.EstadisticasService;

namespace CooperaGame.Views.Partidas
{
    public class PartidaViewModel
    {
        public Partida? Partida {  get; set; }

        public TimeSpan DuracionPartida { get; set; } = new TimeSpan();

        public List<EstadisticaJugador>? Estadisticas { get; set; }
    }
}
