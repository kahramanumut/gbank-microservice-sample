using FluentValidation;

public class AddTransactionValidator:AbstractValidator<AddTransactionDto>
{
    public AddTransactionValidator()
    {
        RuleFor(x => x.Amount)
            .NotEqual(0)
            .WithMessage("Amount must not be zero");

        RuleFor(x => x.AccountId)
            .Length(36)
            .WithMessage("Account Id must be 36 characters");
    }
}