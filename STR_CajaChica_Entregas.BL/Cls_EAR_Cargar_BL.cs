using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;
using System.Xml;
using System.Xml.Linq;


namespace STR_CajaChica_Entregas.BL
{
    public static class Cls_EAR_Cargar_BL
    {
        private static SAPbobsCOM.Company go_SBOCompany = Cls_Global.go_SBOCompany;
        //* * * * * * * * * * * * * * DataSources* * * * * * * * * * * * * * *
        private const string gs_DtcEARCRG = "@STR_EARCRG";
        private const string gs_DtdEARCRGDET = "@STR_EARCRGDET";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * * * User Fields - @STR_CCHCRG * * * * * * * * 
        private static string gs_UflCabDocEnt = "DocEntry";
        private static string gs_UflCabMndEAR = "U_ER_MNDA";
        private static string gs_UflCabCodEAR = "U_ER_NMBR";
        private static string gs_UflCabNroEAR = "U_ER_NMRO";
        private static string gs_UflCabTtXCnt = "U_ER_TTDC";
        private static string gs_UflCabSldFin = "U_ER_SLDF";
        private static string gs_UflCabSldIni = "U_ER_SLDI";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * User Fields - @STR_CCHCRGDET * * * * * * * * 
        private static string gs_UflDetSelecc = "U_ER_SLCC";
        private static string gs_UflDetCodPrv = "U_ER_CDPV";
        private static string gs_UflDetDocFch = "U_ER_FCDC";
        private static string gs_UflDetDocMnd = "U_ER_MNDC";
        private static string gs_UflDetDocTpo = "U_ER_TDOC";
        private static string gs_UflDetDocSre = "U_ER_SDOC";
        private static string gs_UflDetDocCrr = "U_ER_CDOC";
        private static string gs_UflDetLineId = "LineId";
        private static string gs_UflDetTotXLn = "U_ER_TTLN";
        private static string gs_UflDetCntArt = "U_ER_CNAR";
        private static string gs_UflDetDocEst = "U_ER_ESTD";
        private static string gs_UflDetDocFlj = "U_ER_CSHF";    // Add flujo de caja
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        public static void sb_GenerarDocumentosyPagos(ref SAPbouiCOM.Form po_Form, ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbouiCOM.DBDataSource lo_DBDSEARCRG = null;
            SAPbouiCOM.DBDataSource lo_DBDSEARCRGDET = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbobsCOM.Documents lo_Doc = null;
            SAPbobsCOM.UserTable lo_UTbl = null;
            XElement lo_XElmnt = null;

            dynamic lo_BsnssObj = null;
            string ls_DocEntFrm = string.Empty;
            string ls_XmlBsnssObj = string.Empty;
            string ls_CtaPteEAR = string.Empty;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null; ;
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
                lo_DBDSEARCRG = po_Form.DataSources.DBDataSources.Item(gs_DtcEARCRG);
                lo_DBDSEARCRGDET = po_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET);
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                lo_Doc = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                lo_UTbl = go_SBOCompany.UserTables.Item("STR_CCHEAR_SYS");

                while (lo_DBDSEARCRGDET.Size > 0)
                {
                    lb_Flag = false;
                    for (int i = 0; i < lo_DBDSEARCRGDET.Size; i++)
                    {
                        if (lo_DBDSEARCRGDET.GetValue(gs_UflDetSelecc, i) == "Y" && lo_DBDSEARCRGDET.GetValue(gs_UflDetDocEst, i).Trim().ToUpper() == "CRE")
                        {
                            if (go_SBOCompany.InTransaction)
                                go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                            go_SBOCompany.StartTransaction();
                            ls_DocEntFrm = lo_DBDSEARCRG.GetValue(gs_UflCabDocEnt, 0);
                            ls_CodPrv = lo_DBDSEARCRGDET.GetValue(gs_UflDetCodPrv, i).Trim();
                            ls_DocTpo = lo_DBDSEARCRGDET.GetValue(gs_UflDetDocTpo, i).Trim(); ;
                            ls_DocSre = lo_DBDSEARCRGDET.GetValue(gs_UflDetDocSre, i).Trim();
                            ls_DocCrr = lo_DBDSEARCRGDET.GetValue(gs_UflDetDocCrr, i).Trim();
                            ls_Filas = Cls_QueriesManager_EAR.fn_ObtenerLineasDocumento(ls_DocEntFrm, ls_CodPrv, ls_DocTpo, ls_DocSre, ls_DocCrr);
                            ls_XmlBsnssObj = @"<?xml version=""1.0"" encoding=""utf-16""?>";

                            lo_XElmnt = XElement.Parse(sb_CleanUpData(Cls_QueriesManager_EAR.GenerarDocumentoXML(ls_DocEntFrm, ls_Filas, gs_DtdEARCRGDET)));
                            lo_XElmnt.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
                            ls_XmlBsnssObj += lo_XElmnt.ToString();
                            go_SBOCompany.XMLAsString = true;
                            lo_BsnssObj = go_SBOCompany.GetBusinessObjectFromXML(ls_XmlBsnssObj, 0);
                            Cls_Global.WriteToFile(lo_BsnssObj.GetAsXML().ToString()); 
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
                                Cls_Global.go_SBOApplication.StatusBar.SetText(pi_CodErr + " - " + ps_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                Cls_QueriesManager_EAR.ActualizarEstadodeCreacion(string.Empty, "ERR", ls_DocEntFrm, ls_Filas);
                                sb_UpdateDataMatrix(ref po_Form);
                            }
                            else
                            {
                                lo_UTbl.GetByKey("001");
                                ls_CtaPteEAR = lo_UTbl.UserFields.Fields.Item("U_CE_CTPT").Value;//Cuenta puente entregas a rendir
                                ls_CtaPteEAR = Cls_QueriesManager_EAR.fn_ObtenerCodigoCtaPuenteEAR(ls_CtaPteEAR);
                                lo_Doc.GetByKey(Convert.ToInt32(go_SBOCompany.GetNewObjectKey()));
                                sb_ActualizarReferenciasAsDoc(lo_Doc, po_Form.DataSources.DBDataSources.Item(gs_DtcEARCRG).GetValue(gs_UflCabCodEAR, 0).Trim(), po_Form.DataSources.DBDataSources.Item(gs_DtcEARCRG).GetValue(gs_UflCabNroEAR, 0).Trim());
                                if (sb_Pago(new SAPbobsCOM.Documents[] { lo_Doc }, lo_DBDSEARCRG, lo_DBDSEARCRGDET.GetValue(gs_UflDetDocFlj, i).Trim(),ls_CtaPteEAR, ref pi_CodErr, ref ps_DscErr))
                                {
                                    try
                                    {
                                        if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                                        Cls_QueriesManager_EAR.ActualizarEstadodeCreacion(lo_Doc.DocEntry.ToString(), "OK", ls_DocEntFrm, ls_Filas);
                                        lb_Flag = true;
                                        ls_Filas = string.Empty;
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Cls_QueriesManager_EAR.ActualizarEstadodeCreacion(lo_Doc.DocEntry.ToString(), "ERR", ls_DocEntFrm, ls_Filas);
                                        Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
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
                                        Cls_QueriesManager_EAR.ActualizarEstadodeCreacion(string.Empty, "ERR", ls_DocEntFrm, ls_Filas);
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
                lo_Cnd.CondVal = po_Form.DataSources.DBDataSources.Item(gs_DtcEARCRG).GetValue("DocEntry", 0);
                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.BracketOpenNum = 2;
                lo_Cnd.Alias = "U_ER_ESTD";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "ERR";
                lo_Cnd.BracketCloseNum = 1;
                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.BracketOpenNum = 1;
                lo_Cnd.Alias = "U_ER_ESTD";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "CRE";
                lo_Cnd.BracketCloseNum = 2;
                po_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET).Query(lo_Cnds);
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

        private static bool sb_Pago(SAPbobsCOM.Documents[] po_ArrDocs, SAPbouiCOM.DBDataSource ps_DBDts, string ps_fljCj,string ps_CntEAR, ref int pi_CodErr, ref string ps_DscErr)
        {
            SAPbobsCOM.Payments lo_Pay = null;
            SAPbobsCOM.JournalEntries lo_PgoAs = null;
            string ls_DocEntPay = string.Empty;
            double ld_TpoCmb = 0.0;
            bool lb_Result = true;

            lo_Pay = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
            try
            {
                //Datos de Cabecera
                lo_Pay.CardCode = po_ArrDocs[0].CardCode;
                lo_Pay.CardName = po_ArrDocs[0].CardName;
                lo_Pay.DocDate = po_ArrDocs[0].DocDate;
                lo_Pay.DocCurrency = ps_DBDts.GetValue(gs_UflCabMndEAR, 0).Trim();
                if (lo_Pay.DocCurrency != Cls_Global.sb_ObtenerMonedaLocal())
                {
                    lo_Pay.DocRate = Cls_Global.sb_ObtenerTipodeCambioXDia(lo_Pay.DocCurrency, lo_Pay.DocDate, ref pi_CodErr, ref ps_DscErr);
                }
                lo_Pay.DocType = SAPbobsCOM.BoRcptTypes.rSupplier;
                if (ps_CntEAR == string.Empty)
                {
                    Cls_Global.go_SBOApplication.StatusBar.SetText("No se ha registrado cuenta puente EAR ...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return lb_Result = false;
                }
                lo_Pay.CashAccount = ps_CntEAR;
                lo_Pay.UserFields.Fields.Item("U_BPP_TIPR").Value = "EAR";
                lo_Pay.UserFields.Fields.Item("U_BPP_CCHI").Value = ps_DBDts.GetValue(gs_UflCabCodEAR, 0).Trim();
                lo_Pay.UserFields.Fields.Item("U_BPP_NUMC").Value = ps_DBDts.GetValue(gs_UflCabNroEAR, 0).Trim();

                // Flujo de Caja 
                //lo_Pay.PrimaryFormItems.

                //Datos de Detalle
                lo_Pay.Invoices.InvoiceType = SAPbobsCOM.BoRcptInvTypes.it_PurchaseInvoice;
                lo_Pay.Invoices.DocEntry = po_ArrDocs[0].DocEntry;

                //lo_Pay.PrimaryFormItems

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

                        if (!string.IsNullOrEmpty(ps_fljCj))
                        {
                            // Flujo de caja - Validar solo si corresponde ue tenga 
                            lo_Pay.PrimaryFormItems.AmountLC = po_ArrDocs[0].DocTotal;
                            lo_Pay.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtCash;
                            lo_Pay.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(ps_fljCj);
                            lo_Pay.PrimaryFormItems.Add();
                        }
                    }
                    else
                    {
                        ld_TpoCmb = Cls_Global.sb_ObtenerTipodeCambioXDia(po_ArrDocs[0].DocCurrency, lo_Pay.DocDate, ref pi_CodErr, ref ps_DscErr);
                        if (pi_CodErr == 0 && ps_DscErr == string.Empty)
                        {
                            lo_Pay.CashSum = po_ArrDocs[0].DocTotalFc * ld_TpoCmb;

                            if (!string.IsNullOrEmpty(ps_fljCj))
                            {
                                // Flujo de caja - Validar solo si corresponde ue tenga 
                                lo_Pay.PrimaryFormItems.AmountFC = po_ArrDocs[0].DocTotalFc * ld_TpoCmb;
                                lo_Pay.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtCash;
                                lo_Pay.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(ps_fljCj);
                                lo_Pay.PrimaryFormItems.Add();
                            }
                        }
                        else
                        {
                            Cls_Global.go_SBOApplication.StatusBar.SetText(pi_CodErr + " - " + ps_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            lb_Result = false;
                        }
                    }
                }
                else
                { 
                    if (po_ArrDocs[0].DocCurrency == Cls_Global.sb_ObtenerMonedaLocal())
                    {
                        lo_Pay.CashSum = po_ArrDocs[0].DocTotal / lo_Pay.DocRate;

                        if (!string.IsNullOrEmpty(ps_fljCj))
                        {
                            // Flujo de caja - Validar solo si corresponde ue tenga 
                            lo_Pay.PrimaryFormItems.AmountFC = po_ArrDocs[0].DocTotal / lo_Pay.DocRate;
                            lo_Pay.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtCash;
                            lo_Pay.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(ps_fljCj);
                            lo_Pay.PrimaryFormItems.Add();
                        }
                    }
                    else
                    {
                        lo_Pay.CashSum = po_ArrDocs[0].DocTotalFc;

                        if (!string.IsNullOrEmpty(ps_fljCj))
                        {
                            // Flujo de caja - Validar solo si corresponde ue tenga 
                            lo_Pay.PrimaryFormItems.AmountFC = po_ArrDocs[0].DocTotalFc;
                            lo_Pay.PrimaryFormItems.PaymentMeans = SAPbobsCOM.PaymentMeansTypeEnum.pmtCash;
                            lo_Pay.PrimaryFormItems.CashFlowLineItemID = Convert.ToInt32(ps_fljCj);
                            lo_Pay.PrimaryFormItems.Add();
                        }
                    }
                }
                lo_Pay.Invoices.Add();
                string xmlobtener = lo_Pay.GetAsXML().ToString();
                Cls_Global.WriteToFile(xmlobtener);
                if (lo_Pay.Add() != 0)
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
                    Cls_Global.go_SBOApplication.StatusBar.SetText(pi_CodErr + " - " + ps_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    lb_Result = false;
                }
                else
                {
                    ls_DocEntPay = go_SBOCompany.GetNewObjectKey();
                    if (fn_ActualizarReferenciasAsPgo(Convert.ToInt32(ls_DocEntPay), ps_DBDts.GetValue(gs_UflCabCodEAR, 0).Trim(), ps_DBDts.GetValue(gs_UflCabNroEAR, 0).Trim(), ref lo_PgoAs))
                    {
                        if (!fn_GenerarAsientodeCompensacion("PE", lo_PgoAs, ps_DBDts.GetValue(gs_UflCabCodEAR, 0).Trim(), ps_DBDts.GetValue(gs_UflCabNroEAR, 0).Trim(), ls_DocEntPay))
                        {
                            try
                            {
                                if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            catch (Exception ex)
                            {
                                Cls_Global.WriteToFile(ex.Message);
                                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            }
                            lb_Result = false;
                        }
                        else
                        {
                            try
                            {
                                if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                            }
                            catch (Exception ex)
                            {
                                Cls_Global.WriteToFile(ex.Message);
                                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                lb_Result = false;
                            }
                            //sb_GenerarReconciliacion(ps_DBDts.GetValue(gs_UflCabCodEAR, 0).Trim(), Convert.ToInt32(ls_DocEntPay), ls_TrnsIdAsCmp);
                        }
                    }
                    else
                    {
                        try
                        {
                            if (go_SBOCompany.InTransaction) go_SBOCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                        }
                        catch (Exception ex)
                        {
                            Cls_Global.WriteToFile(ex.Message);
                            Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        }
                        lb_Result = false;
                    }
                }
                return lb_Result;
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                pi_CodErr = -1;
                ps_DscErr = ex.Message;
                Cls_Global.go_SBOApplication.StatusBar.SetText(pi_CodErr + " - " + ps_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
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
                lo_DBDSCCHCRG = po_Form.DataSources.DBDataSources.Item(gs_DtcEARCRG);
                lo_DBDSCCHCRGDET = po_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET);
                ls_MndCCH = lo_DBDSCCHCRG.GetValue(gs_UflCabMndEAR, 0).Trim();
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
                po_Form.DataSources.DBDataSources.Item(gs_DtcEARCRG).SetValue(gs_UflCabTtXCnt, 0, ld_MntTotDcs.ToString());
                po_Form.DataSources.DBDataSources.Item(gs_DtcEARCRG).SetValue(gs_UflCabSldFin, 0, ld_SldFinCCH.ToString());
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                pi_CodErr = -1;
                ps_DscErr = ex.Message;
            }

        }

        private static void sb_ActualizarReferenciasAsDoc(SAPbobsCOM.Documents po_Doc, string ps_CCHCdg, string ps_CCHNmr)
        {
            SAPbobsCOM.JournalEntries lo_DocAs = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtAcct = null;
            try
            {
                lo_DocAs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                lo_ChrtAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);

                if (lo_DocAs.GetByKey(po_Doc.TransNum))
                {
                    lo_DocAs.TransactionCode = "EAR";
                    for (int i = 0; i < lo_DocAs.Lines.Count; i++)
                    {
                        lo_DocAs.Lines.SetCurrentLine(i);
                        if (lo_ChrtAcct.GetByKey(lo_DocAs.Lines.AccountCode))
                        {
                            if (lo_ChrtAcct.AccountType == SAPbobsCOM.BoAccountTypes.at_Expenses)
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

        private static bool fn_ActualizarReferenciasAsPgo(int pi_DocEnt, string ps_CCHCdg, string ps_CCHNmr, ref SAPbobsCOM.JournalEntries po_PgoAs)
        {
            SAPbobsCOM.JournalEntries lo_PgoAs = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtAcct = null;
            SAPbobsCOM.Payments lo_PgoEfc = null;
            string ls_XMLPgo = string.Empty;
            bool lb_Result = true;

            lo_PgoAs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
            lo_ChrtAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
            lo_PgoEfc = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
            try
            {
                if (lo_PgoEfc.GetByKey(pi_DocEnt))
                {
                    ls_XMLPgo = lo_PgoEfc.GetAsXML();
                    if (lo_PgoAs.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<TransId>") + 9, ls_XMLPgo.IndexOf("</TransId>") - ls_XMLPgo.IndexOf("<TransId>") - 9))))
                    {
                        lo_PgoAs.TransactionCode = "EAR";
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
                        if (lo_PgoAs.Update() != 0)
                        {
                            Cls_Global.go_SBOApplication.StatusBar.SetText(go_SBOCompany.GetLastErrorCode() + " - " + go_SBOCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            lb_Result = false;
                        }
                        else
                            po_PgoAs = lo_PgoAs;
                    }
                    else
                    {
                        lb_Result = false;
                    }
                }
                else
                {
                    lb_Result = false;
                }
                return lb_Result;
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
        }

        public static bool fn_GenerarAsientodeCompensacion(string ps_TpPgo, SAPbobsCOM.JournalEntries po_AsPgo, string ps_CdgEAR, string ps_NmrEAR, string ps_KeyPgo)
        {
            SAPbobsCOM.JournalEntries lo_AsCmp = null;
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;
            string ls_CtaPte = string.Empty;

            lo_AsCmp = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
            lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
            lo_EmpInf.GetByKey(Convert.ToInt32(ps_CdgEAR.Substring(3, ps_CdgEAR.Length - 3)));
            lo_AsCmp.ReferenceDate = po_AsPgo.ReferenceDate;
            lo_AsCmp.DueDate = po_AsPgo.DueDate;
            lo_AsCmp.TaxDate = po_AsPgo.TaxDate;
            lo_AsCmp.TransactionCode = po_AsPgo.TransactionCode;
            lo_AsCmp.Reference = po_AsPgo.Reference;
            lo_AsCmp.Reference2 = ps_CdgEAR;
            lo_AsCmp.Reference3 = ps_NmrEAR;
            if (ps_TpPgo == "PE")
                lo_AsCmp.UserFields.Fields.Item("U_BPP_CtaTdoc").Value = "46";
            else
                lo_AsCmp.UserFields.Fields.Item("U_BPP_CtaTdoc").Value = "24";
            lo_AsCmp.UserFields.Fields.Item("U_BPP_DocKeyDest").Value = ps_KeyPgo;
            //CtaAs 
            po_AsPgo.Lines.SetCurrentLine(0);
            ls_CtaPte = po_AsPgo.Lines.AccountCode;
            if (ps_TpPgo == "PE")
            {
                lo_AsCmp.Lines.ShortName = lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value;
                lo_AsCmp.Lines.AccountCode = lo_EmpInf.UserFields.Fields.Item("U_CE_CTAS").Value;
            }
            else
            {
                po_AsPgo.Lines.SetCurrentLine(1);
                lo_AsCmp.Lines.AccountCode = ls_CtaPte;
            }
            if (po_AsPgo.Lines.FCCredit + po_AsPgo.Lines.FCDebit > 0)
            {

                lo_AsCmp.Lines.FCCurrency = po_AsPgo.Lines.FCCurrency;
                for (int i = 0; i < po_AsPgo.Lines.Count; i++)
                {
                    po_AsPgo.Lines.SetCurrentLine(i);
                    lo_AsCmp.Lines.Credit += po_AsPgo.Lines.Credit;
                    lo_AsCmp.Lines.FCCredit += po_AsPgo.Lines.FCCredit;
                }
            }
            else
            {
                for (int i = 0; i < po_AsPgo.Lines.Count; i++)
                {
                    po_AsPgo.Lines.SetCurrentLine(i);
                    lo_AsCmp.Lines.Credit += po_AsPgo.Lines.Credit;
                }
            }
            lo_AsCmp.Lines.Add();

            //CtaPte
            if (ps_TpPgo == "PE")
            {
                po_AsPgo.Lines.SetCurrentLine(1);
                lo_AsCmp.Lines.AccountCode = ls_CtaPte;
            }
            else
            {
                po_AsPgo.Lines.SetCurrentLine(0);
                lo_AsCmp.Lines.ShortName = lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value;
                lo_AsCmp.Lines.AccountCode = lo_EmpInf.UserFields.Fields.Item("U_CE_CTAS").Value;
            }
            if (po_AsPgo.Lines.FCCredit + po_AsPgo.Lines.FCDebit > 0)
            {
                lo_AsCmp.Lines.FCCurrency = po_AsPgo.Lines.FCCurrency;
                for (int i = 0; i < po_AsPgo.Lines.Count; i++)
                {
                    po_AsPgo.Lines.SetCurrentLine(i);
                    lo_AsCmp.Lines.Debit += po_AsPgo.Lines.Debit;
                    lo_AsCmp.Lines.FCDebit += po_AsPgo.Lines.FCDebit;
                }
            }
            else
            {
                for (int i = 0; i < po_AsPgo.Lines.Count; i++)
                {
                    po_AsPgo.Lines.SetCurrentLine(i);
                    lo_AsCmp.Lines.Debit += po_AsPgo.Lines.Debit;
                }
            }
            lo_AsCmp.Lines.Add();

            if (lo_AsCmp.Add() != 0)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(go_SBOCompany.GetLastErrorCode() + " - " + go_SBOCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            else
                return true;
        }

        private static string sb_CleanUpData(string data)
        {
            var r = new System.Text.RegularExpressions.Regex(@"&");
            string output = r.Replace(data, "&amp; ");
            return output;
        }
    }
}
