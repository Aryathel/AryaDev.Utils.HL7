using System.Text;

namespace AryaDev.Utils.HL7.Domain.Encoding;

public static class Hl7Escape
{
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
