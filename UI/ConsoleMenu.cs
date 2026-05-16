using GarageV2.Enums;
using GarageV2.Interfaces;

namespace GarageV2.UI;

public class ConsoleMenu: IConsoleMenu
{
    public string GetMainMenuText()
    {

       return  
            $"""
            Welcome to the main menu.
            Enter a number to choose an option.

            {(int)MenuChoice.CreateGarage} = Create garage
            {(int)MenuChoice.PopulateGarage} = Populate garage with vehicles
            {(int)MenuChoice.ListParkedVehicles} = List all parked vehicles
            {(int)MenuChoice.ListVehicleTypes} = List vehicle types and count
            {(int)MenuChoice.ParkVehicle} = Park a vehicle
            {(int)MenuChoice.RemoveVehicle} = Remove a vehicle
            {(int)MenuChoice.FindVehicleByPlateNumber} = Find vehicle by plate number
            {(int)MenuChoice.SearchVehicles} = Search vehicles
            {(int)MenuChoice.Exit} = Exit

            Your choice:
            """;
      
    }

    public string GetVehicleTypeMenuText()
    {
        return
            $"""
            Choose vehicle type:
            {(int)VehicleTypeChoice.Car} = Car
            {(int)VehicleTypeChoice.Motorcycle} = Motorcycle
            {(int)VehicleTypeChoice.Bus} = Bus
            {(int)VehicleTypeChoice.Boat} = Boat
            {(int)VehicleTypeChoice.Airplane} = Airplane
            {(int)VehicleTypeChoice.Back} = Back to main menu

            Your choice:
            """;
        
    }

    public string GetSearchVehicleTypeMenuText()
    {
        return
            $"""
            Search vehicles by filters.
            Choose vehicle type.

            {(int)SearchVehicleTypes.All} = All vehicle types
            {(int)SearchVehicleTypes.Car} = Car
            {(int)SearchVehicleTypes.Motorcycle} = Motorcycle
            {(int)SearchVehicleTypes.Bus} = Bus
            {(int)SearchVehicleTypes.Boat} = Boat
            {(int)SearchVehicleTypes.Airplane} = Airplane

            Your choice:
            """;
    }

    public string GetVehicleColorMenuText()
    {
        return
            $"""
            Choose vehicle color:

            {(int)VehicleColor.Black} = Black
            {(int)VehicleColor.White} = White
            {(int)VehicleColor.Red} = Red
            {(int)VehicleColor.Blue} = Blue
            {(int)VehicleColor.Green} = Green
            {(int)VehicleColor.Silver} = Silver

            Your choice:
            """;
    }

    public string GetFuelTypeMenuText()
    {
        return
            $"""
            Choose Fuel:

            {(int)FuelType.Gasoline} = Gasoline
            {(int)FuelType.Diesel} = Diesel
            Your choice:
            """;
    }
}
