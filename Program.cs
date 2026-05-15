
using GarageV2.Interfaces;
using GarageV2.Moduls;
using GarageV2.UI;

internal class Program
{
    static void Main()
    {
        IConsoleMenu consoleMenu = new ConsoleMenu();
        IConsoleInputReader inputReader = new ConsoleInputReader();
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