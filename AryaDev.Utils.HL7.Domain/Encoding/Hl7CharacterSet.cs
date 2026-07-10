namespace AryaDev.Utils.HL7.Domain.Encoding;

/// <summary>
/// Maps HL7 Table 0211 character set values (MSH-18) to .NET encodings.
/// </summary>
public static class Hl7CharacterSet
{
    public const string DefaultCharacterSet = "ASCII";

    private static readonly Dictionary<string, System.Text.Encoding> KnownEncodings =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["ASCII"] = System.Text.Encoding.ASCII,
            ["ISO IR 6"] = System.Text.Encoding.ASCII,
            ["8859/1"] = System.Text.Encoding.Latin1,
            ["ISO IR 100"] = System.Text.Encoding.Latin1,
            ["ISO8859-1"] = System.Text.Encoding.Latin1,
            ["ISO8859/1"] = System.Text.Encoding.Latin1,
            ["LATIN-1"] = System.Text.Encoding.Latin1,
            ["UTF-8"] = System.Text.Encoding.UTF8,
            ["ISO IR 192"] = System.Text.Encoding.UTF8,
            ["UTF-16"] = System.Text.Encoding.Unicode,
            ["UNICODE"] = System.Text.Encoding.Unicode,
            ["UNICODE UTF-16"] = System.Text.Encoding.Unicode,
            ["UNICODEUTF-16"] = System.Text.Encoding.Unicode,
        };

    /// <summary>
    /// Reads MSH.1 (field separator) and MSH.2 (encoding characters) from the start of an MSH segment line.
    /// </summary>
    public static bool TryGetMshEncodingFromLine(ReadOnlySpan<char> mshLine, out Hl7EncodingCharacters encoding)
    {
        encoding = Hl7EncodingCharacters.Default;

        if (mshLine.Length < 8 || !mshLine.StartsWith("MSH", StringComparison.Ordinal))
            return false;

        // MSH.1 is the field separator at index 3; MSH.2 follows immediately.
        encoding = Hl7EncodingCharacters.FromMsh2(mshLine[3..].ToString());
        return true;
    }

    private static string? GetPrimaryCharacterSet(string? msh18Value, char componentSeparator = '^')
    {
        if (string.IsNullOrWhiteSpace(msh18Value))
            return null;

        var separatorIndex = msh18Value.IndexOf(componentSeparator);
        var primary = (separatorIndex < 0 ? msh18Value : msh18Value[..separatorIndex]).Trim();
        return string.IsNullOrWhiteSpace(primary) ? null : primary;
    }

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
    /// Locates MSH-18 using the field separator from MSH.1 and encoding characters from MSH.2.
    /// </summary>
    public static string? ExtractMsh18FromLine(ReadOnlySpan<char> mshLine) =>
        !TryGetMshEncodingFromLine(mshLine, out var encoding) ? null : ExtractMsh18FromLine(mshLine, encoding);

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
}
