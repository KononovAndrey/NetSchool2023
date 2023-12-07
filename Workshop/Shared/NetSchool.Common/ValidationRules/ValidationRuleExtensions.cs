namespace NetSchool.Settings;

using FluentValidation;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, string> BookTitle<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(1).WithMessage("Minimum length is 1")
            .MaximumLength(100).WithMessage("Maximum length is 100");
    }
}