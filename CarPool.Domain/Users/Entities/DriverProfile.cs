using CarPool.Domain.Common; 
namespace CarPool.Domain.Users.Entities;

public class DriverProfile : Entity<DriverProfileId>
{
    public Vehicle? Vehicle { get; private set; }
    public DriverLicense DriverLicense { get; private set; }
    public int TotalPassengersDriven { get; private set; }
    public int TotalKmsShared { get; private set; }

    private DriverProfile() { }

    private DriverProfile(DriverProfileId driverProfileId, Vehicle vehicle, DriverLicense driverLicense)
        : base(driverProfileId)
    {
        TotalPassengersDriven = 0;
        TotalKmsShared = 0;
        Vehicle = vehicle;
        DriverLicense = driverLicense;
    }

    public static DriverProfile Create(Vehicle vehicle, DriverLicense driverLicense)
    {
        return new(
            DriverProfileId.CreateUnique(),
            vehicle,
            driverLicense
        );
    }

    public void UpdateVehicle(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    public void UpdateDriverLicense(DriverLicense driverLicense)
    {
        DriverLicense = driverLicense;
    }


    public void UpdateExperience(int totalKmsShared, int totalPassengersDriven)
    {
        TotalKmsShared = totalKmsShared;
        TotalPassengersDriven = totalPassengersDriven;
    }

    public bool CanAcceptTrip(DateTime tripDate)
    {
        return DriverLicense?.DriverLicenseExpiryDate > tripDate;
    }

}

