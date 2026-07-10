using System.ComponentModel;
using System.Reflection;
using AryaDev.Utils.HL7.Domain.Attributes;

namespace AryaDev.Utils.HL7.Domain.Enumeration;

public static class SegmentTypeExtensions
{
    private static readonly Dictionary<string, SegmentType> CodeLookup = BuildCodeLookup();

    public static string GetCode(this SegmentType segmentType) =>
        GetSegmentInfo(segmentType).Code;

    public static SegmentChapter GetChapter(this SegmentType segmentType) =>
        GetSegmentInfo(segmentType).Chapter;

    public static bool IsOptional(this SegmentType segmentType) =>
        GetSegmentInfo(segmentType).Optional;

    public static bool IsRepeatable(this SegmentType segmentType) =>
        GetSegmentInfo(segmentType).Repeatable;

    public static string? GetDescription(this SegmentType segmentType)
    {
        var field = typeof(SegmentType).GetField(segmentType.ToString());
        return field?.GetCustomAttribute<DescriptionAttribute>()?.Description;
    }

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

    public static IEnumerable<SegmentType> GetByChapter(SegmentChapter chapter) =>
        Enum.GetValues<SegmentType>().Where(st => st.GetChapter().HasFlag(chapter));

    private static SegmentInfoAttribute GetSegmentInfo(SegmentType segmentType)
    {
        var field = typeof(SegmentType).GetField(segmentType.ToString())
            ?? throw new ArgumentException($"Unknown segment type: {segmentType}", nameof(segmentType));

        return field.GetCustomAttribute<SegmentInfoAttribute>()
            ?? throw new InvalidOperationException($"Segment type {segmentType} is missing SegmentInfoAttribute.");
    }

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
