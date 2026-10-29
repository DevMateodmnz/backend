using System;

public abstract class Cuenta
{
    public string Titular { get; set; }
    public decimal Saldo { get; protected set; }

    public Cuenta(string titular, decimal saldoInicial)
    {
        if (saldoInicial < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(saldoInicial), "El saldo inicial no puede ser negativo.");
        }

        Titular = titular;
        Saldo = saldoInicial;
    }

    public abstract void Depositar(decimal monto);
    public abstract void Retirar(decimal monto);
}

public class CuentaBancaria : Cuenta
{
    protected virtual decimal PorcentajeComisionExterior => 0.07m;

    public CuentaBancaria(string titular, decimal saldoInicial)
        : base(titular, saldoInicial)
    {
    }

    public override void Depositar(decimal monto)
    {
        if (monto <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(monto), "El monto debe ser positivo.");
        }

        Saldo += monto;
    }

    public override void Retirar(decimal monto)
    {
        if (monto <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(monto), "El monto debe ser positivo.");
        }

        if (monto > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente.");
        }

        Saldo -= monto;
    }

    public bool RetirarCajeroExterior(decimal cantidad)
    {
        if (cantidad <= 0 || cantidad > Saldo)
        {
            return false;
        }

        decimal comision = cantidad * PorcentajeComisionExterior;

        if (comision > Saldo - cantidad)
        {
            return false;
        }

        Saldo -= cantidad + comision;
        return true;
    }
}

public class CuentaBancariaPremium : CuentaBancaria
{
    protected override decimal PorcentajeComisionExterior => 0.05m;

    public CuentaBancariaPremium(string titular, decimal saldoInicial)
        : base(titular, saldoInicial)
    {
    }
}
