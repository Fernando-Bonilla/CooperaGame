using System.Diagnostics;
using CooperaGame.Data;
using CooperaGame.Models;
using Microsoft.AspNetCore.Mvc;

namespace CooperaGame.Controllers
{
    public class MiniGamesController : Controller
    {
        [HttpGet]
        public IActionResult MiniGame_ClickIt()
        {
            return PartialView("_MiniJuegoClick.cshtml");
        }
    }
}
