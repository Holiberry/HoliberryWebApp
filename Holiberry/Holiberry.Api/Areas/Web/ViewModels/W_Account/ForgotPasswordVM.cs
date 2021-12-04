using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Web.ViewModels.W_Account
{
    public class ForgotPasswordVM
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordVMValidator : AbstractValidator<ForgotPasswordVM>
    {
        public ForgotPasswordVMValidator()
        {
            RuleFor(a => a.Email)
                .NotNull().WithMessage("Podaj login na jaki utworzone jest konto")
                .NotEmpty().WithMessage("Podaj login na jaki utworzone jest konto");
        }
    }
}
