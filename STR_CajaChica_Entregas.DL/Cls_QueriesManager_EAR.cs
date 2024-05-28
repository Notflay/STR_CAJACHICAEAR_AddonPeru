using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;

namespace STR_CajaChica_Entregas.DL
{
    public static class Cls_QueriesManager_EAR
    {
        public static SAPbobsCOM.BoDataServerTypes go_ServerType;
        private static SAPbobsCOM.Recordset go_RecSet = null;
        private static string gs_Qry = string.Empty;
        private static string[] go_ArrCad = null;

        public static string fn_GenerarCodigoEAR(string ps_CdgCCHEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.GenerarCodigoEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.GenerarCodigoEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0] + ps_CdgCCHEAR + go_ArrCad[1] + "EAR" + go_ArrCad[2];
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

        public static string fn_CuentaDeBancoPropio(string ps_CdgBnc,string ps_CdgCta)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtCuentadeBancoPropio.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtCuentadeBancoPropio.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0] + ps_CdgBnc + go_ArrCad[1] + ps_CdgCta + go_ArrCad[2];
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return string.Empty;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static SAPbobsCOM.Recordset fn_ListadeBancos()
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    gs_Qry = Resources.Queries_SQL_HANA_EAR.ObtListadeBancos.Split('|').GetValue(1).ToString();
                }
                else
                {
                    gs_Qry = Resources.Queries_SQL_HANA_EAR.ObtListadeBancos.Split('|').GetValue(0).ToString();
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

        public static SAPbobsCOM.Recordset fn_CuentasdeBanco(string ps_Bnk,string ps_Mnd)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtCuentasdeBanco.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtCuentasdeBanco.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_Bnk + go_ArrCad[1].Trim() + ps_Mnd + go_ArrCad[2].Trim();
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

        public static bool fn_ValidarPermisosCargaEAR(string ps_EmpID,string ps_User)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarCargaEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarCargaEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0] + ps_EmpID + go_ArrCad[1] + ps_User + go_ArrCad[2];
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (go_RecSet.EoF != true && go_RecSet.Fields.Item(0).Value == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
        }

        public static bool fn_ValidarCerrarCargaEAR(string ps_EmpID, string ps_User)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarCerrarCargaEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarCerrarCargaEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0] + ps_EmpID + go_ArrCad[1] + ps_User + go_ArrCad[2];
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (go_RecSet.EoF != true && go_RecSet.Fields.Item(0).Value == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
        }

        public static bool fn_ValidarRegularizarEAR(string ps_EmpID, string ps_User)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarRegularizarSaldosEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarRegularizarSaldosEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0] + ps_EmpID + go_ArrCad[1] + ps_User + go_ArrCad[2];
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (go_RecSet.EoF != true && go_RecSet.Fields.Item(0).Value == "Y")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
        }

        public static SAPbobsCOM.Recordset fn_NumerosEARActivos(string ps_EARCod)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtNumerosdeEntregaaRendirActivos.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtNumerosdeEntregaaRendirActivos.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0] + ps_EARCod + go_ArrCad[1];
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
        }

        public static int fn_VerificarCantidadNrosEAR(string ps_NroEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.VerificarCantidadNrosEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.VerificarCantidadNrosEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_NroEAR + go_ArrCad[1].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return Convert.ToInt32(go_RecSet.Fields.Item(0).Value);
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return -1;
            }
            finally
            {
                go_RecSet = null;
            }

        }

        public static SAPbobsCOM.Recordset fn_SaldoEntregaaRendir(string ps_NroEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtSaldoEntregasaRendir.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtSaldoEntregasaRendir.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_NroEAR + go_ArrCad[1].Trim();
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

        public static bool fn_ValidarPermisosContabilizarEAR(string ps_Usuario, string NmbEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarContabEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ValidarContabEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_Usuario + go_ArrCad[1].Trim() + NmbEAR + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (go_RecSet.EoF != true && go_RecSet.Fields.Item(0).Value == "Y")
                    return true;
                else 
                    return false;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return true;
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
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.AgruparLineasEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.AgruparLineasEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_DocEnt + go_ArrCad[1].Trim() + ps_CodPrv + go_ArrCad[2].Trim() + ps_TpoDoc + go_ArrCad[3].Trim() + ps_SreDoc + go_ArrCad[4].Trim() + ps_CorDoc + go_ArrCad[5].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (!go_RecSet.EoF)
                {
                    return go_RecSet.Fields.Item(0).Value;
                }
                else
                {
                    Cls_Global.go_SBOApplication.StatusBar.SetText("SP: STR_SP_INT_AgruparLineasEAR sin resultados...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
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

        public static string GenerarDocumentoXML(string ps_DcEnFrm,string ps_Filas,string ps_NmbTbl)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.GenerarDocumentoXML.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.GenerarDocumentoXML.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
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

        public static void ActualizarEstadodeCreacion(string ps_DcDoc, string ps_Estado, string ps_DcEntFrm,string ps_Filas)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ActualizarEstadoCreacion.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ActualizarEstadoCreacion.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_DcDoc + go_ArrCad[1].Trim() + ps_Estado + go_ArrCad[2].Trim() + ps_DcEntFrm + go_ArrCad[3].Trim() + ps_Filas + go_ArrCad[4].Trim();
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

        public static string PagosRealizadosporNroEAR(string CodEAR,string NmrEAR,string TpoRnd)
        {
            try
            {
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtPagosPorNumerodeEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtPagosPorNumerodeEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                return go_ArrCad[0].Trim() + CodEAR + go_ArrCad[1].Trim() + NmrEAR + go_ArrCad[2].Trim() + TpoRnd + go_ArrCad[3].Trim(); 
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return string.Empty;
            }
        }

        public static int fn_ObtenerTransIdPagoaCuenta(string CodEAR,string NmrEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtTransIdPagoaCuenta.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtTransIdPagoaCuenta.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + CodEAR + go_ArrCad[1].Trim() + NmrEAR + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (!go_RecSet.EoF)
                {
                    return go_RecSet.Fields.Item(0).Value;
                }
                else
                {
                    Cls_Global.go_SBOApplication.StatusBar.SetText("qry: ObtTransIdPagoaCuenta sin resultados...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return 0;
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
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtTotalesXCarga.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtTotalesXCarga.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
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

        public static SAPbobsCOM.Recordset fn_VerificarEstadoYSaldoXNroEAR(string ps_NmbCCH, string ps_NroCCH)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.VerificarEstadoySaldoNroEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.VerificarEstadoySaldoNroEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
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

        public static SAPbobsCOM.Recordset fn_MontodeAperturaNmroEAR(string ps_CdgEAR, string ps_NmrEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtMontoApertura.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtMontoApertura.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_CdgEAR + go_ArrCad[1].Trim() + ps_NmrEAR + go_ArrCad[2].Trim();
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

        public static void sb_ActualizarEstadoySaldoXNroEAR(string ps_Estado, double ps_Saldo, string ps_CdgEAR, string ps_NroEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ActEstadoNumerosEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ActEstadoNumerosEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_Estado + go_ArrCad[1].Trim() + ps_Saldo + go_ArrCad[2].Trim() + ps_CdgEAR
                         + go_ArrCad[3].Trim() + ps_NroEAR + go_ArrCad[4].Trim();
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

        public static SAPbobsCOM.Recordset fn_ListarSolicitudesEAR(string ps_Mnda,string ps_Usr)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtListaSolicitudesEAR.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtListaSolicitudesEAR.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_Mnda + go_ArrCad[1].Trim() + ps_Usr + go_ArrCad[2].Trim();
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

        public static string fn_GenerarCodigoXTU(string ps_NmbTbl)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.GenerarCodigoUnicoPorTU.Split('$').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.GenerarCodigoUnicoPorTU.Split('$').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_NmbTbl + go_ArrCad[1].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return go_RecSet.Fields.Item(0).Value;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return string.Empty;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static void sb_EliminarDatosXCodigo(string ps_Tbl,string ps_Fld,string ps_Cdg)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.EliminarRegistrosTU.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                    gs_Qry = go_ArrCad[0].Trim() + ps_Tbl + go_ArrCad[1].Trim() + ps_Fld + go_ArrCad[2].Trim() + ps_Cdg + go_ArrCad[3].Trim();
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.EliminarRegistrosTU.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                    gs_Qry = go_ArrCad[0].Trim() + ps_Tbl + go_ArrCad[1].Trim() + " " + ps_Fld + " " + go_ArrCad[2].Trim() + ps_Cdg + go_ArrCad[3].Trim();
                }
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

        public static string sb_DimencionesXIdEmpleado(string ps_IdEmp,string ps_DimNmb)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtDimensionesXDefecto.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtDimensionesXDefecto.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_IdEmp + go_ArrCad[1].Trim() + ps_DimNmb + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                return Convert.ToString(go_RecSet.Fields.Item(0).Value).Trim();
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return string.Empty;
            }
            finally
            {
                go_RecSet = null;
            }
        }

        public static int fn_ObtenerAsientoCompensacion(int ps_TpoPgo,string ps_KeyPgo)
        {
            int li_Result = 0;
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtAsientoCompensacion.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtAsientoCompensacion.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_TpoPgo + go_ArrCad[1].Trim() + ps_KeyPgo + go_ArrCad[2].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if(!go_RecSet.EoF)
                    li_Result = Convert.ToInt32(go_RecSet.Fields.Item(0).Value);
                return li_Result;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return 0;
            }
            finally
            {
                go_RecSet = null;
            }
        }


        public static string fn_ObtenerCodigoCtaPuenteEAR(string ps_FrmtCd)
        {
            string ls_Result = string.Empty;
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtCodigoCtaPte.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtCodigoCtaPte.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_FrmtCd + go_ArrCad[1].Trim();
                Cls_Global.WriteToFile(gs_Qry);
                go_RecSet.DoQuery(gs_Qry);
                if (!go_RecSet.EoF)
                    ls_Result = go_RecSet.Fields.Item(0).Value;
                return ls_Result;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return string.Empty;
            }
            finally
            {
                go_RecSet = null;
            }
        }


        public static SAPbobsCOM.Recordset fn_ObtDocsaReconciliar(string ps_CdgEAR,string ps_NmrEAR)
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtDocumentosaReconciliar.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.ObtDocumentosaReconciliar.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim() + ps_CdgEAR + go_ArrCad[1].Trim() +ps_NmrEAR+ go_ArrCad[2].Trim();
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

        public static SAPbobsCOM.Recordset fn_VerificarPrvAso()
        {
            try
            {
                go_RecSet = Cls_Global.go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.VerificarProveedorAsociado.Split('|').GetValue(1).ToString().Split(new char[] { '?' });
                }
                else
                {
                    go_ArrCad = Resources.Queries_SQL_HANA_EAR.VerificarProveedorAsociado.Split('|').GetValue(0).ToString().Split(new char[] { '?' });
                }
                gs_Qry = go_ArrCad[0].Trim();
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
    }
}
