using System;
using System.Collections.Generic;
using System.Text;

namespace GarageV2.Interfaces;

public interface IConsoleMenu
{
    public string GetMainMenuText();
    public string GetVehicleTypeMenuText();
    public string GetSearchVehicleTypeMenuText();
    public string GetVehicleColorMenuText();
    public string GetFuelTypeMenuText();
}
