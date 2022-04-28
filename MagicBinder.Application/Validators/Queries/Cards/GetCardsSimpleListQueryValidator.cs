using FluentValidation;
using MagicBinder.Application.Queries.Cards;

namespace MagicBinder.Application.Validators.Queries.Cards;

public class GetCardsSimpleListQueryValidator : AbstractValidator<GetCardsSimpleList>
{
    public GetCardsSimpleListQueryValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0);

        RuleFor(x => x.PageNumber)
            .GreaterThan(0);
    }
}