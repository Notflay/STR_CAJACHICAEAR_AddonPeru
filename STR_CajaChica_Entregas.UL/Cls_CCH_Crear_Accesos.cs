using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;
using System.Data;

namespace STR_CajaChica_Entregas.UL
{
    class Cls_CCH_Crear_Accesos : Cls_Global_Controles
    {
        private SAPbouiCOM.Application lo_SBOApplication = null;
        private SAPbobsCOM.Company lo_SBOCompany = null;
        private SAPbouiCOM.Form go_Form = null;
        public const string gs_NomForm = "FormCCH";
        private string ls_RutaForm = "Resources/CajaChicaEAR/CajasChicas.srf";

        //* * * * * * * * * * * * * * * Menus * * * * * * * * * * * * * * * * * * * * * * * * 
        public const string gs_MenuCCH = "MNU_CCH_CAJACHICA";
        public const string gs_MnuAñadirFila = "MNU_AddLineParam";
        public const string gs_MnuBorrarFila = "MNU_DltLineParam";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        // * * * * * * * * * * * * * * DataSources * * * * * * * * * * * * * * * * * * * * * * * 
        private const string gs_DscCCH_EAR = "@BPP_CAJASCHICAS";
        private const string gs_DsdCCH_EAR = "@BPP_CAJASCHICASACC";
        private const string gs_DsdCCH_EAR2 = "@STR_CAJASCHICASDIM";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
        // * * * * * * * * * * * * * * Controles * * * * * * * * * * * * * * * * * * * * * * * *
        //Matrix
        private string gs_MtxAccesos = "mxAccesos";
        //Combobox
        private string gs_CmbTpoRnd = "cboTipoRen";
        private string gs_CmbMoneda = "cboTipm";
        private string gs_CmbEstado = "cboEstados";
        //EditText
        private string gs_EdtCodigo = "txtCodigo";
        private string gs_EdtNombre = "txtNombre";
        private string gs_EdtCuenta = "txtAcct";
        private string gs_EdtFormatCode = "txtFmtCode";
        private string gs_EdtMontoMax = "txtMntMax";
        private string gs_EdtCantRndc = "txtCntRnd";
        private string gs_EdtPryDfc = "txtPryDfc";
        private string gs_EdtFocus = "txtFocus";
        //Static
        private string gs_SttAcctName = "lblAcct";
        //Buttons
        private string gs_BtnDimns = "btnDimns";
        //CheckBox
        private const string gs_ChkDimns = "chkDimns";
        private const string gs_ChkMntMax = "chkMntMax";
        private const string gs_ChkCntRnd = "chkCntRnd";
        private const string gs_ChkSldNeg = "chkSldNeg";
        private const string gs_ChkProyec = "chkPryDfc";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * User Fields - @STR_CAJASCHICAS* * * * * * * * * *
        private string gs_UflCntRnd = "U_STR_RNDC";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * User Fields - @STR_CAJASCHICASDIMG* * * * * * * * * *
        private string gs_UflDetDimNmb = "U_CC_NMBR";
        private string gs_UflDetDimDsc = "U_CC_DSCR";
        private string gs_UflDetDimDft = "U_CC_DFLT";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * Variables de Clase * * * * * * * * * * * * * * * * * * * *
        private int gi_RightClickRow = -1;
        private string gs_TpoFrm = string.Empty;
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        public static DataTable go_DTblDimns = null;

        public Cls_CCH_Crear_Accesos()
        {
            this.lo_SBOApplication = Cls_Global.go_SBOApplication;
            this.lo_SBOCompany = Cls_Global.go_SBOCompany;
        }

        public void sb_FormLoad(string ps_Tipo)
        {
            try
            {
                if (go_Form == null)
                {
                    go_Form = Cls_Global.fn_CreateForm(gs_NomForm, ls_RutaForm);
                    gs_TpoFrm = ps_Tipo;
                    sb_DataFormLoad();
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                lo_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
            finally
            {
                go_Form.Visible = true;
            }
        }

        public void sb_DataFormLoad()
        {
            string ls_Tipo = string.Empty;
            SAPbobsCOM.SBObob lo_SBObob;

            if (go_Form != null)
            {
                try
                {
                    go_Form.Freeze(true);
                    go_DTblDimns = null;
                    lo_SBObob = lo_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);
                    go_Form.DataBrowser.BrowseBy = gs_EdtCodigo;
                    ls_Tipo = (gs_TpoFrm == "CCH") ? "Caja Chica" : "Entregas a Rendir";
                    go_Form.Title = ls_Tipo;
                    go_Combo = go_Form.Items.Item(gs_CmbTpoRnd).Specific;
                    if (gs_TpoFrm == "CCH")
                    {
                        go_Combo.SelectExclusive(1, SAPbouiCOM.BoSearchKey.psk_Index);
                    }
                    else
                    {
                        go_Combo.SelectExclusive(0, SAPbouiCOM.BoSearchKey.psk_Index);
                    }
                    go_Form.DataSources.DBDataSources.Item(gs_DscCCH_EAR).SetValue(gs_UflCntRnd, 0, "1");
                    go_Form.Items.Item(gs_EdtCodigo).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_Form.Items.Item(gs_CmbTpoRnd).Enabled = false;
                    go_Combo = go_Form.Items.Item(gs_CmbMoneda).Specific;
                    Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_MonedasSociedad());
                    go_Combo.Select(lo_SBObob.GetLocalCurrency().Fields.Item(0).Value, SAPbouiCOM.BoSearchKey.psk_ByValue);
                    go_Form.DataSources.DBDataSources.Item(gs_DscCCH_EAR).SetValue("U_BPP_STAD", 0, "A");
                    go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
                    go_Matrix.AddRow();
                    go_Matrix.Columns.Item("clmUSER").Width = 150;
                    go_Matrix.Columns.Item("clmLineId").Visible = false;
                    go_Matrix.Columns.Item("clmGEST").Visible = false;
                    go_Matrix.Columns.Item("clmDEVO").Visible = false;
                    go_Matrix.Columns.Item("clmREGU").Visible = false;
                    go_Form.Items.Item(gs_CmbEstado).Enabled = false;
                    go_Form.Items.Item(gs_EdtCodigo).Enabled = true;
                    go_Form.Items.Item(gs_EdtNombre).Enabled = true;
                    go_Form.Items.Item(gs_EdtCuenta).Enabled = true;
                    foreach (SAPbouiCOM.Item lo_Item in go_Form.Items)
                    {
                        if (lo_Item.Type == SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX) this.sb_EnableDisableItemsByCheck(lo_Item.UniqueID);
                    }
                }
                catch (Exception ex)
                {
                    Cls_Global.WriteToFile(ex.Message);
                    lo_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
                }
                finally
                {
                    go_Form.Freeze(false);
                }
            }
        }

        public bool fn_HandleItemEvent(string FormUID, ref SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            bool lb_Result = true;
            switch (po_ItmEvent.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD:
                    sb_FormUnload();
                    break;
                case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST:
                    lb_Result = sb_HandleChooseFromList(po_ItmEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_LOAD:
                    break;
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    lb_Result = fn_HandleItemPressed(po_ItmEvent);
                    break;
                case SAPbouiCOM.BoEventTypes.et_VALIDATE:
                    lb_Result = fn_HandleValidate(po_ItmEvent);
                    break;
            }
            return lb_Result;
        }

        private bool fn_HandleValidate(SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            bool lb_Result = true;
            if (po_ItmEvent.BeforeAction && po_ItmEvent.ItemUID == gs_EdtCantRndc)
            {
                if (Convert.ToInt32(go_Form.DataSources.DBDataSources.Item(gs_DscCCH_EAR).GetValue("U_STR_RNDC", 0)) < 1)
                {
                    lo_SBOApplication.StatusBar.SetText("Ingrese una cantidad valida...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    lb_Result = false;
                }
            }
            return lb_Result;
        }

        private bool fn_HandleItemPressed(SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            bool lb_Result = true;
            if (po_ItmEvent.ItemUID != string.Empty && go_Form != null)
            {
                switch (go_Form.Items.Item(po_ItmEvent.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                        if (po_ItmEvent.ItemUID == "1")
                        {
                            if (po_ItmEvent.BeforeAction && go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                            {
                                lb_Result = fn_ValidacionesGenerales();
                            }
                            if (po_ItmEvent.BeforeAction && (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE || go_Form.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE))
                            {
                                this.sb_AddDataToDataSource();
                            }
                        }
                        if (po_ItmEvent.ItemUID == gs_BtnDimns)
                        {
                            if (!po_ItmEvent.BeforeAction)
                            {
                                new Cls_CCH_Dimensiones().sb_FormLoad(go_Form.DataSources.DBDataSources.Item(gs_DscCCH_EAR).GetValue("Code", 0).Trim());
                                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                {
                                    go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                }
                            }
                        }
                        break;
                    case SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX:
                        if (!po_ItmEvent.BeforeAction)
                        {
                            this.sb_EnableDisableItemsByCheck(po_ItmEvent.ItemUID);
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

            go_Edit = go_Form.Items.Item(gs_EdtCodigo).Specific;
            if (go_Edit.Value == string.Empty)
            {
                lb_Result = false;
                ls_MsgErr = "Ingrese un codigo para la Caja Chica...";
                go_Edit.Active = true;
                goto fin;
            }
            go_Edit = go_Form.Items.Item(gs_EdtNombre).Specific;
            if (go_Edit.Value == string.Empty)
            {
                lb_Result = false;
                ls_MsgErr = "Ingrese un valor válido para el nombre...";
                go_Edit.Active = true;
                goto fin;
            }
            go_Edit = go_Form.Items.Item(gs_EdtCuenta).Specific;
            if (go_Edit.Value == string.Empty)
            {
                lb_Result = false;
                ls_MsgErr = "Ingrese un valor válido para la cuenta contable...";
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
                    ls_MsgErr = "Fila sin datos de asignacion de accesos...";
                    go_Edit.Active = true;
                    break;
                }
            }
        fin:
            if (!lb_Result)
            {
                lo_SBOApplication.SetStatusBarMessage(ls_MsgErr, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            return lb_Result;
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
                sb_DeleteRemoveMenu(po_RghClkEvent);
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                lo_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            return lb_Result;
        }

        private void sb_DeleteRemoveMenu(SAPbouiCOM.ContextMenuInfo po_RghClkEvent)
        {
            if (po_RghClkEvent.BeforeAction)
            {
                SAPbouiCOM.MenuItem lo_MenuItem = null;
                lo_MenuItem = lo_SBOApplication.Menus.Item("1283");
                lo_MenuItem.Enabled = false;
                lo_MenuItem = null;
                lo_MenuItem = lo_SBOApplication.Menus.Item("1284");
                lo_MenuItem.Enabled = false;
                lo_MenuItem = null;
            }
        }

        private void sb_AddDeleteRowMenu(SAPbouiCOM.ContextMenuInfo po_RghClkEvent)
        {
            if (po_RghClkEvent.ItemUID != string.Empty)
            {
                if (go_Form.Items.Item(po_RghClkEvent.ItemUID).Type == SAPbouiCOM.BoFormItemTypes.it_MATRIX)
                {
                    if (po_RghClkEvent.BeforeAction)
                    {
                        SAPbouiCOM.Menus lo_Menus = null;
                        SAPbouiCOM.IMenuItem lo_MnuItm = null;
                        SAPbouiCOM.MenuCreationParams lo_MnuCrtPrms = null;
                        lo_MnuItm = lo_SBOApplication.Menus.Item("1280");
                        lo_MnuCrtPrms = lo_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                        lo_Menus = lo_MnuItm.SubMenus;
                        lo_MnuCrtPrms.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                        lo_MnuCrtPrms.UniqueID = gs_MnuAñadirFila;
                        lo_MnuCrtPrms.String = "Añadir linea";
                        lo_MnuCrtPrms.Enabled = true;
                        lo_Menus.AddEx(lo_MnuCrtPrms);
                        lo_MnuCrtPrms = null;
                        if (po_RghClkEvent.Row > 1)
                        {
                            lo_MnuCrtPrms = lo_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
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
                        lo_SBOApplication.Menus.RemoveEx(gs_MnuAñadirFila);
                        if (lo_SBOApplication.Menus.Exists(gs_MnuBorrarFila))
                        {
                            lo_SBOApplication.Menus.RemoveEx(gs_MnuBorrarFila);
                        }
                    }
                }
            }
        }

        private bool sb_HandleChooseFromList(SAPbouiCOM.ItemEvent po_ItmEvent)
        {
            string ls_SelectedValue = string.Empty;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.DataTable lo_DtaTbl = null;
            SAPbouiCOM.ChooseFromList lo_ChsFrmLst = null;
            SAPbouiCOM.ChooseFromListEvent lo_ChsFrmLstEvnt = null;
            try
            {
                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE) return true;
                lo_ChsFrmLstEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvent;
                if (po_ItmEvent.BeforeAction)
                {

                    if (lo_ChsFrmLstEvnt.ChooseFromListUID.ToUpper() != "CFLUSER" && lo_ChsFrmLstEvnt.ChooseFromListUID.ToUpper() != "CFLCODPRY")
                    {
                        // Obtener el objeto ChooseFromList basado en el evento
                        lo_ChsFrmLst = go_Form.ChooseFromLists.Item(lo_ChsFrmLstEvnt.ChooseFromListUID);
                        lo_ChsFrmLst.SetConditions(null);
                        lo_Cnds = lo_ChsFrmLst.GetConditions();

                        // Agregar primera condición basada en el valor del combo box
                        lo_Cnd = lo_Cnds.Add();
                        lo_Cnd.Alias = "ActCurr";
                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        go_Combo = go_Form.Items.Item(gs_CmbMoneda).Specific;
                        if (go_Combo.Selected == null)
                        {
                            lo_Cnd.CondVal = "";
                        }
                        else
                        {
                            lo_Cnd.CondVal = go_Combo.Selected.Value;
                        }

                        // Relación OR para la siguiente condición
                        lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                        lo_Cnd = lo_Cnds.Add();
                        lo_Cnd.Alias = "U_CE_ACCT";
                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        lo_Cnd.CondVal = "Y";

                        // Agregar la segunda condición para el valor "##"

                        lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;

                        lo_Cnd = lo_Cnds.Add();
                        lo_Cnd.Alias = "ActCurr";
                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        lo_Cnd.CondVal = "##";

                        // Relación AND para la siguiente condición
                        lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                        // Agregar la tercera condición que siempre debe cumplirse (AND)

                        lo_Cnd = lo_Cnds.Add();
                        lo_Cnd.Alias = "U_CE_ACCT";
                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        lo_Cnd.CondVal = "Y";

                        // Aplicar las condiciones al objeto ChooseFromList
                        lo_ChsFrmLst.SetConditions(lo_Cnds);
                    }
                    if (lo_ChsFrmLstEvnt.ChooseFromListUID.ToUpper() == "CFLUSER")
                    {
                        lo_ChsFrmLst = go_Form.ChooseFromLists.Item(lo_ChsFrmLstEvnt.ChooseFromListUID);
                        go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
                        lo_ChsFrmLst.SetConditions(null);
                        lo_Cnds = lo_ChsFrmLst.GetConditions();
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
                            lo_Cnd.CondVal = ((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific("clmUSER", i)).Value.Trim();
                        }
                        lo_ChsFrmLst.SetConditions(lo_Cnds);
                    }
                }
                else
                {
                    if (go_Form.Mode != SAPbouiCOM.BoFormMode.fm_FIND_MODE)
                    {
                        lo_DtaTbl = lo_ChsFrmLstEvnt.SelectedObjects;
                        if (lo_DtaTbl != null)
                        {
                            if (lo_ChsFrmLstEvnt.ChooseFromListUID.ToUpper() != "CFLUSER")
                            {
                                if (lo_ChsFrmLstEvnt.ChooseFromListUID.ToUpper() == "ACCT")
                                {
                                    string ls_moneda = "";
                                    go_Combo = go_Form.Items.Item(gs_CmbMoneda).Specific;
                                    if (go_Combo.Selected != null)
                                    {
                                        ls_moneda = go_Combo.Selected.Value;
                                    }

                                    string ls_monedaDtb = lo_DtaTbl.GetValue("ActCurr", 0) == "##" ? "SOL" : lo_DtaTbl.GetValue("ActCurr", 0);

                                    if (ls_moneda == ls_monedaDtb)
                                    {
                                        go_Edit = go_Form.Items.Item(gs_EdtCuenta).Specific;
                                        ls_SelectedValue = lo_DtaTbl.GetValue("AcctCode", 0);
                                        go_Form.DataSources.DBDataSources.Item(go_Edit.DataBind.TableName).SetValue(go_Edit.DataBind.Alias, 0, ls_SelectedValue);
                                        go_Static = go_Form.Items.Item(gs_SttAcctName).Specific;
                                        go_Static.Caption = lo_DtaTbl.GetValue("AcctName", 0);
                                        go_Edit = go_Form.Items.Item(gs_EdtFormatCode).Specific;
                                        go_Edit.Value = lo_DtaTbl.GetValue("FormatCode", 0);
                                    }
                                    else {
                                        go_Edit = go_Form.Items.Item(gs_EdtCuenta).Specific;
                                        go_Form.DataSources.DBDataSources.Item(go_Edit.DataBind.TableName).SetValue(go_Edit.DataBind.Alias, 0, null);
   
                                        throw new Exception("Moneda escogida no es igual al de la Cuenta Contable");
                                    }
                                }
                                if (lo_ChsFrmLstEvnt.ChooseFromListUID.ToUpper() == "CFLCODPRY")
                                {
                                    go_Form.DataSources.DBDataSources.Item(gs_DscCCH_EAR).SetValue("U_STR_PRYD", 0, lo_DtaTbl.GetValue("PrjCode", 0));
                                }
                            }
                            else
                            {
                                go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
                                ls_SelectedValue = lo_DtaTbl.GetValue("USER_CODE", 0);
                                go_Matrix.FlushToDataSource();
                                go_Form.DataSources.DBDataSources.Item(gs_DsdCCH_EAR).SetValue("U_BPP_USER", po_ItmEvent.Row - 1, ls_SelectedValue);
                                go_Matrix.LoadFromDataSource();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                lo_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }

            return true;
        }

        public void sb_AddNewRowMatrix()
        {
            try
            {
                go_Form.Freeze(true);
                go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
                go_Matrix.AddRow();
                go_Matrix.ClearRowData(go_Matrix.RowCount);
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                lo_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                go_Form.Freeze(false);
            }
        }

        public void sb_DeleteRowMatrix()
        {
            go_Matrix = go_Form.Items.Item(gs_MtxAccesos).Specific;
            System.Windows.Forms.DialogResult lo_Resultado;
            if (gi_RightClickRow > 1)
            {
                lo_Resultado = (System.Windows.Forms.DialogResult)lo_SBOApplication.MessageBox("¿Desea eliminar esta fila", 1, "Si", "No");
                if (lo_Resultado == System.Windows.Forms.DialogResult.OK)
                {
                    go_Matrix.DeleteRow(gi_RightClickRow);
                }
            }
            go_Form.Update();
        }

        private void sb_FormUnload()
        {
            go_Form = null;
            go_DTblDimns = null;
            Dispose();
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
                    lo_ChrtAcct = lo_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts);
                    ls_SysAcct = go_Form.DataSources.DBDataSources.Item(gs_DscCCH_EAR).GetValue("U_BPP_ACCT", 0).Trim();
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
                            if (lo_Item.UniqueID != gs_EdtFocus && lo_Item.UniqueID != gs_EdtCantRndc && lo_Item.UniqueID != gs_EdtPryDfc && lo_Item.UniqueID != gs_EdtMontoMax)
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
                Cls_Global.WriteToFile(ex.Message);
                lo_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                lo_ChrtAcct = null;
            }
            return true;
        }

        private void sb_AddDataToDataSource()
        {
            if (go_DTblDimns != null)
            {
                SAPbouiCOM.DBDataSource lo_DBDtsDims = null;
                lo_DBDtsDims = go_Form.DataSources.DBDataSources.Item(gs_DsdCCH_EAR2);
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
                case gs_ChkMntMax:
                    go_Form.Items.Item(gs_EdtFocus).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_CheckBox = go_Form.Items.Item(gs_ChkMntMax).Specific;
                    if (go_CheckBox.Checked == true) go_Form.Items.Item(gs_EdtMontoMax).Enabled = true;
                    else go_Form.Items.Item(gs_EdtMontoMax).Enabled = false;
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

        public void sb_EnableItemsByFindMode()
        {
            go_Form.Items.Item(gs_CmbEstado).Enabled = true;
            go_Form.Items.Item(gs_EdtCodigo).Enabled = true;
            go_Form.Items.Item(gs_EdtNombre).Enabled = true;
        }
    }
}
