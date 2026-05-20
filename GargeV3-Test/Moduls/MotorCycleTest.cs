using GarageV3.Enums;
using GarageV3.Moduls;


namespace GargeV3_Test.Moduls;

public class MotorCycleTest
{
    private string numberPlate = "mc53";
    private VehicleColor color = VehicleColor.Silver;
    private int numberOfWheels = 2;
    private int cylinderVolume = 8;

    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        var motorCycle = new MotorCycle(numberPlate, color, numberOfWheels, cylinderVolume);

        Assert.NotNull(motorCycle);

        Assert.Equal(numberPlate, motorCycle.NumberPlate);
        Assert.Equal(color, motorCycle.Color);
        Assert.Equal(numberOfWheels, motorCycle.NumberOfWheels);
        Assert.Equal(cylinderVolume, motorCycle.CylinderVolume);
    }


    [Fact]
    public void Check_ToString()
    {
        var motorCycle = new MotorCycle(numberPlate, color, numberOfWheels, cylinderVolume);

        Assert.NotNull(motorCycle);

        string toString = $"Number plate: {numberPlate} color: {color} Number of wheels: {numberOfWheels}, Cylinder Volume: {cylinderVolume}";

        Assert.Equal(toString, motorCycle.ToString());
    }
}
