namespace lab02_vaccin.Validators;

public class RegistrationValidator : AbstractValidator<VaccinRegistration>
{
    public RegistrationValidator()
    {
        RuleFor(registration => registration.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(registration => registration.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(registration => registration.VaccinTypeId).NotEmpty().WithMessage("Vaccin Type is required");
        RuleFor(registration => registration.VaccinationLocationId).NotEmpty().WithMessage("Vaccin Locatino is required");
        RuleFor(registration => registration.VaccinationDate).NotEmpty().WithMessage("Vaccination Date is required");
        RuleFor(registration => registration.YearOfBirth).NotEmpty().GreaterThan(1900).LessThan(2023).WithMessage("Year of birth is required and must be between 1900 and the current year");
        RuleFor(registration => registration.EMail).NotEmpty().EmailAddress().WithMessage("Email is required and must be valid");

    }
}
