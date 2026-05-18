using GarageV2.Interfaces;

using System.Text.RegularExpressions;

namespace GarageV2.Validator;

public class VehicleInputValidator : IVehicleInputValidator
{
    public const int MinimumNumberOfWheels = 0;
    public const int MaximumNumberOfWheels = 20;

    public const int MinimumBoatLength = 1;
    public const int MaximumBoatLength = 100;

    public const int MinimumNumberOfSeats = 1;
    public const int MaximumNumberOfSeats = 100;

    public const int MinimumCylinderVolume = 50;
    public const int MaximumCylinderVolume = 2500;

    public const int MinimumNumberOfEngines = 1;
    public const int MaximumNumberOfEngines = 12;

    public const int PlateNumberLength = 6;
    public const string PlateNumberFormat = "a-zA-Z0-9";

    public static readonly string PlateNumberFormatDescription =
    $"{PlateNumberLength} characters, only letters A-Z and numbers 0-9";

    private static readonly string PlateNumberPattern =
        "^[" + PlateNumberFormat + "]{" + PlateNumberLength + "}$";

    public bool IsValidPlateNumber(string? plateNumber)
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
        {
            return false;
        }

        return Regex.IsMatch(plateNumber.Trim(), PlateNumberPattern);
    }

    public bool IsValidNumberOfWheels(int numberOfWheels)
    {
        return numberOfWheels >= MinimumNumberOfWheels
            && numberOfWheels <= MaximumNumberOfWheels;
    }

    public bool IsValidBoatLength(int length)
    {
        return length >= MinimumBoatLength
            && length <= MaximumBoatLength;
    }

    public bool IsValidNumberOfSeats(int numberOfSeats)
    {
        return numberOfSeats >= MinimumNumberOfSeats
            && numberOfSeats <= MaximumNumberOfSeats;
    }

    public bool IsValidCylinderVolume(int cylinderVolume)
    {
        return cylinderVolume >= MinimumCylinderVolume
            && cylinderVolume <= MaximumCylinderVolume;
    }

    public bool IsValidNumberOfEngines(int numberOfEngines)
    {
        return numberOfEngines >= MinimumNumberOfEngines
            && numberOfEngines <= MaximumNumberOfEngines;
    }
}
