using System.Text;
using FluentValidation;
using TaskManagerApi.DTOs.Auth;

namespace TaskManagerApi.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(254).WithMessage("Email must not exceed 254 characters.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters.")
            .Must(p => Encoding.UTF8.GetByteCount(p) <= 72)
            .WithMessage("Password must not exceed 72 UTF-8 bytes.");
    }
}
