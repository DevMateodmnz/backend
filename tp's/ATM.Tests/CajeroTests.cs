using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CajeroAutomatico.Tests
{
    [TestClass]
    public class CajeroTests
    {
        [TestMethod]
        public void AutenticarUsuario_CredencialesCorrectas_RetornaCuenta()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 100m, 100m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });

            // Act
            CuentaBancaria cuentaAutenticada = baseDeDatos.AutenticarUsuario(12345, 54321);

            // Assert
            Assert.AreEqual(cuenta, cuentaAutenticada);
        }

        [TestMethod]
        public void AutenticarUsuario_NipIncorrecto_RetornaNull()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 100m, 100m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });

            // Act
            CuentaBancaria cuentaAutenticada = baseDeDatos.AutenticarUsuario(12345, 11111);

            // Assert
            Assert.IsNull(cuentaAutenticada);
        }

        [TestMethod]
        public void RealizarRetiro_MontoValido_DescuentaAmbosSaldosYBilletes()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 100m, 120m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });
            DispensadorEfectivo dispensador = new DispensadorEfectivo(3);
            Retiro retiro = new Retiro(cuenta, new Pantalla(), new Teclado(), baseDeDatos,
                dispensador);

            // Act
            bool seRetiro = retiro.RealizarRetiro(40m);

            // Assert
            Assert.IsTrue(seRetiro);
            Assert.AreEqual(60m, cuenta.SaldoDisponible);
            Assert.AreEqual(80m, cuenta.SaldoTotal);
            Assert.AreEqual(1, dispensador.CantidadBilletes);
        }

        [TestMethod]
        public void RealizarRetiro_MontoMayorAlSaldoDisponible_NoModificaSaldosNiBilletes()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 20m, 100m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });
            DispensadorEfectivo dispensador = new DispensadorEfectivo(5);
            Retiro retiro = new Retiro(cuenta, new Pantalla(), new Teclado(), baseDeDatos,
                dispensador);

            // Act
            bool seRetiro = retiro.RealizarRetiro(40m);

            // Assert
            Assert.IsFalse(seRetiro);
            Assert.AreEqual(20m, cuenta.SaldoDisponible);
            Assert.AreEqual(100m, cuenta.SaldoTotal);
            Assert.AreEqual(5, dispensador.CantidadBilletes);
        }

        [TestMethod]
        public void RealizarRetiro_DispensadorSinEfectivo_NoModificaSaldos()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 100m, 100m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });
            DispensadorEfectivo dispensador = new DispensadorEfectivo(1);
            Retiro retiro = new Retiro(cuenta, new Pantalla(), new Teclado(), baseDeDatos,
                dispensador);

            // Act
            bool seRetiro = retiro.RealizarRetiro(40m);

            // Assert
            Assert.IsFalse(seRetiro);
            Assert.AreEqual(100m, cuenta.SaldoDisponible);
            Assert.AreEqual(100m, cuenta.SaldoTotal);
            Assert.AreEqual(1, dispensador.CantidadBilletes);
        }

        [TestMethod]
        public void ProcesarDeposito_SobreRecibido_AumentaSoloSaldoTotal()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 100m, 120m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });
            Deposito deposito = new Deposito(cuenta, new Pantalla(), new Teclado(), baseDeDatos,
                new RanuraDeposito());

            // Act
            bool seDeposito = deposito.ProcesarDeposito(25m, 1);

            // Assert
            Assert.IsTrue(seDeposito);
            Assert.AreEqual(100m, cuenta.SaldoDisponible);
            Assert.AreEqual(145m, cuenta.SaldoTotal);
        }

        [TestMethod]
        public void ProcesarDeposito_SobreNoRecibido_NoAcreditaSaldos()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 100m, 120m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });
            Deposito deposito = new Deposito(cuenta, new Pantalla(), new Teclado(), baseDeDatos,
                new RanuraDeposito());

            // Act
            bool seDeposito = deposito.ProcesarDeposito(25m, 2);

            // Assert
            Assert.IsFalse(seDeposito);
            Assert.AreEqual(100m, cuenta.SaldoDisponible);
            Assert.AreEqual(120m, cuenta.SaldoTotal);
        }

        [TestMethod]
        public void ProcesarDeposito_MontoCero_CancelaYNoAcreditaSaldos()
        {
            // Arrange
            CuentaBancaria cuenta = new CuentaBancaria(12345, 54321, 100m, 120m);
            BaseDeDatosBanco baseDeDatos = new BaseDeDatosBanco(
                new List<CuentaBancaria> { cuenta });
            Deposito deposito = new Deposito(cuenta, new Pantalla(), new Teclado(), baseDeDatos,
                new RanuraDeposito());

            // Act
            bool seDeposito = deposito.ProcesarDeposito(0m, 1);

            // Assert
            Assert.IsFalse(seDeposito);
            Assert.AreEqual(100m, cuenta.SaldoDisponible);
            Assert.AreEqual(120m, cuenta.SaldoTotal);
        }
    }
}
