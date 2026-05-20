using GarageV3.Enums;
using GarageV3.Moduls;


namespace GargeV3_Test.Moduls;

public class AirPlaneTest
{
    private string numberPlate = "air555";
    private VehicleColor color = VehicleColor.Silver;
    private  int numberOfWheels = 3;
    private int numberOfEngines = 2;

    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var airPlane = new AirPlane(numberPlate, color, numberOfWheels, numberOfEngines);

        Assert.NotNull(airPlane);

        Assert.Equal(numberPlate, airPlane.NumberPlate);
        Assert.Equal(color, airPlane.Color);
        Assert.Equal(numberOfWheels, airPlane.NumberOfWheels);
        Assert.Equal(numberOfEngines, airPlane.NumberOfEngines);
    }

    [Fact]
    public void Check_ToString()
    {
   
        var airPlane = new AirPlane(numberPlate, color, numberOfWheels, numberOfEngines);

        Assert.NotNull(airPlane);

        string toString = $"Number plate: {numberPlate} color: {color} Number of wheels: {numberOfWheels}, Number of engines: {numberOfEngines}";

        Assert.Equal(toString, airPlane.ToString());
    }
}
