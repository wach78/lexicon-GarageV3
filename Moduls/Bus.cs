using GarageV3.Enums;

namespace GarageV3.Moduls;

public class Bus: Vehicle
{
    public Bus(string numberPlate, VehicleColor color, int numberOfWheels, int numberOfSeats)
    : base(numberPlate, color, numberOfWheels)
    {
        NumberOfSeats = numberOfSeats;
    }

    public int NumberOfSeats { get;}

    public override string ToString()
    {
        return $"{base.ToString()}, Number of seats: {NumberOfSeats}";
    }
}
