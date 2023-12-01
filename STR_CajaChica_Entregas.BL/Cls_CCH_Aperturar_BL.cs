using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;

namespace STR_CajaChica_Entregas.BL
{
    public static class Cls_CCH_Aperturar_BL 
    {
        private static SAPbobsCOM.Company go_SBOCompany = Cls_Global.go_SBOCompany;

        //* * * * * * * * * * * * * * DataSources* * * * * * * * * * * * * * *
        private const string gs_DtcCCHAPR = "@STR_CCHAPR";
        private const string gs_DtdCCHARPDET = "@STR_CCHAPRDET";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * * * User Fields - @STR_CCHAPR * * * * * * * * 
        private static string gs_UflCodScNg = "U_CC_CDSN";
        private static string gs_UflComents = "U_CC_CMNT";
        private static string gs_UflFchCntb = "U_CC_FCHC";
        private static string gs_UflFchVenc = "U_CC_FCHV";
        private static string gs_UflFchDcmn = "U_CC_FCHD";
        private static string gs_UflCshFlw = "U_CC_CSHF";
        private static string gs_UflTpoAprt = "U_CC_TPAP";
        private static string gs_UflMndCaja = "U_CC_MNDA";
        private static string gs_UflCtaCnt = "U_CC_CTBN";
        private static string gs_UflMPSUNAT = "U_CC_MPSN";
        private static string gs_UflMntTotApr = "U_CC_MNTT";
        private static string gs_UflMntTotTrs = "U_CC_MNTR";
        private static string gs_UflTotAprCCH = "U_CC_MNAP";
        private static string gs_UflDocEntPgo = "U_CC_DEPE";
        private static string gs_UflDocNumPgo = "U_CC_NMPE";
        //Datos de Cheque
        private static string gs_UflChqFchVnc = "U_CC_CHFV";
        private static string gs_UflChqBnc = "U_CC_CHBN";
        private static string gs_UflChqNum = "U_CC_CHNM";
        private static string gs_UflChqMnl = "U_CC_CHMN";
        private static string gs_UflChqMPg = "U_CC_CHMP";
        //Datos de Transferencia
        private static string gs_UflTrnFch = "U_CC_TBFC";
        private static string gs_uflTrnRef = "U_CC_TBRF";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * * * User Fields - @STR_CCHAPRDET * * * * * * * * * *
        private static string gs_UflClmCCHCod = "U_CC_CJCH";
        private static string gs_UflClmCCHDsc = "U_CC_DSCP";
        private static string gs_UflClmCtaSys = "U_CC_CDCT";
        private static string gs_UflClmCtaNmb = "U_CC_NMCT";
        private static string gs_UflClmCtaDsc = "U_CC_DSCT";
        private static string gs_UflClmCmntrs = "U_CC_CMNT";
        private static string gs_UflClmNmrCCH = "U_CC_NMCC";
        private static string gs_UflClmMntApr = "U_CC_MNTO";
        private static string gs_UflClmCCHTrs = "U_CC_TRSL";
        private static string gs_UflClmMntTrs = "U_CC_MNTR";
        private static string gs_UflClmMnTotl = "U_CC_MNAP";
        private static string gs_UflClmPrycto = "U_CC_PRYC";
        private static string gs_UflClmDmnsn1 = "U_CC_DIM1";
        private static string gs_UflClmDmnsn2 = "U_CC_DIM2";
        private static string gs_UflClmDmnsn3 = "U_CC_DIM3";
        private static string gs_UflClmDmnsn4 = "U_CC_DIM4";
        private static string gs_UflClmDmnsn5 = "U_CC_DIM5";
        private static string gs_UflClmSaldo = "U_CC_SLDO"; 
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        public static void sb_CalcularTotalesdeApertura(SAPbouiCOM.Form po_Form,ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbouiCOM.DBDataSource lo_DBDSCCHAPR = null;
            SAPbouiCOM.DBDataSource lo_DBDSCCHAPRDET = null;
            double ld_MntTotApr = 0.0;
            double ld_MntTotTrs = 0.0;
            double ls_MntTotalCCH = 0.0;
            try
            {
                lo_DBDSCCHAPR = po_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR);
                lo_DBDSCCHAPRDET = po_Form.DataSources.DBDataSources.Item(gs_DtdCCHARPDET);
                for(int i = 0; i < lo_DBDSCCHAPRDET.Size; i++)
                {
                    lo_DBDSCCHAPRDET.Offset = i;

                    ld_MntTotApr += double.Parse(lo_DBDSCCHAPRDET.GetValue(gs_UflClmMntApr, i));
                    ld_MntTotTrs += double.Parse(lo_DBDSCCHAPRDET.GetValue(gs_UflClmMntTrs, i));
                    ls_MntTotalCCH += double.Parse(lo_DBDSCCHAPRDET.GetValue(gs_UflClmMnTotl, i));
                }
                lo_DBDSCCHAPR.SetValue(gs_UflMntTotApr, 0, ld_MntTotApr.ToString());
                lo_DBDSCCHAPR.SetValue(gs_UflMntTotTrs, 0, ld_MntTotTrs.ToString());
                lo_DBDSCCHAPR.SetValue(gs_UflTotAprCCH, 0, ls_MntTotalCCH.ToString());
            }
            catch (Exception ex)
            {
                ps_DscErr = ex.Message;
                pi_CodErr = -1;
            }
        }

        public static void sb_CalcularTotalesXLinea(SAPbouiCOM.Form po_Form,int pi_Linea, ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbouiCOM.DBDataSource lo_DBDSCCHAPRDET = null;
            double ld_TotLn = 0.0;
            try
            {
                lo_DBDSCCHAPRDET = po_Form.DataSources.DBDataSources.Item(gs_DtdCCHARPDET);
                ld_TotLn = Convert.ToDouble(lo_DBDSCCHAPRDET.GetValue(gs_UflClmMntApr, pi_Linea-1)) + Convert.ToDouble(lo_DBDSCCHAPRDET.GetValue(gs_UflClmMntTrs, pi_Linea-1));
                lo_DBDSCCHAPRDET.SetValue(gs_UflClmMnTotl, pi_Linea - 1, ld_TotLn.ToString());
                lo_DBDSCCHAPRDET.SetValue(gs_UflClmSaldo, pi_Linea - 1, ld_TotLn.ToString());
            }
            catch(Exception ex)
            {
                ps_DscErr = ex.Message;
                pi_CodErr = -1;
            }
        }

        public static void fn_GenerarPagoEfectuado(SAPbouiCOM.Form po_Form, ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbouiCOM.DBDataSource lo_DBDSCCHAPR = null;
            SAPbouiCOM.DBDataSource lo_DBDSCCHAPRDET = null;
            SAPbobsCOM.Payments lo_PgoEfc = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtOfAcct = null;
            SAPbobsCOM.JournalEntries lo_PgoAs = null;
            System.Windows.Forms.DialogResult lo_DlgRsl;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            string ls_CdgCta = string.Empty;
            string ls_XMLPgo = string.Empty;

            try
            {
                lo_DBDSCCHAPR = po_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR);
                lo_DBDSCCHAPRDET = po_Form.DataSources.DBDataSources.Item(gs_DtdCCHARPDET);
                lo_PgoEfc = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                lo_ChrtOfAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
                lo_PgoAs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                lo_PgoEfc.DocType = SAPbobsCOM.BoRcptTypes.rAccount;
                lo_PgoEfc.DocDate = DateTime.ParseExact(lo_DBDSCCHAPR.GetValue(gs_UflFchDcmn, 0), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_PgoEfc.DueDate = DateTime.ParseExact(lo_DBDSCCHAPR.GetValue(gs_UflFchVenc, 0), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_PgoEfc.TaxDate = DateTime.ParseExact(lo_DBDSCCHAPR.GetValue(gs_UflFchCntb, 0), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                lo_PgoEfc.CardName = lo_DBDSCCHAPR.GetValue(gs_UflCodScNg, 0).Trim();
                lo_PgoEfc.Remarks = lo_DBDSCCHAPR.GetValue(gs_UflComents, 0).Trim();
                lo_PgoEfc.JournalRemarks = lo_DBDSCCHAPR.GetValue(gs_UflComents, 0).Trim();
                lo_PgoEfc.ProjectCode = lo_DBDSCCHAPRDET.GetValue(gs_UflClmPrycto, 0).Trim(); ;
                lo_PgoEfc.UserFields.Fields.Item("U_BPP_PtFC").Value = "";
                lo_PgoEfc.UserFields.Fields.Item("U_BPP_MPPG").Value = lo_DBDSCCHAPR.GetValue(gs_UflMPSUNAT, 0).Trim();
                switch (po_Form.PaneLevel)
                {
                    case 1: //Pago con cheque 
                        lo_PgoEfc.Checks.AccounttNum = lo_DBDSCCHAPR.GetValue(gs_UflCtaCnt, 0).Trim();
                        lo_PgoEfc.Checks.BankCode = lo_DBDSCCHAPR.GetValue(gs_UflChqBnc, 0).Trim();
                        lo_PgoEfc.Checks.Branch = lo_DBDSCCHAPR.GetValue(gs_UflMndCaja, 0).Trim();
                        lo_ArrCad = Cls_QueriesManager_CCH.CuentadeBancoPropio.Split(new char[] { '?' });
                        ls_Qry = lo_ArrCad[0] + lo_DBDSCCHAPR.GetValue(gs_UflChqBnc, 0).Trim() + lo_ArrCad[1] + lo_DBDSCCHAPR.GetValue(gs_UflCtaCnt, 0).Trim() + lo_ArrCad[2];
                        lo_RecSet.DoQuery(ls_Qry);
                        if (lo_RecSet.EoF)
                        {
                            ps_DscErr = "La cuenta bancaria no esta relacionada a una cuenta contable...";
                            pi_CodErr = -1;
                            return;
                        }
                        ls_CdgCta = ((string)lo_RecSet.Fields.Item(0).Value).Trim();
                        lo_PgoEfc.Checks.CheckAccount = ls_CdgCta;
                        if (lo_DBDSCCHAPR.GetValue(gs_UflChqMnl, 0).Trim() != "Y")
                        {
                            lo_PgoEfc.Checks.ManualCheck = SAPbobsCOM.BoYesNoEnum.tNO;
                        }
                        else
                        {
                            lo_PgoEfc.Checks.ManualCheck = SAPbobsCOM.BoYesNoEnum.tYES;
                            lo_PgoEfc.Checks.CheckNumber = Convert.ToInt32(lo_DBDSCCHAPR.GetValue(gs_UflChqNum, 0).Trim());
                        }
                        lo_PgoEfc.Checks.CheckSum = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                        lo_PgoEfc.Checks.CountryCode = "PE";
                        lo_PgoEfc.Checks.DueDate = DateTime.ParseExact(lo_DBDSCCHAPR.GetValue(gs_UflChqFchVnc, 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        lo_PgoEfc.Checks.Trnsfrable = SAPbobsCOM.BoYesNoEnum.tNO;
                        //Cash Flow
                        if (lo_DBDSCCHAPR.GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if(lo_DBDSCCHAPR.GetValue(gs_UflMndCaja, 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountLC = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                            }
                            else
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountFC = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                            }
                            lo_PgoEfc.PrimaryFormItems.CheckNumber = "0";
                            lo_PgoEfc.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(lo_DBDSCCHAPR.GetValue(gs_UflCshFlw, 0).Trim());
                            lo_PgoEfc.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtChecks;
                            lo_PgoEfc.PrimaryFormItems.Add();
                        }
                        break;
                    case 2://Pago con Transferencia
                        ls_CdgCta = lo_DBDSCCHAPR.GetValue(gs_UflCtaCnt, 0).Trim();
                        lo_PgoEfc.TransferAccount = ls_CdgCta;
                        lo_PgoEfc.TransferDate = DateTime.ParseExact(lo_DBDSCCHAPR.GetValue(gs_UflTrnFch, 0).Trim(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        lo_PgoEfc.TransferReference = lo_DBDSCCHAPR.GetValue(gs_uflTrnRef, 0).Trim();
                        lo_PgoEfc.TransferSum = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                        //Cash Flow
                        if (lo_DBDSCCHAPR.GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if (lo_DBDSCCHAPR.GetValue(gs_UflMndCaja, 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountLC = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                            }
                            else
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountFC = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                            }
                            lo_PgoEfc.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(lo_DBDSCCHAPR.GetValue(gs_UflCshFlw, 0).Trim());
                            lo_PgoEfc.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtBankTransfer;
                            lo_PgoEfc.PrimaryFormItems.Add();
                        }
                        break;
                    case 3://Pago en Efectivo
                        ls_CdgCta = lo_DBDSCCHAPR.GetValue(gs_UflCtaCnt, 0).Trim();
                        lo_PgoEfc.CashAccount = ls_CdgCta;
                        lo_PgoEfc.CashSum = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                        //Cash Flow
                        if (lo_DBDSCCHAPR.GetValue(gs_UflCshFlw, 0).Trim() != string.Empty)
                        {
                            if (lo_DBDSCCHAPR.GetValue(gs_UflMndCaja, 0).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountLC = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                            }
                            else
                            {
                                lo_PgoEfc.PrimaryFormItems.AmountFC = Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim());
                            }
                            lo_PgoEfc.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtCash;
                            lo_PgoEfc.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(lo_DBDSCCHAPR.GetValue(gs_UflCshFlw, 0).Trim());
                            lo_PgoEfc.PrimaryFormItems.Add();
                        }
                        break;
                }
                for (int i = 0; i < lo_DBDSCCHAPRDET.Size; i++)
                {
                    lo_DBDSCCHAPRDET.Offset = i;
                    lo_PgoEfc.AccountPayments.AccountCode = lo_DBDSCCHAPRDET.GetValue(gs_UflClmCtaSys, i).Trim();
                    lo_ChrtOfAcct.GetByKey(lo_DBDSCCHAPRDET.GetValue(gs_UflClmCtaSys, i).Trim());
                    lo_PgoEfc.AccountPayments.AccountName = lo_ChrtOfAcct.Name;
                    lo_PgoEfc.AccountPayments.GrossAmount = Convert.ToDouble(lo_DBDSCCHAPRDET.GetValue(gs_UflClmMntApr, i));
                    lo_PgoEfc.AccountPayments.SumPaid = lo_PgoEfc.AccountPayments.GrossAmount;
                    lo_PgoEfc.AccountPayments.Decription = lo_DBDSCCHAPRDET.GetValue(gs_UflClmCmntrs, i).Trim();
                    lo_PgoEfc.AccountPayments.ProjectCode = lo_DBDSCCHAPRDET.GetValue(gs_UflClmPrycto, i).Trim();
                    lo_PgoEfc.AccountPayments.ProfitCenter = lo_DBDSCCHAPRDET.GetValue(gs_UflClmDmnsn1, i).Trim();
                    lo_PgoEfc.AccountPayments.ProfitCenter2 = lo_DBDSCCHAPRDET.GetValue(gs_UflClmDmnsn2, i).Trim();
                    lo_PgoEfc.AccountPayments.ProfitCenter3 = lo_DBDSCCHAPRDET.GetValue(gs_UflClmDmnsn3, i).Trim();
                    lo_PgoEfc.AccountPayments.ProfitCenter4 = lo_DBDSCCHAPRDET.GetValue(gs_UflClmDmnsn4, i).Trim();
                    lo_PgoEfc.AccountPayments.ProfitCenter5 = lo_DBDSCCHAPRDET.GetValue(gs_UflClmDmnsn5, i).Trim();
                    lo_PgoEfc.AccountPayments.Add();
                }
                //Valido si se ha sobrepasado el saldo de la cuenta
                ls_Qry = @"SELECT ""AcctCode"" FROM OACT WHERE ""AcctCode"" = '" + ls_CdgCta + "'";
                lo_RecSet.DoQuery(ls_Qry);
                if (!lo_RecSet.EoF)
                { 
                     if (lo_ChrtOfAcct.GetByKey(lo_RecSet.Fields.Item(0).Value))
                    { 
                        if(Convert.ToDouble(lo_DBDSCCHAPR.GetValue(gs_UflMntTotApr, 0).Trim()) > lo_ChrtOfAcct.Balance)
                        {
                          lo_DlgRsl = (System.Windows.Forms.DialogResult) Cls_Global.go_SBOApplication.MessageBox("Se ha sobrepasado el saldo de la cuenta contable " + lo_ChrtOfAcct.Name + ". Saldo " + lo_ChrtOfAcct.Balance.ToString("#.00") + "\n" +
                                "¿Desea continuar?", 1, "Si", "No");
                          if (lo_DlgRsl != System.Windows.Forms.DialogResult.OK)
                          {
                              pi_CodErr = -1;
                              return;
                          }
                        }
                    }
                }

                if (lo_PgoEfc.Add() != 0)
                {
                    go_SBOCompany.GetLastError(out pi_CodErr, out ps_DscErr);
                    ps_DscErr = go_SBOCompany.GetLastErrorDescription();
                    return;
                }
                else
                {                   
                    po_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflDocEntPgo, 0, go_SBOCompany.GetNewObjectKey());
                    if (lo_PgoEfc.GetByKey(Convert.ToInt32(go_SBOCompany.GetNewObjectKey())))
                    {
                        //Actulizo datos del asiento del pago efectuado
                        po_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflDocNumPgo, 0, lo_PgoEfc.DocNum.ToString());
                        ls_XMLPgo = lo_PgoEfc.GetAsXML();
                        if (lo_PgoAs.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<TransId>") + 9, ls_XMLPgo.IndexOf("</TransId>") - ls_XMLPgo.IndexOf("<TransId>") - 9))))
                        {
                            lo_PgoAs.TransactionCode = "CCH";
                            for (int i = 0; i < lo_PgoAs.Lines.Count; i++)
                            {
                                lo_PgoAs.Lines.SetCurrentLine(i);
                                lo_ChrtOfAcct.GetByKey(lo_PgoAs.Lines.AccountCode);
                                if (lo_ChrtOfAcct.UserFields.Fields.Item("U_CE_ACCT").Value != "Y") continue;
                                lo_PgoAs.Lines.Reference1 = lo_DBDSCCHAPRDET.GetValue(gs_UflClmCCHCod, i - 1);
                                lo_PgoAs.Lines.Reference2 = lo_DBDSCCHAPRDET.GetValue(gs_UflClmNmrCCH, i - 1);
                                lo_PgoAs.Lines.LineMemo = lo_DBDSCCHAPRDET.GetValue(gs_UflClmCmntrs, i - 1);
                            }
                            lo_PgoAs.Update();
                        }
                    }
                    //Si existe traspaso de saldo entonces realizo el siguiente update para cerrar las cajas traspasadas
                    for (int i = 0; i < lo_DBDSCCHAPRDET.Size; i++)
                    {
                        if (lo_DBDSCCHAPRDET.GetValue(gs_UflClmCCHTrs, i).Trim() != string.Empty && lo_DBDSCCHAPRDET.GetValue(gs_UflClmCCHTrs, i).Trim() != "---")
                        {
                            Cls_QueriesManager_CCH.sb_ActualizarEstadoySaldoXNroCCH("C",0.0, lo_DBDSCCHAPRDET.GetValue(gs_UflClmCCHCod, i).Trim(), lo_DBDSCCHAPRDET.GetValue(gs_UflClmCCHTrs, i).Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ps_DscErr = ex.Message;
                pi_CodErr = -1;
            }
            finally
            {
                lo_RecSet = null;
            }
        }

    }
}
