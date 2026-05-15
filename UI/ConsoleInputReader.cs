using GarageV2.Enums;
using GarageV2.Interfaces;

namespace GarageV2.UI;
public class ConsoleInputReader : IConsoleInputReader
{
    public MenuChoice? ReadMainMenuChoice()
    {

        string? input = Console.ReadLine();

        if (
            int.TryParse(input, out int numericChoice)
            && Enum.IsDefined(typeof(MenuChoice), numericChoice)
        )
        {
            return (MenuChoice)numericChoice;
        }

        return null;
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

    public VehicleTypeChoice? ReadVehicleTypeChoice()
    {
        string? input = Console.ReadLine();

        if (
            int.TryParse(input, out int numericChoice)
            && Enum.IsDefined(typeof(VehicleTypeChoice), numericChoice)
        )
        {
            return (VehicleTypeChoice)numericChoice;
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

    public SearchVehicleTypes? ReadSearchVehicleTypeChoice()
    {
        string? input = Console.ReadLine();

        if (
            int.TryParse(input, out int numericChoice) &&
            Enum.IsDefined(typeof(SearchVehicleTypes), numericChoice)
        )
        {
            return (SearchVehicleTypes)numericChoice;
        }

        return null;
    }
}
