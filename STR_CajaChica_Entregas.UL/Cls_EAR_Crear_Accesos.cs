using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using STR_CajaChica_Entregas.UTIL;
using System.Data;

namespace STR_CajaChica_Entregas.UL
{
    public class Cls_EAR_Crear_Accesos : Cls_Global_Controles
    {
        private SAPbouiCOM.Form go_Form = null;
        private SAPbouiCOM.Application go_SBOApplication = null;
        private SAPbobsCOM.Company go_SBOCompany = null;
        public const string gs_NomForm = "FrmAccEAR";
        private string gs_RutaForm = "Resources/CajaChicaEAR/FrmAccesosEAR.srf";

        //* * * * * * * * * * * * * * * Menus * * * * * * * * * * * * * * * * *
        public const string gs_MenuEAR = "MNU_EAR_ENTREGAS";
        private string gs_MnuAñadirFila = "1292";
        private string gs_MnuBorrarFila = "1293";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

        //* * * * * * * * * * * * * * DataSources * * * * * * * * * * * * * * *
        private const string gs_DtcEAR = "@STR_EAR";
        private const string gs_DtdEARDET = "@STR_EARDET";
        private const string gs_DtdEARDET2 = "@STR_EARDET2";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * User Fields - @STR_EAR* * * * * * * * * * *        
        private readonly string gs_UflNmbSng = "Name";
        private readonly string gs_UflCdgSng = "U_ER_CDSN";
        private readonly string gs_UflTpoMnd = "U_ER_TPMN";
        private readonly string gs_UflCtaAsc = "U_ER_CTAS";
        private readonly string gs_UflEstado = "U_ER_ESTD";
        private readonly string gs_UflCntRnd = "U_ER_RNDC";
        private readonly string gs_UflPrycDf = "U_ER_PRYD";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * User Fields - @STR_EARDET* * * * * * * * * * *
        private readonly string gs_UflDetCdgUsr = "U_ER_CDUS";
        private readonly string gs_UflDetSlcApr = "U_ER_SAPR";
        private readonly string gs_UflDetSlcGst = "U_ER_SGST";
        private readonly string gs_UflDetSlcDvl = "U_ER_SDVL";
        private readonly string gs_UflDetSlcRgl = "U_ER_SRGL";
        private readonly string gs_UflDetSlcCrg = "U_ER_SCRG";
        private readonly string gs_UflDetSlcCrr = "U_ER_SCCR";
        private readonly string gs_UflDetSlcCnt = "U_ER_SCNT";
        private readonly string gs_UflDetSlcRpt = "U_ER_SRPT";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * User Fields - @STR_EARDET2* * * * * * * * * *
        private string gs_UflDetDimNmb = "U_ER_NMBR";
        private string gs_UflDetDimDsc = "U_ER_DSCR";
        private string gs_UflDetDimDft = "U_ER_DFLT";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //Controles UI
        //EditText
        private string gs_EdtCdgEAR = "txtCdgEAR";
        private string gs_EdtCodScNg = "txtCodSn";
        private string gs_EdtCantRndc = "txtCntRnd";
        private string gs_EdtPryDfc = "txtPryDfc";
        private string gs_EdtFocus = "txtFocus";
        //ChooseFromList
        private string gs_CFLSociosNegocio = "CFLOCRD";
        private string gs_CFLUsuarios = "CFLOUSR";
        private string gs_CFLPrycts = "CFLCODPRY";
        //Matrix
        private string gs_MtxAccesos = "mxAccesos";
        //Columnas Matrix
        private string gs_ClmMtxUsers = "clmUSER";
        //Buttons
        private string gs_BtnCrear = "1";
        private string gs_BtnDimns = "btnDimns";
        //ComboBox
        private string gs_CmbTpoRndc = "cmbTpoRnd";
        private string gs_CmbEstados = "cmbEstados";
        //CheckBox
        private const string gs_ChkDimns = "chkDimns";
        private const string gs_ChkCntRnd = "chkCntRnd";
        private const string gs_ChkProyec = "chkPrycts";

        //Variables Globales
        private int gi_RightClickRow = -1;

        public static DataTable go_DTblDimns = null;

        public Cls_EAR_Crear_Accesos()
        {
            go_SBOApplication = Cls_Global.go_SBOApplication;
            go_SBOCompany = Cls_Global.go_SBOCompany;
        }

        public void sb_FormLoad()
        {
            try
            {
                go_Form = Cls_Global.fn_CreateForm(gs_NomForm, gs_RutaForm);
                sb_DataFormLoad();
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                go_Form.Visible = true;
            }
        }

        public void sb_DataFormLoad()
        {
            try
            {
                go_DTblDimns = null;
                go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).SetValue(gs_UflCntRnd, 0, "1");
                go_Form.DataBrowser.BrowseBy = gs_EdtCodScNg;
                go_Form.Items.Item(gs_EdtCdgEAR).Enabled = true;
                go_Form.Items.Item(gs_EdtCodScNg).Enabled = true;
                go_Form.Items.Item(gs_CmbEstados).Enabled = true;
                ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbTpoRndc).Specific).Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbEstados).Specific).Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxAccesos).Specific).AddRow();
                this.sb_SetConditionsChooseFromList();
                foreach (SAPbouiCOM.Item lo_Item in go_Form.Items)
                {
                    if (lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX) this.sb_EnableDisableItemsByCheck(lo_Item.UniqueID);
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
        }

        private void sb_SetConditionsChooseFromList()
        {
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;

            lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLSociosNegocio);
            lo_CFL.SetConditions(null);
            lo_Cnds = lo_CFL.GetConditions();
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "U_CE_USEAR";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = "Y";
            lo_CFL.SetConditions(lo_Cnds);
        }

        public bool fn_HandleItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            switch (po_ItmEvnt.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD:
                    this.sb_FormUnload();
                    break;
                case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                    this.fn_HandleChooseFromList(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    lb_Result = this.fn_HandleItemPressed(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_VALIDATE:
                    lb_Result = fn_HandleValidate(po_ItmEvnt);
                    break;
            }
            return lb_Result;
        }

        private bool fn_HandleItemPressed(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            if (po_ItmEvnt.ItemUID != string.Empty)
            {
                switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                        if (po_ItmEvnt.ItemUID == "1")
                        {
                            if (po_ItmEvnt.BeforeAction && go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                            {
                                lb_Result = fn_ValidacionesGenerales();
                            }
                            if (po_ItmEvnt.BeforeAction && (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || go_Form.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE))
                            {
                                this.sb_AddDataToDataSource();
                            }
                        }
                        if (po_ItmEvnt.ItemUID == gs_BtnDimns)
                        {
                            if (!po_ItmEvnt.BeforeAction)
                            {
                                new Cls_EAR_Dimensiones().sb_FormLoad(go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).GetValue("Code", 0).Trim());
                                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                {
                                    go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                }
                            }
                        }
                        break;
                    case SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX:
                        if (!po_ItmEvnt.BeforeAction)
                        {
                            this.sb_EnableDisableItemsByCheck(po_ItmEvnt.ItemUID);
                        }
                        break;
                }
            }
            return lb_Result;
        }

        private bool fn_ValidacionesGenerales()
        {
            bool lb_Result = true;
            string ls_MsgErr = string.Empty;
            go_Edit = go_Form.Items.Item(gs_EdtCdgEAR).Specific;
            if (go_Edit.Value == string.Empty)
            {
                lb_Result = false;
                ls_MsgErr = "Ingrese un codigo para la Entrega a Rendir...";
                go_Edit.Active = true;
                goto fin;
            }
            go_Edit = go_Form.Items.Item(gs_EdtCodScNg).Specific;
            if (go_Edit.Value == string.Empty)
            {
                lb_Result = false;
                ls_MsgErr = "Seleccione un socio de negocio para la entrega a rendir...";
                go_Edit.Active = true;
                goto fin;
            }
            go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
            for (int i = 0; i < go_Matrix.RowCount; i++)
            {
                go_Edit = go_Matrix.Columns.Item("clmUSER").Cells.Item(i + 1).Specific;
                if (go_Edit.Value == string.Empty)
                {
                    lb_Result = false;
                    ls_MsgErr = "Fila sin asignación de usuario...";
                    go_Edit.Active = true;
                    break;
                }
            }
        fin:
            if (!lb_Result)
            {
                go_SBOApplication.SetStatusBarMessage(ls_MsgErr, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            return lb_Result;
        }

        private void fn_HandleChooseFromList(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            SAPbouiCOM.ChooseFromListEvent lo_CFLEvnt = null;
            SAPbouiCOM.DataTable lo_DataTable = null;
            SAPbobsCOM.BusinessPartners lo_BsnssPrtnrs = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtOfAcct = null;


            try
            {
                lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
                lo_BsnssPrtnrs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                lo_ChrtOfAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
                if (lo_CFLEvnt.ChooseFromListUID == gs_CFLSociosNegocio)
                {
                    if (!lo_CFLEvnt.BeforeAction)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).SetValue(gs_UflCdgSng, 0, lo_DataTable.GetValue(0, 0));
                            lo_BsnssPrtnrs.GetByKey(lo_DataTable.GetValue(0, 0));
                            go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).SetValue(gs_UflNmbSng, 0, lo_BsnssPrtnrs.CardName);
                            go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).SetValue(gs_UflTpoMnd, 0, lo_BsnssPrtnrs.Currency);
                            go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).SetValue(gs_UflCtaAsc, 0, lo_BsnssPrtnrs.AccountRecivablePayables.AccountCode);
                            lo_ChrtOfAcct.GetByKey(lo_BsnssPrtnrs.AccountRecivablePayables.AccountCode);
                            go_Static = go_Form.Items.Item("lblAcct").Specific;
                            go_Static.Caption = lo_ChrtOfAcct.Name;
                            go_Edit = go_Form.Items.Item("txtFmtCode").Specific;
                            go_Edit.Value = lo_ChrtOfAcct.FormatCode;
                        }
                    }
                }
                if (lo_CFLEvnt.ChooseFromListUID == gs_CFLUsuarios)
                {
                    if (!lo_CFLEvnt.BeforeAction)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
                            go_Matrix.FlushToDataSource();
                            go_Form.DataSources.DBDataSources.Item(gs_DtdEARDET).SetValue(gs_UflDetCdgUsr, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(5, 0));
                            go_Matrix.LoadFromDataSource();
                        }
                    }
                }
                if (lo_CFLEvnt.ChooseFromListUID == gs_CFLPrycts)
                {
                    if (!lo_CFLEvnt.BeforeAction)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).SetValue(gs_UflPrycDf, 0, lo_DataTable.GetValue(0, 0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                lo_BsnssPrtnrs = null;
                lo_ChrtOfAcct = null;
            }
        }

        public bool fn_HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo po_RghClkEvent)
        {
            bool lb_Result = true;
            try
            {
                if (po_RghClkEvent.ItemUID != string.Empty)
                {
                    go_Form.EnableMenu(gs_MnuAñadirFila, false);
                    go_Form.EnableMenu(gs_MnuBorrarFila, false);
                    switch (go_Form.Items.Item(po_RghClkEvent.ItemUID).Type)
                    {
                        case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                            if (po_RghClkEvent.BeforeAction)
                            {
                                go_Form.EnableMenu(gs_MnuAñadirFila, true);
                                if (po_RghClkEvent.Row > 1)
                                {
                                    go_Form.EnableMenu(gs_MnuBorrarFila, true);
                                }
                                else
                                {
                                    go_Form.EnableMenu(gs_MnuBorrarFila, false);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            return lb_Result;
        }

        public void sb_AddNewRowMatrix()
        {
            try
            {
                go_Form.Freeze(true);
                go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
                go_Matrix.AddRow();
                go_Matrix.ClearRowData(go_Matrix.RowCount);
                go_Matrix.FlushToDataSource();
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                go_Form.Freeze(false);
            }
        }

        public bool fn_DeleteRowMatrix()
        {
            System.Windows.Forms.DialogResult lo_Resultado;
            lo_Resultado = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("¿Desea eliminar esta fila?", 1, "Si", "No");
            if (lo_Resultado == System.Windows.Forms.DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void sb_SetDataFromMatrix()
        {
            SAPbobsCOM.CompanyService lo_CmpnSrv = null;
            SAPbobsCOM.GeneralService lo_GnrlSrv = null;
            SAPbobsCOM.GeneralData lo_GnrlDta = null;
            SAPbobsCOM.GeneralData lo_GnrlDtaAux = null;
            SAPbobsCOM.GeneralDataCollection lo_GnrlDtaColl = null;

            lo_CmpnSrv = go_SBOCompany.GetCompanyService();
            lo_GnrlSrv = lo_CmpnSrv.GetGeneralService("2");
            lo_GnrlDta = lo_GnrlSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);
            lo_GnrlDtaColl = lo_GnrlDta.Child("STR_EARACC");
            lo_GnrlDtaAux = lo_GnrlDtaColl.Add();
            lo_GnrlDtaAux.SetProperty("Code", "AAA");
            lo_GnrlSrv.Add(lo_GnrlDta);
        }

        private void sb_FormUnload()
        {
            this.go_Form = null;
            go_DTblDimns = null;
            Dispose();
        }

        private bool fn_HandleValidate(SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            bool lb_Result = true;
            if (po_ItmEvent.BeforeAction && po_ItmEvent.ItemUID == gs_EdtCantRndc)
            {
                if (Convert.ToInt32(go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).GetValue(gs_UflCntRnd, 0)) < 1)
                {
                    go_SBOApplication.StatusBar.SetText("Ingrese una cantidad valida...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    lb_Result = false;
                }
            }
            return lb_Result;
        }

        private void sb_AddDataToDataSource()
        {
            if (go_DTblDimns != null)
            {
                SAPbouiCOM.DBDataSource lo_DBDtsDims = null;
                lo_DBDtsDims = go_Form.DataSources.DBDataSources.Item(gs_DtdEARDET2);
                lo_DBDtsDims.Clear();
                for (int i = 0; i < go_DTblDimns.Rows.Count; i++)
                {
                    lo_DBDtsDims.InsertRecord(i);
                    lo_DBDtsDims.SetValue(gs_UflDetDimNmb, i, go_DTblDimns.Rows[i][0].ToString());
                    lo_DBDtsDims.SetValue(gs_UflDetDimDsc, i, go_DTblDimns.Rows[i][1].ToString());
                    lo_DBDtsDims.SetValue(gs_UflDetDimDft, i, go_DTblDimns.Rows[i][2].ToString());
                }
            }
        }

        private void sb_EnableDisableItemsByCheck(string ps_ItemUID)
        {
            switch (ps_ItemUID)
            {
                case gs_ChkDimns:
                    go_Form.Items.Item(gs_EdtFocus).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_CheckBox = go_Form.Items.Item(gs_ChkDimns).Specific;
                    if (go_CheckBox.Checked == true) go_Form.Items.Item(gs_BtnDimns).Enabled = true;
                    else go_Form.Items.Item(gs_BtnDimns).Enabled = false;
                    break;
                case gs_ChkCntRnd:
                    go_Form.Items.Item(gs_EdtFocus).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_CheckBox = go_Form.Items.Item(gs_ChkCntRnd).Specific;
                    if (go_CheckBox.Checked == true) go_Form.Items.Item(gs_EdtCantRndc).Enabled = true;
                    else go_Form.Items.Item(gs_EdtCantRndc).Enabled = false;
                    break;
                case gs_ChkProyec:
                    go_Form.Items.Item(gs_EdtFocus).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_CheckBox = go_Form.Items.Item(gs_ChkProyec).Specific;
                    if (go_CheckBox.Checked == true) go_Form.Items.Item(gs_EdtPryDfc).Enabled = true;
                    else go_Form.Items.Item(gs_EdtPryDfc).Enabled = false;
                    break;
            }
        }

        public bool fn_HandleFormDataEvent(SAPbouiCOM.BusinessObjectInfo po_BsnssObjInf)
        {
            switch (po_BsnssObjInf.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD:
                    return fn_HandleDataLoad(po_BsnssObjInf);
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                    break;
            }
            return true;
        }

        private bool fn_HandleDataLoad(SAPbouiCOM.BusinessObjectInfo po_BsnssObjInf)
        {
            SAPbobsCOM.ChartOfAccounts lo_ChrtAcct = null;

            string ls_SysAcct = string.Empty;
            try
            {
                if (!po_BsnssObjInf.BeforeAction)
                {
                    lo_ChrtAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
                    ls_SysAcct = go_Form.DataSources.DBDataSources.Item(gs_DtcEAR).GetValue("U_ER_CTAS", 0).Trim();
                    if (lo_ChrtAcct.GetByKey(ls_SysAcct))
                    {
                        go_Static = go_Form.Items.Item("lblAcct").Specific;
                        go_Static.Caption = lo_ChrtAcct.Name;

                        go_Edit = go_Form.Items.Item("txtFmtCode").Specific;
                        go_Edit.Value = lo_ChrtAcct.FormatCode;
                    }
                    foreach (SAPbouiCOM.Item lo_Item in go_Form.Items)
                    {
                        if (lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_EDIT || lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
                        {
                            if (lo_Item.UniqueID != gs_EdtFocus && lo_Item.UniqueID != gs_EdtCantRndc && lo_Item.UniqueID != gs_EdtPryDfc)
                            {
                                go_Form.Items.Item(lo_Item.UniqueID).Enabled = false;
                            }
                        }
                        if (lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX) this.sb_EnableDisableItemsByCheck(lo_Item.UniqueID);
                    }
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                lo_ChrtAcct = null;
            }
            return true;
        }

        public void sb_EnableItemsByFindMode()
        {
            go_Form.Items.Item(gs_EdtCdgEAR).Enabled = true;
            go_Form.Items.Item(gs_EdtCodScNg).Enabled = true;
        }

    }
}
