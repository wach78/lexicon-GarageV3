using GarageV2.Enums;
using GarageV2.Interfaces;

namespace GarageV2.UI;
public class ConsoleInputReader : IConsoleInputReader
{
    
    public int? ReadPositiveInt()
    {
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int value) && value > 0)
        {
            return value;
        }

        return null;
    }

    public string? ReadRequiredString()
    {
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        return input.Trim();
    }

    public int? ReadZeroOrPositiveInt()
    {
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int value) && value >= 0)
        {
            return value;
        }

        return null;
    }

    public string? ReadOptionalString()
    {
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        return input.Trim();
    }

    public int? ReadOptionalZeroOrPositiveInt()
    {
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (int.TryParse(input, out int value) && value >= 0)
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
}
