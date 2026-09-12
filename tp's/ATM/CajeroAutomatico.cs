namespace CajeroAutomatico
{
    public class CajeroAutomatico
    {
        private Pantalla pantalla;
        private Teclado teclado;
        private DispensadorEfectivo dispensadorEfectivo;
        private RanuraDeposito ranuraDeposito;
        private BaseDeDatosBanco baseDeDatos;

        public CajeroAutomatico()
        {
            pantalla = new Pantalla();
            teclado = new Teclado();
            dispensadorEfectivo = new DispensadorEfectivo();
            ranuraDeposito = new RanuraDeposito();
            baseDeDatos = new BaseDeDatosBanco();
        }

        public CajeroAutomatico(Pantalla pantalla, Teclado teclado,
            DispensadorEfectivo dispensadorEfectivo, RanuraDeposito ranuraDeposito,
            BaseDeDatosBanco baseDeDatos)
        {
            this.pantalla = pantalla;
            this.teclado = teclado;
            this.dispensadorEfectivo = dispensadorEfectivo;
            this.ranuraDeposito = ranuraDeposito;
            this.baseDeDatos = baseDeDatos;
        }

        public void Ejecutar()
        {
            while (true)
            {
                CuentaBancaria cuenta = AutenticarUsuario();

                if (cuenta == null)
                {
                    return;
                }

                if (!EjecutarMenuPrincipal(cuenta))
                {
                    return;
                }
            }
        }

        private CuentaBancaria AutenticarUsuario()
        {
            while (true)
            {
                pantalla.MostrarMensajeLinea("\nBienvenido al cajero automático.");
                pantalla.MostrarMensaje("Ingrese su número de cuenta de cinco dígitos: ");
                int? numeroCuenta = teclado.ObtenerEntrada();

                if (!numeroCuenta.HasValue)
                {
                    return null;
                }

                if (!EsNumeroDeCincoDigitos(numeroCuenta.Value))
                {
                    pantalla.MostrarMensajeLinea("El número de cuenta debe tener cinco dígitos.");
                    continue;
                }

                pantalla.MostrarMensaje("Ingrese su NIP de cinco dígitos: ");
                int? nip = teclado.ObtenerEntrada();

                if (!nip.HasValue)
                {
                    return null;
                }

                if (!EsNumeroDeCincoDigitos(nip.Value))
                {
                    pantalla.MostrarMensajeLinea("El NIP debe tener cinco dígitos.");
                    continue;
                }

                CuentaBancaria cuenta = baseDeDatos.AutenticarUsuario(numeroCuenta.Value, nip.Value);
                if (cuenta != null)
                {
                    return cuenta;
                }

                pantalla.MostrarMensajeLinea("Número de cuenta o NIP incorrecto.");
            }
        }

        private bool EjecutarMenuPrincipal(CuentaBancaria cuenta)
        {
            while (true)
            {
                MostrarMenuPrincipal();
                int? seleccion = teclado.ObtenerEntrada();

                if (!seleccion.HasValue)
                {
                    return false;
                }

                Transaccion transaccion;

                switch (seleccion.Value)
                {
                    case 1:
                        transaccion = new ConsultaSaldo(cuenta, pantalla, teclado, baseDeDatos);
                        transaccion.Ejecutar();
                        break;
                    case 2:
                        transaccion = new Retiro(cuenta, pantalla, teclado, baseDeDatos,
                            dispensadorEfectivo);
                        transaccion.Ejecutar();
                        break;
                    case 3:
                        transaccion = new Deposito(cuenta, pantalla, teclado, baseDeDatos,
                            ranuraDeposito);
                        transaccion.Ejecutar();
                        break;
                    case 4:
                        pantalla.MostrarMensajeLinea("Gracias por utilizar el cajero automático.");
                        return true;
                    default:
                        pantalla.MostrarMensajeLinea("Selección inválida. Intente nuevamente.");
                        break;
                }
            }
        }

        private void MostrarMenuPrincipal()
        {
            pantalla.MostrarMensajeLinea("\nMenú principal");
            pantalla.MostrarMensajeLinea("1 - Consultar saldo");
            pantalla.MostrarMensajeLinea("2 - Retirar efectivo");
            pantalla.MostrarMensajeLinea("3 - Depositar fondos");
            pantalla.MostrarMensajeLinea("4 - Salir del sistema");
            pantalla.MostrarMensaje("Seleccione una opción: ");
        }

        private bool EsNumeroDeCincoDigitos(int numero)
        {
            return numero >= 10000 && numero <= 99999;
        }
    }
}
