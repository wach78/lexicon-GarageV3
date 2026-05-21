using GarageV3.Enums;
using GarageV3.Moduls;


namespace GargeV3_Test.Moduls;

public class CommonVehicleDataTest
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {

        string numberPlate = "ABC123";
        VehicleColor color = VehicleColor.White;
        int numberOfWheels = 4;

        var CommonVehicleData = new CommonVehicleData(numberPlate, color , numberOfWheels);

        Assert.NotNull(CommonVehicleData);

        Assert.Equal(numberPlate, CommonVehicleData.NumberPlate);
        Assert.Equal(color, CommonVehicleData.Color);
        Assert.Equal(numberOfWheels, CommonVehicleData.NumberOfWheels);
    }
}
