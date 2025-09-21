using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CooperaGame;
using CooperaGame.Models;

namespace CooperaGame.Tests.Models
{
    public class PartidaTest
    {

        [Fact]
        public void testPruebaPartida()
        {
            // Arrange
            Partida partida = new Partida();

            // Act
            string resultado = partida.PruebaParaTest();

            //Assert
            Assert.Equal("Holita", resultado);

        }
    }
}
