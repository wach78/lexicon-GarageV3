using GarageV2.Enums;
using GarageV2.Interfaces;
using System.Collections;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;


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

    public void Remove()
    {

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
