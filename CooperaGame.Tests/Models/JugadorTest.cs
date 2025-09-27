using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CooperaGame;
using CooperaGame.Models;

namespace CooperaGame.Tests.Models
{
    public class JugadorTest
    {

        [Theory]
        [InlineData("Pepe")]
        [InlineData("Roberto")]
        [InlineData("Roberto09")]
        public void CrearJugadorNombreUsandoCaracteresCorrectos(string nombre)
        {
            // Arrange
            Jugador jugador = new Jugador();
            jugador.Nombre = nombre;

            List<ValidationResult> resultados = new List<ValidationResult>();
            bool valido = Validator.TryValidateObject(jugador, new ValidationContext(jugador), resultados, validateAllProperties: true);

            // Assert
            Assert.True(valido, string.Join(" ", resultados.Select(r => r.ErrorMessage)));
            
        }
    }
}
