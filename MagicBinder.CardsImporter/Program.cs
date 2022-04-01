// See https://aka.ms/new-console-template for more information

using Autofac;
using MagicBinder.CardsImporter;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using MagicBinder.Infrastructure.Integrations.Scryfall;
using MagicBinder.Infrastructure.Integrations.Scryfall.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// IContainer Container = null;
var builder = new ContainerBuilder();
// builder.RegisterType<ConsoleOutput>().As<IOutput>();
// builder.RegisterType<TodayWriter>().As<IDateWriter>();
// Container = builder.Build();

Console.WriteLine("Hello, World!");
var fileName = "cards.json";

Console.WriteLine(fileName);
using var streamReader = File.OpenText(fileName);
var jsonSerializerSettings = new JsonSerializerSettings
{
    ContractResolver = new CustomJsonContractResolver(ModelMappings.GetFullCardMapping())
};

var result = JsonConvert.DeserializeObject<List<CardModel>>(streamReader.ReadToEnd(), jsonSerializerSettings);

var card = result.FirstOrDefault();
Console.WriteLine(" Press return to exit");
Console.ReadLine();
// using (var scope = Container.BeginLifetimeScope())
// {
//     var writer = scope.Resolve<XX>();
//     writer.WriteDate();
// }