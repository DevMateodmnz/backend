using Microsoft.VisualStudio.TestTools.UnitTesting;
using Planetas;

namespace Planetas.Tests
{
    [TestClass]
    public class PlanetaTests
    {
        [TestMethod]
        public void ConstructorSinParametros_ValoresIniciales_CreaPlanetaConValoresDelEnunciado()
        {
            // Arrange
            Planeta planeta = new Planeta();

            // Act
            double densidad = planeta.CalcularDensidad();

            // Assert
            Assert.IsNull(planeta.Nombre);
            Assert.AreEqual(0, planeta.CantidadSatelites);
            Assert.AreEqual(0d, planeta.Masa);
            Assert.AreEqual(0d, planeta.Volumen);
            Assert.AreEqual(0, planeta.Diametro);
            Assert.AreEqual(0, planeta.DistanciaMediaAlSol);
            Assert.IsFalse(planeta.ObservableASimpleVista);
            Assert.AreEqual(0d, densidad);
        }

        [TestMethod]
        public void CalcularDensidad_MasaYVolumen_RetornaCociente()
        {
            // Arrange
            Planeta planeta = new Planeta("Prueba", 0, 100, 20, 10, 100,
                TipoPlaneta.TERRESTRE, false);

            // Act
            double densidad = planeta.CalcularDensidad();

            // Assert
            Assert.AreEqual(5d, densidad);
        }

        [TestMethod]
        public void CalcularDensidad_VolumenCero_RetornaCero()
        {
            // Arrange
            Planeta planeta = new Planeta("Prueba", 0, 100, 0, 10, 100,
                TipoPlaneta.ENANO, false);

            // Act
            double densidad = planeta.CalcularDensidad();

            // Assert
            Assert.AreEqual(0d, densidad);
        }

        [TestMethod]
        public void EsPlanetaExterior_DistanciaMayorAlCinturon_RetornaTrue()
        {
            // Arrange
            Planeta planeta = new Planeta("Júpiter", 95, 1, 1, 1, 778,
                TipoPlaneta.GASEOSO, true);

            // Act
            bool esExterior = planeta.EsPlanetaExterior();

            // Assert
            Assert.IsTrue(esExterior);
        }

        [TestMethod]
        public void EsPlanetaExterior_DistanciaMenorAlCinturon_RetornaFalse()
        {
            // Arrange
            Planeta planeta = new Planeta("Tierra", 1, 1, 1, 1, 150,
                TipoPlaneta.TERRESTRE, true);

            // Act
            bool esExterior = planeta.EsPlanetaExterior();

            // Assert
            Assert.IsFalse(esExterior);
        }
    }
}
