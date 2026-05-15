using GarageV2.Interfaces;
using System.Collections;


namespace GarageV2.Moduls;

public class Garage<T> : IEnumerable<T> where T : IVehicle
{
    private readonly T?[] parkedVehicles;

    public Garage(int capacity)
    {
        Capacity = capacity;
        parkedVehicles = new T?[capacity];
    }

    public int Capacity { get; }
    public int Count => parkedVehicles.Count(vehicle => vehicle is not null);

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T? parkedVehicle in parkedVehicles)
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
