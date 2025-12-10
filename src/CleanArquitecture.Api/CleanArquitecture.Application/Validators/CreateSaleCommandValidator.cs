using CleanArquitecture.Application.Abstractions.Constants;
using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Application.Commands.Sales;
using FluentValidation;

namespace CleanArquitecture.Application.Validators
{
    public class CreateSaleCommandValidator: AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotNull().WithErrorCode(ErrorCode.Null)
                .NotEmpty().WithErrorCode(ErrorCode.Empty)
                .MinimumLength(10)
                .MaximumLength(150)
                .WithErrorCode(ErrorCode.Invalid);
        }
    }
};

