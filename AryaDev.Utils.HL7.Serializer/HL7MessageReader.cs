using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Domain.Encoding;

namespace AryaDev.Utils.HL7.Serializer;

internal static class HL7MessageReader
{
    internal static HL7Message Read(string raw)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(raw);

        var normalized = raw.Replace("\r\n", "\r").Replace('\n', '\r');
        var lines = normalized.Split('\r', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (lines.Length == 0)
            return new HL7Message();

        var encoding = Hl7EncodingCharacters.Default;
        var segments = new List<Segment>();

        foreach (var line in lines)
        {
            if (line.Length < 4)
                continue;

            var segmentName = line[..3].ToUpperInvariant();
            if (segmentName == "MSH")
                encoding = Hl7EncodingCharacters.FromMsh2(line[3..]);
            
            var segment = ParseSegmentLine(line, segmentName, encoding);
            segments.Add(segment);
        }

        return new HL7Message(segments, encoding);
    }

    private static Segment ParseSegmentLine(string line, string segmentName, Hl7EncodingCharacters encoding)
    {
        var separator = encoding.FieldSeparator;
        var parts = line.Split(separator);

        var segment = new Segment(segmentName);

        var mod = 0;

        if (string.Equals(segmentName, "MSH", StringComparison.Ordinal))
        {
            segment.SetFieldFromRaw(1, separator.ToString(), encoding);
            mod = 1;
        }

        for (var i = 1; i < parts.Length; i++)
        {
            var decoded = Hl7Escape.Decode(parts[i], encoding);
            segment.SetFieldFromRaw(i + mod, decoded, encoding);
        }

        return segment;
    }
}
