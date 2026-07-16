using AryaDev.Utils.HL7.Domain.Encoding;

namespace AryaDev.Utils.HL7.Domain.Test;

public class Hl7EscapeTests
{
    private static readonly Hl7EncodingCharacters Default = Hl7EncodingCharacters.Default;

    [Theory]
    [InlineData("|", @"\F\")]
    [InlineData("^", @"\S\")]
    [InlineData("&", @"\T\")]
    [InlineData("~", @"\R\")]
    [InlineData("\\", @"\E\")]
    public void Encode_EscapesSeparators(string input, string expected)
    {
        Hl7Escape.Encode(input, Default).ShouldBe(expected);
    }

    [Theory]
    [InlineData(@"\F\", "|")]
    [InlineData(@"\S\", "^")]
    [InlineData(@"\T\", "&")]
    [InlineData(@"\R\", "~")]
    [InlineData(@"\E\", "\\")]
    public void Decode_UnescapesSeparators(string input, string expected)
    {
        Hl7Escape.Decode(input, Default).ShouldBe(expected);
    }

    [Fact]
    public void EncodeDecode_RoundTripsMixedTextWithSeparators()
    {
        const string original = "A|B^C~D&E\\F";
        var encoded = Hl7Escape.Encode(original, Default);
        Hl7Escape.Decode(encoded, Default).ShouldBe(original);
    }

    [Fact]
    public void Decode_HexEscape_SingleCodePoint()
    {
        Hl7Escape.Decode(@"Letter \X0041\", Default).ShouldBe("Letter A");
        Hl7Escape.Decode(@"Copyright \X00A9\", Default).ShouldBe("Copyright ©");
    }

    [Fact]
    public void Decode_MultiHexEscape_ConsecutiveCodePoints()
    {
        Hl7Escape.Decode(@"Hi \Z00480069\ Hi", Default).ShouldBe("Hi Hi Hi");
        Hl7Escape.Decode(@"\Z004800A9\", Default).ShouldBe("H©");
    }

    [Fact]
    public void EncodeDecode_UsesCustomEncodingCharacters()
    {
        var encoding = new Hl7EncodingCharacters('|', '~', '^', '\\', '&');
        var encoded = Hl7Escape.Encode("A~B", encoding);
        encoded.ShouldBe(@"A\S\B");
        Hl7Escape.Decode(encoded, encoding).ShouldBe("A~B");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Decode_NullOrEmpty_ReturnsInput(string? input)
    {
        Hl7Escape.Decode(input!, Default).ShouldBe(input);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Encode_NullOrEmpty_ReturnsInput(string? input)
    {
        Hl7Escape.Encode(input!, Default).ShouldBe(input);
    }

    [Fact]
    public void Decode_PlainTextWithoutEscapes_Unchanged()
    {
        Hl7Escape.Decode("plain text", Default).ShouldBe("plain text");
    }
}
