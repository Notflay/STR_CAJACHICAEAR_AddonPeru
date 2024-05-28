using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;
using STR_CajaChica_Entregas.BL;

namespace STR_CajaChica_Entregas.UL
{
    public class Cls_EAR_Regularizacion : Cls_Global_Controles
    {
        private SAPbouiCOM.Application go_SBOApplication = null;
        private SAPbobsCOM.Company go_SBOCompany = null;
        private SAPbouiCOM.Form go_Form = null;
        private SAPbouiCOM.DBDataSources go_DBDts = null;

        //Variable Global
        public static bool lb_FlagFrmActive = false;

        //Ruta del formulario
        public const string gs_NomForm = "FormEARRGL";
        private const string gs_RutaForm = "Resources/CajaChicaEAR/FrmRegularizarSaldos.srf";

        //DataSources
        private const string gs_DtcEARCRG = "@STR_EARCRG";
        private const string gs_DtdEARCRGDET = "@STR_EARCRGDET";
        private const string gs_DtdEARCRGDET2 = "@STR_EARCRGDET2";

        //* * * * * * * * * * * * * * User Fields - @STR_EARAPR * * * * * * * * 
        private readonly string gs_UflCtaBnc = "U_ER_CTBN";
        private readonly string gs_UflChqBnco = "U_ER_CHBN";
        private readonly string gs_UflChqMnl = "U_ER_CHMN";
        private readonly string gs_UflChqNum = "U_ER_CHNM";
        private readonly string gs_UflFchTrn = "U_ER_TRFC";
        private readonly string gs_UflMPSUNAT = "U_ER_MPSN";
        private readonly string gs_UflChqFchVnc = "U_ER_CHFV";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //CONTROLES
        //ComboBox
        private readonly string gs_CmbChqBnc = "CmbChqBco";
        private readonly string gs_CmbChqCta = "CmbChqCta";
        private readonly string gs_CmbTpoRgl = "cmbTpoRgl";
        private readonly string gs_CmbChsFlw = "cmbCshFlw";
        //EditText
        private readonly string gs_EdtMntRgl = "txtMntRgl";
        private readonly string gs_EdtMndRgl = "txtMndRgl";
        private readonly string gs_EdtChqNmr = "txtChqNmr";
        private readonly string gs_EdtChqFchV = "txtChqFchV";
        private readonly string gs_EdtChqNmro = "txtChqNmr";
        private readonly string gs_EdtTrnsCta = "txtTrnCta";
        private readonly string gs_EdtEfctCta = "txtEfcCta";
        //CheckBox
        private readonly string gs_ChkChqManual = "ChkChqMnl";
        //ChooseFromList
        private readonly string gs_CFLTrnsCta = "CFLCTATR";
        private readonly string gs_CFLEfctCta = "CFLCTAEF";
        //Static
        private readonly string gs_SttNroCtTr = "lblNroCtTr";
        private readonly string gs_SttNroCtEf = "lblNroCtEf";
        //LinkButton
        private readonly string gs_LnkRglrPgo = "LnkPago";

        public Cls_EAR_Regularizacion()
        {
            go_SBOApplication = Cls_Global.go_SBOApplication;
            go_SBOCompany = Cls_Global.go_SBOCompany;
            this.go_DBDts = Cls_EAR_Carga.go_DBDts;
        }

        public void sb_FormLoad(string ps_TpoRgl, double pd_Saldo)
        {
            try
            {
                go_Form = Cls_Global.fn_CreateForm(gs_NomForm, gs_RutaForm);
                sb_DataFormLoad(ps_TpoRgl, pd_Saldo, go_DBDts.Item(gs_DtcEARCRG).GetValue("U_ER_MNDA", 0).Trim());
                lb_FlagFrmActive = true;
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void sb_DataFormLoad(string ps_TpoRgl, double ld_Saldo, string ls_MdaRgl)
        {
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;

            try
            {
                go_Form.Freeze(true);
                go_Combo = go_Form.Items.Item(gs_CmbTpoRgl).Specific;
                go_Combo.Select(ps_TpoRgl, SAPbouiCOM.BoSearchKey.psk_ByValue);
                go_Edit = go_Form.Items.Item(gs_EdtMntRgl).Specific;
                go_Edit.Value = ld_Saldo.ToString();
                go_Edit = go_Form.Items.Item(gs_EdtMndRgl).Specific;
                go_Edit.Value = ls_MdaRgl;
                go_Edit = go_Form.Items.Item("txtPgoTot").Specific;
                go_Edit.Value = Math.Abs(ld_Saldo).ToString();
                go_Combo = go_Form.Items.Item(gs_CmbChqBnc).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_EAR.fn_ListadeBancos());
                go_Combo = go_Form.Items.Item(gs_CmbChsFlw).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_ListaFlujodeCaja());
                go_Combo.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue("U_ER_FCRG", 0, go_SBOCompany.GetCompanyDate().ToString("yyyyMMdd"));
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue(gs_UflChqFchVnc, 0, DateTime.Now.ToString("yyyyMMdd"));
                ((SAPbouiCOM.Folder)go_Form.Items.Item("fldCHQ").Specific).Select();
                lo_Cnds = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "Postable";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "Y";
                go_Form.Items.Item(gs_EdtChqNmr).Enabled = false;
                go_Form.ChooseFromLists.Item(gs_CFLTrnsCta).SetConditions(lo_Cnds);
                go_Form.ChooseFromLists.Item(gs_CFLEfctCta).SetConditions(lo_Cnds);
            }
            catch (Exception ex)
            {
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
                case SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD:
                    this.sb_FormUnload(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                    lb_Result = this.fn_HandleChooseFromList(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    lb_Result = this.fn_HandleItemPressed(FormUID, po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                    lb_Result = this.fn_HandleComboSelect(FormUID, po_ItmEvnt);
                    break;
                    //case SAPbouiCOM.BoEventTypes.et_VALIDATE:
                    //    lb_Result = this.fn_HandleValidate(po_ItmEvnt);
                    //    break;
                    //case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                    //    lb_Result = this.fn_HandleFormResize(po_ItmEvnt);
                    //    break;
                    //case SAPbouiCOM.BoEventTypes.et_CLICK:
                    //  lb_Result = this.fn_HandleClick(po_ItmEvnt);
                    //   break;
            }
            return lb_Result;
        }

        private void sb_FormUnload(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            if (po_ItmEvnt.BeforeAction)
            {
                lb_FlagFrmActive = false;
            }
        }

        private bool fn_HandleChooseFromList(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            SAPbouiCOM.DataTable lo_DataTable = null;
            SAPbouiCOM.ChooseFromListEvent lo_CFLEvnt = null;

            lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
            if (!lo_CFLEvnt.BeforeAction)
            {
                lo_DataTable = lo_CFLEvnt.SelectedObjects;
                if (lo_DataTable != null)
                {
                    go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue(gs_UflCtaBnc, 0, lo_DataTable.GetValue(0, 0));
                    if (lo_CFLEvnt.ChooseFromListUID == gs_CFLTrnsCta) ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtTr).Specific).Caption = lo_DataTable.GetValue("FormatCode", 0) + " - " + lo_DataTable.GetValue(1, 0);
                    if (lo_CFLEvnt.ChooseFromListUID == gs_CFLEfctCta) ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtEf).Specific).Caption = lo_DataTable.GetValue("FormatCode", 0) + " - " + lo_DataTable.GetValue(1, 0);
                }
            }

            bool lb_Result = true;
            return lb_Result;
        }

        private bool fn_HandleItemPressed(string ps_FormUID, SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            string ls_CdgBnc = string.Empty;
            SAPbouiCOM.Form lo_FrmAux = null;

            if (go_Form == null)
            {
                go_Form = go_SBOApplication.Forms.Item(ps_FormUID);
            }
            switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
            {
                case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                    if (po_ItmEvnt.ItemUID == "1" && go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE && po_ItmEvnt.BeforeAction)
                    {
                        lb_Result = this.sb_ValidacionesGenerales();
                        if (!lb_Result) return lb_Result;

                        go_DBDts = Cls_EAR_Carga.go_DBDts;
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_TPRG", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_TPRG", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_MNRG", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_MNRG", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_MDPG", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_MDPG", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_CHFV", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_CHFV", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_CHBN", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_CHBN", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_CHMN", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_CHMN", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_CHNM", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_CHNM", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_TRFC", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_TRFC", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_TRRF", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_TRRF", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_CTBN", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_CTBN", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_MNTT", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_MNTT", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_MPSN", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_MPSN", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_MNDA", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_MNDA", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_FCRG", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_FCRG", 0));
                        go_DBDts.Item(gs_DtdEARCRGDET2).SetValue("U_ER_NMPG", 0, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_NMPG", 0));
                        lb_Result = Cls_EAR_Regularizar_BL.fn_GenerarPagoxTipoRegularizacion(go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_TPRG", 0).Trim(), go_DBDts);
                        if (lb_Result)
                        {
                            go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue("U_ER_DEPG", 0, go_DBDts.Item(gs_DtdEARCRGDET2).GetValue("U_ER_DEPG", 0));
                            go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue("U_ER_NMPG", 0, go_DBDts.Item(gs_DtdEARCRGDET2).GetValue("U_ER_NMPG", 0));
                            go_DBDts.Item(gs_DtcEARCRG).SetValue("U_ER_SLDI", 0, string.Empty);
                            lo_FrmAux = go_SBOApplication.Forms.GetForm(Cls_EAR_Carga.gs_NomForm, 1);
                            if (lo_FrmAux.Mode != SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                            {
                                lo_FrmAux.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                lo_FrmAux.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            }
                            else
                                lo_FrmAux.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            Cls_QueriesManager_EAR.sb_ActualizarEstadoySaldoXNroEAR("C", 0.0
                            , go_DBDts.Item(gs_DtcEARCRG).GetValue("U_ER_NMBR", 0).Trim(), go_DBDts.Item(gs_DtcEARCRG).GetValue("U_ER_NMRO", 0).Trim());

                            bool validar = fn_GenerarReconciliacion(go_DBDts.Item(gs_DtcEARCRG).GetValue("U_ER_NMBR", 0).Trim(), go_DBDts.Item(gs_DtcEARCRG).GetValue("U_ER_NMRO", 0).Trim());

                            if (validar)
                            {
                                lo_FrmAux.DataSources.DBDataSources.Item(gs_DtcEARCRG).SetValue("Status", 0, "C");
                                if (lo_FrmAux.Mode != SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                                {
                                    lo_FrmAux.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                    lo_FrmAux.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                }
                                else
                                    lo_FrmAux.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                lo_FrmAux.Items.Item("MtxDocs").Enabled = false;
                            }
                        }
                    }
                    else
                        go_Form.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE;
                    break;
                case SAPbouiCOM.BoFormItemTypes.it_FOLDER:
                    sb_SetPaneLevel(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX:
                    if (!po_ItmEvnt.BeforeAction)
                    {
                        if (po_ItmEvnt.ItemUID == gs_ChkChqManual)
                        {
                            sb_EnableEditTextNumeroCheque();
                        }
                    }
                    break;
                case SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON:
                    if (po_ItmEvnt.BeforeAction)
                    {
                        sb_SetLinkObject();
                    }
                    break;
            }
            return lb_Result;
        }

        private void sb_SetPaneLevel(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            string ls_MdoPgo = string.Empty;
            if (po_ItmEvnt.BeforeAction)
            {
                switch (po_ItmEvnt.ItemUID)
                {
                    case "fldCHQ":
                        go_Form.PaneLevel = 1;
                        ls_MdoPgo = "CH";
                        go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue(gs_UflCtaBnc, 0, string.Empty);
                        ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtTr).Specific).Caption = string.Empty;
                        ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtEf).Specific).Caption = string.Empty;
                        break;
                    case "fldTRN":
                        go_Form.PaneLevel = 2;
                        ls_MdoPgo = "TR";
                        go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue(gs_UflCtaBnc, 0, string.Empty);
                        ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtTr).Specific).Caption = string.Empty;
                        ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtEf).Specific).Caption = string.Empty;
                        break;
                    case "fldEFC":
                        go_Form.PaneLevel = 3;
                        ls_MdoPgo = "EF";
                        go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue(gs_UflCtaBnc, 0, string.Empty);
                        ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtTr).Specific).Caption = string.Empty;
                        ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttNroCtEf).Specific).Caption = string.Empty;
                        break;
                }
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue("U_ER_MDPG", 0, ls_MdoPgo);
            }
        }

        private bool fn_HandleComboSelect(string ps_FormUID, SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            string ls_CdgBnc = string.Empty;
            if (go_Form == null)
            {
                go_Form = go_SBOApplication.Forms.Item(ps_FormUID);
            }
            if (!po_ItmEvnt.BeforeAction && po_ItmEvnt.ItemUID == gs_CmbChqBnc)
            {
                go_Combo = go_Form.Items.Item(gs_CmbChqBnc).Specific;
                ls_CdgBnc = go_Combo.Value.Trim();
                go_Combo = go_Form.Items.Item(gs_CmbChqCta).Specific;
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue("U_ER_CTBN", 0, string.Empty);
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_EAR.fn_CuentasdeBanco(ls_CdgBnc, go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).GetValue("U_ER_MNDA", 0).Trim()));
            }
            return lb_Result;
        }
        //Excepcion UI
        public bool fn_GenerarReconciliacion(string ps_CdgEAR, string ps_NroEAR)
        {
            SAPbouiCOM.Form lo_Form = null;
            SAPbouiCOM.Form lo_Form2 = null;
            SAPbouiCOM.EditText lo_Edit = null;
            SAPbouiCOM.Matrix lo_Matrix = null;
            SAPbouiCOM.CheckBox lo_CheckBox = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;

            string ls_Qry = string.Empty;
            try
            {
                lo_RecSet = Cls_QueriesManager_EAR.fn_ObtDocsaReconciliar(ps_CdgEAR, ps_NroEAR);
                if (lo_RecSet.RecordCount > 0)
                {
                    lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
                    lo_EmpInf.GetByKey(Convert.ToInt32(ps_CdgEAR.Substring(3, ps_CdgEAR.Length - 3)));
                    Cls_Global.go_SBOApplication.ActivateMenuItem("9459");
                    lo_Form = Cls_Global.go_SBOApplication.Forms.GetForm("120060803", 1);
                    lo_Form.Items.Item("120000005").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    lo_Edit = lo_Form.Items.Item("120000008").Specific;
                    lo_Edit.Value = lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value;

                    /*
                    lo_Form.Items.Item("120000002").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    lo_Form2 = Cls_Global.go_SBOApplication.Forms.GetForm("120060805", 1);
                    lo_Matrix = lo_Form2.Items.Item("120000039").Specific;
                    lo_Matrix.Columns.Item("120000005").TitleObject.Sort(SAPbouiCOM.BoGridSortType.gst_Ascending);

                    //lo_Form2.Visible = false;
                    while (!lo_RecSet.EoF)
                    {
                        for (int i = 0; i < lo_Matrix.RowCount; i++)
                        {
                            lo_Edit = lo_Matrix.Columns.Item("120000003").Cells.Item(i + 1).Specific;
                            if (lo_Edit.Value == Convert.ToString(lo_RecSet.Fields.Item("NRO").Value))
                            {
                                lo_CheckBox = lo_Matrix.Columns.Item("120000002").Cells.Item(i + 1).Specific;
                                lo_CheckBox.Checked = true;
                                break;
                            }
                        }
                        lo_RecSet.MoveNext();
                    }
                    lo_Form2.Items.Item("120000002").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    lo_Form2 = go_SBOApplication.Forms.ActiveForm;
                    lo_Form2.Items.Item("120000002").Click(SAPbouiCOM.BoCellClickType.ct_Regular);*/
                    
                    lo_Form.Items.Item("120000001").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    lo_Form2 = Cls_Global.go_SBOApplication.Forms.GetForm("120060805", 1);
                    lo_Matrix = lo_Form2.Items.Item("120000039").Specific;
                    lo_Matrix.Columns.Item("120000005").TitleObject.Sort(SAPbouiCOM.BoGridSortType.gst_Ascending);

                    //lo_Form2.Visible = false;
                    while (!lo_RecSet.EoF)
                    {
                        for (int i = 0; i < lo_Matrix.RowCount; i++)
                        {
                            lo_Edit = lo_Matrix.Columns.Item("120000003").Cells.Item(i + 1).Specific;
                            if (lo_Edit.Value == Convert.ToString(lo_RecSet.Fields.Item("NRO").Value))
                            {
                                lo_CheckBox = lo_Matrix.Columns.Item("120000002").Cells.Item(i + 1).Specific;
                                lo_CheckBox.Checked = true;
                                break;
                            }
                        }
                        lo_RecSet.MoveNext();
                    }
                    lo_Form2.Items.Item("120000001").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    lo_Form2 = go_SBOApplication.Forms.ActiveForm;
                    try
                    {
                        lo_Form2.Items.Item("120000001").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    }
                    catch (Exception)
                    {

                    }
                    
                    if (go_SBOApplication.Forms.ActiveForm.TypeEx != "120060805")
                        return true;
                    else
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Cls_Global.go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
        }

        private void sb_EnableEditTextNumeroCheque()
        {
            go_CheckBox = go_Form.Items.Item(gs_ChkChqManual).Specific;
            if (go_CheckBox.Checked)
            {
                go_Form.Items.Item(gs_EdtChqNmr).Enabled = true;
            }
            else
            {
                if (((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtChqNmr).Specific).Active == true)
                {
                    go_Form.Items.Item(gs_EdtChqFchV).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2).SetValue(gs_UflChqNum, 0, "0");
                    go_Form.Items.Item(gs_EdtChqNmr).Enabled = false;
                }
                else
                {
                    go_Form.Items.Item(gs_EdtChqNmr).Enabled = false;
                }
            }
        }

        private void sb_SetLinkObject()
        {
            go_LinkButton = go_Form.Items.Item(gs_LnkRglrPgo).Specific;
            go_Combo = go_Form.Items.Item(gs_CmbTpoRgl).Specific;
            if (go_Combo.Value.Trim() == "RNT")
                go_LinkButton.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_VendorPayment;
            else
                go_LinkButton.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_Receipt;
        }

        private bool sb_ValidacionesGenerales()
        {
            bool lb_Result = true;
            string ls_MsgErr = string.Empty;
            SAPbouiCOM.DBDataSource lo_DBDTSEARAPRDET2 = null;

            lo_DBDTSEARAPRDET2 = go_Form.DataSources.DBDataSources.Item(gs_DtdEARCRGDET2);
            //Datos de Cabecera 
            switch (go_Form.PaneLevel)
            {
                case 1://Cheque
                    if (lo_DBDTSEARAPRDET2.GetValue(gs_UflChqBnco, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Debe Seleccionar un banco...";
                        lb_Result = false;
                        ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbChqBnc).Specific).Active = true;
                        goto fin;
                    }
                    if (lo_DBDTSEARAPRDET2.GetValue(gs_UflCtaBnc, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione una cuenta bancaria...";
                        lb_Result = false;
                        ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbChqCta).Specific).Active = true;
                        goto fin;
                    }
                    if (lo_DBDTSEARAPRDET2.GetValue(gs_UflChqMnl, 0).Trim() == "Y" && (lo_DBDTSEARAPRDET2.GetValue(gs_UflChqNum, 0).Trim() == string.Empty || lo_DBDTSEARAPRDET2.GetValue(gs_UflChqNum, 0).Trim() == "0"))
                    {
                        ls_MsgErr = "Ingrese el numero de cheque...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtChqNmro).Specific).Active = true;
                        goto fin;
                    }
                    break;
                case 2://Transferencia
                    if (lo_DBDTSEARAPRDET2.GetValue(gs_UflCtaBnc, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione cuenta de transferencia...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtTrnsCta).Specific).Active = true;
                        goto fin;
                    }
                    if (lo_DBDTSEARAPRDET2.GetValue(gs_UflFchTrn, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Ingrese fecha de transferencia...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item("txtTrnFch").Specific).Active = true;
                        goto fin;
                    }
                    break;
                case 3:
                    if (lo_DBDTSEARAPRDET2.GetValue(gs_UflCtaBnc, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione cuenta para pagos en efectivo...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtEfctCta).Specific).Active = true;
                        goto fin;
                    }
                    break;
            }
            if (lo_DBDTSEARAPRDET2.GetValue(gs_UflMPSUNAT, 0).Trim() == string.Empty)
            {
                ls_MsgErr = "Seleccione el medio de pago SUNAT...";
                lb_Result = false;
                goto fin;
            }
        fin:
            if (!lb_Result)
            {
                go_SBOApplication.StatusBar.SetText(ls_MsgErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            return lb_Result;
        }

    }
}
