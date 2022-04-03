using MagicBinder.Domain.Aggregates;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace MagicBinder.Infrastructure.Repositories.Mappings;

public static class AggregateMappings
{
    public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<Card>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.CardId).SetIgnoreIfDefault(false);
            cm.SetIdMember(cm.GetMemberMap(c => c.CardId));
            cm.IdMemberMap.SetIdGenerator(GuidGenerator.Instance);
        });
    }
}