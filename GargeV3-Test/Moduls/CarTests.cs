using GarageV3.Enums;
using GarageV3.Moduls;


namespace GargeV3_Test.Moduls;

public class CarTests
{
    private string numberPlate = "ABC123";
    private VehicleColor color = VehicleColor.White;
    private int numberOfWheels = 4;
   

    [Theory]
    [InlineData(FuelType.Gasoline)]
    [InlineData(FuelType.Diesel)]
    public void Constructor_ShouldSetProperties(FuelType fuelType)
    {
        var car = new Car(numberPlate, color, numberOfWheels, fuelType);

        Assert.NotNull(car);

        Assert.Equal(numberPlate, car.NumberPlate);
        Assert.Equal(color, car.Color);
        Assert.Equal(numberOfWheels, car.NumberOfWheels);
        
        Assert.Equal(fuelType, car.FuelType);
    }

    [Fact]
    public void Check_ToString()
    {
        FuelType fuel = FuelType.Gasoline;

        var car = new Car(numberPlate, color, numberOfWheels, fuel);

        Assert.NotNull(car);

        string toString = $"Number plate: {numberPlate} color: {color} Number of wheels: {numberOfWheels}, Fuel type: {fuel}";

        Assert.Equal(toString, car.ToString());
    }
}
