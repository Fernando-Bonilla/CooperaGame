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

        // uno que chequee la cantidad y lo compare con la meta
        public async Task<bool> metaRecursoAlcanzada(int idPartida, string recurso)
        {
            if(recurso == "wood")
            {
                return await maderaMetaAlcanzada(idPartida, recurso);
            }
            else if(recurso == "food")
            {
                return await comidaMetaAlcanzada(idPartida, recurso);
            }
            else
            {
                return await piedraMetaAlcanzada(idPartida, recurso);
            }

            //return false;
        }

        public async Task<bool> maderaMetaAlcanzada(int idPartida, string recurso)
        {
            Partida? partida = await _context.Partidas
                .FirstOrDefaultAsync(p => p.Id == idPartida);

            int cantidadRecoletado = await cantidadRecursoRecoletado(idPartida, recurso);

            if (partida.CantMadera == cantidadRecoletado)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> comidaMetaAlcanzada(int idPartida, string recurso)
        {
            Partida? partida = await _context.Partidas
                .FirstOrDefaultAsync(p => p.Id == idPartida);

            int cantidadRecoletado = await cantidadRecursoRecoletado(idPartida, recurso);

            if (partida.CantComida == cantidadRecoletado)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> piedraMetaAlcanzada(int idPartida, string recurso)
        {
            Partida? partida = await _context.Partidas
                .FirstOrDefaultAsync(p => p.Id == idPartida);

            int cantidadRecoletado = await cantidadRecursoRecoletado(idPartida, recurso);

            if (partida.CantPiedra == cantidadRecoletado)
            {
                return true;
            }

            return false;
        }

        public 

    }
}
