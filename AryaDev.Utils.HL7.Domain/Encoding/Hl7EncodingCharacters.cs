namespace AryaDev.Utils.HL7.Domain.Encoding;

public sealed class Hl7EncodingCharacters(
    char fieldSeparator,
    char componentSeparator,
    char repetitionSeparator,
    char escapeCharacter,
    char subcomponentSeparator)
{
    public static Hl7EncodingCharacters Default { get; } = new('|', '^', '~', '\\', '&');

    public char FieldSeparator { get; } = fieldSeparator;
    public char ComponentSeparator { get; } = componentSeparator;
    public char RepetitionSeparator { get; } = repetitionSeparator;
    public char EscapeCharacter { get; } = escapeCharacter;
    public char SubcomponentSeparator { get; } = subcomponentSeparator;

    public static Hl7EncodingCharacters FromMsh2(string msh2)
    {
        if (string.IsNullOrEmpty(msh2))
            return Default;

        return new Hl7EncodingCharacters(
            msh2[0],
            msh2.Length > 1 ? msh2[1] : Default.ComponentSeparator,
            msh2.Length > 2 ? msh2[2] : Default.RepetitionSeparator,
            msh2.Length > 3 ? msh2[3] : Default.EscapeCharacter,
            msh2.Length > 4 ? msh2[4] : Default.SubcomponentSeparator);
    }

    public string ToMsh2() =>
        string.Concat(ComponentSeparator, RepetitionSeparator, EscapeCharacter, SubcomponentSeparator);
}
