using GarageV3.Moduls;
using GarageV3.Enums;

namespace GargeV3_Test.Moduls;

public  class VehicleTests
{
    private string numberPlate = "ABC123";
    private VehicleColor color = VehicleColor.Red;
    private int numberOfWheels = 4;

    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        Vehicle vehicle = new Vehicle(numberPlate, color, numberOfWheels);

        Assert.NotNull(vehicle);

        Assert.Equal(numberPlate, vehicle.NumberPlate);
        Assert.Equal(color, vehicle.Color);
        Assert.Equal(numberOfWheels, vehicle.NumberOfWheels);
    }

    [Fact]
    public void Check_ToString()
    {
        var vehicle = new Vehicle(numberPlate, color, numberOfWheels);

        Assert.NotNull(vehicle);

        string toString = $"Number plate: {numberPlate} color: {color} Number of wheels: {numberOfWheels}";

        Assert.Equal(toString, vehicle.ToString());
    }
}
