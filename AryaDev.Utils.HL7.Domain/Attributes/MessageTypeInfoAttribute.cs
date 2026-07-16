using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AryaDev.Utils.HL7.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
[method: SetsRequiredMembers]
public class MessageTypeInfoAttribute(string code) : Attribute
{
    [Required] public required string Code { get; init; } = code;
}
