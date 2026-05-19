
using GarageV3.Interfaces;
using GarageV3.Moduls;
using GarageV3.UI;
using GarageV3.Validator;

internal class Program
{
    static void Main()
    {
        IConsoleMenu consoleMenu = new ConsoleMenu();
        IVehicleInputValidator vehicleInputValidator = new VehicleInputValidator();
        IConsoleInputReader inputReader = new ConsoleInputReader(vehicleInputValidator);
        IConsoleOutputWriter outputWriter = new ConsoleOutputWriter();
        IGarageHandler garageHandler = new GarageHandler();

        IConsoleUI consoleUI = new ConsoleUI(
            consoleMenu,
            inputReader,
            outputWriter,
            garageHandler
        );

        consoleUI.Run();
    }
}