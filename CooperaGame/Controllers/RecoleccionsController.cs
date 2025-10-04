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
using CooperaGame.Services;

namespace CooperaGame.Controllers
{
    public class RecoleccionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PartidaService _partidaService;

        public RecoleccionsController(ApplicationDbContext context, PartidaService partidaService)
        {
            _context = context;
            _partidaService = partidaService;
        }

        // GET: Recoleccions
        /*public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recolecciones.Include(r => r.Jugador).Include(r => r.Partida);
            return View(await applicationDbContext.ToListAsync());
        }*/
       
        // GET: Recoleccions/Create
        /*public IActionResult Crear()
        {
            ViewData["JugadorId"] = new SelectList(_context.Jugadores, "Id", "Nombre");
            ViewData["PartidaId"] = new SelectList(_context.Partidas, "Id", "Id");
            return View();
        }*/
        
        [HttpPost]        
        public async Task<IActionResult> Crear([FromBody] RecoleccionDTO recoleccionDTO) 
        {
            Jugador? jugador = _context.Jugadores.FirstOrDefault(j => j.Nombre == recoleccionDTO.NombreJugador);
            if(jugador == null)
            {
                return BadRequest("El jugador no existe.");
            }

            // si no se alcanzó la meta del recurso guarda la recoleccion
            if(await _partidaService.metaRecursoAlcanzada(recoleccionDTO.PartidaId, recoleccionDTO.Recurso) == false)
            {
                Recoleccion recoleccion = new Recoleccion();
                recoleccion.JugadorId = jugador.Id;
                recoleccion.Recurso = recoleccionDTO.Recurso;
                recoleccion.PartidaId = recoleccionDTO.PartidaId;

                _context.Add(recoleccion);
                await _context.SaveChangesAsync();

                // cada vez que se guarda un registro recoleccion chequeamos si se cumplieron todas las metas, si true, partida.Estado pasa a ser "finalizada"
                await _partidaService.setearEstadoPartidaFinalizadaCuandoTodasLasMetasSeCumplen(recoleccionDTO.PartidaId); 
            }           
           
            // se reedirige al action "Index" en PartidasController
            string? url = Url.Action("Index", "Partidas", new { id = recoleccionDTO.PartidaId });
            return Ok(new { redirectUrl = url });
        }
        
        /*private bool RecoleccionExists(int id)
        {
            return _context.Recolecciones.Any(e => e.Id == id);
        }*/
    }
}
