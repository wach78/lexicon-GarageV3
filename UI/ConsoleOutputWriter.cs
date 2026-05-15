using GarageV2.Interfaces;
using GarageV2.Enums;

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
                Console.WriteLine("Vehicle was parked successfully.");
                break;

            case AddVehicleResult.GarageFull:
                Console.WriteLine("Could not park vehicle. The garage is full.");
                break;

            case AddVehicleResult.DuplicatePlateNumber:
                Console.WriteLine("Could not park vehicle. A vehicle with that plate number already exists.");
                break;

            default:
                Console.WriteLine("Could not park vehicle. Unknown error.");
                break;
        }

        Console.WriteLine();

        WaitForUser();
    }
}
