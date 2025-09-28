using System.Diagnostics;
using CooperaGame.Data;
using CooperaGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace CooperaGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Partida? ultimaPartida = _context.Partidas.FirstOrDefault(p => p.Estado == "Activa");

            if (ultimaPartida != null)
            {
                return RedirectToAction("Index", "Partidas");
            }

            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
