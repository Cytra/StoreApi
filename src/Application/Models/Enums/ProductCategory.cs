using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Application.Models.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ProductCategory
{
    None,
    [EnumMember(Value = "Įrankiai")]
    Tools
}