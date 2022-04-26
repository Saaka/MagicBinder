using FluentValidation;
using MagicBinder.Application.Commands.Imports;

namespace MagicBinder.Application.Validators.Commands.Imports;

public class ImportSetsFromScryfallFileValidator : AbstractValidator<ImportSetsFromScryfallFile>
{
    public ImportSetsFromScryfallFileValidator()
    {
        RuleFor(x => x.JsonFileContent)
            .NotEmpty();
    }
}