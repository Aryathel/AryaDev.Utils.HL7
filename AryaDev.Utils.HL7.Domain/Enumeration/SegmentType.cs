using System.ComponentModel;
using AryaDev.Utils.HL7.Domain.Attributes;

namespace AryaDev.Utils.HL7.Domain.Enumeration;

/// <summary>
/// Referenced from
/// <see href="https://www.hl7.org/implement/standards/product_brief.cfm?product_id=649">HL7 v2.9.1</see>.
/// </summary>
public enum SegmentType
{
    [SegmentInfo("ADD", SegmentChapter.Control)]
    [Description("The ADD segment is used to define the continuation of the prior segment in a continuation message.")]
    Addendum,
    [SegmentInfo("BHS", SegmentChapter.Control)]
    [Description("The BHS segment defines the start of a batch.")]
    BatchHeader,
    [SegmentInfo("BTS", SegmentChapter.Control)]
    [Description("The BTS segment defines the end of a batch.")]
    BatchTrailer,
    [SegmentInfo("DSC", SegmentChapter.Control)]
    [Description("The DSC segment is used in the continuation protocol.")]
    ContinuationPointer,
    [SegmentInfo("ERR", SegmentChapter.Control)]
    [Description("The ERR segment is used to add error comments to acknowledgment messages.")]
    Error,
    [SegmentInfo("FHS", SegmentChapter.Control)]
    [Description("The FHS segment is used to head a file (group of batches).")]
    FileHeader,
    [SegmentInfo("FTS", SegmentChapter.Control)]
    [Description("The FTS segment defines the end of a file.")]
    FileTrailer,
    [SegmentInfo("MSA", SegmentChapter.Control, repeatable: false)]
    [Description("The MSA segment contains information sent while acknowledging another message.")]
    MessageAcknowledgement,
    [SegmentInfo("MSH", SegmentChapter.Control, repeatable: false)]
    [Description("The MSH segment defines the intent, source, destination, and some specifics of the syntax of a message.")]
    MessageHeader,
    [SegmentInfo("NTE", SegmentChapter.Control)]
    [Description("The NTE segment is defined here for inclusion in messages defined in other chapters. It is commonly used for sending notes and comments.")]
    NotesAndComments,
    [SegmentInfo("OVR", SegmentChapter.Control)]
    [Description("This segment allows a sender to override specific receiving application's business rules to allow for processing of a message that would normally be rejected or ignored.")]
    OverrideSegment,
    [SegmentInfo("SFT", SegmentChapter.Control)]
    [Description("This segment provides additional information about the software product(s) used as a Sending Application. The primary purpose of this segment is for diagnostic use. There MAY be additional uses per site-specific agreements.")]
    SoftwareSegment,
    [SegmentInfo("SGH", SegmentChapter.Control)]
    [Description("The SGH segment is only used to provide information about the instantiated message structure to indicate that a new segment group begins and subsequent segments SHOULD be interpreted accordingly. It does not contain any patient related data.")]
    SegmentGroupHeader,
    [SegmentInfo("SGT", SegmentChapter.Control)]
    [Description("The SGT segment is only used to provide information about the instantiated message structure to the parsing process to indicate that the segment group ends and subsequent segments SHOULD be interpreted accordingly. It does not contain any patient related data. The Segment Group Trailer is required if the segment group header exists.")]
    SegmentGroupTrailer,
    [SegmentInfo("UAC", SegmentChapter.Control)]
    [Description("This optional segment provides user authentication credentials, a Kerberos Service Ticket or SAML assertion, to be used by the receiving system to obtain user identification data.")]
    UserAuthenticationCredentialSegment,
    [SegmentInfo("Z**", SegmentChapter.Control)]
    [Description("Any segments prefixed with \"Z\" are considered a custom site-defined segment for use in a required custom workflow.")]
    ZSegment,
    
    [SegmentInfo("AL1", SegmentChapter.PatientAdministration)]
    [Description("The AL1 segment contains patient allergy information of various types. Most of this information will be derived from user-defined tables. Each AL1 segment describes a single patient allergy.")]
    PatientAllergyInformation,
    [SegmentInfo("ARV", SegmentChapter.PatientAdministration)]
    [Description("The ARV segment is used to communicate the requested/required type of access restrictions from system to system, at both the person/patient and the encounter/visit level.")]
    AccessRestriction,
    [SegmentInfo("DB1", SegmentChapter.PatientAdministration)]
    [Description("The disability segment contains information related to the disability of a person. This segment was created instead of adding disability attributes to each segment that contains a person (to which disability may apply).")]
    Disability,
    [SegmentInfo("EVN", SegmentChapter.PatientAdministration)]
    [Description("The EVN segment is used to communicate necessary trigger event information to receiving applications.")]
    EventType,
    [SegmentInfo("GSC", SegmentChapter.PatientAdministration)]
    [Description("A Sex Parameter for Clinical Use is a parameter that provides guidance on how a recipient should apply settings or reference ranges that are derived from observable information such as an organ inventory, recent hormone lab tests, genetic testing, menstrual status, obstetric history, etc.")]
    SexParameterForClinicalUse,
    [SegmentInfo("GSP", SegmentChapter.PatientAdministration)]
    [Description("It is imperative that both sex and gender vocabulary be formally integrated into clinical care because they are not interchangeable. Both influence health outcomes. Gender-marginalized individuals face significant barriers to adequate and culturally responsive healthcare, leading to numerous health disparities. The single field PID-8 Sex with user-defined values was renamed in V2.4 (2000) to Administrative Sex in recognition that it was insufficient or inappropriate for conveying Sex Parameter for Clinical Use.")]
    PersonGenderAndSexSegment,
    [SegmentInfo("GSR", SegmentChapter.PatientAdministration)]
    [Description("The recorded sex and gender is to be used to differentiate existing sex or gender data. This element is used for existing 'sex' or 'gender' elements in a document or record when the intent and meaning is unclear. An individual may have zero to many such attributes.")]
    RecordedGenderAndSexSegment,
    [SegmentInfo("IAM", SegmentChapter.PatientAdministration)]
    [Description("The IAM segment contains person/patient adverse reaction information of various types. Most of this information will be derived from user-defined tables. Each IAM segment describes a single person/patient adverse reaction. This segment is used in lieu of the AL1 - Patient Allergy Information Segment.")]
    PatientAdverseReactionInformation,
    [SegmentInfo("IAR", SegmentChapter.PatientAdministration)]
    [Description("The IAR segment is used to transmit a single reaction and information associated with this single reaction occurrence for a particular patient allergy (IAM – patient adverse reaction).")]
    AllergyReaction,
    [SegmentInfo("MRG", SegmentChapter.PatientAdministration)]
    [Description("The MRG segment provides receiving applications with information necessary to initiate the merging of patient data as well as groups of records. It is intended that this segment be used throughout the Standard to allow the merging of registration, accounting, and clinical records within specific applications.")]
    MergePatientInformation,
    [SegmentInfo("NK1", SegmentChapter.PatientAdministration)]
    [Description("The NK1 segment contains information about the patient's other related parties. Any associated parties may be identified. Utilizing NK1-1 - set ID, multiple NK1 segments can be sent to patient accounts.")]
    NextOfKin,
    [SegmentInfo("NPU", SegmentChapter.PatientAdministration)]
    [Description("The NPU segment allows the updating of census (bed status) data without sending patient-specific data. An example might include changing the status of a bed from \"housekeeping\" to \"unoccupied.\"")]
    BedStatusUpdate,
    [SegmentInfo("OH1",  SegmentChapter.PatientAdministration)]
    [Description("The OH1 segment is a clinical statement about the subject’s state of being employed at the point in time the statement is recorded.")]
    PersonEmploymentStatus,
    [SegmentInfo("OH2",  SegmentChapter.PatientAdministration)]
    [Description("The OH2 segment is used to communicate the information about a job or jobs which the subject currently holds or has held in the past. It includes related observations about the occupation (type of work), the type of business (industry) in which that occupation is performed, supervisory level (including military pay grade), and the employer's name and location.")]
    PastOrPresentJob,
    [SegmentInfo("OH3",  SegmentChapter.PatientAdministration)]
    [Description("The OH3 segment contains information about the occupation which the subject has held for the longest duration through his or her working history, at the point in time the statement is recorded.")]
    UsualWork,
    [SegmentInfo("OH4",  SegmentChapter.PatientAdministration)]
    [Description("The OH4 segment contains the date range an individual has worked in what is considered a combat or hazardous duty zone; both civilian and military.")]
    CombatZoneWork,
    [SegmentInfo("PD1", SegmentChapter.PatientAdministration)]
    [Description("The patient additional demographic segment contains demographic information that is likely to change about the patient.")]
    PatientAdditionalDemographic,
    [SegmentInfo("PDA", SegmentChapter.PatientAdministration)]
    [Description("This segment carries information on a patient's death and possible autopsy.")]
    PatientDeathAndAutopsy,
    [SegmentInfo("PID", SegmentChapter.PatientAdministration)]
    [Description("The PID segment is used by all applications as the primary means of communicating patient identification information. This segment contains permanent patient identifying and demographic information that, for the most part, is not likely to change frequently")]
    PatientIdentification,
    [SegmentInfo("PV1",  SegmentChapter.PatientAdministration)]
    [Description("The PV1 segment is used by Registration/Patient Administration applications to communicate information on an account or visit-specific basis.")]
    PatientVisit,
    [SegmentInfo("PV2", SegmentChapter.PatientAdministration)]
    [Description("The PV2 segment is a continuation of information contained on the PV1 segment.")]
    PatientVisitAdditionalInformation,
    
    [SegmentInfo("BLG", SegmentChapter.OrderEntry)]
    [Description("The BLG segment is used to provide billing information, on the ordered service, to the filling application.")]
    Billing,
    [SegmentInfo("BPO", SegmentChapter.OrderEntry)]
    [Description("Blood product order messages require additional information that is not available in other standard HL7 order messages. Blood product order messages need to contain accompanying details regarding the blood product component, such as special processing requirements (e.g., irradiation and leukoreduction) and the amount of the blood product to be administered.")]
    BloodProductOrder,
    [SegmentInfo("BPX", SegmentChapter.OrderEntry)]
    [Description("In the processing of blood products, it is necessary for the transfusion service and the placer system to communicate information. The status messages need to contain additional information regarding the blood products requested, such as the unique donation ID, product code, blood type, expiration date/time of the blood product, and current status of the product.")]
    BloodProductDispenseStatus,
    [SegmentInfo("BTX", SegmentChapter.OrderEntry)]
    [Description("BTX records the transfusion or disposition of a blood product, including what happened to a unit and when.")]
    BloodProductTransfusionDisposition,
    [SegmentInfo("BUI", SegmentChapter.OrderEntry)]
    [Description("The intent of this segment is to describe the information associated with a blood unit, one example of which is one or more blood unit(s) resulting from a donation.")]
    BloodUnitInformationSegment,
    [SegmentInfo("CDO", SegmentChapter.OrderEntry)]
    [Description("The Cumulative Dosage segment allows for the communication of cumulative dosage limits that administrations against this medication order should stay within. As part of one of the pharmacy messages, one may want to indicate one or more limits that apply, e.g., limit for the duration of the order, lifetime limit, or weekly limit.")]
    CumulativeDosage,
    [SegmentInfo("DON", SegmentChapter.OrderEntry)]
    [Description("The intent of this segment is to describe the actual donation procedure.")]
    DonationSegment,
    [SegmentInfo("IPC", SegmentChapter.OrderEntry)]
    [Description("The IPC segment contains information about tasks that need to be performed in order to fulfill the request for imaging service. The information includes location, type and instance identification of equipment (acquisition modality) and stages (procedure steps).")]
    ImagingProcedureControlSegment,
    [SegmentInfo("OBR", SegmentChapter.OrderEntry | SegmentChapter.ObservationReporting)]
    [Description("The Observation Request (OBR) segment is used to transmit information specific to an order for a diagnostic study or observation, physical exam, or assessment.")]
    ObservationRequest,
    [SegmentInfo("ODS", SegmentChapter.OrderEntry)]
    [Description("ODS carries dietary orders, supplements, and preferences.")]
    DietaryOrdersSupplementsAndPreferences,
    [SegmentInfo("ODT", SegmentChapter.OrderEntry)]
    [Description("This segment addresses tray instructions. These are independent of diet codes, supplements, and preferences and therefore get separate order numbers.")]
    DietTrayInstructions,
    [SegmentInfo("ORC", SegmentChapter.OrderEntry)]
    [Description("The Common Order segment (ORC) is used to transmit fields that are common to all orders (all types of services that are requested).")]
    CommonOrder,
    [SegmentInfo("RQ1", SegmentChapter.OrderEntry)]
    [Description("RQ1 contains additional detail for each non-stock requisitioned item. This segment definition is paired with a preceding RQD segment.")]
    RequisitionDetail1,
    [SegmentInfo("RQD", SegmentChapter.OrderEntry)]
    [Description("RQD contains the detail for each requisitioned item.")]
    RequisitionDetail,
    [SegmentInfo("RXA", SegmentChapter.OrderEntry)]
    [Description("The ORC must have the filler order number and the order control code RE. As a site-specific variant, the RXO and associated RXCs and/or the RXE (and associated RXCs) may be present if the receiving application needs any of their data. The RXA carries the administration data.")]
    PharmacyTreatmentAdministration,
    [SegmentInfo("RXC", SegmentChapter.OrderEntry)]
    [Description("If the drug or treatment ordered with the RXO segment is a compound drug OR an IV solution, AND there is not a coded value for OBR-4-universal service ID, which specifies the components (base and all additives), then the components (the base and additives) are specified by two or more RXC segments. The policy of the pharmacy or treatment application on substitutions at the RXC level is identical to that for the RXO level.")]
    PharmacyTreatmentComponentOrder,
    [SegmentInfo("RXD", SegmentChapter.OrderEntry)]
    [Description("RXD records pharmacy dispense detail: what was dispensed, quantity, timing, lot, package, and pharmacy handling.")]
    PharmacyTreatmentDispense,
    [SegmentInfo("RXE", SegmentChapter.OrderEntry)]
    [Description("The RXE segment details the pharmacy or treatment application's encoding of the order. It also contains several pharmacy-specific order status fields, such as RXE-16-number of refills remaining, RXE-17-number of refills/doses dispensed, RXE-18-D/T of most recent refill or dose dispensed, and RXE-19-total daily dose. ")]
    PharmacyTreatmentEncodedOrder,
    [SegmentInfo("RXG", SegmentChapter.OrderEntry)]
    [Description("RXG describes a scheduled or given treatment instance in give-style pharmacy workflows.")]
    PharmacyTreatmentGive,
    [SegmentInfo("RXO", SegmentChapter.OrderEntry)]
    [Description("This is the \"master\" pharmacy/treatment order segment. It contains order data not specific to components or additives. Unlike the OBR, it does not contain status fields or other data that are results-only.")]
    PharmacyTreatmentOrder,
    [SegmentInfo("RXR", SegmentChapter.OrderEntry)]
    [Description("The Pharmacy/Treatment Route segment contains the alternative combination of route, site, administration device, and administration method that are prescribed as they apply to a particular order. The pharmacy, treatment staff and/or nursing staff has a choice between the routes based on either their professional judgment or administration instructions provided by the physician.")]
    PharmacyTreatmentRoute,
    [SegmentInfo("RXV", SegmentChapter.OrderEntry)]
    [Description("The RXV segment details the pharmacy or treatment application’s encoding of specific infusion order parameters.")]
    PharmacyTreatmentInfusion,
    [SegmentInfo("TQ1", SegmentChapter.OrderEntry)]
    [Description("The TQ1 segment is used to specify the complex timing of events and actions such as those that occur in order management and scheduling systems. This segment determines the quantity, frequency, priority and timing of a service. By allowing the segment to repeat, it is possible to have service requests that vary the quantity, frequency and priority of a service request over time.")]
    TimingQuantity,
    [SegmentInfo("TQ2", SegmentChapter.OrderEntry)]
    [Description("The TQ2 segment is used to form a relationship between the service request the TQ1/TQ2 segments are associated with, and other service requests. The TQ2 segment will link the current service request with one or more other service requests.")]
    TimingQuantityRelationship,
    
    [SegmentInfo("DSP", SegmentChapter.Query)]
    [Description("The DSP segment is used to contain data that has been preformatted by the sender for display. The\nsemantic content of the data is lost; the data is simply treated as lines of text.")]
    DisplayData,
    [SegmentInfo("QAK", SegmentChapter.Query)]
    [Description("The QAK segment contains information sent with responses to a query. The QAK segment may appear as\nan optional segment placed after the (optional) ERR segment in any query response (message) to any\noriginal mode query")]
    QueryAcknowledgement,
    [SegmentInfo("QID", SegmentChapter.Query)]
    [Description("The QID segment contains the information necessary to uniquely identify a query. Its primary use is in\nquery cancellation or subscription cancellation.\n")]
    QueryIdentification,
    [SegmentInfo("QPD", SegmentChapter.Query)]
    [Description("The QPD segment defines the parameters of the query.\n")]
    QueryParameterIdentification,
    [SegmentInfo("QRD", SegmentChapter.Query)]
    [Description("QRD is the older query-definition segment used before QPD/RCP became the cleaner pattern.")]
    [Obsolete("Withdrawn in v2.4")]
    OriginalStyleQueryDefinition,
    [SegmentInfo("QRF", SegmentChapter.Query)]
    [Description("QRF adds filter detail to older query messages.")]
    [Obsolete("Withdrawn in v2.4")]
    OriginalStyleQueryFilter,
    [SegmentInfo("QRI", SegmentChapter.Query)]
    [Description("The QRI segment is used to indicate the weight match for a returned record (where the responding system\nemploys a numeric algorithm) and/or the match reason code (where the responding system uses rules or\nother match options).")]
    QueryResponseInstance,
    [SegmentInfo("RCP", SegmentChapter.Query)]
    [Description("The RCP segment is used to restrict the amount of data that should be returned in response to query.\n")]
    ResponseControlParameter,
    [SegmentInfo("RDF", SegmentChapter.Query)]
    [Description("The RDF segment defines the content of the row data segments (RDT) in the tabular response (RTB).\n")]
    TableRowDefinition,
    [SegmentInfo("RDT", SegmentChapter.Query)]
    [Description("The RDT segment contains the row data of the tabular data response message (TBR).")]
    TableRowData,
    [SegmentInfo("URD", SegmentChapter.Query)]
    [Description("URD defines an unsolicited results/update request or subscription.")]
    ResultsUpdateDefinition,
    [SegmentInfo("URS", SegmentChapter.Query)]
    [Description("URS adds selection criteria for unsolicited update workflows.")]
    ResultsUpdateSelectionCriteria,
    
    [SegmentInfo("ABS", SegmentChapter.FinancialManagement)]
    [Description("")]
    Abstract,
    [SegmentInfo("ACC", SegmentChapter.FinancialManagement)]
    [Description("")]
    Accident,
    [SegmentInfo("BLC", SegmentChapter.FinancialManagement)]
    [Description("")]
    BloodCode,
    [SegmentInfo("DG1", SegmentChapter.FinancialManagement)]
    [Description("")]
    Diagnosis,
    [SegmentInfo("DRG", SegmentChapter.FinancialManagement)]
    [Description("")]
    DiagnosisRelatedGroup,
    [SegmentInfo("FT1", SegmentChapter.FinancialManagement)]
    [Description("")]
    FinancialTransaction,
    [SegmentInfo("GP1", SegmentChapter.FinancialManagement)]
    [Description("")]
    GroupingReimbursementVisit,
    [SegmentInfo("GP2", SegmentChapter.FinancialManagement)]
    [Description("")]
    GroupingReimbursementProcedureLineItem,
    [SegmentInfo("GT1", SegmentChapter.FinancialManagement)]
    [Description("")]
    Guarantor,
    [SegmentInfo("IN1", SegmentChapter.FinancialManagement)]
    [Description("")]
    Insurance,
    [SegmentInfo("IN2", SegmentChapter.FinancialManagement)]
    [Description("")]
    InsuranceAdditionaInformation,
    [SegmentInfo("IN3", SegmentChapter.FinancialManagement)]
    [Description("")]
    InsuranceAdditionalInformationCertification,
    [SegmentInfo("PR1", SegmentChapter.FinancialManagement)]
    [Description("")]
    Procedures,
    [SegmentInfo("RMI", SegmentChapter.FinancialManagement)]
    [Description("")]
    RiskManagementIncident,
    [SegmentInfo("UB1", SegmentChapter.FinancialManagement)]
    [Description("")]
    Ub82,
    [SegmentInfo("UB2", SegmentChapter.FinancialManagement)]
    [Description("")]
    UniformBillingData,
    
    [SegmentInfo("CSP", SegmentChapter.ObservationReporting)]
    [Description("")]
    ClinicalStudyPhase,
    [SegmentInfo("CSR", SegmentChapter.ObservationReporting)]
    [Description("")]
    ClinicalStudyRegistration,
    [SegmentInfo("CSS", SegmentChapter.ObservationReporting)]
    [Description("")]
    ClinicalStudyDataScheduleSegment,
    [SegmentInfo("CTI", SegmentChapter.ObservationReporting)]
    [Description("")]
    ClinicalTrialIdentification,
    [SegmentInfo("FAC", SegmentChapter.ObservationReporting)]
    [Description("")]
    Facility,
    [SegmentInfo("OBX", SegmentChapter.ObservationReporting)]
    [Description("")]
    ObservationResult,
    [SegmentInfo("PAC", SegmentChapter.ObservationReporting)]
    [Description("")]
    ShipmentPackage,
    [SegmentInfo("PCR", SegmentChapter.ObservationReporting)]
    [Description("")]
    PossibleCausalRelationship,
    [SegmentInfo("PDC", SegmentChapter.ObservationReporting)]
    [Description("")]
    ProductDetailCountry,
    [SegmentInfo("PEO", SegmentChapter.ObservationReporting)]
    [Description("")]
    ProductExperienceObservation,
    [SegmentInfo("PES", SegmentChapter.ObservationReporting)]
    [Description("")]
    ProductExperienceSender,
    [SegmentInfo("PRT", SegmentChapter.ObservationReporting)]
    [Description("")]
    ParticipationInformation,
    [SegmentInfo("PSH", SegmentChapter.ObservationReporting)]
    [Description("")]
    ProductSummaryHeader,
    [SegmentInfo("SHP", SegmentChapter.ObservationReporting)]
    [Description("")]
    Shipment,
    [SegmentInfo("SPM", SegmentChapter.ObservationReporting)]
    [Description("")]
    Specimen,
    
    [SegmentInfo("CDM", SegmentChapter.MasterFiles)]
    [Description("")]
    ChargeDescriptionMaster,
    [SegmentInfo("CM0", SegmentChapter.MasterFiles)]
    [Description("")]
    ClinicalStudyMaster,
    [SegmentInfo("CM1", SegmentChapter.MasterFiles)]
    [Description("")]
    ClinicalStudyPhaseMaster,
    [SegmentInfo("CM2", SegmentChapter.MasterFiles)]
    [Description("")]
    ClinicalStudyScheduleMaster,
    [SegmentInfo("CTR", SegmentChapter.MasterFiles)]
    [Description("")]
    ContractMasterOutbound,
    [SegmentInfo("DMI", SegmentChapter.MasterFiles)]
    [Description("")]
    DrgMasterFileInformation,
    [SegmentInfo("DPS", SegmentChapter.MasterFiles)]
    [Description("")]
    DiagnosisAndProcedureCodeSegment,
    [SegmentInfo("LCC", SegmentChapter.MasterFiles)]
    [Description("")]
    LocationChargeCode,
    [SegmentInfo("LCH", SegmentChapter.MasterFiles)]
    [Description("")]
    LocationCharacteristic,
    [SegmentInfo("LDP", SegmentChapter.MasterFiles)]
    [Description("")]
    LocationDepartment,
    [SegmentInfo("LOC", SegmentChapter.MasterFiles)]
    [Description("")]
    LocationIdentification,
    [SegmentInfo("LRL", SegmentChapter.MasterFiles)]
    [Description("")]
    LocationRelationship,
    [SegmentInfo("MCP", SegmentChapter.MasterFiles)]
    [Description("")]
    MasterFileCoverage,
    [SegmentInfo("MFA", SegmentChapter.MasterFiles)]
    [Description("")]
    MasterFileAcknowledgement,
    [SegmentInfo("MFE", SegmentChapter.MasterFiles)]
    [Description("")]
    MasterFileEntry,
    [SegmentInfo("MFI", SegmentChapter.MasterFiles)]
    [Description("")]
    MasterFileIdentification,
    [SegmentInfo("OM1", SegmentChapter.MasterFiles)]
    [Description("")]
    GeneralSegment,
    [SegmentInfo("OM2", SegmentChapter.MasterFiles)]
    [Description("")]
    NumericObservation,
    [SegmentInfo("OM3", SegmentChapter.MasterFiles)]
    [Description("")]
    CategoricalServiceTestObservation,
    [SegmentInfo("OM4", SegmentChapter.MasterFiles)]
    [Description("")]
    ObservationsThatRequireSpecimens,
    [SegmentInfo("OM5", SegmentChapter.MasterFiles)]
    [Description("")]
    ObservationBatteriesSets,
    [SegmentInfo("OM6", SegmentChapter.MasterFiles)]
    [Description("")]
    ObservationsThatAreCalculatedFromOtherObservations,
    [SegmentInfo("OM7", SegmentChapter.MasterFiles)]
    [Description("")]
    AdditionalBasicAttributes,
    [SegmentInfo("OMC", SegmentChapter.MasterFiles)]
    [Description("")]
    SupportingClinicalInformation,
    [SegmentInfo("PM1", SegmentChapter.MasterFiles)]
    [Description("")]
    PayerMasterFile,
    [SegmentInfo("PRC", SegmentChapter.MasterFiles)]
    [Description("")]
    Pricing,
    
    [SegmentInfo("CON", SegmentChapter.MedicalRecords_InformationManagement)]
    [Description("")]
    ConsentSegment,
    [SegmentInfo("TXA", SegmentChapter.MedicalRecords_InformationManagement)]
    [Description("")]
    TranscriptionDocumentHeader,
    
    [SegmentInfo("AIG", SegmentChapter.Scheduling)]
    [Description("")]
    AppointmentInformationGeneralResource,
    [SegmentInfo("AIL", SegmentChapter.Scheduling)]
    [Description("")]
    AppointmentInformationLocationResource,
    [SegmentInfo("AIP", SegmentChapter.Scheduling)]
    [Description("")]
    AppointmentInformationPersonnelResource,
    [SegmentInfo("AIS", SegmentChapter.Scheduling)]
    [Description("")]
    AppointmentInformation,
    [SegmentInfo("APR", SegmentChapter.Scheduling)]
    [Description("")]
    AppointmentPreference,
    [SegmentInfo("ARQ", SegmentChapter.Scheduling)]
    [Description("")]
    AppointmentRequest,
    [SegmentInfo("RGS", SegmentChapter.Scheduling)]
    [Description("")]
    ResourceGroup,
    [SegmentInfo("SCH", SegmentChapter.Scheduling)]
    [Description("")]
    SchedulingActivityInformation,
    
    [SegmentInfo("AUT", SegmentChapter.PatientReferral)]
    [Description("")]
    AuthorizationInformation,
    [SegmentInfo("CTD", SegmentChapter.PatientReferral)]
    [Description("")]
    ContactData,
    [SegmentInfo("PRD", SegmentChapter.PatientReferral)]
    [Description("")]
    ProviderData,
    [SegmentInfo("RF1", SegmentChapter.PatientReferral)]
    [Description("")]
    ReferralInformation,
    
    [SegmentInfo("GOL", SegmentChapter.PatientCare)]
    [Description("")]
    GoalDetail,
    [SegmentInfo("PRB", SegmentChapter.PatientCare)]
    [Description("")]
    ProblemDetails,
    [SegmentInfo("PTH", SegmentChapter.PatientCare)]
    [Description("")]
    Pathway,
    [SegmentInfo("REL", SegmentChapter.PatientCare)]
    [Description("")]
    ClinicalRelationshipSegment,
    [SegmentInfo("VAR", SegmentChapter.PatientCare)]
    [Description("")]
    Variance,
    
    [SegmentInfo("CNS", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    ClearNotification,
    [SegmentInfo("DST", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    TransportDestination,
    [SegmentInfo("ECD", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    EquipmentCommand,
    [SegmentInfo("ECR", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    EquipmentCommandResponse,
    [SegmentInfo("EQP", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    EquipmentLogService,
    [SegmentInfo("EQU", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    EquipmentDetail,
    [SegmentInfo("INV", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    InventoryDetail,
    [SegmentInfo("ISD", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    InteractionStatusDetail,
    [SegmentInfo("NDS", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    NotificationDetail,
    [SegmentInfo("SAC", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    SpecimenContainerDetail,
    [SegmentInfo("SID", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    SubstanceIdentifier,
    [SegmentInfo("TCC", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    TestCodeConfiguration,
    [SegmentInfo("TCD", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("")]
    TestCodeDetail,
    
    [SegmentInfo("NCK", SegmentChapter.ApplicationManagement)]
    [Description("")]
    SystemClock,
    [SegmentInfo("NSC", SegmentChapter.ApplicationManagement)]
    [Description("")]
    ApplicationStatusChange,
    [SegmentInfo("NST", SegmentChapter.ApplicationManagement)]
    [Description("")]
    ApplicationControlLevelStatistics,
    
    [SegmentInfo("AFF", SegmentChapter.PersonnelManagement)]
    [Description("")]
    ProfessionalAffiliation,
    [SegmentInfo("CER", SegmentChapter.PersonnelManagement)]
    [Description("")]
    CertificateDetail,
    [SegmentInfo("EDU", SegmentChapter.PersonnelManagement)]
    [Description("")]
    EducationalDetaul,
    [SegmentInfo("LAN", SegmentChapter.PersonnelManagement)]
    [Description("")]
    LanguageDetail,
    [SegmentInfo("ORG", SegmentChapter.PersonnelManagement)]
    [Description("")]
    PractitionerOrganizationUnit,
    [SegmentInfo("PRA", SegmentChapter.PersonnelManagement)]
    [Description("")]
    PractitionerDetail,
    [SegmentInfo("ROL", SegmentChapter.PersonnelManagement)]
    [Description("")]
    Role,
    [SegmentInfo("STF", SegmentChapter.PersonnelManagement)]
    [Description("")]
    StaffIdentification,
    
    [SegmentInfo("ADJ", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    Adjustment,
    [SegmentInfo("IPR", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    InvoiceProcessingResults,
    [SegmentInfo("IVC", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    InvoiceSegment,
    [SegmentInfo("PMT", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    PaymentInformation,
    [SegmentInfo("PSG", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    ProductServiceGroup,
    [SegmentInfo("PSL", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    ProductServiceLineItem,
    [SegmentInfo("PSS", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    ProductServiceSection,
    [SegmentInfo("PYE", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    PayeeInformation,
    [SegmentInfo("RFI", SegmentChapter.ClaimsAndReimbursement)]
    [Description("")]
    RequestForInformation,
    
    [SegmentInfo("DEV", SegmentChapter.MaterialsManagement)]
    [Description("")]
    DeviceSegment,
    [SegmentInfo("IIM", SegmentChapter.MaterialsManagement)]
    [Description("")]
    InventoryItemMaster,
    [SegmentInfo("ILT", SegmentChapter.MaterialsManagement)]
    [Description("")]
    MaterialLot,
    [SegmentInfo("ITM", SegmentChapter.MaterialsManagement)]
    [Description("")]
    MaterialItem,
    [SegmentInfo("IVT", SegmentChapter.MaterialsManagement)]
    [Description("")]
    MaterialLocation,
    [SegmentInfo("PCE", SegmentChapter.MaterialsManagement)]
    [Description("")]
    PatientChargeCostCenterExceptions,
    [SegmentInfo("PKG", SegmentChapter.MaterialsManagement)]
    [Description("")]
    ItemPackaging,
    [SegmentInfo("SCD", SegmentChapter.MaterialsManagement)]
    [Description("")]
    AntiMicrobialCycleData,
    [SegmentInfo("SCP", SegmentChapter.MaterialsManagement)]
    [Description("")]
    SterilizerConfiguration,
    [SegmentInfo("SDD", SegmentChapter.MaterialsManagement)]
    [Description("")]
    SterilizationDeviceData,
    [SegmentInfo("SLT", SegmentChapter.MaterialsManagement)]
    [Description("")]
    SterilizationLot,
    [SegmentInfo("STZ", SegmentChapter.MaterialsManagement)]
    [Description("")]
    SterilizationParameter,
    [SegmentInfo("VND", SegmentChapter.MaterialsManagement)]
    [Description("")]
    PurchasingVendor,
}