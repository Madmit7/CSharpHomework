using HW2_Cars.Abstract;
using HW2_Cars.Interfaces;

namespace HW2_Cars.Cars;

public class Toyota : ACar, IMechanical, IManualTransmission
{
    public override string Brand => "Toyota";
    public override int SeatsCount => 5;
    public string FuelType => "бензин";
    public double EngineVolumeL => 1.6;

    public override string GetDescription() =>
        $"{Brand}: автомобиль с {((IManualTransmission)this).TransmissionType}, " +
        $"{FormatSeats()}, двигатель {EngineVolumeL}л ({FuelType})";
}
