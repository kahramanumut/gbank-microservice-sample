using FluentValidation;

public class AddAccountValidator:AbstractValidator<AddAccountDto>
{
    public AddAccountValidator()
    {
        RuleFor(x => x.CustomerId)
            .Length(36)
            .WithMessage("Customer Id must be 36 characters");
    }
}