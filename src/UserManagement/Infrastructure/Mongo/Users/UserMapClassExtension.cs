using MongoDB.Bson.Serialization;
using Octopus.Core.Domain.Entities;
using Octopus.Core.Domain.ValueObjects;
using Octopus.UserManagement.Core.Domain.Users.Entities;
using Octopus.UserManagement.Core.Domain.Users.ValueObjects;

namespace Octopus.UserManagement.Core.Mongo.Users;

internal class UserMapClassExtension
{
    internal static void Register()
    {
        BsonClassMap.RegisterClassMap<EntityBase<UserId>>(cm =>
        {
            cm.MapMember(m => m.Id).SetElementName("_id");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.MapMember(m => m.UserName).SetElementName("UserName");
            cm.MapMember(m => m.FirstName).SetElementName("FirstName");
            cm.MapMember(m => m.LastName).SetElementName("LastName");
            cm.MapMember(m => m.RefreshTokens).SetElementName("RefreshTokens");
            cm.MapMember(m => m.PhoneNumber).SetElementName("PhoneNumber");
            cm.MapMember(m => m.OtpCodes).SetElementName("OtpCodes");
            cm.MapMember(m => m.Password).SetElementName("Password");
            cm.MapMember(m => m.IsActivated).SetElementName("IsActivated");
            cm.MapMember(m => m.CreatedAt).SetElementName("CreatedAt");
            cm.MapMember(m => m.Roles).SetElementName("Roles");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<RefreshTokenInfo>(cm =>
        {
            cm.MapMember(m => m.Expires).SetElementName("Expires");
            cm.MapMember(m => m.RevokedByIp).SetElementName("RevokedByIp");
            cm.MapMember(m => m.CreatedAt).SetElementName("CreatedAt");
            cm.MapMember(m => m.CreatedByIp).SetElementName("CreatedByIp");
            cm.MapMember(m => m.RevokedAt).SetElementName("RevokedAt");
            cm.MapMember(m => m.Token).SetElementName("Token");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<OtpCode>(cm =>
        {
            cm.MapMember(m => m.Code).SetElementName("Code");
            cm.MapMember(m => m.CreatedAt).SetElementName("CreatedAt");
            cm.MapMember(m => m.CreatedByIp).SetElementName("CreatedByIp");
            cm.MapMember(m => m.RetryCount).SetElementName("RetryCount");
            cm.MapMember(m => m.RevokedAt).SetElementName("Revoked");
            cm.MapMember(m => m.RevokedByIp).SetElementName("RevokedByIp");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<Password>(cm =>
        {
            cm.MapMember(m => m.PasswordSalt).SetElementName("PasswordSalt");
            cm.MapMember(m => m.PasswordHash).SetElementName("PasswordHash");

            cm.SetIgnoreExtraElements(true);
        });

        BsonClassMap.RegisterClassMap<PhoneNumber>(cm =>
        {
            cm.MapMember(m => m.Extension).SetElementName("Extension");
            cm.MapMember(m => m.Number).SetElementName("Number");
            cm.MapMember(m => m.CountryCode).SetElementName("CountryCode");

            cm.SetIgnoreExtraElements(true);
        });
    }
}