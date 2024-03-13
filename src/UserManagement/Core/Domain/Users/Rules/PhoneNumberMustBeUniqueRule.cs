using Octopus.Core.Domain.Rules;
using Octopus.UserManagement.Core.Domain.Users.Exceptions;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Domain.Users.Rules;

public class PhoneNumberMustBeUniqueRule : IAsyncBusinessRule
{
    private readonly IUserRepository _userRepository;
    private readonly PhoneNumber _phoneNumber;

    public PhoneNumberMustBeUniqueRule(IUserRepository userRepository, PhoneNumber phoneNumber)
    {
        _userRepository = userRepository;
        _phoneNumber = phoneNumber;
    }

    public async Task Validate()
    {
        if (await _userRepository.ExistsWithPhoneNumber(_phoneNumber))
            throw new PhoneNumberMustBeUniqueException(_phoneNumber.ToString());
    }
}