using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Holiberry.Api.Persistence;

namespace Holiberry.Api.Areas.Public.Requests.P_Account
{
    public class RegisterAPIRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
    public class RegisterAPIRequestValidator : AbstractValidator<RegisterAPIRequest>
    {
        private readonly ApplicationDbContext _db;

        public RegisterAPIRequestValidator(ApplicationDbContext db)
        {
            _db = db;

            RuleFor(a => a.Login)
                .NotNull().WithMessage("Pole \"Email\" nie może być puste")
                .NotEmpty().WithMessage("Pole \"Email\" nie może być puste")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Podany niepoprawny adres email")
                .MustAsync(IsEmailUnique).WithMessage("Podany email jest już w użyciu");

            RuleFor(a => a.Password)
               .NotNull().WithMessage("Pole \"Hasło\" nie może być puste")
               .NotEmpty().WithMessage("Pole \"Hasło\" nie może być puste")
               .MinimumLength(6).WithMessage("Minimalna długość hasła wynosi 6 znaków")
               .Must(a => a.Distinct().ToArray().Length >= 4).WithMessage("Hasło musi posiadać conajmniej 4 unikalne znaki");

            RuleFor(a => a.ConfirmPassword)
                .NotEmpty().WithMessage("Pole \"Potwierdzenie hasła\" nie może być puste")
                .Equal(a => a.Password).WithMessage("Hasła nie są takie same");
        }

        public async Task<bool> IsEmailUnique(RegisterAPIRequest model, string login, CancellationToken cancellationToken)
        {
            if (login == null)
                return true;

            return !await _db.Users.AsNoTracking().AnyAsync(a => a.UserName.ToUpper() == login.ToUpper());
        }
    }
}
