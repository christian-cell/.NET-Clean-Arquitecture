using CleanArquitecture.Application.Abstractions.Constants;
using CleanArquitecture.Application.Commands.Auth.Register;
using CleanArquitecture.Application.Exceptions;
using CleanArquitecture.Application.Services.Auth;
using CleanArquitecture.Domain.Interfaces;
using FluentValidation;

namespace CleanArquitecture.Application.Validators
{
    public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;
        
        
        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;
            
            RuleFor(x => x.FirstName)
                .NotNull().WithErrorCode(ErrorCode.Null)
                .NotEmpty().WithErrorCode(ErrorCode.Empty)
                .MinimumLength(3)
                .MaximumLength(30)
                .WithErrorCode(ErrorCode.Invalid);
        
            RuleFor(x => x.LastName)
                .NotNull().WithErrorCode(ErrorCode.Null)
                .NotEmpty().WithErrorCode(ErrorCode.Empty)
                .MinimumLength(3)
                .MaximumLength(30)
                .WithErrorCode(ErrorCode.Invalid);
            
            RuleFor(x => x.DocumentNumber)
                .NotNull().WithErrorCode(ErrorCode.Null)
                .NotEmpty().WithErrorCode(ErrorCode.Empty)
                .MinimumLength(6)
                .MaximumLength(12)
                .WithErrorCode(ErrorCode.Invalid);
            
            RuleFor(x => x.Email)
                .NotNull().WithErrorCode(ErrorCode.Null)
                .NotEmpty().WithErrorCode(ErrorCode.Empty)
                .EmailAddress()
                .MinimumLength(12)
                .MaximumLength(60)
                .WithErrorCode(ErrorCode.Invalid);

            RuleFor(x => x.Email)
                .MustAsync(CheckIfUserExistsByEmail)
                .WithErrorCode(ErrorCode.Invalid);
            
            RuleFor(x => x.Phone)
                .NotNull().WithErrorCode(ErrorCode.Null)
                .NotEmpty().WithErrorCode(ErrorCode.Empty)
                .MinimumLength(9)
                .MaximumLength(14)
                .WithErrorCode(ErrorCode.Invalid);
            
            RuleFor(x => x.Password)
                .NotNull().WithErrorCode(ErrorCode.Null)
                .NotEmpty().WithErrorCode(ErrorCode.Empty)
                .MinimumLength(3)
                .MaximumLength(100)
                .WithErrorCode(ErrorCode.Invalid);

            RuleFor(x => x.PasswordConfirm)
                .Equal(x => x.Password)
                .WithErrorCode(ErrorCode.Invalid);
        }

        private async Task<bool> CheckIfUserExistsByEmail(string email, CancellationToken cancellation)
        {
            var user = await _userService.SearchUserByEmail(email);

            if (user is not null) throw new AlreadyExistsException("User", "Email", email);
            
            return true;
        }
    }
};

