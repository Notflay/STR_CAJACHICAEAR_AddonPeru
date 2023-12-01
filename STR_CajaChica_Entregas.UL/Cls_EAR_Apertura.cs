using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;
using STR_CajaChica_Entregas.BL;
using System.Xml;


namespace STR_CajaChica_Entregas.UL
{
    class Cls_EAR_Apertura : Cls_Global_Controles
    {
        private readonly SAPbobsCOM.Company go_SBOCompany = null;
        private readonly SAPbouiCOM.Application go_SBOApplication = null;
        //Datos Form
        //Nombre unico del formulario
        public const string gs_NomForm = "FrmAprEAR";
        //Ruta del Formulario
        private readonly string gs_RutaForm = "Resources/CajaChicaEAR/FrmAperturaEAR.srf";
        //* * * * * * * * * * * * * * * Menus* * * * * * * * * * * * * * * * * 
        public const string gs_MnuAprEAR = "MNU_EAR_APERTURAR";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * DataSources* * * * * * * * * * * * * * *
        private const string gs_DtcEARAPR = "@STR_EARAPR";
        private const string gs_DtdEARAPRDET = "@STR_EARAPRDET";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * User Fields - @STR_EARAPR * * * * * * * * 
        //private string gs_UflCodScNg = "U_CC_CDSN";
        //private string gs_UflComents = "U_CC_CMNT";
        private string gs_UflMdioPgo = "U_ER_MDPG";
        private string gs_UflFchCntb = "U_ER_FCHC";
        private string gs_UflFchVenc = "U_ER_FCHV";
        private string gs_UflFchDcmn = "U_ER_FCHD";
        private string gs_UflEARMnda = "U_ER_MNDA";
        private string gs_UflChqBnco = "U_ER_CHBN";
        //private string gs_UflCshFlw = "U_CC_CSHF";
        //private string gs_UflTpoAprt = "U_CC_TPAP";
        //private string gs_UflMndCaja = "U_CC_MNDA";
        private string gs_UflCtaCnt = "U_ER_CTBN";
        private string gs_UflMPSUNAT = "U_ER_MPSN";
        //private string gs_UflMdoPgo = "U_CC_MDPG";
        private string gs_UflChqFchVnc = "U_ER_CHFV";
        //private string gs_UflChqBnc = "U_CC_CHBN";
        private string gs_UflChqNum = "U_ER_CHNM";
        private string gs_UflChqMnl = "U_ER_CHMN";
        //private string gs_UflChqMPg = "U_CC_CHMP";
        private string gs_UflFchTrn = "U_ER_TBFC";
        //private string gs_UflMntAprCCH = "U_CC_MNTT";
        //private string gs_UflMntTotTrs = "U_CC_MNTR";
        //private string gs_UflTotAprCCH = "U_CC_MNAP";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        ////* * * * * * * * * * * * * * User Fields - @STR_EARAPRDET * * * * * * * * * *
        private string gs_UflDetSelecc = "U_ER_SLCC";
        private string gs_UflDetEARCod = "U_ER_EARN";
        private string gs_UflDetEARDsc = "U_ER_DSCP";
        private string gs_UflDetCtaSys = "U_ER_CDCT";
        private string gs_UflDetCtaNmb = "U_ER_NMCT";
        private string gs_UflDetCtaDsc = "U_ER_DSCT";
        private string gs_UflDetNmrEAR = "U_ER_NMER";
        private string gs_UflDetEARCmt = "U_ER_CMNT";
        private string gs_UflDetCdgSlc = "U_ER_DESL";
        private string gs_UflDetNroSlc = "U_ER_NRSL";
        private string gs_UflDetEARMnt = "U_ER_MNTO";
        private string gs_UflDetMntApr = "U_ER_MNAP";
        private string gs_UflDetMdoPgo = "U_ER_MDPG";
        private string gs_UflDetEstdo = "U_ER_STDO";
        private string gs_UflDetPryct = "U_ER_PRYC";
        private string gs_UflDetDimn1 = "U_ER_DIM1";
        private string gs_UflDetDimn2 = "U_ER_DIM2";
        private string gs_UflDetDimn3 = "U_ER_DIM3";
        private string gs_UflDetDimn4 = "U_ER_DIM4";
        private string gs_UflDetDimn5 = "U_ER_DIM5";
        ////* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //Controles 
        //Form
        private SAPbouiCOM.Form go_Form = null;
        //ComboBox
        private readonly string gs_CmbTpoRnd = "cmbTpRnd";
        private readonly string gs_CmbTpoMnd = "cmbTpMnd";
        private readonly string gs_CmbChsFlw = "cmbCshFlw";
        private readonly string gs_CmbSeries = "cmbSerie";
        private readonly string gs_CmbChqBnc = "cmbChqBnk";
        private readonly string gs_CmbChqCta = "cmbChqCnta";
        //Folders
        private const string gs_FldCheque = "fldCheque";
        private const string gs_FldTransf = "fldTransf";
        private const string gs_FldSinPgo = "fldSinPago";
        private const string gs_FldEfectv = "fldEfec";
        //Matrix
        private readonly string gs_MtxEAR = "mtxEAR";
        //Columnas Matrix
        private readonly string gs_ClmMtxSelecc = "clmSlc";
        private readonly string gs_ClmMtxDEnCta = "clmDcEnSlc";
        private readonly string gs_ClmMtxDEnSlc = "clmCodCnt";
        //ChooseFromList
        private readonly string gs_CFLTrnsCta = "CFLTRNCTA";
        private readonly string gs_CFLEfctCta = "CFLEFCCTA";
        //EditText
        private readonly string gs_EdtDocNumm = "txtDocNum";
        private readonly string gs_EdtFchCntb = "txtFchCnt";
        private readonly string gs_EdtFchVncm = "txtFchVnc";
        private readonly string gs_EdtFchDcmn = "txtFchDcm";
        private readonly string gs_EdtDocEntr = "txtDocEnt";
        private readonly string gs_EdtTrnsCta = "txtTrnCta";
        private readonly string gs_EdtEfctCta = "txtEfcCta";
        private readonly string gs_EdtChqNmro = "txtChqNum";
        private readonly string gs_EdtChqFchV = "txtChqFchV";
        //Buttons
        private readonly string gs_BtnAdd = "1";
        //CheckBox
        private readonly string gs_ChkChqManual = "chkChqMnl";
        //Variables Globales
        public Cls_EAR_Apertura()
        {
            this.go_SBOCompany = Cls_Global.go_SBOCompany;
            this.go_SBOApplication = Cls_Global.go_SBOApplication;
        }

        public void sb_FormLoad()
        {
            try
            {
                if (go_Form == null)
                {
                    this.go_Form = Cls_Global.fn_CreateForm(Cls_EAR_Apertura.gs_NomForm, this.gs_RutaForm);
                    this.sb_DataFormLoad();
                    this.sb_DataFormLoadAdd();
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                go_Form.Visible = true;
            }
        }

        private void sb_DataFormLoad()
        {
            SAPbobsCOM.DimensionsService lo_DmnsSrv = null;
            SAPbobsCOM.CompanyService lo_CmpSrv = null;
            SAPbobsCOM.Dimension lo_Dim = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            try
            {
                lo_CmpSrv = go_SBOCompany.GetCompanyService();
                lo_DmnsSrv = lo_CmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.DimensionsService);
                go_Matrix = go_Form.Items.Item(gs_MtxEAR).Specific;
                go_Form.DataBrowser.BrowseBy = gs_EdtDocEntr;
                go_Form.PaneLevel = 1;
                foreach (var lo_Item in lo_DmnsSrv.GetDimensionList())
                {
                    lo_Dim = lo_DmnsSrv.GetDimension((SAPbobsCOM.DimensionParams)lo_Item);
                    if (lo_Dim.IsActive == SAPbobsCOM.BoYesNoEnum.tYES)
                    {
                        go_Matrix.Columns.Item("clmDim" + lo_Dim.DimensionCode).TitleObject.Caption = lo_Dim.DimensionDescription;
                        lo_Cnds = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
                        lo_Cnd = lo_Cnds.Add();
                        lo_Cnd.Alias = "DimCode";
                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        lo_Cnd.CondVal = lo_Dim.DimensionCode.ToString();
                        go_Form.ChooseFromLists.Item("CFLDIM" + lo_Dim.DimensionCode).SetConditions(lo_Cnds);
                    }
                    else
                    {
                        go_Matrix.Columns.Item("clmDim" + lo_Dim.DimensionCode).Visible = false;
                    }
                }
                lo_Cnds = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "Postable";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "Y";
                go_Form.ChooseFromLists.Item(gs_CFLTrnsCta).SetConditions(lo_Cnds);
                go_Form.ChooseFromLists.Item(gs_CFLEfctCta).SetConditions(lo_Cnds);
                go_Combo = go_Form.Items.Item(gs_CmbTpoRnd).Specific;
                go_Combo.Select("EAR", SAPbouiCOM.BoSearchKey.psk_ByValue);
                go_Combo = go_Form.Items.Item(gs_CmbTpoMnd).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_MonedasSociedad());
                go_Combo = go_Form.Items.Item(gs_CmbChsFlw).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_ListaFlujodeCaja());
                go_Combo.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                sb_SetAutoManageItemsForm();
            }
        }

        public void sb_DataFormLoadAdd()
        {
            string ls_Serie = string.Empty;

            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflFchCntb, 0, DateTime.Now.ToString("yyyyMMdd"));
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflFchVenc, 0, DateTime.Now.ToString("yyyyMMdd"));
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflFchDcmn, 0, DateTime.Now.ToString("yyyyMMdd"));
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflChqFchVnc, 0, DateTime.Now.ToString("yyyyMMdd"));
            ((SAPbouiCOM.Folder)go_Form.Items.Item(gs_FldCheque).Specific).Select();
            go_Combo = go_Form.Items.Item(gs_CmbSeries).Specific;
            go_Combo.ValidValues.LoadSeries(go_Form.BusinessObject.Type, SAPbouiCOM.BoSeriesMode.sf_Add);
            if (go_Combo.Selected == null && go_Combo.ValidValues.Count > 0) ls_Serie = go_Combo.ValidValues.Item(0).Value;
            else ls_Serie = go_Combo.Selected.Value;
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue("Series", 0, ls_Serie);
            this.sb_GetNextDocumentNumber();
        }

        private void sb_GetNextDocumentNumber()
        {
            string ls_Serie = string.Empty;
            ls_Serie = go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).GetValue("Series", 0);
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue("DocNum", 0, go_Form.BusinessObject.GetNextSerialNumber(ls_Serie, go_Form.BusinessObject.Type).ToString());
        }

        private void sb_SetAutoManageItemsForm()
        {
            foreach (SAPbouiCOM.Item lo_Item in go_Form.Items)
            {
                if (lo_Item.Type != SAPbouiCOM.BoFormItemTypes.it_BUTTON && lo_Item.Type != SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
                {
                    //En modo OK 
                    lo_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                    //En modo Find
                    if (lo_Item.UniqueID != gs_CmbTpoMnd && lo_Item.UniqueID != gs_EdtDocNumm && lo_Item.UniqueID != gs_EdtFchCntb && lo_Item.UniqueID != gs_EdtFchVncm && lo_Item.UniqueID != gs_EdtFchDcmn)
                    {
                        lo_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                    }
                }
            }
        }

        private void sb_LoadDataMatrix(string ps_Mnd)
        {
            string ls_Qry = string.Empty;
            SAPbobsCOM.Recordset lo_RecSet = null;
            int li_Linea = 0;

            lo_RecSet = Cls_QueriesManager_EAR.fn_ListarSolicitudesEAR(ps_Mnd, go_SBOCompany.UserName.Trim());
            go_Matrix = go_Form.Items.Item(gs_MtxEAR).Specific;
            go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).Clear();
            while (!lo_RecSet.EoF)
            {
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).InsertRecord(li_Linea);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetEARCod, li_Linea, lo_RecSet.Fields.Item(0).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetEARDsc, li_Linea, lo_RecSet.Fields.Item(2).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetCtaSys, li_Linea, lo_RecSet.Fields.Item(3).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetCtaNmb, li_Linea, lo_RecSet.Fields.Item(4).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetCtaDsc, li_Linea, lo_RecSet.Fields.Item(5).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetMntApr, li_Linea, lo_RecSet.Fields.Item(6).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetEARMnt, li_Linea, lo_RecSet.Fields.Item(6).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetEARCmt, li_Linea, lo_RecSet.Fields.Item(7).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetCdgSlc, li_Linea, lo_RecSet.Fields.Item(8).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetNroSlc, li_Linea, lo_RecSet.Fields.Item(9).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetPryct, li_Linea, lo_RecSet.Fields.Item(10).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn2, li_Linea, lo_RecSet.Fields.Item(11).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn3, li_Linea, lo_RecSet.Fields.Item(12).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn4, li_Linea, lo_RecSet.Fields.Item(13).Value);
                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn5, li_Linea, lo_RecSet.Fields.Item(14).Value);
                lo_RecSet.MoveNext();
            }
            go_Matrix.LoadFromDataSource();
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
                    lb_Result = this.fn_HandleChooseFromList(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    lb_Result = this.fn_HandleItemPressed(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                    lb_Result = this.fn_HandleComboSelect(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                    sb_FixWidthColumn(po_ItmEvnt);
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

        private void sb_FixWidthColumn(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            if (!po_ItmEvnt.BeforeAction && go_Form != null)
            {
                go_Matrix = go_Form.Items.Item(gs_MtxEAR).Specific;
                go_Matrix.Columns.Item(gs_ClmMtxDEnCta).Width = 15;
                go_Matrix.Columns.Item(gs_ClmMtxDEnSlc).Width = 15;
            }
        }

        private bool fn_HandleComboSelect(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            string ls_CdgBnc = string.Empty;
            string ls_EARMnd = string.Empty;


            if (po_ItmEvnt.ItemUID == gs_CmbTpoMnd && !po_ItmEvnt.BeforeAction && go_Form.Mode != SAPbouiCOM.BoFormMode.fm_FIND_MODE)
            {
                go_Combo = go_Form.Items.Item(gs_CmbChqBnc).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_EAR.fn_ListadeBancos());
                go_Combo = go_Form.Items.Item(gs_CmbTpoMnd).Specific;
                this.sb_LoadDataMatrix(go_Combo.Value.Trim());
            }
            if (po_ItmEvnt.ItemUID == gs_CmbChqBnc && !po_ItmEvnt.BeforeAction)
            {
                go_Combo = go_Form.Items.Item(gs_CmbChqCta).Specific;
                ls_CdgBnc = go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).GetValue(gs_UflChqBnco, 0).Trim();
                ls_EARMnd = go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).GetValue(gs_UflEARMnda, 0).Trim();
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_EAR.fn_CuentasdeBanco(ls_CdgBnc, ls_EARMnd));
            }
            return lb_Result;
        }

        private bool fn_HandleChooseFromList(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            SAPbouiCOM.DataTable lo_DataTable = null;
            SAPbouiCOM.ChooseFromListEvent lo_CFLEvnt = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;

            lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
            go_Matrix = go_Form.Items.Item(gs_MtxEAR).Specific;
            if (!lo_CFLEvnt.BeforeAction)
            {
                if (po_ItmEvnt.ItemUID == gs_MtxEAR)
                {
                    lo_DataTable = lo_CFLEvnt.SelectedObjects;
                    if (lo_DataTable != null)
                    {
                        go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(go_Matrix.Columns.Item(po_ItmEvnt.ColUID).DataBind.Alias, po_ItmEvnt.Row - 1, lo_DataTable.GetValue(0, 0));
                    }
                    go_Matrix.LoadFromDataSource();
                }
                else if (po_ItmEvnt.ItemUID == gs_EdtTrnsCta)
                {
                    lo_DataTable = lo_CFLEvnt.SelectedObjects;
                    if (lo_DataTable != null)
                    {
                        go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtTrnsCta).Specific).DataBind.Alias, 0, lo_DataTable.GetValue(0, 0));
                    }
                }
                else if (po_ItmEvnt.ItemUID == gs_EdtEfctCta)
                {
                    lo_DataTable = lo_CFLEvnt.SelectedObjects;
                    if (lo_DataTable != null)
                    {
                        go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtEfctCta).Specific).DataBind.Alias, 0, lo_DataTable.GetValue(0, 0));
                    }
                }
            }

            return lb_Result;
        }

        private bool fn_HandleItemPressed(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            int li_CodErr = 0;
            string ls_MsgErr = string.Empty;
            System.Windows.Forms.DialogResult lo_DlgRsl;

            switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
            {
                case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                    if (!po_ItmEvnt.BeforeAction)
                    {
                        if (po_ItmEvnt.ColUID == gs_ClmMtxSelecc)
                        {
                            lb_Result = fn_GenerarNumeroEAR(po_ItmEvnt);
                        }
                    }
                    else
                    {
                        if (po_ItmEvnt.ColUID == gs_ClmMtxSelecc && po_ItmEvnt.Row > 0 && po_ItmEvnt.Row < ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxEAR).Specific).RowCount + 1)
                        {
                            try
                            {
                                go_Form.Freeze(true);
                                ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxEAR).Specific).FlushToDataSource();
                                for (int i = 0; i < go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).Size; i++)
                                {
                                    if (i != po_ItmEvnt.Row - 1)
                                    {
                                        sb_ClearRowData(i);
                                    }
                                }
                                ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxEAR).Specific).LoadFromDataSource();
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
                    }
                    break;
                case SAPbouiCOM.BoFormItemTypes.it_FOLDER:
                    if (!po_ItmEvnt.BeforeAction)
                    {
                        sb_SetPaneLevel(po_ItmEvnt.ItemUID);
                    }
                    break;
                case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                    if (po_ItmEvnt.BeforeAction && po_ItmEvnt.ItemUID == gs_BtnAdd && go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                    {
                        lb_Result = this.sb_ValidacionesGenerales();
                        if (!lb_Result) return lb_Result;
                        lo_DlgRsl = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("Se procedera a realizar la apertura de la entrega a rendir seleccionada. \n ¿Desea continuar?", 1, "Si", "No");
                        if (lo_DlgRsl == System.Windows.Forms.DialogResult.OK)
                        {
                            this.sb_QuitarFilasNoSeleccionadas();
                            Cls_EAR_Apertura_BL.fn_GenerarPagoEfectuado(go_Form.DataSources.DBDataSources, ref li_CodErr, ref ls_MsgErr);
                            if (li_CodErr != 0 && ls_MsgErr != string.Empty)
                            {
                                go_SBOApplication.StatusBar.SetText(li_CodErr + " - " + ls_MsgErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                lb_Result = false;
                            }
                        }
                        else
                        {
                            lb_Result = false;
                        }
                    }
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
            }
            return lb_Result;
        }

        private void sb_QuitarFilasNoSeleccionadas()
        {
            bool lb_Flag = true;

            go_Matrix = go_Form.Items.Item(gs_MtxEAR).Specific;
            while (true)
            {
                lb_Flag = false;
                for (int i = 1; i < go_Matrix.RowCount + 1; i++)
                {
                    go_CheckBox = go_Matrix.GetCellSpecific(gs_ClmMtxSelecc, i);
                    if (!go_CheckBox.Checked)
                    {
                        go_Matrix.DeleteRow(i);
                        lb_Flag = true;
                        break;
                    }
                }
                if (!lb_Flag) break;
            }
            go_Matrix.FlushToDataSource();
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflMdioPgo, 0, go_Form.PaneLevel.ToString());
        }

        private void sb_SetPaneLevel(string ps_FolderID)
        {
            switch (ps_FolderID)
            {
                case gs_FldCheque:
                    go_Form.PaneLevel = 1;
                    sb_ClearDataForm();
                    break;
                case gs_FldTransf:
                    go_Form.PaneLevel = 2;
                    sb_ClearDataForm();
                    break;
                case gs_FldSinPgo:
                    go_Form.PaneLevel = 4;
                    sb_ClearDataForm();
                    break;
                case gs_FldEfectv:
                    go_Form.PaneLevel = 3;
                    sb_ClearDataForm();
                    break;
            }
        }

        private bool fn_GenerarNumeroEAR(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            SAPbobsCOM.DimensionsService lo_DmnsSrv = null;
            SAPbobsCOM.EmployeesInfo lo_EmpInf = null;
            SAPbobsCOM.CompanyService lo_CmpSrv = null;

            string ls_CdgEAR = string.Empty;
            string ls_NmrEAR = string.Empty;
            int li_CdgErr = 0;
            string ls_MsgErr = string.Empty;
            string ls_DimNmb = string.Empty;
            string ls_CdgEmp = string.Empty;

            try
            {
                if (po_ItmEvnt.Row > 0 && po_ItmEvnt.Row - 1 < go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).Size)
                {
                    go_Form.Freeze(true);
                    go_Matrix = go_Form.Items.Item(gs_MtxEAR).Specific;
                    go_Matrix.FlushToDataSource();
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetSelecc, po_ItmEvnt.Row - 1).Trim() == "Y")
                    {
                        lo_CmpSrv = go_SBOCompany.GetCompanyService();
                        lo_DmnsSrv = lo_CmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.DimensionsService);
                        ls_CdgEAR = go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetEARCod, po_ItmEvnt.Row - 1).Trim();
                        if (go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetSelecc, po_ItmEvnt.Row - 1) == "Y")
                            ls_NmrEAR = Cls_QueriesManager_EAR.fn_GenerarCodigoEAR(ls_CdgEAR);
                        go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetNmrEAR, po_ItmEvnt.Row - 1, ls_NmrEAR);
                        go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetEstdo, po_ItmEvnt.Row - 1, "A");
                        ls_CdgEmp = go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).GetValue(gs_UflDetEARCod, po_ItmEvnt.Row - 1);
                        ls_CdgEmp = ls_CdgEmp.Substring(3, ls_CdgEmp.Length - 3).Trim();
                        lo_EmpInf = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oEmployeesInfo);
                        lo_EmpInf.GetByKey(Convert.ToInt32(ls_CdgEmp));

                        var trim = lo_EmpInf.UserFields.Fields.Item("U_CE_PRYS").Value.Trim();

                        if (lo_EmpInf.UserFields.Fields.Item("U_CE_PRYS").Value.Trim() == "Y")
                        {
                            go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetPryct, po_ItmEvnt.Row - 1, lo_EmpInf.UserFields.Fields.Item("U_CE_PRYC").Value.Trim());
                        }
                        //else
                        //{
                        //    go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetPryct, po_ItmEvnt.Row - 1, string.Empty);
                        //}
                        if (lo_EmpInf.UserFields.Fields.Item("U_CE_DMNS").Value.Trim() == "Y")
                        {
                            foreach (SAPbobsCOM.DimensionParams lo_DimPrm in lo_DmnsSrv.GetDimensionList())
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue("U_ER_DIM" + lo_DimPrm.DimensionCode, po_ItmEvnt.Row - 1, Cls_QueriesManager_EAR.sb_DimencionesXIdEmpleado(ls_CdgEmp, lo_DimPrm.DimensionName));
                            }
                        }
                        //else
                        //{
                        //    foreach (SAPbobsCOM.DimensionParams lo_DimPrm in lo_DmnsSrv.GetDimensionList())
                        //    {
                        //        go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue("U_ER_DIM" + lo_DimPrm.DimensionCode, po_ItmEvnt.Row - 1, string.Empty);
                        //    }
                        //}
                    }
                    else
                        sb_ClearRowData(po_ItmEvnt.Row - 1);
                    Cls_EAR_Apertura_BL.sb_CalcularTotalesdeApertura(go_Form.DataSources.DBDataSources, ref li_CdgErr, ref ls_MsgErr);
                    go_Matrix.LoadFromDataSource();
                    if (li_CdgErr != 0)
                    {
                        go_SBOApplication.StatusBar.SetText(ls_MsgErr, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return false;
            }
            finally
            {
                go_Form.Freeze(false);
            }
        }

        private void sb_FormUnload()
        {
            go_Form = null;
            Dispose();
        }

        private bool sb_ValidacionesGenerales()
        {
            bool lb_Result = true;
            string ls_MsgErr = string.Empty;
            SAPbouiCOM.DBDataSource lo_DBDTSEARAPR = null;
            SAPbouiCOM.DBDataSource lo_DBDTSEARAPRDET = null;
            //Datos de Cabecera
            lo_DBDTSEARAPR = go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR);
            if (lo_DBDTSEARAPR.GetValue(gs_UflEARMnda, 0).Trim() == string.Empty)
            {
                ls_MsgErr = "Seleccione el tipo de moneda para realizar la apertura...";
                lb_Result = false;
                ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbTpoMnd).Specific).Active = true;
                goto fin;
            }
            //Datos del Detalle
            lb_Result = false;
            lo_DBDTSEARAPRDET = go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET);
            ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxEAR).Specific).FlushToDataSource();
            for (int i = 0; i < lo_DBDTSEARAPRDET.Size; i++)
            {
                if (lo_DBDTSEARAPRDET.GetValue(gs_UflDetSelecc, i).Trim() == "Y")
                    lb_Result = true;
            }
            if (!lb_Result)
            {
                ls_MsgErr = "No se ha seleccionado ninguna fila...";
                goto fin;
            }

            //Datos de Cabecera 
            switch (go_Form.PaneLevel)
            {
                case 1://Cheque
                    if (lo_DBDTSEARAPR.GetValue(gs_UflChqBnco, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Debe Seleccionar un banco...";
                        lb_Result = false;
                        ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbChqBnc).Specific).Active = true;
                        goto fin;
                    }
                    if (lo_DBDTSEARAPR.GetValue(gs_UflCtaCnt, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione una cuenta bancaria...";
                        lb_Result = false;
                        ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbChqCta).Specific).Active = true;
                        goto fin;
                    }
                    if (lo_DBDTSEARAPR.GetValue(gs_UflChqMnl, 0).Trim() == "Y" && (lo_DBDTSEARAPR.GetValue(gs_UflChqNum, 0).Trim() == string.Empty || lo_DBDTSEARAPR.GetValue(gs_UflChqNum, 0).Trim() == "0"))
                    {
                        ls_MsgErr = "Ingrese el numero de cheque...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtChqNmro).Specific).Active = true;
                        goto fin;
                    }
                    break;
                case 2://Transferencia
                    if (lo_DBDTSEARAPR.GetValue(gs_UflCtaCnt, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione cuenta de transferencia...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtTrnsCta).Specific).Active = true;
                        goto fin;
                    }
                    if (lo_DBDTSEARAPR.GetValue(gs_UflFchTrn, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Ingrese fecha de transferencia...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item("32").Specific).Active = true;
                        goto fin;
                    }
                    break;
                case 3:
                    if (lo_DBDTSEARAPR.GetValue(gs_UflCtaCnt, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione cuenta para pagos en efectivo...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtEfctCta).Specific).Active = true;
                        goto fin;
                    }
                    break;
            }
            if (lo_DBDTSEARAPR.GetValue(gs_UflMPSUNAT, 0).Trim() == string.Empty)
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

        private void sb_ClearDataForm()
        {
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflCtaCnt, 0, string.Empty);
            go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflMPSUNAT, 0, string.Empty);
        }

        private void sb_ClearRowData(int pi_Linea)
        {
            go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue("U_ER_SLCC", pi_Linea, "N");
            go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetNmrEAR, pi_Linea, string.Empty);
            go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetEstdo, pi_Linea, string.Empty);
            //go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetPryct, pi_Linea, string.Empty);
            //go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn1, pi_Linea, string.Empty);
            //go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn2, pi_Linea, string.Empty);
            //go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn3, pi_Linea, string.Empty);
            //go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn4, pi_Linea, string.Empty);
            //go_Form.DataSources.DBDataSources.Item(gs_DtdEARAPRDET).SetValue(gs_UflDetDimn5, pi_Linea, string.Empty);
        }

        private void sb_EnableEditTextNumeroCheque()
        {
            go_CheckBox = go_Form.Items.Item(gs_ChkChqManual).Specific;
            if (go_CheckBox.Checked)
            {
                go_Form.Items.Item(gs_EdtChqNmro).Enabled = true;
            }
            else
            {
                if (((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtChqNmro).Specific).Active == true)
                {
                    go_Form.Items.Item(gs_EdtChqFchV).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    go_Form.DataSources.DBDataSources.Item(gs_DtcEARAPR).SetValue(gs_UflChqNum, 0, "0");
                    go_Form.Items.Item(gs_EdtChqNmro).Enabled = false;
                }
                else
                {
                    go_Form.Items.Item(gs_EdtChqNmro).Enabled = false;
                }
            }
        }
    }

}
