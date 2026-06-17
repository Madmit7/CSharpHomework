using HW2_Cars.Abstract;
using HW2_Cars.Interfaces;

namespace HW2_Cars.Cars;

public class Ford : ACar, IMechanical, IManualTransmission
{
    public override string Brand => "Ford";
    public override int SeatsCount => 5;
    public string FuelType => "дизель";
    public double EngineVolumeL => 2.5;

    public override string GetDescription() =>
        $"{Brand}: автомобиль с {((IManualTransmission)this).TransmissionType}, " +
        $"{FormatSeats()}, двигатель {EngineVolumeL}л ({FuelType})";
}
