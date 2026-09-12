using System;

namespace CajeroAutomatico
{
    public class Pantalla
    {
        public void MostrarMensaje(string mensaje)
        {
            Console.Write(mensaje);
        }

        public void MostrarMensajeLinea(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }
}
