using System.Text.RegularExpressions;

namespace AryaDev.Utils.HL7.Domain.Model;

public sealed partial class Hl7Path
{
    private static readonly Regex PathRegex = PathPattern();

    public string SegmentName { get; }
    public int SegmentOccurrence { get; }
    public int? Field { get; }
    public int Repetition { get; }
    public int? Component { get; }
    public int? Subcomponent { get; }

    public Hl7Path(
        string segmentName,
        int segmentOccurrence = 1,
        int? field = null,
        int repetition = 1,
        int? component = null,
        int? subcomponent = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(segmentName);
        if (segmentName.Length != 3)
        {
            throw new ArgumentException("Segment name must be exactly 3 characters.", nameof(segmentName));
        }

        ArgumentOutOfRangeException.ThrowIfLessThan(segmentOccurrence, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(repetition, 1);

        SegmentName = segmentName.ToUpperInvariant();
        SegmentOccurrence = segmentOccurrence;
        Field = field;
        Repetition = repetition;
        Component = component;
        Subcomponent = subcomponent;
    }

    public static bool TryParse(string path, out Hl7Path? result)
    {
        result = null;
        if (string.IsNullOrWhiteSpace(path))
            return false;

        var match = PathRegex.Match(path.Trim());
        if (!match.Success)
            return false;

        string segmentName;
        int occurrence;
        int? field;
        int repetition;
        int? component;
        int? subcomponent;
        
        try
        {
            segmentName = match.Groups["segment"].Value;
            occurrence = match.Groups["occurrence"].Success
                ? int.Parse(match.Groups["occurrence"].Value)
                : 1;

            field = match.Groups["field"].Success
                ? int.Parse(match.Groups["field"].Value)
                : null;

            repetition = match.Groups["repetition"].Success
                ? int.Parse(match.Groups["repetition"].Value)
                : 1;

            component = match.Groups["component"].Success
                ? int.Parse(match.Groups["component"].Value)
                : null;

            subcomponent = match.Groups["subcomponent"].Success
                ? int.Parse(match.Groups["subcomponent"].Value)
                : null;
        }
        catch (Exception)
        {
            return false;
        }
        

        if (field is < 1 || component is < 1 || subcomponent is < 1 || occurrence < 1 || repetition < 1)
            return false;

        result = new Hl7Path(segmentName, occurrence, field, repetition, component, subcomponent);
        return true;
    }

    public static Hl7Path Parse(string path) =>
        TryParse(path, out var result) ? result! : throw new FormatException($"Invalid HL7 path: '{path}'.");

    [GeneratedRegex(
        @"^(?<segment>[A-Za-z0-9\*]{3})(?:\[(?<occurrence>\d+)\])?(?:\.(?<field>\d+))?(?:~(?<repetition>\d+))?(?:\.(?<component>\d+))?(?:\.(?<subcomponent>\d+))?$",
        RegexOptions.CultureInvariant)]
    private static partial Regex PathPattern();
}
