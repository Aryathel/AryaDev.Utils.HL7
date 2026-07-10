using System.Text;
using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Domain.Encoding;

namespace AryaDev.Utils.HL7.Serializer;

internal static class HL7MessageWriter
{
    internal static string Write(HL7Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

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

    private static string WriteSegment(Segment segment, Hl7EncodingCharacters encoding)
    {
        var separator = encoding.FieldSeparator;
        var builder = new StringBuilder(segment.Name);
        var lastField = GetLastPopulatedField(segment);

        var seed = string.Equals(segment.Name, "MSH", StringComparison.Ordinal) ? 2 : 1;

        for (var field = seed; field <= lastField; field++)
        {
            builder.Append(separator);
            var raw = segment.GetFieldRaw(field, encoding);
            builder.Append(Hl7Escape.Encode(raw, encoding));
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
