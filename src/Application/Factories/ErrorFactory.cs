using Application.Models.Enums;
using Application.Models;

namespace Application.Factories;

public static class ErrorFactory
{
    private static readonly Dictionary<ErrorCode, string> Errors = new()
    {
        { ErrorCode.ProductNotFound, "Product not found" }
    };

    public static Error GetError(ErrorCode errorCode)
    {
        Errors.TryGetValue(errorCode, out var errorString);
        return new Error(errorCode, errorString ?? string.Empty);
    }

    public static Error GetError(ErrorCode errorCode, string reason)
    {
        return new Error(errorCode, reason);
    }
}