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
    /*
    GarageHasVehicles();
    
    AddVehicle(Vehicle vehicle);
    RemoveByPlateNumber(string? numberPlate);
    FindByPlateNumber(string? numberPlate);
    GetParkedVehicles();
    
    SearchVehicles(string? color, int? numberOfWheels, Type? vehicleType);
    */
}
