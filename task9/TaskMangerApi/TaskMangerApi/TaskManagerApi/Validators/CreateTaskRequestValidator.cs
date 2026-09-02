using FluentValidation;
using System.Text.RegularExpressions;
using TaskManagerApi.DTOs;

namespace TaskManagerApi.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Title is required.")
            .MaximumLength(200)
                .WithMessage("Title must not exceed 200 characters.")
            .Must(title => !Regex.IsMatch(title, @"<[^>]+>"))
                .WithMessage("Title must not contain HTML tags.");

        RuleFor(x => x.DueDate)
            .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
                .WithMessage("Due date must be in the future.");
    }
}