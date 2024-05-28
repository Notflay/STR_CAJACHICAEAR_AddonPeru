using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;

namespace STR_CajaChica_Entregas.DL
{
    public static class Cls_QueriesManager_CCH
    {
        public static SAPbobsCOM.BoDataServerTypes go_ServerType;
        private static SAPbobsCOM.Recordset go_RecSet = null;
        private static string gs_Qry = string.Empty;
        private static string[] go_ArrCad = null;

        public static SAPbobsCOM.Recordset fn_MonedasSociedad()
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    gs_Qry = Resources.Queries_SQL_HANA_CCH.ObtMonedas.Split('|').GetValue(1).ToString();
                }
                else
                {
                    gs_Qry = Resources.Queries_SQL_HANA_CCH.ObtMonedas.Split('|').GetValue(0).ToString();
                }
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static SAPbobsCOM.Recordset fn_ListaFlujodeCaja()
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    gs_Qry = Resources.Queries_SQL_HANA_CCH.ObtFlujodeCaja.Split('|').GetValue(1).ToString();
                }
                else
                {
                    gs_Qry = Resources.Queries_SQL_HANA_CCH.ObtFlujodeCaja.Split('|').GetValue(0).ToString();
                }
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static string ListadeBancos
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtListadeBancos.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtListadeBancos.Split('|').GetValue(0).ToString();
                }

                return ls_Value;
            }
        }

        public static string CuentasdeBanco
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtCuentaBanco.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtCuentaBanco.Split('|').GetValue(0).ToString();
                }

                return ls_Value;
            }
        }

        public static string DatosCuentaCCH
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtDatosCuentaCCH.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtDatosCuentaCCH.Split('|').GetValue(0).ToString();
                }

                return ls_Value;
            }
        
        }

        public static string fn_GenerarCodigoCCH(string ps_CodCCHEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.GenerarCodigoCCH.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.GenerarCodigoCCH.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0] + ps_CodCCHEAR + go_ArrCad[1] + "CCH" + go_ArrCad[2];
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static string CuentadeBancoPropio
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtCuentadeBancoPropio.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtCuentadeBancoPropio.Split('|').GetValue(0).ToString();
                }

                return ls_Value;
            }
        }

        public static string NumerosdeCCHActivos
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtNumerosdeCajaChicaActivos.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtNumerosdeCajaChicaActivos.Split('|').GetValue(0).ToString();
                }

                return ls_Value;
            }          
        }

        public static string SaldoCajaChica
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtSaldoCajaChica.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtSaldoCajaChica.Split('|').GetValue(0).ToString();
                }

                return ls_Value;
            }
        }

        public static string TiposDeDocumentos
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtTiposdeDocumentos.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtTiposdeDocumentos.Split('|').GetValue(0).ToString();
                }
                return ls_Value;
            }
        }

        public static string CuentadeCajaChica
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtCuentadeCajaChica.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtCuentadeCajaChica.Split('|').GetValue(0).ToString();
                }
                return ls_Value;
            }
        }

        public static void sb_ActualizarEstadoySaldoXNroCCH(string ps_Estado,double ps_Saldo,string ps_NmbCCH,string ps_NroCCH)
        { 
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ActEstadoNumerosCCH.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ActEstadoNumerosCCH.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_Estado + go_ArrCad[1].Trim() + ps_Saldo + go_ArrCad[2].Trim() + ps_NmbCCH
                         + go_ArrCad[3].Trim() + ps_NroCCH + go_ArrCad[4].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                go_RecSet = null;
            }        
        }

        public static void sb_ActualizarEstadoPorReaperturaCCH(string ps_NmbCCH, string ps_NroCCH)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ActEstadoReapertura.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ActEstadoReapertura.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_NmbCCH + go_ArrCad[1].Trim() + ps_NroCCH + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static SAPbobsCOM.Recordset fn_InfoTotalesPorCarga(string ps_DcEnt)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ObtTotalesXCarga.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ObtTotalesXCarga.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_DcEnt + go_ArrCad[1].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static SAPbobsCOM.Recordset fn_MontodeAperturaNmroCCH(string ps_NmbCCH,string ps_NmrCCH)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ObtMontoApertura.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ObtMontoApertura.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_NmbCCH + go_ArrCad[1].Trim() + ps_NmrCCH + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static string PagosPorNrosCCH
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtPagosPorNumerodeCajaChica.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ObtPagosPorNumerodeCajaChica.Split('|').GetValue(0).ToString();
                }
                return ls_Value;
            }
        }

        public static string GenerarDocumentoXML(string ps_DcEnFrm, string ps_Filas, string ps_NmbTbl)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.GenerarDocumentoXML.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.GenerarDocumentoXML.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_DcEnFrm + go_ArrCad[1].Trim() + ps_Filas + go_ArrCad[2].Trim() + ps_NmbTbl + go_ArrCad[3].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (!go_RecSet.EoF)
                {
                    return go_RecSet.Fields.Item(0).Value;
                }
                else
                {
                    Cls_Global.go_SBOApplication.StatusBar.SetText("SP: STR_SP_Create_OPCH_XML sin resultados...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static string ActualizarEstadodeCreacion
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ActualizarEstadoCreacion.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ActualizarEstadoCreacion.Split('|').GetValue(0).ToString();
                }
                return ls_Value;
            }
        }

        public static string VerificarDocumentoExistente
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.VerificarExisteDocumento.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.VerificarExisteDocumento.Split('|').GetValue(0).ToString();
                }
                return ls_Value;
            }
        }

        public static string ValidarPermisosAperturaCCH
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ValidarAperturaCCH.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ValidarAperturaCCH.Split('|').GetValue(0).ToString();
                }
                return ls_Value;
            }
        }

        public static string ValidarPermisosCargaCCH
        {
            get
            {
                string ls_Value = string.Empty;
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ValidarCargaCCH.Split('|').GetValue(1).ToString();
                }
                else
                {
                    ls_Value = Resources.Queries_SQL_HANA_CCH.ValidarCargaCCH.Split('|').GetValue(0).ToString();
                }
                return ls_Value;
            }
        }

        public static SAPbobsCOM.Recordset fn_VerificarCantidadNrosCCH(string ps_NroCCH)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad =  Resources.Queries_SQL_HANA_CCH.VerificarCantidadNrosCCH.Split('|').GetValue(1).ToString().Split(new char[]{ '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.VerificarCantidadNrosCCH.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_NroCCH + go_ArrCad[1].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        
        }

        public static SAPbobsCOM.Recordset fn_VerificarEstadoYSaldoXNroCCH(string ps_NmbCCH,string ps_NroCCH)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.VerificarEstadoySaldoNroCCH.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.VerificarEstadoySaldoNroCCH.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_NmbCCH + go_ArrCad[1].Trim() + ps_NroCCH + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }

        }

        public static string fn_ValidarPermisosCerrarCargaCCH(string ps_Usuario,string NmbCCH)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ValidarCerrarCargaCCH.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ValidarCerrarCargaCCH.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_Usuario + go_ArrCad[1].Trim() + NmbCCH + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static string fn_ValidarPermisosContabilizarCCH(string ps_Usuario, string NmbCCH)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ValidarContabCCH.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.ValidarContabCCH.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_Usuario + go_ArrCad[1].Trim() + NmbCCH + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static string fn_ObtenerLineasDocumento(string ps_DocEnt, string ps_CodPrv, string ps_TpoDoc, string ps_SreDoc, string ps_CorDoc)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.AgruparLineasCCH.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_CCH.AgruparLineasCCH.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_DocEnt + go_ArrCad[1].Trim() + ps_CodPrv + go_ArrCad[2].Trim() + ps_TpoDoc + go_ArrCad[3].Trim() + ps_SreDoc + go_ArrCad[4].Trim() + ps_CorDoc + go_ArrCad[5].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
            finally
            {
                go_RecSet = null;
            }

        }

    }
}
