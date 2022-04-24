using FluentValidation;
using MagicBinder.Application.Queries.Cards;

namespace MagicBinder.Application.Validators.Queries.Cards;

public class GetCardDetailsQueryValidator : AbstractValidator<GetCardDetailsQuery>
{
    public GetCardDetailsQueryValidator()
    {
        RuleFor(x => x.OracleId)
            .NotEmpty();
    }
}