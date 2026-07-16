using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Domain.Test;

public class MessageTypeExtensionsTests
{
    [Theory]
    [InlineData("ADT_A01", MessageType.ADT_A01)]
    [InlineData("adt_a01", MessageType.ADT_A01)]
    [InlineData("ORU_R01", MessageType.ORU_R01)]
    [InlineData("RDE_O11", MessageType.RDE_O11)]
    [InlineData("ACK", MessageType.ACK)]
    public void TryParseCode_KnownCodes_Succeed(string code, MessageType expected)
    {
        MessageTypeExtensions.TryParseCode(code, out var type).ShouldBeTrue();
        type.ShouldBe(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("ZZZ_Z99")]
    public void TryParseCode_UnknownOrEmpty_ReturnsFalse(string? code)
    {
        MessageTypeExtensions.TryParseCode(code, out var type).ShouldBeFalse();
        type.ShouldBe(MessageType.Unknown);
    }

    [Fact]
    public void ParseFromMsh9_PrefersStructureComponent()
    {
        MessageTypeExtensions.ParseFromMsh9("ADT", "A04", "ADT_A01")
            .ShouldBe(MessageType.ADT_A01);
    }

    [Fact]
    public void ParseFromMsh9_SynthesizesFromCodeAndTrigger()
    {
        MessageTypeExtensions.ParseFromMsh9("ORU", "R01", null)
            .ShouldBe(MessageType.ORU_R01);
        MessageTypeExtensions.ParseFromMsh9("ADT", "A01", "")
            .ShouldBe(MessageType.ADT_A01);
    }

    [Fact]
    public void ParseFromMsh9_CodeAlone_WhenStructureCode()
    {
        MessageTypeExtensions.ParseFromMsh9("ACK", null, null)
            .ShouldBe(MessageType.ACK);
        MessageTypeExtensions.ParseFromMsh9("ORU_R01", null, string.Empty)
            .ShouldBe(MessageType.ORU_R01);
    }

    [Fact]
    public void ParseFromMsh9_Unrecognized_ReturnsUnknown()
    {
        MessageTypeExtensions.ParseFromMsh9("ZZZ", "Z99", "ZZZ_Z99")
            .ShouldBe(MessageType.Unknown);
        MessageTypeExtensions.ParseFromMsh9(null, null, null)
            .ShouldBe(MessageType.Unknown);
    }

    [Fact]
    public void ToMsh9Value_DefaultEncoding_BuildsThreeComponents()
    {
        MessageType.ADT_A01.ToMsh9Value(Hl7EncodingCharacters.Default)
            .ShouldBe("ADT^A01^ADT_A01");
        MessageType.RDE_O11.ToMsh9Value(Hl7EncodingCharacters.Default)
            .ShouldBe("RDE^O11^RDE_O11");
    }

    [Fact]
    public void ToMsh9Value_Ack_UsesEmptyTrigger()
    {
        MessageType.ACK.ToMsh9Value(Hl7EncodingCharacters.Default)
            .ShouldBe("ACK^^ACK");
    }

    [Fact]
    public void ToMsh9Value_CustomComponentSeparator()
    {
        var encoding = new Hl7EncodingCharacters('|', '~', '^', '\\', '&');
        MessageType.ORU_R01.ToMsh9Value(encoding).ShouldBe("ORU~R01~ORU_R01");
    }

    [Fact]
    public void ToMsh9Value_Unknown_ReturnsNull()
    {
        MessageType.Unknown.ToMsh9Value(Hl7EncodingCharacters.Default).ShouldBeNull();
    }

    [Fact]
    public void ToMsh9Value_NullEncoding_Throws()
    {
        Should.Throw<ArgumentNullException>(() =>
            MessageType.ADT_A01.ToMsh9Value(null!));
    }

    [Fact]
    public void GetCode_ReturnsTable0354Code()
    {
        MessageType.ADT_A01.GetCode().ShouldBe("ADT_A01");
        MessageType.ACK.GetCode().ShouldBe("ACK");
        MessageType.Unknown.GetCode().ShouldBe("");
    }

    [Fact]
    public void GetDescription_ReturnsAttributedText()
    {
        MessageType.ADT_A01.GetDescription().ShouldContain("A01");
        MessageType.Unknown.GetDescription().ShouldNotBeNullOrWhiteSpace();
    }
}
