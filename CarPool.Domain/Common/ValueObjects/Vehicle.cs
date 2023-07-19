using System;
namespace CarPool.Domain.Common;


public class Vehicle : ValueObject
{
    public string Make { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public string Colour { get; private set; }
    public string LicensePlate { get; private set; }

    public Vehicle(string make, string model, int year, string colour, string licensePlate)
    {
        Make = make;
        Model = model;
        Year = year;
        Colour = colour;
        LicensePlate = licensePlate;
    }

    public override string ToString()
    {
        return $"{Year}, {Colour} {Make} {Model} - {LicensePlate}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
        yield return Colour;
        yield return Make;
        yield return Model;
        yield return LicensePlate;
    }
}

