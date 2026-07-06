using AryaDev.Utils.HL7.Domain.Attributes;

namespace AryaDev.Utils.HL7.Domain.Enumeration;

/// <summary>
///  <summary>

/// </summary>
public enum SegmentType
{
    [SegmentInfo("ADD", SegmentChapter.Control)]
    Addendum,
    [SegmentInfo("BHS", SegmentChapter.Control)]
    BatchHeader,
    [SegmentInfo("BTS", SegmentChapter.Control)]
    BatchTrailer,
    [SegmentInfo("DSC", SegmentChapter.Control)]
    ContinuationPointer,
    [SegmentInfo("ERR", SegmentChapter.Control)]
    Error,
    [SegmentInfo("FHS", SegmentChapter.Control)]
    FileHeader,
    [SegmentInfo("FTS", SegmentChapter.Control)]
    FileTrailer,
    [SegmentInfo("MSA", SegmentChapter.Control, repeatable: false)]
    MessageAcknowledgement,
    [SegmentInfo("MSH", SegmentChapter.Control, repeatable: false)]
    MessageHeader,
    [SegmentInfo("NTE", SegmentChapter.Control)]
    NotesAndComments,
    [SegmentInfo("OVR", SegmentChapter.Control)]
    OverrideSegment,
    [SegmentInfo("SFT", SegmentChapter.Control)]
    SoftwareSegment,
    [SegmentInfo("UAC", SegmentChapter.Control)]
    UserAuthenticationCredentialSegment,
    [SegmentInfo("Z**", SegmentChapter.Control)]
    ZSegment,
    
    [SegmentInfo("AL1", SegmentChapter.PatientAdministration)]
    PatientAllergyInformation,
    [SegmentInfo("ARV", SegmentChapter.PatientAdministration)]
    AccessRestriction,
    [SegmentInfo("DB1", SegmentChapter.PatientAdministration)]
    Disability,
    [SegmentInfo("EVN", SegmentChapter.PatientAdministration)]
    EventType,
    [SegmentInfo("IAM", SegmentChapter.PatientAdministration)]
    PatientAdverseReactionInformation,
    [SegmentInfo("IAR", SegmentChapter.PatientAdministration)]
    AllergyReaction,
    [SegmentInfo("MRG", SegmentChapter.PatientAdministration)]
    MergePatientInformation,
    [SegmentInfo("NK1", SegmentChapter.PatientAdministration)]
    NextOfKin,
    [SegmentInfo("NPU", SegmentChapter.PatientAdministration)]
    BedStatusUpdate,
    [SegmentInfo("PD1", SegmentChapter.PatientAdministration)]
    PatientAdditionalDemographic,
    [SegmentInfo("PDA", SegmentChapter.PatientAdministration)]
    PatientDeathAndAutopsy,
    [SegmentInfo("PID", SegmentChapter.PatientAdministration)]
    PatientIdentification,
    [SegmentInfo("PV1",  SegmentChapter.PatientAdministration)]
    PatientVisit,
    [SegmentInfo("PV2", SegmentChapter.PatientAdministration)]
    PatientVisitAdditionalInformation,
    
    [SegmentInfo("BLG", SegmentChapter.OrderEntry)]
    Billing,
    [SegmentInfo("BPO", SegmentChapter.OrderEntry)]
    BloodProductOrder,
    [SegmentInfo("BPX", SegmentChapter.OrderEntry)]
    BloodProductDispenseStatus,
    [SegmentInfo("BTX", SegmentChapter.OrderEntry)]
    BloodProductTransfusionDisposition,
    [SegmentInfo("BUI", SegmentChapter.OrderEntry)]
    BloodUnitInformationSegment,
    [SegmentInfo("CDO", SegmentChapter.OrderEntry)]
    CumulativeDosage,
    [SegmentInfo("DON", SegmentChapter.OrderEntry)]
    DonationSegment,
    [SegmentInfo("IPC", SegmentChapter.OrderEntry)]
    ImagingProcedureControlSegment,
    [SegmentInfo("OBR", SegmentChapter.OrderEntry | SegmentChapter.ObservationReporting)]
    ObservationRequest,
    [SegmentInfo("ODS", SegmentChapter.OrderEntry)]
    DietaryOrdersSupplementsAndPreferences,
    [SegmentInfo("ODT", SegmentChapter.OrderEntry)]
    DietTrayInstructions,
    [SegmentInfo("ORC", SegmentChapter.OrderEntry)]
    CommonOrder,
    [SegmentInfo("RQ1", SegmentChapter.OrderEntry)]
    RequisitionDetail1,
    [SegmentInfo("RQD", SegmentChapter.OrderEntry)]
    RequisitionDetail,
    [SegmentInfo("RXA", SegmentChapter.OrderEntry)]
    PharmacyTreatmentAdministration,
    [SegmentInfo("RXC", SegmentChapter.OrderEntry)]
    PharmacyTreatmentComponentOrder,
    [SegmentInfo("RXD", SegmentChapter.OrderEntry)]
    PharmacyTreatmentDispense,
    [SegmentInfo("RXE", SegmentChapter.OrderEntry)]
    PharmactTreatmentEncodedOrder,
    [SegmentInfo("RXG", SegmentChapter.OrderEntry)]
    PharmacyTreatmentGive,
    [SegmentInfo("RXO", SegmentChapter.OrderEntry)]
    PharmacyTreatmentOrder,
    [SegmentInfo("RXR", SegmentChapter.OrderEntry)]
    PharmacyTreatmentRoute,
    [SegmentInfo("RXV", SegmentChapter.OrderEntry)]
    PharmacyTreatmentInfusion,
    [SegmentInfo("TQ1", SegmentChapter.OrderEntry)]
    TimingQuantity,
    [SegmentInfo("TQ1", SegmentChapter.OrderEntry)]
    TimingQuantityRelationship,
    
    [SegmentInfo("DSP", SegmentChapter.Query)]
    DisplayData,
    [SegmentInfo("QAK", SegmentChapter.Query)]
    QueryAcknowledgement,
    [SegmentInfo("QID", SegmentChapter.Query)]
    QueryIdentification,
    [SegmentInfo("QPD", SegmentChapter.Query)]
    QueryParameterIdentification,
    [SegmentInfo("QRD", SegmentChapter.Query)]
    [Obsolete("Withdrawn")]
    OriginalStyleQueryDefinition,
    [SegmentInfo("QRF", SegmentChapter.Query)]
    [Obsolete("Withdrawn")]
    OriginalStyleQueryFilter,
    [SegmentInfo("QRI", SegmentChapter.Query)]
    QueryResponseInstance,
    [SegmentInfo("RCP", SegmentChapter.Query)]
    ResponseControlParameter,
    [SegmentInfo("RDF", SegmentChapter.Query)]
    TableRowDefinition,
    [SegmentInfo("RDT", SegmentChapter.Query)]
    TableRowData,
    [SegmentInfo("URD", SegmentChapter.Query)]
    ResultsUpdateDefinition,
    [SegmentInfo("URS", SegmentChapter.Query)]
    ResultsUpdateSelectionCriteria,
    
    [SegmentInfo("ABS", SegmentChapter.FinancialManagement)]
    Abstract,
    [SegmentInfo("ACC", SegmentChapter.FinancialManagement)]
    Accident,
    [SegmentInfo("BLC", SegmentChapter.FinancialManagement)]
    BloodCode,
    [SegmentInfo("DG1", SegmentChapter.FinancialManagement)]
    Diagnosis,
    [SegmentInfo("DRG", SegmentChapter.FinancialManagement)]
    DiagnosisRelatedGroup,
    [SegmentInfo("FT1", SegmentChapter.FinancialManagement)]
    FinancialTransaction,
    [SegmentInfo("GP1", SegmentChapter.FinancialManagement)]
    GroupingReimbursementVisit,
    [SegmentInfo("GP2", SegmentChapter.FinancialManagement)]
    GroupingReimbursementProcedureLineItem,
    [SegmentInfo("GT1", SegmentChapter.FinancialManagement)]
    Guarantor,
    [SegmentInfo("IN1", SegmentChapter.FinancialManagement)]
    Insurance,
    [SegmentInfo("IN2", SegmentChapter.FinancialManagement)]
    InsuranceAdditionaInformation,
    [SegmentInfo("IN3", SegmentChapter.FinancialManagement)]
    InsuranceAdditionalInformationCertification,
    [SegmentInfo("PR1", SegmentChapter.FinancialManagement)]
    Procedures,
    [SegmentInfo("RMI", SegmentChapter.FinancialManagement)]
    RiskManagementIncident,
    [SegmentInfo("UB1", SegmentChapter.FinancialManagement)]
    Ub82,
    [SegmentInfo("UB2", SegmentChapter.FinancialManagement)]
    UniformBillingData,
    
    [SegmentInfo("CSP", SegmentChapter.ObservationReporting)]
    ClinicalStudyPhase,
    [SegmentInfo("CSR", SegmentChapter.ObservationReporting)]
    ClinicalStudyRegistration,
    [SegmentInfo("CSS", SegmentChapter.ObservationReporting)]
    ClinicalStudyDataScheduleSegment,
    [SegmentInfo("CTI", SegmentChapter.ObservationReporting)]
    ClinicalTrialIdentification,
    [SegmentInfo("FAC", SegmentChapter.ObservationReporting)]
    Facility,
    [SegmentInfo("OBX", SegmentChapter.ObservationReporting)]
    ObservationResult,
    [SegmentInfo("PAC", SegmentChapter.ObservationReporting)]
    ShipmentPackage,
    [SegmentInfo("PCR", SegmentChapter.ObservationReporting)]
    PossibleCausalRelationship,
    [SegmentInfo("PDC", SegmentChapter.ObservationReporting)]
    ProductDetailCountry,
    [SegmentInfo("PEO", SegmentChapter.ObservationReporting)]
    ProductExperienceObservation,
    [SegmentInfo("PES", SegmentChapter.ObservationReporting)]
    ProductExperienceSender,
    [SegmentInfo("PRT", SegmentChapter.ObservationReporting)]
    ParticipationInformation,
    [SegmentInfo("PSH", SegmentChapter.ObservationReporting)]
    ProductSummaryHeader,
    [SegmentInfo("SHP", SegmentChapter.ObservationReporting)]
    Shipment,
    [SegmentInfo("SPM", SegmentChapter.ObservationReporting)]
    Specimen,
    
    [SegmentInfo("CDM", SegmentChapter.MasterFiles)]
    ChargeDescriptionMaster,
    [SegmentInfo("CM0", SegmentChapter.MasterFiles)]
    ClinicalStudyMaster,
    [SegmentInfo("CM1", SegmentChapter.MasterFiles)]
    ClinicalStudyPhaseMaster,
    [SegmentInfo("CM2", SegmentChapter.MasterFiles)]
    ClinicalStudyScheduleMaster,
    [SegmentInfo("DMI", SegmentChapter.MasterFiles)]
    DrgMasterFileInformation,
    [SegmentInfo("LCC", SegmentChapter.MasterFiles)]
    LocationChargeCode,
    [SegmentInfo("LCH", SegmentChapter.MasterFiles)]
    LocationCharacteristic,
    [SegmentInfo("LDP", SegmentChapter.MasterFiles)]
    LocationDepartment,
    [SegmentInfo("LOC", SegmentChapter.MasterFiles)]
    LocationIdentification,
    [SegmentInfo("LRL", SegmentChapter.MasterFiles)]
    LocationRelationship,
    [SegmentInfo("MFA", SegmentChapter.MasterFiles)]
    MasterFileAcknowledgement,
    [SegmentInfo("MFE", SegmentChapter.MasterFiles)]
    MasterFileEntry,
    [SegmentInfo("MFI", SegmentChapter.MasterFiles)]
    MasterFileIdentification,
    [SegmentInfo("OM1", SegmentChapter.MasterFiles)]
    GeneralSegment,
    [SegmentInfo("OM2", SegmentChapter.MasterFiles)]
    NumericObservation,
    [SegmentInfo("OM3", SegmentChapter.MasterFiles)]
    CategoricalServiceTestObservation,
    [SegmentInfo("OM4", SegmentChapter.MasterFiles)]
    ObservationsThatRequireSpecimens,
    [SegmentInfo("OM5", SegmentChapter.MasterFiles)]
    ObservationBatteriesSets,
    [SegmentInfo("OM6", SegmentChapter.MasterFiles)]
    ObservationsThatAreCalculatedFromOtherObservations,
    [SegmentInfo("OM7", SegmentChapter.MasterFiles)]
    AdditionalBasicAttributes,
    [SegmentInfo("PRC", SegmentChapter.MasterFiles)]
    Pricing,
    
    [SegmentInfo("CON", SegmentChapter.MedicalRecords_InformationManagement)]
    ConsentSegment,
    [SegmentInfo("TXA", SegmentChapter.MedicalRecords_InformationManagement)]
    TranscriptionDocumentHeader,
    
    [SegmentInfo("AIG", SegmentChapter.Scheduling)]
    AppointmentInformationGeneralResource,
    [SegmentInfo("AIL", SegmentChapter.Scheduling)]
    AppointmentInformationLocationResource,
    [SegmentInfo("AIP", SegmentChapter.Scheduling)]
    AppointmentInformationPersonnelResource,
    [SegmentInfo("AIS", SegmentChapter.Scheduling)]
    AppointmentInformation,
    [SegmentInfo("APR", SegmentChapter.Scheduling)]
    AppointmentPreference,
    [SegmentInfo("ARQ", SegmentChapter.Scheduling)]
    AppointmentRequest,
    [SegmentInfo("RGS", SegmentChapter.Scheduling)]
    ResourceGroup,
    [SegmentInfo("SCH", SegmentChapter.Scheduling)]
    SchedulingActivityInformation,
    
    [SegmentInfo("AUT", SegmentChapter.PatientReferral)]
    AuthorizationInformation,
    [SegmentInfo("CTD", SegmentChapter.PatientReferral)]
    ContactData,
    [SegmentInfo("PRD", SegmentChapter.PatientReferral)]
    ProviderData,
    [SegmentInfo("RF1", SegmentChapter.PatientReferral)]
    ReferralInformation,
    
    [SegmentInfo("GOL", SegmentChapter.PatientCare)]
    GoalDetail,
    [SegmentInfo("PRB", SegmentChapter.PatientCare)]
    ProblemDetails,
    [SegmentInfo("PTH", SegmentChapter.PatientCare)]
    Pathway,
    [SegmentInfo("REL", SegmentChapter.PatientCare)]
    ClinicalRelationshipSegment,
    [SegmentInfo("VAR", SegmentChapter.PatientCare)]
    Variance,
    
    [SegmentInfo("CNS", SegmentChapter.ClinicalLaboratoryAutomation)]
    ClearNotification,
    [SegmentInfo("ECD", SegmentChapter.ClinicalLaboratoryAutomation)]
    EquipmentCommand,
    [SegmentInfo("ECR", SegmentChapter.ClinicalLaboratoryAutomation)]
    EquipmentCommandResponse,
    [SegmentInfo("EQP", SegmentChapter.ClinicalLaboratoryAutomation)]
    EquipmentLogService,
    [SegmentInfo("EQU", SegmentChapter.ClinicalLaboratoryAutomation)]
    EquipmentDetail,
    [SegmentInfo("INV", SegmentChapter.ClinicalLaboratoryAutomation)]
    InventoryDetail,
    [SegmentInfo("ISD", SegmentChapter.ClinicalLaboratoryAutomation)]
    InteractionStatusDetail,
    [SegmentInfo("NDS", SegmentChapter.ClinicalLaboratoryAutomation)]
    NotificationDetail,
    [SegmentInfo("SAC", SegmentChapter.ClinicalLaboratoryAutomation)]
    SpecimenContainerDetail,
    [SegmentInfo("SID", SegmentChapter.ClinicalLaboratoryAutomation)]
    SubstanceIdentifier,
    [SegmentInfo("TCC", SegmentChapter.ClinicalLaboratoryAutomation)]
    TestCodeConfiguration,
    [SegmentInfo("TCD", SegmentChapter.ClinicalLaboratoryAutomation)]
    TestCodeDetail,
    
    [SegmentInfo("NCK", SegmentChapter.ApplicationManagement)]
    SystemClock,
    [SegmentInfo("NSC", SegmentChapter.ApplicationManagement)]
    ApplicationStatusChange,
    [SegmentInfo("NST", SegmentChapter.ApplicationManagement)]
    ApplicationControlLevelStatistics,
    
    [SegmentInfo("AFF", SegmentChapter.PersonnelManagement)]
    ProfessionalAffiliation,
    [SegmentInfo("CER", SegmentChapter.PersonnelManagement)]
    CertificateDetail,
    [SegmentInfo("EDU", SegmentChapter.PersonnelManagement)]
    EducationalDetaul,
    [SegmentInfo("LAN", SegmentChapter.PersonnelManagement)]
    LanguageDetail,
    [SegmentInfo("ORG", SegmentChapter.PersonnelManagement)]
    PractitionerOrganizationUnit,
    [SegmentInfo("PRA", SegmentChapter.PersonnelManagement)]
    PractitionerDetail,
    [SegmentInfo("ROL", SegmentChapter.PersonnelManagement)]
    Role,
    [SegmentInfo("STF", SegmentChapter.PersonnelManagement)]
    StaffIdentification,
    
    [SegmentInfo("ADJ", SegmentChapter.ClaimsAndReimbursement)]
    Adjustment,
    [SegmentInfo("IPR", SegmentChapter.ClaimsAndReimbursement)]
    InvoiceProcessingResults,
    [SegmentInfo("IVC", SegmentChapter.ClaimsAndReimbursement)]
    InvoiceSegment,
    [SegmentInfo("PMT", SegmentChapter.ClaimsAndReimbursement)]
    PaymentInformation,
    [SegmentInfo("PSG", SegmentChapter.ClaimsAndReimbursement)]
    ProductServiceGroup,
    [SegmentInfo("PSL", SegmentChapter.ClaimsAndReimbursement)]
    ProductServiceLineItem,
    [SegmentInfo("PSS", SegmentChapter.ClaimsAndReimbursement)]
    ProductServiceSection,
    [SegmentInfo("PYE", SegmentChapter.ClaimsAndReimbursement)]
    PayeeInformation,
    [SegmentInfo("RFI", SegmentChapter.ClaimsAndReimbursement)]
    RequestForInformation,
}