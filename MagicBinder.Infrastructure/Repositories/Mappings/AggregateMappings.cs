using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace MagicBinder.Infrastructure.Repositories.Mappings;

public static class AggregateMappings
{
    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<Card>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.OracleId).SetIgnoreIfDefault(false);
            cm.SetIdMember(cm.GetMemberMap(c => c.OracleId));
            cm.IdMemberMap.SetIdGenerator(GuidGenerator.Instance);
            cm.MapMember(x => x.Layout).SetSerializer(new EnumSerializer<LayoutType>(BsonType.String));
        });

        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.UserGuid).SetIgnoreIfDefault(false);
            cm.SetIdMember(cm.GetMemberMap(c => c.UserGuid));
            cm.IdMemberMap.SetIdGenerator(GuidGenerator.Instance);
        });
    }
}