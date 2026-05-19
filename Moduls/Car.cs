using GarageV3.Enums;
using Microsoft.VisualBasic;

namespace GarageV3.Moduls;

public class Car: Vehicle
{
    public Car(string numberPlate, VehicleColor color, int numberOfWheels, FuelType fuelType)
    : base(numberPlate, color, numberOfWheels)
    {
        FuelType = fuelType;
    }

    public FuelType FuelType { get;}

    public override string ToString()
    {
        return $"{base.ToString()}, Fuel type: {FuelType}";
    }
}
