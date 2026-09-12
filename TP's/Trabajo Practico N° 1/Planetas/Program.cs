using System;

namespace Planetas
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Planeta tierra = new Planeta("Tierra", 1, 5.972E24, 1.08321E12, 12742, 150,
                TipoPlaneta.TERRESTRE, true);
            Planeta jupiter = new Planeta("Júpiter", 95, 1.898E27, 1.43128E15, 139820, 778,
                TipoPlaneta.GASEOSO, true);

            Console.WriteLine("Planeta 1");
            tierra.MostrarInformacion();
            Console.WriteLine("Densidad: " + tierra.CalcularDensidad() + " kg/km3");
            Console.WriteLine("¿Es un planeta exterior?: " + tierra.EsPlanetaExterior());

            Console.WriteLine();
            Console.WriteLine("Planeta 2");
            jupiter.MostrarInformacion();
            Console.WriteLine("Densidad: " + jupiter.CalcularDensidad() + " kg/km3");
            Console.WriteLine("¿Es un planeta exterior?: " + jupiter.EsPlanetaExterior());
        }
    }
}
