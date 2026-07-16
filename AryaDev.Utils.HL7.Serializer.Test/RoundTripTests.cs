using AryaDev.Utils.HL7.Domain.Enumeration;
using AryaDev.Utils.HL7.Serializer.Test.Fixtures;

namespace AryaDev.Utils.HL7.Serializer.Test;

public class RoundTripTests
{
    private readonly HL7Serializer _serializer = new();

    [Fact]
    public void RoundTrip_OruR01_PreservesKeyFieldsAndMessageType()
    {
        var original = _serializer.Deserialize(SampleMessages.OruR01);
        var wire = _serializer.Serialize(original);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped.MessageType.ShouldBe(MessageType.ORU_R01);
        roundTripped.Segments.Count.ShouldBe(original.Segments.Count);
        roundTripped["MSH.10"].ShouldBe("MSG00001");
        roundTripped["PID.5.1"].ShouldBe("DOE");
        roundTripped["PID.5.2"].ShouldBe("JOHN");
        roundTripped["OBX.5"].ShouldBe("7.2");
        roundTripped["OBR.4.1"].ShouldBe("CBC");
        roundTripped["MSH.9"].ShouldBe("ORU^R01^ORU_R01");
    }

    [Fact]
    public void RoundTrip_AdtA01_PreservesKeyFieldsAndMessageType()
    {
        var original = _serializer.Deserialize(SampleMessages.AdtA01);
        var wire = _serializer.Serialize(original);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped.MessageType.ShouldBe(MessageType.ADT_A01);
        roundTripped["MSH.10"].ShouldBe("MSG00002");
        roundTripped["PID.5.1"].ShouldBe("SMITH");
        roundTripped["PV1.2"].ShouldBe("I");
        roundTripped["EVN.1"].ShouldBe("A01");
        roundTripped["MSH.9"].ShouldBe("ADT^A01^ADT_A01");
    }

    [Fact]
    public void RoundTrip_RdeO11_PreservesKeyFieldsAndMessageType()
    {
        var original = _serializer.Deserialize(SampleMessages.RdeO11);
        var wire = _serializer.Serialize(original);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped.MessageType.ShouldBe(MessageType.RDE_O11);
        roundTripped["MSH.10"].ShouldBe("MSG00003");
        roundTripped["PID.5.1"].ShouldBe("JONES");
        roundTripped["ORC.2"].ShouldBe("ORD1001");
        roundTripped["RXE.3"].ShouldBe("500");
        roundTripped["MSH.9"].ShouldBe("RDE^O11^RDE_O11");
    }

    [Fact]
    public void RoundTrip_Ack_PreservesAcknowledgement()
    {
        var original = _serializer.Deserialize(SampleMessages.Ack);
        var wire = _serializer.Serialize(original);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped.MessageType.ShouldBe(MessageType.ACK);
        roundTripped["MSA.1"].ShouldBe("AA");
        roundTripped["MSA.2"].ShouldBe("MSG00001");
    }

    [Fact]
    public void RoundTrip_AfterMutatingObx_PreservesChangeAndOtherFields()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01);
        message["OBX.5"] = "99.1";

        var wire = _serializer.Serialize(message);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped["OBX.5"].ShouldBe("99.1");
        roundTripped["PID.5.1"].ShouldBe("DOE");
        roundTripped["MSH.10"].ShouldBe("MSG00001");
        roundTripped.MessageType.ShouldBe(MessageType.ORU_R01);
    }

    [Fact]
    public void RoundTrip_AfterMutatingPid_PreservesChange()
    {
        var message = _serializer.Deserialize(SampleMessages.AdtA01);
        message["PID.5.1"] = "JOHNSON";

        var wire = _serializer.Serialize(message);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped["PID.5.1"].ShouldBe("JOHNSON");
        roundTripped["PID.5.2"].ShouldBe("JANE");
        roundTripped["PV1.3.1"].ShouldBe("2000");
        roundTripped.MessageType.ShouldBe(MessageType.ADT_A01);
    }

    [Fact]
    public void RoundTrip_EscapedValues_SurviveSerializeDeserialize()
    {
        var original = _serializer.Deserialize(SampleMessages.WithEscapes);
        var wire = _serializer.Serialize(original);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped["NTE.3"].ShouldBe("Note with | pipe and ^ caret");
        wire.ShouldContain(@"\F\");
        wire.ShouldContain(@"\S\");
    }

    [Fact]
    public void RoundTrip_CustomComponentSeparator_RemainsConsistent()
    {
        var original = _serializer.Deserialize(SampleMessages.CustomComponentSeparator);
        var wire = _serializer.Serialize(original);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped.Encoding.ComponentSeparator.ShouldBe('~');
        roundTripped.MessageType.ShouldBe(MessageType.ADT_A01);
        roundTripped["PID.5.1"].ShouldBe("DOE");
        roundTripped["PID.5.2"].ShouldBe("JOHN");
        roundTripped["MSH.9"].ShouldBe("ADT~A01~ADT_A01");
    }

    [Fact]
    public void RoundTrip_Repetitions_SurviveSerializeDeserialize()
    {
        var original = _serializer.Deserialize(SampleMessages.PidWithRepetitions);
        var wire = _serializer.Serialize(original);
        var roundTripped = _serializer.Deserialize(wire);

        roundTripped["PID.3~1.1"].ShouldBe("ID1");
        roundTripped["PID.3~2.1"].ShouldBe("ID2");
        roundTripped["PID.3~2.5"].ShouldBe("SSN");
    }

    [Fact]
    public void RoundTrip_ChangingMessageType_DoesNotOverrideExistingMsh9()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01);
        message.MessageType = MessageType.ADT_A01;

        var wire = _serializer.Serialize(message);
        var roundTripped = _serializer.Deserialize(wire);

        // Manual/existing MSH.9 from original message wins
        roundTripped["MSH.9"].ShouldBe("ORU^R01^ORU_R01");
        roundTripped.MessageType.ShouldBe(MessageType.ORU_R01);
    }

    [Fact]
    public void RoundTrip_Bytes_Utf8_PreservesNonAscii()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(
            SampleMessages.Utf8Oru.Replace("\r\n", "\r").Replace("\n", "\r"));

        var original = _serializer.Deserialize(bytes);
        var outBytes = _serializer.SerializeBytes(original);
        var roundTripped = _serializer.Deserialize(outBytes);

        roundTripped["PID.5.1"].ShouldBe("MÜLLER");
        roundTripped["OBX.5"].ShouldBe("café");
        roundTripped.MessageType.ShouldBe(MessageType.ORU_R01);
    }
}
