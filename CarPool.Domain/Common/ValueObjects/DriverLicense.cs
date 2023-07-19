using System;
namespace CarPool.Domain.Common;

public class DriverLicense : ValueObject
{
    public string DriverLicenseNumber { get; private set; }
    public DateTime DriverLicenseExpiryDate { get; private set; }

    public DriverLicense(string driverLicenseNumber, DateTime driverLicenseExpiryDate)
    {
        DriverLicenseNumber = driverLicenseNumber;
        DriverLicenseExpiryDate = driverLicenseExpiryDate;
    }
    public override string ToString()
    {
        return $"{DriverLicenseNumber}, {DriverLicenseExpiryDate}";
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DriverLicenseNumber;
        yield return DriverLicenseExpiryDate;
    }
}

