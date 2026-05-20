using GarageV3.Enums;
using GarageV3.Moduls;


namespace GargeV3_Test.Moduls;

public class BoatTests
{
    private string numberPlate = "bb321";
    private VehicleColor color = VehicleColor.White;
    private int numberOfWheels = 0;
    private int length = 5;

    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var boat = new Boat(numberPlate, color, numberOfWheels, length);

        Assert.NotNull(boat);

        Assert.Equal(numberPlate, boat.NumberPlate);
        Assert.Equal(color, boat.Color);
        Assert.Equal(numberOfWheels, boat.NumberOfWheels);
        Assert.Equal(length, boat.Length);
    }

    [Fact]
    public void Check_ToString()
    {
        var boat = new Boat(numberPlate, color, numberOfWheels, length);

        Assert.NotNull(boat);

        string toString = $"Number plate: {numberPlate} color: {color} Number of wheels: {numberOfWheels}, Length: {length}";

        Assert.Equal(toString, boat.ToString());
    }
}
