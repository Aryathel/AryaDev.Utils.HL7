using System.Text;
using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Serializer;

internal static class HL7MessageReader
{
    internal static HL7Message Read(byte[] raw)
    {
        ArgumentNullException.ThrowIfNull(raw);

        if (raw.Length == 0)
            return new HL7Message();

        var mshLineLength = FindFirstSegmentLength(raw);
        var mshLine = Encoding.ASCII.GetString(raw, 0, mshLineLength);
        var (characterSet, textEncoding) = ResolveCharacterSetFromMshLine(mshLine);
        var decoded = textEncoding.GetString(raw);

        return Read(decoded, characterSet, textEncoding);
    }

    internal static HL7Message Read(string raw)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(raw);

        var mshLineEnd = raw.AsSpan().IndexOfAny('\r', '\n');
        var mshLine = mshLineEnd < 0 ? raw : raw[..mshLineEnd];
        var (characterSet, textEncoding) = ResolveCharacterSetFromMshLine(mshLine);

        return Read(raw, characterSet, textEncoding);
    }

    private static HL7Message Read(string raw, string? characterSet, Encoding textEncoding)
    {
        var normalized = raw.Replace("\r\n", "\r").Replace('\n', '\r');
        var lines = normalized.Split('\r', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (lines.Length == 0)
            return new HL7Message(characterSet: characterSet, textEncoding: textEncoding);

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

        var message = new HL7Message(segments, encoding, characterSet, textEncoding);
        message.MessageType = MessageTypeExtensions.ParseFromMsh9(
            message["MSH.9.1"],
            message["MSH.9.2"],
            message["MSH.9.3"]);
        return message;
    }

    private static (string? CharacterSet, Encoding TextEncoding) ResolveCharacterSetFromMshLine(string mshLine)
    {
        if (!Hl7CharacterSet.TryGetMshEncodingFromLine(mshLine, out var mshEncoding))
            return (null, Encoding.ASCII);

        var characterSet = Hl7CharacterSet.ExtractMsh18FromLine(mshLine, mshEncoding);
        var textEncoding = Hl7CharacterSet.GetEncoding(characterSet, mshEncoding);
        return (characterSet, textEncoding);
    }

    private static int FindFirstSegmentLength(byte[] raw)
    {
        for (var i = 0; i < raw.Length; i++)
            if (raw[i] is (byte)'\r' or (byte)'\n')
                return i;

        return raw.Length;
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
