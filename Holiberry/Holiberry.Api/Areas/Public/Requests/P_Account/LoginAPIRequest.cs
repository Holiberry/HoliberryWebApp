using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Areas.Public.Requests.P_Account
{
    public class LoginAPIRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginAPIRequestValidator : AbstractValidator<LoginAPIRequest>
    {
        public LoginAPIRequestValidator()
        {
            RuleFor(a => a.Login)
                .NotNull()
                .NotEmpty().WithMessage("Nazwa użytkownika jest wymagana");

            RuleFor(a => a.Password)
                .NotNull()
                .NotEmpty().WithMessage("Proszę podać hasło");
        }
    }
}
