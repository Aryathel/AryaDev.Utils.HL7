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
    [Description("The DSP segment is used to contain data that has been preformatted by the sender for display. The semantic content of the data is lost; the data is simply treated as lines of text.")]
    DisplayData,
    [SegmentInfo("QAK", SegmentChapter.Query)]
    [Description("The QAK segment contains information sent with responses to a query. The QAK segment may appear as an optional segment placed after the (optional) ERR segment in any query response (message) to any original mode query")]
    QueryAcknowledgement,
    [SegmentInfo("QID", SegmentChapter.Query)]
    [Description("The QID segment contains the information necessary to uniquely identify a query. Its primary use is in query cancellation or subscription cancellation. ")]
    QueryIdentification,
    [SegmentInfo("QPD", SegmentChapter.Query)]
    [Description("The QPD segment defines the parameters of the query. ")]
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
    [Description("The QRI segment is used to indicate the weight match for a returned record (where the responding system employs a numeric algorithm) and/or the match reason code (where the responding system uses rules or other match options).")]
    QueryResponseInstance,
    [SegmentInfo("RCP", SegmentChapter.Query)]
    [Description("The RCP segment is used to restrict the amount of data that should be returned in response to query. ")]
    ResponseControlParameter,
    [SegmentInfo("RDF", SegmentChapter.Query)]
    [Description("The RDF segment defines the content of the row data segments (RDT) in the tabular response (RTB). ")]
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
    [Description("This segment was created to communicate patient abstract information used for billing and reimbursement purposes. \"Abstract\" is a condensed form of medical history created for analysis, care planning, etc.")]
    Abstract,
    [SegmentInfo("ACC", SegmentChapter.FinancialManagement)]
    [Description("The ACC segment contains patient information relative to an accident in which the patient has been involved.")]
    Accident,
    [SegmentInfo("BLC", SegmentChapter.FinancialManagement)]
    [Description("The BLC segment contains data necessary to communicate patient abstract blood information used for billing and reimbursement purposes. This segment is repeating to report blood product codes and the associated blood units.")]
    BloodCode,
    [SegmentInfo("DG1", SegmentChapter.FinancialManagement)]
    [Description("The DG1 segment contains patient diagnosis information of various types, for example, admitting, primary, etc. The DG1 segment is used to send multiple diagnoses (for example, for medical records encoding).")]
    Diagnosis,
    [SegmentInfo("DRG", SegmentChapter.FinancialManagement)]
    [Description("The DRG segment contains diagnoses-related grouping information of various types. The DRG segment is used to send the DRG information, for example, for billing and medical records encoding. ")]
    DiagnosisRelatedGroup,
    [SegmentInfo("FT1", SegmentChapter.FinancialManagement)]
    [Description("The FT1 segment contains the detail data necessary to post charges, payments, adjustments, etc., to patient accounting records.")]
    FinancialTransaction,
    [SegmentInfo("GP1", SegmentChapter.FinancialManagement)]
    [Description("These fields are used in grouping and reimbursement for CMS APCs. Please refer to the \"Outpatient Prospective Payment System Final Rule\" (\"OPPS Final Rule\") issued by CMS.")]
    GroupingReimbursementVisit,
    [SegmentInfo("GP2", SegmentChapter.FinancialManagement)]
    [Description("This segment is used for items that pertain to each HCPC/CPT line item.")]
    GroupingReimbursementProcedureLineItem,
    [SegmentInfo("GT1", SegmentChapter.FinancialManagement)]
    [Description("The GT1 segment contains guarantor (e.g., the person or the organization with financial responsibility for payment of a patient account) data for patient and insurance billing applications.")]
    Guarantor,
    [SegmentInfo("IN1", SegmentChapter.FinancialManagement)]
    [Description("The IN1 segment contains insurance policy coverage information necessary to produce properly pro-rated and patient and insurance bills. ")]
    Insurance,
    [SegmentInfo("IN2", SegmentChapter.FinancialManagement)]
    [Description("The IN2 segment contains additional insurance policy coverage and benefit information necessary for proper billing and reimbursement. Fields used by this segment are defined by CMS or other regulatory agencies.")]
    InsuranceAdditionalInformation,
    [SegmentInfo("IN3", SegmentChapter.FinancialManagement)]
    [Description("The IN3 segment contains additional insurance information for certifying the need for patient care. Fields used by this segment are defined by CMS, or other regulatory agencies.")]
    InsuranceAdditionalInformationCertification,
    [SegmentInfo("PR1", SegmentChapter.FinancialManagement)]
    [Description("The PR1 segment contains information relative to various types of procedures that can be performed on a patient. The PR1 segment can be used to send procedure information, for example: Surgical, Nuclear Medicine, X-ray with contrast, etc. The PR1 segment is used to send multiple procedures, for example, for medical records encoding or for billing systems.")]
    Procedures,
    [SegmentInfo("RMI", SegmentChapter.FinancialManagement)]
    [Description("The RMI segment is used to report an occurrence of an incident event pertaining or attaching to a patient encounter")]
    RiskManagementIncident,
    [SegmentInfo("UB1", SegmentChapter.FinancialManagement)]
    [Description("The UB1 segment contains data specific to the United States. Only billing/claims fields that do not exist in other HL7 defined segments appear in this segment. The codes listed as examples are not an exhaustive or current list.")]
    [Obsolete("The UB1 segment was deprecated as of v 2.3 and the detail was withdrawn and removed from the standard as of v 2.6.")]
    Ub82,
    [SegmentInfo("UB2", SegmentChapter.FinancialManagement)]
    [Description("The UB2 segment contains data necessary to complete UB92 bills specific to the United States. Realms outside the US are referred to chapter 16. Only Uniform Billing fields that do not exist in other HL7 defined segments appear in this segment. For example, Patient Name and Date of Birth are required; they are included in the PID segment and therefore do not appear here. Uniform Billing field locators are provided in parentheses ( ). The UB codes listed as examples are not an exhaustive or current list; refer to a UB specification for additional information.")]
    UniformBillingData,
    
    [SegmentInfo("CSP", SegmentChapter.ObservationReporting)]
    [Description("The CSP segment contains information on a patient's status for a particular phase of the study. This segment is optional and is useful when a study has different evaluation intervals within it.")]
    ClinicalStudyPhase,
    [SegmentInfo("CSR", SegmentChapter.ObservationReporting)]
    [Description("The CSR segment will contain fundamental administrative and regulatory information required to document a patient's enrollment on a clinical trial. This segment is all that is required if one needs to message another system that an enrollment has taken place, i.e., from clinical trials to pharmacy, accounting, or order entry systems.")]
    ClinicalStudyRegistration,
    [SegmentInfo("CSS", SegmentChapter.ObservationReporting)]
    [Description("The Clinical Study Data Schedule (CSS) segment is optional depending on whether messaging of study data needs to be linked to the scheduled data time points for the study.")]
    ClinicalStudyDataScheduleSegment,
    [SegmentInfo("CTI", SegmentChapter.ObservationReporting)]
    [Description("The CTI segment is an optional segment that contains information to identify the clinical trial, phase and time point with which an order or result is associated.")]
    ClinicalTrialIdentification,
    [SegmentInfo("FAC", SegmentChapter.ObservationReporting)]
    [Description("FAC describes a facility record, including identifiers, names, contact details, and service attributes.")]
    [Obsolete("This segment is maintained for backwards compatibility only as of v2.7.")]
    Facility,
    [SegmentInfo("OBX", SegmentChapter.ObservationReporting)]
    [Description("The OBX segment is used to transmit a single observation or observation fragment. It represents the smallest indivisible unit of a report. The OBX segment can also contain encapsulated data, e.g., a CDA document or a DICOM image. ")]
    ObservationResult,
    [SegmentInfo("PAC", SegmentChapter.ObservationReporting)]
    [Description("The intent of this segment is to describe the information associated with the shipping package specimens are sent in. ")]
    ShipmentPackage,
    [SegmentInfo("PCR", SegmentChapter.ObservationReporting)]
    [Description("The PCR segment is used to communicate a potential or suspected relationship between a product (drug or device) or test and an event with detrimental effect on a patient. This segment identifies a potential causal relationship between the product identified in this segment and the event identified in the PEO segment.")]
    PossibleCausalRelationship,
    [SegmentInfo("PDC", SegmentChapter.ObservationReporting)]
    [Description("PDC carries country-specific product detail, especially for product safety and regulatory reporting.")]
    [Obsolete("This segment is maintained for backwards compatibility only as of v2.7.")]
    ProductDetailCountry,
    [SegmentInfo("PEO", SegmentChapter.ObservationReporting)]
    [Description("Details related to a particular clinical experience or event are embodied in the PEO segment. This segment can be used to characterize an event which might be attributed to a product to which the patient was exposed. Products with a possible causal relationship to the observed experience are described in the following PCR (possible causal relationship) segments.")]
    ProductExperienceObservation,
    [SegmentInfo("PES", SegmentChapter.ObservationReporting)]
    [Description("PES identifies the sender and submission context for a product-experience report.")]
    ProductExperienceSender,
    [SegmentInfo("PRT", SegmentChapter.ObservationReporting)]
    [Description("The Participation Information segment contains the data necessary to add, update, correct, and delete from the record persons, organizations, devices, or locations (participants) participating in the activity being transmitted.")]
    ParticipationInformation,
    [SegmentInfo("PSH", SegmentChapter.ObservationReporting)]
    [Description("PSH is the product summary header for product safety or regulatory product reporting.")]
    [Obsolete("This segment is maintained for backwards compatibility only as of v2.7.")]
    ProductSummaryHeader,
    [SegmentInfo("SHP", SegmentChapter.ObservationReporting)]
    [Description("The intent of this segment is to describe the information associated with the transportation of the shipment. ")]
    Shipment,
    [SegmentInfo("SPM", SegmentChapter.ObservationReporting)]
    [Description("The intent of this segment is to describe the characteristics of a specimen. It differs from the intent of the OBR in that the OBR addresses order-specific information. It differs from the SAC segment in that the SAC addresses specimen container attributes")]
    Specimen,
    
    [SegmentInfo("CDM", SegmentChapter.MasterFiles)]
    [Description("CDM is a charge-description master row, used to distribute charge codes, prices, descriptions, and effective dates.")]
    ChargeDescriptionMaster,
    [SegmentInfo("CM0", SegmentChapter.MasterFiles)]
    [Description("CM0 is the master record for a clinical study, carrying the study identifier, sponsor, chair, timing, and related administrative details.")]
    ClinicalStudyMaster,
    [SegmentInfo("CM1", SegmentChapter.MasterFiles)]
    [Description("CM1 defines a phase within a clinical study, so messages can refer to trial stages consistently.")]
    ClinicalStudyPhaseMaster,
    [SegmentInfo("CM2", SegmentChapter.MasterFiles)]
    [Description("CM2 describes study schedule timing, including when events are planned within a clinical-study protocol.")]
    ClinicalStudyScheduleMaster,
    [SegmentInfo("CTR", SegmentChapter.MasterFiles)]
    [Description("The main purpose of this integration point is to assign the contract price to the supply items.")]
    ContractMasterOutbound,
    [SegmentInfo("DMI", SegmentChapter.MasterFiles)]
    [Description("DMI carries DRG Master File Information information in HL7 v2.9 messages.")]
    DrgMasterFileInformation,
    [SegmentInfo("DPS", SegmentChapter.MasterFiles)]
    [Description("When MFI-1 is MLCP (Medical Limited Coverage Process) this segment is identifing what is in limited coverage. When MFI-1 is MACP (Medical Approved Coverage Process) this segment is identifing what is approved. This segment defines the test that are approved for a given Diagnosis Code based on the Procedure Code.")]
    DiagnosisAndProcedureCodeSegment,
    [SegmentInfo("LCC", SegmentChapter.MasterFiles)]
    [Description("The optional LCC segment identifies how a patient location room can be billed by a certain department. A department can use different charge codes for the same room or bed, so there can be multiple LCC segments following an LDP segment.")]
    LocationChargeCode,
    [SegmentInfo("LCH", SegmentChapter.MasterFiles)]
    [Description("The LCH segment is used to identify location characteristics which determine which patients will be assigned to the room or bed. It contains the location characteristics of the room or bed identified in the preceding LOC segment. There should be one LCH segment for each attribute.")]
    LocationCharacteristic,
    [SegmentInfo("LDP", SegmentChapter.MasterFiles)]
    [Description("The LDP segment identifies how a patient location room is being used by a certain department. Multiple departments can use the same patient location, so there can be multiple LDP segments following an LOC segment. There must be at least one LDP segment for each LOC segment. This is not intended to include any current occupant information.")]
    LocationDepartment,
    [SegmentInfo("LOC", SegmentChapter.MasterFiles)]
    [Description("The LOC segment can identify any patient location referenced by information systems. This segment gives physical set up information about the location. This is not intended to include any current occupant or current use information.")]
    LocationIdentification,
    [SegmentInfo("LRL", SegmentChapter.MasterFiles)]
    [Description("The LRL segment is used to identify one location's relationship to another location, the nearest lab, pharmacy, etc.")]
    LocationRelationship,
    [SegmentInfo("MCP", SegmentChapter.MasterFiles)]
    [Description("The Technical Steward for the PM1 segment is Orders and Observations. For the payer defined in PM1-1 and the service provider defined in MFE-4: When MFI-1 is MLCP (Medical Limited Coverage Process) this segment is identifing what is in limited coverage. When MFI-1 is MACP (Medical Approved Coverage Process) this segment is identifing what is approved. This segment defines the tests that are approved for a given Diagnosis Code based on the Procedure Code.")]
    MasterFileCoverage,
    [SegmentInfo("MFA", SegmentChapter.MasterFiles)]
    [Description("MFA acknowledges the result of applying a master-file entry update.")]
    MasterFileAcknowledgement,
    [SegmentInfo("MFE", SegmentChapter.MasterFiles)]
    [Description("MFE describes one master-file entry action, including the key value and effective timing.")]
    MasterFileEntry,
    [SegmentInfo("MFI", SegmentChapter.MasterFiles)]
    [Description("MFI identifies the master file being maintained and sets the file-level update context.")]
    MasterFileIdentification,
    [SegmentInfo("OM1", SegmentChapter.MasterFiles)]
    [Description("OM1 is the general service/test/observation master record, the broad catalog entry many order and result workflows depend on.")]
    GeneralSegment,
    [SegmentInfo("OM2", SegmentChapter.MasterFiles)]
    [Description("OM2 adds numeric observation attributes such as ranges, units, precision, and expected values.")]
    NumericObservation,
    [SegmentInfo("OM3", SegmentChapter.MasterFiles)]
    [Description("OM3 adds categorical observation attributes and answer-set style configuration.")]
    CategoricalServiceTestObservation,
    [SegmentInfo("OM4", SegmentChapter.MasterFiles)]
    [Description("OM4 describes specimen requirements for observations and services.")]
    ObservationsThatRequireSpecimens,
    [SegmentInfo("OM5", SegmentChapter.MasterFiles)]
    [Description("OM5 defines observation batteries or sets, tying individual observations into ordered groups.")]
    ObservationBatteriesSets,
    [SegmentInfo("OM6", SegmentChapter.MasterFiles)]
    [Description("OM6 describes observations calculated from other observations.")]
    ObservationsThatAreCalculatedFromOtherObservations,
    [SegmentInfo("OM7", SegmentChapter.MasterFiles)]
    [Description("OM7 carries additional service/test attributes such as categorization, consent, departments, and reporting rules.")]
    AdditionalBasicAttributes,
    [SegmentInfo("OMC", SegmentChapter.MasterFiles)]
    [Description("")]
    SupportingClinicalInformation,
    [SegmentInfo("PM1", SegmentChapter.MasterFiles)]
    [Description("The PM1 segment contains per insurance company (payer) the policies specific to their organization. Trailing this segment in the message structure are either the Limited Coverage Policy or the Approved Coverage Policy. If an insurance company is listed they have limited coverage. Note, the first 10 fields come directly from the IN1 segment.")]
    PayerMasterFile,
    [SegmentInfo("PRC", SegmentChapter.MasterFiles)]
    [Description("PRC carries pricing rules for a charge, product, or service master item.")]
    Pricing,
    
    [SegmentInfo("CON", SegmentChapter.MedicalRecords_InformationManagement)]
    [Description("This segment identifies patient consent information relating to a particular message. It can be used as part of existing messages to convey information about patient consent to procedures, admissions, information release/exchange or other events discussed by the message.")]
    ConsentSegment,
    [SegmentInfo("TXA", SegmentChapter.MedicalRecords_InformationManagement)]
    [Description("The TXA segment contains information specific to a transcribed document but does not include the text of the document. The message is created as a result of a document status change. This information updates other healthcare systems and allows them to identify reports that are available in the transcription system.")]
    TranscriptionDocumentHeader,
    
    [SegmentInfo("AIG", SegmentChapter.Scheduling)]
    [Description("The AIG segment contains information about various kinds of resources (other than those with specifically defined segments in this chapter) that can be scheduled. Resources included in a transaction using this segment are assumed to be controlled by a schedule on a schedule filler application. Resources not controlled by a schedule are not identified on a schedule request using this segment. Resources described by this segment are general kinds of resources, such as equipment, that are identified with a simple identification code.")]
    AppointmentInformationGeneralResource,
    [SegmentInfo("AIL", SegmentChapter.Scheduling)]
    [Description("The AIL segment contains information about location resources (meeting rooms, operating rooms, examination rooms, or other locations) that can be scheduled. Resources included in a transaction using this segment are assumed to be controlled by a schedule on a schedule filler application. Resources not controlled by a schedule are not identified on a schedule request using this segment. Location resources are identified with this specific segment because of the specific encoding of locations used by the HL7 specification.")]
    AppointmentInformationLocationResource,
    [SegmentInfo("AIP", SegmentChapter.Scheduling)]
    [Description("The AIP segment contains information about the personnel types that can be scheduled. Personnel included in a transaction using this segment are assumed to be controlled by a schedule on a schedule filler application. Personnel not controlled by a schedule are not identified on a schedule request using this segment. The kinds of personnel described on this segment include any healthcare provider in the institution controlled by a schedule (for example: technicians, physicians, nurses, surgeons, anesthesiologists, or CRNAs).")]
    AppointmentInformationPersonnelResource,
    [SegmentInfo("AIS", SegmentChapter.Scheduling)]
    [Description("The AIS segment contains information about various kinds of services that can be scheduled. Services included in a transaction using this segment are assumed to be controlled by a schedule on a schedule filler application. Services not controlled by a schedule are not identified on a schedule request using this segment.")]
    AppointmentInformation,
    [SegmentInfo("APR", SegmentChapter.Scheduling)]
    [Description("The APR segment contains parameters and preference specifications used for requesting appointments in the SRM message. It allows placer applications to provide coded parameters and preference indicators to the filler application, to help determine when a requested appointment should be scheduled. An APR segment can be provided in conjunction with either the ARQ segment or any of the service and resource segments (AIG, AIS, AIP , and AIL). If an APR segment appears in conjunction with an ARQ segment, its parameters and preference indicators pertain to the schedule request as a whole. If the APR segment appears with any of the service and resource segments, then its parameters and preferences apply only to the immediately preceding service or resource.")]
    AppointmentPreference,
    [SegmentInfo("ARQ", SegmentChapter.Scheduling)]
    [Description("The ARQ segment defines a request for the booking of an appointment. It is used in transactions sent from an application acting in the role of a placer.")]
    AppointmentRequest,
    [SegmentInfo("RGS", SegmentChapter.Scheduling)]
    [Description("The RGS segment is used to identify relationships between resources identified for a scheduled event. This segment can be used, on a site specified basis, to identify groups of resources that are used together within a scheduled event, or to describe some other relationship between resources. To specify related groups of resources within a message, begin each group with an RGS segment, and then follow that RGS with one or more of the Appointment Information segments (AIG, AIL, AIS, or AIP). If a message does not require any grouping of resources, then specify a single RGS in the message, and follow it with all of the Appointment Information segments for the scheduled event. (At least one RGS segment is required in each message - even if no grouping of resources is required - to allow parsers to properly understand the message.)")]
    ResourceGroup,
    [SegmentInfo("SCH", SegmentChapter.Scheduling)]
    [Description("The SCH segment contains general information about the scheduled appointment.")]
    SchedulingActivityInformation,
    
    [SegmentInfo("AUT", SegmentChapter.PatientReferral)]
    [Description("This segment represents an authorization or a pre-authorization for a referred procedure or requested service by the payor covering the patient's health care.")]
    AuthorizationInformation,
    [SegmentInfo("CTD", SegmentChapter.PatientReferral)]
    [Description("The CTD segment may identify any contact personnel associated with a patient referral message and its related transactions. The CTD segment will be paired with a PRD segment. The PRD segment contains data specifically focused on provider information in a referral. While it is important in an inter-enterprise transaction to transmit specific information regarding the providers involved (referring and referred-to), it may also be important to identify the contact personnel associated with the given provider. For example, a provider receiving a referral may need to know the office manager or the billing person at the institution of the provider who sent the referral. This segment allows for multiple contact personnel to be associated with a single provider.")]
    ContactData,
    [SegmentInfo("PRD", SegmentChapter.PatientReferral)]
    [Description("This segment will be employed as part of a patient referral message and its related transactions. The PRD segment contains data specifically focused on a referral, and it is inter-enterprise in nature. The justification for this new segment comes from the fact that we are dealing with referrals that are external to the facilities that received them. Therefore, using a segment such as the current PV1 would be inadequate for all the return information that may be required by the receiving facility or application. In addition, the PV1 does not always provide information sufficient to enable the external facility to make a complete identification of the referring entity. The information contained in the PRD segment will include the referring provider, the referred-to provider, the referred-to location or service, and the referring provider clinic address.")]
    ProviderData,
    [SegmentInfo("RF1", SegmentChapter.PatientReferral)]
    [Description("This segment represents information that may be useful when sending referrals from the referring provider to the referred-to provider.")]
    ReferralInformation,
    
    [SegmentInfo("GOL", SegmentChapter.PatientCare)]
    [Description("The goal detail segment contains the data necessary to add, update, correct, and delete the goals for an individual.")]
    GoalDetail,
    [SegmentInfo("PRB", SegmentChapter.PatientCare)]
    [Description("The problem detail segment contains the data necessary to add, update, correct, and delete the problems of a given individual.")]
    ProblemDetails,
    [SegmentInfo("PTH", SegmentChapter.PatientCare)]
    [Description("The pathway segment contains the data necessary to add, update, correct, and delete from the record pathways that are utilized to address an individual's health care.")]
    Pathway,
    [SegmentInfo("REL", SegmentChapter.PatientCare)]
    [Description("The Clinical Relationship segment contains the data necessary to relate two data elements within and/or external to the message at run-time. It also includes information about the relationship. Relationships are constrained to being between explicit segments of messages rather than between the identities of entities they reference. Segments are available within the message but related persistent information may not be. Because of the transient nature of messages applications must manage the associations with entities which persist outside or across messages. Examples: Problem is a consequence of a diagnosis. Diagnosis is supported by observation. Observation is a manifestation of a diagnosis. Complication is a consequence of a procedure.")]
    ClinicalRelationshipSegment,
    [SegmentInfo("VAR", SegmentChapter.PatientCare)]
    [Description("The variance segment contains the data necessary to describe differences that may have occurred at the time when a healthcare event was documented.")]
    Variance,
    
    [SegmentInfo("CNS", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The clear equipment notification segment contains the data necessary to allow the receiving equipment to clear any associated notifications.")]
    ClearNotification,
    [SegmentInfo("DST", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The Transport Destination segment contains information relevant for transport of the specimen container to specific destination on the specific equipment. This segment should be used in conjunction with the TT, AF, and AT commands of the ECD segment used in the EAC message.")]
    TransportDestination,
    [SegmentInfo("ECD", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The equipment command segment contains the information required to notify the receiving component what is to happen.")]
    EquipmentCommand,
    [SegmentInfo("ECR", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The equipment command response segment contains the receiving component's response to the previously received command.")]
    EquipmentCommandResponse,
    [SegmentInfo("EQP", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The equipment log/service segment is the data necessary to maintain an adequate audit trail of events that have occurred on a particular piece of equipment.")]
    EquipmentLogService,
    [SegmentInfo("EQU", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The equipment detail segment contains the data necessary to identify and maintain the equipment that is being used throughout the Laboratory Automation System.")]
    EquipmentDetail,
    [SegmentInfo("INV", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The inventory detail segment is the data necessary to track the inventory of substances (e.g. reagent, tips, waste) and equipment state indicators (a special type of non-material inventory items) on equipment.")]
    InventoryDetail,
    [SegmentInfo("ISD", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The interaction detail segment contains information about the status of specific interaction (e.g., processing - see section Glossary) on the specific equipment.")]
    InteractionStatusDetail,
    [SegmentInfo("NDS", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The equipment notification detail segment is the data necessary to maintain an adequate audit trail as well as notifications of events, (e.g., alarms that have occurred on a particular piece of equipment).")]
    NotificationDetail,
    [SegmentInfo("SAC", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The container detail segment is the data necessary to maintain the containers that are being used throughout the Laboratory Automation System. The specimens in many laboratories are transported and processed in containers (e.g., sample tubes). When SPM and SAC are used in the same message, then the conceptually duplicate attributes will be valued only in the SPM. This applies to SAC-6 Specimen Source, SAC-27 Additives, and SAC-43 Special Handling Considerations.")]
    SpecimenContainerDetail,
    [SegmentInfo("SID", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The Substance Identifier segment contains data necessary to identify the substance (e.g., reagents) used in the production of analytical test results. The combination of these fields must uniquely identify the substance, i.e., depending on the manufacturer all or some fields are required (this is the reason the optionality is 'C' (conditional)). If the analysis requires multiple substances, this segment is repeated for each substance. The segment(s) should be attached to the TCD segment. Another purpose of this segment is to transfer the control manufacturer, lot, etc., information for control specimens. In this case the SID segment should be attached to the SAC segment describing the container with the control specimen.")]
    SubstanceIdentifier,
    [SegmentInfo("TCC", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The test (e.g., analyte) code configuration segment is the data necessary to maintain and transmit information concerning the test entity codes that are being used throughout the \"automated system.\"")]
    TestCodeConfiguration,
    [SegmentInfo("TCD", SegmentChapter.ClinicalLaboratoryAutomation)]
    [Description("The test code detail segment contains the data necessary to perform operations or calculations, or execute decisions by the laboratory automation system, and which are not supported by the original HL7 segments related to orders (ORC, OBR). For detail of use see messages of laboratory orders and observations in chapters 4 and 7.")]
    TestCodeDetail,
    
    [SegmentInfo("NCK", SegmentChapter.ApplicationManagement)]
    [Description("The NCK segment is used to allow the various applications on the network to synchronize their system clocks (system date and time). Usage Notes: If this message is to be used to automatically reset/correct system clocks, it is recommended that the system or administrative personnel initiating the NMQ with the NCK segment have the authority to correct the clock (system date and time) for the other systems on the network. This is important in order to avoid the obvious confusion of multiple systems attempting to resynchronize each other's clocks. If this message is used only to gather information on the various systems' clocks, it is still important for an administrative procedure to be worked out to avoid conflicts when resetting clocks.")]
    SystemClock,
    [SegmentInfo("NSC", SegmentChapter.ApplicationManagement)]
    [Description("The NSC segment is used to inform (NMR query response) or announce (NMD unsolicited update) the start-up, shut-down, and/or migration (to a different CPU or file-server/file-system) of a particular application. Usage Notes: Fields 2-9. These are not applicable (\"n/a\") when the type of change being requested or reported is start-up or shut-down. If the change is of type \"M\", at least one of fields 2-5 must be different from its corresponding field in range 6-9. Fields 4-5, 8-9. See definitions for the MSH, message header segment, in Chapter 2, \"Control Section,\" for fields 3-4, for system and facility. \"Application\" is available for interfacing with lower level protocols. \"Facility\" is entirely site-defined. Fields 2-3, 6-7: entirely site-defined.")]
    ApplicationStatusChange,
    [SegmentInfo("NST", SegmentChapter.ApplicationManagement)]
    [Description("The NST segment allows application control-level statistical information to be passed between the various systems on the network. Some fields in this segment refer to portions of lower level protocols; they contain information that can be used by application management applications monitoring the state of various network links. Usage Notes: Fields 2-15. These are all marked optional since the statistics kept on a particular link and negotiated between the two systems in question will vary. Not all values will apply to each system. Some values are concerned with the type of port, and some values pertain to the lower level protocol.")]
    ApplicationControlLevelStatistics,
    
    [SegmentInfo("AFF", SegmentChapter.PersonnelManagement)]
    [Description("The AFF segment adds detailed information regarding professional affiliations with which the staff member identified by the STF segment is/was associated.")]
    ProfessionalAffiliation,
    [SegmentInfo("CER", SegmentChapter.PersonnelManagement)]
    [Description("The CER segment adds detailed information regarding the formal authorizations to provide a service (e.g. licenses, certificates) held by the health professional identified by the STF segment.")]
    CertificateDetail,
    [SegmentInfo("EDU", SegmentChapter.PersonnelManagement)]
    [Description("The EDU segment adds detailed educational information to the staff member identified by the STF segment. An EDU segment may optionally follow an STF segment. An EDU segment must always have been preceded by a corresponding STF segment.")]
    EducationalDetaul,
    [SegmentInfo("LAN", SegmentChapter.PersonnelManagement)]
    [Description("The LAN segment adds detailed language information to the staff member identified by the STF segment. An LAN segment may optionally follow an STF segment. An LAN segment must always have been preceded by a corresponding STF segment.")]
    LanguageDetail,
    [SegmentInfo("ORG", SegmentChapter.PersonnelManagement)]
    [Description("The ORG segment relates a practitioner to an organization unit and adds detailed information regarding the practitioner's practicing specialty in that organization unit. An ORG segment may optionally follow an STF segment. An ORG segment must always have been preceded by a corresponding STF segment. If no organization unit is specified, this segment is used to relate practitioners with their practicing specialties, including effective and end dates. When it is not necessary to record organization unit or dates associated with the practicing specialty, this data is recorded in PRA-3-Practitioner Category.")]
    PractitionerOrganizationUnit,
    [SegmentInfo("PRA", SegmentChapter.PersonnelManagement)]
    [Description("The Technical Steward for the PRA segment is P A and Personnel Management. The PRA segment adds detailed medical practitioner information to the personnel identified by the STF segment. A PRA segment may optionally follow an STF segment. A PRA segment must always have been preceded by a corresponding STF segment. The PRA segment may also be used for staff who work in healthcare who are not practitioners but need to be certified, e.g., \"medical records staff.\"")]
    PractitionerDetail,
    [SegmentInfo("ROL", SegmentChapter.PersonnelManagement)]
    [Description("The role segment contains the data necessary to add, update, correct, and delete from the record persons involved, as well as their functional involvement with the activity being transmitted. In general, the ROL segment is used to describe a person playing a particular role within the context of the message. In PM, for example, in the Grant Certificate/Permission message (B07), the ROL segment is used to describe the roles a person may perform pertinent to the certificate in the message.")]
    Role,
    [SegmentInfo("STF", SegmentChapter.PersonnelManagement)]
    [Description("The Technical Steward for the STF segment is P A and Personnel Management. The STF segment can identify any personnel referenced by information systems. These can be providers, staff, system users, and referring agents. In a network environment, this segment can be used to define personnel to other applications, for example, order entry clerks, insurance verification clerks, admission clerks, as well as provider demographics. When using the STF and PRA segments in the Staff/Practitioner Master File message, MFE-4-primary key value is used to link all the segments pertaining to the same master file entry. Therefore, in the MFE segment, MFE-4-primary key value must be filled in. Other segments may follow the STF segment to provide data for a particular type of staff member. The PRA segment (practitioner) is one such. It may optionally follow the STF segment in order to add practitioner- specific data. Other segments may be defined as needed. When using the segments included in this chapter for other then Staff/Practitioner Master File messages, disregard references to MFE-4 - primary key value.")]
    StaffIdentification,
    
    [SegmentInfo("ADJ", SegmentChapter.ClaimsAndReimbursement)]
    [Description("This segment describes Provider and/or Payer adjustments to a Product/Service Line Item or Response Summary. These include surcharges such as tax, dispensing fees and mark ups.")]
    Adjustment,
    [SegmentInfo("IPR", SegmentChapter.ClaimsAndReimbursement)]
    [Description("The Invoice Processing Results (IPR) segment provides summary information about an adjudicated Product/Service Group or Product/Service Line Item.")]
    InvoiceProcessingResults,
    [SegmentInfo("IVC", SegmentChapter.ClaimsAndReimbursement)]
    [Description("The Invoice segment is used for HealthCare Services Invoices and contains header style information for an invoice including invoice numbers, Provider Organization and Payer Organization identification.")]
    InvoiceSegment,
    [SegmentInfo("PMT", SegmentChapter.ClaimsAndReimbursement)]
    [Description("This segment contains information that describes a payment made by a Payer organization.")]
    PaymentInformation,
    [SegmentInfo("PSG", SegmentChapter.ClaimsAndReimbursement)]
    [Description("The Product/Service Group segment is used to form a logical grouping of Product/Service Line Items, Patients and Response Summaries for a particular Invoice. For example, a Product/Service Group can be used to group all Product/Service Line Items that must be adjudicated as a group in order to be paid.")]
    ProductServiceGroup,
    [SegmentInfo("PSL", SegmentChapter.ClaimsAndReimbursement)]
    [Description("The Product/Service Line Item segment is used to identify individual product/service items that typically are aggregated into an Invoice. Each instance of a Product/Service Line Item corresponds to a unique product delivered or service rendered.")]
    ProductServiceLineItem,
    [SegmentInfo("PSS", SegmentChapter.ClaimsAndReimbursement)]
    [Description("The Product/Service Section segment is used to form a logical grouping of Product/Service Group segments, Patients and Response Summaries for a particular Invoice.")]
    ProductServiceSection,
    [SegmentInfo("PYE", SegmentChapter.ClaimsAndReimbursement)]
    [Description("This segment is used to define payee information.")]
    PayeeInformation,
    [SegmentInfo("RFI", SegmentChapter.ClaimsAndReimbursement)]
    [Description("The RFI segment contains the request date, response due date, patient consent indicator, and date additional information was submitted for a request for additional information in support of an invoice or authorization.")]
    RequestForInformation,
    
    [SegmentInfo("DEV", SegmentChapter.MaterialsManagement)]
    [Description("The Device segment identifies an instance or a type of a manufactured item that is used in the provision of healthcare without being substantially changed through that activity. The device may be a medical or non-medical device. Medical devices include durable (reusable) medical equipment, implantable devices, as well as disposable equipment used for diagnostic, treatment, and research for healthcare and public health. Non-medical devices may include items such as a machine, cellphone, computer, application, etc.")]
    DeviceSegment,
    [SegmentInfo("IIM", SegmentChapter.MaterialsManagement)]
    [Description("The Inventory Item Master segment (IIM) contains information about the stock of product that can be used to fulfill an ordered test/service. All of the fields in this segment describe the test/service and other basic attributes pertaining to Service Item defined within an Other Observation/Service Item master file. This segment is related to centrally stocked or supply management concerns.")]
    InventoryItemMaster,
    [SegmentInfo("ILT", SegmentChapter.MaterialsManagement)]
    [Description("The Material Lot segment (ILT) contains material information specific to a lot within an inventory location associated with the item in the IVT segment. This segment is similar to the IIM segment used with the limited inventory item master message.")]
    MaterialLot,
    [SegmentInfo("ITM", SegmentChapter.MaterialsManagement)]
    [Description("The Material Item segment (ITM) contains information about inventory supply items (stocked or non-stocked).")]
    MaterialItem,
    [SegmentInfo("IVT", SegmentChapter.MaterialsManagement)]
    [Description("The Material Location segment (IVT) contains information specific to an inventory location for the inventory supply item in the Material Item (ITM) segment.")]
    MaterialLocation,
    [SegmentInfo("PCE", SegmentChapter.MaterialsManagement)]
    [Description("The Patient Charge Cost Center Exception segment identifies the Patient Price associated with Cost Center and Patient Charge Identifier combinations that should be used in an instance that the item is billed to a patient. The grouping of Cost Center accounts, Patient Charge Identifier, and Patient Price is unique.")]
    PatientChargeCostCenterExceptions,
    [SegmentInfo("PKG", SegmentChapter.MaterialsManagement)]
    [Description("This segment contains the type of packaging available for the inventory supply item to be ordered and/or issued to a department or other supply location for a specified Purchasing Vendor. It would be recommended to send this segment in descending unit of measure order corresponding with the ascending Set ID.")]
    ItemPackaging,
    [SegmentInfo("SCD", SegmentChapter.MaterialsManagement)]
    [Description("The SCD segment contains cycle data representing an instance of a sterilization or decontamination.")]
    AntiMicrobialCycleData,
    [SegmentInfo("SCP", SegmentChapter.MaterialsManagement)]
    [Description("The sterilization configuration segment contains information specific to configuration of a sterilizer or washer for processing sterilization or decontamination loads.")]
    SterilizerConfiguration,
    [SegmentInfo("SDD", SegmentChapter.MaterialsManagement)]
    [Description("The SDD segment contains the attributes of an instance of a cycle that provides sterilization or decontamination of medical supplies.")]
    SterilizationDeviceData,
    [SegmentInfo("SLT", SegmentChapter.MaterialsManagement)]
    [Description("The SLT segment defines requests, responses, and notifications of sterilization lots and supply item descriptions. This message may be used for CPD (Central Supply) and OR (Sub-sterile area outside of an Operating Room) mode.")]
    SterilizationLot,
    [SegmentInfo("STZ", SegmentChapter.MaterialsManagement)]
    [Description("The STZ segment contains sterilization-specific attributes of a supply item.")]
    SterilizationParameter,
    [SegmentInfo("VND", SegmentChapter.MaterialsManagement)]
    [Description("This segment contains purchasing vendors that supply the inventory supply item specified in the ITM segment.")]
    PurchasingVendor,
}