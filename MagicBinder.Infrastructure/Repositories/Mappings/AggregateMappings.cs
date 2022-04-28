using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace MagicBinder.Infrastructure.Repositories.Mappings;

public static class AggregateMappings
{
    public static void RegisterClassMaps()
    {
        RegisterInventoryMappings();
        RegisterCardsMaps();
        RegisterDictionariesMaps();
        RegisterUsersMaps();
    }

    private static void RegisterInventoryMappings()
    {
        BsonClassMap.RegisterClassMap<Inventory>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.Key).SetIgnoreIfDefault(false);
            cm.SetIdMember(cm.GetMemberMap(x => x.Key));
        });
    }

    private static void RegisterCardsMaps()
    {
        BsonClassMap.RegisterClassMap<Card>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.OracleId).SetIgnoreIfDefault(false);
            cm.SetIdMember(cm.GetMemberMap(c => c.OracleId));
            cm.IdMemberMap.SetIdGenerator(GuidGenerator.Instance);
            cm.GetMemberMap(x => x.Layout).SetSerializer(new EnumSerializer<LayoutType>(BsonType.String));
            cm.GetMemberMap(x => x.LegalIn).SetSerializer(new ArraySerializer<FormatType>(new EnumSerializer<FormatType>(BsonType.String)));
            cm.GetMemberMap(x => x.Games).SetSerializer(new ArraySerializer<GameType>(new EnumSerializer<GameType>(BsonType.String)));
            cm.GetMemberMap(x => x.Colors).SetSerializer(new ArraySerializer<ColorType>(new EnumSerializer<ColorType>(BsonType.String)));
            cm.GetMemberMap(x => x.ColorIdentity).SetSerializer(new ArraySerializer<ColorType>(new EnumSerializer<ColorType>(BsonType.String)));
        });

        BsonClassMap.RegisterClassMap<CardPrinting>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.Games).SetSerializer(new ArraySerializer<GameType>(new EnumSerializer<GameType>(BsonType.String)));
            cm.GetMemberMap(x => x.LegalIn).SetSerializer(new ArraySerializer<FormatType>(new EnumSerializer<FormatType>(BsonType.String)));
        });

        BsonClassMap.RegisterClassMap<CardFace>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.Layout).SetSerializer(new EnumSerializer<LayoutType>(BsonType.String));
            cm.GetMemberMap(x => x.Colors).SetSerializer(new ArraySerializer<ColorType>(new EnumSerializer<ColorType>(BsonType.String)));
        });
    }

    private static void RegisterDictionariesMaps()
    {
        BsonClassMap.RegisterClassMap<Set>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.SetId).SetIgnoreIfDefault(false);
            cm.SetIdMember(cm.GetMemberMap(c => c.SetId));
            cm.IdMemberMap.SetIdGenerator(GuidGenerator.Instance);
        });
    }

    private static void RegisterUsersMaps()
    {
        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.AutoMap();
            cm.GetMemberMap(x => x.UserId).SetIgnoreIfDefault(false);
            cm.SetIdMember(cm.GetMemberMap(c => c.UserId));
            cm.IdMemberMap.SetIdGenerator(GuidGenerator.Instance);
        });
    }
}