using MagicBinder.Core.Json;
using MagicBinder.Infrastructure.Integrations.Scryfall.Mappers;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;
using Newtonsoft.Json;

namespace MagicBinder.Infrastructure.Integrations.Scryfall;

public class JsonSetsParser
{
    public virtual List<SetModel> ParseSets(string json)
    {
        var jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CustomJsonContractResolver(SetsModelMapper.GetFullSetsMapping())
        };

        var result = JsonConvert.DeserializeObject<SetsImportModel>(json, jsonSerializerSettings);
        return result?.Sets ?? new List<SetModel>();
    }
}