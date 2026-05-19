using GarageV3.Enums;
using GarageV3.Interfaces;
using System.Collections;

namespace GarageV3.Moduls;

public class Garage<T> : IEnumerable<T> where T : class, IVehicle
{
    private readonly T?[] _parkedVehicles;

    public Garage(int capacity)
    {
        Capacity = capacity;
        _parkedVehicles = new T?[capacity];
    }

    public int Capacity { get; }

    public AddVehicleResult Add(T vehicle)
    {
        
        if (FindByPlateNumber(vehicle.NumberPlate) is object)
        {
            return AddVehicleResult.DuplicatePlateNumber;
        }

        for (int index = 0; index < _parkedVehicles.Length; index++)
        {
            if (_parkedVehicles[index] is null)
            {
                _parkedVehicles[index] = vehicle;
                return AddVehicleResult.Success;
            }
        }

        return AddVehicleResult.GarageFull;
    
    }

    public int AddMany(T[] vehicles)
    {
        int addedVehicles = 0;

        foreach (T vehicle in vehicles)
        {
            AddVehicleResult result = Add(vehicle);

            if (result == AddVehicleResult.Success)
            {
                addedVehicles++;
            }
        }

        return addedVehicles;
    }

    private int FindIndexByPlateNumber(string? plateNumber)
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
        {
            return -1;
        }

        for (int index = 0; index < _parkedVehicles.Length; index++)
        {
            T? vehicle = _parkedVehicles[index];

            if (vehicle is null)
            {
                continue;
            }

            if (string.Equals(
                vehicle.NumberPlate,
                plateNumber.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                return index;
            }
        }

        return -1;
    }

    public bool RemoveByPlateNumber(string? plateNumber)
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
        {
            return false;
        }

        int index = FindIndexByPlateNumber(plateNumber);

        if (index == -1)
        {
            return false;
        }

        _parkedVehicles[index] = null;
        return true;
    }

    public T[] GetParkedVehicles()
    {
        return this.ToArray();
    }

    public T? FindByPlateNumber(string? plateNumber)
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
        {
            return null;
        }

        return this
        .FirstOrDefault(vehicle =>
            string.Equals(
                vehicle.NumberPlate,
                plateNumber,
                StringComparison.OrdinalIgnoreCase
            )
        );
    }

    public T[] SearchVehicles(VehicleColor? color, int? numberOfWheels, Type? vehicleType)
    {
        return this
            .Where(vehicle =>
                (!color.HasValue || vehicle.Color == color.Value)
                && (!numberOfWheels.HasValue || vehicle.NumberOfWheels == numberOfWheels.Value)
                && (vehicleType is null || vehicle.GetType() == vehicleType)
            )
            .ToArray();
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T? parkedVehicle in _parkedVehicles)
        {
            if (parkedVehicle is not null)
            {
                yield return parkedVehicle;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
