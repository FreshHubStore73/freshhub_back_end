using FluentValidation;
using FreshHub_BE.Data.Entities;
using FreshHub_BE.Models;

namespace FreshHub_BE.Validation
{
    public class UserValidation : AbstractValidator<EditUserModel>
    {
        public UserValidation()
        {
            RuleFor(u => u.PhoneNumber)
               .NotEmpty().WithErrorCode("Empty.")
               .Length(12).WithErrorCode("Length.");           
            RuleFor(u => u.FirstName)
                .NotEmpty().WithErrorCode("Empty")
                .MinimumLength(2).WithErrorCode("Length.")
                .MaximumLength(20).WithErrorCode("Length.");
            RuleFor(u => u.LastName)
                .NotEmpty().WithErrorCode("Empty")
                .MinimumLength(2).WithErrorCode("Length.")
                .MaximumLength(20).WithErrorCode("Length.");
        }
    }
}
