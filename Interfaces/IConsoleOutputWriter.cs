using GarageV3.Enums;

namespace GarageV3.Interfaces;

public interface IConsoleOutputWriter
{
    public void Write(string text);
    public void WriteLine(string text);
    public void WriteEmptyLine();
    public void WriteError(string text);
    public void WaitForUser();
    public void WriteAddVehicleResultMessage(AddVehicleResult result);
}
