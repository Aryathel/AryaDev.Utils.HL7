namespace AryaDev.Utils.HL7.Domain.Encoding;

/// <summary>
/// Maps HL7 Table 0211 character-set values (MSH-18) to .NET encodings.
/// </summary>
public static class Hl7CharacterSet
{
    /// <summary>
    /// Default character-set name when MSH-18 is absent.
    /// </summary>
    public const string DefaultCharacterSet = "ASCII";

    /// <summary>
    /// Known HL7 Table 0211 aliases and their .NET encodings.
    /// </summary>
    private static readonly Dictionary<string, System.Text.Encoding> KnownEncodings =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["ASCII"] = System.Text.Encoding.ASCII,
            ["ISO IR 6"] = System.Text.Encoding.ASCII,
            ["LATIN-1"] = System.Text.Encoding.Latin1,
            ["8859/1"] = System.Text.Encoding.Latin1,
            ["ISO IR 100"] = System.Text.Encoding.Latin1,
            ["ISO8859-1"] = System.Text.Encoding.Latin1,
            ["ISO8859/1"] = System.Text.Encoding.Latin1,
            ["UTF-8"] = System.Text.Encoding.UTF8,
            ["ISO IR 192"] = System.Text.Encoding.UTF8,
            ["UTF-16"] = System.Text.Encoding.Unicode,
            ["UNICODE"] = System.Text.Encoding.Unicode,
            ["UNICODE UTF-16"] = System.Text.Encoding.Unicode,
            ["UNICODEUTF-16"] = System.Text.Encoding.Unicode,
        };

    /// <summary>
    /// Reads delimiter characters from the start of an MSH segment line.
    /// </summary>
    /// <param name="mshLine">First segment line beginning with <c>MSH</c>.</param>
    /// <param name="encoding">Parsed encoding characters when successful.</param>
    /// <returns><see langword="true"/> when the line looks like a valid MSH header.</returns>
    /// <remarks>
    /// Uses the character at index 3 as the field separator and the following characters as MSH-2.
    /// </remarks>
    public static bool TryGetMshEncodingFromLine(ReadOnlySpan<char> mshLine, out Hl7EncodingCharacters encoding)
    {
        encoding = Hl7EncodingCharacters.Default;

        if (mshLine.Length < 8 || !mshLine.StartsWith("MSH", StringComparison.Ordinal))
            return false;

        encoding = Hl7EncodingCharacters.FromMsh2(mshLine[3..].ToString());
        return true;
    }

    /// <summary>
    /// Returns the primary (first) character-set token from an MSH-18 value.
    /// </summary>
    private static string? GetPrimaryCharacterSet(string? msh18Value, char componentSeparator = '^')
    {
        if (string.IsNullOrWhiteSpace(msh18Value))
            return null;

        var separatorIndex = msh18Value.IndexOf(componentSeparator);
        var primary = (separatorIndex < 0 ? msh18Value : msh18Value[..separatorIndex]).Trim();
        return string.IsNullOrWhiteSpace(primary) ? null : primary;
    }

    /// <summary>
    /// Resolves a .NET encoding from an MSH-18 value.
    /// </summary>
    /// <param name="msh18Value">Raw MSH-18 text (may list multiple sets separated by the component separator).</param>
    /// <param name="encoding">Optional delimiter set; component separator selects the primary token.</param>
    /// <returns>
    /// Matching encoding from the known map, <see cref="System.Text.Encoding.GetEncoding(string)"/>,
    /// or ASCII when absent/unrecognized.
    /// </returns>
    public static System.Text.Encoding GetEncoding(string? msh18Value, Hl7EncodingCharacters? encoding = null)
    {
        var primary = GetPrimaryCharacterSet(msh18Value, encoding?.ComponentSeparator ?? '^');
        if (primary is null)
            return System.Text.Encoding.ASCII;

        if (KnownEncodings.TryGetValue(primary, out var knownEncoding))
            return knownEncoding;

        try
        {
            return System.Text.Encoding.GetEncoding(primary);
        }
        catch (ArgumentException)
        {
            return System.Text.Encoding.ASCII;
        }
    }

    /// <summary>
    /// Extracts MSH-18 from an MSH line using delimiters parsed from that line.
    /// </summary>
    /// <param name="mshLine">MSH segment line.</param>
    /// <returns>MSH-18 value, or <see langword="null"/> when unavailable.</returns>
    public static string? ExtractMsh18FromLine(ReadOnlySpan<char> mshLine) =>
        !TryGetMshEncodingFromLine(mshLine, out var encoding) ? null : ExtractMsh18FromLine(mshLine, encoding);

    /// <summary>
    /// Extracts MSH-18 from an MSH line using the provided field separator.
    /// </summary>
    /// <param name="mshLine">MSH segment line.</param>
    /// <param name="encoding">Delimiter set (field separator must match the line).</param>
    /// <returns>MSH-18 value, or <see langword="null"/> when the field is missing/empty.</returns>
    public static string? ExtractMsh18FromLine(ReadOnlySpan<char> mshLine, Hl7EncodingCharacters encoding)
    {
        if (mshLine.Length < 8 || !mshLine.StartsWith("MSH", StringComparison.Ordinal))
            return null;

        var fieldSeparator = encoding.FieldSeparator;
        if (mshLine[3] != fieldSeparator)
            return null;

        // HL7 field 1 is the separator after MSH; field 18 is the 17th value after MSH{MSH.1}.
        const int msh18FieldIndex = 17;
        var fieldIndex = 0;
        var start = 4;

        for (var i = 4; i <= mshLine.Length; i++)
            if (i == mshLine.Length || mshLine[i] == fieldSeparator)
            {
                fieldIndex++;
                if (fieldIndex == msh18FieldIndex)
                {
                    var value = mshLine[start..i];
                    return value.IsEmpty ? null : value.ToString();
                }

                start = i + 1;
            }

        return null;
    }

    /// <summary>
    /// Maps a .NET encoding to a preferred HL7 Table 0211 name, when known.
    /// </summary>
    /// <param name="encoding">.NET encoding instance.</param>
    /// <returns>Matching Table 0211 alias, or <see langword="null"/> when not in the known map.</returns>
    public static string? ToMsh18Value(this System.Text.Encoding encoding)
    {
        foreach (var (k, v) in KnownEncodings)
            if (encoding.Equals(v))
                return k;

        return null;
    }
}
