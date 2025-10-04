using CooperaGame.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CooperaGame.Models;

namespace CooperaGame.Services
{
    public class PartidaService
    {
        private readonly ApplicationDbContext _context;
        public PartidaService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        // metodos que voy a precisar

        // uno que me diga cuanto va recolectado de cada recurso
        public async Task<int> cantidadRecursoRecoletado(int idPartida, string recurso)
        {          
            int cantidad = await _context.Recolecciones
                .Where(r => r.PartidaId == idPartida && r.Recurso == recurso)
                .CountAsync();

            return cantidad;
        }        

       // uno que segun el recurso llame al que corresponda
        public async Task<bool> metaRecursoAlcanzada(int idPartida, string recurso)
        {
            Partida? partida = await _context.Partidas
                .FirstOrDefaultAsync(p => p.Id == idPartida);

            if (recurso == "wood")
            {
                return partida.CantMadera == await cantidadRecursoRecoletado(idPartida, recurso); 
            }
            else if(recurso == "food")
            {
                return partida.CantComida == await cantidadRecursoRecoletado(idPartida, recurso);
            }
            else
            {
                return partida.CantPiedra == await cantidadRecursoRecoletado(idPartida, recurso);
            }
           
        }       

        public async Task setearEstadoPartidaFinalizadaCuandoTodasLasMetasSeCumplen(int idPartida)
        {
            Partida? partida = await _context.Partidas
                .FirstOrDefaultAsync(p => p.Id == idPartida);

            if(partida.CantMadera == await cantidadRecursoRecoletado(idPartida, "wood") &&
                partida.CantComida == await cantidadRecursoRecoletado(idPartida, "food") &&
                partida.CantPiedra == await cantidadRecursoRecoletado(idPartida, "stone"))
            {
                partida.Estado = "finalizada";
                _context.Update(partida);
                await _context.SaveChangesAsync();
            }
        }

    }
}
