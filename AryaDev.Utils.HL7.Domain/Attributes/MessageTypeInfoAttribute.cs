using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AryaDev.Utils.HL7.Domain.Attributes;

/// <summary>
/// Metadata for a <see cref="Enumeration.MessageType"/> enum member.
/// </summary>
/// <param name="code">HL7 Table 0354 code (for example <c>ADT_A01</c>).</param>
[AttributeUsage(AttributeTargets.Field)]
[method: SetsRequiredMembers]
public class MessageTypeInfoAttribute(string code) : Attribute
{
    /// <summary>
    /// HL7 message structure / Table 0354 code.
    /// </summary>
    [Required] public required string Code { get; init; } = code;
}
