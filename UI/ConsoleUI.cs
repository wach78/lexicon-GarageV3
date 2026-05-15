using GarageV2.Enums;
using GarageV2.Interfaces;

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
                _outputWriter.WriteLine("Invalid choice.");
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

                default:
                    _outputWriter.WriteLine("This menu option is not implemented yet.");
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
           _outputWriter.WriteLine("Invalid capacity.");
           _outputWriter.WaitForUser();
            return;
        }

        _garageHandler.CreateGarage(capacity.Value);

        _outputWriter.WriteLine($"Garage created with {capacity.Value} parking spaces.");
        _outputWriter.WaitForUser();
    }
}
