using FluentValidation;
using MagicBinder.Application.Commands.Decks;
using MagicBinder.Domain.Enums;

namespace MagicBinder.Application.Validators.Commands.Decks;

public class CreateDeckValidator: AbstractValidator<CreateDeck>
{
    public CreateDeckValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Format).Must(x => x == FormatType.Commander);

        RuleFor(x => x.GameType).Must(x => x == GameType.Paper);
    }
}