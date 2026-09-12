namespace CajeroAutomatico
{
    public class Retiro : Transaccion
    {
        private DispensadorEfectivo dispensadorEfectivo;

        public Retiro(CuentaBancaria cuenta, Pantalla pantalla, Teclado teclado,
            BaseDeDatosBanco baseDeDatos, DispensadorEfectivo dispensadorEfectivo)
            : base(cuenta, pantalla, teclado, baseDeDatos)
        {
            this.dispensadorEfectivo = dispensadorEfectivo;
        }

        public override void Ejecutar()
        {
            while (true)
            {
                MostrarMenuRetiro();
                int? seleccion = Teclado.ObtenerEntrada();

                if (!seleccion.HasValue)
                {
                    return;
                }

                if (seleccion.Value == 6)
                {
                    return;
                }

                decimal monto = ObtenerMonto(seleccion.Value);
                if (monto == 0)
                {
                    Pantalla.MostrarMensajeLinea("Selección inválida.");
                    continue;
                }

                if (monto > Cuenta.SaldoDisponible)
                {
                    Pantalla.MostrarMensajeLinea("Saldo insuficiente. Seleccione un monto menor.");
                    continue;
                }

                if (!dispensadorEfectivo.TieneEfectivoSuficiente(monto))
                {
                    Pantalla.MostrarMensajeLinea("El dispensador no tiene efectivo suficiente.");
                    continue;
                }

                if (RealizarRetiro(monto))
                {
                    Pantalla.MostrarMensajeLinea("Retire su dinero del dispensador.");
                    return;
                }
            }
        }

        public bool RealizarRetiro(decimal monto)
        {
            if (monto <= 0 || monto > Cuenta.SaldoDisponible)
            {
                return false;
            }

            if (!dispensadorEfectivo.TieneEfectivoSuficiente(monto))
            {
                return false;
            }

            if (!BaseDeDatos.Debitar(Cuenta, monto))
            {
                return false;
            }

            return dispensadorEfectivo.DispensarEfectivo(monto);
        }

        private void MostrarMenuRetiro()
        {
            Pantalla.MostrarMensajeLinea("\nMenú de retiro");
            Pantalla.MostrarMensajeLinea("1 - $20");
            Pantalla.MostrarMensajeLinea("2 - $40");
            Pantalla.MostrarMensajeLinea("3 - $60");
            Pantalla.MostrarMensajeLinea("4 - $100");
            Pantalla.MostrarMensajeLinea("5 - $200");
            Pantalla.MostrarMensajeLinea("6 - Cancelar transacción");
            Pantalla.MostrarMensaje("Seleccione una opción: ");
        }

        private decimal ObtenerMonto(int seleccion)
        {
            switch (seleccion)
            {
                case 1:
                    return 20m;
                case 2:
                    return 40m;
                case 3:
                    return 60m;
                case 4:
                    return 100m;
                case 5:
                    return 200m;
                default:
                    return 0m;
            }
        }
    }
}
