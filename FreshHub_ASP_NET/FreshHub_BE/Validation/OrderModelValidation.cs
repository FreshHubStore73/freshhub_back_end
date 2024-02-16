using FluentValidation;
using FreshHub_BE.Models;

namespace FreshHub_BE.Validation
{
    public class OrderModelValidation : AbstractValidator<OrderModel>
    {
        public OrderModelValidation()
        {
            RuleFor(o => o.PhoneNumber)
                .NotEmpty().WithErrorCode("Empty.")
                .Length(12).WithErrorCode("Length.");
            RuleFor(o => o.Flat)
                .NotEmpty().WithErrorCode("Empty.");
            RuleFor(o => o.Floor)
                .NotEmpty().WithErrorCode("Empty.");
            RuleFor(o => o.StreetHouse)
                .NotEmpty().WithErrorCode("Empty.");
            RuleFor(o => o.Payment)
                .NotEmpty().WithErrorCode("Empty.");
            RuleFor(o => o.NumberPerson)
                .GreaterThanOrEqualTo(1).WithErrorCode("Not zero.");
            RuleFor(o => o.Recipient)
                .NotEmpty().WithErrorCode("Empty.")
                .MinimumLength(3).WithErrorCode("Length.");

        }
    }
}
