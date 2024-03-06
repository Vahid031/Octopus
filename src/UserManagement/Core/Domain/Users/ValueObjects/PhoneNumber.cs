using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using System.Text.RegularExpressions;

namespace Octopus.UserManagement.Core.Domain.Users.ValueObjects;

public class PhoneNumber : ValueObject<PhoneNumber>
{
    public string CountryCode { get; private set; }
    public string Number { get; private set; }
    public string Extension { get; private set; }

    private PhoneNumber(string countryCode, string number, string extension)
    {
        CountryCode = countryCode;
        Number = number;
        Extension = extension;
    }

    public static PhoneNumber Create(string countryCode, string number, string? extension = null)
    {
        ValidateCountryCode(countryCode);
        ValidatePhoneNumber(number);
        return new PhoneNumber(countryCode, number, extension);
    }

    public static PhoneNumber FromString(string phoneNumber)
    {
        // Assuming the format is +CountryCode-Number-Extension
        var match = Regex.Match(phoneNumber, @"\+(?<countryCode>(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|4[987654310]|3[9643210]|2[70]|7|1))(-)?(?<number>\d{1,14})(-)?(?<extension>\d+)?$");
        if (match.Success)
        {
            var countryCode = match.Groups["countryCode"].Value;
            var number = match.Groups["number"].Value;
            var extension = match.Groups["extension"].Success ? match.Groups["extension"].Value : null;
            return Create(countryCode, number, extension);
        }
        else
        {
            throw new UserPhoneNumberInvalidException("Invalid phone number format.");
        }
    }

    private static void ValidateCountryCode(string countryCode)
    {
        if (string.IsNullOrWhiteSpace(countryCode))
            throw new UserPhoneNumberInvalidCountryCodeException(countryCode);
        // You can add more validation logic for country code if needed
    }

    private static void ValidatePhoneNumber(string number)
    {
        // You can adjust the regex pattern based on your requirements
        var pattern = @"^\d{10}$"; // Assuming a 10-digit number
        if (!Regex.IsMatch(number, pattern))
            throw new UserPhoneNumberInvalidException(number);
    }

    public static implicit operator PhoneNumber(string phoneNumber) =>
        FromString(phoneNumber);

    public static explicit operator string(PhoneNumber phoneNumber) =>
        phoneNumber.ToString();

    public override bool ObjectIsEqual(PhoneNumber other)
    {
        return other.CountryCode == CountryCode && other.Number == Number && other.Extension == Extension;
    }

    public override int ObjectGetHashCode()
    {
        return HashCode.Combine(CountryCode, Number, Extension);
    }

    public override string ToString()
    {
        return $"+{CountryCode}{Number}{Extension}";
    }

    protected IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return Number;
        yield return Extension ?? string.Empty;
    }
}