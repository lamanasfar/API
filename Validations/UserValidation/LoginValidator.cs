﻿using API.DTOs.RegisterDtos;
using FluentValidation;

namespace API.Validations.UserValidation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {

            RuleFor(user => user.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email address format.")
                .MaximumLength(50)
                .WithMessage("Email cannot exceed 100 characters.");
            RuleFor(user => user.Password)
                .NotEmpty()
                .WithMessage("Password is required.");

        }

       
    }
}
