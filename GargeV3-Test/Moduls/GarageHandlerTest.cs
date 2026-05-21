using GarageV3.Enums;
using GarageV3.Interfaces;
using GarageV3.Moduls;
using System.Reflection.Metadata.Ecma335;


namespace GargeV3_Test.Moduls;

public  class GarageHandlerTest
{
    [Fact]
    public void HasGarage_ShouldReturnFalse_WhenGarageHasNotBeenCreated()
    {
        var garageHandler = new GarageHandler();

        bool hasGarage = garageHandler.HasGarage;

        Assert.False(hasGarage);
    }

    [Fact]
    public void HasGarage_ShouldReturnTrue_WhenGarageHasBeenCreated()
    {
        var garageHandler = new GarageHandler();

        garageHandler.CreateGarage(6);

        Assert.True(garageHandler.HasGarage);
    }

    [Fact]
    public void Create_garage()
    {
        int capacity = 6;

        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(capacity);

        bool hasGarage = garageHandler.HasGarage;

        Assert.True(hasGarage);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(6, 6)]
    [InlineData(7, 6)]
    public void Populate_return_numberOfParkedCars(int capacity, int expectedAddedVehicles)
    {
        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(capacity);

        bool hasGarage = garageHandler.HasGarage;

        Assert.True(hasGarage);

        int? numberOfParkVehicles = garageHandler.Populate();

        Assert.Equal(expectedAddedVehicles, numberOfParkVehicles);
    }


    [Theory]
    [InlineData(2, 2)]
    [InlineData(6, 6)]
    [InlineData(7, 6)]
    public void GetParkedVehicles(int capacity, int expectedVehicles)
    {
        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(capacity);


        garageHandler.Populate();

        Vehicle[]? vehicle = garageHandler.GetParkedVehicles();

        Assert.NotNull(vehicle);

        Assert.Equal(expectedVehicles, vehicle.Length);
    }

    [Fact]
    public void GetParkedVehicles_ShouldReturnEmptyArray_WhenGarageHasNoVehicles()
    {
        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(6);

        Vehicle[]? vehicle = garageHandler.GetParkedVehicles();
        Assert.NotNull(vehicle);

        Assert.Equal([],vehicle);
        Assert.Empty(vehicle);
    }

    [Fact]
    public void GetParkedVehicleTypeCounts_ShouldReturnNull_WhenGarageHasNotBeenCreated()
    {
       var garageHandler = new GarageHandler();

        Dictionary<string, int>? vehicleTypeCounts = garageHandler.GetParkedVehicleTypeCounts();

        Assert.Null(vehicleTypeCounts);
    }

    [Fact]
    public void GetParkedVehicleTypeCounts_ShouldReturnEmptyDictionary_WhenGarageHasNoVehicles()
    {
        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(6);

        Assert.True(garageHandler.HasGarage);

        Dictionary<string, int>? vehicleTypeCounts = garageHandler.GetParkedVehicleTypeCounts();

        // Assert
        Assert.NotNull(vehicleTypeCounts);
        Assert.Empty(vehicleTypeCounts);
    }

    [Fact]
    public void GetParkedVehicleTypeCounts_ShouldReturnCorrectCounts_WhenGarageIsPopulated()
    {
        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(6);

        Assert.True(garageHandler.HasGarage);

        garageHandler.Populate();

        Dictionary<string, int>? vehicleTypeCounts = garageHandler.GetParkedVehicleTypeCounts();

        Assert.NotNull(vehicleTypeCounts);

        Assert.Equal(5, vehicleTypeCounts.Count);

        Assert.Equal(2, vehicleTypeCounts["Car"]);
        Assert.Equal(1, vehicleTypeCounts["MotorCycle"]);
        Assert.Equal(1, vehicleTypeCounts["Bus"]);
        Assert.Equal(1, vehicleTypeCounts["Boat"]);
        Assert.Equal(1, vehicleTypeCounts["AirPlane"]);
    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    [InlineData("AbC123")]
    public void RemoveByPlateNumber_WhenVehicleExistsIgnoringCase_ReturnsTrueAndRemovesVehicle(string numberPlate)
    {
        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(1);

        Assert.True(garageHandler.HasGarage);

        garageHandler.ParkVehicle(new Car("ABC123", VehicleColor.Red, 4, FuelType.Gasoline));

       bool removed = garageHandler.RemoveByPlateNumber(numberPlate);

       Assert.True(removed);

        Vehicle[]? vehicles = garageHandler.GetParkedVehicles();

        Assert.NotNull(vehicles);
        Assert.Empty(vehicles);

    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
   public void RemoveByPlateNumber_ShouldReturnFalse_WhenPlateNumberDoesNotExist(string numberPlate)
    {
        var garageHandler = new GarageHandler();
        garageHandler.CreateGarage(1);

        Assert.True(garageHandler.HasGarage);

        garageHandler.ParkVehicle(new Car("XXX123", VehicleColor.Red, 4, FuelType.Gasoline));

        bool removed = garageHandler.RemoveByPlateNumber(numberPlate);

        Assert.False(removed);
    }

    public void RemoveByPlateNumber_ShouldReturnFalse_WhenGarageIsEmpty(string numberPlate)
    {
        var garageHandler = new GarageHandler();

        bool hasGarage = garageHandler.HasGarage;

        Assert.False(hasGarage);


        bool removed = garageHandler.RemoveByPlateNumber(numberPlate);
        Assert.False(removed);
    }

}
