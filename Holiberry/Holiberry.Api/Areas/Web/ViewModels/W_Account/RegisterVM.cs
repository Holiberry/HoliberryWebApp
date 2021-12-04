using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Web.ViewModels.W_Account
{
    public class RegisterVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool TermsOfServiceAccepted { get; set; }
        public bool MarketingAccepted { get; set; }
        public bool SendingCommercialsAccepted { get; set; }
    }


    public class RegisterVMValidator : AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            RuleFor(a => a.Email)
                .NotNull().WithMessage("Email nie może być pusty")
                .NotEmpty().WithMessage("Email nie może być pusty");

            RuleFor(a => a.Email)
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Niepoprawny adres email");


            RuleFor(a => a.Password)
                .NotNull().WithMessage("Hasło nie może być puste");

            RuleFor(a => a.Password)
                .NotEmpty().WithMessage("Hasło nie może być puste")
                .MinimumLength(6).WithMessage("Minimalna długość hasła wynosi 6 znaków");


            RuleFor(a => a.ConfirmPassword)
                .NotNull().WithMessage("Pole nie może być puste")
                .NotEmpty().WithMessage("Pole nie może być puste");

            RuleFor(a => a.ConfirmPassword)
                .Equal(a => a.Password).WithMessage("Hasła nie są takie same");
        }
    }
}
