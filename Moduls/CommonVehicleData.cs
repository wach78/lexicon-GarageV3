

using GarageV2.Enums;

namespace GarageV2.Moduls;

public class CommonVehicleData
{
    public CommonVehicleData(string numberPlate, VehicleColor color, int numberOfWheels)
    {
        NumberPlate = numberPlate;
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public string NumberPlate { get; }

    public VehicleColor Color { get; }

    public int NumberOfWheels { get; }
}
