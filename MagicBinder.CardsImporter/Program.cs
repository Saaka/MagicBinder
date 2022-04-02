// See https://aka.ms/new-console-template for more information

using Autofac;
using MagicBinder.CardsImporter.CompositionRoots;
using MagicBinder.CardsImporter.Services;
using MagicBinder.Infrastructure.Integrations.Scryfall;

const string defaultFileName = "import.json";
try
{
    var builder = new ContainerBuilder()
        .RegisterAppModules();
    var container = builder.Build();

    await using var scope = container.BeginLifetimeScope();
    
    Console.WriteLine("Please provide file name to import (default is '{0}'):", defaultFileName);
    var fileName = Console.ReadLine();
    fileName = string.IsNullOrWhiteSpace(fileName) ? defaultFileName : fileName;
    
    var jsonService = scope.Resolve<CardsJsonService>();
    Console.WriteLine("Reading file '{0}'...", fileName);
    var json = await jsonService.GetJson(fileName);
    
    var parser = scope.Resolve<JsonCardsParser>();
    var cards = parser.ParseCards(json);
    Console.WriteLine("Found {0} cards", cards.Count);
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex);
}