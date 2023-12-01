using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;

namespace STR_CajaChica_Entregas.BL
{
    public static class Cls_EAR_Apertura_BL
    {
        private static SAPbobsCOM.Company go_SBOCompany = Cls_Global.go_SBOCompany;

        //* * * * * * * * * * * * * * DataSources* * * * * * * * * * * * * * *
        private const string gs_DtcEARAPR = "@STR_EARAPR";
        private const string gs_DtdEARAPRDET = "@STR_EARAPRDET";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * * * User Fields - @STR_EARAPR * * * * * * * * 
        private static readonly string gs_UflFchCnt = "U_ER_FCHC";
        private static readonly string gs_UflFchVnc = "U_ER_FCHV";
        private static readonly string gs_UflFchDcm = "U_ER_FCHD";
        private static readonly string gs_UflMPSUNAT = "U_ER_MPSN";
        private static readonly string gs_UflMdoPgo = "U_ER_MDPG";
        private static readonly string gs_UflCtaCnt = "U_ER_CTBN";
        private static readonly string gs_UflCdgBnc = "U_ER_CHBN";
        private static readonly string gs_UflMndEAR = "U_ER_MNDA";
        private static readonly string gs_UflChqMnl = "U_ER_CHMN";
        private static readonly string gs_UflChqNmr = "U_ER_CHNM";
        private static readonly string gs_UflChqFcV = "U_ER_CHFV";
        private static readonly string gs_UflTrnFch = "U_ER_TBFC";
        private static readonly string gs_UflTrnRef = "U_ER_TBRF";
        private static readonly string gs_UflMntApr = "U_ER_MNAP";
        private static readonly string gs_UflCshFlw = "U_ER_CSHF";
        private static readonly string gs_UflDcNmPgo = "U_ER_NMPE";
        private static readonly string gs_UflDcEnPgo = "U_ER_DEPE";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * User Fields - @STR_EARAPRDET * * * * * * * 
        private static readonly string gs_UflDetEARSlc = "U_ER_SLCC";
        private static readonly string gs_UflDetCdgEAR = "U_ER_EARN";
        private static readonly string gs_UflDetNmrEAR = "U_ER_NMER";
        private static readonly string gs_UflDetCdgSng = "U_ER_CDSN";
        private static readonly string gs_UflDetCmntrs = "U_ER_CMNT";
        private static readonly string gs_UflDetEARMnt = "U_ER_MNTO";
        private static readonly string gs_UflDetCodCta = "U_ER_CDCT";
        private static readonly string gs_UflDetDscCta = "U_ER_DSCT";
        private static readonly string gs_UflDetCdgSlc = "U_ER_DESL";
        private static readonly string gs_UflDetCdgPry = "U_ER_PRYC";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        public static void sb_CalcularTotalesdeApertura(SAPbouiCOM.DBDataSources po_DBDTS, ref int pi_CodErr, ref string ps_DscErr)
        {
            double ld_TotApr = 0.0;
            for (int i = 0; i < po_DBDTS.Item(gs_DtdEARAPRDET).Size; i++)
            {
                if (po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetEARSlc, i).Trim() != "Y") continue;
                ld_TotApr += Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetEARMnt, i).Trim());   
            }
            po_DBDTS.Item(gs_DtcEARAPR).SetValue(gs_UflMntApr, 0, ld_TotApr.ToString());
        }

        public static void fn_GenerarPagoEfectuado(SAPbouiCOM.DBDataSources po_DBDTS, ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbobsCOM.Payments lo_PgoEfc = null;
            SAPbobsCOM.HouseBankAccounts lo_BnkAccts = null;
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;
            SAPbobsCOM.JournalEntries lo_PgoAs = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtOfAcct = null;
            string ls_CdgCta = string.Empty;
            string ls_CdgEAR = string.Empty;
            string ls_XMLPgo = string.Empty;
            int li_EmpId;
            
            try
            {
                lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
                lo_PgoEfc = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
                lo_BnkAccts = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oHouseBankAccounts);
                lo_ChrtOfAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
                lo_PgoAs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                ls_CdgEAR = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCdgEAR, 0).Trim();
                lo_EmpInf.GetByKey(Convert.ToInt32(ls_CdgEAR.Substring(3,ls_CdgEAR.Length-3)));
                lo_PgoEfc.DocObjectCode = SAPbobsCOM.BoPaymentsObjectType.bopot_OutgoingPayments;
                lo_PgoEfc.DocType = SAPbobsCOM.BoRcptTypes.rSupplier;
                lo_PgoEfc.DocCurrency = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMndEAR, 0).Trim();
                lo_PgoEfc.CardCode = lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value;
                lo_PgoEfc.TaxDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflFchCnt, 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_PgoEfc.DueDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflFchVnc, 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_PgoEfc.DocDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflFchDcm, 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_PgoEfc.Remarks = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCmntrs, 0).Trim();
                lo_PgoEfc.JournalRemarks = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCmntrs, 0).Trim();
                lo_PgoEfc.UserFields.Fields.Item("U_BPP_PtFC").Value = "";
                lo_PgoEfc.UserFields.Fields.Item("U_BPP_MPPG").Value = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMPSUNAT, 0).Trim();
                lo_PgoEfc.ProjectCode = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCdgPry, 0).Trim();
                switch (Convert.ToInt32(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMdoPgo, 0).Trim()))
                {
                    case 1:
                        lo_PgoEfc.Checks.AccounttNum = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCtaCnt, 0).Trim();
                        lo_PgoEfc.Checks.BankCode = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCdgBnc, 0).Trim();
                        lo_PgoEfc.Checks.Branch = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMndEAR, 0).Trim();
                        ls_CdgCta = Cls_QueriesManager_EAR.fn_CuentaDeBancoPropio(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCdgBnc, 0).Trim(), po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCtaCnt, 0).Trim());
                        if (ls_CdgCta == string.Empty)
                        {
                            pi_CodErr = -1;
                            ps_DscErr = "La cuenta bancaria no esta relacionada a una cuenta contable...";
                            return;
                        }
                        lo_PgoEfc.Checks.CheckAccount = ls_CdgCta;
                        if (po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflChqMnl, 0).Trim() != "Y")
                        {
                            lo_PgoEfc.Checks.ManualCheck = SAPbobsCOM.BoYesNoEnum.tNO;
                        }
                        else
                        {
                            lo_PgoEfc.Checks.ManualCheck = SAPbobsCOM.BoYesNoEnum.tYES;
                            lo_PgoEfc.Checks.CheckNumber = Convert.ToInt32(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflChqNmr, 0).Trim());
                        }
                        lo_PgoEfc.Checks.CheckSum = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                        lo_PgoEfc.Checks.CountryCode = "PE";
                        lo_PgoEfc.Checks.DueDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflChqFcV, 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        lo_PgoEfc.Checks.Trnsfrable = SAPbobsCOM.BoYesNoEnum.tNO;
                        if (po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if (po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMndEAR, 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountLC = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                            }
                            else
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountFC = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                            }
                            lo_PgoEfc.PrimaryFormItems.CheckNumber = "0";
                            lo_PgoEfc.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCshFlw, 0).Trim());
                            lo_PgoEfc.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtChecks;
                            lo_PgoEfc.PrimaryFormItems.Add();
                        }
                        break;
                    case 2:
                        ls_CdgCta = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCtaCnt, 0).Trim();
                        lo_PgoEfc.TransferAccount = ls_CdgCta;
                        lo_PgoEfc.TransferDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflTrnFch, 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        lo_PgoEfc.TransferReference = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflTrnRef, 0).Trim();
                        lo_PgoEfc.TransferSum = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                        //Cash Flow
                        if (po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if (po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMndEAR, 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountLC = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                            }
                            else
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountFC = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                            }
                            lo_PgoEfc.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCshFlw, 0).Trim());
                            lo_PgoEfc.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtBankTransfer;
                            lo_PgoEfc.PrimaryFormItems.Add();
                        }
                        break;
                    case 3:
                        ls_CdgCta = po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCtaCnt, 0).Trim();
                        lo_PgoEfc.CashAccount = ls_CdgCta;
                        lo_PgoEfc.CashSum = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                        if (po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if (po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMndEAR, 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountLC = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                            }
                            else
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountFC = Convert.ToDouble(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflMntApr, 0).Trim());
                            }
                            lo_PgoEfc.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtCash;
                            lo_PgoEfc.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(po_DBDTS.Item(gs_DtcEARAPR).GetValue(gs_UflCshFlw, 0).Trim());
                            lo_PgoEfc.PrimaryFormItems.Add();
                        }
                        break;
                }
                lo_PgoEfc.ControlAccount = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCodCta, 0).Trim();
                if (lo_PgoEfc.Add() != 0)
                {
                    pi_CodErr = go_SBOCompany.GetLastErrorCode();
                    ps_DscErr = go_SBOCompany.GetLastErrorDescription();
                }
                else
                {
                    if (lo_PgoEfc.GetByKey(Convert.ToInt32(go_SBOCompany.GetNewObjectKey())))
                    {
                        po_DBDTS.Item(gs_DtcEARAPR).SetValue(gs_UflDcEnPgo, 0, go_SBOCompany.GetNewObjectKey());
                        po_DBDTS.Item(gs_DtcEARAPR).SetValue(gs_UflDcNmPgo, 0, lo_PgoEfc.DocNum.ToString());
                            //Actulizo datos del asiento del pago efectuado
                            ls_XMLPgo = lo_PgoEfc.GetAsXML();
                            if (lo_PgoAs.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<TransId>") + 9, ls_XMLPgo.IndexOf("</TransId>") - ls_XMLPgo.IndexOf("<TransId>") - 9))))
                            {
                                lo_PgoAs.TransactionCode = "EAR";
                                for (int i = 0; i < lo_PgoAs.Lines.Count; i++)
                                {
                                    lo_PgoAs.Lines.SetCurrentLine(i);
                                    lo_ChrtOfAcct.GetByKey(lo_PgoAs.Lines.AccountCode);
                                    if (lo_ChrtOfAcct.UserFields.Fields.Item("U_CE_ACCT").Value != "Y") continue;
                                    lo_PgoAs.Lines.Reference1 = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCdgEAR, 0).Trim();
                                    lo_PgoAs.Lines.Reference2 = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetNmrEAR, 0).Trim();
                                    lo_PgoAs.Lines.LineMemo = po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCmntrs, 0).Trim();
                                }
                                lo_PgoAs.Update();
                            }
                    }
                    sb_CerrarDocumentodeSolicitud(po_DBDTS.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetCdgSlc, 0).Trim());
                }
            }
            catch (Exception ex)
            {
                pi_CodErr = 999;
                ps_DscErr = ex.Message;
            }
            finally
            {
                lo_PgoEfc = null;
                lo_BnkAccts = null;
                lo_EmpInf = null;
            }
        }

        public static void sb_CerrarDocumentodeSolicitud(string ps_DocEntSlc)
        {
            try
            {
                SAPbobsCOM.Documents lo_Doc = null;
                lo_Doc = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseRequest);
                if (lo_Doc.GetByKey(Convert.ToInt32(ps_DocEntSlc)))
                {
                    if (lo_Doc.Close() != 0)
                    {
                        Cls_Global.go_SBOApplication.StatusBar.SetText(go_SBOCompany.GetLastErrorCode() + " - " + go_SBOCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
                else
                {
                    Cls_Global.go_SBOApplication.StatusBar.SetText("No se encontro documento de solicitud con DocEntry: " + ps_DocEntSlc , SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                }
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }
    }
}
