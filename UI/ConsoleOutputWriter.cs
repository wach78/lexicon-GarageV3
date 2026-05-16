using GarageV2.Enums;
using GarageV2.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace GarageV2.UI;

public class ConsoleOutputWriter : IConsoleOutputWriter
{
    public void Write(string text)
    {
        Console.Write(text);
    }

    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }

    public void WriteEmptyLine()
    {
        Console.WriteLine();
    }

    public void WriteError(string text)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;

        Console.WriteLine(text);

        Console.ForegroundColor = originalColor;
    }

    public void WaitForUser()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey(intercept: true);
        Console.WriteLine();
    }

    public void WriteAddVehicleResultMessage(AddVehicleResult result)
    {
        switch (result)
        {
            case AddVehicleResult.Success:
                WriteLine("Vehicle was parked successfully.");
                break;

            case AddVehicleResult.GarageFull:
                WriteError("Could not park vehicle. The garage is full.");
                break;

            case AddVehicleResult.DuplicatePlateNumber:
                WriteError("Could not park vehicle. A vehicle with that plate number already exists.");
                break;

            case AddVehicleResult.GarageNotCreated:
                WriteError("Could not park vehicle. Create garage first.");
                break;

            default:
                WriteError("Could not park vehicle. Unknown error.");
                break;
        }

        WriteEmptyLine();

        WaitForUser();
    }
}
