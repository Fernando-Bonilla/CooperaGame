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
                Console.WriteLine($"========== No entra partida null {id}");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear()
        {
            Partida partida = new Partida();
            partida.GenerarMetasRecursos();

            if (ModelState.IsValid)
            {
                partida.Estado = "activa";
                _context.Add(partida);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new {id = partida.Id });
            }
                        
            return RedirectToAction("Index", new { id = partida.Id });
        }       

        private bool PartidaExists(int id)
        {
            return _context.Partidas.Any(e => e.Id == id);
        }
    }
}
