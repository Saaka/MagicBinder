using MagicBinder.Core.Json;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;
using Newtonsoft.Json;

namespace MagicBinder.Infrastructure.Integrations.Scryfall;

public class JsonCardsParser
{
    public virtual List<CardModel> ParseCards(string json)
    {
        var jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CustomJsonContractResolver(ModelMappings.GetFullCardMapping())
        };

        var result = JsonConvert.DeserializeObject<List<CardModel>>(json, jsonSerializerSettings);
        return result ?? new List<CardModel>();
    }
}