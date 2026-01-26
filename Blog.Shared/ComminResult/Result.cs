using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Shared.ComminResult;

public record Result<T>(
    bool Success,
    T? Value,
    string? Error = null,
    IReadOnlyList<string>? Errors = null);

public static class Result
{
    public static Result<T> Ok<T>(T value) => new(true, value);

    public static Result<T> Fail<T>(string error) =>
        new(false, default, error, string.IsNullOrWhiteSpace(error) ? [] : [error]);

    public static Result<T> Fail<T>(IReadOnlyList<string> errors) =>
        new(false, default, errors.FirstOrDefault(), errors);
}