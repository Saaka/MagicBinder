using FluentValidation;
using MagicBinder.Application.Commands.Decks;
using MagicBinder.Domain.Constants;
using MagicBinder.Domain.Enums;

namespace MagicBinder.Application.Validators.Commands.Decks;

public class CreateDeckValidator : AbstractValidator<CreateDeck>
{
    public CreateDeckValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithValidationError(ValidationError.DeckNameRequired);
        
        RuleFor(x => x.Name)
            .MaximumLength(DecksConst.NameMaxLength)
            .WithValidationError(ValidationError.DeckNameTooLong);

        RuleFor(x => x.Format)
            .Must(x => x == FormatType.Commander)
            .WithValidationError(ValidationError.FormatTypeNotSupported);

        RuleFor(x => x.GameType)
            .Must(x => x == GameType.Paper)
            .WithValidationError(ValidationError.GameTypeNotSupported);
    }
}