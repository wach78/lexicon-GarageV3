

namespace GarageV2.Moduls;

public class MotorCycle: Vehicle
{
    public MotorCycle(string numberPlate, string color, int numberOfWheels, int cylinderVolume)
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
