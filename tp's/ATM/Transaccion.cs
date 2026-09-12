namespace CajeroAutomatico
{
    public abstract class Transaccion
    {
        protected CuentaBancaria Cuenta { get; private set; }
        protected Pantalla Pantalla { get; private set; }
        protected Teclado Teclado { get; private set; }
        protected BaseDeDatosBanco BaseDeDatos { get; private set; }

        public Transaccion(CuentaBancaria cuenta, Pantalla pantalla, Teclado teclado,
            BaseDeDatosBanco baseDeDatos)
        {
            Cuenta = cuenta;
            Pantalla = pantalla;
            Teclado = teclado;
            BaseDeDatos = baseDeDatos;
        }

        public abstract void Ejecutar();
    }
}
