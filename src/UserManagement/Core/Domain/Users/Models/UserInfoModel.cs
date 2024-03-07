using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Enums;

namespace Octopus.UserManagement.Core.Domain.Users.Models;

public class UserInfoModel(UserId id, string firstName, string lastName, string userName, string phoneNumber, List<RoleType> roleTypes)
{
	public UserId Id => id;
	public string FirstName => firstName;
	public string LastName => lastName;
	public string UserName => userName;
	public string PhoneNumber => phoneNumber;
	public List<RoleType> Roles => roleTypes;
}