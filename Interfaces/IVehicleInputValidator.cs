namespace GarageV2.Interfaces
{
    public interface IVehicleInputValidator
    {
        bool IsValidPlateNumber(string? plateNumber);

        bool IsValidNumberOfWheels(int numberOfWheels);

        bool IsValidBoatLength(int length);

        bool IsValidNumberOfSeats(int numberOfSeats);

        bool IsValidCylinderVolume(int cylinderVolume);

        bool IsValidNumberOfEngines(int numberOfEngines);
    }
}
