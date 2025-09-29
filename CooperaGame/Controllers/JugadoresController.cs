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
    public class JugadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JugadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jugadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jugadores.ToListAsync());
        }

        // GET: Jugadores/Details/5
       /* public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jugador = await _context.Jugadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jugador == null)
            {
                return NotFound();
            }

            return View(jugador);
        }*/

        // GET: Jugadores/Create
        /*public IActionResult Create()
        {
            return View();
        }*/

        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(int idPartida, Jugador jugador)
        {
            Console.WriteLine($"Nombre: {jugador.Nombre}");
            Console.WriteLine($"id: {idPartida}");
            if (ModelState.IsValid)
            {
                _context.Add(jugador);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Partidas", new {id = idPartida});
            }

            ModelState.AddModelError("Nombre", "El nombre debe contener min 1 caracter, max 20 caracteres");
            return View(jugador);
        }

  
    }
}
