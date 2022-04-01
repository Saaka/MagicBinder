// See https://aka.ms/new-console-template for more information

using Autofac;
using MagicBinder.CardsImporter.CompositionRoots;
using MagicBinder.CardsImporter.Services;
using MagicBinder.Infrastructure.Integrations.Scryfall;

try
{
    var builder = new ContainerBuilder()
        .RegisterAppModules();
    var container = builder.Build();

    await using var scope = container.BeginLifetimeScope();
    var jsonService = scope.Resolve<CardsJsonService>();
    var fileName = Console.ReadLine();
    var json = await jsonService.GetJson(fileName ?? "import.json");

    var parser = scope.Resolve<JsonCardsParser>();
    var cards = parser.ParseCards(json);
    Console.WriteLine("Found {0} cards", cards.Count);
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex);
}