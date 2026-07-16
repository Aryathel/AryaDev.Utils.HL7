using System.Text;
using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Serializer;

internal static class HL7MessageWriter
{
    internal static string Write(HL7Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        ApplyMessageTypeIfMsh9Unset(message);
        ApplyEncodingIfMsh18Unset(message);

        var encoding = message.Encoding;
        var builder = new StringBuilder();

        for (var i = 0; i < message.Segments.Count; i++)
        {
            if (i > 0)
                builder.Append('\r');

            builder.Append(WriteSegment(message.Segments[i], encoding));
        }

        return builder.ToString();
    }

    internal static byte[] WriteBytes(HL7Message message)
    {
        var text = Write(message);
        return message.TextEncoding.GetBytes(text);
    }

    /// <summary>
    /// When MSH-9 is empty and <see cref="HL7Message.MessageType"/> is known, populate MSH-9 from the typed value.
    /// Does not overwrite an existing MSH-9 value.
    /// </summary>
    private static void ApplyMessageTypeIfMsh9Unset(HL7Message message)
    {
        if (message.MessageType == MessageType.Unknown)
            return;

        if (!string.IsNullOrWhiteSpace(message["MSH.9"]))
            return;

        var msh9 = message.MessageType.ToMsh9Value(message.Encoding);
        if (!string.IsNullOrWhiteSpace(msh9))
            message["MSH.9"] = msh9;
    }

    private static void ApplyEncodingIfMsh18Unset(HL7Message message)
    {
        if (message.TextEncoding is null)
            return;

        if (!string.IsNullOrWhiteSpace(message["MSH.18"]))
            return;
        
        var msh18 = message.TextEncoding.ToMsh18Value();
        if (!string.IsNullOrWhiteSpace(msh18))
            message["MSH.18"] = msh18;
    }

    private static string WriteSegment(Segment segment, Hl7EncodingCharacters encoding)
    {
        var separator = encoding.FieldSeparator;
        var builder = new StringBuilder(segment.Name);
        var lastField = GetLastPopulatedField(segment);

        var seed = string.Equals(segment.Name, "MSH", StringComparison.Ordinal) ? 2 : 1;

        for (var field = seed; field <= lastField; field++)
        {
            builder.Append(separator);
            var raw = segment.GetFieldEscaped(field, encoding);
            builder.Append(raw);
        }

        return builder.ToString();
    }

    private static int GetLastPopulatedField(Segment segment)
    {
        var fields = segment.Fields;
        for (var i = fields.Count - 1; i >= 0; i--)
            if (FieldHasContent(fields[i]))
                return i + 1;

        return string.Equals(segment.Name, "MSH", StringComparison.Ordinal) ? 1 : 0;
    }

    private static bool FieldHasContent(IReadOnlyList<IReadOnlyList<IReadOnlyList<string>>> repetitions)
    {
        foreach (var repetition in repetitions)
        {
            foreach (var component in repetition)
            {
                foreach (var subcomponent in component)
                    if (!string.IsNullOrEmpty(subcomponent))
                        return true;

                if (component.Count > 1)
                    return true;
            }

            if (repetition.Count > 1)
                return true;
        }

        return repetitions.Count > 0;
    }
}
