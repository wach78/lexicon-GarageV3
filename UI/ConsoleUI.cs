using GarageV2.Enums;
using GarageV2.Interfaces;
using GarageV2.Moduls;
using Microsoft.VisualBasic.FileIO;
using System.Reflection.Metadata.Ecma335;

namespace GarageV2.UI;

public class ConsoleUI : IConsoleUI
{
    private readonly IConsoleMenu _consoleMenu;
    private readonly IConsoleInputReader _inputReader;
    private readonly IConsoleOutputWriter _outputWriter;
    private readonly IGarageHandler _garageHandler;

    public ConsoleUI(
        IConsoleMenu consoleMenu,
        IConsoleInputReader inputReader,
        IConsoleOutputWriter outputWriter,
        IGarageHandler garageHandler
        )
    {
        this._consoleMenu = consoleMenu;
        this._inputReader = inputReader;
        this._outputWriter = outputWriter;
        this._garageHandler = garageHandler;
    }

    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            string menuText = _consoleMenu.GetMainMenuText();

            _outputWriter.Write(menuText);

            MenuChoice? menuChoice = _inputReader.ReadMainMenuChoice();

            if (menuChoice is null)
            {
                _outputWriter.WriteError("Invalid choice.");
                _outputWriter.WaitForUser();
                continue;
            }

            switch (menuChoice.Value)
            {
                case MenuChoice.Exit:
                    isRunning = false;
                    _outputWriter.WriteLine("Exit.");
                    break;

                case MenuChoice.CreateGarage:
                    HandleCreateGarage();
                    break;

                case MenuChoice.PopulateGarage:
                    HandlePopulateGarage();
                    break;

                case MenuChoice.ListParkedVehicles:
                    HandleListAllVehicles();
                    break;

                case MenuChoice.ListVehicleTypes:
                    HandleListVehiclesTypes();
                    break;

                case MenuChoice.ParkVehicle:
                    HandleParkedVehicle();
                    break;

                case MenuChoice.RemoveVehicle:
                    HandleRemoveVehicle();
                    break;

                case MenuChoice.FindVehicleByPlateNumber:
                    HandleFindVehicleByPlateNumber();
                    break;

                default:
                    _outputWriter.WriteError("This menu option is not implemented yet.");
                    _outputWriter.WaitForUser();
                    break;
            }

            _outputWriter.WriteEmptyLine();
        }
    }

    private void HandleCreateGarage()
    {
        _outputWriter.Write("Enter garage capacity: ");

        int? capacity = _inputReader.ReadPositiveInt();

        if (capacity is null)
        {
            _outputWriter.WriteError("Invalid capacity.");
            _outputWriter.WaitForUser();
            return;
        }

        _garageHandler.CreateGarage(capacity.Value);

        _outputWriter.WriteLine($"Garage created with {capacity.Value} parking spaces.");
        _outputWriter.WaitForUser();
    }

    private void HandlePopulateGarage()
    {
        if (!EnsureGarageCreated())
        {
            return;
        }

        int? addedVehicles = _garageHandler.Populate();

        _outputWriter.WriteLine($"{addedVehicles} vehicles were added to the garage.");
        _outputWriter.WaitForUser();
    }

    private void HandleListAllVehicles()
    {
        if (!EnsureGarageCreated())
        {
            return;
        }

        Vehicle[]? vehicles = _garageHandler.GetParkedVehicles();

        if (vehicles is null || vehicles.Length == 0)
        {
            _outputWriter.WriteLine("The garage is empty.");
            _outputWriter.WaitForUser();
            return;
        }

        foreach (Vehicle vehicle in vehicles)
        {
            _outputWriter.WriteLine(vehicle.ToString());
        }
    }

    private void HandleListVehiclesTypes()
    {
        if (!EnsureGarageCreated())
        {
            return;
        }

        Dictionary<string, int>? vehicleTypeCounts = _garageHandler.GetParkedVehicleTypeCounts();

        if (vehicleTypeCounts is null)
        {
            _outputWriter.WriteError("You must create a garage first.");
            _outputWriter.WaitForUser();
            return;
        }

        if (vehicleTypeCounts.Count == 0)
        {
            _outputWriter.WriteLine("The garage is empty.");
            _outputWriter.WaitForUser();
            return;
        }

        foreach (KeyValuePair<string, int> vehicleTypeCount in vehicleTypeCounts)
        {
            _outputWriter.WriteLine($"{vehicleTypeCount.Key}: {vehicleTypeCount.Value}");
        }

        _outputWriter.WaitForUser();

    }

    public void HandleRemoveVehicle()
    {
        if (!EnsureGarageCreated())
        {
            return;
        }

        _outputWriter.Write("Enter plate number:");
        string? platenumber = _inputReader.ReadRequiredString();

        var isDeleted = _garageHandler.RemoveByPlateNumber(platenumber);

        if (isDeleted is true)
        {
            _outputWriter.WriteLine($"Vehicle removed for plate number {platenumber}");
        }
        else
        {
            _outputWriter.WriteError($"Vehicle not found for plate number {platenumber}");
        }
    }

    public void HandleFindVehicleByPlateNumber()
    {
        if (!EnsureGarageCreated())
        {
            return;
        }

        _outputWriter.Write("Enter plate number:");
        string? platenumber = _inputReader.ReadRequiredString();

        var vehicle = _garageHandler.FindByPlateNumber(platenumber);

        if (vehicle is null)
        {
            _outputWriter.WriteLine($"Vehicle not found for plate number {platenumber}");
            _outputWriter.WaitForUser();
            return;
        }

        _outputWriter.WriteLine($"Vehicle found: {vehicle}");
        _outputWriter.WaitForUser();
    }

    public void HandleParkedVehicle()
    {
        if (!EnsureGarageCreated())
        {
            return;
        }

        _outputWriter.Write(_consoleMenu.GetVehicleTypeMenuText());

        VehicleTypeChoice? vehicleTypeChoice = _inputReader.ReadVehicleTypeChoice();

        if (vehicleTypeChoice is null)
        {
            _outputWriter.WriteError("Invalid vehicle type.");
            _outputWriter.WaitForUser();
            return;
        }

        if (vehicleTypeChoice == VehicleTypeChoice.Back)
        {
            return;
        }

        CommonVehicleData? commonVehicleData = ReadCommonVehicleData();

        if (commonVehicleData is null)
        {
            return;
        }

        Vehicle? vehicle = CreateVehicleFromChoice(vehicleTypeChoice.Value, commonVehicleData);

        if (vehicle is null)
        {
            return;
        }

        AddVehicleResult result = _garageHandler.ParkVehicle(vehicle);

        _outputWriter.WriteAddVehicleResultMessage(result);
    }


    private bool EnsureGarageCreated()
    {
        if (_garageHandler.HasGarage)
        {
            return true;
        }

        _outputWriter.WriteError("You must create a garage first.");
        _outputWriter.WaitForUser();

        return false;
    }

    private CommonVehicleData? ReadCommonVehicleData()
    {
        _outputWriter.Write("Enter plate number: ");
        string? numberPlate = _inputReader.ReadRequiredString();

        if (numberPlate is null)
        {
            _outputWriter.WriteError("Invalid plate number.");
            _outputWriter.WaitForUser();
            return null;
        }

        _outputWriter.Write("Enter number of wheels: ");
        int? numberOfWheels = _inputReader.ReadZeroOrPositiveInt();

        if (numberOfWheels is null)
        {
            _outputWriter.WriteError("Invalid number of wheels.");
            _outputWriter.WaitForUser();
            return null;
        }

        _outputWriter.Write(_consoleMenu.GetVehicleColorMenuText());


        VehicleColor? color = _inputReader.ReadVehicleColor();

        if (color is null)
        {
            _outputWriter.WriteError("Invalid color.");
            _outputWriter.WaitForUser();
            return null;
        }

        return new CommonVehicleData(
            numberPlate,
            color.Value,
            numberOfWheels.Value
        );
    }

    private Vehicle? CreateVehicleFromChoice(VehicleTypeChoice vehicleTypeChoice, CommonVehicleData commonVehicleData)
    {
        switch (vehicleTypeChoice)
        {
            case VehicleTypeChoice.Car:
                return CreateCar(commonVehicleData);
           
            case VehicleTypeChoice.Motorcycle:
                return CreateMotorCycle(commonVehicleData);
 
            case VehicleTypeChoice.Bus:
                return CreateBus(commonVehicleData);

            case VehicleTypeChoice.Boat:
                return CreateBoat(commonVehicleData);

            case VehicleTypeChoice.Airplane:
                return CreateAirPlane(commonVehicleData);
            
            default:
                return null;
        }
    }

    private Vehicle? CreateCar(CommonVehicleData commonVehicleData)
    {
        _outputWriter.Write(_consoleMenu.GetFuelTypeMenuText());
      
        FuelType? fuelType = _inputReader.ReadFuelType();

        if (fuelType is null)
        {
            _outputWriter.WriteError("Invalid fuel type.");
            _outputWriter.WaitForUser();
            return null;
        }

        return new Car(
            commonVehicleData.NumberPlate,
            commonVehicleData.Color,
            commonVehicleData.NumberOfWheels,
            fuelType.Value
        );
    }

    private Vehicle? CreateMotorCycle(CommonVehicleData commonVehicleData)
    {
        _outputWriter.Write("Enter Cylinder Volume: ");

        int? cylinderVolume = _inputReader.ReadPositiveInt();

        if (cylinderVolume is null)
        {
            _outputWriter.WriteLine("Invalid cylinderVolume.");
            return null;
        }

        return new MotorCycle(
            commonVehicleData.NumberPlate,
            commonVehicleData.Color,
            commonVehicleData.NumberOfWheels,
            cylinderVolume.Value
        );
    }

    private Vehicle? CreateBus(CommonVehicleData commonVehicleData)
    {
        _outputWriter.Write("Enter Number of Seats: ");
        int? seats = _inputReader.ReadPositiveInt();

        if (seats is null)
        {
            _outputWriter.WriteLine("Invalid number of seats.");
            return null;
        }

        return new Bus(
            commonVehicleData.NumberPlate,
            commonVehicleData.Color,
            commonVehicleData.NumberOfWheels,
            seats.Value
        );
    }

    private Vehicle? CreateBoat(CommonVehicleData commonVehicleData)
    {
        _outputWriter.Write("Enter lenght: ");
        int? length = _inputReader.ReadPositiveInt();

        if (length is null)
        {
            _outputWriter.WriteLine("Invalid length");
            return null;
        }

        return new Boat(
            commonVehicleData.NumberPlate,
            commonVehicleData.Color,
            commonVehicleData.NumberOfWheels,
            length.Value
        );
    }

    private Vehicle? CreateAirPlane(CommonVehicleData commonVehicleData)
    {
        _outputWriter.Write("Enter number of engines: ");
        int? numberOfEngines = _inputReader.ReadPositiveInt();

        if (numberOfEngines is null)
        {
            _outputWriter.WriteLine("Invalid number of engines.");
            return null;
        }

        return new AirPlane(
            commonVehicleData.NumberPlate,
            commonVehicleData.Color,
            commonVehicleData.NumberOfWheels,
            numberOfEngines.Value
        );
    }
}
