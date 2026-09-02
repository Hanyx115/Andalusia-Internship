using System;
using System.Collections.Generic;
using TaskManagerApi.DTOs;
using TaskManagerApi.Validators;
using Xunit;

namespace TaskManagerApi.Tests;

public class TaskValidatorTests
{
    public static IEnumerable<object?[]> ValidationCases()
    {
        // Invalid titles.
        yield return new object?[]
        {
            null, null, "Title", "Title is required."
        };

        yield return new object?[]
        {
            "", null, "Title", "Title is required."
        };

        yield return new object?[]
        {
            "   ", null, "Title", "Title is required."
        };

        yield return new object?[]
        {
            new string('A', 201), null, "Title",
            "Title must not exceed 200 characters."
        };

        yield return new object?[]
        {
            "<script>alert(1)</script>", null, "Title",
            "Title must not contain HTML tags."
        };

        // Invalid due date: yesterday.
        yield return new object?[]
        {
            "Team meeting", -1, "DueDate",
            "Due date must be in the future."
        };

        // Valid request without a due date.
        yield return new object?[]
        {
            "Team meeting", null, null, null
        };

        // Valid request with tomorrow's due date.
        yield return new object?[]
        {
            "Team meeting", 1, null, null
        };

        // Valid title at the maximum length.
        yield return new object?[]
        {
            new string('A', 200), 1, null, null
        };
    }

    [Theory]
    [MemberData(nameof(ValidationCases))]
    public void CreateValidator_ReturnsExpectedResult(
        string? title,
        int? daysFromToday,
        string? expectedProperty,
        string? expectedMessage)
    {
        var validator = new CreateTaskRequestValidator();

        var request = new CreateTaskRequest
        {
            Title = title!,
            DueDate = daysFromToday.HasValue
                ? DateTime.UtcNow.AddDays(daysFromToday.Value)
                : null
        };

        var result = validator.Validate(request);

        if (expectedMessage is null)
        {
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
        else
        {
            Assert.False(result.IsValid);

            var error = Assert.Single(result.Errors);

            Assert.Equal(expectedProperty, error.PropertyName);
            Assert.Equal(expectedMessage, error.ErrorMessage);
        }
    }

    [Theory]
    [MemberData(nameof(ValidationCases))]
    public void UpdateValidator_ReturnsExpectedResult(
        string? title,
        int? daysFromToday,
        string? expectedProperty,
        string? expectedMessage)
    {
        var validator = new UpdateTaskRequestValidator();

        var request = new UpdateTaskRequest
        {
            Title = title!,
            IsCompleted = true,
            DueDate = daysFromToday.HasValue
                ? DateTime.UtcNow.AddDays(daysFromToday.Value)
                : null
        };

        var result = validator.Validate(request);

        if (expectedMessage is null)
        {
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
        else
        {
            Assert.False(result.IsValid);

            var error = Assert.Single(result.Errors);

            Assert.Equal(expectedProperty, error.PropertyName);
            Assert.Equal(expectedMessage, error.ErrorMessage);
        }
    }
}