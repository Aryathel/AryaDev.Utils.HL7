using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
[method: SetsRequiredMembers]
public class SegmentInfoAttribute(string code, SegmentChapter chapter, bool optional = true, bool repeatable = true) : Attribute
{
    [Required] public required string Code { get; init; } = code;
    [Required] public required SegmentChapter Chapter { get; init; } = chapter;
    [Required] public required bool Optional { get; init; } = optional;
    [Required] public required bool Repeatable { get; init; } = repeatable;
}