using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;

namespace STR_CajaChica_Entregas.BL
{
    public static class Cls_EAR_Regularizar_BL
    {
        private static SAPbobsCOM.Company go_SBOCompany = Cls_Global.go_SBOCompany;
        
        private const string gs_DtcEARCRG = "@STR_EARCRG";
        private const string gs_DtdEARCRGDET = "@STR_EARCRGDET";
        private const string gs_DtdEARAPRDET2 = "@STR_EARCRGDET2";

        //UserFields

        private readonly static string gs_UflCshFlw = "U_ER_CSHF"; 

        public static bool fn_GenerarPagoxTipoRegularizacion(string ps_TpoRgl,SAPbouiCOM.DBDataSources po_DBDTS)
        { 
            SAPbobsCOM.Payments lo_Pay = null;
            SAPbobsCOM.HouseBankAccounts lo_BnkAccts = null;
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;
            SAPbobsCOM.JournalEntries lo_PgoAs = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtOfAcct = null;
            string ls_CdgCta = string.Empty;
            string ls_CdgEAR = string.Empty;
            string ls_XMLPgo = string.Empty;
            bool lb_Result = true;

            try
            {
                if (ps_TpoRgl == "DVL")
                {
                    lo_Pay = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                    lo_Pay.DocObjectCode = SAPbobsCOM.BoPaymentsObjectType.bopot_IncomingPayments;
                }
                else
                {
                    lo_Pay = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
                    lo_Pay.DocObjectCode = SAPbobsCOM.BoPaymentsObjectType.bopot_OutgoingPayments;                    
                }
                lo_ChrtOfAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
                lo_PgoAs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                lo_BnkAccts = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oHouseBankAccounts);
                lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
                ls_CdgEAR = po_DBDTS.Item(gs_DtcEARCRG).GetValue("U_ER_NMBR", 0).Trim();
                lo_EmpInf.GetByKey(Convert.ToInt32(ls_CdgEAR.Substring(3, ls_CdgEAR.Length - 3)));
                //Se Inicia la creacion del pago
                lo_Pay.DocType = SAPbobsCOM.BoRcptTypes.rSupplier;
                lo_Pay.DocCurrency = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNDA", 0).Trim();
                lo_Pay.CardCode = lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value;
                lo_Pay.TaxDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_FCRG", 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_Pay.DueDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_FCRG", 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_Pay.DocDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_FCRG", 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);

                //lo_Pay.Remarks = "";
                //lo_Pay.JournalRemarks = "";
                //lo_Pay.UserFields.Fields.Item("U_BPP_PtFC").Value = "";
                lo_Pay.UserFields.Fields.Item("U_BPP_MPPG").Value = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MPSN", 0).Trim();
                switch (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MDPG", 0).Trim())
                {
                    case "CH":
                        
                        lo_Pay.Checks.BankCode = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CHBN", 0).Trim();
                        lo_Pay.Checks.Branch = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNDA", 0).Trim();
                        ls_CdgCta = Cls_QueriesManager_EAR.fn_CuentaDeBancoPropio(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CHBN", 0).Trim(), po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CTBN", 0).Trim());
                        if (ls_CdgCta == string.Empty)
                        {
                            Cls_Global.go_SBOApplication.StatusBar.SetText("La cuenta bancaria no esta relacionada a una cuenta contable...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            return lb_Result = false;
                        }
                        if (ps_TpoRgl == "RNT")
                        {
                            lo_Pay.Checks.AccounttNum = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CTBN", 0).Trim();
                            lo_Pay.Checks.CheckAccount = ls_CdgCta;
                        }
                        else
                            lo_Pay.CheckAccount = ls_CdgCta;
                        if (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CHMN", 0).Trim() != "Y")
                        {
                            lo_Pay.Checks.ManualCheck = SAPbobsCOM.BoYesNoEnum.tNO;
                        }
                        else
                        {
                            lo_Pay.Checks.ManualCheck = SAPbobsCOM.BoYesNoEnum.tYES;
                            lo_Pay.Checks.CheckNumber = Convert.ToInt32(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CHNM", 0).Trim());
                        }
                        lo_Pay.Checks.CheckSum = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                        lo_Pay.Checks.CountryCode = "PE";
                        lo_Pay.Checks.DueDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CHFV", 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        lo_Pay.Checks.Trnsfrable = SAPbobsCOM.BoYesNoEnum.tNO;
                        if (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNDA", 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_Pay.PrimaryFormItems.AmountLC = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                            }
                            else
                            {
                                lo_Pay.PrimaryFormItems.AmountFC = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                            }
                            lo_Pay.PrimaryFormItems.CheckNumber = "0";
                            lo_Pay.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue(gs_UflCshFlw, 0).Trim());
                            lo_Pay.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtChecks;
                            lo_Pay.PrimaryFormItems.Add();
                        }
                        
                   break;
                case "TR":
                      ls_CdgCta = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CTBN", 0).Trim();
                      lo_Pay.TransferAccount = ls_CdgCta;
                      lo_Pay.TransferDate = DateTime.ParseExact(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_TRFC", 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                      lo_Pay.TransferReference = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_TRRF", 0).Trim();
                      lo_Pay.TransferSum = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                      //Cash Flow
                      if (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                      {
                          if (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNDA", 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                          {
                              lo_Pay.PrimaryFormItems.AmountLC = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                          }
                          else
                          {
                              lo_Pay.PrimaryFormItems.AmountFC = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                          }
                          lo_Pay.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue(gs_UflCshFlw, 0).Trim());
                          lo_Pay.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtBankTransfer;
                          lo_Pay.PrimaryFormItems.Add();
                      }
                  break;
                case "EF":
                        ls_CdgCta = po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_CTBN", 0).Trim();
                        lo_Pay.CashAccount = ls_CdgCta;
                        lo_Pay.CashSum = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                        if (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if (po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNDA", 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_Pay.PrimaryFormItems.AmountLC = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                            }
                            else
                            {
                                lo_Pay.PrimaryFormItems.AmountFC = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                            }
                            lo_Pay.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtCash;
                            lo_Pay.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue(gs_UflCshFlw, 0).Trim());
                            lo_Pay.PrimaryFormItems.Add();
                        }
                break;
                }
                if (ps_TpoRgl == "DVL")
                {
                    lo_Pay.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_JournalEntry;
                    lo_Pay.Invoices.DocEntry = Cls_QueriesManager_EAR.fn_ObtenerTransIdPagoaCuenta(po_DBDTS.Item(gs_DtcEARCRG).GetValue("U_ER_NMBR", 0).Trim(), po_DBDTS.Item(gs_DtcEARCRG).GetValue("U_ER_NMRO", 0).Trim());
                    lo_Pay.Invoices.DocLine = 1;
                    if (lo_Pay.DocCurrency == Cls_Global.sb_ObtenerMonedaLocal())
                    {
                        lo_Pay.Invoices.SumApplied = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                    }
                    else
                    {
                        lo_Pay.Invoices.AppliedFC = Convert.ToDouble(po_DBDTS.Item(gs_DtdEARAPRDET2).GetValue("U_ER_MNTT", 0).Trim());
                    }
                    lo_Pay.Invoices.Add();
                }
                else
                {
                    lo_Pay.ControlAccount = lo_EmpInf.UserFields.Fields.Item("U_CE_CTAS").Value;
                }
                if (lo_Pay.Add() != 0)
                {
                    Cls_Global.go_SBOApplication.StatusBar.SetText(go_SBOCompany.GetLastErrorCode() + " - " + go_SBOCompany.GetLastErrorDescription(),SAPbouiCOM.BoMessageTime.bmt_Short,SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    lb_Result = false;
                }
                else
                {
                    if (lo_Pay.GetByKey(Convert.ToInt32(go_SBOCompany.GetNewObjectKey())))
                    {
                        po_DBDTS.Item(gs_DtdEARAPRDET2).SetValue("U_ER_DEPG", 0, lo_Pay.DocEntry.ToString());
                        po_DBDTS.Item(gs_DtdEARAPRDET2).SetValue("U_ER_NMPG", 0, lo_Pay.DocNum.ToString());
                        //Actulizo datos del asiento del pago efectuado
                        ls_XMLPgo = lo_Pay.GetAsXML();
                        if (lo_PgoAs.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<TransId>") + 9, ls_XMLPgo.IndexOf("</TransId>") - ls_XMLPgo.IndexOf("<TransId>") - 9))))
                        {
                            lo_PgoAs.TransactionCode = "EAR";
                            for (int i = 0; i < lo_PgoAs.Lines.Count; i++)
                            {
                                lo_PgoAs.Lines.SetCurrentLine(i);
                                lo_ChrtOfAcct.GetByKey(lo_PgoAs.Lines.AccountCode);
                                if (lo_ChrtOfAcct.UserFields.Fields.Item("U_CE_ACCT").Value != "Y") continue;
                                lo_PgoAs.Lines.Reference1 = po_DBDTS.Item(gs_DtcEARCRG).GetValue("U_ER_NMBR", 0).Trim();
                                lo_PgoAs.Lines.Reference2 = po_DBDTS.Item(gs_DtcEARCRG).GetValue("U_ER_NMRO", 0).Trim();
                                if (ps_TpoRgl == "DVL")
                                    lo_PgoAs.Lines.LineMemo = "EAR - Devolución";
                                else
                                    lo_PgoAs.Lines.LineMemo = "EAR - Reintegro";
                            }
                            lo_PgoAs.Update();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                lb_Result = false;
            }
            return lb_Result;
        }
    }
}
