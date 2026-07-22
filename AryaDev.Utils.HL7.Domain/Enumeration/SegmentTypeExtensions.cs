using System.ComponentModel;
using System.Reflection;
using AryaDev.Utils.HL7.Domain.Attributes;

namespace AryaDev.Utils.HL7.Domain.Enumeration;

/// <summary>
/// Helpers for <see cref="SegmentType"/> codes, chapters, and descriptions.
/// </summary>
public static class SegmentTypeExtensions
{
    /// <summary>
    /// Lookup of three-character segment codes to <see cref="SegmentType"/>.
    /// </summary>
    private static readonly Dictionary<string, SegmentType> CodeLookup = BuildCodeLookup();

    /// <summary>
    /// Returns the three-character HL7 segment code.
    /// </summary>
    /// <param name="segmentType">Segment type.</param>
    /// <returns>Segment code from <see cref="SegmentInfoAttribute"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="segmentType"/> is not a defined enum field.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the member lacks <see cref="SegmentInfoAttribute"/>.</exception>
    public static string GetCode(this SegmentType segmentType) =>
        GetSegmentInfo(segmentType).Code;

    /// <summary>
    /// Returns the HL7 chapter flag(s) for the segment.
    /// </summary>
    /// <param name="segmentType">Segment type.</param>
    /// <returns>Chapter metadata from <see cref="SegmentInfoAttribute"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="segmentType"/> is not a defined enum field.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the member lacks <see cref="SegmentInfoAttribute"/>.</exception>
    public static SegmentChapter GetChapter(this SegmentType segmentType) =>
        GetSegmentInfo(segmentType).Chapter;

    /// <summary>
    /// Returns the <see cref="DescriptionAttribute"/> text for the segment, if any.
    /// </summary>
    /// <param name="segmentType">Segment type.</param>
    /// <returns>Description text, or <see langword="null"/> when absent.</returns>
    public static string? GetDescription(this SegmentType segmentType)
    {
        var field = typeof(SegmentType).GetField(segmentType.ToString());
        return field?.GetCustomAttribute<DescriptionAttribute>()?.Description;
    }

    /// <summary>
    /// Attempts to resolve a segment type from a three-character code.
    /// </summary>
    /// <param name="code">Segment ID such as <c>PID</c>.</param>
    /// <param name="segmentType">Resolved type when successful.</param>
    /// <returns><see langword="true"/> when the code is recognized (including Z-segments).</returns>
    /// <remarks>
    /// Codes starting with <c>Z</c> map to <see cref="SegmentType.ZSegment"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when <paramref name="code"/> is null or whitespace.</exception>
    public static bool TryParseCode(string code, out SegmentType segmentType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        var normalized = code.ToUpperInvariant();
        if (normalized.Length == 3 && normalized[0] == 'Z')
        {
            segmentType = SegmentType.ZSegment;
            return true;
        }

        return CodeLookup.TryGetValue(normalized, out segmentType);
    }

    /// <summary>
    /// Returns segment types whose chapter flags include <paramref name="chapter"/>.
    /// </summary>
    /// <param name="chapter">Chapter flag to match (supports combined flags).</param>
    /// <returns>Matching segment types.</returns>
    public static IEnumerable<SegmentType> GetByChapter(SegmentChapter chapter) =>
        Enum.GetValues<SegmentType>().Where(st => st.GetChapter().HasFlag(chapter));

    /// <summary>
    /// Reads <see cref="SegmentInfoAttribute"/> for an enum member.
    /// </summary>
    private static SegmentInfoAttribute GetSegmentInfo(SegmentType segmentType)
    {
        var field = typeof(SegmentType).GetField(segmentType.ToString())
            ?? throw new ArgumentException($"Unknown segment type: {segmentType}", nameof(segmentType));

        return field.GetCustomAttribute<SegmentInfoAttribute>()
            ?? throw new InvalidOperationException($"Segment type {segmentType} is missing SegmentInfoAttribute.");
    }

    /// <summary>
    /// Builds the static code → type lookup (excludes <see cref="SegmentType.ZSegment"/>).
    /// </summary>
    private static Dictionary<string, SegmentType> BuildCodeLookup()
    {
        var lookup = new Dictionary<string, SegmentType>(StringComparer.OrdinalIgnoreCase);
        foreach (var segmentType in Enum.GetValues<SegmentType>())
        {
            if (segmentType == SegmentType.ZSegment)
                continue;

            var code = segmentType.GetCode();
            lookup.TryAdd(code, segmentType);
        }

        return lookup;
    }
}
