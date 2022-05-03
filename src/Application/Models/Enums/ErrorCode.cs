using System.Text.Json.Serialization;

namespace Application.Models.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ErrorCode
{
    None = 0,
    InternalError = 1,
    ValidationError = 2,
    ProductNotFound = 3
}