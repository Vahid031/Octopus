using MongoDB.Driver;
using Octopus.Core.Domain.ValueObjects;
using Octopus.Infrastructure.Mongo.Shared;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;

namespace Octopus.UserManagement.Core.Mongo.Users;

internal class MongoUserRepository : MongoRepositoryBase<User, UserId>, IUserRepository
{
    public const string CollectionName = "Users";
    public MongoUserRepository(IUnitOfWork unitOfWork, IMongoCollection<User> collection)
        : base(unitOfWork, collection)
    {
    }


    public Task<User> GetByUserName(string userName) =>
        _collection.Find(x => x.UserName.Equals(userName)).SingleOrDefaultAsync();

    public async Task<User> GetByPhoneNumber(string phoneNumber)
    {
        var result = await _collection.FindAsync(u => u.PhoneNumber.Equals(phoneNumber));
        return await result.SingleOrDefaultAsync();
    }

    public bool Exists(string userName) =>
        _collection.Find(x => x.UserName == userName).Any();
}
