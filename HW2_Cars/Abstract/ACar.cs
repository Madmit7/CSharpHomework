using HW2_Cars.Interfaces;

namespace HW2_Cars.Abstract;

public abstract class ACar : ICar
{
    public abstract string Brand { get; }
    public abstract int SeatsCount { get; }
    public abstract string GetDescription();

    protected string FormatSeats() =>
        SeatsCount == 1 ? "1 место" : $"{SeatsCount} места";
}
