using API.DTOs.RegisterDtos;
using API.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Net;

namespace API.Validations.UserValidation
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 50 characters.")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("First name can only contain letters.");




            RuleFor(user => user.LastName)
              .NotEmpty()
              .WithMessage("Lastname is required.")
              .MaximumLength(50)
              .WithMessage("Lastname cannot exceed 50 characters.")
              .Matches(@"^[a-zA-Z]+$")
              .WithMessage("Last name can only contain letters.");



            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email address format.")
                .MaximumLength(50)
                .WithMessage("Email cannot exceed 100 characters.");
            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100)
                .WithMessage("Password must be between 8 and 100 characters.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$")
                .WithMessage("Password must be at least 8 characters long and include uppercase, lowercase, number, and special character.");
                









        }
    }
}
