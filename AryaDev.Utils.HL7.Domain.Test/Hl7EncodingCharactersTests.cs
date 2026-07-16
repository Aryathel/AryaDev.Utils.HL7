using AryaDev.Utils.HL7.Domain.Encoding;

namespace AryaDev.Utils.HL7.Domain.Test;

public class Hl7EncodingCharactersTests
{
    [Fact]
    public void Default_UsesStandardSeparators()
    {
        var encoding = Hl7EncodingCharacters.Default;

        encoding.FieldSeparator.ShouldBe('|');
        encoding.ComponentSeparator.ShouldBe('^');
        encoding.RepetitionSeparator.ShouldBe('~');
        encoding.EscapeCharacter.ShouldBe('\\');
        encoding.SubcomponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void FromMsh2_ParsesStandardEncodingPrefix()
    {
        // Reader passes MSH line after "MSH", beginning with field separator
        var encoding = Hl7EncodingCharacters.FromMsh2("|^~\\&|SENDING|");

        encoding.FieldSeparator.ShouldBe('|');
        encoding.ComponentSeparator.ShouldBe('^');
        encoding.RepetitionSeparator.ShouldBe('~');
        encoding.EscapeCharacter.ShouldBe('\\');
        encoding.SubcomponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void FromMsh2_ParsesCustomComponentSeparator()
    {
        var encoding = Hl7EncodingCharacters.FromMsh2("|~^\\&|APP|");

        encoding.FieldSeparator.ShouldBe('|');
        encoding.ComponentSeparator.ShouldBe('~');
        encoding.RepetitionSeparator.ShouldBe('^');
    }

    [Fact]
    public void FromMsh2_Empty_ReturnsDefaultSeparators()
    {
        var encoding = Hl7EncodingCharacters.FromMsh2("");
        encoding.FieldSeparator.ShouldBe('|');
        encoding.ComponentSeparator.ShouldBe('^');
        encoding.RepetitionSeparator.ShouldBe('~');
        encoding.EscapeCharacter.ShouldBe('\\');
        encoding.SubcomponentSeparator.ShouldBe('&');
    }

    [Fact]
    public void ToMsh2_OmitsFieldSeparator()
    {
        Hl7EncodingCharacters.Default.ToMsh2().ShouldBe("^~\\&");
    }

    [Fact]
    public void ToMsh2_ReflectsCustomSeparators()
    {
        var encoding = new Hl7EncodingCharacters('|', '~', '^', '\\', '&');
        encoding.ToMsh2().ShouldBe("~^\\&");
    }
    
    [Theory]
    [InlineData("MSH|^~\\&|LAB|FAC|EHR|FAC|20260101||ORU^R01|MSG1|P|2.9.", '|', '^', '~', '\\', '&')]
    [InlineData("MSH|^()&|LAB|FAC|EHR|FAC|20260101||ORU^R01|MSG1|P|2.9.", '|', '^', '(', ')', '&')]
    [InlineData("MSH$^~\\&$LAB$FAC$EHR$FAC$20260101$$ORU^R01$MSG1$P$2.9.", '$', '^', '~', '\\', '&')]
    public void TryGetMshEncodingFromLine_CustomEncodingCharacters(string msh, char fieldSeparator, char componentSeparator, char repetitionSeparator, char escapeSeparator, char subcomponentSeparator)
    {
        var ok = Hl7CharacterSet.TryGetMshEncodingFromLine(
            msh,
            out var encoding);

        ok.ShouldBeTrue();
        encoding.FieldSeparator.ShouldBe(fieldSeparator);
        encoding.ComponentSeparator.ShouldBe(componentSeparator);
        encoding.RepetitionSeparator.ShouldBe(repetitionSeparator);
        encoding.EscapeCharacter.ShouldBe(escapeSeparator);
        encoding.SubcomponentSeparator.ShouldBe(subcomponentSeparator);
    }
}
