using CooperaGame.Data;
using CooperaGame.DTO;
using CooperaGame.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CooperaGame.Controllers
{
    public class RecoleccionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecoleccionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recoleccions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recolecciones.Include(r => r.Jugador).Include(r => r.Partida);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recoleccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recoleccion = await _context.Recolecciones
                .Include(r => r.Jugador)
                .Include(r => r.Partida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recoleccion == null)
            {
                return NotFound();
            }

            return View(recoleccion);
        }

        // GET: Recoleccions/Create
        public IActionResult Crear()
        {
            ViewData["JugadorId"] = new SelectList(_context.Jugadores, "Id", "Nombre");
            ViewData["PartidaId"] = new SelectList(_context.Partidas, "Id", "Id");
            return View();
        }

        // POST: Recoleccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Crear([FromBody] RecoleccionDTO recoleccionDTO) //Fecha [Bind("Recurso,JugadorId,PartidaId")] Recoleccion recoleccion
        {
            Jugador? jugador = _context.Jugadores.FirstOrDefault(j => j.Nombre == recoleccionDTO.NombreJugador);
            if(jugador == null)
            {
                return BadRequest("El jugador no existe.");
            }
            Recoleccion recoleccion = new Recoleccion();
            recoleccion.JugadorId = jugador.Id;
            recoleccion.Recurso = recoleccionDTO.Recurso;
            recoleccion.PartidaId = recoleccionDTO.PartidaId;

           
            _context.Add(recoleccion);
            await _context.SaveChangesAsync();

            string? url = Url.Action("Index", "Partidas", new { id = recoleccionDTO.PartidaId });
            return Ok(new { redirectUrl = url });
        }

        // GET: Recoleccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recoleccion = await _context.Recolecciones.FindAsync(id);
            if (recoleccion == null)
            {
                return NotFound();
            }
            ViewData["JugadorId"] = new SelectList(_context.Jugadores, "Id", "Nombre", recoleccion.JugadorId);
            ViewData["PartidaId"] = new SelectList(_context.Partidas, "Id", "Id", recoleccion.PartidaId);
            return View(recoleccion);
        }

        // POST: Recoleccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Recurso,Fecha,JugadorId,PartidaId")] Recoleccion recoleccion)
        {
            if (id != recoleccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recoleccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecoleccionExists(recoleccion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["JugadorId"] = new SelectList(_context.Jugadores, "Id", "Nombre", recoleccion.JugadorId);
            ViewData["PartidaId"] = new SelectList(_context.Partidas, "Id", "Id", recoleccion.PartidaId);
            return View(recoleccion);
        }

        // GET: Recoleccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recoleccion = await _context.Recolecciones
                .Include(r => r.Jugador)
                .Include(r => r.Partida)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recoleccion == null)
            {
                return NotFound();
            }

            return View(recoleccion);
        }

        // POST: Recoleccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recoleccion = await _context.Recolecciones.FindAsync(id);
            if (recoleccion != null)
            {
                _context.Recolecciones.Remove(recoleccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecoleccionExists(int id)
        {
            return _context.Recolecciones.Any(e => e.Id == id);
        }
    }
}
