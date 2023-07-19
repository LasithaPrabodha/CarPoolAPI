using CarPool.Domain.Common;
using CarPool.Domain.Common.Models;
using CarPool.Domain.Users.Entities;
using CarPool.Domain.Users.Enums;

namespace CarPool.Domain.Users;

public class User : AggregateRoot<UserId, Guid>, IAggregateRoot
{
    public string Username { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public DateTime? DateOfBirth { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Bio { get; private set; }
    public Address? Address { get; private set; }

    public Gender? Gender { get; private set; }
    public Language? Language { get; private set; }
    public ChatPreference? ChatPreference { get; private set; }
    public ScentsPreference? ScentsPreference { get; private set; }

    public bool? IsDriver { get; private set; }
    public DriverProfile? DriverProfile { get; private set; }

    private User() { }

    private User(UserId userId, string username,  string name, string email)
        : base(userId)
    {
        Username = username;
        Name = name;
        Email = email;
    }

    public static User Create(string username, string name, string email)
    {
        return new(
            UserId.CreateUnique(),
            username,
            name,
            email
        );
    }

    public void AddDriverDetails(Vehicle vehicle, DriverLicense driverLicense)
    {
        var driverProfile = DriverProfile.Create(vehicle, driverLicense);
        DriverProfile = driverProfile;
    }

    public void SetDriverStatus(bool isDriver)
    {
        IsDriver = isDriver;
    }

   
    public void UpdateProfile(
        DateTime? dob = null,
        string? phone = null,
        string? email = null,
        Gender? gender = null,
        string? bio = null,
        Address? address = null,
        Language? language = null,
        ChatPreference? chatPreference = null,
        ScentsPreference? scentsPreference = null)
    {
        DateOfBirth = dob ?? DateOfBirth;
        Email = email ?? Email;
        PhoneNumber = phone ?? PhoneNumber;
        Bio = bio ?? Bio;
        Gender = gender ?? Gender;
        Address = address ?? Address;
        Language = language ?? Language;
        ChatPreference = chatPreference ?? ChatPreference;
        ScentsPreference = scentsPreference ?? ScentsPreference;
    }
}

