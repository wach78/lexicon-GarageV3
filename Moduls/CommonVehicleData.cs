using GarageV3.Enums;

namespace GarageV3.Moduls;

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
