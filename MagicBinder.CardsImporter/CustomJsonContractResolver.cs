using Newtonsoft.Json.Serialization;

namespace MagicBinder.CardsImporter;

public class CustomJsonContractResolver : DefaultContractResolver
{
    private Dictionary<string, string> PropertyMappings { get; }

    public CustomJsonContractResolver(Dictionary<string, string> mappings)
    {
        PropertyMappings = mappings;
    }

    protected override string ResolvePropertyName(string propertyName)
    {
        var resolved = this.PropertyMappings.TryGetValue(propertyName, out var resolvedName);
        return (resolved && resolvedName != null) ? resolvedName : base.ResolvePropertyName(propertyName);
    }
}