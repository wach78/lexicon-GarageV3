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

    /*
    
    AddVehicle(Vehicle vehicle);
    
    SearchVehicles(string? color, int? numberOfWheels, Type? vehicleType);
    */
}
