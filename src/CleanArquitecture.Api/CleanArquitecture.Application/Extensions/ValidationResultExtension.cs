using CleanArquitecture.Application.Exceptions;
using FluentValidation.Results;

namespace CleanArquitecture.Application.Extensions
{
    public static class ValidationResultExtension
    {
        public static ValidationError[] ToValidationErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.Select(failure => new ValidationError()
            {
                Code = failure.ErrorCode,
                PropertyName = failure.PropertyName,
                Error = failure.ErrorMessage
            }).ToArray();
        }
    }
};

