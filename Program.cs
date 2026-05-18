
using GarageV2.Interfaces;
using GarageV2.Moduls;
using GarageV2.UI;
using GarageV2.Validator;

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