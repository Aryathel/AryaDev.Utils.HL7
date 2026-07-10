using AryaDev.Utils.HL7.Domain;

namespace AryaDev.Utils.HL7.Serializer;

public class HL7Serializer
{
    public HL7Message Deserialize(string raw) => HL7MessageReader.Read(raw);

    public string Serialize(HL7Message message) => HL7MessageWriter.Write(message);
}
