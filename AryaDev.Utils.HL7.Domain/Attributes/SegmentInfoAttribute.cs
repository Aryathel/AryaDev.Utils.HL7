using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Domain.Attributes;

/// <summary>
/// Metadata for a <see cref="SegmentType"/> enum member.
/// </summary>
/// <param name="code">Three-character HL7 segment ID (for example <c>PID</c>).</param>
/// <param name="chapter">HL7 chapter(s) associated with the segment.</param>
[AttributeUsage(AttributeTargets.Field)]
[method: SetsRequiredMembers]
public class SegmentInfoAttribute(string code, SegmentChapter chapter) : Attribute
{
    /// <summary>
    /// Three-character HL7 segment code.
    /// </summary>
    [Required] public required string Code { get; init; } = code;

    /// <summary>
    /// HL7 chapter classification for the segment.
    /// </summary>
    [Required] public required SegmentChapter Chapter { get; init; } = chapter;
}
