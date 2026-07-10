using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Domain.Attributes;

[AttributeUsage(AttributeTargets.Field)]
[method: SetsRequiredMembers]
public class SegmentInfoAttribute(string code, SegmentChapter chapter) : Attribute
{
    [Required] public required string Code { get; init; } = code;
    [Required] public required SegmentChapter Chapter { get; init; } = chapter;
}