using FluentValidation;
using MagicBinder.Application.Commands.Imports;

namespace MagicBinder.Application.Validators.Commands.Imports;

public class ImportCardsFromScryfallFileValidator : AbstractValidator<ImportCardsFromScryfallFile>
{
    public ImportCardsFromScryfallFileValidator()
    {
        RuleFor(x => x.JsonFileContent)
            .NotEmpty();
    }
}