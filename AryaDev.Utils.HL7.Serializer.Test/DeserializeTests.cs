using System.Text;
using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;
using AryaDev.Utils.HL7.Serializer.Test.Fixtures;

namespace AryaDev.Utils.HL7.Serializer.Test;

public class DeserializeTests
{
    private readonly HL7Serializer _serializer = new();

    [Fact]
    public void Deserialize_OruR01_ParsesSegmentsAndFields()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01);

        message.Segments.Count.ShouldBe(4);
        message.Segments.Select(s => s.Name).ShouldBe(["MSH", "PID", "OBR", "OBX"]);

        message["MSH.10"].ShouldBe("MSG00001");
        message["PID.5.1"].ShouldBe("DOE");
        message["PID.5.2"].ShouldBe("JOHN");
        message["PID.3.1"].ShouldBe("12345");
        message["OBX.5"].ShouldBe("7.2");
        message["OBR.4.1"].ShouldBe("CBC");
    }

    [Fact]
    public void Deserialize_AdtA01_ParsesPatientAndVisit()
    {
        var message = _serializer.Deserialize(SampleMessages.AdtA01);

        message.Segments.Count.ShouldBe(4);
        message["MSH.10"].ShouldBe("MSG00002");
        message["PID.5.1"].ShouldBe("SMITH");
        message["PID.5.2"].ShouldBe("JANE");
        message["PV1.2"].ShouldBe("I");
        message["PV1.3.1"].ShouldBe("2000");
        message["EVN.1"].ShouldBe("A01");
    }

    [Fact]
    public void Deserialize_RdeO11_ParsesOrderFields()
    {
        var message = _serializer.Deserialize(SampleMessages.RdeO11);

        message.Segments.Count.ShouldBe(4);
        message["MSH.10"].ShouldBe("MSG00003");
        message["PID.5.1"].ShouldBe("JONES");
        message["ORC.1"].ShouldBe("NW");
        message["ORC.2"].ShouldBe("ORD1001");
        message["RXE.3"].ShouldBe("500");
        message["RXE.4"].ShouldBe("mg");
    }

    [Fact]
    public void Deserialize_Ack_ParsesAcknowledgement()
    {
        var message = _serializer.Deserialize(SampleMessages.Ack);

        message.Segments.Count.ShouldBe(2);
        message["MSA.1"].ShouldBe("AA");
        message["MSA.2"].ShouldBe("MSG00001");
    }

    [Fact]
    public void Deserialize_OruR01_SetsMessageType()
    {
        _serializer.Deserialize(SampleMessages.OruR01).MessageType.ShouldBe(MessageType.ORU_R01);
    }

    [Fact]
    public void Deserialize_AdtA01_SetsMessageType()
    {
        _serializer.Deserialize(SampleMessages.AdtA01).MessageType.ShouldBe(MessageType.ADT_A01);
    }

    [Fact]
    public void Deserialize_RdeO11_SetsMessageType()
    {
        _serializer.Deserialize(SampleMessages.RdeO11).MessageType.ShouldBe(MessageType.RDE_O11);
    }

    [Fact]
    public void Deserialize_Ack_SetsMessageType()
    {
        _serializer.Deserialize(SampleMessages.Ack).MessageType.ShouldBe(MessageType.ACK);
    }

    [Fact]
    public void Deserialize_TwoPartMsh9_SynthesizesMessageType()
    {
        _serializer.Deserialize(SampleMessages.AdtA01TwoPart).MessageType.ShouldBe(MessageType.ADT_A01);
    }

    [Fact]
    public void Deserialize_StructureOnlyMsh9_ParsesMessageType()
    {
        _serializer.Deserialize(SampleMessages.OruStructureOnly).MessageType.ShouldBe(MessageType.ORU_R01);
    }

    [Fact]
    public void Deserialize_UnknownMsh9_SetsUnknownMessageType()
    {
        _serializer.Deserialize(SampleMessages.UnknownMessageType).MessageType.ShouldBe(MessageType.Unknown);
    }

    [Fact]
    public void Deserialize_DefaultEncoding_WhenMsh2IsStandard()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01);

        message.Encoding.FieldSeparator.ShouldBe('|');
        message.Encoding.ComponentSeparator.ShouldBe('^');
        message.Encoding.RepetitionSeparator.ShouldBe('~');
        message.Encoding.EscapeCharacter.ShouldBe('\\');
        message.Encoding.SubcomponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void Deserialize_AbsentMsh18_DefaultsToAscii()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01);

        message.CharacterSet.ShouldBeNull();
        message.TextEncoding.ShouldBe(Encoding.ASCII);
    }

    [Fact]
    public void Deserialize_Utf8Msh18_ResolvesTextEncoding()
    {
        var message = _serializer.Deserialize(SampleMessages.Utf8Oru);

        message.CharacterSet.ShouldBe("UTF-8");
        message.TextEncoding.WebName.ShouldBe("utf-8");
        message["PID.5.1"].ShouldBe("MÜLLER");
        message["OBX.5"].ShouldBe("café");
    }

    [Fact]
    public void Deserialize_Bytes_Utf8Payload_PreservesNonAscii()
    {
        var bytes = Encoding.UTF8.GetBytes(SampleMessages.Utf8Oru.Replace("\r\n", "\r").Replace("\n", "\r"));

        var message = _serializer.Deserialize(bytes);

        message.CharacterSet.ShouldBe("UTF-8");
        message["PID.5.1"].ShouldBe("MÜLLER");
        message["OBX.5"].ShouldBe("café");
    }

    [Fact]
    public void Deserialize_LfLineEndings_ParsesSameAsCr()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01Lf);

        message.Segments.Count.ShouldBe(3);
        message["PID.5.1"].ShouldBe("DOE");
        message["OBX.5"].ShouldBe("7.2");
        message.MessageType.ShouldBe(MessageType.ORU_R01);
    }

    [Fact]
    public void Deserialize_CrlfLineEndings_ParsesSegments()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01Crlf);

        message.Segments.Count.ShouldBe(3);
        message["PID.5.1"].ShouldBe("DOE");
        message.MessageType.ShouldBe(MessageType.ORU_R01);
    }

    [Fact]
    public void Deserialize_EscapedSeparators_AreDecoded()
    {
        var message = _serializer.Deserialize(SampleMessages.WithEscapes);

        message["NTE.3"].ShouldBe("Note with | pipe and ^ caret");
    }

    [Fact]
    public void Deserialize_SegmentType_ResolvesKnownCodes()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01);
        var pid = message.GetSegments("PID").Single();

        pid.Type.ShouldBe(SegmentType.PatientIdentification);
        pid.Name.ShouldBe("PID");
    }

    [Fact]
    public void Deserialize_FieldRepetitions_AreAccessible()
    {
        var message = _serializer.Deserialize(SampleMessages.PidWithRepetitions);

        message["PID.3"].ShouldContain("ID1");
        message["PID.3~1.1"].ShouldBe("ID1");
        message["PID.3~2.1"].ShouldBe("ID2");
        message["PID.3~1.5"].ShouldBe("MR");
        message["PID.3~2.5"].ShouldBe("SSN");
    }

    [Fact]
    public void Deserialize_CustomComponentSeparator_ParsesComponents()
    {
        var message = _serializer.Deserialize(SampleMessages.CustomComponentSeparator);

        message.Encoding.ComponentSeparator.ShouldBe('~');
        message.MessageType.ShouldBe(MessageType.ADT_A01);
        message["PID.5.1"].ShouldBe("DOE");
        message["PID.5.2"].ShouldBe("JOHN");
    }

    [Fact]
    public void Deserialize_EmptyBytes_ReturnsEmptyMessage()
    {
        var message = _serializer.Deserialize([]);

        message.Segments.Count.ShouldBe(0);
        message.MessageType.ShouldBe(MessageType.Unknown);
    }

    [Fact]
    public void Deserialize_RawMsh9_RemainsAccessible()
    {
        var message = _serializer.Deserialize(SampleMessages.OruR01);

        message["MSH.9"].ShouldBe("ORU^R01^ORU_R01");
        message["MSH.9.1"].ShouldBe("ORU");
        message["MSH.9.2"].ShouldBe("R01");
        message["MSH.9.3"].ShouldBe("ORU_R01");
    }

    [Fact]
    public void Deserialize_HexEscape_DecodesSingleCodePoint()
    {
        var message = _serializer.Deserialize(SampleMessages.WithHexEscapes);

        message["NTE.3"].ShouldBe("Copyright © and letter A");
    }

    [Fact]
    public void Deserialize_MultiHexEscape_DecodesConsecutiveCodePoints()
    {
        var message = _serializer.Deserialize(SampleMessages.WithMultiHexEscapes);

        message["NTE.3"].ShouldBe("Greetings, Hi to you!");
    }

    [Fact]
    public void Deserialize_FieldRepetitions_DefaultRepetitionIsFirst()
    {
        var message = _serializer.Deserialize(SampleMessages.PidWithRepetitions);

        message["PID.3.1"].ShouldBe("ID1");
        message["PID.3.5"].ShouldBe("MR");
    }

    [Fact]
    public void Deserialize_FieldRepetitions_AllRepetitionsAccessibleByIndex()
    {
        var message = _serializer.Deserialize(SampleMessages.FieldRepetitionsExtended);

        message["PID.3~1.1"].ShouldBe("MR1");
        message["PID.3~2.1"].ShouldBe("MR2");
        message["PID.3~3.1"].ShouldBe("SSN1");
        message["PID.3~1.5"].ShouldBe("MR");
        message["PID.3~3.5"].ShouldBe("SSN");

        message["NK1.5~1.1"].ShouldBe("5551111");
        message["NK1.5~1.2"].ShouldBe("PRN");
        message["NK1.5~2.1"].ShouldBe("5552222");
        message["NK1.5~2.2"].ShouldBe("WPN");
    }

    [Fact]
    public void Deserialize_FieldRepetitions_WholeFieldJoinsWithRepetitionSeparator()
    {
        var message = _serializer.Deserialize(SampleMessages.PidWithRepetitions);

        message["PID.3"].ShouldBe("ID1^^^A^MR~ID2^^^B^SSN");
    }

    [Fact]
    public void Deserialize_SegmentRepetitions_AreAccessibleByOccurrence()
    {
        var message = _serializer.Deserialize(SampleMessages.MultipleObxSegments);

        message.GetSegments("OBX").Count().ShouldBe(3);

        message["OBX.5"].ShouldBe("7.2");
        message["OBX[1].5"].ShouldBe("7.2");
        message["OBX[2].5"].ShouldBe("4.8");
        message["OBX[3].5"].ShouldBe("normal");

        message["OBX[1].3.1"].ShouldBe("WBC");
        message["OBX[2].3.1"].ShouldBe("RBC");
        message["OBX[3].2"].ShouldBe("ST");
    }

    [Fact]
    public void Deserialize_SegmentRepetitions_NteCommentsByOccurrence()
    {
        var message = _serializer.Deserialize(SampleMessages.MultipleNteSegments);

        message.GetSegments("NTE").Count().ShouldBe(3);
        message["NTE[1].3"].ShouldBe("First comment");
        message["NTE[2].3"].ShouldBe("Second comment");
        message["NTE[3].3"].ShouldBe("Third comment");
        message["NTE.3"].ShouldBe("First comment");
    }

    [Fact]
    public void Deserialize_SegmentRepetitions_MissingOccurrence_ReturnsNull()
    {
        var message = _serializer.Deserialize(SampleMessages.MultipleObxSegments);

        message["OBX[4].5"].ShouldBeNull();
    }
}
