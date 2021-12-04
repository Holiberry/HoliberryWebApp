using AutoMapper;
using FluentValidation;
using FluentValidation.Validators;
using Holiberry.Api.Mappings;
using Holiberry.Api.Models.Users.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Admin.ViewModels.A_Users
{
    public class CreateUserVM : IMapFrom<UserM>
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserM, CreateUserVM>();

            profile.CreateMap<CreateUserVM, UserM>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(s => s.Email.ToUpper()))
                .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(s => s.Email.ToUpper()));
        }
    }

    public class CreateUserVMValidator : AbstractValidator<CreateUserVM>
    {
        public CreateUserVMValidator()
        {
            RuleFor(a => a.Email)
                .NotNull().WithMessage("Pole nie może być puste")
                .NotEmpty().WithMessage("Pole nie może być puste");
            RuleFor(a => a.Email)
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Niepoprawny format emaila");

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


            RuleFor(a => a.FirstName)
                .NotNull().WithMessage("Pole nie może być puste")
                .NotEmpty().WithMessage("Pole nie może być puste")
                .MaximumLength(50).WithMessage("Pole może mieć maksymalnie 50 znaków");

            RuleFor(a => a.LastName)
                .NotNull().WithMessage("Pole nie może być puste")
                .NotEmpty().WithMessage("Pole nie może być puste")
                .MaximumLength(50).WithMessage("Pole może mieć maksymalnie 50 znaków");
        }

    }
}
