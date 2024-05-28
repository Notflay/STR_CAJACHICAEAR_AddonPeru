using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;
using STR_CajaChica_Entregas.BL;
using System.Diagnostics;

namespace STR_CajaChica_Entregas.UL
{
    class Cls_FormsRelacionados : Cls_Global_Controles
    {
        //Menus
        public const string gs_MnuAñadirFila = "MNU_AddLineEAR";
        public const string gs_MnuBorrarFila = "MNU_DltLineEAR";

        //Id Form
        //Plan de cuentas
        public const string gs_NomFormPlanCuentas = "806";
        //Solicitud de compra 
        public const string gs_NomFormSolicCompra = "1470000200";
        //Socios de Negocio
        public const string gs_NomFormSocioNegocio = "134";
        //Medios de Pago - Pago Efectuado
        public const string gs_NomFormMediosdePago_PE = "196";
        //Medios de Pago - Pago Recibido
        public const string gs_NomFormMediosdePago_PR = "146";
        //Pago Efectuado
        public const string gs_NomFormPagoEfectuado = "426";
        //Pago Recibido
        public const string gs_NomFormPagoRecibido= "170";
        //Datos maestros empleado
        public const string gs_NomFormMaestroEmpleados = "60100";
        //Message Box Reconciliacion
        public const string gs_NomFormMsgBxReconciliacion = "0";

        //Variables Globales
        private int gi_RightClickRow = -1;

        //Controles 
        //-->Plan de Cuentas
        //CheckBox 
        private string gs_ChkCuentaCCH_EAR = "chkCCHEAR";
        private string gs_ChkExcCCHEAR = "chkExcCE";

        //-->Form Solicitud de Compras
        //ChooseFromList
        private string gs_CFLProveedores = "28";
        private string gs_CFLEmpleado = "7";
        //ComboBox
        private string gs_CmbMndEAR = "cboMndEAR";
        //EditText
        private string gs_EdtCCHNmb = "txtCode";
        private string gs_EdtTotEAR = "txtTotEAR";
        //Static
        private string gs_SttMndEAR = "lblMndEAR";

        //-->Medios de Pago
        //ChooseFromList
        private string gs_CFLCCHNMB = "CFLCCHNMB";
        private string gs_CFLEARNMB = "CFLEARNMB";
        //ComboBox
        private string gs_CmbTpoRnd = "cboTipr";
        private string gs_CmbNroCCH = "cboNumC";

        //-->Form datos maestros empleado
        //DataSources 
        private string gs_DTSDETHEMEAR = "@STR_HEMEAR";

        //Folder
        private string gs_FldEAR = "fldEAR";
        //Static
        private string gs_SttSocNg = "sttSocNg";
        private string gs_SttNmbPv = "sttNmbPv";
        private string gs_SttMndPv = "sttMndPv";
        private string gs_SttCtaCt = "sttCtaCt";
        private string gs_SttEstEAR = "sttEstEAR";
        private string gs_SttAcces = "sttAcces";
        //EditText
        private const string gs_EdtSocNg = "edtSocNg";
        private const string gs_EdtNmbPv = "edtNmbPv";
        private const string gs_EdtPrycs = "edtPrycs";
        private const string gs_EdtRndcs = "edtRndcs";
        //ComboBox
        private const string gs_CmbMndPv = "cmbMndPv";
        private const string gs_CmbEstEAR = "cmbEstEAR";
        private const string gs_CmbCtaCt = "cmbCtaCt";
        //CheckBox 
        private const string gs_ChkPrycs = "chkPrycs";
        private const string gs_ChkDimns = "chkDimns";
        private const string gs_ChkNroRnd = "chkNroRnd";
        //Button
        private string gs_BtnDimns = "BtnDimns";
        //Matrix
        private string gs_MtxAccesos = "mtxAccesos";
        //ChooseFromList
        private string gs_CFLPrvs = "cflPrvs";
        private string gs_CFLUsrs = "cflUsrs";
        private string gs_CFLPrys = "cflPrys";

        private SAPbobsCOM.Company go_SBOCompany = null;
        private SAPbouiCOM.Application go_SBOApplication = null;

        public Cls_FormsRelacionados()
        {
            go_SBOCompany = Cls_Global.go_SBOCompany;
            go_SBOApplication = Cls_Global.go_SBOApplication;
        }

        private bool fn_HandleFormLoad(SAPbouiCOM.Form po_Form, SAPbouiCOM.ItemEvent po_ItemEvent)
        {
            if (po_ItemEvent.BeforeAction)
            {
                switch (po_ItemEvent.FormTypeEx)
                {
                    case gs_NomFormPlanCuentas:
                        sb_FormPlandeCuentasLoad(po_Form);
                        break;
                    case gs_NomFormSolicCompra:
                        sb_FormSolucituddeCompraLoad(po_Form);
                        break;
                    case gs_NomFormSocioNegocio:
                        sb_FormSociodeNegocioLoad(po_Form);
                        break;
                    case gs_NomFormMediosdePago_PE:
                    case gs_NomFormMediosdePago_PR:
                        sb_FormMediosdePagoLoad(po_Form);
                        break;
                    case gs_NomFormMaestroEmpleados:
                        sb_FormMaestroEmpleadosLoad(po_Form);
                        break;
                }
            }
            else
            {
                switch (po_ItemEvent.FormTypeEx)
                {
                    case gs_NomFormMsgBxReconciliacion:
                        this.sb_FormMsgBxReconciliacionLoad(po_Form);
                        break;
                }
            }
            return true;
        }

        private void sb_FormMsgBxReconciliacionLoad(SAPbouiCOM.Form po_Form)
        {
            if(Cls_EAR_Regularizacion.lb_FlagFrmActive)
            po_Form.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
        }

        private void sb_FormPlandeCuentasLoad(SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Item lo_Item = null;
            try
            {
                lo_Item = po_Form.Items.Add(gs_ChkCuentaCCH_EAR, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
                lo_Item.Height = po_Form.Items.Item("49").Height;
                lo_Item.Width = po_Form.Items.Item("49").Width;
                lo_Item.Left = po_Form.Items.Item("30").Left;
                lo_Item.FromPane = 1;
                lo_Item.ToPane = 1;
                lo_Item.Top = po_Form.Items.Item("92").Top;
                go_CheckBox = lo_Item.Specific;
                go_CheckBox.Caption = "Cuenta CCH/EAR";
                go_CheckBox.DataBind.SetBound(true, "OACT", "U_CE_ACCT");
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium);   
            }
        }

        private void sb_FormSolucituddeCompraLoad(SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Item lo_Item = null;
            try
            {
                lo_Item = po_Form.Items.Add(gs_ChkCuentaCCH_EAR, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
                lo_Item.Height = po_Form.Items.Item("1470002173").Height;
                lo_Item.Width = po_Form.Items.Item("1470002173").Width;
                lo_Item.Left = po_Form.Items.Item("1470002173").Left;
                lo_Item.Top = po_Form.Items.Item("1470002173").Top + 30;
                lo_Item.FromPane = 0;
                lo_Item.ToPane = 0;
                lo_Item.Visible = false;
                go_CheckBox = lo_Item.Specific;
                go_CheckBox.Caption = "Solicitud de Dinero Entrega a Rendir";
                go_CheckBox.DataBind.SetBound(true, "OPRQ", "U_CE_EAR");

                lo_Item = po_Form.Items.Add(gs_SttMndEAR, SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_Item.Height = po_Form.Items.Item("1470002171").Height;
                lo_Item.Width = po_Form.Items.Item("1470002171").Width;
                lo_Item.Left = po_Form.Items.Item("1470002171").Left;
                lo_Item.Top = po_Form.Items.Item("1470002171").Top + 30;
                lo_Item.FromPane = 0;
                lo_Item.ToPane = 0;
                lo_Item.Visible = true;
                go_Static = lo_Item.Specific;
                go_Static.Caption = "Moneda SN";

                lo_Item = po_Form.Items.Add(gs_CmbMndEAR, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                lo_Item.Height = po_Form.Items.Item("1470002172").Height;
                lo_Item.Width = po_Form.Items.Item("1470002172").Width / 2;
                lo_Item.Left = po_Form.Items.Item("1470002172").Left;
                lo_Item.Top = po_Form.Items.Item("1470002172").Top + 30;
                lo_Item.FromPane = 0;
                lo_Item.ToPane = 0;
                lo_Item.Visible = true;
                go_Combo = lo_Item.Specific;
                go_Combo.DataBind.SetBound(true, "OPRQ", "U_CE_MNDA");
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_MonedasSociedad());

                lo_Item = po_Form.Items.Add(gs_EdtTotEAR, SAPbouiCOM.BoFormItemTypes.it_EDIT);
                lo_Item.Height = po_Form.Items.Item("29").Height;
                lo_Item.Width = po_Form.Items.Item("29").Width;
                lo_Item.Left = po_Form.Items.Item("29").Left;
                lo_Item.Top = po_Form.Items.Item("29").Top;
                lo_Item.FromPane = po_Form.Items.Item("29").FromPane;
                lo_Item.ToPane = po_Form.Items.Item("29").ToPane;
                lo_Item.Enabled = false;
                lo_Item.Visible = true;
                lo_Item.RightJustified = true;
                lo_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable,((int)SAPbouiCOM.BoAutoFormMode.afm_All),SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Edit = lo_Item.Specific;
                go_Edit.DataBind.SetBound(true, "OPRQ", "U_CE_TTSL");
                if (go_CheckBox.Checked)
                {
                    this.sb_VisualizarModificacionesSC(po_Form);
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
        
        }

        private void sb_FormSociodeNegocioLoad(SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Item lo_Item = null;
            try
            {
                lo_Item = po_Form.Items.Add(gs_ChkCuentaCCH_EAR, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
                lo_Item.Height = po_Form.Items.Item("135").Height;
                lo_Item.Width = po_Form.Items.Item("135").Width + 30;
                lo_Item.Left = po_Form.Items.Item("135").Left;
                lo_Item.FromPane = po_Form.Items.Item("135").FromPane;
                lo_Item.ToPane = po_Form.Items.Item("135").ToPane;
                lo_Item.Top = po_Form.Items.Item("135").Top + 15;
                go_CheckBox = lo_Item.Specific;
                go_CheckBox.Caption = "Usuario de Entrega a Rendir";
                go_CheckBox.DataBind.SetBound(true, "OCRD", "U_CE_USEAR");

                lo_Item = po_Form.Items.Add(gs_ChkExcCCHEAR, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
                lo_Item.Height = po_Form.Items.Item("380002062").Height;
                lo_Item.Width = po_Form.Items.Item("380002062").Width + 150;
                lo_Item.Left = po_Form.Items.Item("380002062").Left;
                lo_Item.FromPane = po_Form.Items.Item("380002062").FromPane;
                lo_Item.ToPane = po_Form.Items.Item("380002062").ToPane;
                lo_Item.Top = po_Form.Items.Item("380002062").Top + 15;
                go_CheckBox = lo_Item.Specific;
                go_CheckBox.Caption = "Excluir de registro de documentos caja chica - entrega a rendir";
                go_CheckBox.DataBind.SetBound(true, "OCRD", "U_CE_NOCE");
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
        }

        private void sb_FormMediosdePagoLoad(SAPbouiCOM.Form po_Form)
        { 
            SAPbouiCOM.Item lo_Item = null;
            SAPbouiCOM.ChooseFromListCreationParams lo_CFLCrtPrms = null;
            SAPbouiCOM.ChooseFromListCollection lo_CFLColltn = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;

            try
            {
                if (Process.GetProcessesByName("STR_Addon").Length > 0) return;
                lo_Item = po_Form.Items.Add("lblTipr", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_Item.FromPane = 3;
                lo_Item.ToPane = 3;
                lo_Item.Width = 100;
                lo_Item.Left = 10;
                lo_Item.Top = po_Form.Items.Item("42").Top + po_Form.Items.Item("35").Top + 2;
                lo_Item.Height = 12;
                go_Static = lo_Item.Specific;
                go_Static.Caption = "Tipo Rendición";
                
                lo_Item = po_Form.Items.Add(gs_CmbTpoRnd, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                lo_Item.FromPane = 3;
                lo_Item.ToPane = 3;
                lo_Item.Top = po_Form.Items.Item("42").Top + po_Form.Items.Item("35").Top + 2;
                lo_Item.Left = po_Form.Items.Item("32").Left;
                lo_Item.Width = po_Form.Items.Item("32").Width;
                lo_Item.Height = 14;
                lo_Item.DisplayDesc = true;
                go_Combo = lo_Item.Specific;
                go_Combo.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_BPP_TIPR");                

                lo_Item = po_Form.Items.Add("lblcode",SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_Item.FromPane = 3;
                lo_Item.ToPane = 3;
                lo_Item.Width = 100;
                lo_Item.Left = 10;
                lo_Item.Top = po_Form.Items.Item("42").Top + po_Form.Items.Item("35").Top + 18;
                lo_Item.Height = 12;
                go_Static = lo_Item.Specific;
                go_Static.Caption = "Caja/Entrega";

                lo_Item = po_Form.Items.Add(gs_EdtCCHNmb, SAPbouiCOM.BoFormItemTypes.it_EDIT);
                lo_Item.FromPane = 3;
                lo_Item.ToPane = 3;
                lo_Item.Top = po_Form.Items.Item("42").Top + po_Form.Items.Item("35").Top + 18;
                lo_Item.Left = po_Form.Items.Item("32").Left;
                lo_Item.Width = po_Form.Items.Item("32").Width;
                lo_Item.Height = 14;
                go_Edit = lo_Item.Specific;
                go_Edit.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_BPP_CCHI");
                //Caja Chicas
                lo_CFLCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                lo_CFLColltn = po_Form.ChooseFromLists;
                lo_CFLCrtPrms.MultiSelection = false;
                lo_CFLCrtPrms.ObjectType = "BPP_CAJASCHICAS";
                lo_CFLCrtPrms.UniqueID = gs_CFLCCHNMB;
                lo_CFL = lo_CFLColltn.Add(lo_CFLCrtPrms);

                lo_Cnds = lo_CFL.GetConditions();
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "U_BPP_STAD";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "A";
                lo_CFL.SetConditions(lo_Cnds);

                //Entregas a Rendir
                lo_CFLCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
                lo_CFLColltn = po_Form.ChooseFromLists;
                lo_CFLCrtPrms.MultiSelection = false;
                lo_CFLCrtPrms.ObjectType = "171";
                lo_CFLCrtPrms.UniqueID = gs_CFLEARNMB;
                lo_CFL = lo_CFLColltn.Add(lo_CFLCrtPrms);

                lo_Item = po_Form.Items.Add("lblNumC", SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_Item.FromPane = 3;
                lo_Item.ToPane = 3;
                lo_Item.Width = 100;
                lo_Item.Left = 10;
                lo_Item.Top = po_Form.Items.Item("42").Top + po_Form.Items.Item("35").Top + 34;
                lo_Item.Height = 12;
                go_Static = lo_Item.Specific;
                go_Static.Caption = "Num. Caja";

                lo_Item = po_Form.Items.Add(gs_CmbNroCCH, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                lo_Item.FromPane = 3;
                lo_Item.ToPane = 3;
                lo_Item.Top = po_Form.Items.Item("42").Top + po_Form.Items.Item("35").Top + 34;
                lo_Item.Left = po_Form.Items.Item("32").Left;
                lo_Item.Width = po_Form.Items.Item("32").Width;
                lo_Item.Height = 14;
                go_Combo = lo_Item.Specific;
                go_Combo.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_BPP_NUMC");
                
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
        }
        
        public bool fn_HandleItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            bool lb_Result = true;
            SAPbouiCOM.Form lo_FrmAux = null;
            try
            {
                switch (po_ItmEvent.EventType)
                {
                    case SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD:
                        break;
                    case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                        lo_FrmAux = go_SBOApplication.Forms.Item(FormUID);
                        fn_HandleFormLoad(lo_FrmAux,po_ItmEvent);
                        break;
                    case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                        lo_FrmAux = go_SBOApplication.Forms.Item(FormUID);
                        sb_HandleChooseFromList(lo_FrmAux,po_ItmEvent);
                        break;
                    case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                        lo_FrmAux = go_SBOApplication.Forms.Item(FormUID);
                        sb_HandleComboSelect(lo_FrmAux, po_ItmEvent);
                        break;
                    case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                        lo_FrmAux = go_SBOApplication.Forms.Item(FormUID);
                        sb_HandleItemPressed(lo_FrmAux, po_ItmEvent);
                        break;
                    case SAPbouiCOM.BoEventTypes.et_LOST_FOCUS:
                        lo_FrmAux = go_SBOApplication.Forms.Item(FormUID);
                        sb_handleLostFocus(lo_FrmAux, po_ItmEvent);
                        break;
                    case SAPbouiCOM.BoEventTypes.et_VALIDATE:
                        lo_FrmAux = go_SBOApplication.Forms.Item(FormUID);
                        sb_HandleValidate(lo_FrmAux, po_ItmEvent);
                        break;
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium);
            }
            finally
            {
                lo_FrmAux = null;
            }
            return lb_Result;
        }

        private void sb_HandleValidate(SAPbouiCOM.Form lo_FrmAux,SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            double ld_TotSlc = 0.0;

            if(po_ItmEvnt.ItemUID != string.Empty)
            {
                switch (po_ItmEvnt.FormTypeEx)
                {
                    case gs_NomFormSolicCompra:
                        if(po_ItmEvnt.BeforeAction == false && po_ItmEvnt.ColUID == "U_CE_IMSL")
                        {
                            try
                            {
                                if (lo_FrmAux.DataSources.DBDataSources.Item("OPRQ").GetValue("DocType", 0).Trim() == "S")
                                    go_Matrix = lo_FrmAux.Items.Item("39").Specific;
                                else
                                    go_Matrix = lo_FrmAux.Items.Item("38").Specific;
                                go_Edit = lo_FrmAux.Items.Item(gs_EdtTotEAR).Specific;
                                //go_Edit.Value = ((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific("U_CE_IMSL", 1)).Value;
                                for (int i = 1; i < go_Matrix.RowCount + 1; i++)
                                {
                                    if (((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific("1", i)).Value == string.Empty) continue;
                                    ld_TotSlc += Convert.ToDouble(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific("U_CE_IMSL", i)).Value);
                                }
                                go_Edit.Value = ld_TotSlc.ToString();
                                ld_TotSlc = 0.0;
                            }
                            catch (Exception ex)
                            {
                                go_SBOApplication.StatusBar.SetText(ex.Message,SAPbouiCOM.BoMessageTime.bmt_Short,SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                            }
                        }
                        break;
                }
            }
        }

        private void sb_handleLostFocus(SAPbouiCOM.Form lo_FrmAux, SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            if(po_ItmEvent.ItemUID != string.Empty)
            {
                switch(po_ItmEvent.FormTypeEx)
                {
                    case gs_NomFormSolicCompra:
                        if (po_ItmEvent.BeforeAction == false && po_ItmEvent.ItemUID == "1470002187")
                        {
                            sb_AñadirProveedorAsociado(lo_FrmAux);
                        }
                        break;
                }
            }
        }

        private void sb_HandleItemPressed(SAPbouiCOM.Form lo_FrmAux, SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            if(po_ItmEvent.ItemUID != string.Empty)
            {
                switch(po_ItmEvent.FormTypeEx)
                {
                    case gs_NomFormSolicCompra:
                        if (po_ItmEvent.ItemUID == gs_ChkCuentaCCH_EAR)
                        {
                            if (!po_ItmEvent.BeforeAction)
                            {
                                go_CheckBox = lo_FrmAux.Items.Item(gs_ChkCuentaCCH_EAR).Specific;
                                if (go_CheckBox.Checked)
                                {
                                    this.sb_VisualizarModificacionesSC(lo_FrmAux);
                                    sb_AñadirProveedorAsociado(lo_FrmAux);
                                }
                                else 
                                {
                                    lo_FrmAux.Items.Item("1470002179").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                    lo_FrmAux.Items.Item(gs_SttMndEAR).Visible = false;
                                    lo_FrmAux.Items.Item(gs_CmbMndEAR).Visible = false;
                                    lo_FrmAux.Items.Item(gs_EdtTotEAR).Visible = false;
                                    lo_FrmAux.Items.Item("29").Visible = true;
                                    //sb_OcultarColumnsMatrixSC(true, lo_FrmAux);
                                }
                            }
                        }
                        break;
                    case gs_NomFormMaestroEmpleados:
                        switch (lo_FrmAux.Items.Item(po_ItmEvent.ItemUID).Type)
                        { 
                            case SAPbouiCOM.BoFormItemTypes.it_FOLDER:
                                    if (po_ItmEvent.BeforeAction && po_ItmEvent.ItemUID == gs_FldEAR)
                                    {
                                        lo_FrmAux.PaneLevel = 8;
                                        foreach (SAPbouiCOM.Item lo_Item in lo_FrmAux.Items)
                                        {
                                            if (lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX) this.sb_EnableDisableItemsByCheck(lo_FrmAux, lo_Item.UniqueID);
                                        }
                                    }
                                    break;
                            case SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX:
                                    if (!po_ItmEvent.BeforeAction)
                                    {
                                        this.sb_EnableDisableItemsByCheck(lo_FrmAux,po_ItmEvent.ItemUID);
                                    }
                                    break;
                            case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                                    if(!po_ItmEvent.BeforeAction && po_ItmEvent.ItemUID == gs_BtnDimns)
                                    {
                                        new Cls_EAR_Dimensiones().sb_FormLoad(lo_FrmAux.DataSources.DBDataSources.Item("OHEM").GetValue("empID",0).Trim());
                                    }
                                    break;
                        }
                        break;
                }
            }
        }

        private void sb_HandleComboSelect(SAPbouiCOM.Form po_FrmAux, SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;

            lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);

            if (po_ItmEvent.ItemUID != string.Empty)
            { 
                switch(po_ItmEvent.FormTypeEx)
                {
                    case gs_NomFormMediosdePago_PE:
                    case gs_NomFormMediosdePago_PR:
                        if(po_ItmEvent.ItemUID == gs_CmbTpoRnd)
                        {
                            if (!po_ItmEvent.BeforeAction)
                            {
                                go_Combo = po_FrmAux.Items.Item(gs_CmbTpoRnd).Specific;
                                go_Edit = po_FrmAux.Items.Item(gs_EdtCCHNmb).Specific;
                                go_Edit.Item.Refresh();
                                if (go_Combo.Value.Trim() == "CCH")
                                {
                                    go_Edit.ChooseFromListUID = gs_CFLCCHNMB;
                                    go_Edit.ChooseFromListAlias = "Code";
                                }
                                else if (go_Combo.Value.Trim() == "EAR")
                                {
                                    go_Edit.ChooseFromListUID = gs_CFLEARNMB;
                                    go_Edit.ChooseFromListAlias = "U_CE_CEAR";
                                }
                                go_Edit.Value = string.Empty;
                                go_Combo = po_FrmAux.Items.Item(gs_CmbNroCCH).Specific;
                                if (go_Combo.ValidValues.Count > 0)
                                {
                                    while (go_Combo.ValidValues.Count > 0)
                                    {
                                        go_Combo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                                    }
                                }
                                go_Combo.ValidValues.Add("---", "---");
                                go_Combo.SelectExclusive(0, SAPbouiCOM.BoSearchKey.psk_Index);
                            }
                        }
                        break;
                    case gs_NomFormSolicCompra:
                        if(!po_ItmEvent.BeforeAction && po_ItmEvent.ItemUID == "1470002186")
                        {
                            go_Edit = po_FrmAux.Items.Item("1470002177").Specific;
                            go_Combo = po_FrmAux.Items.Item("1470002186").Specific;
                            if(go_Combo.Value.Trim() == "171" && go_Edit.Value != string.Empty)
                            {
                                lo_EmpInf.GetByKey(Convert.ToInt32(go_Edit.Value.Trim()));
                                if (lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value.Trim() != string.Empty)
                                    po_FrmAux.Items.Item(gs_ChkCuentaCCH_EAR).Visible = true;
                                else
                                    po_FrmAux.Items.Item(gs_ChkCuentaCCH_EAR).Visible = false;
                            }
                            else
                                po_FrmAux.Items.Item(gs_ChkCuentaCCH_EAR).Visible = false;       
                        }
                        break;
                }
            }
        }

        private void sb_HandleChooseFromList(SAPbouiCOM.Form po_Form, SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.ChooseFromListEvent lo_CFLEvnt = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbouiCOM.DataTable lo_DataTable = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbobsCOM.ChartOfAccounts lo_ChrtOfAcct = null;
            SAPbobsCOM.UserTable lo_UTbl = null;
            SAPbobsCOM.BusinessPartners lo_BsnssPrtnrs = null;
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;
            SAPbouiCOM.Form lo_FrmAux = null;
            string ls_CdgCta = string.Empty;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            string ls_CdgPrvAs = string.Empty;

            lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            lo_ChrtOfAcct = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
            lo_UTbl = go_SBOCompany.UserTables.Item("STR_CCHEAR_SYS");
            lo_BsnssPrtnrs = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
            lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);

            switch(po_ItmEvnt.FormTypeEx)
            {
                case gs_NomFormSolicCompra:
                    lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
                    if (lo_CFLEvnt.BeforeAction && lo_CFLEvnt.ChooseFromListUID == gs_CFLProveedores)
                    {
                        lo_EmpInf.GetByKey(Convert.ToInt32(po_Form.DataSources.DBDataSources.Item("OPRQ").GetValue("Requester", 0).Trim()));
                        lo_CFL = po_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                        if (po_Form.DataSources.DBDataSources.Item("OPRQ").GetValue("U_CE_EAR", 0).ToUpper() == "Y")
                        {
                            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLProveedores)
                            {
                                lo_CFL.SetConditions(null);
                                lo_Cnds = lo_CFL.GetConditions();
                                lo_Cnd = lo_Cnds.Add();
                                lo_Cnd.Alias = "CardCode";
                                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                                lo_Cnd.CondVal = Convert.ToString(lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value).Trim();
                                lo_CFL.SetConditions(lo_Cnds);
                            }
                        }
                        else
                        {
                            lo_CFL.SetConditions(null);
                        }
                    }
                    if(!lo_CFLEvnt.BeforeAction && lo_CFLEvnt.ChooseFromListUID ==  "7")
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if(lo_DataTable != null)
                        {
                            lo_EmpInf.GetByKey(lo_DataTable.GetValue(0, 0));
                            go_Combo = po_Form.Items.Item("1470002186").Specific;
                            if(go_Combo.Value.Trim() == "171" && lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value != string.Empty)
                            {
                                po_Form.Items.Item(gs_ChkCuentaCCH_EAR).Visible = true;
                            }
                            else
                                po_Form.Items.Item(gs_ChkCuentaCCH_EAR).Visible = false;
                        }
                    }
                    break;   
                case gs_NomFormMediosdePago_PE:
                case gs_NomFormMediosdePago_PR:
                    lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
                    if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCCHNMB || lo_CFLEvnt.ChooseFromListUID == gs_CFLEARNMB)
                    {
                        go_Combo = po_Form.Items.Item(gs_CmbTpoRnd).Specific;
                        if (lo_CFLEvnt.BeforeAction)
                        {
                            lo_CFL = po_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                            lo_CFL.SetConditions(null);
                            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCCHNMB)
                            {
                                lo_Cnds = lo_CFL.GetConditions();
                                lo_Cnd = lo_Cnds.Add();
                                lo_Cnd.Alias = "U_BPP_TIPR";
                                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                                lo_Cnd.CondVal = go_Combo.Value;
                                lo_CFL.SetConditions(lo_Cnds);
                            }

                            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLEARNMB && go_Combo.Value.Trim() == "---")
                            {
                                lo_Cnds = lo_CFL.GetConditions();
                                lo_Cnd = lo_Cnds.Add();
                                lo_Cnd.Alias = "empID";
                                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                                lo_Cnd.CondVal = "-1";
                                lo_CFL.SetConditions(lo_Cnds);
                            }
                            else if (lo_CFLEvnt.ChooseFromListUID == gs_CFLEARNMB)
                            {
                                lo_Cnds = lo_CFL.GetConditions();
                                lo_Cnd = lo_Cnds.Add();
                                lo_Cnd.Alias = "Active";
                                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                                lo_Cnd.CondVal = "Y";
                                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                                lo_Cnd = lo_Cnds.Add();
                                lo_Cnd.Alias = "U_CE_PVAS";
                                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_NULL;
                                lo_CFL.SetConditions(lo_Cnds);
                            }
                        }
                        else
                        { 
                            lo_DataTable = lo_CFLEvnt.SelectedObjects;
                            if(lo_DataTable != null)
                            {                 
                                go_Combo = po_Form.Items.Item(gs_CmbTpoRnd).Specific;
                                if (go_Combo.Value.Trim() == "CCH")
                                {
                                    try
                                    {
                                        go_Edit = po_Form.Items.Item(lo_CFLEvnt.ItemUID).Specific;
                                        go_Edit.Value = lo_DataTable.GetValue("Code", 0);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    ls_CdgCta = lo_DataTable.GetValue("U_BPP_ACCT", 0);
                                    lo_ArrCad = Cls_QueriesManager_CCH.NumerosdeCCHActivos.Split(new char[] { '?' });
                                    ls_Qry = lo_ArrCad[0].Trim() + lo_DataTable.GetValue(0, 0) + lo_ArrCad[1].Trim();
                                    Cls_Global.WriteToFile(ls_Qry);
                                    lo_RecSet.DoQuery(ls_Qry);
                                }
                                else                                
                                {
                                    try
                                    {
                                        go_Edit = po_Form.Items.Item(lo_CFLEvnt.ItemUID).Specific;
                                        go_Edit.Value = lo_DataTable.GetValue("U_CE_CEAR", 0);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    lo_UTbl.GetByKey("001");
                                    ls_CdgCta = lo_UTbl.UserFields.Fields.Item("U_CE_CTPT").Value;
                                    ls_CdgCta = Cls_QueriesManager_EAR.fn_ObtenerCodigoCtaPuenteEAR(ls_CdgCta);
                                    if(ls_CdgCta == string.Empty)
                                    {
                                        go_SBOApplication.StatusBar.SetText("No se ha registrado cuenta puente EAR ...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                        return;
                                    }
                                    lo_RecSet = Cls_QueriesManager_EAR.fn_NumerosEARActivos(lo_DataTable.GetValue("U_CE_CEAR", 0));
                                }
                                go_Combo = po_Form.Items.Item(gs_CmbNroCCH).Specific;
                                Cls_Global.sb_CargarCombo(go_Combo, lo_RecSet,true);
                                go_Combo.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

                                if (lo_ChrtOfAcct.GetByKey(ls_CdgCta))
                                {
                                    try
                                    {
                                        go_Edit = po_Form.Items.Item("32").Specific;
                                        go_Edit.String = lo_ChrtOfAcct.FormatCode.Substring(0, 6).Trim();                                       
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    go_Static = po_Form.Items.Item("35").Specific;
                                    go_Static.Caption = lo_ChrtOfAcct.Name;
                                }                                
                            }                            
                        }
                    }
                    break;
                case gs_NomFormMaestroEmpleados:
                    lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
                    if (lo_CFLEvnt.ChooseFromListUID == gs_CFLPrvs)
                    {
                        if (!lo_CFLEvnt.BeforeAction)
                        {
                            lo_DataTable = lo_CFLEvnt.SelectedObjects;
                            if (lo_DataTable != null)
                            {
                                try
                                {
                                    ((SAPbouiCOM.EditText)po_Form.Items.Item(gs_EdtSocNg).Specific).Value = lo_DataTable.GetValue(0, 0);
                                }
                                catch (Exception)
                                { }
                                lo_BsnssPrtnrs.GetByKey(lo_DataTable.GetValue(0, 0));
                                ((SAPbouiCOM.EditText)po_Form.Items.Item(gs_EdtNmbPv).Specific).Value = lo_BsnssPrtnrs.CardName;
                                ((SAPbouiCOM.ComboBox)po_Form.Items.Item(gs_CmbMndPv).Specific).Select(lo_BsnssPrtnrs.Currency, SAPbouiCOM.BoSearchKey.psk_ByValue);
                                lo_ChrtOfAcct.GetByKey(lo_BsnssPrtnrs.AccountRecivablePayables.AccountCode);
                                go_Combo = po_Form.Items.Item(gs_CmbCtaCt).Specific;
                                while (go_Combo.ValidValues.Count > 0)
                                {
                                    go_Combo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                                }
                                go_Combo.ValidValues.Add(lo_BsnssPrtnrs.AccountRecivablePayables.AccountCode, lo_ChrtOfAcct.FormatCode);
                                go_Combo.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                                if (po_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                    po_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                //((SAPbouiCOM.EditText)po_Form.Items.Item(gs_EdtCtaCt).Specific).Value = lo_BsnssPrtnrs.AccountRecivablePayables.AccountCode;
                                //lo_ChrtOfAcct.GetByKey(lo_BsnssPrtnrs.AccountRecivablePayables.AccountCode);
                                //go_Static = go_Form.Items.Item("lblAcct").Specific;
                                //go_Static.Caption = lo_ChrtOfAcct.Name;
                                //go_Edit = go_Form.Items.Item("txtFmtCode").Specific;
                                //go_Edit.Value = lo_ChrtOfAcct.FormatCode;
                            }
                        }
                        else
                        {
                            lo_CFL = po_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                            lo_CFL.SetConditions(null);
                            lo_Cnds = lo_CFL.GetConditions();
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.Alias = "CardType";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            lo_Cnd.CondVal = "S";
                            lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.Alias = "U_CE_USEAR";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            lo_Cnd.CondVal = "Y";

                            lo_RecSet = Cls_QueriesManager_EAR.fn_VerificarPrvAso();
                            while (!lo_RecSet.EoF)
                            {
                                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                                lo_Cnd = lo_Cnds.Add();
                                lo_Cnd.Alias = "CardCode";
                                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL;
                                lo_Cnd.CondVal = lo_RecSet.Fields.Item(1).Value.Trim();
                                lo_RecSet.MoveNext();
                            }

                            lo_CFL.SetConditions(lo_Cnds);
                        }
                    }
                    if (!lo_CFLEvnt.BeforeAction && lo_CFLEvnt.ChooseFromListUID == gs_CFLPrys)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            try
                            {
                                ((SAPbouiCOM.EditText)po_Form.Items.Item(gs_EdtPrycs).Specific).Value = lo_DataTable.GetValue(0, 0);
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    if (lo_CFLEvnt.ChooseFromListUID == gs_CFLUsrs)
                    {
                        if (!lo_CFLEvnt.BeforeAction)
                        {
                            lo_DataTable = lo_CFLEvnt.SelectedObjects;
                            if (lo_DataTable != null)
                            {
                                try
                                {
                                    ((SAPbouiCOM.Matrix)po_Form.Items.Item(gs_MtxAccesos).Specific).SetCellWithoutValidation(po_ItmEvnt.Row, "clmUser", lo_DataTable.GetValue(5, 0));
                                }
                                catch (Exception)
                                { }
                            }
                        }
                        else
                        {
                            go_Matrix = po_Form.Items.Item(gs_MtxAccesos).Specific;
                            lo_CFL = po_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                            lo_CFL.SetConditions(null);
                            lo_Cnds = lo_CFL.GetConditions();
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.Alias = "Locked";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            lo_Cnd.CondVal = "N";
                            for (int i = 1; i <= go_Matrix.RowCount; i++)
                            {
                                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                                lo_Cnd = lo_Cnds.Add();
                                lo_Cnd.Alias = "USER_CODE";
                                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL;
                                lo_Cnd.CondVal = ((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific("clmUser", i)).Value.Trim();                               
                            }
                            lo_CFL.SetConditions(lo_Cnds);
                        }
                    }
                    break;
            } 
        }

        private void sb_OcultarColumnsMatrixSC(bool pb_Flag, SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Matrix lo_MtxAux = null;

            po_Form.Freeze(true);
            go_Matrix = po_Form.Items.Item("39").Specific;
            foreach (SAPbouiCOM.Column lo_Clmn in go_Matrix.Columns)
            {
                try
                {
                    if (lo_Clmn.UniqueID == "0" || lo_Clmn.UniqueID == "1" || lo_Clmn.UniqueID == "540002066" || lo_Clmn.UniqueID == "1470002090" || lo_Clmn.UniqueID.Contains("U_")) continue;
                    lo_Clmn.Visible = pb_Flag;
                }
                catch (Exception ex)
                {

                }
            }

            lo_MtxAux = po_Form.Items.Item("38").Specific;
            foreach (SAPbouiCOM.Column lo_Clmn in lo_MtxAux.Columns)
            {
                try
                {
                    if (lo_Clmn.UniqueID == "0" || lo_Clmn.UniqueID == "1" || lo_Clmn.UniqueID == "3" || lo_Clmn.UniqueID == "1470002179" || lo_Clmn.UniqueID == "540002123" || lo_Clmn.UniqueID == "1470002090" || lo_Clmn.UniqueID.Contains("U_")) continue;
                    lo_Clmn.Visible = pb_Flag;
                }
                catch (Exception ex)
                {
                    
                }
            }

            po_Form.Freeze(false);
        }

        private void sb_VisualizarModificacionesSC(SAPbouiCOM.Form po_FrmAux)
        {
            po_FrmAux.Items.Item(gs_SttMndEAR).Visible = true;
            po_FrmAux.Items.Item(gs_CmbMndEAR).Visible = true;
            po_FrmAux.Items.Item(gs_EdtTotEAR).Visible = true;
            po_FrmAux.Items.Item("29").Visible = false;
            //sb_OcultarColumnsMatrixSC(false, po_FrmAux);
        }

        public bool fn_HandleFormDataEvent(SAPbouiCOM.BusinessObjectInfo po_BsnssObjInf)
        {
            bool lb_Result = true;
            SAPbouiCOM.Form lo_Form = null;
            SAPbobsCOM.Payments lo_Pgo = null;
            SAPbobsCOM.JournalEntries lo_AsPgo = null;
            string ls_TpoRnd = string.Empty;
            string ls_CdgRnd = string.Empty;
            string ls_NroRnd = string.Empty;
            string ls_XMLPgo = string.Empty;
            string ls_TpoPgo = string.Empty;
            int li_PgoTpo = 0;

            switch (po_BsnssObjInf.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD:
                    if (!po_BsnssObjInf.BeforeAction)
                    {
                        if (po_BsnssObjInf.ActionSuccess && po_BsnssObjInf.FormTypeEx == gs_NomFormSolicCompra)
                        {
                           this.sb_VisualizarModificacionesSC(go_SBOApplication.Forms.Item(po_BsnssObjInf.FormUID));
                        }
                        if (po_BsnssObjInf.ActionSuccess && po_BsnssObjInf.FormTypeEx == gs_NomFormMaestroEmpleados)
                        {
                            this.sb_LoadDataFromDataSource(go_SBOApplication.Forms.Item(po_BsnssObjInf.FormUID));
                            foreach (SAPbouiCOM.Item lo_Item in go_SBOApplication.Forms.Item(po_BsnssObjInf.FormUID).Items)
                            {
                                if (lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX) this.sb_EnableDisableItemsByCheck(go_SBOApplication.Forms.Item(po_BsnssObjInf.FormUID), lo_Item.UniqueID);
                            }
                        }
                    }
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE:                  
                    if (po_BsnssObjInf.ActionSuccess && po_BsnssObjInf.BeforeAction != true)
                    {
                        if (po_BsnssObjInf.FormTypeEx == gs_NomFormMaestroEmpleados)
                        {
                            lo_Form = go_SBOApplication.Forms.Item(po_BsnssObjInf.FormUID);
                            ((SAPbouiCOM.Matrix)lo_Form.Items.Item(gs_MtxAccesos).Specific).FlushToDataSource();
                            Cls_EAR_Crear_Accesos_BL.fn_AddDataFromDataSourceToAccesTable(lo_Form.DataSources.DBDataSources);
                        }
                        if(po_BsnssObjInf.FormTypeEx == gs_NomFormPagoEfectuado|| po_BsnssObjInf.FormTypeEx == gs_NomFormPagoRecibido)
                        {
                            if(po_BsnssObjInf.ActionSuccess && po_BsnssObjInf.BeforeAction != true)
                            {
                                try
                                {
                                    if (po_BsnssObjInf.FormTypeEx == gs_NomFormPagoEfectuado)
                                    {
                                        lo_Pgo = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
                                        li_PgoTpo = 46;
                                    }
                                    else
                                    {
                                        lo_Pgo = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                                        li_PgoTpo = 24;
                                    }
                                    ls_XMLPgo = po_BsnssObjInf.ObjectKey;
                                    if (lo_Pgo.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<DocEntry>") + 10, ls_XMLPgo.IndexOf("</DocEntry>") - ls_XMLPgo.IndexOf("<DocEntry>") - 10))))
                                    {
                                        if (lo_Pgo.Cancelled == SAPbobsCOM.BoYesNoEnum.tYES)
                                        {
                                            lo_AsPgo = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                                            if (lo_AsPgo.GetByKey(Cls_QueriesManager_EAR.fn_ObtenerAsientoCompensacion(li_PgoTpo, lo_Pgo.DocEntry.ToString())))
                                                if(lo_AsPgo.Cancel() != 0)
                                                    go_SBOApplication.StatusBar.SetText(go_SBOCompany.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                                else
                                                    go_SBOApplication.StatusBar.SetText("Asiento EAR relacionado al pago cancelado correctamente...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);     
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                }
                            }
                        }
                    }
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                    if (po_BsnssObjInf.ActionSuccess && po_BsnssObjInf.BeforeAction != true)
                    {
                        if (po_BsnssObjInf.FormTypeEx == gs_NomFormPagoEfectuado || po_BsnssObjInf.FormTypeEx == gs_NomFormPagoRecibido)
                        { 
                            lo_Form = go_SBOApplication.Forms.Item(po_BsnssObjInf.FormUID);
                            if (po_BsnssObjInf.FormTypeEx == gs_NomFormPagoEfectuado)
                            {
                                lo_Pgo = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oVendorPayments);
                                ls_TpoPgo = "PE";
                            }
                            else
                            {
                                lo_Pgo = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                                ls_TpoPgo = "PR";
                            }
                            lo_AsPgo = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);
                            ls_TpoRnd = lo_Form.DataSources.DBDataSources.Item(lo_Form.DataSources.DBDataSources.Item(0).TableName).GetValue("U_BPP_TIPR", 0).Trim();
                            ls_CdgRnd = lo_Form.DataSources.DBDataSources.Item(lo_Form.DataSources.DBDataSources.Item(0).TableName).GetValue("U_BPP_CCHI", 0).Trim();
                            ls_NroRnd = lo_Form.DataSources.DBDataSources.Item(lo_Form.DataSources.DBDataSources.Item(0).TableName).GetValue("U_BPP_NUMC", 0).Trim();
                            if(ls_TpoRnd == "EAR" && ls_CdgRnd != string.Empty && ls_NroRnd != string.Empty)
                            {
                                ls_XMLPgo = po_BsnssObjInf.ObjectKey;
                                if (lo_Pgo.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<DocEntry>") + 10, ls_XMLPgo.IndexOf("</DocEntry>") - ls_XMLPgo.IndexOf("<DocEntry>") - 10))))
                                {
                                    try
                                    {
                                        ls_XMLPgo = lo_Pgo.GetAsXML();
                                        if (lo_AsPgo.GetByKey(Convert.ToInt32(ls_XMLPgo.Substring(ls_XMLPgo.IndexOf("<TransId>") + 9, ls_XMLPgo.IndexOf("</TransId>") - ls_XMLPgo.IndexOf("<TransId>") - 9))))
                                            Cls_EAR_Cargar_BL.fn_GenerarAsientodeCompensacion(ls_TpoPgo, lo_AsPgo, ls_CdgRnd, ls_NroRnd, lo_Pgo.DocEntry.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                    }
                                }
                            }
                        }
                        if (po_BsnssObjInf.FormTypeEx == gs_NomFormMaestroEmpleados)
                        {
                            lo_Form = go_SBOApplication.Forms.Item(po_BsnssObjInf.FormUID);
                            ((SAPbouiCOM.Matrix)lo_Form.Items.Item(gs_MtxAccesos).Specific).FlushToDataSource();
                            Cls_EAR_Crear_Accesos_BL.fn_AddDataFromDataSourceToAccesTable(lo_Form.DataSources.DBDataSources);
                        }
                    }
                    break;
            }
            return lb_Result;
        }

        private void sb_AñadirProveedorAsociado(SAPbouiCOM.Form po_Form)
        {
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;
            int li_EmpId;
            if (po_Form.DataSources.DBDataSources.Item("OPRQ").GetValue("U_CE_EAR", 0).ToUpper() == "Y")
            {
                go_Combo = po_Form.Items.Item("3").Specific;
                go_Combo.SelectExclusive("S", SAPbouiCOM.BoSearchKey.psk_ByValue);
                lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
                li_EmpId = Convert.ToInt32(((SAPbouiCOM.EditText)po_Form.Items.Item("1470002187").Specific).Value.Trim());
                lo_EmpInf.GetByKey(li_EmpId);
                go_Matrix = po_Form.Items.Item("39").Specific;
                ((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific("1470002090", 1)).Value = lo_EmpInf.UserFields.Fields.Item("U_CE_PVAS").Value;
            }
        }

        #region DatosMaestrosEmpleado

        private void sb_FormMaestroEmpleadosLoad(SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Item lo_ItmAux = null;

            try
            {
                sb_AddChooseFromListsToForm(po_Form);

                go_Item = po_Form.Items.Add(gs_FldEAR, SAPbouiCOM.BoFormItemTypes.it_FOLDER);
                go_Item.Top = po_Form.Items.Item("147").Top;
                go_Item.Height = po_Form.Items.Item("147").Height;
                go_Item.Width = po_Form.Items.Item("147").Width;
                go_Item.Left = po_Form.Items.Item("147").Left + po_Form.Items.Item("147").Width;

                go_Folder = go_Item.Specific;
                go_Folder.Caption = "Entrega a Rendir";
                go_Folder.GroupWith("147");
                po_Form.PaneLevel = 1;

                go_Item = po_Form.Items.Add(gs_SttSocNg, SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_ItmAux = po_Form.Items.Item("7");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_Static = go_Item.Specific;
                go_Static.Caption = "Proveedor asociado";

                go_Item = po_Form.Items.Add(gs_EdtSocNg, SAPbouiCOM.BoFormItemTypes.it_EDIT);
                lo_ItmAux = po_Form.Items.Item("42");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width + 40, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_Edit = go_Item.Specific;
                go_Edit.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_PVAS");
                go_Edit.ChooseFromListUID = gs_CFLPrvs;
                go_Edit.ChooseFromListAlias = "CardCode";

                go_Item = po_Form.Items.Add("lnkPrv", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON);
                lo_ItmAux = po_Form.Items.Item("42");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, 5, lo_ItmAux.Height, lo_ItmAux.Left - 8, 8, 8);
                go_LinkButton = go_Item.Specific;
                go_LinkButton.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_BusinessPartner;
                go_LinkButton.Item.LinkTo = gs_EdtSocNg;

                go_Item = po_Form.Items.Add(gs_SttNmbPv, SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_ItmAux = po_Form.Items.Item("102");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_Static = go_Item.Specific;
                go_Static.Caption = "Nombre";

                go_Item = po_Form.Items.Add(gs_EdtNmbPv, SAPbouiCOM.BoFormItemTypes.it_EDIT);
                lo_ItmAux = po_Form.Items.Item("113");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width + 40, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_All, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Edit = go_Item.Specific;
                go_Edit.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_PVNM");

                go_Item = po_Form.Items.Add(gs_SttMndPv, SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_ItmAux = po_Form.Items.Item("103");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_Static = go_Item.Specific;
                go_Static.Caption = "Tipo de Moneda";

                go_Item = po_Form.Items.Add(gs_CmbMndPv, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                lo_ItmAux = po_Form.Items.Item("106");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_All, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Item.DisplayDesc = true;
                go_Combo = go_Item.Specific;
                go_Combo.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_PVMN");

                go_Item = po_Form.Items.Add(gs_SttCtaCt, SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_ItmAux = po_Form.Items.Item("1980002104");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top - 2, lo_ItmAux.Width-90, lo_ItmAux.Height, po_Form.Items.Item("103").Left, 8, 8);
                go_Static = go_Item.Specific;
                go_Static.Caption = "Cuenta Contable";

                go_Item = po_Form.Items.Add(gs_CmbCtaCt, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                lo_ItmAux = po_Form.Items.Item("1980002106");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top - 2, po_Form.Items.Item("113").Width, lo_ItmAux.Height, po_Form.Items.Item("106").Left, 8, 8);
                go_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_All, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Item.DisplayDesc = true;
                go_Combo = go_Item.Specific;
                go_Combo.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_CTAS");

                go_Item = po_Form.Items.Add("lnkCta", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON);
                lo_ItmAux = po_Form.Items.Item("1980002106");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, 5, lo_ItmAux.Height, po_Form.Items.Item("106").Left - 8, 8, 8);
                go_LinkButton = go_Item.Specific;
                go_LinkButton.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_GLAccounts;
                go_LinkButton.Item.LinkTo = gs_CmbCtaCt;

                go_Item = po_Form.Items.Add(gs_ChkPrycs, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
                lo_ItmAux = po_Form.Items.Item("109");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width + 30, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_CheckBox = go_Item.Specific;
                go_CheckBox.Caption = "Proyecto por defecto";
                go_CheckBox.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_PRYS");

                go_Item = po_Form.Items.Add(gs_EdtPrycs, SAPbouiCOM.BoFormItemTypes.it_EDIT);
                lo_ItmAux = po_Form.Items.Item("114");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width, lo_ItmAux.Height, lo_ItmAux.Left + 40, 8, 8);
                go_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_All, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Edit = go_Item.Specific;
                go_Edit.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_PRYC");
                go_Edit.ChooseFromListUID = gs_CFLPrys;
                go_Edit.ChooseFromListAlias = "PrjCode";

                go_Item = po_Form.Items.Add(gs_ChkDimns, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
                lo_ItmAux = po_Form.Items.Item("1980002105");
                Cls_Global.sb_AlinearItem(ref go_Item, po_Form.Items.Item("114").Top, lo_ItmAux.Width - 20, lo_ItmAux.Height, lo_ItmAux.Left + 70, 8, 8);
                go_CheckBox = go_Item.Specific;
                go_CheckBox.Caption = "Dimenciones por defecto";
                go_CheckBox.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_DMNS");

                go_Item = po_Form.Items.Add(gs_BtnDimns, SAPbouiCOM.BoFormItemTypes.it_BUTTON);
                lo_ItmAux = po_Form.Items.Item("1980002105");
                Cls_Global.sb_AlinearItem(ref go_Item, po_Form.Items.Item("114").Top, 30, lo_ItmAux.Height, po_Form.Items.Item("79").Left + 60, 8, 8);
                go_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_All, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Button = go_Item.Specific;
                go_Button.Caption = "...";

                go_Item = po_Form.Items.Add(gs_ChkNroRnd, SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
                lo_ItmAux = po_Form.Items.Item("108");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top, lo_ItmAux.Width + 175, lo_ItmAux.Height, lo_ItmAux.Left, 8, 8);
                go_CheckBox = go_Item.Specific;
                go_CheckBox.Caption = "Fijar cantidad de rendiciones por n° de entrega a rendir";
                go_CheckBox.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_RNDS");

                go_Item = po_Form.Items.Add(gs_EdtRndcs, SAPbouiCOM.BoFormItemTypes.it_EDIT);
                lo_ItmAux = po_Form.Items.Item("1980002105");
                Cls_Global.sb_AlinearItem(ref go_Item, po_Form.Items.Item(gs_ChkNroRnd).Top, po_Form.Items.Item("38").Width, lo_ItmAux.Height, lo_ItmAux.Left + 70, 8, 8);
                go_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_All, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Edit = go_Item.Specific;
                go_Edit.DataBind.SetBound(true, po_Form.DataSources.DBDataSources.Item(0).TableName, "U_CE_RNDC");
                
                go_Item = po_Form.Items.Add(gs_SttAcces, SAPbouiCOM.BoFormItemTypes.it_STATIC);
                lo_ItmAux = po_Form.Items.Item("19");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top - 8, lo_ItmAux.Width + 30, lo_ItmAux.Height, po_Form.Items.Item("108").Left, 8, 8);
                go_Static = go_Item.Specific;
                go_Static.Caption = "Accesos por usuario";

                go_Item = po_Form.Items.Add(gs_MtxAccesos, SAPbouiCOM.BoFormItemTypes.it_MATRIX);
                lo_ItmAux = po_Form.Items.Item("22");
                Cls_Global.sb_AlinearItem(ref go_Item, lo_ItmAux.Top - 25, 500, 110, lo_ItmAux.Left, 8, 8);

                sb_DataFormLoad(po_Form);
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void sb_AddChooseFromListsToForm(SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.ChooseFromListCreationParams lo_CFLCrtPrms = null;
            SAPbouiCOM.ChooseFromListCollection lo_CFLCll = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbobsCOM.Recordset lo_RecSet = null;

            lo_CFLCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
            lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            lo_CFLCll = po_Form.ChooseFromLists;
            lo_CFLCrtPrms.UniqueID = gs_CFLPrvs;
            lo_CFLCrtPrms.MultiSelection = false;
            lo_CFLCrtPrms.ObjectType = "2";
            lo_CFL = lo_CFLCll.Add(lo_CFLCrtPrms);

            lo_CFLCrtPrms.UniqueID = gs_CFLUsrs;
            lo_CFLCrtPrms.MultiSelection = false;
            lo_CFLCrtPrms.ObjectType = "12";
            lo_CFLCll.Add(lo_CFLCrtPrms);

            lo_CFLCrtPrms.UniqueID = gs_CFLPrys;
            lo_CFLCrtPrms.MultiSelection = false;
            lo_CFLCrtPrms.ObjectType = "63";
            lo_CFLCll.Add(lo_CFLCrtPrms);
        }

        private void sb_DataFormLoad(SAPbouiCOM.Form po_Form)
        {
            po_Form.DataSources.DBDataSources.Add(gs_DTSDETHEMEAR);
            go_Combo = po_Form.Items.Item(gs_CmbMndPv).Specific;
            Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_MonedasSociedad());
            go_Combo.ValidValues.Add("##", "Todas");
            sb_AddColumnsToEARAccesMatrix(po_Form);
        }

        public void sb_DataFormLoadAdd()
        {
            SAPbouiCOM.Form lo_Form = null;
            lo_Form = go_SBOApplication.Forms.ActiveForm;
            ((SAPbouiCOM.EditText)lo_Form.Items.Item(gs_EdtRndcs).Specific).Value = "1";
            foreach (SAPbouiCOM.Item lo_Item in lo_Form.Items)
            {
                if (lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX) this.sb_EnableDisableItemsByCheck(lo_Form,lo_Item.UniqueID);
            }
            sb_AddNewRowMatrix();
        }

        private void sb_AddColumnsToEARAccesMatrix(SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Column lo_Clm = null;

            go_Matrix = po_Form.Items.Item(gs_MtxAccesos).Specific;

            lo_Clm = go_Matrix.Columns.Add("#", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            lo_Clm.TitleObject.Caption = "#";
            lo_Clm.DataBind.SetBound(true, gs_DTSDETHEMEAR, "U_LineID");
            lo_Clm.Editable = false;

            lo_Clm = go_Matrix.Columns.Add("clmUser", SAPbouiCOM.BoFormItemTypes.it_EDIT);
            lo_Clm.TitleObject.Caption = "Usuario";
            lo_Clm.DataBind.SetBound(true, gs_DTSDETHEMEAR, "U_ER_CDUS");
            lo_Clm.ChooseFromListUID = gs_CFLUsrs;
            lo_Clm.ChooseFromListAlias = "USER_CODE";
            lo_Clm.Width = 100;           

            lo_Clm = go_Matrix.Columns.Add("clmAprt", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
            lo_Clm.TitleObject.Caption = "Aperturar";
            lo_Clm.DataBind.SetBound(true, gs_DTSDETHEMEAR, "U_ER_SAPR");
            lo_Clm.Width = 60;

            lo_Clm = go_Matrix.Columns.Add("clmCntb", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
            lo_Clm.TitleObject.Caption = "Contabilizar Doc";
            lo_Clm.DataBind.SetBound(true, gs_DTSDETHEMEAR, "U_ER_SCNT");
            lo_Clm.Width = 80;

            lo_Clm = go_Matrix.Columns.Add("clmRglr", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
            lo_Clm.TitleObject.Caption = "Regularizar Saldos";
            lo_Clm.DataBind.SetBound(true, gs_DTSDETHEMEAR, "U_ER_SRGL");
            lo_Clm.Width = 80;

            lo_Clm = go_Matrix.Columns.Add("clmCrgr", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
            lo_Clm.TitleObject.Caption = "Cargar Documentos";
            lo_Clm.DataBind.SetBound(true, gs_DTSDETHEMEAR, "U_ER_SCRG");
            lo_Clm.Width = 80;

            lo_Clm = go_Matrix.Columns.Add("clmCrCrg", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX);
            lo_Clm.TitleObject.Caption = "Cerrar Cargar Doc";
            lo_Clm.DataBind.SetBound(true, gs_DTSDETHEMEAR, "U_ER_SCCR");
            lo_Clm.Width = 80;
        }

        public void sb_AddNewRowMatrix()
        {
            SAPbouiCOM.Form lo_Form = null;
            lo_Form = go_SBOApplication.Forms.ActiveForm;

            try
            {
                lo_Form.Freeze(true);
                go_Matrix = lo_Form.Items.Item(gs_MtxAccesos).Specific;
                go_Matrix.AddRow();
                go_Matrix.ClearRowData(go_Matrix.RowCount);
                this.sb_AddIdRowMatrix(lo_Form);
                go_Matrix.FlushToDataSource();
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                lo_Form.Freeze(false);
            }
        }

        public void sb_DeleteRowMatrix()
        {
            SAPbouiCOM.Form lo_Form = null;

            lo_Form = go_SBOApplication.Forms.ActiveForm;
            go_Matrix = lo_Form.Items.Item(gs_MtxAccesos).Specific;
            System.Windows.Forms.DialogResult lo_Resultado;
            if (gi_RightClickRow > 1)
            {
                lo_Resultado = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("¿Desea eliminar esta fila", 1, "Si", "No");
                if (lo_Resultado == System.Windows.Forms.DialogResult.OK)
                {
                    go_Matrix.DeleteRow(gi_RightClickRow);
                    this.sb_AddIdRowMatrix(lo_Form);
                }
            }
        }

        public bool fn_HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo po_RghClkEvent)
        {
            bool lb_Result = true;
            try
            {
                //* * * * * * Fila del Evento * * * * * * * * * * * * * * 
                gi_RightClickRow = po_RghClkEvent.Row;
                //* * * * * * * * * * * * * * * * * * * * * * * * * * * * 
                sb_AddDeleteRowMenu(po_RghClkEvent);
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            return lb_Result;
        }

        private void sb_AddDeleteRowMenu(SAPbouiCOM.ContextMenuInfo po_RghClkEvent)
        {
            SAPbouiCOM.Form lo_Form = null;

            lo_Form = go_SBOApplication.Forms.Item(po_RghClkEvent.FormUID);
            if (po_RghClkEvent.ItemUID != string.Empty)
            {
                if (lo_Form.Items.Item(po_RghClkEvent.ItemUID).Type == SAPbouiCOM.BoFormItemTypes.it_MATRIX)
                {
                    if (po_RghClkEvent.BeforeAction)
                    {
                        SAPbouiCOM.Menus lo_Menus = null;
                        SAPbouiCOM.IMenuItem lo_MnuItm = null;
                        SAPbouiCOM.MenuCreationParams lo_MnuCrtPrms = null;

                        lo_MnuItm = go_SBOApplication.Menus.Item("1280");
                        lo_MnuCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                        lo_Menus = lo_MnuItm.SubMenus;
                        lo_MnuCrtPrms.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                        lo_MnuCrtPrms.UniqueID = gs_MnuAñadirFila;
                        lo_MnuCrtPrms.String = "Añadir linea";
                        lo_MnuCrtPrms.Enabled = true;
                        lo_Menus.AddEx(lo_MnuCrtPrms);
                        lo_MnuCrtPrms = null;
                        if (po_RghClkEvent.Row > 1)
                        {
                            lo_MnuCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                            lo_MnuCrtPrms.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                            lo_MnuCrtPrms.UniqueID = gs_MnuBorrarFila;
                            lo_MnuCrtPrms.String = "Borrar linea";
                            lo_MnuCrtPrms.Enabled = true;
                            lo_Menus.AddEx(lo_MnuCrtPrms);
                            lo_MnuCrtPrms = null;
                        }
                    }
                    else
                    {
                        go_SBOApplication.Menus.RemoveEx(gs_MnuAñadirFila);
                        if (go_SBOApplication.Menus.Exists(gs_MnuBorrarFila))
                        {
                            go_SBOApplication.Menus.RemoveEx(gs_MnuBorrarFila);
                        }
                    }
                }
            }
        }

        private void sb_AddIdRowMatrix(SAPbouiCOM.Form po_Form)
        {
            go_Matrix = po_Form.Items.Item(gs_MtxAccesos).Specific;
            for (int i = 1; i < go_Matrix.RowCount+1; i++)
            {
                ((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific("#", i)).Value = i.ToString();    
            }

        }

        private void sb_LoadDataFromDataSource(SAPbouiCOM.Form po_Form)
        {
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;

            lo_Cnds = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "U_empID";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = po_Form.DataSources.DBDataSources.Item(0).GetValue("empID", 0).Trim();
            po_Form.DataSources.DBDataSources.Item(gs_DTSDETHEMEAR).Query(lo_Cnds);
            go_Matrix = po_Form.Items.Item(gs_MtxAccesos).Specific;
            go_Matrix.LoadFromDataSource();
        }

        private void sb_EnableDisableItemsByCheck(SAPbouiCOM.Form po_Form,string ps_ItemUID)
        {
            switch (ps_ItemUID)
            {
                case gs_ChkDimns:
                    po_Form.Items.Item("38").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_CheckBox = po_Form.Items.Item(gs_ChkDimns).Specific;
                    if (go_CheckBox.Checked == true) po_Form.Items.Item(gs_BtnDimns).Enabled = true;
                    else po_Form.Items.Item(gs_BtnDimns).Enabled = false;
                    break;
                case gs_ChkNroRnd:
                    po_Form.Items.Item("38").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_CheckBox = po_Form.Items.Item(gs_ChkNroRnd).Specific;
                    if (go_CheckBox.Checked == true) po_Form.Items.Item(gs_EdtRndcs).Enabled = true;
                    else po_Form.Items.Item(gs_EdtRndcs).Enabled = false;
                    break;
                case gs_ChkPrycs:
                    po_Form.Items.Item("38").Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_CheckBox = po_Form.Items.Item(gs_ChkPrycs).Specific;
                    if (go_CheckBox.Checked == true) po_Form.Items.Item(gs_EdtPrycs).Enabled = true;
                    else po_Form.Items.Item(gs_EdtPrycs).Enabled = false;
                    break;            
            }
        }

        #endregion
    }
}
