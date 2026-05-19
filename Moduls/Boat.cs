using GarageV3.Enums;

namespace GarageV3.Moduls;

public class Boat: Vehicle
{
    public Boat(string numberPlate, VehicleColor color, int numberOfWheels, int length)
    : base(numberPlate, color, numberOfWheels)
    {
        Length = length;
    }

    public int Length { get;}

    public override string ToString()
    {
        return $"{base.ToString()}, Length: {Length}";
    }
}
