using GarageV2.Enums;
using GarageV2.Interfaces;
using GarageV2.Moduls;

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
}
