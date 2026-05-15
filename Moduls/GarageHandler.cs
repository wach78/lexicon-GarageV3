using GarageV2.Enums;
using GarageV2.Interfaces;
using System.Diagnostics.Metrics;

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
            new Car("ABC123", "Red", 4, FuelType.Gasoline),
            new Car("abc321", "Green", 4, FuelType.Diesel),
            new MotorCycle("MC123", "Black", 2, 600),
            new Bus("bUs123", "Blue", 6, 45),
            new Boat("BoA123", "White", 0, 8),
            new AirPlane("air123", "Silver", 3, 2)
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

        Dictionary<string, int> vehicleTypeCounts = new();

        foreach (Vehicle vehicle in vehicles)
        {
            string vehicleType = vehicle.GetType().Name;

            if (!vehicleTypeCounts.TryAdd(vehicleType, 1))
            {
                vehicleTypeCounts[vehicleType]++;
            }
        }

        return vehicleTypeCounts;
    }
}
