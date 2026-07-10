# AryaDev.Utils.HL7

A .NET library for working with [HL7 v2.9.1](https://www.hl7.org/implement/standards/product_brief.cfm?product_id=649) pipe-delimited messages. Messages are deserialized into a string-based model and fields are accessed using standard HL7 location notation (for example, `PID.5.1` or `PV1.3.2.1`).

The intent of this library is to make HL7 field access much more intuitive - if someone tells me they will populate a value in the "Provider's Administration Instructions" field,
I would have to take time to identify exactly what they were referring to. Instead, most use the standard reference language like "RXE.7.2" - and I know exactly where that field is in the message.
With this library, you can reference the segments using the same reference language that is used elsewhere to get the string value out from that field and handle it how you want.

> [!IMPORTANT]
> This library is NOT intended for validation against the HL7 standard. Every organization
> has a slightly different implementation or version that they use, and data in certain fields will be formatted differently.
> This library simply provides an easier-to-understand interface for accessing the fields in an HL7 message.

## Deserialization

Pass a raw HL7 message string or byte array to `HL7Serializer.Deserialize`. The parser normalizes line endings, reads delimiter encoding characters from `MSH-2`, resolves the text character set from `MSH-18` (defaulting to ASCII when absent), unescapes field values, and returns an `HL7Message` containing a collection of `Segment` objects.

```csharp
using AryaDev.Utils.HL7.Domain;
using AryaDev.Utils.HL7.Serializer;

var raw = """
    MSH|^~\&|SENDING|FACILITY|RECEIVING|FACILITY|20260101120000||ORU^R01|MSG00001|P|2.9.1
    PID|1||12345^^^FACILITY^MR||DOE^JOHN^A||19800101|M
    OBR|1|||CBC^Complete Blood Count^L|||20260101120000
    OBX|1|NM|WBC^White Blood Cells^L||7.2|10*3/uL|4.0-11.0|N|||F
    """;

var serializer = new HL7Serializer();
HL7Message message = serializer.Deserialize(raw);

Console.WriteLine(message.Segments.Count); // 4
Console.WriteLine(message.TextEncoding.WebName); // us-ascii (default when MSH-18 is absent)
```

### Character Set (MSH-18)

HL7 Table 0211 values in `MSH-18` control how raw bytes are decoded. When deserializing from `byte[]`, the first segment line is read as ASCII to locate `MSH-18`, then the full buffer is decoded with the resolved encoding. When `MSH-18` is empty or missing, ASCII is used.

```csharp
// UTF-8 message bytes from a TCP/MLLP stream
byte[] inbound = ReadFromSocket();
HL7Message message = serializer.Deserialize(inbound);

string? charset = message.CharacterSet;       // e.g. "UTF-8"
var encoding = message.TextEncoding;          // System.Text.Encoding.UTF8

// Serialize back to bytes using the message character set
byte[] outbound = serializer.SerializeBytes(message);
```

Supported values include common HL7 aliases such as `ASCII`, `UTF-8`, `8859/1`, and `ISO IR 100`. When multiple character sets are listed in `MSH-18` (separated by `^`), the first value is used as the primary encoding.

## Accessing Fields

All field values are `string`. Use the `HL7Message` indexer with HL7 reference paths:

```csharp
// Message header
string? messageType = message["MSH.9"];           // ORU^R01
string? controlId   = message["MSH.10"];          // MSG00001

// Patient identification (CX datatype — component level)
string? patientId   = message["PID.3.1"];         // 12345
string? lastName    = message["PID.5.1"];         // DOE
string? firstName   = message["PID.5.2"];         // JOHN

// Observation result
string? observationValue = message["OBX.5"];      // 7.2
string? units            = message["OBX.6"];      // 10*3/uL
```

### Path Notation

Paths use **1-based** indexing throughout, aligned with HL7 field numbering:

| Path part | Example | Meaning |
|-----------|---------|---------|
| Segment name | `PID` | Three-character segment ID |
| `[n]` | `PID[2]` | Segment occurrence (defaults to `1`) |
| `.{field}` | `.5` | Field number |
| `~{rep}` | `.3~2` | Field repetition (defaults to `1`) |
| `.{component}` | `.5.1` | Component within a field |
| `.{subcomponent}` | `.3.2.1` | Subcomponent within a component |

Examples:

```
PV1.3.2.1      → first PV1, field 3, component 2, subcomponent 1
PID[2].5       → second PID segment, field 5
OBX.5          → first OBX, field 5 (entire field value)
MSH.9          → message type
DG1[3].5~2.3.1 → third DG1, field 5, repetition 2, component 3, subcomponent 1
```

When a path targets a field without specifying component or subcomponent depth, the library returns the assembled field value with HL7 separators preserved.

### Setting Field Values

The indexer also supports assignment. Setting a value on a path that does not yet exist creates the required segment and field structure:

```csharp
message["PID.5.1"] = "SMITH";
message["PID.5.2"] = "JANE";
message["OBX.5"] = "98.6";
```

### Enumerating Segments

```csharp
foreach (Segment pid in message.GetSegments("PID"))
{
    Console.WriteLine(pid.Name);  // PID
    Console.WriteLine(pid.Type);  // SegmentType.PatientIdentification (when recognized)
}
```

### Segment Metadata

The `SegmentType` enum catalogs all HL7 v2.9.1 segments with chapter, code, and description metadata:

```csharp
using AryaDev.Utils.HL7.Domain.Enumeration;

string code = SegmentType.PatientIdentification.GetCode();       // PID
string? desc = SegmentType.PatientIdentification.GetDescription();
bool optional = SegmentType.PatientIdentification.IsOptional();

SegmentTypeExtensions.TryParseCode("OBX", out var segmentType);    // true
```

## Serialization

Pass an `HL7Message` back to `HL7Serializer.Serialize` to produce a pipe-delimited HL7 string. Segments are joined with carriage return (`\r`) and field values are escaped according to the message encoding characters.

```csharp
string output = serializer.Serialize(message);
```

An example round-trip workflow:

```csharp
HL7Message parsed = serializer.Deserialize(inboundRaw);

string patientName = parsed["PID.5.1"];
parsed["OBX.5"] = "99.1";

string outboundRaw = serializer.Serialize(parsed);
```