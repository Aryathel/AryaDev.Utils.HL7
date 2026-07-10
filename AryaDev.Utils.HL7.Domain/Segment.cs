using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;
using AryaDev.Utils.HL7.Domain.Model;

namespace AryaDev.Utils.HL7.Domain;

/// <summary>
/// A single HL7 segment with nested string field storage.
/// Fields, repetitions, components, and subcomponents are all 1-based when accessed via <see cref="Hl7Path"/>.
/// </summary>
public class Segment
{
    /// <summary>
    /// fields[fieldIndex] = repetitions for HL7 field (fieldIndex + 1) <br />
    /// repetitions[repIndex] = components <br />
    /// components[compIndex] = subcomponents
    /// </summary>
    private readonly List<List<List<List<string>>>> _fields = [];

    public Segment(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length != 3)
            throw new ArgumentException("Segment name must be exactly 3 characters.", nameof(name));

        Name = name.ToUpperInvariant();
    }

    public string Name { get; }

    public SegmentType? Type => SegmentTypeExtensions.TryParseCode(Name, out var type) ? type : null;

    public IReadOnlyList<IReadOnlyList<IReadOnlyList<IReadOnlyList<string>>>> Fields =>
        _fields;

    internal List<List<List<List<string>>>> FieldsInternal => _fields;

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
        var repIndex = path.Repetition - 1;
        if (repIndex < 0 || repIndex >= repetitions.Count)
            return null;

        var components = repetitions[repIndex];

        if (path.Component is null)
            return JoinFieldLevel(repetitions, encoding);

        var compIndex = path.Component.Value - 1;
        if (compIndex < 0 || compIndex >= components.Count)
            return null;

        var subcomponents = components[compIndex];

        if (path.Subcomponent is null)
            return JoinComponents(components, encoding);

        var subIndex = path.Subcomponent.Value - 1;
        if (subIndex < 0 || subIndex >= subcomponents.Count)
            return null;

        return subcomponents[subIndex];
    }

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

        var repIndex = path.Repetition - 1;
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

    internal void SetFieldCount(int count)
    {
        while (_fields.Count < count)
            _fields.Add([]);
    }

    public void SetFieldFromRaw(int fieldNumber, string rawValue, Hl7EncodingCharacters encoding)
    {
        var fieldIndex = fieldNumber - 1;
        EnsureFieldCapacity(fieldIndex);
        _fields[fieldIndex] = ParseFieldValue(rawValue, encoding);
    }

    public string GetFieldRaw(int fieldNumber, Hl7EncodingCharacters encoding)
    {
        var fieldIndex = fieldNumber - 1;
        if (fieldIndex < 0 || fieldIndex >= _fields.Count)
            return string.Empty;

        return JoinFieldLevel(_fields[fieldIndex], encoding);
    }

    private static string JoinFieldLevel(List<List<List<string>>> repetitions, Hl7EncodingCharacters encoding)
    {
        if (repetitions.Count == 0)
            return string.Empty;

        return string.Join(
            encoding.RepetitionSeparator,
            repetitions.Select(rep => JoinComponents(rep, encoding)));
    }

    private static string JoinComponents(List<List<string>> components, Hl7EncodingCharacters encoding)
    {
        if (components.Count == 0)
            return string.Empty;

        return string.Join(
            encoding.ComponentSeparator,
            components.Select(comp => JoinSubcomponents(comp, encoding)));
    }

    private static string JoinSubcomponents(List<string> subcomponents, Hl7EncodingCharacters encoding)
    {
        if (subcomponents.Count == 0)
            return string.Empty;

        return string.Join(encoding.SubcomponentSeparator, subcomponents);
    }

    private static List<List<List<string>>> ParseFieldValue(string rawValue, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(rawValue))
            return [[[]]];

        var repetitions = rawValue.Split(encoding.RepetitionSeparator);
        return repetitions.Select(rep => ParseComponentList(rep, encoding)).ToList();
    }

    private static List<List<string>> ParseComponentList(string repetitionValue, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(repetitionValue))
            return [[]];

        var components = repetitionValue.Split(encoding.ComponentSeparator);
        return components.Select(comp => ParseSubcomponentList(comp, encoding)).ToList();
    }

    private static List<string> ParseSubcomponentList(string componentValue, Hl7EncodingCharacters encoding)
    {
        if (string.IsNullOrEmpty(componentValue))
            return [];

        return componentValue.Split(encoding.SubcomponentSeparator).ToList();
    }

    private static List<string> ParseComponentValue(string rawValue, Hl7EncodingCharacters encoding) =>
        ParseSubcomponentList(rawValue, encoding);

    private void EnsureFieldCapacity(int fieldIndex)
    {
        while (_fields.Count <= fieldIndex)
            _fields.Add([]);
    }

    private void EnsureRepetitionCapacity(int fieldIndex, int repIndex)
    {
        var repetitions = _fields[fieldIndex];
        while (repetitions.Count <= repIndex)
            repetitions.Add([]);
    }

    private void EnsureComponentCapacity(int fieldIndex, int repIndex, int compIndex)
    {
        var components = _fields[fieldIndex][repIndex];
        while (components.Count <= compIndex)
            components.Add([]);
    }

    private void EnsureSubcomponentCapacity(int fieldIndex, int repIndex, int compIndex, int subIndex)
    {
        var subcomponents = _fields[fieldIndex][repIndex][compIndex];
        while (subcomponents.Count <= subIndex)
            subcomponents.Add(string.Empty);
    }
}
