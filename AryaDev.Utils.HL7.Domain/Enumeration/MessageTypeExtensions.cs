using System.ComponentModel;
using System.Reflection;
using AryaDev.Utils.HL7.Domain.Attributes;
using AryaDev.Utils.HL7.Domain.Encoding;

namespace AryaDev.Utils.HL7.Domain.Enumeration;

public static class MessageTypeExtensions
{
    private static readonly Dictionary<string, MessageType> CodeLookup = BuildCodeLookup();

    public static string GetCode(this MessageType messageType) =>
        GetMessageTypeInfo(messageType).Code;

    public static string? GetDescription(this MessageType messageType)
    {
        var field = typeof(MessageType).GetField(messageType.ToString());
        return field?.GetCustomAttribute<DescriptionAttribute>()?.Description;
    }

    /// <summary>
    /// Attempts to parse an HL7 message type / Table 0354 code (e.g. <c>ADT_A01</c>, <c>ACK</c>).
    /// </summary>
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
    /// Prefers MSH-9.3, then synthesizes <c>{MSH-9.1}_{MSH-9.2}</c>, then treats MSH-9.1 alone as a type code.
    /// Returns <see cref="MessageType.Unknown"/> when nothing matches.
    /// </summary>
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
    /// Builds the standard three-component MSH-9 value (<c>Code{sep}Trigger{sep}Structure</c>) for a known type,
    /// using <paramref name="encoding"/>'s component separator.
    /// Returns <see langword="null"/> for <see cref="MessageType.Unknown"/>.
    /// </summary>
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

    private static MessageTypeInfoAttribute GetMessageTypeInfo(MessageType messageType)
    {
        var field = typeof(MessageType).GetField(messageType.ToString())
            ?? throw new ArgumentException($"Unknown message type: {messageType}", nameof(messageType));

        return field.GetCustomAttribute<MessageTypeInfoAttribute>()
            ?? throw new InvalidOperationException(
                $"Message type {messageType} is missing MessageTypeInfoAttribute.");
    }

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
