using GarageV2.Moduls;


namespace GarageV2.Interfaces;

public interface IGarageHandler
{
    void CreateGarage(int capacity);
    int? Populate();
    Vehicle[]? GetParkedVehicles();

    bool HasGarage { get; }
    /*GarageExists();
    GarageHasVehicles();
    
    AddVehicle(Vehicle vehicle);
    RemoveByPlateNumber(string? numberPlate);
    FindByPlateNumber(string? numberPlate);
    GetParkedVehicles();
    GetParkedVehicleTypeCounts();
    SearchVehicles(string? color, int? numberOfWheels, Type? vehicleType);
    */
}
