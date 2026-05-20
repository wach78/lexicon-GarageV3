using Xunit;
﻿using GarageV3.Enums;
using GarageV3.Interfaces;
using GarageV3.Moduls;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Linq;


namespace GargeV3_Test.Moduls;

public class GarageTest
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        int capacity = 5;
        var garage = new Garage<Vehicle>(capacity);

        Assert.NotNull(garage);

        Assert.Equal(capacity, garage.Capacity);
    }

    [Fact]
    public void Add_Vehicle_to_garage_return_success()
    {
        int capacity = 5;
        var garage = new Garage<Vehicle>(capacity);

        Assert.NotNull(garage);

        AddVehicleResult result = garage.Add(new Car("ABC123", VehicleColor.Red, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, result);

    }

    [Fact]
    public void Add_Vehicle_to_garage_return_garage_full()
    {
        int capacity = 1;
        var garage = new Garage<Vehicle>(capacity);

        Assert.NotNull(garage);

        AddVehicleResult resultSuccess = garage.Add(new Car("ABC123", VehicleColor.Red, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, resultSuccess);

        AddVehicleResult resultGarageFull = garage.Add(new Car("xxx666", VehicleColor.Red, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.GarageFull, resultGarageFull);
    }


    [Theory]
    [InlineData("ABC123", "abc123")]
    [InlineData("abc123", "ABC123")]
    [InlineData("AbC123", "aBc123")]
    public void Add_Vehicle_to_garage_return_duplicate_plate_number(string firstNumberPlate, string duplicateNumberPlate)
    {
        int capacity = 2;
        var garage = new Garage<Vehicle>(capacity);

        Assert.NotNull(garage);

        AddVehicleResult resultSuccess = garage.Add(new Car(firstNumberPlate, VehicleColor.Red, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, resultSuccess);

        AddVehicleResult resultDuplicate = garage.Add(new Car(duplicateNumberPlate, VehicleColor.Red, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.DuplicatePlateNumber, resultDuplicate);
    }

    

    [Theory]
    [InlineData(2, 2)]
    [InlineData(6, 6)]
    [InlineData(7, 6)]
    public void AddMany_WhenVehiclesAreAdded_ReturnsNumberOfSuccessfullyAddedVehicles(int capacity, int expectedAddedVehicles)
    {
        var garage = new Garage<Vehicle>(capacity);

        Vehicle[] vehicles = GetVehicles();

        int numberOfAddedVehicles = garage.AddMany(vehicles);

        Assert.Equal(expectedAddedVehicles, numberOfAddedVehicles);
    }

    [Fact]
    public void AddMany_WhenVehiclesContainDuplicateNumberPlate_ReturnsOnlySuccessfullyAddedVehicles()
    {
        var garage = new Garage<Vehicle>(5);

        Vehicle[] vehicles = GetVehiclesWithDuplicatePlateNumber();

        int numberOfAddedVehicles = garage.AddMany(vehicles);

        Assert.Equal(4, numberOfAddedVehicles);
    }

    [Fact]
    public void AddMany_WhenVehiclesArrayIsEmpty_ReturnsZero()
    {
        var garage = new Garage<Vehicle>(5);

        Vehicle[] vehicles = [];

        int numberOfAddedVehicles = garage.AddMany(vehicles);

        Assert.Equal(0, numberOfAddedVehicles);
        Assert.Empty(garage.GetParkedVehicles());
    }


    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    [InlineData("AbC123")]
    public void RemoveByPlateNumber_WhenVehicleExistsIgnoringCase_ReturnsTrueAndRemovesVehicle(string numberPlate)
    {
        var garage = new Garage<Vehicle>(1);

        AddVehicleResult addResult = garage.Add(new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, addResult);

        bool removeResult = garage.RemoveByPlateNumber(numberPlate);
        bool secondRemoveResult = garage.RemoveByPlateNumber(numberPlate);

        Assert.True(removeResult);
        Assert.False(secondRemoveResult);
    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    [InlineData("AbC123")]
    public void Dont_Remove_vehicle_By_PlateNumber(string numberPlate)
    {
        int capacity = 1;
        var garage = new Garage<Vehicle>(capacity);

        string plate = "XXX123";
        VehicleColor color = VehicleColor.Black;
        int numberOfWheels = 4;


        AddVehicleResult result = garage.Add(new Car(plate, color, numberOfWheels, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, result);

        bool removeResult = garage.RemoveByPlateNumber(numberPlate);

        Assert.False(removeResult);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("     ")]
    public void RemoveByPlateNumber_WhenPlateNumberIsNullOrWhiteSpace_ReturnsFalse(string? plateNumber)
    {
        var garage = new Garage<Vehicle>(1);

        AddVehicleResult addResult = garage.Add(new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, addResult);

        bool removeResult = garage.RemoveByPlateNumber(plateNumber);

        Assert.False(removeResult);
    }

    [Theory]
    [InlineData(" ABC123")]
    [InlineData("ABC123 ")]
    [InlineData(" ABC123 ")]
    public void RemoveByPlateNumber_WhenPlateNumberHasLeadingOrTrailingWhiteSpace_ReturnsTrue(
    string plateNumber)
    {
        var garage = new Garage<Vehicle>(1);

        AddVehicleResult addResult = garage.Add(
            new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline)
        );

        Assert.Equal(AddVehicleResult.Success, addResult);

        bool removeResult = garage.RemoveByPlateNumber(plateNumber);

        Assert.True(removeResult);
    }


    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    [InlineData("AbC123")]
    public void FFindByPlateNumber_WhenVehicleExistsIgnoringCase_ReturnsVehicle(string numberPlate)
    {
        var garage = new Garage<Vehicle>(1);

        string plate = "ABC123";
        VehicleColor color = VehicleColor.Black;
        int numberOfWheels = 4;


        AddVehicleResult result = garage.Add(new Car(plate, color, numberOfWheels, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, result);

        var vehicle = garage.FindByPlateNumber(numberPlate);

        Assert.NotNull(vehicle);

        var car = Assert.IsType<Car>(vehicle);

        Assert.Equal(plate, car.NumberPlate);
        Assert.Equal(color, car.Color);
        Assert.Equal(numberOfWheels, car.NumberOfWheels);
        Assert.Equal(FuelType.Gasoline, car.FuelType);
    }

    [Theory]
    [InlineData("ABC123")]
    [InlineData("abc123")]
    [InlineData("aBc123")]
    [InlineData("AbC123")]
    public void FindByPlateNumber_WhenVehicleDoesNotExist_ReturnsNull(string numberPlate)
    {

        var garage = new Garage<Vehicle>(1);

        string plate = "xxx123";
        VehicleColor color = VehicleColor.Black;
        int numberOfWheels = 4;


        AddVehicleResult result = garage.Add(new Car(plate, color, numberOfWheels, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, result);

        var vehicle = garage.FindByPlateNumber(numberPlate);

        Assert.Null(vehicle);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("     ")]
    public void FindByPlateNumber_WhenPlateNumberIsNullOrWhiteSpace_ReturnsNull(string? plateNumber)
    {
        var garage = new Garage<Vehicle>(1);

        AddVehicleResult addResult = garage.Add(new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline));

        Assert.Equal(AddVehicleResult.Success, addResult);

        var vehicle = garage.FindByPlateNumber(plateNumber);

        Assert.Null(vehicle);
    }

    [Theory]
    [InlineData(6, 6)]
    [InlineData(7, 6)]
    [InlineData(3, 3)]
    public void GetParkedVehicles_WhenVehiclesAreAdded_ReturnsExpectedNumberOfVehicles(int capacity, int expectedParkedVehicles)
    {
        var garage = new Garage<Vehicle>(capacity);

        Vehicle[] vehicles = GetVehicles();

        garage.AddMany(vehicles);

        Vehicle[] parkedVehicles = garage.GetParkedVehicles();

        Assert.Equal(expectedParkedVehicles, parkedVehicles.Length);
    }

    [Fact]
    public void GetParkedVehicles_WhenNoVehiclesAreAdded_ReturnsEmptyArray()
    {
        var garage = new Garage<Vehicle>(6);

        Vehicle[] parkedVehicles = garage.GetParkedVehicles();

        Assert.Empty(parkedVehicles);
    }

    [Fact]
    public void SearchVehicles_WhenNoFiltersAreProvided_ReturnsAllParkedVehicles()
    {
        var garage = new Garage<Vehicle>(6);

        Vehicle[] vehicles = GetVehicles();

        garage.AddMany(vehicles);

        Vehicle[] result = garage.SearchVehicles(null, null, null);

        Assert.Equal(vehicles.Length, result.Length);
    }

    [Fact]
    public void SearchVehicles_WhenColorFilterMatches_ReturnsOnlyVehiclesWithThatColor()
    {
        var garage = new Garage<Vehicle>(6);

        garage.AddMany(GetVehicles());

        Vehicle[] result = garage.SearchVehicles(VehicleColor.Black, null, null);

        Assert.NotEmpty(result);
        Assert.All(result, vehicle => Assert.Equal(VehicleColor.Black, vehicle.Color));
    }

    [Fact]
    public void SearchVehicles_WhenNumberOfWheelsFilterMatches_ReturnsOnlyVehiclesWithThatNumberOfWheels()
    {
        var garage = new Garage<Vehicle>(6);

        garage.AddMany(GetVehicles());

        Vehicle[] result = garage.SearchVehicles(null, 4, null);

        Assert.NotEmpty(result);
        Assert.All(result, vehicle => Assert.Equal(4, vehicle.NumberOfWheels));
    }

    [Fact]
    public void SearchVehicles_WhenVehicleTypeFilterMatches_ReturnsOnlyVehiclesOfThatType()
    {
        var garage = new Garage<Vehicle>(6);

        garage.AddMany(GetVehicles());

        Vehicle[] result = garage.SearchVehicles(null, null, typeof(Car));

        Assert.NotEmpty(result);
        Assert.All(result, vehicle => Assert.IsType<Car>(vehicle));
    }

    [Fact]
    public void SearchVehicles_WhenAllFiltersMatch_ReturnsOnlyMatchingVehicles()
    {
        var garage = new Garage<Vehicle>(6);

        garage.AddMany(GetVehicles());

        Vehicle[] result = garage.SearchVehicles(
            VehicleColor.Black,
            4,
            typeof(Car)
        );

        Assert.NotEmpty(result);

        Assert.All(result, vehicle =>
        {
            Car car = Assert.IsType<Car>(vehicle);

            Assert.Equal(VehicleColor.Black, car.Color);
            Assert.Equal(4, car.NumberOfWheels);
        });
    }

    [Fact]
    public void SearchVehicles_WhenNoVehiclesMatchFilters_ReturnsEmptyArray()
    {
        var garage = new Garage<Vehicle>(6);

        garage.AddMany(GetVehicles());

        Vehicle[] result = garage.SearchVehicles(
            VehicleColor.Green,
            99,
            typeof(Car)
        );

        Assert.Empty(result);
    }

    [Fact]
    public void SearchVehicles_WhenGarageIsEmpty_ReturnsEmptyArray()
    {
        var garage = new Garage<Vehicle>(6);

        Vehicle[] result = garage.SearchVehicles(null, null, null);

        Assert.Empty(result);
    }

    [Fact]
    public void GetEnumerator_WhenGarageIsEmpty_ReturnsNoVehicles()
    {
        var garage = new Garage<Vehicle>(3);

        Vehicle[] vehicles = garage.ToArray();

        Assert.Empty(vehicles);
    }

    [Fact]
    public void GetEnumerator_WhenGarageHasVehicles_ReturnsParkedVehicles()
    {
        var garage = new Garage<Vehicle>(3);

        var car = new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline);
        var motorcycle = new MotorCycle("MC123", VehicleColor.Red, 2, 500);

        garage.Add(car);
        garage.Add(motorcycle);

        Vehicle[] vehicles = garage.ToArray();

        Assert.Equal(2, vehicles.Length);
        Assert.Contains(car, vehicles);
        Assert.Contains(motorcycle, vehicles);
    }

    [Fact]
    public void GetEnumerator_WhenVehicleHasBeenRemoved_SkipsEmptySlots()
    {
        var garage = new Garage<Vehicle>(3);

        var firstCar = new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline);
        var secondCar = new Car("DEF456", VehicleColor.Red, 4, FuelType.Diesel);
        var thirdCar = new Car("GHI789", VehicleColor.Blue, 4, FuelType.Gasoline);

        Assert.Equal(AddVehicleResult.Success, garage.Add(firstCar));
        Assert.Equal(AddVehicleResult.Success, garage.Add(secondCar));
        Assert.Equal(AddVehicleResult.Success, garage.Add(thirdCar));

        bool removed = garage.RemoveByPlateNumber("DEF456");

        Vehicle[] vehicles = garage.ToArray();

        Assert.True(removed);
        Assert.Equal(2, vehicles.Length);
        Assert.Contains(firstCar, vehicles);
        Assert.DoesNotContain(secondCar, vehicles);
        Assert.Contains(thirdCar, vehicles);
    }

    [Fact]
    public void GetEnumerator_WhenUsedInForeach_IteratesOnlyParkedVehicles()
    {
        var garage = new Garage<Vehicle>(3);

        garage.Add(new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline));
        garage.Add(new Car("DEF456", VehicleColor.Red, 4, FuelType.Diesel));

        int count = 0;

        foreach (Vehicle vehicle in garage)
        {
            Assert.NotNull(vehicle);
            count++;
        }

        Assert.Equal(2, count);
    }



    [Fact]
    public void GetEnumerator_WhenUsedAsNonGenericEnumerable_ReturnsParkedVehicles()
    {
        var garage = new Garage<Vehicle>(2);

        var car = new Car("ABC123", VehicleColor.Black, 4, FuelType.Gasoline);

        garage.Add(car);

        IEnumerable enumerable = garage;

        IEnumerator enumerator = enumerable.GetEnumerator();

        Assert.True(enumerator.MoveNext());
        Assert.Same(car, enumerator.Current);
        Assert.False(enumerator.MoveNext());
    }

    private static Vehicle[] GetVehicles()
    {
        return
          [
              new Car("ABC123", VehicleColor.Red, 4, FuelType.Gasoline),
              new Car("abc321", VehicleColor.Black, 4, FuelType.Diesel),
              new MotorCycle("MC123", VehicleColor.Black, 2, 600),
              new Bus("bUs123", VehicleColor.Blue, 6, 45),
              new Boat("BoA123", VehicleColor.White, 0, 8),
              new AirPlane("air123", VehicleColor.Silver, 3, 2)
          ];
    }

    private static Vehicle[] GetVehiclesWithDuplicatePlateNumber()
    {
        return
          [
              new Car("ABC123", VehicleColor.Red, 4, FuelType.Gasoline),
              new Car("ABC123", VehicleColor.Black, 4, FuelType.Diesel),
              new MotorCycle("MC123", VehicleColor.Black, 2, 600),
              new Bus("MC123", VehicleColor.Blue, 6, 45),
              new Boat("BoA123", VehicleColor.White, 0, 8),
              new AirPlane("air123", VehicleColor.Silver, 3, 2)
          ];
    }
}
