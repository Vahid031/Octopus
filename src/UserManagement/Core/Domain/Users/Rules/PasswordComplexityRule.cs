using Octopus.Core.Domain.Rules;
using System.Text.RegularExpressions;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

internal class PasswordComplexityRule : IBusinessRule
{
    private readonly string _password;

    public PasswordComplexityRule(string password)
    {
        _password = password;
    }

    public void Validate()
    {
        if (_password.Length < MinimumLength)
            throw new PasswordMinimumLengthException(MinimumLength);

        if (UpperCaseCount(_password) < UpperCaseLength)
            throw new PasswordMinimumUpperCaseLengthException(UpperCaseLength);

        if (LowerCaseCount(_password) < LowerCaseLength)
            throw new PasswordMinimumLowerCaseLengthException(LowerCaseLength);

        if (NumericCount(_password) < NumericLength)
            throw new PasswordMinimumNumericLengthException(NumericLength);

        if (NonAlphaCount(_password) < NonAlphaLength)
            throw new PasswordMinimumNonAlphaLengthException(NonAlphaLength);
    }

    private const int MinimumLength = 7;
    private const int UpperCaseLength = 0;
    private const int LowerCaseLength = 0;
    private const int NonAlphaLength = 0;
    private const int NumericLength = 0;

    private static int UpperCaseCount(string password) => Regex.Matches(password, "[A-Z]").Count;

    private static int LowerCaseCount(string password) => Regex.Matches(password, "[a-z]").Count;

    private static int NumericCount(string password) => Regex.Matches(password, "[0-9]").Count;

    private static int NonAlphaCount(string password) => Regex.Matches(password, @"[^0-9a-zA-Z\._]").Count;
}