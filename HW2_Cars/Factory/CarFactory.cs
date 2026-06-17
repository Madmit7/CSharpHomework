using HW2_Cars.Cars;
using HW2_Cars.Enums;
using HW2_Cars.Interfaces;

namespace HW2_Cars.Factory;

public static class CarFactory
{
    public static ICar Create(CarType carType) => carType switch
    {
        CarType.Tesla  => new Tesla(),
        CarType.BMW    => new BMW(),
        CarType.Toyota => new Toyota(),
        CarType.Ford   => new Ford(),
        _ => throw new ArgumentException($"Неизвестный тип автомобиля: {carType}")
    };

    public static bool TryParse(string input, out CarType carType) =>
        Enum.TryParse(input, ignoreCase: true, out carType);
}
