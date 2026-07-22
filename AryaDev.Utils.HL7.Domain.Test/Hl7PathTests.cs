using AryaDev.Utils.HL7.Domain.Model;

namespace AryaDev.Utils.HL7.Domain.Test;

public class Hl7PathTests
{
    [Theory]
    [InlineData("PID", "PID", 1, null, null, null, null)]
    [InlineData("pid", "PID", 1, null, null, null, null)]
    [InlineData("PID.5", "PID", 1, 5, null, null, null)]
    [InlineData("PID.5.1", "PID", 1, 5, null, 1, null)]
    [InlineData("PID.5.1.2", "PID", 1, 5, null, 1, 2)]
    [InlineData("PID[2].5", "PID", 2, 5, null, null, null)]
    [InlineData("PID.3~2", "PID", 1, 3, 2, null, null)]
    [InlineData("PID.3~2.1", "PID", 1, 3, 2, 1, null)]
    [InlineData("OBX[3].5~2.1.1", "OBX", 3, 5, 2, 1, 1)]
    [InlineData("MSH.9.3", "MSH", 1, 9, null, 3, null)]
    public void Parse_ValidPaths_ReturnsExpectedParts(
        string path,
        string segment,
        int occurrence,
        int? field,
        int? repetition,
        int? component,
        int? subcomponent)
    {
        var parsed = Hl7Path.Parse(path);

        parsed.SegmentName.ShouldBe(segment);
        parsed.SegmentOccurrence.ShouldBe(occurrence);
        parsed.Field.ShouldBe(field);
        parsed.Repetition.ShouldBe(repetition);
        parsed.Component.ShouldBe(component);
        parsed.Subcomponent.ShouldBe(subcomponent);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("PI")]
    [InlineData("PIDX")]
    [InlineData("PID.0")]
    [InlineData("PID[0].5")]
    [InlineData("PID.5~0")]
    [InlineData("PID.5.0")]
    [InlineData("PID.5.1.0")]
    [InlineData("PID..5")]
    [InlineData("123.1")]
    public void TryParse_InvalidPaths_ReturnsFalse(string path)
    {
        Hl7Path.TryParse(path, out var result).ShouldBeFalse();
        result.ShouldBeNull();
    }

    [Fact]
    public void Parse_InvalidPath_ThrowsFormatException()
    {
        Should.Throw<FormatException>(() => Hl7Path.Parse("PID.0"));
    }

    [Theory]
    [InlineData("PI")]
    [InlineData("   ")]
    [InlineData("123")]
    public void Constructor_RejectsInvalidSegmentName(string segmentName)
    {
        Should.Throw<ArgumentException>(() => new Hl7Path(segmentName));
    }

    [Fact]
    public void Constructor_RejectsNonPositiveOccurrenceOrRepetition()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new Hl7Path("PID", segmentOccurrence: 0));
        Should.Throw<ArgumentOutOfRangeException>(() => new Hl7Path("PID", repetition: 0));
    }
}
