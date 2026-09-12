namespace CajeroAutomatico
{
    public class CuentaBancaria
    {
        public int NumeroCuenta { get; private set; }
        public int Nip { get; private set; }
        public decimal SaldoDisponible { get; private set; }
        public decimal SaldoTotal { get; private set; }

        public CuentaBancaria(int numeroCuenta, int nip, decimal saldoDisponible,
            decimal saldoTotal)
        {
            NumeroCuenta = numeroCuenta;
            Nip = nip;
            SaldoDisponible = saldoDisponible;
            SaldoTotal = saldoTotal;
        }

        public bool ValidarNip(int nip)
        {
            return Nip == nip;
        }

        public void AcreditarDeposito(decimal monto)
        {
            if (monto > 0)
            {
                SaldoTotal = SaldoTotal + monto;
            }
        }

        public bool Debitar(decimal monto)
        {
            if (monto > 0 && monto <= SaldoDisponible)
            {
                SaldoDisponible = SaldoDisponible - monto;
                SaldoTotal = SaldoTotal - monto;
                return true;
            }

            return false;
        }
    }
}
