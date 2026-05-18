using GarageV2.Enums;
using GarageV2.Interfaces;

namespace GarageV2.Moduls;

public class GarageHandler : IGarageHandler
{
    private Garage<Vehicle>? _garage;

    public GarageHandler()
    {

    }

    public int Capacity { get; }
    public bool HasGarage => _garage is not null;

    public void CreateGarage(int capacity)
    {
        _garage = new Garage<Vehicle>(capacity);
    }


    public int? Populate()
    {
        if (_garage is null)
        {
            return null;
        }

        Vehicle[] vehicles =
        [
            new Car("ABC123", VehicleColor.Red, 4, FuelType.Gasoline),
            new Car("abc321", VehicleColor.Green, 4, FuelType.Diesel),
            new MotorCycle("MCC123", VehicleColor.Black, 2, 600),
            new Bus("bUs123", VehicleColor.Blue, 6, 45),
            new Boat("BoA123", VehicleColor.White, 0, 8),
            new AirPlane("air123", VehicleColor.Silver, 3, 2)
        ];

        return _garage.AddMany(vehicles);
    }

    public Vehicle[]? GetParkedVehicles()
    {
        return _garage?.GetParkedVehicles();
    }

    public Dictionary<string, int>? GetParkedVehicleTypeCounts()
    {
        Vehicle[]? vehicles = _garage?.GetParkedVehicles();


        if (vehicles is null)
        {
            return null;
        }

        return vehicles
        .GroupBy(vehicle => vehicle.GetType().Name)
        .ToDictionary(
            group => group.Key,
            group => group.Count()
        );
    }

    public bool RemoveByPlateNumber(string? numberPlate)
    {
        if (_garage is null)
        {
            return false;
        }

        return _garage.RemoveByPlateNumber(numberPlate);
    }

    public Vehicle? FindByPlateNumber(string? numberPlate)
    {
        if (_garage is null)
        {
            return null;
        }

        return _garage.FindByPlateNumber(numberPlate);
    }

    public AddVehicleResult ParkVehicle(Vehicle vehicle)
    {
        if (_garage is null)
        {
            return AddVehicleResult.GarageNotCreated;
        }

        return _garage.Add(vehicle);
    }

    public Vehicle[]? SearchVehicles(VehicleColor? color, int? numberOfWheels, Type? vehicleType)
    {
        if (_garage is null)
        {
            return null;
        }

        return _garage.SearchVehicles(color, numberOfWheels, vehicleType);
    }
}
