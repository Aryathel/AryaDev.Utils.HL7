namespace AryaDev.Utils.HL7.Domain.Enumeration;

[Flags]
public enum SegmentChapter
{
    Unknown = 0,
    Control = 1 << 0,
    PatientAdministration = 1 << 1,
    OrderEntry = 1 << 2,
    Query = 1 << 3,
    FinancialManagement = 1 << 4,
    ObservationReporting = 1 << 5,
    MasterFiles = 1 << 6,
    MedicalRecords_InformationManagement = 1 << 7,
    Scheduling = 1 << 8,
    PatientReferral = 1 << 9,
    PatientCare = 1 << 10,
    ClinicalLaboratoryAutomation = 1 << 11,
    ApplicationManagement = 1 << 12,
    PersonnelManagement = 1 << 13,
    ClaimsAndReimbursement = 1 << 14,
    MaterialsManagement = 1 << 15,
}