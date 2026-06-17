using HW2_Cars.Abstract;
using HW2_Cars.Interfaces;

namespace HW2_Cars.Cars;

public class BMW : ACar, IMechanical, IAutomaticTransmission
{
    public override string Brand => "BMW";
    public override int SeatsCount => 5;
    public string FuelType => "бензин";
    public double EngineVolumeL => 2.0;

    public override string GetDescription() =>
        $"{Brand}: автомобиль с {((IAutomaticTransmission)this).TransmissionType}, " +
        $"{FormatSeats()}, двигатель {EngineVolumeL}л ({FuelType})";
}
