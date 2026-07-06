using System.ComponentModel.DataAnnotations;

namespace AryaDev.Utils.HL7.Domain;

public class Segment
{
    [Required]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Segment length must be 3 characters.")]
    public required string SegmentType { get; set; }
}