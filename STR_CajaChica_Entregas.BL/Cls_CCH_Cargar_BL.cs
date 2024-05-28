using STR_CajaChica_Entregas.DL;
using STR_CajaChica_Entregas.UTIL;
using System;
using System.Linq;
using System.Xml.Linq;

namespace STR_CajaChica_Entregas.BL
{
    public static class Cls_CCH_Cargar_BL
    {
        private static SAPbobsCOM.Company go_SBOCompany = Cls_Global.go_SBOCompany;
        //* * * * * * * * * * * * * * DataSources* * * * * * * * * * * * * * *
        private const string gs_DtcCCHCRG = "@STR_CCHCRG";
        private const string gs_DtdCCHCRGDET = "@STR_CCHCRGDET";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * * * User Fields - @STR_CCHCRG * * * * * * * * 
        private static string gs_UflCabDocEnt = "DocEntry";
        private static string gs_UflCabMndCCH = "U_CC_MNDA";
        private static string gs_UflCabCodCCH = "U_CC_NMBR";
        private static string gs_UflCabNroCCH = "U_CC_NMRO";
        private static string gs_UflCabTtXCnt = "U_CC_TTDC";
        private static string gs_UflCabSldFin = "U_CC_SLDF";
        private static string gs_UflCabSldIni = "U_CC_SLDI";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * User Fields - @STR_CCHCRGDET * * * * * * * * 
        private static string gs_UflDetSelecc = "U_CC_SLCC";
        private static string gs_UflDetCodPrv = "U_CC_CDPV";
        private static string gs_UflDetDocFch = "U_CC_FCDC";
        private static string gs_UflDetDocMnd = "U_CC_MNDC";
        private static string gs_UflDetDocTpo = "U_CC_TDOC";
        private static string gs_UflDetDocSre = "U_CC_SDOC";
        private static string gs_UflDetDocCrr = "U_CC_CDOC";
        private static string gs_UflDetLineId = "LineId";
        private static string gs_UflDetTotXLn = "U_CC_TTLN";
        private static string gs_UflDetCntArt = "U_CC_CNAR";
        private static string gs_UflDetDocEst = "U_CC_ESTD";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        public static void sb_GenerarDocumentosyPagos(ref SAPbouiCOM.Form po_Form, ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbouiCOM.DBDataSource lo_DBDSCCHCRG = null;
            SAPbouiCOM.DBDataSource lo_DBDSCCHCRGDET = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbobsCOM.Documents lo_Doc = null;

            XElement lo_XElmnt = null;
            dynamic lo_BsnssObj = null;
            string ls_DocEntFrm = string.Empty;
            string ls_XmlBsnssObj = string.Empty;
            string ls_CntaCCH = string.Empty;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;;
            string ls_Filas = string.Empty;
            string ls_CodPrv = string.Empty;
            string ls_CodPrvS = string.Empty;
            string ls_DocFch = string.Empty;
            string ls_DocFchS = string.Empty;
            string ls_DocMnd = string.Empty;
            string ls_DocMndS = string.Empty;
            string ls_DocTpo = string.Empty;
            string ls_DocTpoS = string.Empty;
            string ls_DocSre = string.Empty;
            string ls_DocSreS = string.Empty;
            string ls_DocCrr = string.Empty;
            string ls_DocCrrS = string.Empty;
            string ls_LineId = string.Empty;
            bool lb_UltFila = true;
            bool lb_Flag = true;
            try
            {               
                lo_DBDSCCHCRG = po_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG);
                lo_DBDSCCHCRGDET = po_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET);
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                lo_Doc = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);

                while (lo_DBDSCCHCRGDET.Size > 0)
                {
                    lb_Flag = false;
                    for (int i = 0; i < lo_DBDSCCHCRGDET.Size; i++)
                    {
                        if (lo_DBDSCCHCRGDET.GetValue(gs_UflDetSelecc, i) == "Y" && lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocEst, i).Trim().ToUpper() == "CRE")
                        {
                            if (go_SBOCompany.InTransaction)
                                go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                            else
                                //Se inicia la transaccion....
                                go_SBOCompany.StartTransaction();
                            ls_DocEntFrm = lo_DBDSCCHCRG.GetValue(gs_UflCabDocEnt, 0);
                            ls_CodPrv = lo_DBDSCCHCRGDET.GetValue(gs_UflDetCodPrv, i).Trim();
                            ls_DocTpo = lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocTpo, i).Trim();;
                            ls_DocSre = lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocSre, i).Trim();
                            ls_DocCrr = lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocCrr, i).Trim();
                            ls_Filas = Cls_QueriesManager_CCH.fn_ObtenerLineasDocumento(ls_DocEntFrm, ls_CodPrv, ls_DocTpo, ls_DocSre, ls_DocCrr);
                            ls_XmlBsnssObj = @"<?xml version=""1.0"" encoding=""utf-16""?>";
                            lo_XElmnt = XElement.Parse(sb_CleanUpData(Cls_QueriesManager_CCH.GenerarDocumentoXML(ls_DocEntFrm, ls_Filas, gs_DtdCCHCRGDET)));
                            lo_XElmnt.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
                            ls_XmlBsnssObj += lo_XElmnt.ToString();
                            go_SBOCompany.XMLAsString = true;
                            lo_BsnssObj = go_SBOCompany.GetBusinessObjectFromXML(ls_XmlBsnssObj, 0);
                            if (lo_BsnssObj.Add() != 0)
                            {
                                try
                                {
                                    if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                }
                                catch (Exception ex)
                                {
                                    Cls_Global.WriteToFile(ex.Message);
                                }
                                go_SBOCompany.GetLastError(out pi_CodErr, out ps_DscErr);
                                Cls_Global.go_SBOApplication.SetStatusBarMessage(pi_CodErr + " - " + ps_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                lo_ArrCad = Cls_QueriesManager_CCH.ActualizarEstadodeCreacion.Split(new char[] { '?' });
                                ls_Qry = lo_ArrCad[0].Trim() + lo_Doc.DocEntry + lo_ArrCad[1].Trim() + "ERR" + lo_ArrCad[2].Trim() + ls_DocEntFrm + lo_ArrCad[3].Trim() + ls_Filas + lo_ArrCad[4].Trim();
                                Cls_Global.WriteToFile(ls_Qry);
                                lo_RecSet.DoQuery(ls_Qry);
                                sb_UpdateDataMatrix(ref po_Form);
                            }
                            else
                            {
                                lo_ArrCad = Cls_QueriesManager_CCH.CuentadeCajaChica.Split(new char[] { '?' });
                                ls_Qry = lo_ArrCad[0].Trim() + lo_DBDSCCHCRG.GetValue(gs_UflCabCodCCH, 0).Trim() + lo_ArrCad[1].Trim();
                                lo_Doc.GetByKey(Convert.ToInt32(go_SBOCompany.GetNewObjectKey()));
                                sb_ActualizarReferenciasAsDoc(lo_Doc, po_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCabCodCCH, 0).Trim(), po_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCabNroCCH, 0).Trim());
                                Cls_Global.WriteToFile(ls_Qry);
                                lo_RecSet.DoQuery(ls_Qry);
                                ls_CntaCCH = lo_RecSet.Fields.Item(0).Value;
                                if (sb_Pago(new SAPbobsCOM.Documents[] { lo_Doc }, lo_DBDSCCHCRG, ls_CntaCCH, ref pi_CodErr, ref ps_DscErr))
                                {
                                    try
                                    {
                                        if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                                        lo_ArrCad = Cls_QueriesManager_CCH.ActualizarEstadodeCreacion.Split(new char[] { '?' });
                                        ls_Qry = lo_ArrCad[0].Trim() + lo_Doc.DocEntry + lo_ArrCad[1].Trim() + "OK" + lo_ArrCad[2].Trim() + ls_DocEntFrm + lo_ArrCad[3].Trim() + ls_Filas + lo_ArrCad[4].Trim();
                                        Cls_Global.WriteToFile(ls_Qry);
                                        lo_RecSet.DoQuery(ls_Qry);
                                        lb_Flag = true;
                                        ls_Filas = string.Empty;
                                        sb_UpdateDataMatrix(ref po_Form);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Cls_Global.WriteToFile(ex.Message);
                                        lo_ArrCad = Cls_QueriesManager_CCH.ActualizarEstadodeCreacion.Split(new char[] { '?' });
                                        ls_Qry = lo_ArrCad[0].Trim() + lo_Doc.DocEntry + lo_ArrCad[1].Trim() + "ERR" + lo_ArrCad[2].Trim() + ls_DocEntFrm + lo_ArrCad[3].Trim() + ls_Filas + lo_ArrCad[4].Trim();
                                        Cls_Global.WriteToFile(ls_Qry);
                                        lo_RecSet.DoQuery(ls_Qry);
                                        Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short,SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                    }
                                    finally
                                    {
                                        sb_UpdateDataMatrix(ref po_Form);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                        lo_ArrCad = Cls_QueriesManager_CCH.ActualizarEstadodeCreacion.Split(new char[] { '?' });
                                        ls_Qry = lo_ArrCad[0].Trim() + lo_Doc.DocEntry + lo_ArrCad[1].Trim() + "ERR" + lo_ArrCad[2].Trim() + ls_DocEntFrm + lo_ArrCad[3].Trim() + ls_Filas + lo_ArrCad[4].Trim();
                                        Cls_Global.WriteToFile(ls_Qry);
                                        lo_RecSet.DoQuery(ls_Qry);
                                        go_SBOCompany.GetLastError(out pi_CodErr, out ps_DscErr);
                                        Cls_Global.go_SBOApplication.SetStatusBarMessage(pi_CodErr + " - " + ps_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                    }
                                    catch (Exception ex)
                                    {
                                        Cls_Global.WriteToFile(ex.Message);
                                        Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                    }

                                    sb_UpdateDataMatrix(ref po_Form);
                                }
                            }
                            ls_Filas = string.Empty;
                        }    
                    }
                    if (!lb_Flag) break;
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                ps_DscErr = ex.Message;
                pi_CodErr = -1;
                Cls_Global.go_SBOApplication.StatusBar.SetText(ps_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
            }
            finally
            {
                lo_RecSet = null;
            }
        }

        private static void sb_UpdateDataMatrix(ref SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbouiCOM.Matrix lo_Matrix = null;

            try
            {
                po_Form.Freeze(true);
                lo_Cnds = Cls_Global.go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
                lo_Matrix = po_Form.Items.Item("MtxDocs").Specific;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DocEntry";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = po_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("DocEntry", 0);
                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.BracketOpenNum = 2;
                lo_Cnd.Alias = "U_CC_ESTD";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "ERR";
                lo_Cnd.BracketCloseNum = 1;
                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.BracketOpenNum = 1;
                lo_Cnd.Alias = "U_CC_ESTD";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "CRE";
                lo_Cnd.BracketCloseNum = 2;
                po_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).Query(lo_Cnds);
                lo_Matrix.LoadFromDataSource();
                lo_Matrix.Columns.Item(0).TitleObject.Sort(SAPbouiCOM.BoGridSortType.gst_Ascending);
                lo_Matrix.FlushToDataSource();
                lo_Matrix.LoadFromDataSource();
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                Cls_Global.go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                po_Form.Freeze(false);
            }


        }

        private static bool sb_Pago(SAPbobsCOM.Documents[] po_ArrDocs,SAPbouiCOM.DBDataSource ps_DBDts,string ps_CntCCH,ref int pi_CodErr,ref string ps_DscErr)
        {
            SAPbobsCOM.Payments lo_Pay = null;
            double ld_TpoCmb = 0.0;
            lo_Pay = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
            try
            {
                //Datos de Cabecera
                lo_Pay.CardCode = po_ArrDocs[0].CardCode;
                lo_Pay.CardName = po_ArrDocs[0].CardName;
                lo_Pay.DocDate = po_ArrDocs[0].DocDate;
                lo_Pay.DocCurrency = ps_DBDts.GetValue(gs_UflCabMndCCH, 0).Trim();
                if (lo_Pay.DocCurrency != Cls_Global.sb_ObtenerMonedaLocal())
                {
                    lo_Pay.DocRate = Cls_Global.sb_ObtenerTipodeCambioXDia(lo_Pay.DocCurrency, lo_Pay.DocDate,ref pi_CodErr,ref ps_DscErr);
                }
                lo_Pay.DocType = SAPbobsCOM.BoRcptTypes.rSupplier;
                lo_Pay.CashAccount = ps_CntCCH;
                lo_Pay.UserFields.Fields.Item("U_BPP_TIPR").Value = "CCH";
                lo_Pay.UserFields.Fields.Item("U_BPP_CCHI").Value = ps_DBDts.GetValue(gs_UflCabCodCCH, 0).Trim();
                lo_Pay.UserFields.Fields.Item("U_BPP_NUMC").Value = ps_DBDts.GetValue(gs_UflCabNroCCH, 0).Trim();

                //Datos de Detalle
                lo_Pay.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_PurchaseInvoice;
                lo_Pay.Invoices.DocEntry = po_ArrDocs[0].DocEntry;
                if (po_ArrDocs[0].DocCurrency == Cls_Global.sb_ObtenerMonedaLocal())
                {
                    lo_Pay.Invoices.SumApplied = po_ArrDocs[0].DocTotal;
                }
                else
                {
                    lo_Pay.Invoices.AppliedFC = po_ArrDocs[0].DocTotalFc;
                }

                if (lo_Pay.DocCurrency == Cls_Global.sb_ObtenerMonedaLocal())
                {
                    if (po_ArrDocs[0].DocCurrency == Cls_Global.sb_ObtenerMonedaLocal())
                    {
                        lo_Pay.CashSum = po_ArrDocs[0].DocTotal;
                    }
                    else
                    {
                        ld_TpoCmb = Cls_Global.sb_ObtenerTipodeCambioXDia(po_ArrDocs[0].DocCurrency, lo_Pay.DocDate, ref pi_CodErr, ref ps_DscErr);
                        if (pi_CodErr == 0 && ps_DscErr == string.Empty)
                        {
                            lo_Pay.CashSum = po_ArrDocs[0].DocTotalFc * ld_TpoCmb;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (po_ArrDocs[0].DocCurrency == Cls_Global.sb_ObtenerMonedaLocal())
                    {
                        lo_Pay.CashSum = po_ArrDocs[0].DocTotal / lo_Pay.DocRate;
                    }
                    else
                    {
                        lo_Pay.CashSum = po_ArrDocs[0].DocTotalFc;
                    }
                }
                lo_Pay.Invoices.Add();
                if (lo_Pay.Add() != 0)
                {
                    go_SBOCompany.GetLastError(out pi_CodErr, out ps_DscErr);
                    return false;
                }
                else
                {
                    sb_ActualizarReferenciasAsPgo(Convert.ToInt32(go_SBOCompany.GetNewObjectKey()), ps_DBDts.GetValue(gs_UflCabCodCCH, 0).Trim(), ps_DBDts.GetValue(gs_UflCabNroCCH, 0).Trim());
                    return true;
                }
            }
            catch(Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                pi_CodErr = -1;
                ps_DscErr = ex.Message;
                return false;
            }

        }

        public static void sb_InfoTotalesXCarga(SAPbouiCOM.Form po_Form, ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbouiCOM.DBDataSource lo_DBDSCCHCRG = null;
            SAPbouiCOM.DBDataSource lo_DBDSCCHCRGDET = null;
            string ls_MndCCH = string.Empty;
            double ld_MntTotDcs = 0.0;
            double ld_SldIniCCH = 0.0;
            double ld_SldFinCCH = 0.0;
            
            try
            {
                lo_DBDSCCHCRG = po_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG);
                lo_DBDSCCHCRGDET = po_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET);
                ls_MndCCH = lo_DBDSCCHCRG.GetValue(gs_UflCabMndCCH, 0).Trim();
                ld_SldIniCCH = Convert.ToDouble(lo_DBDSCCHCRG.GetValue(gs_UflCabSldIni, 0).Trim());

                for (int i = 0; i < lo_DBDSCCHCRGDET.Size; i++)
                {
                    if (lo_DBDSCCHCRGDET.GetValue(gs_UflDetSelecc, i).Trim() == "Y")
                    {

                        if (ls_MndCCH == Cls_Global.sb_ObtenerMonedaLocal())
                        {
                            if (lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocMnd, i).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {

                                ld_MntTotDcs += Convert.ToDouble(lo_DBDSCCHCRGDET.GetValue(gs_UflDetTotXLn, i));
                            }
                            else
                            {
                                ld_MntTotDcs += (Convert.ToDouble(lo_DBDSCCHCRGDET.GetValue(gs_UflDetTotXLn, i)) * Cls_Global.sb_ObtenerTipodeCambioXDia(lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocMnd, i).Trim(),
                                    DateTime.ParseExact(lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocFch, i).Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), ref pi_CodErr, ref ps_DscErr));
                            }
                        }
                        else
                        {
                            if (lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocMnd, i).Trim() == Cls_Global.sb_ObtenerMonedaLocal())
                            {
                                ld_MntTotDcs += Convert.ToDouble(lo_DBDSCCHCRGDET.GetValue(gs_UflDetTotXLn, i)) / Cls_Global.sb_ObtenerTipodeCambioXDia(ls_MndCCH.Trim(),
                                    DateTime.ParseExact(lo_DBDSCCHCRGDET.GetValue(gs_UflDetDocFch, i).Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), ref pi_CodErr, ref ps_DscErr);
                            }
                            else
                            {
                                ld_MntTotDcs += Convert.ToDouble(lo_DBDSCCHCRGDET.GetValue(gs_UflDetTotXLn, i));
                            }
                        }
                    }
                }

                ld_SldFinCCH = ld_SldIniCCH - ld_MntTotDcs;
                po_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflCabTtXCnt, 0, ld_MntTotDcs.ToString());
                po_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflCabSldFin,0,ld_SldFinCCH.ToString());
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                pi_CodErr = -1;
                ps_DscErr = ex.Message;
            }

        }

        private static void sb_ActualizarReferenciasAsDoc(SAPbobsCOM.Documents po_Doc,string ps_CCHCdg,string ps_CCHNmr)
        {
            SAPbobsCOM.JournalEntries lo_DocAs = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtAcct = null;
            try
            {
                lo_DocAs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                lo_ChrtAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);

                if (lo_DocAs.GetByKey(po_Doc.TransNum))
                {
                    lo_DocAs.TransactionCode = "CCH";
                    for (int i = 0; i < lo_DocAs.Lines.Count; i++)
                    {
                        lo_DocAs.Lines.SetCurrentLine(i);
                        if(lo_ChrtAcct.GetByKey(lo_DocAs.Lines.AccountCode))
                        {
                            if(lo_ChrtAcct.AccountType == SAPbobsCOM.BoAccountTypes.at_Expenses)
                            {
                                lo_DocAs.Lines.Reference2 = ps_CCHCdg;
                                lo_DocAs.Lines.AdditionalReference = ps_CCHNmr;                        
                            }
                        }
                    }
                    lo_DocAs.Update();
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                Cls_Global.go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }

        }

        private static void sb_ActualizarReferenciasAsPgo(int pi_DocEnt, string ps_CCHCdg, string ps_CCHNmr)
        {
            string ls_XMLPgo = string.Empty;
            SAPbobsCOM.JournalEntries lo_PgoAs = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtAcct = null;
            SAPbobsCOM.Payments lo_PgoEfc = null;

            lo_PgoAs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
            lo_ChrtAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
            lo_PgoEfc = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
            if (lo_PgoEfc.GetByKey(pi_DocEnt))
            {
                ls_XMLPgo = lo_PgoEfc.GetAsXML();
                if (lo_PgoAs.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<TransId>") + 9, ls_XMLPgo.IndexOf("</TransId>") - ls_XMLPgo.IndexOf("<TransId>") - 9))))
                {
                    lo_PgoAs.TransactionCode = "CCH";
                    for (int i = 0; i < lo_PgoAs.Lines.Count; i++)
                    {
                        lo_PgoAs.Lines.SetCurrentLine(i);
                        if (lo_ChrtAcct.GetByKey(lo_PgoAs.Lines.AccountCode))
                        { 
                            if (lo_ChrtAcct.UserFields.Fields.Item("U_CE_ACCT").Value == "Y")
                            {
                                lo_PgoAs.Lines.Reference2 = ps_CCHCdg;
                                lo_PgoAs.Lines.AdditionalReference = ps_CCHNmr;
                            }
                        }
                    }
                    lo_PgoAs.Update();
                }
            }
        }

        private static string sb_CleanUpData(string data)
        {
            var r = new System.Text.RegularExpressions.Regex(@"&");
            string output = r.Replace(data, "&amp; ");
            return output;
        }
    }
}
