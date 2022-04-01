// See https://aka.ms/new-console-template for more information

using Autofac;
using MagicBinder.CardsImporter;
using MagicBinder.Domain.Aggregates;
using MagicBinder.Domain.Aggregates.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// IContainer Container = null;
var builder = new ContainerBuilder();
// builder.RegisterType<ConsoleOutput>().As<IOutput>();
// builder.RegisterType<TodayWriter>().As<IDateWriter>();
// Container = builder.Build();

Console.WriteLine("Hello, World!");
var fileName = Console.ReadLine();

var dict = new Dictionary<string, string>()
{
    { nameof(Card.OracleId), "oracle_id" },
    { nameof(Card.Name), "name" },
    { nameof(Card.CardId), "id" },
    { nameof(Card.ImageUris), "image_uris" },
    { nameof(ImageUris.Small), "small" },
    { nameof(ImageUris.Normal), "normal" },
    { nameof(ImageUris.Large), "large" },
};

Console.WriteLine(fileName);
using var streamReader = File.OpenText(fileName);
var jsonSerializerSettings = new JsonSerializerSettings
{
    ContractResolver = new CustomJsonContractResolver(dict)
};

var result = JsonConvert.DeserializeObject<List<Card>>(streamReader.ReadToEnd(), jsonSerializerSettings);

var card = result.FirstOrDefault();
Console.WriteLine(" Press return to exit");
Console.ReadLine();
// using (var scope = Container.BeginLifetimeScope())
// {
//     var writer = scope.Resolve<XX>();
//     writer.WriteDate();
// }