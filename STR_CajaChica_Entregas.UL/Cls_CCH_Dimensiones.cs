using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using STR_CajaChica_Entregas.UTIL;
using System.Data;

namespace STR_CajaChica_Entregas.UL
{
    class Cls_CCH_Dimensiones : Cls_Global_Controles
    {
        private SAPbouiCOM.Application go_SBOApplication = null;
        private SAPbobsCOM.Company go_SBOCompany = null;
        protected SAPbouiCOM.Form go_Form = null;
        //Ruta del formulario
        public const string gs_NomForm = "FormCCHDIM";
        private string gs_RutaForm = "Resources/CajaChicaEAR/FrmDimDefault.srf";
        //DataSources
        private string gs_DtdCAJASCHICASDIM = "@STR_CAJASCHICASDIM";
        //Columnas Matrix
        private string gs_ClmMtxValDflt = "clmValDfl";
        //* * * * * * * * User Fields - @STR_CAJASCHICASDIMG* * * * * * * * * *
        private string gs_UflDetDimNmb = "U_CC_NMBR";
        private string gs_UflDetDimDsc = "U_CC_DSCR";
        private string gs_UflDetDimDft = "U_CC_DFLT";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //Controles
        //Matrix
        private string gs_MtxDimns = "mtxDimns";

        public Cls_CCH_Dimensiones()
        {
            this.go_SBOApplication = Cls_Global.go_SBOApplication;
            this.go_SBOCompany = Cls_Global.go_SBOCompany;
        }

        public void sb_FormLoad(string ps_Code)
        {
            try
            {
                try
                {
                    if (go_SBOApplication.Forms.GetForm(gs_NomForm, 0) != null)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Cls_Global.WriteToFile(ex.Message);
                }
                go_Form = Cls_Global.fn_CreateForm(gs_NomForm, gs_RutaForm);
                sb_LoadMatrix(ps_Code);
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void sb_LoadMatrix(string ps_Code)
        {
            SAPbobsCOM.DimensionsService lo_DmnsSrv = null;
            SAPbobsCOM.CompanyService lo_CmpSrv = null;
            SAPbobsCOM.Dimension lo_Dim = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            int li_Fila = 0;
            int li_ActvDmns = 0;

            try
            {
                go_Form.Freeze(true);
                lo_CmpSrv = go_SBOCompany.GetCompanyService();
                lo_DmnsSrv = lo_CmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.DimensionsService);
                lo_Cnds = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "Code";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = ps_Code;
                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).Query(lo_Cnds);
                foreach (var lo_Item in lo_DmnsSrv.GetDimensionList())
                {
                    lo_Dim = lo_DmnsSrv.GetDimension((SAPbobsCOM.DimensionParams)lo_Item);
                    if (lo_Dim.IsActive == SAPbobsCOM.BoYesNoEnum.tYES)
                    {
                        li_ActvDmns += 1;
                    }
                }
                go_Matrix = go_Form.Items.Item(gs_MtxDimns).Specific;
                go_Matrix.LoadFromDataSource();
                if (go_Matrix.RowCount != li_ActvDmns)
                {
                    if (Cls_CCH_Crear_Accesos.go_DTblDimns != null)
                    {
                        if (Cls_CCH_Crear_Accesos.go_DTblDimns.Rows.Count > 0)
                        {
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).Clear();
                            for (int i = 0; i < Cls_CCH_Crear_Accesos.go_DTblDimns.Rows.Count; i++)
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).InsertRecord(li_Fila);
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).SetValue(gs_UflDetDimNmb, li_Fila, Cls_CCH_Crear_Accesos.go_DTblDimns.Rows[i][1].ToString());
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).SetValue(gs_UflDetDimDsc, li_Fila, Cls_CCH_Crear_Accesos.go_DTblDimns.Rows[i][2].ToString());
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).SetValue(gs_UflDetDimDft, li_Fila, Cls_CCH_Crear_Accesos.go_DTblDimns.Rows[i][3].ToString());
                                li_Fila += 1;
                            }
                        }
                    }
                    else
                    {
                        go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).Clear();
                        foreach (var lo_Item in lo_DmnsSrv.GetDimensionList())
                        {
                            lo_Dim = lo_DmnsSrv.GetDimension((SAPbobsCOM.DimensionParams)lo_Item);
                            if (lo_Dim.IsActive == SAPbobsCOM.BoYesNoEnum.tYES)
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).InsertRecord(li_Fila);
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).SetValue(gs_UflDetDimNmb, li_Fila, lo_Dim.DimensionName);
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).SetValue(gs_UflDetDimDsc, li_Fila, lo_Dim.DimensionDescription);
                                li_Fila += 1;
                            }
                        }
                    }
                    go_Matrix.LoadFromDataSource();
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                go_Form.Freeze(false);
            }
        }

        public bool fn_HandleItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            switch (po_ItmEvnt.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                    lb_Result = this.fn_HandleChooseFromList(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    lb_Result = this.fn_HandleItemPressed(po_ItmEvnt);
                    break;
            }
            return lb_Result;
        }

        public bool fn_HandleItemPressed(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            if (po_ItmEvnt.ItemUID != string.Empty)
            {
                if (this.go_Form == null)
                {
                    go_Form = go_SBOApplication.Forms.GetForm(po_ItmEvnt.FormUID, po_ItmEvnt.FormTypeCount);
                }
                switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                        if (po_ItmEvnt.BeforeAction && po_ItmEvnt.ItemUID == "1")
                        {
                            if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                            {
                                go_Matrix = go_Form.Items.Item(gs_MtxDimns).Specific;
                                go_Matrix.FlushToDataSource();
                                Cls_CCH_Crear_Accesos.go_DTblDimns = this.sb_AddDataToDataTable();
                            }
                        }
                        break;
                }
            }
            return lb_Result;
        }

        public bool fn_HandleChooseFromList(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            SAPbouiCOM.ChooseFromListEvent lo_CFLEvnt = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.DataTable lo_DataTable = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            string ls_DimNmb = string.Empty;

            if (this.go_Form == null)
            {
                go_Form = go_SBOApplication.Forms.GetForm(po_ItmEvnt.FormUID, po_ItmEvnt.FormTypeCount);
            }
            lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
            ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxDimns).Specific).FlushToDataSource();
            ls_DimNmb = go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).GetValue(gs_UflDetDimNmb, lo_CFLEvnt.Row - 1).Trim();
            ls_DimNmb = ls_DimNmb.Substring(ls_DimNmb.Length - 1, 1);
            if (lo_CFLEvnt.BeforeAction)
            {
                lo_CFL = go_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                lo_CFL.SetConditions(null);
                lo_Cnds = lo_CFL.GetConditions();
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DimCode";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = ls_DimNmb;
                lo_CFL.SetConditions(lo_Cnds);
            }
            else
            {
                lo_DataTable = lo_CFLEvnt.SelectedObjects;
                if (lo_DataTable != null)
                {
                    go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).SetValue(gs_UflDetDimDft, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(0, 0));
                }
            }
            ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxDimns).Specific).LoadFromDataSource();
            if (go_Form.Mode != SAPbouiCOM.BoFormMode.fm_UPDATE_MODE) go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
            return true;
        }

        private DataTable sb_AddDataToDataTable()
        {
            DataRow lo_DtRw = null;
            DataTable lo_DTBLDIMS = null;

            lo_DTBLDIMS = new DataTable();
            lo_DTBLDIMS.Columns.Add(new DataColumn("DimNmb", typeof(string)));
            lo_DTBLDIMS.Columns.Add(new DataColumn("DimDsc", typeof(string)));
            lo_DTBLDIMS.Columns.Add(new DataColumn("DimVal", typeof(string)));
            for (int i = 0; i < go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).Size; i++)
            {
                lo_DtRw = lo_DTBLDIMS.NewRow();
                lo_DtRw[0] = go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).GetValue(gs_UflDetDimNmb, i);
                lo_DtRw[1] = go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).GetValue(gs_UflDetDimDsc, i);
                lo_DtRw[2] = go_Form.DataSources.DBDataSources.Item(gs_DtdCAJASCHICASDIM).GetValue(gs_UflDetDimDft, i);
                lo_DTBLDIMS.Rows.Add(lo_DtRw);
            }
            return lo_DTBLDIMS;
        }
    }
}
