using FluentValidation;
using MagicBinder.Domain.Enums;

namespace MagicBinder.Application.Validators;

public static class Extensions
{
    public static IRuleBuilderOptions<T, TProperty> WithValidationError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ValidationError error)
    {
        rule.WithMessage(error.ToString());
        return rule;
    }
}