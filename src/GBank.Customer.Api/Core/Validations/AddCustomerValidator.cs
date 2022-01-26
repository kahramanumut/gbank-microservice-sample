using FluentValidation;

public class AddCustomerValidator:AbstractValidator<AddCustomerDto>
{
    public AddCustomerValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .WithMessage("Name must be greater than one character")
            .MaximumLength(40)
            .WithMessage("Name must be less than 40 character");

        RuleFor(x => x.Surname)
            .MinimumLength(1)
            .WithMessage("Surname must be greater than one character")
            .MaximumLength(60)
            .WithMessage("Surname must be less than 60 character");

            RuleFor(x => x.Age)
            .GreaterThan(18)
            .WithMessage("Age must be greater than 18");
    }
}