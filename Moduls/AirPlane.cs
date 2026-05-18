
using GarageV2.Enums;

namespace GarageV2.Moduls;

public class AirPlane: Vehicle
{
    public AirPlane(string numberPlate, VehicleColor color, int numberOfWheels, int numberOfEngines)
    : base(numberPlate, color, numberOfWheels)
    {
        NumberOfEngines = numberOfEngines;
    }
    public int NumberOfEngines { get;}

    public override string ToString()
    {
        return $"{base.ToString()}, Number of engines: {NumberOfEngines}";
    }
}
