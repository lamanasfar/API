
using API.Entities;
using FluentValidation;
using System.Collections.Generic;
using System.Net;

namespace API.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 50 characters.");



            RuleFor(user => user.LastName)
              .NotEmpty()
              .WithMessage("Lastname is required.")
              .MaximumLength(50)
              .WithMessage("Lastname cannot exceed 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email address format.")
                .MaximumLength(50)
                .WithMessage("Email cannot exceed 100 characters.");
            RuleFor(user => user.PasswordHash)
                .NotEmpty();
            RuleFor(user => user.PasswordSalt)
               .NotEmpty();






        }
    }
}
