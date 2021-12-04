using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Web.ViewModels.W_Account
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }


    public class ResetPasswordVMValidator : AbstractValidator<ResetPasswordVM>
    {
        public ResetPasswordVMValidator()
        {
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
