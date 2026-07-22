using System.ComponentModel;
using System.Reflection;
using AryaDev.Utils.HL7.Domain.Attributes;
using AryaDev.Utils.HL7.Domain.Encoding;

namespace AryaDev.Utils.HL7.Domain.Enumeration;

/// <summary>
/// Helpers for <see cref="MessageType"/> codes, MSH-9 conversion, and descriptions.
/// </summary>
public static class MessageTypeExtensions
{
    /// <summary>
    /// Lookup of Table 0354 codes to <see cref="MessageType"/>.
    /// </summary>
    private static readonly Dictionary<string, MessageType> CodeLookup = BuildCodeLookup();

    /// <summary>
    /// Returns the HL7 Table 0354 code for the message type.
    /// </summary>
    /// <param name="messageType">Message type.</param>
    /// <returns>Code from <see cref="MessageTypeInfoAttribute"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="messageType"/> is not a defined enum field.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the member lacks <see cref="MessageTypeInfoAttribute"/>.</exception>
    public static string GetCode(this MessageType messageType) =>
        GetMessageTypeInfo(messageType).Code;

    /// <summary>
    /// Returns the <see cref="DescriptionAttribute"/> text for the message type, if any.
    /// </summary>
    /// <param name="messageType">Message type.</param>
    /// <returns>Description text, or <see langword="null"/> when absent.</returns>
    public static string? GetDescription(this MessageType messageType)
    {
        var field = typeof(MessageType).GetField(messageType.ToString());
        return field?.GetCustomAttribute<DescriptionAttribute>()?.Description;
    }

    /// <summary>
    /// Attempts to parse an HL7 Table 0354 code (for example <c>ADT_A01</c> or <c>ACK</c>).
    /// </summary>
    /// <param name="code">Message type / structure code.</param>
    /// <param name="messageType">Resolved type when successful; otherwise <see cref="MessageType.Unknown"/>.</param>
    /// <returns><see langword="true"/> when the code matches a known type.</returns>
    public static bool TryParseCode(string? code, out MessageType messageType)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            messageType = MessageType.Unknown;
            return false;
        }

        return CodeLookup.TryGetValue(code.Trim().ToUpperInvariant(), out messageType);
    }

    /// <summary>
    /// Resolves a message type from MSH-9 components.
    /// </summary>
    /// <param name="messageCode">MSH-9.1 message code.</param>
    /// <param name="triggerEvent">MSH-9.2 trigger event.</param>
    /// <param name="messageStructure">MSH-9.3 message structure.</param>
    /// <returns>Matched <see cref="MessageType"/>, or <see cref="MessageType.Unknown"/>.</returns>
    /// <remarks>
    /// Preference order: MSH-9.3, then <c>{MSH-9.1}_{MSH-9.2}</c>, then MSH-9.1 alone.
    /// </remarks>
    public static MessageType ParseFromMsh9(string? messageCode, string? triggerEvent, string? messageStructure)
    {
        if (!string.IsNullOrWhiteSpace(messageStructure) &&
            TryParseCode(messageStructure, out var fromStructure))
            return fromStructure;

        if (!string.IsNullOrWhiteSpace(messageCode) && !string.IsNullOrWhiteSpace(triggerEvent))
        {
            var synthesized = $"{messageCode.Trim()}_{triggerEvent.Trim()}";
            if (TryParseCode(synthesized, out var fromParts))
                return fromParts;
        }

        if (!string.IsNullOrWhiteSpace(messageCode) &&
            string.IsNullOrWhiteSpace(triggerEvent) &&
            string.IsNullOrWhiteSpace(messageStructure) &&
            TryParseCode(messageCode, out var fromCodeAlone))
            return fromCodeAlone;

        return MessageType.Unknown;
    }

    /// <summary>
    /// Builds a three-component MSH-9 value for a known message type.
    /// </summary>
    /// <param name="messageType">Message type to format.</param>
    /// <param name="encoding">Delimiter set (component separator used between parts).</param>
    /// <returns>
    /// <c>Code{sep}Trigger{sep}Structure</c>, or <see langword="null"/> for <see cref="MessageType.Unknown"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="encoding"/> is null.</exception>
    public static string? ToMsh9Value(this MessageType messageType, Hl7EncodingCharacters encoding)
    {
        ArgumentNullException.ThrowIfNull(encoding);

        if (messageType == MessageType.Unknown)
            return null;

        var sep = encoding.ComponentSeparator;
        var code = messageType.GetCode();
        var underscore = code.IndexOf('_');
        if (underscore <= 0 || underscore == code.Length - 1)
            return $"{code}{sep}{sep}{code}";

        var messageCode = code[..underscore];
        var triggerEvent = code[(underscore + 1)..];
        return $"{messageCode}{sep}{triggerEvent}{sep}{code}";
    }

    /// <summary>
    /// Reads <see cref="MessageTypeInfoAttribute"/> for an enum member.
    /// </summary>
    private static MessageTypeInfoAttribute GetMessageTypeInfo(MessageType messageType)
    {
        var field = typeof(MessageType).GetField(messageType.ToString())
            ?? throw new ArgumentException($"Unknown message type: {messageType}", nameof(messageType));

        return field.GetCustomAttribute<MessageTypeInfoAttribute>()
            ?? throw new InvalidOperationException(
                $"Message type {messageType} is missing MessageTypeInfoAttribute.");
    }

    /// <summary>
    /// Builds the static code → type lookup (skips <see cref="MessageType.Unknown"/>).
    /// </summary>
    private static Dictionary<string, MessageType> BuildCodeLookup()
    {
        var lookup = new Dictionary<string, MessageType>(StringComparer.OrdinalIgnoreCase);
        foreach (var messageType in Enum.GetValues<MessageType>())
        {
            if (messageType == MessageType.Unknown)
                continue;

            var code = messageType.GetCode();
            if (string.IsNullOrEmpty(code))
                continue;

            lookup.TryAdd(code, messageType);
        }

        return lookup;
    }
}
