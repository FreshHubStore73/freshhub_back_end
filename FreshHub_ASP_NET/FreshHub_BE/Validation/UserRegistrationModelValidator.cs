using FluentValidation;
using FreshHub_BE.Models;

namespace FreshHub_BE.Validation
{
    public class UserRegistrationModelValidator: AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationModelValidator()
        {
            RuleFor(u => u.PhoneNumber)
               .NotEmpty().WithErrorCode("Empty.")
               .Length(12).WithErrorCode("Length.");
            RuleFor(u => u.Password)
                .NotEmpty().WithErrorCode("Empty.")
                .MinimumLength(4).WithErrorCode("Length.")
                .MaximumLength(8).WithErrorCode("Length.");
            RuleFor(u => u.FirstName)
                .NotEmpty().WithErrorCode("Empty")
                .MinimumLength(2).WithErrorCode("Length.")
                .MinimumLength(20).WithErrorCode("Length.");
            RuleFor(u => u.LastName)
                .NotEmpty().WithErrorCode("Empty")
                .MinimumLength(2).WithErrorCode("Length.")
                .MinimumLength(20).WithErrorCode("Length.");
        }
    }
}
