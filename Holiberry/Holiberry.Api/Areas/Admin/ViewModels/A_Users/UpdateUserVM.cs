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
    public class UpdateUserVM : IMapFrom<UserM>
    {

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }


        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }


        public string FullName { get; set; }
        public string WMSNumber { get; set; }



        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserM, UpdateUserVM>();

            profile.CreateMap<UpdateUserVM, UserM>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(s => s.Email.ToUpper()))
                .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(s => s.Email.ToUpper()));
        }
    }


    public class UpdateUserVMValidator : AbstractValidator<UpdateUserVM>
    {
        public UpdateUserVMValidator()
        {
            RuleFor(a => a.Email)
                .NotNull().WithMessage("Pole nie może być puste")
                .NotEmpty().WithMessage("Pole nie może być puste");
            RuleFor(a => a.Email)
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Niepoprawny format emaila");


            RuleFor(a => a.PhoneNumber)
                .Must(a => !string.IsNullOrWhiteSpace(a))
                    .When(a => a.PhoneNumberConfirmed).WithMessage("Numer telefonu nie może być pusty jeśli jest potwierdzony");

            RuleFor(a => a.FullName)
                .NotNull().WithMessage("Pole nie może być puste")
                .NotEmpty().WithMessage("Pole nie może być puste")
                .MaximumLength(50).WithMessage("Pole może mieć maksymalnie 50 znaków");

            RuleFor(a => a.WMSNumber)
                .NotNull().WithMessage("Pole nie może być puste")
                .NotEmpty().WithMessage("Pole nie może być puste")
                .MaximumLength(10).WithMessage("Pole może mieć maksymalnie 10 znaków");

            RuleFor(a => a.WMSNumber)
                .Must(a => a.All(b => Char.IsNumber(b) || (Char.IsUpper(b) && Char.IsLetter(b)))).WithMessage("Numer WMS musi składać się z samych wielkich liter oraz cyfr");

        }
    }
}
