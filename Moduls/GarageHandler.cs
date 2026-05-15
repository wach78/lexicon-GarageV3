using GarageV2.Interfaces;

namespace GarageV2.Moduls;

public class GarageHandler: IGarageHandler
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
}
