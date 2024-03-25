using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;
using Xunit;

namespace Octopus.UserManagementTest.Core.DomainTest.Users.ValueObjects;

public class PhoneNumberTests
{
	[Theory]
	[InlineData("98", "9024135634", null)]
	[InlineData("1", "1234567890", null)]
	[InlineData("44", "7890123456", "123")]
	public void Create_Should_CreateValidPhoneNumberInstance(string countryCode, string number, string extension)
	{
		// Act
		var phoneNumber = PhoneNumber.Create(countryCode, number, extension);

		// Assert
		Assert.NotNull(phoneNumber);
		Assert.Equal(countryCode, phoneNumber.CountryCode);
		Assert.Equal(number, phoneNumber.Number);
		Assert.Equal(extension, phoneNumber.Extension);
	}

	[Theory]
	[InlineData("+989024135634", "98", "9024135634", null)]
	[InlineData("+11234567890", "1", "1234567890", null)]
	public void FromString_Should_ParseValidPhoneNumberString(string phoneNumberString, string expectedCountryCode, string expectedNumber, string expectedExtension)
	{
		// Act
		var phoneNumber = PhoneNumber.FromString(phoneNumberString);

		// Assert
		Assert.NotNull(phoneNumber);
		Assert.Equal(expectedCountryCode, phoneNumber.CountryCode);
		Assert.Equal(expectedNumber, phoneNumber.Number);
		Assert.Equal(expectedExtension, phoneNumber.Extension);
	}

	[Theory]
	[InlineData("+98-9024135634", "98", "9024135634", null)]
	[InlineData("+1-1234567890", "1", "1234567890", null)]
	public void FromString_Should_ParseValidPhoneNumberStringWithDash(string phoneNumberString, string expectedCountryCode, string expectedNumber, string expectedExtension)
	{
		// Act
		var phoneNumber = PhoneNumber.FromString(phoneNumberString);

		// Assert
		Assert.NotNull(phoneNumber);
		Assert.Equal(expectedCountryCode, phoneNumber.CountryCode);
		Assert.Equal(expectedNumber, phoneNumber.Number);
		Assert.Equal(expectedExtension, phoneNumber.Extension);
	}

	[Theory]
	[InlineData("+989024135634")]
	[InlineData("+11234567890")]
	public void ToString_Should_ConvertPhoneNumberToString(string phoneNumberString)
	{
		// Arrange
		var phone = PhoneNumber.FromString(phoneNumberString);

		// Act
		var phoneNumber = phone.ToString();

		// Assert
		Assert.NotNull(phoneNumber);
		Assert.Equal(phoneNumberString, phoneNumber);
	}

	[Theory]
	[InlineData("+1234567890")]
	[InlineData("+1987654321")]
	[InlineData("+44987654321")]
	public void FromString_Should_ThrowExceptionWhenParsingInvalidString(string invalidPhoneNumberString)
	{
		// Act & Assert
		Assert.Throws<UserPhoneNumberInvalidException>(() => PhoneNumber.FromString(invalidPhoneNumberString));
	}
}