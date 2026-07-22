using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;
using AryaDev.Utils.HL7.Domain.Model;

namespace AryaDev.Utils.HL7.Domain;

/// <summary>
/// Structured representation of an HL7 v2 message.
/// </summary>
/// <remarks>
/// Field access uses HL7 path notation through the indexer (for example <c>PID.5.1</c>).
/// This type stores values; it does not validate against HL7 message profiles.
/// </remarks>
public class HL7Message
{
    /// <summary>
    /// Backing list of segments in message order.
    /// </summary>
    private readonly List<Segment> _segments;

    /// <summary>
    /// Creates a message, optionally seeding segments and encoding metadata.
    /// </summary>
    /// <param name="segments">Initial segments; <see langword="null"/> yields an empty message.</param>
    /// <param name="encoding">Delimiter set; defaults to <see cref="Hl7EncodingCharacters.Default"/>.</param>
    /// <param name="characterSet">Raw MSH-18 value, if known.</param>
    /// <param name="textEncoding">
    /// .NET encoding for byte conversion; when omitted, resolved from <paramref name="characterSet"/>.
    /// </param>
    public HL7Message(
        IEnumerable<Segment>? segments = null,
        Hl7EncodingCharacters? encoding = null,
        string? characterSet = null,
        System.Text.Encoding? textEncoding = null)
    {
        _segments = segments?.ToList() ?? [];
        Encoding = encoding ?? Hl7EncodingCharacters.Default;
        CharacterSet = characterSet;
        TextEncoding = textEncoding ?? Hl7CharacterSet.GetEncoding(characterSet);
    }

    /// <summary>
    /// Segments in message order.
    /// </summary>
    public IReadOnlyList<Segment> Segments => _segments;

    /// <summary>
    /// Delimiter characters used when reading and writing field structure.
    /// </summary>
    public Hl7EncodingCharacters Encoding { get; set; }

    /// <summary>
    /// Raw MSH-18 character-set identifier.
    /// </summary>
    /// <remarks>
    /// When null or empty, text operations default to ASCII via <see cref="TextEncoding"/>.
    /// </remarks>
    public string? CharacterSet { get; set; }

    /// <summary>
    /// .NET encoding used for byte serialize/deserialize.
    /// </summary>
    /// <remarks>
    /// Typically derived from <see cref="CharacterSet"/> (HL7 Table 0211).
    /// </remarks>
    public System.Text.Encoding TextEncoding { get; set; }

    /// <summary>
    /// Typed message type from HL7 Table 0354.
    /// </summary>
    /// <remarks>
    /// Filled from MSH-9 on deserialize. On serialize, written to MSH-9 only when that field is empty.
    /// The raw MSH-9 string remains available via the indexer (for example <c>message["MSH.9"]</c>).
    /// </remarks>
    public MessageType MessageType { get; set; } = MessageType.Unknown;

    /// <summary>
    /// Gets or sets a value using HL7 path notation.
    /// </summary>
    /// <param name="path">Location such as <c>PID.5.1</c> or <c>OBX[2].5</c>.</param>
    /// <returns>The value at the path, or <see langword="null"/> when missing.</returns>
    /// <remarks>
    /// Getter delegates to <see cref="Segment.GetValue(Hl7Path, Hl7EncodingCharacters)"/>.
    /// Setter parses separators via <see cref="Segment.SetValue(Hl7Path, string?, Hl7EncodingCharacters)"/>
    /// and creates missing segments as needed. Prefer <see cref="SetRaw"/> for literal text containing delimiters.
    /// </remarks>
    /// <exception cref="FormatException">Thrown when <paramref name="path"/> is invalid.</exception>
    public string? this[string path]
    {
        get
        {
            var hl7Path = Hl7Path.Parse(path);
            var segment = FindSegment(hl7Path);
            return segment?.GetValue(hl7Path, Encoding);
        }
        set
        {
            var hl7Path = Hl7Path.Parse(path);
            var segment = FindOrCreateSegment(hl7Path);
            segment.SetValue(hl7Path, value, Encoding);
        }
    }

    /// <inheritdoc cref="SetRaw(Hl7Path, string)"/>
    /// <exception cref="FormatException">Thrown when <paramref name="path"/> is invalid.</exception>
    public void SetRaw(string path, string value) =>
        SetRaw(Hl7Path.Parse(path), value);

    /// <summary>
    /// Sets a literal value without parsing HL7 separator characters.
    /// </summary>
    /// <param name="path">HL7 location path.</param>
    /// <param name="value">Literal text to store.</param>
    /// <remarks>
    /// Unlike the indexer setter, characters such as <c>^</c> and <c>|</c> are kept as data.
    /// Missing segments are created as needed.
    /// </remarks>
    public void SetRaw(Hl7Path path, string value)
    {
        var segment = FindOrCreateSegment(path);
        segment.SetValueRaw(path, value);
    }

    /// <summary>
    /// Returns all segments with the given three-character name.
    /// </summary>
    /// <param name="segmentName">Segment ID such as <c>MSH</c> or <c>PID</c>.</param>
    /// <returns>Matching segments in message order.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="segmentName"/> is null or whitespace.</exception>
    public IEnumerable<Segment> GetSegments(string segmentName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(segmentName);
        var name = segmentName.ToUpperInvariant();
        return _segments.Where(s => string.Equals(s.Name, name, StringComparison.Ordinal));
    }
    
    /// <summary>
    /// Returns all segments of the given typed segment kind.
    /// </summary>
    /// <param name="segmentType">Segment type to match.</param>
    /// <returns>Matching segments in message order.</returns>
    public IEnumerable<Segment> GetSegments(SegmentType segmentType) =>
        _segments.Where(s => s.Type == segmentType);

    /// <summary>
    /// Mutable segment list used by the serializer.
    /// </summary>
    internal List<Segment> SegmentsInternal => _segments;

    /// <summary>
    /// Finds the segment for a path occurrence, if present.
    /// </summary>
    /// <param name="path">Path whose segment name and occurrence are used.</param>
    /// <returns>The matching segment, or <see langword="null"/> if absent.</returns>
    private Segment? FindSegment(Hl7Path path)
    {
        var matches = GetSegments(path.SegmentName).ToList();
        var index = path.SegmentOccurrence - 1;
        return index >= 0 && index < matches.Count ? matches[index] : null;
    }

    /// <summary>
    /// Finds the segment for a path occurrence, creating preceding occurrences if needed.
    /// </summary>
    /// <param name="path">Path whose segment name and occurrence are used.</param>
    /// <returns>The existing or newly created segment.</returns>
    private Segment FindOrCreateSegment(Hl7Path path)
    {
        var segment = FindSegment(path);
        if (segment is not null)
            return segment;

        var matches = GetSegments(path.SegmentName).ToList();
        while (matches.Count < path.SegmentOccurrence)
        {
            var newSegment = new Segment(path.SegmentName);
            _segments.Add(newSegment);
            matches.Add(newSegment);
        }

        return matches[path.SegmentOccurrence - 1];
    }
}
