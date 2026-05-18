using GarageV2.Enums;

namespace GarageV2.Interfaces;

public interface IVehicle
{
    string NumberPlate { get; }

    VehicleColor Color { get; }

    int NumberOfWheels { get; }
}
