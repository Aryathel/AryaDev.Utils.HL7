using System.ComponentModel;
using AryaDev.Utils.HL7.Domain.Attributes;

namespace AryaDev.Utils.HL7.Domain.Enumeration;

/// <summary>
/// HL7 message types / Table 0354 message structures (MSH-9.3). Referenced from
/// <see href="https://www.hl7.org/implement/standards/product_brief.cfm?product_id=649">HL7 v2.9.1</see>.
/// </summary>
public enum MessageType
{
    /// <summary>Unrecognized, missing, or unparsable MSH-9 message type.</summary>
    [MessageTypeInfo("")]
    [Description("Unrecognized, missing, or unparsable MSH-9 message type.")]
    Unknown = 0,

    [MessageTypeInfo("ACK")]
    [Description("")]
    ACK,

    [MessageTypeInfo("ADR_A19")]
    [Description("")]
    ADR_A19,

    [MessageTypeInfo("ADT_A01")]
    [Description("A01, A04, A08, A13")]
    ADT_A01,

    [MessageTypeInfo("ADT_A02")]
    [Description("A02")]
    ADT_A02,

    [MessageTypeInfo("ADT_A03")]
    [Description("A03")]
    ADT_A03,

    [MessageTypeInfo("ADT_A05")]
    [Description("A05, A14, A28, A31")]
    ADT_A05,

    [MessageTypeInfo("ADT_A06")]
    [Description("A06, A07")]
    ADT_A06,

    [MessageTypeInfo("ADT_A09")]
    [Description("A09, A10, A11")]
    ADT_A09,

    [MessageTypeInfo("ADT_A12")]
    [Description("A12")]
    ADT_A12,

    [MessageTypeInfo("ADT_A15")]
    [Description("A15")]
    ADT_A15,

    [MessageTypeInfo("ADT_A16")]
    [Description("A16")]
    ADT_A16,

    [MessageTypeInfo("ADT_A17")]
    [Description("A17")]
    ADT_A17,

    [MessageTypeInfo("ADT_A18")]
    [Description("")]
    ADT_A18,

    [MessageTypeInfo("ADT_A20")]
    [Description("A20")]
    ADT_A20,

    [MessageTypeInfo("ADT_A21")]
    [Description("A21, A22, A23, A25, A26, A27, A29, A32, A33")]
    ADT_A21,

    [MessageTypeInfo("ADT_A24")]
    [Description("A24")]
    ADT_A24,

    [MessageTypeInfo("ADT_A30")]
    [Description("")]
    ADT_A30,

    [MessageTypeInfo("ADT_A37")]
    [Description("A37")]
    ADT_A37,

    [MessageTypeInfo("ADT_A38")]
    [Description("A38")]
    ADT_A38,

    [MessageTypeInfo("ADT_A39")]
    [Description("A39, A40, A41, A42")]
    ADT_A39,

    [MessageTypeInfo("ADT_A43")]
    [Description("A43")]
    ADT_A43,

    [MessageTypeInfo("ADT_A44")]
    [Description("A44")]
    ADT_A44,

    [MessageTypeInfo("ADT_A45")]
    [Description("A45")]
    ADT_A45,

    [MessageTypeInfo("ADT_A50")]
    [Description("A50, A51")]
    ADT_A50,

    [MessageTypeInfo("ADT_A52")]
    [Description("A52, A53")]
    ADT_A52,

    [MessageTypeInfo("ADT_A54")]
    [Description("A54, A55")]
    ADT_A54,

    [MessageTypeInfo("ADT_A60")]
    [Description("A60")]
    ADT_A60,

    [MessageTypeInfo("ADT_A61")]
    [Description("A61, A62")]
    ADT_A61,

    [MessageTypeInfo("BAR_P01")]
    [Description("P01")]
    BAR_P01,

    [MessageTypeInfo("BAR_P02")]
    [Description("P02")]
    BAR_P02,

    [MessageTypeInfo("BAR_P05")]
    [Description("P05")]
    BAR_P05,

    [MessageTypeInfo("BAR_P06")]
    [Description("P06")]
    BAR_P06,

    [MessageTypeInfo("BAR_P10")]
    [Description("P10")]
    BAR_P10,

    [MessageTypeInfo("BAR_P12")]
    [Description("P12")]
    BAR_P12,

    [MessageTypeInfo("BPS_O29")]
    [Description("O29")]
    BPS_O29,

    [MessageTypeInfo("BRP_O30")]
    [Description("O30")]
    BRP_O30,

    [MessageTypeInfo("BRT_O32")]
    [Description("O32")]
    BRT_O32,

    [MessageTypeInfo("BTS_O31")]
    [Description("O31")]
    BTS_O31,

    [MessageTypeInfo("CCF_I22")]
    [Description("I22")]
    CCF_I22,

    [MessageTypeInfo("CCI_I22")]
    [Description("I22")]
    CCI_I22,

    [MessageTypeInfo("CCM_I21")]
    [Description("I21")]
    CCM_I21,

    [MessageTypeInfo("CCQ_I19")]
    [Description("I19")]
    CCQ_I19,

    [MessageTypeInfo("CCR_I16")]
    [Description("I16, I17, I18")]
    CCR_I16,

    [MessageTypeInfo("CCU_I20")]
    [Description("I20")]
    CCU_I20,

    [MessageTypeInfo("CQU_I19")]
    [Description("I19")]
    CQU_I19,

    [MessageTypeInfo("CRM_C01")]
    [Description("C01, C02, C03, C04, C05, C06, C07, C08")]
    CRM_C01,

    [MessageTypeInfo("CSU_C09")]
    [Description("C09, C10, C11, C12")]
    CSU_C09,

    [MessageTypeInfo("DFT_P03")]
    [Description("P03")]
    DFT_P03,

    [MessageTypeInfo("DFT_P11")]
    [Description("P11")]
    DFT_P11,

    [MessageTypeInfo("DOC_T12")]
    [Description("")]
    DOC_T12,

    [MessageTypeInfo("EAC_U07")]
    [Description("U07")]
    EAC_U07,

    [MessageTypeInfo("EAN_U09")]
    [Description("U09")]
    EAN_U09,

    [MessageTypeInfo("EAR_U08")]
    [Description("U08")]
    EAR_U08,

    [MessageTypeInfo("EHC_E01")]
    [Description("E01")]
    EHC_E01,

    [MessageTypeInfo("EHC_E02")]
    [Description("E02")]
    EHC_E02,

    [MessageTypeInfo("EHC_E04")]
    [Description("E04")]
    EHC_E04,

    [MessageTypeInfo("EHC_E10")]
    [Description("E10")]
    EHC_E10,

    [MessageTypeInfo("EHC_E12")]
    [Description("E12")]
    EHC_E12,

    [MessageTypeInfo("EHC_E13")]
    [Description("E13")]
    EHC_E13,

    [MessageTypeInfo("EHC_E15")]
    [Description("E15")]
    EHC_E15,

    [MessageTypeInfo("EHC_E20")]
    [Description("E20")]
    EHC_E20,

    [MessageTypeInfo("EHC_E21")]
    [Description("E21")]
    EHC_E21,

    [MessageTypeInfo("EHC_E24")]
    [Description("E24")]
    EHC_E24,

    [MessageTypeInfo("ESR_U02")]
    [Description("U02")]
    ESR_U02,

    [MessageTypeInfo("ESU_U01")]
    [Description("U01")]
    ESU_U01,

    [MessageTypeInfo("INR_U06")]
    [Description("U06")]
    INR_U06,

    [MessageTypeInfo("INU_U05")]
    [Description("U05")]
    INU_U05,

    [MessageTypeInfo("LSU_U12")]
    [Description("U12, U13")]
    LSU_U12,

    [MessageTypeInfo("MDM_T01")]
    [Description("T01, T03, T05, T07, T09, T11")]
    MDM_T01,

    [MessageTypeInfo("MDM_T02")]
    [Description("T02, T04, T06, T08, T10")]
    MDM_T02,

    [MessageTypeInfo("MFK_M01")]
    [Description("M01, M02, M03, M04, M05, M06, M07, M08, M09, M10, M11")]
    MFK_M01,

    [MessageTypeInfo("MFN_M01")]
    [Description("")]
    MFN_M01,

    [MessageTypeInfo("MFN_M02")]
    [Description("M02")]
    MFN_M02,

    [MessageTypeInfo("MFN_M03")]
    [Description("M03")]
    MFN_M03,

    [MessageTypeInfo("MFN_M04")]
    [Description("M04")]
    MFN_M04,

    [MessageTypeInfo("MFN_M05")]
    [Description("M05")]
    MFN_M05,

    [MessageTypeInfo("MFN_M06")]
    [Description("M06")]
    MFN_M06,

    [MessageTypeInfo("MFN_M07")]
    [Description("M07")]
    MFN_M07,

    [MessageTypeInfo("MFN_M08")]
    [Description("M08")]
    MFN_M08,

    [MessageTypeInfo("MFN_M09")]
    [Description("M09")]
    MFN_M09,

    [MessageTypeInfo("MFN_M10")]
    [Description("M10")]
    MFN_M10,

    [MessageTypeInfo("MFN_M11")]
    [Description("M11")]
    MFN_M11,

    [MessageTypeInfo("MFN_M12")]
    [Description("M12")]
    MFN_M12,

    [MessageTypeInfo("MFN_M13")]
    [Description("M13")]
    MFN_M13,

    [MessageTypeInfo("MFN_M15")]
    [Description("M15")]
    MFN_M15,

    [MessageTypeInfo("MFN_M16")]
    [Description("M16")]
    MFN_M16,

    [MessageTypeInfo("MFN_M17")]
    [Description("M17")]
    MFN_M17,

    [MessageTypeInfo("MFN_Znn")]
    [Description("Znn")]
    MFN_Znn,

    [MessageTypeInfo("MFQ_M01")]
    [Description("M01, M02, M03, M04, M05, M06")]
    MFQ_M01,

    [MessageTypeInfo("MFR_M01")]
    [Description("M01, M02, M03")]
    MFR_M01,

    [MessageTypeInfo("MFR_M04")]
    [Description("M04")]
    MFR_M04,

    [MessageTypeInfo("MFR_M05")]
    [Description("M05")]
    MFR_M05,

    [MessageTypeInfo("MFR_M06")]
    [Description("M06")]
    MFR_M06,

    [MessageTypeInfo("MFR_M07")]
    [Description("M07")]
    MFR_M07,

    [MessageTypeInfo("NMD_N02")]
    [Description("N02")]
    NMD_N02,

    [MessageTypeInfo("NMQ_N01")]
    [Description("N01")]
    NMQ_N01,

    [MessageTypeInfo("NMR_N01")]
    [Description("N01")]
    NMR_N01,

    [MessageTypeInfo("OMB_O27")]
    [Description("O27")]
    OMB_O27,

    [MessageTypeInfo("OMD_O03")]
    [Description("O03")]
    OMD_O03,

    [MessageTypeInfo("OMG_O19")]
    [Description("O19")]
    OMG_O19,

    [MessageTypeInfo("OMI_O23")]
    [Description("O23")]
    OMI_O23,

    [MessageTypeInfo("OML_O21")]
    [Description("O21")]
    OML_O21,

    [MessageTypeInfo("OML_O33")]
    [Description("O33")]
    OML_O33,

    [MessageTypeInfo("OML_O35")]
    [Description("O35")]
    OML_O35,

    [MessageTypeInfo("OML_O39")]
    [Description("O39")]
    OML_O39,

    [MessageTypeInfo("OMN_O07")]
    [Description("O07")]
    OMN_O07,

    [MessageTypeInfo("OMP_O09")]
    [Description("O09")]
    OMP_O09,

    [MessageTypeInfo("OMS_O05")]
    [Description("O05")]
    OMS_O05,

    [MessageTypeInfo("OPL_O37")]
    [Description("O37")]
    OPL_O37,

    [MessageTypeInfo("OPR_O38")]
    [Description("O38")]
    OPR_O38,

    [MessageTypeInfo("OPU_R25")]
    [Description("R25")]
    OPU_R25,

    [MessageTypeInfo("ORA_R33")]
    [Description("R33")]
    ORA_R33,

    [MessageTypeInfo("ORB_O28")]
    [Description("O28")]
    ORB_O28,

    [MessageTypeInfo("ORD_O04")]
    [Description("O04")]
    ORD_O04,

    [MessageTypeInfo("ORF_R04")]
    [Description("R04")]
    ORF_R04,

    [MessageTypeInfo("ORG_O20")]
    [Description("O20")]
    ORG_O20,

    [MessageTypeInfo("ORI_O24")]
    [Description("O24")]
    ORI_O24,

    [MessageTypeInfo("ORL_O22")]
    [Description("O22")]
    ORL_O22,

    [MessageTypeInfo("ORL_O34")]
    [Description("O34")]
    ORL_O34,

    [MessageTypeInfo("ORL_O36")]
    [Description("O36")]
    ORL_O36,

    [MessageTypeInfo("ORL_O40")]
    [Description("O40")]
    ORL_O40,

    [MessageTypeInfo("ORM_O01")]
    [Description("O01")]
    ORM_O01,

    [MessageTypeInfo("ORN_O08")]
    [Description("O08")]
    ORN_O08,

    [MessageTypeInfo("ORP_O10")]
    [Description("O10")]
    ORP_O10,

    [MessageTypeInfo("ORR_O02")]
    [Description("O02")]
    ORR_O02,

    [MessageTypeInfo("ORS_O06")]
    [Description("O06")]
    ORS_O06,

    [MessageTypeInfo("ORU_R01")]
    [Description("R01")]
    ORU_R01,

    [MessageTypeInfo("ORU_R30")]
    [Description("R30")]
    ORU_R30,

    [MessageTypeInfo("ORU_W01")]
    [Description("W01")]
    ORU_W01,

    [MessageTypeInfo("OSM_R26")]
    [Description("R26")]
    OSM_R26,

    [MessageTypeInfo("OSQ_Q06")]
    [Description("Q06")]
    OSQ_Q06,

    [MessageTypeInfo("OSR_Q06")]
    [Description("Q06")]
    OSR_Q06,

    [MessageTypeInfo("OUL_R21")]
    [Description("R21")]
    OUL_R21,

    [MessageTypeInfo("OUL_R22")]
    [Description("R22")]
    OUL_R22,

    [MessageTypeInfo("OUL_R23")]
    [Description("R23")]
    OUL_R23,

    [MessageTypeInfo("OUL_R24")]
    [Description("R24")]
    OUL_R24,

    [MessageTypeInfo("PEX_P07")]
    [Description("P07, P08")]
    PEX_P07,

    [MessageTypeInfo("PGL_PC6")]
    [Description("PC6, PC7, PC8")]
    PGL_PC6,

    [MessageTypeInfo("PMU_B01")]
    [Description("B01, B02")]
    PMU_B01,

    [MessageTypeInfo("PMU_B03")]
    [Description("B03")]
    PMU_B03,

    [MessageTypeInfo("PMU_B04")]
    [Description("B04, B05, B06")]
    PMU_B04,

    [MessageTypeInfo("PMU_B07")]
    [Description("B07")]
    PMU_B07,

    [MessageTypeInfo("PMU_B08")]
    [Description("B08")]
    PMU_B08,

    [MessageTypeInfo("PPG_PCG")]
    [Description("PCC, PCG, PCH, PCJ")]
    PPG_PCG,

    [MessageTypeInfo("PPP_PCB")]
    [Description("PCB, PCD")]
    PPP_PCB,

    [MessageTypeInfo("PPR_PC1")]
    [Description("PC1, PC2, PC3")]
    PPR_PC1,

    [MessageTypeInfo("PPT_PCL")]
    [Description("PCL")]
    PPT_PCL,

    [MessageTypeInfo("PPV_PCA")]
    [Description("PCA")]
    PPV_PCA,

    [MessageTypeInfo("PRR_PC5")]
    [Description("PC5")]
    PRR_PC5,

    [MessageTypeInfo("PTR_PCF")]
    [Description("PCF")]
    PTR_PCF,

    [MessageTypeInfo("QBP_E03")]
    [Description("E03")]
    QBP_E03,

    [MessageTypeInfo("QBP_E22")]
    [Description("E22")]
    QBP_E22,

    [MessageTypeInfo("QBP_Q11")]
    [Description("Q11")]
    QBP_Q11,

    [MessageTypeInfo("QBP_Q13")]
    [Description("Q13")]
    QBP_Q13,

    [MessageTypeInfo("QBP_Q15")]
    [Description("Q15")]
    QBP_Q15,

    [MessageTypeInfo("QBP_Q21")]
    [Description("Q21, Q22, Q23, Q24, Q25")]
    QBP_Q21,

    [MessageTypeInfo("QBP_Qnn")]
    [Description("Qnn")]
    QBP_Qnn,

    [MessageTypeInfo("QBP_Z73")]
    [Description("Z73")]
    QBP_Z73,

    [MessageTypeInfo("QCK_Q02")]
    [Description("Q02")]
    QCK_Q02,

    [MessageTypeInfo("QCN_J01")]
    [Description("J01, J02")]
    QCN_J01,

    [MessageTypeInfo("QRF_W02")]
    [Description("W02")]
    QRF_W02,

    [MessageTypeInfo("QRY_A19")]
    [Description("A19")]
    QRY_A19,

    [MessageTypeInfo("QRY_PC4")]
    [Description("PC4, PC9, PCE, PCK")]
    QRY_PC4,

    [MessageTypeInfo("QRY_Q01")]
    [Description("Q01, Q26, Q27, Q28, Q29, Q30")]
    QRY_Q01,

    [MessageTypeInfo("QRY_Q02")]
    [Description("Q02")]
    QRY_Q02,

    [MessageTypeInfo("QRY_R02")]
    [Description("R02")]
    QRY_R02,

    [MessageTypeInfo("QRY_T12")]
    [Description("T12")]
    QRY_T12,

    [MessageTypeInfo("QSB_Q16")]
    [Description("Q16")]
    QSB_Q16,

    [MessageTypeInfo("QVR_Q17")]
    [Description("Q17")]
    QVR_Q17,

    [MessageTypeInfo("RAR_RAR")]
    [Description("RAR")]
    RAR_RAR,

    [MessageTypeInfo("RAS_O17")]
    [Description("O17")]
    RAS_O17,

    [MessageTypeInfo("RCI_I05")]
    [Description("I05")]
    RCI_I05,

    [MessageTypeInfo("RCL_I06")]
    [Description("I06")]
    RCL_I06,

    [MessageTypeInfo("RDE_O11")]
    [Description("O11, O25")]
    RDE_O11,

    [MessageTypeInfo("RDR_RDR")]
    [Description("RDR")]
    RDR_RDR,

    [MessageTypeInfo("RDS_O13")]
    [Description("O13")]
    RDS_O13,

    [MessageTypeInfo("RDY_K15")]
    [Description("K15")]
    RDY_K15,

    [MessageTypeInfo("REF_I12")]
    [Description("I12, I13, I14, I15")]
    REF_I12,

    [MessageTypeInfo("RER_RER")]
    [Description("RER")]
    RER_RER,

    [MessageTypeInfo("RGR_RGR")]
    [Description("RGR")]
    RGR_RGR,

    [MessageTypeInfo("RGV_O15")]
    [Description("O15")]
    RGV_O15,

    [MessageTypeInfo("ROR_ROR")]
    [Description("ROR")]
    ROR_ROR,

    [MessageTypeInfo("RPA_I08")]
    [Description("I08, I09, I10, I11")]
    RPA_I08,

    [MessageTypeInfo("RPI_I01")]
    [Description("I01, I04")]
    RPI_I01,

    [MessageTypeInfo("RPI_I04")]
    [Description("I04")]
    RPI_I04,

    [MessageTypeInfo("RPL_I02")]
    [Description("I02")]
    RPL_I02,

    [MessageTypeInfo("RPR_I03")]
    [Description("I03")]
    RPR_I03,

    [MessageTypeInfo("RQA_I08")]
    [Description("I08, I09, I10, I11")]
    RQA_I08,

    [MessageTypeInfo("RQC_I05")]
    [Description("I05, I06")]
    RQC_I05,

    [MessageTypeInfo("RQI_I01")]
    [Description("I01, I02, I03, I07")]
    RQI_I01,

    [MessageTypeInfo("RQP_I04")]
    [Description("I04")]
    RQP_I04,

    [MessageTypeInfo("RRA_O18")]
    [Description("O18")]
    RRA_O18,

    [MessageTypeInfo("RRD_O14")]
    [Description("O14")]
    RRD_O14,

    [MessageTypeInfo("RRE_O12")]
    [Description("O12, O26")]
    RRE_O12,

    [MessageTypeInfo("RRG_O16")]
    [Description("O16")]
    RRG_O16,

    [MessageTypeInfo("RRI_I12")]
    [Description("I12, I13, I14, I15")]
    RRI_I12,

    [MessageTypeInfo("RSP_E03")]
    [Description("E03")]
    RSP_E03,

    [MessageTypeInfo("RSP_E22")]
    [Description("E22")]
    RSP_E22,

    [MessageTypeInfo("RSP_K11")]
    [Description("K11")]
    RSP_K11,

    [MessageTypeInfo("RSP_K21")]
    [Description("K21")]
    RSP_K21,

    [MessageTypeInfo("RSP_K22")]
    [Description("K22")]
    RSP_K22,

    [MessageTypeInfo("RSP_K23")]
    [Description("K23, K24")]
    RSP_K23,

    [MessageTypeInfo("RSP_K25")]
    [Description("K25")]
    RSP_K25,

    [MessageTypeInfo("RSP_K31")]
    [Description("K31")]
    RSP_K31,

    [MessageTypeInfo("RSP_K32")]
    [Description("K32")]
    RSP_K32,

    [MessageTypeInfo("RSP_Q11")]
    [Description("Q11")]
    RSP_Q11,

    [MessageTypeInfo("RSP_Z82")]
    [Description("Z82")]
    RSP_Z82,

    [MessageTypeInfo("RSP_Z86")]
    [Description("Z86")]
    RSP_Z86,

    [MessageTypeInfo("RSP_Z88")]
    [Description("Z88")]
    RSP_Z88,

    [MessageTypeInfo("RSP_Z90")]
    [Description("Z90")]
    RSP_Z90,

    [MessageTypeInfo("RTB_K13")]
    [Description("K13")]
    RTB_K13,

    [MessageTypeInfo("RTB_Knn")]
    [Description("Knn")]
    RTB_Knn,

    [MessageTypeInfo("RTB_Z74")]
    [Description("Z74")]
    RTB_Z74,

    [MessageTypeInfo("SDR_S31")]
    [Description("S31, S36")]
    SDR_S31,

    [MessageTypeInfo("SDR_S32")]
    [Description("S32, S37")]
    SDR_S32,

    [MessageTypeInfo("SIU_S12")]
    [Description("S12, S13, S14, S15, S16, S17, S18, S19, S20, S21, S22, S23, S24, S26")]
    SIU_S12,

    [MessageTypeInfo("SLR_S28")]
    [Description("S28, S29, S30, S34, S35")]
    SLR_S28,

    [MessageTypeInfo("SQM_S25")]
    [Description("S25")]
    SQM_S25,

    [MessageTypeInfo("SQR_S25")]
    [Description("S25")]
    SQR_S25,

    [MessageTypeInfo("SRM_S01")]
    [Description("S01, S02, S03, S04, S05, S06, S07, S08, S09, S10, S11")]
    SRM_S01,

    [MessageTypeInfo("SRR_S01")]
    [Description("S01, S02, S03, S04, S05, S06, S07, S08, S09, S10, S11")]
    SRR_S01,

    [MessageTypeInfo("SSR_U04")]
    [Description("U04")]
    SSR_U04,

    [MessageTypeInfo("SSU_U03")]
    [Description("U03")]
    SSU_U03,

    [MessageTypeInfo("STC_S33")]
    [Description("S33")]
    STC_S33,

    [MessageTypeInfo("SUR_P09")]
    [Description("P09")]
    SUR_P09,

    [MessageTypeInfo("TCU_U10")]
    [Description("U10, U11")]
    TCU_U10,

    [MessageTypeInfo("UDM_Q05")]
    [Description("Q05")]
    UDM_Q05,

    [MessageTypeInfo("VXQ_V01")]
    [Description("V01")]
    VXQ_V01,

    [MessageTypeInfo("VXR_V03")]
    [Description("V03")]
    VXR_V03,

    [MessageTypeInfo("VXU_V04")]
    [Description("V04")]
    VXU_V04,

    [MessageTypeInfo("VXX_V02")]
    [Description("V02")]
    VXX_V02,
}
