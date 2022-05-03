using System.Text.Json.Serialization;
using Application.Models.Enums;

namespace Application.Models;

public class ErrorResponse
{
    public List<Error> Errors { get; set; } = new();
}

public class Error
{
    public Error(ErrorCode errorCode, string errorReason) :
        this((int)errorCode, errorReason)
    { }

    [JsonConstructor]
    public Error(int errorCode, string errorReason)
    {
        ErrorCode = errorCode;
        ErrorReason = errorReason;
    }

    public int ErrorCode { get; set; }
    public string ErrorReason { get; set; }
}