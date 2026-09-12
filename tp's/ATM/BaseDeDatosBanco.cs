using System.Collections.Generic;

namespace CajeroAutomatico
{
    public class BaseDeDatosBanco
    {
        private List<CuentaBancaria> cuentas;

        public BaseDeDatosBanco()
        {
            cuentas = new List<CuentaBancaria>();
            cuentas.Add(new CuentaBancaria(12345, 54321, 1000m, 1200m));
            cuentas.Add(new CuentaBancaria(98765, 56789, 200m, 200m));
        }

        public BaseDeDatosBanco(List<CuentaBancaria> cuentas)
        {
            this.cuentas = cuentas;
        }

        public CuentaBancaria AutenticarUsuario(int numeroCuenta, int nip)
        {
            foreach (CuentaBancaria cuenta in cuentas)
            {
                if (cuenta.NumeroCuenta == numeroCuenta && cuenta.ValidarNip(nip))
                {
                    return cuenta;
                }
            }

            return null;
        }

        public void AcreditarDeposito(CuentaBancaria cuenta, decimal monto)
        {
            cuenta.AcreditarDeposito(monto);
        }

        public bool Debitar(CuentaBancaria cuenta, decimal monto)
        {
            return cuenta.Debitar(monto);
        }
    }
}
