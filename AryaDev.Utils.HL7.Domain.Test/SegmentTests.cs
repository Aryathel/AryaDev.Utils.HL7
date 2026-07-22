using AryaDev.Utils.HL7.Domain.Encoding;
using AryaDev.Utils.HL7.Domain.Enumeration;
using AryaDev.Utils.HL7.Domain.Model;

namespace AryaDev.Utils.HL7.Domain.Test;

public class SegmentTests
{
    private static readonly Hl7EncodingCharacters Encoding = Hl7EncodingCharacters.Default;

    [Fact]
    public void Constructor_NormalizesNameToUpper()
    {
        var segment = new Segment("pid");
        segment.Name.ShouldBe("PID");
    }

    [Theory]
    [InlineData("")]
    [InlineData("PI")]
    [InlineData("PIDX")]
    [InlineData("123")]
    public void Constructor_RejectsInvalidName(string name)
    {
        Should.Throw<ArgumentException>(() => new Segment(name));
    }

    [Theory]
    [InlineData("PID", SegmentType.PatientIdentification)]
    [InlineData("MSH", SegmentType.MessageHeader)]
    [InlineData("OBX", SegmentType.ObservationResult)]
    [InlineData("D12", null)]
    public void Type_ResolvesKnownSegmentCodes(string segmentName, SegmentType? segmentType)
    {
        new Segment(segmentName).Type.ShouldBe(segmentType);
    }

    [Fact]
    public void Type_ZSegment_MapsToZSegment()
    {
        new Segment("ZAB").Type.ShouldBe(SegmentType.ZSegment);
        new Segment("ZZZ").Type.ShouldBe(SegmentType.ZSegment);
    }

    [Fact]
    public void SetValue_GetValue_FieldLevel()
    {
        var segment = new Segment("PID");
        var path = Hl7Path.Parse("PID.5");
        segment.SetValue(path, "DOE^JOHN", Encoding);

        segment.GetValue(path, Encoding).ShouldBe("DOE^JOHN");
    }

    [Fact]
    public void SetValue_GetValue_ComponentAndSubcomponent()
    {
        var segment = new Segment("PID");
        segment.SetValue("PID.5.1", "DOE", Encoding);
        segment.SetValue("PID.5.2", "JOHN", Encoding);
        segment.SetValue("PID.3.1.1", "ID1", Encoding);

        segment.GetValue("PID.5.1", Encoding).ShouldBe("DOE");
        segment.GetValue("PID.5.2", Encoding).ShouldBe("JOHN");
        segment.GetValue("PID.5", Encoding).ShouldBe("DOE^JOHN");
        segment.GetValue("PID.3.1.1", Encoding).ShouldBe("ID1");
    }

    [Fact]
    public void SetValue_GetValue_FieldRepetition()
    {
        var segment = new Segment("PID");
        segment.SetValue("PID.3~1.1", "ID1", Encoding);
        segment.SetValue("PID.3~2.1", "ID2", Encoding);

        segment.GetValue("PID.3~1.1", Encoding).ShouldBe("ID1");
        segment.GetValue("PID.3~2.1", Encoding).ShouldBe("ID2");
        segment.GetValue("PID.3", Encoding).ShouldBe("ID1~ID2");
        segment.GetValue("PID.3~1", Encoding).ShouldBe("ID1");
        segment.GetValue("PID.3~2", Encoding).ShouldBe("ID2");
    }

    [Fact]
    public void GetValue_FieldWithoutRepetition_ReturnsAllRepetitions()
    {
        var segment = new Segment("PID");
        segment.SetFieldFromRaw(3, "ID1^^^A^MR~ID2^^^B^SSN", Encoding);

        segment.GetValue("PID.3", Encoding).ShouldBe("ID1^^^A^MR~ID2^^^B^SSN");
    }

    [Fact]
    public void GetValue_FieldWithExplicitRepetition_ReturnsOnlyThatRepetition()
    {
        var segment = new Segment("PID");
        segment.SetFieldFromRaw(3, "ID1^^^A^MR~ID2^^^B^SSN", Encoding);

        segment.GetValue("PID.3~1", Encoding).ShouldBe("ID1^^^A^MR");
        segment.GetValue("PID.3~2", Encoding).ShouldBe("ID2^^^B^SSN");
        segment.GetValue("PID.3~3", Encoding).ShouldBeNull();
    }

    [Fact]
    public void SetValue_WithoutRepetition_StillTargetsFirstRepetition()
    {
        var segment = new Segment("PID");
        segment.SetFieldFromRaw(3, "ID1^^^A^MR~ID2^^^B^SSN", Encoding);

        segment.SetValue("PID.3.1", "NEWID", Encoding);

        segment.GetValue("PID.3~1.1", Encoding).ShouldBe("NEWID");
        segment.GetValue("PID.3~2.1", Encoding).ShouldBe("ID2");
        segment.GetValue("PID.3", Encoding).ShouldBe("NEWID^^^A^MR~ID2^^^B^SSN");
    }

    [Fact]
    public void SetValue_FieldWithoutRepetition_StillTargetsFirstRepetition()
    {
        var segment = new Segment("PID");
        segment.SetFieldFromRaw(3, "OLD1~OLD2", Encoding);

        segment.SetValue("PID.3", "NEW1^NEW2", Encoding);

        segment.GetValue("PID.3~1", Encoding).ShouldBe("NEW1^NEW2");
        segment.GetValue("PID.3~2", Encoding).ShouldBe("OLD2");
        segment.GetValue("PID.3", Encoding).ShouldBe("NEW1^NEW2~OLD2");
    }

    [Fact]
    public void SetFieldFromRaw_SplitsOnEncodingSeparators()
    {
        var segment = new Segment("PID");
        segment.SetFieldFromRaw(5, "DOE^JOHN^A", Encoding);
        segment.SetFieldFromRaw(3, "ID1^^^A^MR~ID2^^^B^SSN", Encoding);

        segment.GetValue("PID.5.1", Encoding).ShouldBe("DOE");
        segment.GetValue("PID.5.2", Encoding).ShouldBe("JOHN");
        segment.GetValue("PID.3~2.1", Encoding).ShouldBe("ID2");
        segment.GetValue("PID.3~2.5", Encoding).ShouldBe("SSN");
    }

    [Fact]
    public void GetValue_MissingPath_ReturnsNull()
    {
        var segment = new Segment("PID");
        segment.GetValue("PID.5.1", Encoding).ShouldBeNull();
    }

    [Fact]
    public void GetValue_WrongSegmentName_Throws()
    {
        var segment = new Segment("PID");
        Should.Throw<ArgumentException>(() =>
            segment.GetValue("OBX.5", Encoding));
    }

    [Fact]
    public void GetFieldRaw_AssemblesWithSeparators()
    {
        var segment = new Segment("PID");
        segment.SetValue("PID.5.1", "DOE", Encoding);
        segment.SetValue("PID.5.2", "JOHN", Encoding);

        segment.GetFieldRaw(5, Encoding).ShouldBe("DOE^JOHN");
    }

    [Fact]
    public void SetValueRaw_FieldLevel_DoesNotSplitOnComponentSeparator()
    {
        var segment = new Segment("PID");
        segment.SetValueRaw("PID.5", "DOE^JOHN^A");

        // Stored as one component / one subcomponent — not split into DOE, JOHN, A
        segment.GetValue("PID.5.1.1", Encoding).ShouldBe("DOE^JOHN^A");
        segment.GetValue("PID.5.2", Encoding).ShouldBeNull();
        // Component/field getters escape separators for wire-safe output
        segment.GetValue("PID.5.1", Encoding).ShouldBe(@"DOE^JOHN^A");
        segment.GetValue("PID.5", Encoding).ShouldBe(@"DOE^JOHN^A");
    }

    [Fact]
    public void SetValueRaw_ContrastsWithSetValue_WhichSplitsComponents()
    {
        var parsed = new Segment("PID");
        parsed.SetValue("PID.5", "DOE^JOHN", Encoding);

        var raw = new Segment("PID");
        raw.SetValueRaw("PID.5", "DOE^JOHN");

        parsed.GetValue("PID.5.1", Encoding).ShouldBe("DOE");
        parsed.GetValue("PID.5.2", Encoding).ShouldBe("JOHN");

        raw.GetValue("PID.5.1.1", Encoding).ShouldBe("DOE^JOHN");
        raw.GetValue("PID.5.2", Encoding).ShouldBeNull();
    }

    [Fact]
    public void SetValueRaw_ComponentLevel_DoesNotSplitOnSubcomponentSeparator()
    {
        var segment = new Segment("PID");
        segment.SetValueRaw("PID.3.1", "ID1&FAC");

        segment.GetValue("PID.3.1.1", Encoding).ShouldBe("ID1&FAC");
        segment.GetValue("PID.3.1.2", Encoding).ShouldBeNull();
        segment.GetValue("PID.3.1", Encoding).ShouldBe("ID1&FAC");
    }

    [Fact]
    public void SetValueRaw_SubcomponentLevel_StoresLiteral()
    {
        var segment = new Segment("PID");
        segment.SetValueRaw("PID.3.1.1", "ID1");
        segment.SetValueRaw("PID.3.1.2", "FAC&MORE");

        segment.GetValue("PID.3.1.1", Encoding).ShouldBe("ID1");
        segment.GetValue("PID.3.1.2", Encoding).ShouldBe("FAC&MORE");
    }

    [Fact]
    public void SetValueRaw_NullValue_StoresEmptyString()
    {
        var segment = new Segment("PID");
        segment.SetValueRaw("PID.5.1", null);

        segment.GetValue("PID.5.1.1", Encoding).ShouldBe("");
    }

    [Fact]
    public void SetValueRaw_FieldRepetition_StoresPerRepetition()
    {
        var segment = new Segment("PID");
        segment.SetValueRaw("PID.3~1", "ID1^^^A^MR");
        segment.SetValueRaw("PID.3~2", "ID2^^^B^SSN");

        segment.GetValue("PID.3~1.1.1", Encoding).ShouldBe("ID1^^^A^MR");
        segment.GetValue("PID.3~2.1.1", Encoding).ShouldBe("ID2^^^B^SSN");
        segment.GetValue("PID.3", Encoding).ShouldContain("~");
    }

    [Fact]
    public void SetValueRaw_Hl7PathOverload_Works()
    {
        var segment = new Segment("NTE");
        segment.SetValueRaw("NTE.3", "note with | and ^");

        segment.GetValue("NTE.3.1.1", Encoding).ShouldBe("note with | and ^");
        segment.GetValue("NTE.3", Encoding).ShouldBe("note with | and ^");
    }

    [Fact]
    public void SetValueRaw_WrongSegmentName_Throws()
    {
        var segment = new Segment("PID");
        Should.Throw<ArgumentException>(() => segment.SetValueRaw("OBX.5", "x"));
    }

    [Fact]
    public void SetValueRaw_PathWithoutField_Throws()
    {
        var segment = new Segment("PID");
        Should.Throw<ArgumentException>(() =>
            segment.SetValueRaw(new Hl7Path("PID"), "x"));
    }

    [Fact]
    public void SetValueRaw_NullPath_Throws()
    {
        var segment = new Segment("PID");
        Should.Throw<ArgumentNullException>(() =>
            segment.SetValueRaw((Hl7Path)null!, "x"));
    }
}
