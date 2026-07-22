using AryaDev.Utils.HL7.Domain;

namespace AryaDev.Utils.HL7.Serializer;

/// <summary>
/// Entry point for deserializing and serializing HL7 v2 pipe-delimited messages.
/// </summary>
public class HL7Serializer
{
    /// <summary>
    /// Deserializes a message from a string.
    /// </summary>
    /// <param name="raw">Raw HL7 message text.</param>
    /// <returns>Parsed <see cref="HL7Message"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="raw"/> is null or whitespace.</exception>
    public HL7Message Deserialize(string raw) => HL7MessageReader.Read(raw);

    /// <summary>
    /// Deserializes a message from bytes, resolving character set from MSH-18.
    /// </summary>
    /// <param name="raw">Raw HL7 message bytes.</param>
    /// <returns>Parsed <see cref="HL7Message"/> (empty when <paramref name="raw"/> is empty).</returns>
    /// <remarks>
    /// The first segment line is read as ASCII to locate MSH-18; the full buffer is then decoded
    /// with the resolved encoding (ASCII when MSH-18 is absent).
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="raw"/> is null.</exception>
    public HL7Message Deserialize(byte[] raw) => HL7MessageReader.Read(raw);

    /// <summary>
    /// Serializes a message to pipe-delimited text (segments separated by CR).
    /// </summary>
    /// <param name="message">Message to write.</param>
    /// <returns>HL7 message string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="message"/> is null.</exception>
    public string Serialize(HL7Message message) => HL7MessageWriter.Write(message);

    /// <summary>
    /// Serializes a message to bytes using <see cref="HL7Message.TextEncoding"/>.
    /// </summary>
    /// <param name="message">Message to write.</param>
    /// <returns>Encoded message bytes.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="message"/> is null.</exception>
    public byte[] SerializeBytes(HL7Message message) => HL7MessageWriter.WriteBytes(message);
}
