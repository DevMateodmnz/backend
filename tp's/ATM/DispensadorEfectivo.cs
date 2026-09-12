namespace CajeroAutomatico
{
    public class DispensadorEfectivo
    {
        private const decimal ValorBillete = 20m;

        public int CantidadBilletes { get; private set; }

        public DispensadorEfectivo()
        {
            CantidadBilletes = 500;
        }

        public DispensadorEfectivo(int cantidadBilletes)
        {
            CantidadBilletes = cantidadBilletes;
        }

        public bool TieneEfectivoSuficiente(decimal monto)
        {
            if (monto <= 0 || monto % ValorBillete != 0)
            {
                return false;
            }

            int billetesNecesarios = (int)(monto / ValorBillete);
            return billetesNecesarios <= CantidadBilletes;
        }

        public bool DispensarEfectivo(decimal monto)
        {
            if (!TieneEfectivoSuficiente(monto))
            {
                return false;
            }

            int billetesNecesarios = (int)(monto / ValorBillete);
            CantidadBilletes = CantidadBilletes - billetesNecesarios;
            return true;
        }
    }
}
