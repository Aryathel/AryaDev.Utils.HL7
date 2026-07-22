using System.Text.RegularExpressions;

namespace AryaDev.Utils.HL7.Domain.Model;

/// <summary>
/// Parsed HL7 location path (segment, optional occurrence, field, repetition, component, subcomponent).
/// </summary>
/// <remarks>
/// String form examples: <c>PID.5.1</c>, <c>OBX[2].5</c>, <c>PID.3~2.1</c>, <c>DG1[3].5~2.3.1</c>.
/// All numeric parts are 1-based. Segment names are normalized to uppercase.
/// </remarks>
public sealed partial class Hl7Path
{
    private static readonly Regex PathRegex = PathPattern();

    /// <summary>
    /// Three-character segment ID (uppercase).
    /// </summary>
    public string SegmentName { get; }

    /// <summary>
    /// 1-based occurrence among segments of the same name (defaults to 1).
    /// </summary>
    public int SegmentOccurrence { get; }

    /// <summary>
    /// 1-based field number, or <see langword="null"/> when the path is segment-only.
    /// </summary>
    public int? Field { get; }

    /// <summary>
    /// 1-based field repetition, or <see langword="null"/> when <c>~n</c> was omitted.
    /// </summary>
    /// <remarks>
    /// On get, a null repetition at field level means “all repetitions.”
    /// On set, a null repetition is treated as repetition 1.
    /// </remarks>
    public int? Repetition { get; }

    /// <summary>
    /// 1-based component number, or <see langword="null"/> when not specified.
    /// </summary>
    public int? Component { get; }

    /// <summary>
    /// 1-based subcomponent number, or <see langword="null"/> when not specified.
    /// </summary>
    public int? Subcomponent { get; }

    /// <summary>
    /// Creates a path from explicit parts.
    /// </summary>
    /// <param name="segmentName">Three-character segment ID.</param>
    /// <param name="segmentOccurrence">1-based segment occurrence (default 1).</param>
    /// <param name="field">Optional 1-based field number.</param>
    /// <param name="repetition">Optional 1-based repetition; omit for unspecified.</param>
    /// <param name="component">Optional 1-based component number.</param>
    /// <param name="subcomponent">Optional 1-based subcomponent number.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="segmentName"/> is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="segmentOccurrence"/> or <paramref name="repetition"/> is less than 1.
    /// </exception>
    public Hl7Path(
        string segmentName,
        int segmentOccurrence = 1,
        int? field = null,
        int? repetition = null,
        int? component = null,
        int? subcomponent = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(segmentName);
        if (segmentName.Length != 3)
            throw new ArgumentException("Segment name must be exactly 3 characters.", nameof(segmentName));
        
        if (!char.IsLetter(segmentName[0]) || !segmentName.All(char.IsLetterOrDigit))
            throw new ArgumentException("Segment name must start with a letter and contain only letters and digits.", nameof(segmentName));

        ArgumentOutOfRangeException.ThrowIfLessThan(segmentOccurrence, 1);
        if (repetition is not null)
            ArgumentOutOfRangeException.ThrowIfLessThan(repetition.Value, 1);

        SegmentName = segmentName.ToUpperInvariant();
        SegmentOccurrence = segmentOccurrence;
        Field = field;
        Repetition = repetition;
        Component = component;
        Subcomponent = subcomponent;
    }

    /// <summary>
    /// Attempts to parse a path string.
    /// </summary>
    /// <param name="path">Path text to parse.</param>
    /// <param name="result">Parsed path when successful; otherwise <see langword="null"/>.</param>
    /// <returns><see langword="true"/> when parsing succeeds; otherwise <see langword="false"/>.</returns>
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
        int? repetition;
        int? component;
        int? subcomponent;
        
        try
        {
            segmentName = match.Groups["segment"].Value;
            if (!char.IsLetter(segmentName[0]))
                throw new Exception("Invalid segment name: '" + segmentName + "'.");
            
            occurrence = match.Groups["occurrence"].Success
                ? int.Parse(match.Groups["occurrence"].Value)
                : 1;

            field = match.Groups["field"].Success
                ? int.Parse(match.Groups["field"].Value)
                : null;

            repetition = match.Groups["repetition"].Success
                ? int.Parse(match.Groups["repetition"].Value)
                : null;

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
        

        if (field is < 1 || component is < 1 || subcomponent is < 1 || occurrence < 1 || repetition is < 1)
            return false;

        result = new Hl7Path(segmentName, occurrence, field, repetition, component, subcomponent);
        return true;
    }

    /// <summary>
    /// Parses a path string, throwing on failure.
    /// </summary>
    /// <param name="path">Path text to parse.</param>
    /// <returns>The parsed path.</returns>
    /// <exception cref="FormatException">Thrown when <paramref name="path"/> is not a valid HL7 path.</exception>
    public static Hl7Path Parse(string path) =>
        TryParse(path, out var result) ? result! : throw new FormatException($"Invalid HL7 path: '{path}'.");

    /// <summary>
    /// Generated regex for HL7 path syntax.
    /// </summary>
    [GeneratedRegex(
        @"^(?<segment>[A-Za-z0-9\*]{3})(?:\[(?<occurrence>\d+)\])?(?:\.(?<field>\d+))?(?:~(?<repetition>\d+))?(?:\.(?<component>\d+))?(?:\.(?<subcomponent>\d+))?$",
        RegexOptions.CultureInvariant)]
    private static partial Regex PathPattern();
}
