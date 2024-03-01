using MongoDB.Driver;
using Octopus.Infrastructure.Mongo.Shared;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.Services;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.Catalog.Core.Mongo.Products;

internal class MongoUserRepository : MongoRepositoryBase<User, UserId>, IUserRepository
{
    public const string CollectionName = "Users";
    public MongoUserRepository(IUnitOfWork unitOfWork, IMongoCollection<User> collection)
        : base(unitOfWork, collection)
    {
    }

    public Task<User> GetByPhoneNumber(string phoneNumber) =>
        _collection.Find(x => x.PhoneNumber.Equals(phoneNumber)).SingleOrDefaultAsync();

}
