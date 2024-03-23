using Octopus.Core.Domain.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Customers.ValueObjects;

public class GeoLocation : ValueObject<GeoLocation>
{
    public decimal Latitude { get; private protected set; }
    public decimal Longitude { get; private protected set; }

    private GeoLocation(decimal latitude, decimal longitude)
    {
        if (!(latitude is <= 90 and >= -90))
            throw new Exception();

        if (!(longitude is <= 90 and >= -90))
            throw new Exception();

        Latitude = latitude;
        Longitude = longitude;
    }

    public static GeoLocation Create(decimal latitude, decimal longitude)
    {
        return new(latitude, longitude);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}
