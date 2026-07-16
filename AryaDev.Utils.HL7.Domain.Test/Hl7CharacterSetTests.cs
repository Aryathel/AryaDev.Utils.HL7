using AryaDev.Utils.HL7.Domain.Encoding;
using TextEnc = System.Text.Encoding;

namespace AryaDev.Utils.HL7.Domain.Test;

public class Hl7CharacterSetTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetEncoding_NullOrEmpty_ReturnsAscii(string? msh18)
    {
        Hl7CharacterSet.GetEncoding(msh18).ShouldBe(TextEnc.ASCII);
    }

    [Theory]
    [InlineData("ASCII")]
    [InlineData("ISO IR 6")]
    [InlineData("ascii")]
    public void GetEncoding_AsciiAliases_ReturnAscii(string msh18)
    {
        Hl7CharacterSet.GetEncoding(msh18).ShouldBe(TextEnc.ASCII);
    }

    [Theory]
    [InlineData("UTF-8")]
    [InlineData("ISO IR 192")]
    public void GetEncoding_Utf8Aliases_ReturnUtf8(string msh18)
    {
        Hl7CharacterSet.GetEncoding(msh18).WebName.ShouldBe("utf-8");
    }

    [Theory]
    [InlineData("8859/1")]
    [InlineData("ISO IR 100")]
    [InlineData("ISO8859-1")]
    [InlineData("LATIN-1")]
    public void GetEncoding_Latin1Aliases_ReturnLatin1(string msh18)
    {
        Hl7CharacterSet.GetEncoding(msh18).ShouldBe(TextEnc.Latin1);
    }

    [Theory]
    [InlineData("UTF-16")]
    [InlineData("UNICODE")]
    [InlineData("UNICODE UTF-16")]
    public void GetEncoding_Utf16Aliases_ReturnUnicode(string msh18)
    {
        Hl7CharacterSet.GetEncoding(msh18).ShouldBe(TextEnc.Unicode);
    }

    [Fact]
    public void GetEncoding_MultiComponentMsh18_UsesPrimary()
    {
        Hl7CharacterSet.GetEncoding("UTF-8^8859/1").WebName.ShouldBe("utf-8");
    }

    [Fact]
    public void GetEncoding_UnknownCharset_FallsBackToAscii()
    {
        Hl7CharacterSet.GetEncoding("NOT-A-REAL-CHARSET").ShouldBe(TextEnc.ASCII);
    }

    [Fact]
    public void TryGetMshEncodingFromLine_ValidMsh_ReturnsTrue()
    {
        var ok = Hl7CharacterSet.TryGetMshEncodingFromLine(
            "MSH|^~\\&|LAB|FAC|EHR|FAC|20260101||ORU^R01|MSG1|P|2.9.",
            out var encoding);

        ok.ShouldBeTrue();
        encoding.FieldSeparator.ShouldBe('|');
        encoding.ComponentSeparator.ShouldBe('^');
        encoding.RepetitionSeparator.ShouldBe('~');
        encoding.EscapeCharacter.ShouldBe('\\');
        encoding.SubcomponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void TryGetMshEncodingFromLine_NonMsh_ReturnsFalse()
    {
        Hl7CharacterSet.TryGetMshEncodingFromLine("PID|1||123", out _).ShouldBeFalse();
    }

    [Fact]
    public void ExtractMsh18FromLine_WhenPresent_ReturnsValue()
    {
        var line = "MSH|^~\\&|LAB|FAC|EHR|FAC|20260101||ORU^R01|MSG1|P|2.9.1||||||UTF-8";
        Hl7CharacterSet.ExtractMsh18FromLine(line).ShouldBe("UTF-8");
    }

    [Fact]
    public void ExtractMsh18FromLine_WhenAbsent_ReturnsNull()
    {
        var line = "MSH|^~\\&|LAB|FAC|EHR|FAC|20260101||ORU^R01|MSG1|P|2.9.1";
        Hl7CharacterSet.ExtractMsh18FromLine(line).ShouldBeNull();
    }
}
