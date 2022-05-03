using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Application.Models.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ProductSubCategory
{
    None,
    [EnumMember(Value = "Replės")]
    Pliers
}