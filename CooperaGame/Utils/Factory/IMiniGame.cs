using System.Diagnostics;
using CooperaGame.Data;
using CooperaGame.Models;
using Microsoft.AspNetCore.Mvc;


    namespace CooperaGame.Utils.Factory
    {
        public interface IMiniGame
        {
            public IActionResult MostrarJuego();

        }
    }
