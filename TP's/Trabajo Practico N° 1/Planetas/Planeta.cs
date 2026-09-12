using System;

namespace Planetas
{
    public class Planeta
    {
        private const double KilometrosPorUnidadAstronomica = 149597870;

        public string Nombre { get; set; }
        public int CantidadSatelites { get; set; }
        public double Masa { get; set; }
        public double Volumen { get; set; }
        public int Diametro { get; set; }
        public int DistanciaMediaAlSol { get; set; }
        public TipoPlaneta Tipo { get; set; }
        public bool ObservableASimpleVista { get; set; }

        public Planeta()
            : this(null, 0, 0, 0, 0, 0, TipoPlaneta.GASEOSO, false)
        {
        }

        public Planeta(string nombre, int cantidadSatelites, double masa, double volumen,
            int diametro, int distanciaMediaAlSol, TipoPlaneta tipo,
            bool observableASimpleVista)
        {
            Nombre = nombre;
            CantidadSatelites = cantidadSatelites;
            Masa = masa;
            Volumen = volumen;
            Diametro = diametro;
            DistanciaMediaAlSol = distanciaMediaAlSol;
            Tipo = tipo;
            ObservableASimpleVista = observableASimpleVista;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Cantidad de satélites: " + CantidadSatelites);
            Console.WriteLine("Masa (kg): " + Masa);
            Console.WriteLine("Volumen (km3): " + Volumen);
            Console.WriteLine("Diámetro (km): " + Diametro);
            Console.WriteLine("Distancia media al Sol (millones de km): " + DistanciaMediaAlSol);
            Console.WriteLine("Tipo: " + Tipo);
            Console.WriteLine("Observable a simple vista: " + ObservableASimpleVista);
        }

        public double CalcularDensidad()
        {
            if (Volumen == 0)
            {
                return 0;
            }

            return Masa / Volumen;
        }

        public bool EsPlanetaExterior()
        {
            double distanciaEnKilometros = DistanciaMediaAlSol * 1000000d;
            double limiteExteriorCinturon = 3.4 * KilometrosPorUnidadAstronomica;

            return distanciaEnKilometros > limiteExteriorCinturon;
        }
    }
}
