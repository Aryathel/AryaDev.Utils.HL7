using AryaDev.Utils.HL7.Domain.Enumeration;

namespace AryaDev.Utils.HL7.Domain.Test;

public class SegmentTypeExtensionsTests
{
    [Fact]
    public void GetCode_ReturnsHl7SegmentId()
    {
        SegmentType.PatientIdentification.GetCode().ShouldBe("PID");
        SegmentType.MessageHeader.GetCode().ShouldBe("MSH");
        SegmentType.ObservationResult.GetCode().ShouldBe("OBX");
    }

    [Fact]
    public void GetChapter_ReturnsAttributedChapter()
    {
        SegmentType.PatientIdentification.GetChapter()
            .ShouldBe(SegmentChapter.PatientAdministration);
        SegmentType.MessageHeader.GetChapter()
            .ShouldBe(SegmentChapter.Control);
    }

    [Fact]
    public void GetDescription_ReturnsNonEmptyForKnownSegments()
    {
        SegmentType.PatientIdentification.GetDescription().ShouldNotBeNullOrWhiteSpace();
        SegmentType.MessageHeader.GetDescription().ShouldNotBeNullOrWhiteSpace();
    }

    [Theory]
    [InlineData("PID", SegmentType.PatientIdentification)]
    [InlineData("pid", SegmentType.PatientIdentification)]
    [InlineData("MSH", SegmentType.MessageHeader)]
    [InlineData("OBX", SegmentType.ObservationResult)]
    public void TryParseCode_KnownCodes_Succeed(string code, SegmentType expected)
    {
        SegmentTypeExtensions.TryParseCode(code, out var type).ShouldBeTrue();
        type.ShouldBe(expected);
    }

    [Theory]
    [InlineData("ZAB")]
    [InlineData("zzz")]
    [InlineData("Z99")]
    public void TryParseCode_ZPrefix_MapsToZSegment(string code)
    {
        SegmentTypeExtensions.TryParseCode(code, out var type).ShouldBeTrue();
        type.ShouldBe(SegmentType.ZSegment);
    }

    [Fact]
    public void TryParseCode_Unknown_ReturnsFalse()
    {
        SegmentTypeExtensions.TryParseCode("QQQ", out _).ShouldBeFalse();
    }

    [Fact]
    public void TryParseCode_NullOrWhitespace_Throws()
    {
        Should.Throw<ArgumentException>(() => SegmentTypeExtensions.TryParseCode(" ", out _));
        Should.Throw<ArgumentException>(() => SegmentTypeExtensions.TryParseCode(null!, out _));
    }

    [Fact]
    public void GetByChapter_Control_IncludesMsh()
    {
        var control = SegmentTypeExtensions.GetByChapter(SegmentChapter.Control).ToList();
        control.ShouldContain(SegmentType.MessageHeader);
        control.ShouldContain(SegmentType.MessageAcknowledgement);
    }

    [Fact]
    public void GetByChapter_PatientAdministration_IncludesPid()
    {
        SegmentTypeExtensions.GetByChapter(SegmentChapter.PatientAdministration)
            .ShouldContain(SegmentType.PatientIdentification);
    }
}
