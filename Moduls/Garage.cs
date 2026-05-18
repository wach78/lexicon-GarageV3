using GarageV2.Enums;
using GarageV2.Interfaces;
using System.Collections;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace GarageV2.Moduls;

public class Garage<T> : IEnumerable<T> where T : class, IVehicle
{
    private readonly T?[] _parkedVehicles;

    public Garage(int capacity)
    {
        Capacity = capacity;
        _parkedVehicles = new T?[capacity];
    }

    public int Capacity { get; }
    public int Count => _parkedVehicles.Count(vehicle => vehicle is not null);

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
        T[] result = new T[Count];

        int index = 0;

        foreach (T vehicle in this)
        {
            result[index] = vehicle;
            index++;
        }

        return result;
    }

    public T? FindByPlateNumber(string? plateNumber)
    {
        if (string.IsNullOrWhiteSpace(plateNumber))
        {
            return null;
        }

        foreach (T vehicle in this)
        {
            if (string.Equals(
                vehicle.NumberPlate,
                plateNumber,
                StringComparison.OrdinalIgnoreCase))
            {
                return vehicle;
            }
        }

        return null;
    }

    public T[] SearchVehicles(VehicleColor? color, int? numberOfWheels, Type? vehicleType)
    {
        T?[] matches = new T?[Count];
        int matchCount = 0;

        foreach (T vehicle in this)
        {
            if (color.HasValue && vehicle.Color != color.Value)
            {
                continue;
            }

            if (numberOfWheels.HasValue && vehicle.NumberOfWheels != numberOfWheels.Value)
            {
                continue;
            }

            if (vehicleType is not null && vehicle.GetType() != vehicleType)
            {
                continue;
            }

            matches[matchCount] = vehicle;
            matchCount++;
        }

        T[] result = new T[matchCount];

        for (int index = 0; index < matchCount; index++)
        {
            result[index] = matches[index]!;
        }

        return result;
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
