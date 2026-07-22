namespace AryaDev.Utils.HL7.Domain.Encoding;

/// <summary>
/// HL7 delimiter characters for a message (typically from MSH-1 / MSH-2).
/// </summary>
/// <param name="fieldSeparator">Field separator (MSH-1), usually <c>|</c>.</param>
/// <param name="componentSeparator">Component separator, usually <c>^</c>.</param>
/// <param name="repetitionSeparator">Repetition separator, usually <c>~</c>.</param>
/// <param name="escapeCharacter">Escape character, usually <c>\</c>.</param>
/// <param name="subcomponentSeparator">Subcomponent separator, usually <c>&amp;</c>.</param>
/// <remarks>
/// Standard default set is <c>|^~\&amp;</c> via <see cref="Default"/>.
/// </remarks>
public sealed class Hl7EncodingCharacters(
    char fieldSeparator,
    char componentSeparator,
    char repetitionSeparator,
    char escapeCharacter,
    char subcomponentSeparator)
{
    /// <summary>
    /// Standard HL7 encoding characters: <c>|</c>, <c>^</c>, <c>~</c>, <c>\</c>, <c>&amp;</c>.
    /// </summary>
    public static Hl7EncodingCharacters Default { get; } = new('|', '^', '~', '\\', '&');

    /// <summary>
    /// Field separator (MSH-1).
    /// </summary>
    public char FieldSeparator { get; } = fieldSeparator;

    /// <summary>
    /// Component separator (first character of MSH-2).
    /// </summary>
    public char ComponentSeparator { get; } = componentSeparator;

    /// <summary>
    /// Repetition separator (second character of MSH-2).
    /// </summary>
    public char RepetitionSeparator { get; } = repetitionSeparator;

    /// <summary>
    /// Escape character (third character of MSH-2).
    /// </summary>
    public char EscapeCharacter { get; } = escapeCharacter;

    /// <summary>
    /// Subcomponent separator (fourth character of MSH-2).
    /// </summary>
    public char SubcomponentSeparator { get; } = subcomponentSeparator;

    /// <summary>
    /// Builds encoding characters from an MSH-2 string.
    /// </summary>
    /// <param name="msh2">MSH-2 value (encoding characters after the field separator).</param>
    /// <returns>
    /// Parsed encoding characters; missing trailing characters fall back to <see cref="Default"/>.
    /// Empty/null input returns <see cref="Default"/>.
    /// </returns>
    /// <remarks>
    /// The first character of <paramref name="msh2"/> is treated as the field separator in this API
    /// when callers pass <c>line[3..]</c> from an MSH line (separator + MSH-2).
    /// </remarks>
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

    /// <summary>
    /// Formats the four MSH-2 encoding characters (excluding the field separator).
    /// </summary>
    /// <returns>String of component, repetition, escape, and subcomponent separators.</returns>
    public string ToMsh2() =>
        string.Concat(ComponentSeparator, RepetitionSeparator, EscapeCharacter, SubcomponentSeparator);
}
