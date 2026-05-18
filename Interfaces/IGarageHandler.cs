using GarageV2.Enums;
using GarageV2.Moduls;

namespace GarageV2.Interfaces;

public interface IGarageHandler
{
    void CreateGarage(int capacity);
    int? Populate();
    Vehicle[]? GetParkedVehicles();
    Dictionary<string, int>? GetParkedVehicleTypeCounts();
    bool HasGarage { get; }
    bool RemoveByPlateNumber(string? numberPlate);
    public Vehicle? FindByPlateNumber(string? numberPlate);
    public AddVehicleResult ParkVehicle(Vehicle vehicle);
    public Vehicle[]? SearchVehicles(VehicleColor? color, int? numberOfWheels, Type? vehicleType);
}
