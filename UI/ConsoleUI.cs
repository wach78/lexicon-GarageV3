using GarageV2.Enums;
using GarageV2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GarageV2.UI;

public class ConsoleUI : IConsoleUI
{
    private readonly IConsoleMenu consoleMenu;
    private readonly IConsoleInputReader inputReader;
    private readonly IConsoleOutputWriter outputWriter;

    public ConsoleUI(
        IConsoleMenu consoleMenu, 
        IConsoleInputReader inputReader, 
        IConsoleOutputWriter outputWriter
        )
    {
        this.consoleMenu = consoleMenu;
        this.inputReader = inputReader;
        this.outputWriter = outputWriter;
    }

    public void Run()
    {
        bool isRunning = true;

        while (isRunning)
        {
            string menuText = consoleMenu.GetMainMenuText();

            outputWriter.Write(menuText);

            MenuChoice? menuChoice = inputReader.ReadMainMenuChoice();

            if (menuChoice is null)
            {
                outputWriter.WriteLine("Invalid choice.");
                outputWriter.WaitForUser();
                continue;
            }

            switch (menuChoice.Value)
            {
                case MenuChoice.Exit:
                    isRunning = false;
                    outputWriter.WriteLine("Exit.");
                    break;

                default:
                    outputWriter.WriteLine("This menu option is not implemented yet.");
                    outputWriter.WaitForUser();
                    break;
            }

            outputWriter.WriteEmptyLine();
        }
}
}