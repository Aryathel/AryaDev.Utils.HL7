using System.Text;

namespace AryaDev.Utils.HL7.Domain.Encoding;

/// <summary>
/// Encodes and decodes HL7 escape sequences within field data.
/// </summary>
/// <remarks>
/// Supports standard escapes (<c>\F\</c>, <c>\S\</c>, <c>\T\</c>, <c>\R\</c>, <c>\E\</c>)
/// plus hex forms (<c>\Xhhhh\</c>, multi-codepoint <c>\Z...\</c>).
/// </remarks>
public static class Hl7Escape
{
    /// <inheritdoc cref="Decode(string, Hl7EncodingCharacters)"/>
    public static string Decode(this Hl7EncodingCharacters encoding, string value) => Decode(value, encoding);
    
    /// <summary>
    /// Decodes HL7 escape sequences into their literal characters.
    /// </summary>
    /// <param name="value">Possibly escaped text.</param>
    /// <param name="encoding">Delimiter set that defines separator/escape characters.</param>
    /// <returns>Decoded text; returns <paramref name="value"/> unchanged when no escapes are present.</returns>
    public static string Decode(string value, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        var escape = encoding.EscapeCharacter;
        if (!value.Contains(escape))
            return value;

        var result = new StringBuilder(value.Length);
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] != escape || i + 1 >= value.Length)
            {
                result.Append(value[i]);
                continue;
            }

            var endEscape = value.IndexOf(escape, i+1);
            if (endEscape == -1)
            {
                result.Append(value[i..]);
                break;
            }
            var encoded = value.Substring(i+1, endEscape - i - 1);

            if (encoded.Length == 0 || encoded.Any(char.IsWhiteSpace))
            {
                result.Append(value[i]);
                continue;
            }
            
            switch (encoded[0])
            {
                case 'F':
                    result.Append(encoding.FieldSeparator);
                    break;
                case 'S':
                    result.Append(encoding.ComponentSeparator);
                    break;
                case 'T':
                    result.Append(encoding.SubcomponentSeparator);
                    break;
                case 'R':
                    result.Append(encoding.RepetitionSeparator);
                    break;
                case 'E':
                    result.Append(encoding.EscapeCharacter);
                    break;
                case 'X':
                    TryAppendHexEscape(encoded, result);
                    break;
                case 'Z':
                    TryAppendMultiHexEscape(encoded, result);
                    break;
                default:
                    result.Append(encoded);
                    break;
            }
            i = endEscape;
        }

        return result.ToString();
    }

    
    /// <inheritdoc cref="Encode(string, Hl7EncodingCharacters)"/>
    public static string Encode(this Hl7EncodingCharacters encoding, string value) => Encode(value, encoding);
    
    /// <summary>
    /// Encodes delimiter characters as HL7 escape sequences.
    /// </summary>
    /// <param name="value">Literal text that may contain delimiter characters.</param>
    /// <param name="encoding">Delimiter set that defines which characters must be escaped.</param>
    /// <returns>Wire-safe text with separators escaped.</returns>
    public static string Encode(string value, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        var result = new StringBuilder(value.Length);
        foreach (var ch in value)
        {
            if (ch == encoding.FieldSeparator)
                result.Append(encoding.EscapeCharacter).Append('F').Append(encoding.EscapeCharacter);
            else if (ch == encoding.ComponentSeparator)
                result.Append(encoding.EscapeCharacter).Append('S').Append(encoding.EscapeCharacter);
            else if (ch == encoding.RepetitionSeparator)
                result.Append(encoding.EscapeCharacter).Append('R').Append(encoding.EscapeCharacter);
            else if (ch == encoding.EscapeCharacter)
                result.Append(encoding.EscapeCharacter).Append('E').Append(encoding.EscapeCharacter);
            else if (ch == encoding.SubcomponentSeparator)
                result.Append(encoding.EscapeCharacter).Append('T').Append(encoding.EscapeCharacter);
            else
                result.Append(ch);
        }

        return result.ToString();
    }

    /// <summary>
    /// Appends a single-codepoint <c>\X...\</c> escape, or the raw payload on failure.
    /// </summary>
    private static bool TryAppendHexEscape(string value, StringBuilder result)
    {
        if (5 > value.Length)
        {
            result.Append(value);
            return false;
        }

        if (int.TryParse(value[1..], System.Globalization.NumberStyles.HexNumber, null, out var codePoint))
        {
            result.Append((char)codePoint);
            return true;
        }

        result.Append(value);
        return false;
    }

    /// <summary>
    /// Appends consecutive 4-digit hex code points from a <c>\Z...\</c> escape.
    /// </summary>
    private static bool TryAppendMultiHexEscape(string value, StringBuilder result)
    {
        var index = 1;
        while (index + 4 <= value.Length)
        {
            var hex = value.Substring(index, 4);
            if (!int.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out var codePoint))
                return false;

            result.Append((char)codePoint);
            index += 4;
        }
        
        return true;
    }
}
