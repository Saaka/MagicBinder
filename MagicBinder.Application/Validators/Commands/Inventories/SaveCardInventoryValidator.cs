using FluentValidation;
using MagicBinder.Application.Commands.Inventories;

namespace MagicBinder.Application.Validators.Commands.Inventories;

public class SaveCardInventoryValidator : AbstractValidator<SaveCardInventory>
{
    public SaveCardInventoryValidator()
    {
        RuleFor(x => x.OracleId).NotEmpty();
    }
}