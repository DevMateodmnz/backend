namespace CajeroAutomatico
{
    public class ConsultaSaldo : Transaccion
    {
        public ConsultaSaldo(CuentaBancaria cuenta, Pantalla pantalla, Teclado teclado,
            BaseDeDatosBanco baseDeDatos)
            : base(cuenta, pantalla, teclado, baseDeDatos)
        {
        }

        public override void Ejecutar()
        {
            Pantalla.MostrarMensajeLinea("Saldo disponible: $" + Cuenta.SaldoDisponible);
            Pantalla.MostrarMensajeLinea("Saldo total: $" + Cuenta.SaldoTotal);
        }
    }
}
