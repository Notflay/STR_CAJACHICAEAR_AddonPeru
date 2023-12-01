using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;

namespace STR_CajaChica_Entregas.BL
{
    public static class Cls_EAR_Crear_Accesos_BL
    {
        private static SAPbobsCOM.Company go_SBOCompany = Cls_Global.go_SBOCompany;
        private const string gs_DtcEARCRG = "@STR_EARCRG";
        private const string gs_DTSDETHEMEAR = "@STR_HEMEAR";
        private const string gs_DTSDETHEMEAR2 = "@STR_HEMEAR2";

        private static readonly string gs_UflEmpId = "U_empID";
        private static readonly string gs_UflLneId = "U_LineID";
        private static readonly string gs_UflCdUsr = "U_ER_CDUS";
        private static readonly string gs_UflSAprt = "U_ER_SAPR";
        private static readonly string gs_UflSCntb = "U_ER_SCNT";
        private static readonly string gs_UflSRglr = "U_ER_SRGL";
        private static readonly string gs_UflSCrgr = "U_ER_SCRG";
        private static readonly string gs_UflSCCrg = "U_ER_SCCR";

        private static readonly string gs_UflNmbr = "U_ER_NMBR";
        private static readonly string gs_UflDscr = "U_ER_DSCR";
        private static readonly string gs_UflDflt = "U_ER_DFLT";

        public static bool fn_AddDataFromDataSourceToAccesTable(SAPbouiCOM.DBDataSources po_DBDts)
        {
            bool lb_Result = true;
            SAPbobsCOM.UserTable lo_UsrTbl = null;
            SAPbouiCOM.DBDataSource lo_DBDts = null;
            string ls_Codigo = string.Empty;

            try
            {
                lo_UsrTbl = go_SBOCompany.UserTables.Item("STR_HEMEAR");
                lo_DBDts = po_DBDts.Item(gs_DTSDETHEMEAR);

                Cls_QueriesManager_EAR.sb_EliminarDatosXCodigo(gs_DTSDETHEMEAR, gs_UflEmpId, po_DBDts.Item(0).GetValue("empID", 0).Trim());
                for (int i = 0; i < lo_DBDts.Size; i++)
                {
                    ls_Codigo = Cls_QueriesManager_EAR.fn_GenerarCodigoXTU(gs_DTSDETHEMEAR);
                    lo_UsrTbl.Code = ls_Codigo;
                    lo_UsrTbl.Name = ls_Codigo;
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflEmpId).Value = po_DBDts.Item(0).GetValue("empID",0).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflLneId).Value = lo_DBDts.GetValue(gs_UflLneId, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflCdUsr).Value = lo_DBDts.GetValue(gs_UflCdUsr, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflSAprt).Value = lo_DBDts.GetValue(gs_UflSAprt, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflSCntb).Value = lo_DBDts.GetValue(gs_UflSCntb, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflSRglr).Value = lo_DBDts.GetValue(gs_UflSRglr, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflSCrgr).Value = lo_DBDts.GetValue(gs_UflSCrgr, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflSCCrg).Value = lo_DBDts.GetValue(gs_UflSCCrg, i).Trim();
                    if (lo_UsrTbl.Add() != 0)
                    {
                        Cls_Global.go_SBOApplication.StatusBar.SetText(go_SBOCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        lb_Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            return lb_Result;
        }

        public static bool fn_AddDataFromDataSourceToDimnsTable(SAPbouiCOM.DBDataSources po_DBDts,string ps_CdgEmp)
        {
            bool lb_Result = true;
            SAPbobsCOM.UserTable lo_UsrTbl = null;
            SAPbouiCOM.DBDataSource lo_DBDts = null;
            string ls_Codigo = string.Empty;

            try
            {
                lo_UsrTbl = go_SBOCompany.UserTables.Item("STR_HEMEAR2");
                lo_DBDts = po_DBDts.Item(gs_DTSDETHEMEAR2);

                Cls_QueriesManager_EAR.sb_EliminarDatosXCodigo(gs_DTSDETHEMEAR2, gs_UflEmpId, ps_CdgEmp);
                for (int i = 0; i < lo_DBDts.Size; i++)
                {
                    ls_Codigo = Cls_QueriesManager_EAR.fn_GenerarCodigoXTU(gs_DTSDETHEMEAR2);
                    lo_UsrTbl.Code = ls_Codigo;
                    lo_UsrTbl.Name = ls_Codigo;
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflEmpId).Value = ps_CdgEmp;
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflLneId).Value = lo_DBDts.GetValue(gs_UflLneId, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflNmbr).Value = lo_DBDts.GetValue(gs_UflNmbr, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflDscr).Value = lo_DBDts.GetValue(gs_UflDscr, i).Trim();
                    lo_UsrTbl.UserFields.Fields.Item(gs_UflDflt).Value = lo_DBDts.GetValue(gs_UflDflt, i).Trim();
                    if (lo_UsrTbl.Add() != 0)
                    {
                        Cls_Global.go_SBOApplication.StatusBar.SetText(go_SBOCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        lb_Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            return lb_Result;
        }
    }
}
