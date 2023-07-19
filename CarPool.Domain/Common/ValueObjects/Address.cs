using System;
namespace CarPool.Domain.Common;


public class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string Province { get; private set; }
    public string PostalCode { get; private set; }

    public Address(string street, string city, string province, string postalCode)
    {
        Street = street;
        City = city;
        Province = province;
        PostalCode = postalCode;
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {Province} {PostalCode}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return Province;
        yield return PostalCode;
    }
}

