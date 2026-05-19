using GarageV3.Enums;
using GarageV3.Interfaces;


namespace GarageV3.Moduls;

public class Vehicle : IVehicle
{

    public Vehicle(string numberPlate, VehicleColor color, int numberOfWheels) 
    {
        NumberPlate = numberPlate; 
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public string NumberPlate { get;}
    public VehicleColor Color { get; }
    public int NumberOfWheels { get;}

    public override string ToString()
    {
        return $"Number plate: {NumberPlate} color: {Color} Number of wheels: {NumberOfWheels}";
    }

}
