using CooperaGame.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        /*public async int metaRecurso(int idPartida, string recurso)
        {

        }*/

        // uno que chequee la cantidad y lo compare con la meta
        public bool metaRecursoAlcanzada(int idPartida, string recurso)
        {
            
            return false;
        }

    }
}
