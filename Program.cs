
using GarageV2.Interfaces;
using GarageV2.UI;

internal class Program
{
    static void Main()
    {
        IConsoleMenu consoleMenu = new ConsoleMenu();
        IConsoleInputReader inputReader = new ConsoleInputReader();
        IConsoleOutputWriter outputWriter = new ConsoleOutputWriter();

        IConsoleUI consoleUI = new ConsoleUI(
            consoleMenu,
            inputReader,
            outputWriter
        );

        consoleUI.Run();
    }
}