using GarageV3.Enums;
using GarageV3.Moduls;

namespace GargeV3_Test.Moduls;
public class BusTests
{
    private string numberPlate = "abc123";
    private VehicleColor color = VehicleColor.Black;
    private int numberOfWheels = 8;
    private int numberOfSeats = 48;

    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var bus = new Bus(numberPlate, color, numberOfWheels, numberOfSeats);

        Assert.NotNull(bus);

        Assert.Equal(numberPlate, bus.NumberPlate);
        Assert.Equal(color, bus.Color);
        Assert.Equal(numberOfWheels, bus.NumberOfWheels);
        Assert.Equal(numberOfSeats, bus.NumberOfSeats);
    }

    [Fact]
    public void Check_ToString()
    {
        var bus = new Bus(numberPlate, color, numberOfWheels, numberOfSeats);

        Assert.NotNull(bus);

        string toString = $"Number plate: {numberPlate} color: {color} Number of wheels: {numberOfWheels}, Number of seats: {numberOfSeats}";

        Assert.Equal(toString, bus.ToString());
    }

}
