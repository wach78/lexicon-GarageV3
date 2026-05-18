using GarageV2.Enums;
using GarageV2.Interfaces;

namespace GarageV2.UI;
public class ConsoleInputReader : IConsoleInputReader
{

    private readonly IVehicleInputValidator _vehicleInputValidator;

    public ConsoleInputReader(IVehicleInputValidator vehicleInputValidator)
    {
        _vehicleInputValidator = vehicleInputValidator;
    }

    private int? ReadValidatedInt(Func<int, bool> isValid)
    {
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int value))
        {
            return null;
        }

        if (!isValid(value))
        {
            return null;
        }

        return value;
    }

    public string? ReadPlateNumber()
    {
        string? input = Console.ReadLine();

        if (!_vehicleInputValidator.IsValidPlateNumber(input))
        {
            return null;
        }

        return input!.Trim().ToUpperInvariant();
    }

    public int? ReadNumberOfWheels()
    {
        return ReadValidatedInt(_vehicleInputValidator.IsValidNumberOfWheels);
    }

    public int? ReadBoatLength()
    {
        return ReadValidatedInt(_vehicleInputValidator.IsValidBoatLength);
    }

    public int? ReadNumberOfSeats()
    {
        return ReadValidatedInt(_vehicleInputValidator.IsValidNumberOfSeats);
    }

    public int? ReadCylinderVolume()
    {
        return ReadValidatedInt(_vehicleInputValidator.IsValidCylinderVolume);
    }

    public int? ReadNumberOfEngines()
    {
        return ReadValidatedInt(_vehicleInputValidator.IsValidNumberOfEngines);
    }

    public int? ReadPositiveInt()
    {
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int value) && value > 0)
        {
            return value;
        }

        return null;
    }

    public VehicleColor? ReadVehicleColor()
    {
        return ReadEnumChoice<VehicleColor>();
    }

    public FuelType? ReadFuelType()
    {
        return ReadEnumChoice<FuelType>();
    }

    public VehicleTypeChoice? ReadVehicleTypeChoice()
    {
        return ReadEnumChoice<VehicleTypeChoice>();
    }

    public MenuChoice? ReadMainMenuChoice()
    {
        return ReadEnumChoice<MenuChoice>();
    }

    public SearchVehicleTypes? ReadSearchVehicleTypeChoice()
    {
        return ReadEnumChoice<SearchVehicleTypes>();
    }

    private static TEnum? ReadEnumChoice<TEnum>() where TEnum : struct, Enum
    {
        string? input = Console.ReadLine();

        if (
            int.TryParse(input, out int numericChoice) &&
            Enum.IsDefined(typeof(TEnum), numericChoice)
        )
        {
            return (TEnum)Enum.ToObject(typeof(TEnum), numericChoice);
        }

        return null;
    }

    public bool TryReadSearchVehicleColor(out VehicleColor? color)
    {
        color = null;

        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return true;
        }

        if (
            int.TryParse(input, out int numericChoice) &&
            Enum.IsDefined(typeof(VehicleColor), numericChoice)
        )
        {
            color = (VehicleColor)Enum.ToObject(typeof(VehicleColor), numericChoice);
            return true;
        }

        return false;
    }
}
