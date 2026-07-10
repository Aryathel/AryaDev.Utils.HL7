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
            if (value[i] != escape)
            {
                result.Append(value[i]);
                continue;
            }

            if (i + 1 >= value.Length)
            {
                result.Append(escape);
                continue;
            }

            var code = value[i + 1];
            switch (code)
            {
                case 'F':
                    result.Append(encoding.FieldSeparator);
                    i++;
                    break;
                case 'S':
                    result.Append(encoding.ComponentSeparator);
                    i++;
                    break;
                case 'T':
                    result.Append(encoding.SubcomponentSeparator);
                    i++;
                    break;
                case 'R':
                    result.Append(encoding.RepetitionSeparator);
                    i++;
                    break;
                case 'E':
                    result.Append(encoding.EscapeCharacter);
                    i++;
                    break;
                case 'X':
                    i = AppendHexEscape(value, i, result);
                    break;
                case 'Z':
                    i = AppendMultiHexEscape(value, i, result);
                    break;
                default:
                    result.Append(escape);
                    break;
            }
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

    private static int AppendHexEscape(string value, int escapeIndex, StringBuilder result)
    {
        var start = escapeIndex + 2;
        if (start + 4 > value.Length)
        {
            result.Append(value[escapeIndex]);
            return escapeIndex;
        }

        var hex = value.Substring(start, 4);
        if (int.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out var codePoint))
        {
            result.Append((char)codePoint);
            return start + 3;
        }

        result.Append(value[escapeIndex]);
        return escapeIndex;
    }

    private static int AppendMultiHexEscape(string value, int escapeIndex, StringBuilder result)
    {
        var index = escapeIndex + 2;
        while (index + 4 <= value.Length)
        {
            var hex = value.Substring(index, 4);
            if (!int.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out var codePoint))
                break;

            result.Append((char)codePoint);
            index += 4;
        }

        if (index > escapeIndex + 2)
            return index - 1;

        result.Append(value[escapeIndex]);
        return escapeIndex;
    }
}
