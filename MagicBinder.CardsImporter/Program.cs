// See https://aka.ms/new-console-template for more information

using Autofac;
using MagicBinder.Application.Commands.Cards;
using MagicBinder.CardsImporter.CompositionRoots;
using MagicBinder.CardsImporter.CompositionRoots.Extensions;
using MagicBinder.CardsImporter.Services;
using MediatR;
using Microsoft.Extensions.Logging;

const string defaultFileName = "import.json";
try
{
    var builder = new ContainerBuilder()
        .RegisterAppModules();
    var container = builder.Build();

    await using var scope = container.BeginLifetimeScope()
        .AddHangfire();

    var logger = container.Resolve<ILogger<Program>>();
    
    logger.LogInformation("Please provide file name to import (default is '{0}'):", defaultFileName);
    var fileName = Console.ReadLine();
    fileName = string.IsNullOrWhiteSpace(fileName) ? defaultFileName : fileName;
    
    var jsonService = scope.Resolve<CardsJsonService>();
    logger.LogInformation("Reading file '{0}'...", fileName);
    var json = await jsonService.GetJson(fileName);

    var mediator = scope.Resolve<IMediator>();
    await mediator.Send(new ImportCardsFromScryfallFile(json));
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex);
}