using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CooperaGame.Data;
using CooperaGame.Models;

namespace CooperaGame.Controllers
{
    public class PartidasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Partidas
        public async Task<IActionResult> Index(int id)
        {
            if (id <= 0)
            {                
                return NotFound();
            }

            Partida? partida = await _context.Partidas
                .FindAsync(id);

            if (partida == null) 
            {
                
                return NotFound();
            }

            int cantidadPiedra = _context.Recolecciones
                                    .Where(r => (r.PartidaId == id) && (r.Recurso == "stone"))
                                    .Count();

            int cantidadMadera = _context.Recolecciones
                                    .Where(r => (r.PartidaId == id) && (r.Recurso == "wood"))
                                    .Count();

            int cantidadComida = _context.Recolecciones
                                    .Where(r => (r.PartidaId == id) && (r.Recurso == "food"))
                                    .Count();

            ViewBag.CantPiedra = cantidadPiedra;
            ViewBag.CantMadera = cantidadMadera;
            ViewBag.CantComida = cantidadComida;

            return View(partida);
        }

        // POST: Partidas/Crear
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]        
        public async Task<IActionResult> Crear()
        {
            Partida partida = new Partida();
            partida.GenerarMetasRecursos();

            _context.Add(partida);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = partida.Id });
          
        }*/

        public async Task<int> CrearPartida()
        {
            Partida partida = new Partida();
            partida.GenerarMetasRecursos();

            try
            {
                partida.Estado = "activa";
                _context.Add(partida);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }            

            return partida.Id;            
        }

        private bool PartidaExists(int id)
        {
            return _context.Partidas.Any(e => e.Id == id);
        }

        private async Task<bool> PartidaEnCurso(int id) // Este metodo puede que lo tengamos que mover a ServicePartida
        {
            if(id <= 0)
            {
                return false;
            }
            Partida? partida = await _context.Partidas.FindAsync(id);

            if (partida == null) 
            {
                return false;
            }

            if(partida.Estado != "finalizada")
            {
                return true;
            }

            return false;
        }

        public async Task<int> ObtenerIdUltimaPartida()
        {
            Partida partida = await _context.Partidas
                .OrderByDescending(p => p.Id)
                .FirstAsync();

            return partida.Id;
        }
        
        public async Task<IActionResult> MetodoPadre()
        {
            // Obtengo la ultima partida
            int idPartida = await ObtenerIdUltimaPartida();

            // Chequeo si esa partida esta en estado jugando
            if(!await PartidaEnCurso(idPartida)) // Si no tiene estado jugando creo una nueva partida
            {
                int idNuevaPartida = await CrearPartida();
                return RedirectToAction("Index", new { id = idNuevaPartida });
            }
            else // Si tiene estado jugando reedirijo a index con esa partida id
            {
               return RedirectToAction("Index", new {id = idPartida});
            }
            
        }
    }
}
