using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Application.Models.Enums;


[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum Brand
{
    None,
    [EnumMember(Value = "C.K. Tools")]
    CkTools,
}