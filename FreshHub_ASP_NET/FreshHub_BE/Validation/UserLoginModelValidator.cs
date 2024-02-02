using FluentValidation;
using FreshHub_BE.Models;

namespace FreshHub_BE.Validation
{
    public class UserLoginModelValidator: AbstractValidator<UserLoginModel>
    {
        public UserLoginModelValidator()
        {
            RuleFor(u=>u.PhoneNumber)
                .NotEmpty().WithErrorCode("Empty.")
                .Length(12).WithErrorCode("Length.");
            RuleFor(u => u.Password)
                .NotEmpty().WithErrorCode("Empty.")
                .MinimumLength(8).WithErrorCode("Length.")
                .MaximumLength(15).WithErrorCode("Length.");
        }
    }
}
