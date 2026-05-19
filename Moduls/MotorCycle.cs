using GarageV3.Enums;

namespace GarageV3.Moduls;

public class MotorCycle: Vehicle
{
    public MotorCycle(string numberPlate, VehicleColor color, int numberOfWheels, int cylinderVolume)
    : base(numberPlate, color, numberOfWheels)
    {
        CylinderVolume = cylinderVolume;
    }
    public int CylinderVolume { get;}

    public override string ToString()
    {
        return $"{base.ToString()}, Cylinder Volume: {CylinderVolume}";
    }
}
