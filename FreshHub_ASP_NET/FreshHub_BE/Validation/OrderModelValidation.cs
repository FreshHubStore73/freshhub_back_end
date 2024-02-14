using FluentValidation;
using FreshHub_BE.Models;

namespace FreshHub_BE.Validation
{
    public class OrderModelValidation: AbstractValidator<OrderModel>
    {
        public OrderModelValidation()
        {
            RuleFor(o => o.PhoneNumber)
                .NotEmpty().WithErrorCode("Empty.")
                .Length(12).WithErrorCode("Length.");
            //RuleFor(o => o)
        }
    }
}
