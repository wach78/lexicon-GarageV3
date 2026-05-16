using GarageV2.Enums;


namespace GarageV2.Interfaces;

public interface IConsoleInputReader
{
    public MenuChoice? ReadMainMenuChoice();
    public int? ReadPositiveInt();
    public VehicleTypeChoice? ReadVehicleTypeChoice();
    public string? ReadRequiredString();
    public int? ReadZeroOrPositiveInt();
    public string? ReadOptionalString();
    public int? ReadOptionalZeroOrPositiveInt();
    public SearchVehicleTypes? ReadSearchVehicleTypeChoice();
    public VehicleColor? ReadVehicleColor();
    public FuelType? ReadFuelType();
}
