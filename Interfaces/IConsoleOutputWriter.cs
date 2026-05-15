using System;
using System.Collections.Generic;
using System.Text;

namespace GarageV2.Interfaces;

public interface IConsoleOutputWriter
{
    public void Write(string text);
    public void WriteLine(string text);
    public void WriteEmptyLine();
    public void WriteError(string text);
    public void WaitForUser();
}
