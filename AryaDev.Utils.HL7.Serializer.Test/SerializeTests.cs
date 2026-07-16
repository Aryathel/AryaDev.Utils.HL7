using System.Text;
using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Serializer.Test;

public class SerializeTests
{
    private readonly HL7Serializer _serializer = new();

    [Fact]
    public void Serialize_BuiltMessage_ProducesPipeDelimitedCrSeparatedOutput()
    {
        var message = new HL7Message();
        message["MSH.3"] = "SENDING";
        message["MSH.4"] = "FACILITY";
        message["MSH.9"] = "ORU^R01^ORU_R01";
        message["MSH.10"] = "MSG100";
        message["MSH.11"] = "P";
        message["MSH.12"] = "2.9.1";
        message["PID.5.1"] = "DOE";
        message["PID.5.2"] = "JOHN";
        message["OBX.5"] = "7.2";

        var output = _serializer.Serialize(message);

        output.ShouldContain("MSH|");
        output.ShouldContain("|ORU^R01^ORU_R01|");
        output.ShouldContain("|MSG100|");
        output.ShouldContain("PID|");
        output.ShouldContain("DOE^JOHN");
        output.ShouldContain("OBX|");
        output.ShouldContain("7.2");
        output.Split('\r').Length.ShouldBe(3);
        output.ShouldNotContain("\n");
    }

    [Fact]
    public void Serialize_MessageType_FillsMsh9WhenUnset()
    {
        var message = new HL7Message { MessageType = MessageType.ADT_A01 };
        message["MSH.3"] = "ADT";
        message["MSH.10"] = "MSG200";

        var output = _serializer.Serialize(message);

        output.ShouldContain("|ADT^A01^ADT_A01|");
        message["MSH.9"].ShouldBe("ADT^A01^ADT_A01");
    }

    [Fact]
    public void Serialize_MessageType_DoesNotOverwriteExistingMsh9()
    {
        var message = new HL7Message { MessageType = MessageType.ADT_A01 };
        message["MSH.3"] = "LAB";
        message["MSH.9"] = "ORU^R01";
        message["MSH.10"] = "MSG201";

        var output = _serializer.Serialize(message);

        output.ShouldContain("|ORU^R01|");
        output.ShouldNotContain("ADT^A01^ADT_A01");
        message["MSH.9"].ShouldBe("ORU^R01");
    }

    [Fact]
    public void Serialize_UnknownMessageType_DoesNotInventMsh9()
    {
        var message = new HL7Message { MessageType = MessageType.Unknown };
        message.TextEncoding = Encoding.ASCII;
        message["MSH.3"] = "APP";
        message["MSH.10"] = "MSG202";

        var output = _serializer.Serialize(message);

        // MSH.9 should remain empty — control id is still present
        output.ShouldContain("|MSG202|");
        string.IsNullOrWhiteSpace(message["MSH.9"]).ShouldBeTrue();
    }

    [Fact]
    public void Serialize_MessageType_UsesCustomComponentSeparator()
    {
        var encoding = new Hl7EncodingCharacters('|', '~', '^', '\\', '&');
        var message = new HL7Message(encoding: encoding) { MessageType = MessageType.RDE_O11 };
        message["MSH.3"] = "PHARM";
        message["MSH.10"] = "MSG203";

        var output = _serializer.Serialize(message);

        output.ShouldContain("|RDE~O11~RDE_O11|");
        message["MSH.9"].ShouldBe("RDE~O11~RDE_O11");
    }

    [Fact]
    public void Serialize_AckMessageType_WritesSingleTokenStructure()
    {
        var message = new HL7Message { MessageType = MessageType.ACK };
        message["MSH.3"] = "EHR";
        message["MSH.10"] = "MSG204";

        var output = _serializer.Serialize(message);

        output.ShouldContain("|ACK^^ACK|");
    }

    [Fact]
    public void Serialize_EscapesSeparatorsInFieldValues()
    {
        var message = new HL7Message();
        message["MSH.3"] = "APP";
        message["MSH.9"] = "ORU^R01^ORU_R01";
        message["MSH.10"] = "MSG205";
        message.SetRaw("NTE.3", "Note with | pipe and ^ caret");

        var output = _serializer.Serialize(message);

        output.ShouldContain(@"Note with \F\ pipe and \S\ caret");
    }

    [Fact]
    public void SerializeBytes_UsesMessageTextEncoding()
    {
        var message = new HL7Message(
            characterSet: "UTF-8",
            textEncoding: Encoding.UTF8)
        {
            MessageType = MessageType.ORU_R01
        };
        message["MSH.3"] = "LAB";
        message["MSH.10"] = "MSG206";
        message["MSH.18"] = "UTF-8";
        message["PID.5.1"] = "MÜLLER";

        var bytes = _serializer.SerializeBytes(message);
        var decoded = Encoding.UTF8.GetString(bytes);

        decoded.ShouldContain("MÜLLER");
        decoded.ShouldContain("UTF-8");
        message.TextEncoding.ShouldBe(Encoding.UTF8);
    }

    [Fact]
    public void Serialize_WritesMshEncodingCharactersAsField2()
    {
        var message = new HL7Message();
        message["MSH.3"] = "APP";
        message["MSH.9"] = "ACK^^ACK";
        message["MSH.10"] = "MSG207";

        var output = _serializer.Serialize(message);
        var msh = output.Split('\r')[0];

        msh.ShouldStartWith(@"MSH|^~\&|");
    }
}
