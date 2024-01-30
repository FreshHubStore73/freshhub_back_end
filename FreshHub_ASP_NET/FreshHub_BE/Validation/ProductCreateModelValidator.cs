using FluentValidation;
using FreshHub_BE.Models;

namespace FreshHub_BE.Validation
{
    public class ProductCreateModelValidator: AbstractValidator<ProductCreateModel>
    {
        public ProductCreateModelValidator()
        {
            RuleFor(p => p.ProductName)
               .NotEmpty().WithErrorCode(ErrorCode.Empty)
               .MaximumLength(50).WithErrorCode(ErrorCode.Length)
               .MinimumLength(4).WithErrorCode(ErrorCode.Length);
            RuleFor(p => p.Price).LessThan(0).WithErrorCode(ErrorCode.Negative);
            RuleFor(p => p.CategoryId).LessThanOrEqualTo(0).WithErrorCode(ErrorCode.Negative);
            RuleFor(p => p.Weight).LessThanOrEqualTo(0).WithErrorCode(ErrorCode.Negative);
            RuleFor(p => p.Description).Length(500).WithErrorCode(ErrorCode.Length);
            
        }
    }
}
