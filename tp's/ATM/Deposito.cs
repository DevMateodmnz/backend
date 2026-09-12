namespace CajeroAutomatico
{
    public class Deposito : Transaccion
    {
        private RanuraDeposito ranuraDeposito;

        public Deposito(CuentaBancaria cuenta, Pantalla pantalla, Teclado teclado,
            BaseDeDatosBanco baseDeDatos, RanuraDeposito ranuraDeposito)
            : base(cuenta, pantalla, teclado, baseDeDatos)
        {
            this.ranuraDeposito = ranuraDeposito;
        }

        public override void Ejecutar()
        {
            Pantalla.MostrarMensaje("Ingrese el monto en centavos o 0 para cancelar: ");
            int? montoEnCentavos = Teclado.ObtenerEntrada();

            if (!montoEnCentavos.HasValue || montoEnCentavos.Value == 0)
            {
                return;
            }

            if (montoEnCentavos.Value < 0)
            {
                Pantalla.MostrarMensajeLinea("El monto debe ser mayor que cero.");
                return;
            }

            decimal monto = montoEnCentavos.Value / 100m;
            Pantalla.MostrarMensajeLinea("Introduzca el sobre de depósito en la ranura.");
            Pantalla.MostrarMensajeLinea("Simulación: 1 - sobre recibido dentro de 2 minutos; 2 - no recibido.");
            Pantalla.MostrarMensaje("Seleccione una opción: ");
            int? confirmacion = Teclado.ObtenerEntrada();

            if (!confirmacion.HasValue)
            {
                return;
            }

            if (ProcesarDeposito(monto, confirmacion.Value))
            {
                Pantalla.MostrarMensajeLinea("El depósito fue acreditado al saldo total de su cuenta.");
            }
            else
            {
                Pantalla.MostrarMensajeLinea("La transacción fue cancelada por inactividad.");
            }
        }

        public bool ProcesarDeposito(decimal monto, int confirmacionUsuario)
        {
            if (monto <= 0 || !ranuraDeposito.RecibirSobre(confirmacionUsuario))
            {
                return false;
            }

            BaseDeDatos.AcreditarDeposito(Cuenta, monto);
            return true;
        }
    }
}
