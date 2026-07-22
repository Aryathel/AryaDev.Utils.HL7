using System.Text;
using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Serializer;

/// <summary>
/// Parses raw HL7 text or bytes into <see cref="HL7Message"/> instances.
/// </summary>
internal static class HL7MessageReader
{
    /// <summary>
    /// Reads a message from bytes, resolving MSH-18 before decoding the full payload.
    /// </summary>
    /// <param name="raw">Message bytes.</param>
    /// <returns>Parsed message; empty when <paramref name="raw"/> has zero length.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="raw"/> is null.</exception>
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

    /// <summary>
    /// Reads a message from already-decoded text.
    /// </summary>
    /// <param name="raw">Message text.</param>
    /// <returns>Parsed message.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="raw"/> is null or whitespace.</exception>
    internal static HL7Message Read(string raw)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(raw);

        var mshLineEnd = raw.AsSpan().IndexOfAny('\r', '\n');
        var mshLine = mshLineEnd < 0 ? raw : raw[..mshLineEnd];
        var (characterSet, textEncoding) = ResolveCharacterSetFromMshLine(mshLine);

        return Read(raw, characterSet, textEncoding);
    }

    /// <summary>
    /// Parses segment lines and builds an <see cref="HL7Message"/>.
    /// </summary>
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

    /// <summary>
    /// Resolves MSH-18 and the corresponding .NET encoding from the first MSH line.
    /// </summary>
    private static (string? CharacterSet, Encoding TextEncoding) ResolveCharacterSetFromMshLine(string mshLine)
    {
        if (!Hl7CharacterSet.TryGetMshEncodingFromLine(mshLine, out var mshEncoding))
            return (null, Encoding.ASCII);

        var characterSet = Hl7CharacterSet.ExtractMsh18FromLine(mshLine, mshEncoding);
        var textEncoding = Hl7CharacterSet.GetEncoding(characterSet, mshEncoding);
        return (characterSet, textEncoding);
    }

    /// <summary>
    /// Returns the byte length of the first segment line (up to CR/LF, or full buffer).
    /// </summary>
    private static int FindFirstSegmentLength(byte[] raw)
    {
        for (var i = 0; i < raw.Length; i++)
            if (raw[i] is (byte)'\r' or (byte)'\n')
                return i;

        return raw.Length;
    }

    /// <summary>
    /// Parses one segment line into a <see cref="Segment"/>.
    /// </summary>
    /// <remarks>
    /// For MSH, field 1 is the field separator character and subsequent fields are offset by one.
    /// </remarks>
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
            segment.SetFieldFromRaw(i + mod, parts[i], encoding);

        return segment;
    }
}
