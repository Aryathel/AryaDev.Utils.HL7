namespace AryaDev.Utils.HL7.Serializer.Test.Fixtures;

internal static class SampleMessages
{
    /// <summary>ORU^R01^ORU_R01 observation result (CR-separated).</summary>
    public const string OruR01 = """
        MSH|^~\&|LAB|FACILITY|EHR|FACILITY|20260101120000||ORU^R01^ORU_R01|MSG00001|P|2.9.1
        PID|1||12345^^^FACILITY^MR||DOE^JOHN^A||19800101|M
        OBR|1|||CBC^Complete Blood Count^L|||20260101120000
        OBX|1|NM|WBC^White Blood Cells^L||7.2|10*3/uL|4.0-11.0|N|||F
        """;

    /// <summary>ADT^A01^ADT_A01 admit/visit notification.</summary>
    public const string AdtA01 = """
        MSH|^~\&|ADT|HOSPITAL|EHR|HOSPITAL|20260101120000||ADT^A01^ADT_A01|MSG00002|P|2.9.1
        EVN|A01|20260101120000
        PID|1||98765^^^HOSPITAL^MR||SMITH^JANE^B||19900202|F
        PV1|1|I|2000^2012^01||||004777^ATTENDING^AARON
        """;

    /// <summary>RDE^O11^RDE_O11 pharmacy/treatment encoded order.</summary>
    public const string RdeO11 = """
        MSH|^~\&|PHARM|FACILITY|EHR|FACILITY|20260101120000||RDE^O11^RDE_O11|MSG00003|P|2.9.1
        PID|1||55555^^^FACILITY^MR||JONES^ROBERT^C||19750315|M
        ORC|NW|ORD1001|||||||20260101120000
        RXE|1^BID^D10^^^RX||500|mg
        """;

    /// <summary>ACK acknowledgment (single-token message type structure).</summary>
    public static string Ack { get; } =
        "MSH|^~\\&|EHR|FACILITY|LAB|FACILITY|20260101120000||ACK^ACK^ACK|MSG00004|P|2.9.1\r" +
        "MSA|AA|MSG00001";

    /// <summary>Two-component MSH-9 only (ADT^A01, no structure id).</summary>
    public const string AdtA01TwoPart = """
        MSH|^~\&|ADT|HOSPITAL|EHR|HOSPITAL|20260101120000||ADT^A01|MSG00005|P|2.9.1
        PID|1||11111^^^HOSPITAL^MR||BROWN^LEE
        """;

    /// <summary>MSH-9 is structure code alone.</summary>
    public const string OruStructureOnly = """
        MSH|^~\&|LAB|FACILITY|EHR|FACILITY|20260101120000||ORU_R01|MSG00006|P|2.9.1
        PID|1||22222^^^FACILITY^MR||LEE^SAM
        """;

    /// <summary>Unrecognized MSH-9.</summary>
    public const string UnknownMessageType = """
        MSH|^~\&|APP|FAC|APP|FAC|20260101120000||ZZZ^Z99^ZZZ_Z99|MSG00007|P|2.9.1
        """;

    /// <summary>LF line endings instead of CR.</summary>
    public static string OruR01Lf { get; } = """
        MSH|^~\&|LAB|FACILITY|EHR|FACILITY|20260101120000||ORU^R01^ORU_R01|MSG00008|P|2.9.1
        PID|1||12345^^^FACILITY^MR||DOE^JOHN^A||19800101|M
        OBX|1|NM|WBC||7.2
        """.Replace("\r\n", "\n").Replace("\r", "\n");

    /// <summary>CRLF line endings.</summary>
    public const string OruR01Crlf = """
        MSH|^~\&|LAB|FACILITY|EHR|FACILITY|20260101120000||ORU^R01^ORU_R01|MSG00009|P|2.9.1
        PID|1||12345^^^FACILITY^MR||DOE^JOHN^A||19800101|M
        OBX|1|NM|WBC||7.2
        """;

    /// <summary>Escaped field/component separators in a value.</summary>
    public const string WithEscapes = """
        MSH|^~\&|APP|FAC|APP|FAC|20260101120000||ORU^R01^ORU_R01|MSG00010|P|2.9.1
        NTE|1||Note with \F\ pipe and \S\ caret
        """;

    /// <summary>UTF-8 character set in MSH-18 with non-ASCII patient name.</summary>
    public const string Utf8Oru = """
        MSH|^~\&|LAB|FACILITY|EHR|FACILITY|20260101120000||ORU^R01^ORU_R01|MSG00011|P|2.9.1||||||UTF-8
        PID|1||33333^^^FACILITY^MR||MÜLLER^HANS
        OBX|1|ST|NOTE||café
        """;

    /// <summary>Custom encoding characters: component separator is ~ (unusual but valid for testing).</summary>
    public const string CustomComponentSeparator = """
        MSH|~^\&|APP|FAC|APP|FAC|20260101120000||ADT~A01~ADT_A01|MSG00012|P|2.9.1
        PID|1||44444^^^FAC^MR||DOE~JOHN
        """;

    /// <summary>PID with repeating identifier (CX repetitions).</summary>
    public const string PidWithRepetitions = """
        MSH|^~\&|ADT|HOSPITAL|EHR|HOSPITAL|20260101120000||ADT^A01^ADT_A01|MSG00013|P|2.9.1
        PID|1||ID1^^^A^MR~ID2^^^B^SSN||SMITH^JANE
        """;

    /// <summary>Hex escape \Xhhhh\ in a note (U+00A9 copyright, U+0041 'A').</summary>
    public const string WithHexEscapes = """
        MSH|^~\&|APP|FAC|APP|FAC|20260101120000||ORU^R01^ORU_R01|MSG00014|P|2.9.1
        NTE|1||Copyright \X00A9\ and letter \X0041\
        """;

    /// <summary>Multi-hex escape \Zhhhh...\ decoding consecutive code points ("Hi").</summary>
    public const string WithMultiHexEscapes = """
        MSH|^~\&|APP|FAC|APP|FAC|20260101120000||ORU^R01^ORU_R01|MSG00015|P|2.9.1
        NTE|1||Greetings, \Z00480069\ to you!
        """;

    /// <summary>Multiple OBX segments (segment occurrence).</summary>
    public const string MultipleObxSegments = """
        MSH|^~\&|LAB|FACILITY|EHR|FACILITY|20260101120000||ORU^R01^ORU_R01|MSG00016|P|2.9.1
        PID|1||12345^^^FACILITY^MR||DOE^JOHN
        OBX|1|NM|WBC||7.2
        OBX|2|NM|RBC||4.8
        OBX|3|ST|NOTE||normal
        """;

    /// <summary>Field repetitions on NK1 phone numbers and PID identifiers.</summary>
    public const string FieldRepetitionsExtended = """
        MSH|^~\&|ADT|HOSPITAL|EHR|HOSPITAL|20260101120000||ADT^A01^ADT_A01|MSG00017|P|2.9.1
        PID|1||MR1^^^A^MR~MR2^^^B^MR~SSN1^^^C^SSN||SMITH^JANE
        NK1|1|SMITH^JOHN|SPO|123 MAIN^^CITY^ST^12345|5551111^PRN^PH~5552222^WPN^PH
        """;

    /// <summary>Repeated NTE segments with distinct comments.</summary>
    public const string MultipleNteSegments = """
        MSH|^~\&|LAB|FACILITY|EHR|FACILITY|20260101120000||ORU^R01^ORU_R01|MSG00018|P|2.9.1
        PID|1||99999^^^FACILITY^MR||LEE^SAM
        NTE|1||First comment
        NTE|2||Second comment
        NTE|3||Third comment
        """;
}
