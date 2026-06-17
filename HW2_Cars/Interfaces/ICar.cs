namespace HW2_Cars.Interfaces;

public interface ICar
{
    string Brand { get; }
    int SeatsCount { get; }
    string GetDescription();
}
