// See https://aka.ms/new-console-template for more information

using Autofac;
using MagicBinder.CardsImporter.CompositionRoots;
using MagicBinder.CardsImporter.Services;
using MagicBinder.Core.Json;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;
using Newtonsoft.Json;

try
{
    var builder = new ContainerBuilder()
        .RegisterAppModules();
    var container = builder.Build();

    await using var scope = container.BeginLifetimeScope();
    var jsonService = scope.Resolve<CardsJsonService>();
    var fileName = Console.ReadLine();
    var json = jsonService.GetJson(fileName ?? "import.json");
    
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex);
}

var jsonSerializerSettings = new JsonSerializerSettings
{
    ContractResolver = new CustomJsonContractResolver(ModelMappings.GetFullCardMapping())
};

var result = JsonConvert.DeserializeObject<List<CardModel>>(streamReader.ReadToEnd(), jsonSerializerSettings);

var card = result.FirstOrDefault();

Console.WriteLine(card.Name);