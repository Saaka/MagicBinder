// See https://aka.ms/new-console-template for more information

using Autofac;
using MagicBinder.Application.Commands.Imports;
using MagicBinder.CardsImporter;
using MagicBinder.CardsImporter.CompositionRoots;
using MagicBinder.CardsImporter.CompositionRoots.Extensions;
using MagicBinder.CardsImporter.Services;
using MagicBinder.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

try
{
    var builder = new ContainerBuilder()
        .RegisterAppModules();
    var container = builder.Build();

    await MongoIndexInitializer.CreateIndexes(container.Resolve<IMongoDatabase>());

    await using var scope = container.BeginLifetimeScope()
        .AddHangfire();
    var logger = container.Resolve<ILogger<Program>>();

    logger.LogInformation("Please select import type and press Enter key:");
    var imports = Import.GetImports();
    imports.ForEach(x => logger.LogInformation("Name: {ImportName} Type: '{id}'", x.Name, x.Id));

    var selectedImportId = Console.ReadLine();
    var importType = imports.FirstOrDefault(x => x.Id == selectedImportId);
    if (importType == null)
    {
        logger.LogError("Selected import type does not exists, closing importer...");
        Console.ReadLine();
        return;
    }

    logger.LogInformation("Please provide file name to import (default is '{0}'):", importType.DefaultFileName);
    var fileName = Console.ReadLine();
    fileName = string.IsNullOrWhiteSpace(fileName) ? importType.DefaultFileName : fileName;

    var jsonService = scope.Resolve<JsonService>();
    logger.LogInformation("Reading file '{0}'...", fileName);
    var json = await jsonService.GetJson(fileName);

    var mediator = scope.Resolve<IMediator>();
    await mediator.Send(importType.RequestFactory(json));
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex);
}