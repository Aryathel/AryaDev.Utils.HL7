using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;
using TextEnc = System.Text.Encoding;

namespace AryaDev.Utils.HL7.Domain.Test;

public class HL7MessageTests
{
    [Fact]
    public void Constructor_Defaults_EmptySegmentsAndDefaultEncoding()
    {
        var message = new HL7Message();

        message.Segments.Count.ShouldBe(0);
        message.Encoding.FieldSeparator.ShouldBe('|');
        message.MessageType.ShouldBe(MessageType.Unknown);
        message.TextEncoding.ShouldBe(TextEnc.ASCII);
    }

    [Fact]
    public void Indexer_Set_CreatesSegmentAndField()
    {
        var message = new HL7Message();
        message["PID.5.1"] = "DOE";
        message["PID.5.2"] = "JOHN";

        message.Segments.Count.ShouldBe(1);
        message.Segments[0].Name.ShouldBe("PID");
        message["PID.5.1"].ShouldBe("DOE");
        message["PID.5.2"].ShouldBe("JOHN");
        message["PID.5"].ShouldBe("DOE^JOHN");
    }

    [Fact]
    public void Indexer_Set_CreatesSegmentOccurrences()
    {
        var message = new HL7Message();
        message["OBX[1].5"] = "7.2";
        message["OBX[2].5"] = "4.8";
        message["OBX[3].5"] = "normal";

        message.GetSegments("OBX").Count().ShouldBe(3);
        message["OBX.5"].ShouldBe("7.2");
        message["OBX[2].5"].ShouldBe("4.8");
        message["OBX[3].5"].ShouldBe("normal");
    }

    [Fact]
    public void Indexer_Get_MissingPath_ReturnsNull()
    {
        var message = new HL7Message();
        message["PID.5.1"] = "DOE";

        message["OBX.5"].ShouldBeNull();
        message["PID[2].5.1"].ShouldBeNull();
    }

    [Fact]
    public void GetSegments_FiltersByNameCaseInsensitive()
    {
        var message = new HL7Message();
        message["PID.5.1"] = "DOE";
        message["OBX.5"] = "1";
        message["obx[2].5"] = "2";

        message.GetSegments("pid").Select(s => s.Name).ShouldBe(["PID"]);
        message.GetSegments("OBX").Count().ShouldBe(2);
    }

    [Fact]
    public void MessageType_IsStoredIndependentlyOfMsh9()
    {
        var message = new HL7Message { MessageType = MessageType.ADT_A01 };
        message["MSH.9"] = "ORU^R01^ORU_R01";

        message.MessageType.ShouldBe(MessageType.ADT_A01);
        message["MSH.9"].ShouldBe("ORU^R01^ORU_R01");

        message.MessageType = MessageType.RDE_O11;
        message["MSH.9"].ShouldBe("ORU^R01^ORU_R01");
        message.MessageType.ShouldBe(MessageType.RDE_O11);
    }

    [Fact]
    public void Indexer_UsesMessageEncodingForComponents()
    {
        var encoding = new Hl7EncodingCharacters('|', '~', '^', '\\', '&');
        var message = new HL7Message(encoding: encoding);
        message["PID.5.1"] = "DOE";
        message["PID.5.2"] = "JOHN";

        message["PID.5"].ShouldBe("DOE~JOHN");
    }

    [Fact]
    public void CharacterSet_ConstructorResolvesTextEncoding()
    {
        var message = new HL7Message(characterSet: "UTF-8");
        message.CharacterSet.ShouldBe("UTF-8");
        message.TextEncoding.WebName.ShouldBe("utf-8");
    }

    [Fact]
    public void Indexer_Set_FieldRepetitions()
    {
        var message = new HL7Message();
        message["PID.3~1.1"] = "ID1";
        message["PID.3~2.1"] = "ID2";

        message["PID.3~1.1"].ShouldBe("ID1");
        message["PID.3~2.1"].ShouldBe("ID2");
        message["PID.3"].ShouldBe("ID1~ID2");
    }
}
