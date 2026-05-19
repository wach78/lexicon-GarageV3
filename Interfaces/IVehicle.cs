using GarageV3.Enums;

namespace GarageV3.Interfaces;

public interface IVehicle
{
    string NumberPlate { get; }

    VehicleColor Color { get; }

    int NumberOfWheels { get; }
}
