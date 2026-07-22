namespace AryaDev.Utils.HL7.Domain.Enumeration;

/// <summary>
/// HL7 chapter groupings for segments (flags; a segment may belong to more than one).
/// </summary>
[Flags]
public enum SegmentChapter
{
    /// <summary>No chapter association.</summary>
    Unknown = 0,

    /// <summary>Control / infrastructure segments (MSH, MSA, ERR, …).</summary>
    Control = 1 << 0,

    /// <summary>Patient administration segments.</summary>
    PatientAdministration = 1 << 1,

    /// <summary>Order entry segments.</summary>
    OrderEntry = 1 << 2,

    /// <summary>Query segments.</summary>
    Query = 1 << 3,

    /// <summary>Financial management segments.</summary>
    FinancialManagement = 1 << 4,

    /// <summary>Observation reporting segments.</summary>
    ObservationReporting = 1 << 5,

    /// <summary>Master files segments.</summary>
    MasterFiles = 1 << 6,

    /// <summary>Medical records / information management segments.</summary>
    MedicalRecords_InformationManagement = 1 << 7,

    /// <summary>Scheduling segments.</summary>
    Scheduling = 1 << 8,

    /// <summary>Patient referral segments.</summary>
    PatientReferral = 1 << 9,

    /// <summary>Patient care segments.</summary>
    PatientCare = 1 << 10,

    /// <summary>Clinical laboratory automation segments.</summary>
    ClinicalLaboratoryAutomation = 1 << 11,

    /// <summary>Application management segments.</summary>
    ApplicationManagement = 1 << 12,

    /// <summary>Personnel management segments.</summary>
    PersonnelManagement = 1 << 13,

    /// <summary>Claims and reimbursement segments.</summary>
    ClaimsAndReimbursement = 1 << 14,

    /// <summary>Materials management segments.</summary>
    MaterialsManagement = 1 << 15,
}
