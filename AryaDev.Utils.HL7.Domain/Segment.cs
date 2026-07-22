using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;
using AryaDev.Utils.HL7.Domain.Model;

namespace AryaDev.Utils.HL7.Domain;

/// <summary>
/// A single HL7 segment with nested string field storage.
/// </summary>
/// <remarks>
/// Storage layout is field → repetition → component → subcomponent.
/// Paths use 1-based indexing via <see cref="Hl7Path"/>.
/// </remarks>
public class Segment
{
    /// <summary>
    /// Nested field storage for this segment.
    /// </summary>
    /// <remarks>
    /// <c>fields[fieldIndex]</c> = repetitions for HL7 field <c>fieldIndex + 1</c>;
    /// each repetition holds components; each component holds subcomponents.
    /// </remarks>
    private readonly List<List<List<List<string>>>> _fields = [];

    /// <summary>
    /// Creates a segment with the given three-character name.
    /// </summary>
    /// <param name="name">Segment ID (exactly three letters/digits, starting with a letter).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="name"/> is null/whitespace, not length 3, or has an invalid format.
    /// </exception>
    public Segment(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length != 3)
            throw new ArgumentException("Segment name must be exactly 3 characters.", nameof(name));
        
        if (!char.IsLetter(name[0]) || !name.All(char.IsLetterOrDigit))
            throw new ArgumentException("Segment name must start with a letter and contain only letters and digits.", nameof(name));

        Name = name.ToUpperInvariant();
    }

    /// <summary>
    /// Three-character segment ID, always uppercased.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Known <see cref="SegmentType"/> for <see cref="Name"/>, if recognized.
    /// </summary>
    /// <remarks>
    /// Returns <see langword="null"/> when the name is not a mapped standard segment (Z-segments map to <see cref="SegmentType.ZSegment"/>).
    /// </remarks>
    public SegmentType? Type => SegmentTypeExtensions.TryParseCode(Name, out var type) ? type : null;

    /// <summary>
    /// Read-only view of nested field storage.
    /// </summary>
    /// <remarks>
    /// Indices are 0-based in this collection; HL7 field numbers are 1-based when using path APIs.
    /// </remarks>
    public IReadOnlyList<IReadOnlyList<IReadOnlyList<IReadOnlyList<string>>>> Fields =>
        _fields;

    /// <summary>
    /// Mutable field storage used by the serializer.
    /// </summary>
    internal List<List<List<List<string>>>> FieldsInternal => _fields;

    /// <inheritdoc cref="GetValue(Hl7Path, Hl7EncodingCharacters)"/>
    /// <exception cref="FormatException">Thrown when <paramref name="path"/> cannot be parsed.</exception>
    public string? GetValue(string path, Hl7EncodingCharacters encoding) =>
        GetValue(Hl7Path.Parse(path), encoding);
    
    /// <summary>
    /// Gets the value at the given path.
    /// </summary>
    /// <param name="path">HL7 location path.</param>
    /// <param name="encoding">Encoding characters used when joining values.</param>
    /// <returns>The value at the path, or <see langword="null"/> when the location is missing.</returns>
    /// <remarks>
    /// Field paths without <c>~n</c> return all repetitions joined by the repetition separator.
    /// Field paths with <c>~n</c> return that repetition only.
    /// Component/subcomponent paths default to repetition 1 when <c>~n</c> is omitted.
    /// Values are returned unescaped (decoded).
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="path"/> or <paramref name="encoding"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="path"/> targets a different segment name.</exception>
    public string? GetValue(Hl7Path path, Hl7EncodingCharacters encoding)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(encoding);
        
        if (!string.Equals(path.SegmentName, Name, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"Path segment '{path.SegmentName}' does not match segment '{Name}'.");

        if (path.Field is null)
            return null;

        var fieldIndex = path.Field.Value - 1;
        if (fieldIndex < 0 || fieldIndex >= _fields.Count)
            return null;

        var repetitions = _fields[fieldIndex];

        if (path.Component is null)
        {
            if (path.Repetition is null)
                return JoinFieldLevel(repetitions, encoding, false);

            var explicitRepIndex = path.Repetition.Value - 1;
            if (explicitRepIndex < 0 || explicitRepIndex >= repetitions.Count)
                return null;

            return JoinComponents(repetitions[explicitRepIndex], encoding, false);
        }

        var repIndex = (path.Repetition ?? 1) - 1;
        if (repIndex < 0 || repIndex >= repetitions.Count)
            return null;

        var components = repetitions[repIndex];
        var compIndex = path.Component.Value - 1;
        if (compIndex < 0 || compIndex >= components.Count)
            return null;

        var subcomponents = components[compIndex];

        if (path.Subcomponent is null)
            return JoinSubcomponents(subcomponents, encoding, false);

        var subIndex = path.Subcomponent.Value - 1;
        if (subIndex < 0 || subIndex >= subcomponents.Count)
            return null;

        return subcomponents[subIndex];
    }

    /// <inheritdoc cref="SetValue(Hl7Path, string?, Hl7EncodingCharacters)"/>
    /// <exception cref="FormatException">Thrown when <paramref name="path"/> cannot be parsed.</exception>
    public void SetValue(string path, string? value, Hl7EncodingCharacters encoding) =>
        SetValue(Hl7Path.Parse(path), value, encoding);

    /// <summary>
    /// Sets a value at the given path, parsing separator characters in <paramref name="value"/>.
    /// </summary>
    /// <param name="path">HL7 location path (field required).</param>
    /// <param name="value">Value to store; <see langword="null"/> is treated as empty.</param>
    /// <param name="encoding">Encoding characters that define how <paramref name="value"/> is split.</param>
    /// <remarks>
    /// Omitting <c>~n</c> targets repetition 1.
    /// Field-level sets parse component/subcomponent separators; use <see cref="SetValueRaw(Hl7Path, string?)"/> for literals.
    /// Missing structure is created as needed.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="path"/> or <paramref name="encoding"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the path segment name mismatches, or when <paramref name="path"/> has no field.
    /// </exception>
    public void SetValue(Hl7Path path, string? value, Hl7EncodingCharacters encoding)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(encoding);

        if (!string.Equals(path.SegmentName, Name, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"Path segment '{path.SegmentName}' does not match segment '{Name}'.");

        if (path.Field is null)
            throw new ArgumentException("Field is required to set a value.", nameof(path));

        var fieldIndex = path.Field.Value - 1;
        EnsureFieldCapacity(fieldIndex);

        var repIndex = (path.Repetition ?? 1) - 1;
        EnsureRepetitionCapacity(fieldIndex, repIndex);

        if (path.Component is null)
        {
            _fields[fieldIndex][repIndex] = ParseComponentList(value ?? string.Empty, encoding);
            return;
        }

        var compIndex = path.Component.Value - 1;
        EnsureComponentCapacity(fieldIndex, repIndex, compIndex);

        if (path.Subcomponent is null)
        {
            _fields[fieldIndex][repIndex][compIndex] = ParseComponentValue(value ?? string.Empty, encoding);
            return;
        }

        var subIndex = path.Subcomponent.Value - 1;
        EnsureSubcomponentCapacity(fieldIndex, repIndex, compIndex, subIndex);
        _fields[fieldIndex][repIndex][compIndex][subIndex] = value ?? string.Empty;
    }

    /// <inheritdoc cref="SetValueRaw(Hl7Path, string?)"/>
    /// <exception cref="FormatException">Thrown when <paramref name="path"/> cannot be parsed.</exception>
    public void SetValueRaw(string path, string? value) =>
        SetValueRaw(Hl7Path.Parse(path), value);
    
    /// <summary>
    /// Sets a literal value at the given path without parsing HL7 separators.
    /// </summary>
    /// <param name="path">HL7 location path (field required).</param>
    /// <param name="value">Literal value to store without splitting on separators; <see langword="null"/> is treated as empty.</param>
    /// <remarks>
    /// Characters such as <c>^</c> and <c>~</c> remain part of the stored text.
    /// Omitting <c>~n</c> targets repetition 1.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="path"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the path segment name mismatches, or when <paramref name="path"/> has no field.
    /// </exception>
    public void SetValueRaw(Hl7Path path, string? value)
    {
        ArgumentNullException.ThrowIfNull(path);

        if (!string.Equals(path.SegmentName, Name, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException($"Path segment '{path.SegmentName}' does not match segment '{Name}'.");

        if (path.Field is null)
            throw new ArgumentException("Field is required to set a value.", nameof(path));

        var fieldIndex = path.Field.Value - 1;
        EnsureFieldCapacity(fieldIndex);

        var repIndex = (path.Repetition ?? 1) - 1;
        EnsureRepetitionCapacity(fieldIndex, repIndex);

        if (path.Component is null)
        {
            _fields[fieldIndex][repIndex] = [[value ?? string.Empty]];
            return;
        }

        var compIndex = path.Component.Value - 1;
        EnsureComponentCapacity(fieldIndex, repIndex, compIndex);

        if (path.Subcomponent is null)
        {
            _fields[fieldIndex][repIndex][compIndex] = [value ?? string.Empty];
            return;
        }

        var subIndex = path.Subcomponent.Value - 1;
        EnsureSubcomponentCapacity(fieldIndex, repIndex, compIndex, subIndex);
        _fields[fieldIndex][repIndex][compIndex][subIndex] = value ?? string.Empty;
    }
    
    /// <summary>
    /// Ensures the segment has at least <paramref name="count"/> fields.
    /// </summary>
    /// <param name="count">Minimum field count (0-based capacity target as count of fields).</param>
    internal void SetFieldCount(int count)
    {
        while (_fields.Count < count)
            _fields.Add([]);
    }

    /// <summary>
    /// Replaces a field by parsing a raw value (repetitions, components, escapes).
    /// </summary>
    /// <param name="fieldNumber">1-based HL7 field number.</param>
    /// <param name="rawValue">Raw field text as it appears between field separators.</param>
    /// <param name="encoding">Encoding characters used to split and decode <paramref name="rawValue"/>.</param>
    public void SetFieldFromRaw(int fieldNumber, string rawValue, Hl7EncodingCharacters encoding)
    {
        var fieldIndex = fieldNumber - 1;
        EnsureFieldCapacity(fieldIndex);
        _fields[fieldIndex] = ParseFieldValue(rawValue, encoding);
    }

    /// <summary>
    /// Assembles a field for serialization with HL7 escape sequences applied.
    /// </summary>
    /// <param name="fieldNumber">1-based HL7 field number.</param>
    /// <param name="encoding">Encoding characters used for joining and escaping.</param>
    /// <returns>Escaped field text, or empty when the field is absent.</returns>
    /// <remarks>
    /// For empty MSH-2, returns <see cref="Hl7EncodingCharacters.ToMsh2"/>.
    /// </remarks>
    public string GetFieldEscaped(int fieldNumber, Hl7EncodingCharacters encoding) =>
        GetField(fieldNumber, encoding, true);

    /// <summary>
    /// Assembles a field without applying escape encoding.
    /// </summary>
    /// <param name="fieldNumber">1-based HL7 field number.</param>
    /// <param name="encoding">Encoding characters used for joining.</param>
    /// <returns>Unescaped field text, or empty when the field is absent.</returns>
    public string GetFieldRaw(int fieldNumber, Hl7EncodingCharacters encoding) =>
        GetField(fieldNumber, encoding, false);

    /// <summary>
    /// Builds the full representation of a field.
    /// </summary>
    /// <param name="fieldNumber">1-based HL7 field number.</param>
    /// <param name="encoding">Encoding characters used for joining.</param>
    /// <param name="escaped">Whether to escape special characters.</param>
    private string GetField(int fieldNumber, Hl7EncodingCharacters encoding, bool escaped)
    {
        if (fieldNumber == 2 && Name == "MSH" && string.IsNullOrWhiteSpace(GetValue("MSH.2", encoding)))
            return encoding.ToMsh2();
        
        var fieldIndex = fieldNumber - 1;
        if (fieldIndex < 0 || fieldIndex >= _fields.Count)
            return string.Empty;

        return JoinFieldLevel(_fields[fieldIndex], encoding, escaped);
    }

    /// <summary>
    /// Joins all repetitions of a field.
    /// </summary>
    private static string JoinFieldLevel(List<List<List<string>>> repetitions, Hl7EncodingCharacters encoding, bool escaped)
    {
        if (repetitions.Count == 0)
            return string.Empty;

        return string.Join(
            encoding.RepetitionSeparator,
            repetitions.Select(rep => JoinComponents(rep, encoding, escaped)));
    }

    /// <summary>
    /// Joins components within a single repetition.
    /// </summary>
    private static string JoinComponents(List<List<string>> components, Hl7EncodingCharacters encoding, bool escaped)
    {
        if (components.Count == 0)
            return string.Empty;

        return string.Join(
            encoding.ComponentSeparator,
            components.Select(comp => JoinSubcomponents(comp, encoding, escaped)));
    }

    /// <summary>
    /// Joins subcomponents within a single component.
    /// </summary>
    private static string JoinSubcomponents(List<string> subcomponents, Hl7EncodingCharacters encoding, bool escaped)
    {
        if (subcomponents.Count == 0)
            return string.Empty;

        return string.Join(encoding.SubcomponentSeparator, escaped ? subcomponents.Select(s => Hl7Escape.Encode(s, encoding)) : subcomponents);
    }

    /// <summary>
    /// Parses a raw field value into repetitions → components → subcomponents.
    /// </summary>
    private static List<List<List<string>>> ParseFieldValue(string rawValue, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(rawValue))
            return [[[]]];

        var repetitions = rawValue.Split(encoding.RepetitionSeparator);
        return repetitions.Select(rep => ParseComponentList(rep, encoding)).ToList();
    }

    /// <summary>
    /// Parses one repetition into components (and decodes subcomponents).
    /// </summary>
    private static List<List<string>> ParseComponentList(string repetitionValue, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(repetitionValue))
            return [[]];

        var components = repetitionValue.Split(encoding.ComponentSeparator);
        return components.Select(comp => ParseSubcomponentList(comp, encoding)).ToList();
    }

    /// <summary>
    /// Parses one component into decoded subcomponent strings.
    /// </summary>
    private static List<string> ParseSubcomponentList(string componentValue, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(componentValue))
            return [];

        return componentValue.Split(encoding.SubcomponentSeparator).Select(c => Hl7Escape.Decode(c, encoding)).ToList();
    }

    /// <summary>
    /// Parses a component-level value into subcomponents.
    /// </summary>
    private static List<string> ParseComponentValue(string rawValue, Hl7EncodingCharacters encoding) =>
        ParseSubcomponentList(rawValue, encoding);

    /// <summary>
    /// Ensures field storage reaches at least <paramref name="fieldIndex"/>.
    /// </summary>
    private void EnsureFieldCapacity(int fieldIndex)
    {
        while (_fields.Count <= fieldIndex)
            _fields.Add([]);
    }

    /// <summary>
    /// Ensures repetition storage reaches at least <paramref name="repIndex"/> for a field.
    /// </summary>
    private void EnsureRepetitionCapacity(int fieldIndex, int repIndex)
    {
        var repetitions = _fields[fieldIndex];
        while (repetitions.Count <= repIndex)
            repetitions.Add([]);
    }

    /// <summary>
    /// Ensures component storage reaches at least <paramref name="compIndex"/> for a repetition.
    /// </summary>
    private void EnsureComponentCapacity(int fieldIndex, int repIndex, int compIndex)
    {
        var components = _fields[fieldIndex][repIndex];
        while (components.Count <= compIndex)
            components.Add([]);
    }

    /// <summary>
    /// Ensures subcomponent storage reaches at least <paramref name="subIndex"/> for a component.
    /// </summary>
    private void EnsureSubcomponentCapacity(int fieldIndex, int repIndex, int compIndex, int subIndex)
    {
        var subcomponents = _fields[fieldIndex][repIndex][compIndex];
        while (subcomponents.Count <= subIndex)
            subcomponents.Add(string.Empty);
    }
}
