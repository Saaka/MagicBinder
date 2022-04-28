using FluentValidation;
using MagicBinder.Application.Commands.Inventories;

namespace MagicBinder.Application.Validators.Commands.Inventories;

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