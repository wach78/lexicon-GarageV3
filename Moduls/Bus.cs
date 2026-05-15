

namespace GarageV1.Moduls;

public class Bus: Vehicle
{
    public Bus(string numberPlate, string color, int numberOfWheels, int numberOfSeats)
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
