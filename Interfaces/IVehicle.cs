using GarageV2.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageV2.Interfaces;

public interface IVehicle
{
    string NumberPlate { get; }

    VehicleColor Color { get; }

    int NumberOfWheels { get; }
}
