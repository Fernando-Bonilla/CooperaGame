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
        public void PruebaGeneradorNumAleatorioValorMinDiezValorMaxCien()
        {
            // Arrange
            Partida partida1 = new Partida(10);
            Partida partida2 = new Partida(10);

            // Act
            partida1.GenerarMetasRecursos();
            partida2.GenerarMetasRecursos();

            // Assert
            Assert.InRange(partida1.CantMadera, 10, 15);
            Assert.InRange(partida2.CantMadera, 10, 15);
        }

        [Fact]
        public void PruebaGeneradorNumAleatorioValorIgualUsandoMismoSeed()
        {
            // Arrange
            Partida partida1 = new Partida(10);
            Partida partida2 = new Partida(10);

            // Act
            partida1.GenerarMetasRecursos();
            partida2.GenerarMetasRecursos();

            // Assert
            Assert.Equal(partida1.CantMadera, partida2.CantMadera);            
        }

        [Fact]
        public void GenerarMetasRecursosPartidaEntreValorMinimoYValorMaximo()
        {
            // Arrange
            Partida partida = new Partida();

            // Act
            partida.GenerarMetasRecursos();

            //Assert
            Assert.InRange(partida.CantMadera, 10, 15);
            Assert.InRange(partida.CantPiedra, 10, 15);
            Assert.InRange(partida.CantComida, 10, 15);
        }

    }
}
