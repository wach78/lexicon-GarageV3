using GarageV2.Enums;
using GarageV2.Validator;


namespace GarageV2.Interfaces;

public interface IConsoleInputReader
{
    public MenuChoice? ReadMainMenuChoice();
    public int? ReadPositiveInt();
    public VehicleTypeChoice? ReadVehicleTypeChoice();
    public SearchVehicleTypes? ReadSearchVehicleTypeChoice();
    public VehicleColor? ReadVehicleColor();
    public FuelType? ReadFuelType();
    public bool TryReadSearchVehicleColor(out VehicleColor? color);
    public string? ReadPlateNumber();
    public int? ReadNumberOfWheels();
    public int? ReadBoatLength();
    public int? ReadNumberOfSeats();
    public int? ReadCylinderVolume();
    public int? ReadNumberOfEngines();
}
