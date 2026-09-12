using System;

namespace CajeroAutomatico
{
    public class Teclado
    {
        public int? ObtenerEntrada()
        {
            while (true)
            {
                string entrada = Console.ReadLine();

                if (entrada == null)
                {
                    return null;
                }

                int numero;
                if (int.TryParse(entrada, out numero))
                {
                    return numero;
                }

                Console.WriteLine("Entrada inválida. Ingrese solamente un número.");
            }
        }
    }
}
