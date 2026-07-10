using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Model;

namespace AryaDev.Utils.HL7.Domain;

public class HL7Message
{
    private readonly List<Segment> _segments;

    public HL7Message(IEnumerable<Segment>? segments = null, Hl7EncodingCharacters? encoding = null)
    {
        _segments = segments?.ToList() ?? [];
        Encoding = encoding ?? Hl7EncodingCharacters.Default;
    }

    public IReadOnlyList<Segment> Segments => _segments;

    public Hl7EncodingCharacters Encoding { get; set; }

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

    public IEnumerable<Segment> GetSegments(string segmentName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(segmentName);
        var name = segmentName.ToUpperInvariant();
        return _segments.Where(s => string.Equals(s.Name, name, StringComparison.Ordinal));
    }

    internal List<Segment> SegmentsInternal => _segments;

    private Segment? FindSegment(Hl7Path path)
    {
        var matches = GetSegments(path.SegmentName).ToList();
        var index = path.SegmentOccurrence - 1;
        return index >= 0 && index < matches.Count ? matches[index] : null;
    }

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
