using FluentValidation;
using MagicBinder.Application.Commands.UserCardsInventory;

namespace MagicBinder.Application.Validators.Commands.UserCardsInventory;

public class SaveCardInventoryValidator : AbstractValidator<SaveCardInventory>
{
    public SaveCardInventoryValidator()
    {
        RuleFor(x => x.OracleId).NotEmpty();
        RuleFor(x => x.Printings).NotEmpty();
        RuleForEach(x => x.Printings).ChildRules(printings =>
        {
            printings.RuleFor(x => x.CardId).NotEmpty();
        });
    }
}