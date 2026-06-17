using HW2_Cars.Abstract;
using HW2_Cars.Interfaces;

namespace HW2_Cars.Cars;

public class Tesla : ACar, IElectric, IAutomaticTransmission
{
    public override string Brand => "Tesla";
    public override int SeatsCount => 5;
    public int BatteryCapacityKwh => 100;
    public string OperatingSystem => "Android";

    public override string GetDescription() =>
        $"{Brand}: электромобиль с {((IAutomaticTransmission)this).TransmissionType}, " +
        $"{FormatSeats()}, {OperatingSystem} на борту, " +
        $"батарея {BatteryCapacityKwh} кВт·ч";
}
