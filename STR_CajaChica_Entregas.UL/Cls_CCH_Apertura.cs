using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SAPbouiCOM;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;
using STR_CajaChica_Entregas.BL;
using System.Xml;



namespace STR_CajaChica_Entregas.UL
{
    class Cls_CCH_Apertura : Cls_Global_Controles
    {
        private SAPbouiCOM.Form go_Form = null;
        private SAPbouiCOM.Application go_SBOApplication = null;
        private SAPbobsCOM.Company go_SBOCompany = null;
        //Ruta del Formulario
        private string gs_RutaForm = "Resources/CajaChicaEAR/AperturarCaja.srf";
        //Nombre unico del formulario
        public const string gs_NomForm = "FrmAprCCH";

        //* * * * * * * * * * * * * * * Menus* * * * * * * * * * * * * * * * * 
        public const string gs_MnuAprCCH = "MNU_CCH_APERTURAR";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * DataSources* * * * * * * * * * * * * * *
        private const string gs_DtcCCHAPR = "@STR_CCHAPR";
        private const string gs_DtdCCHAPRDET = "@STR_CCHAPRDET";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * User Fields - @STR_CCHAPR * * * * * * * * 
        private string gs_UflCodScNg = "U_CC_CDSN";
        private string gs_UflComents = "U_CC_CMNT";
        private string gs_UflFchCntb = "U_CC_FCHC";
        private string gs_UflFchVenc = "U_CC_FCHV";
        private string gs_UflFchDcmn = "U_CC_FCHD";
        private string gs_UflCshFlw = "U_CC_CSHF";
        private string gs_UflTpoAprt = "U_CC_TPAP";
        private string gs_UflMndCaja = "U_CC_MNDA";
        private string gs_UflCtaCnt = "U_CC_CTBN";
        private string gs_UflMPSUNAT = "U_CC_MPSN";
        private string gs_UflMdoPgo = "U_CC_MDPG";
        private string gs_UflChqFchVnc = "U_CC_CHFV";
        private string gs_UflChqBnc = "U_CC_CHBN";
        private string gs_UflChqNum = "U_CC_CHNM";
        private string gs_UflChqMnl = "U_CC_CHMN";
        private string gs_UflChqMPg = "U_CC_CHMP";
        private string gs_UflFchTrn = "U_CC_TBFC";
        private string gs_UflMntAprCCH = "U_CC_MNTT";
        private string gs_UflMntTotTrs = "U_CC_MNTR";
        private string gs_UflTotAprCCH = "U_CC_MNAP";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * * * User Fields - @STR_CCHAPRDET * * * * * * * * * *
        private string gs_UflClmCCHCod = "U_CC_CJCH";
        private string gs_UflClmCCHDsc = "U_CC_DSCP";
        private string gs_UflClmCtaSys = "U_CC_CDCT";
        private string gs_UflClmCtaNmb = "U_CC_NMCT";
        private string gs_UflClmCtaDsc = "U_CC_DSCT";
        private string gs_UflClmNmrCCH = "U_CC_NMCC";
        private string gs_UflClmNmCCHR = "U_CC_NCCR";
        private string gs_UflClmCCHTrs = "U_CC_TRSL";
        private string gs_UflClmMntTrs = "U_CC_MNTR";
        private string gs_UflClmMntApr = "U_CC_MNTO";
        private string gs_UflClmMnTotl = "U_CC_MNAP";
        private string gs_UflClmEstado = "U_CC_STDO";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 


        //Controles UI
        //Combobox
        private string gs_CmbSeries = "cmbSerie";
        private string gs_CmbTpoRnd = "cmbTpRnd";
        private string gs_CmbTpoMnd = "cmbTpMnd";
        private string gs_CmbCshFlw = "cmbCshFlw";
        private string gs_CmbChqBanc = "cmbChqBnk";
        private string gs_CmbChqCnta = "cmbChqCnta";
        private string gs_CmbChqMdPg = "cmbChqMdPg";
        //EditText
        private string gs_EdtFchCntb = "txtFchCnt";
        private string gs_EdtFchVenc = "txtFchVnc";
        private string gs_EdtFchDcmn = "txtFchDcm";
        private string gs_EdtSocNegs = "txtSocNeg";
        private string gs_EdtDocEntr = "txtDocEnt";
        private string gs_EdtChqNmro = "txtChqNum";
        private string gs_EdtChqFchV = "txtChqFchV";
        private string gs_EdtTrnCta = "txtTrnCta";
        private string gs_EdtEfcCta = "txtEfcCta";
        //OptionButton
        private string gs_OptAprt = "optAprt";
        private string gs_OptRprt = "optRprt";
        //CheckBox
        private string gs_ChkChqManual = "chkChqMnl";
        //Folders
        private string gs_FldCheque = "fldCheque";
        private string gs_FldTransf = "fldTransf";
        private string gs_FldSinPgo = "fldSinPago";
        private string gs_FldEfectv = "fldEfec";
        //Matrix
        private string gs_MtxAprtCCH = "mtxAprtCCH";
        //Columnas Matrix
        private string gs_ClmMtxCCHCodigo = "clmCCHCod";
        private string gs_ClmMtxCCHNumRap = "clmNmCCHR";
        private string gs_ClmMtxCCHNumero = "clmNumCCH";
        private string gs_ClmMtxCCHSysCta = "clmCodCnt";
        private string gs_ClmMtxCCHNroCta = "clmNmrCnt";
        private string gs_ClmMtxCCHDscCta = "clmDscCnt";
        private string gs_ClmMtxCCHMntApr = "clmMntApr";
        private string gs_ClmMtxCCHTrsSld = "clmTrsSld";
        private string gs_ClmMtxCCHMntTAp = "clmMnTotAp";
        private string gs_ClmMtxCCHProjec = "clmPjPg";
        private string gs_ClmMtxCCHDimen1 = "clmDim1";
        private string gs_ClmMtxCCHDimen2 = "clmDim2";
        private string gs_ClmMtxCCHDimen3 = "clmDim3";
        private string gs_ClmMtxCCHDimen4 = "clmDim4";
        private string gs_ClmMtxCCHDimen5 = "clmDim5";
        //ChooseFromList
        private string gs_CFLCjCh = "CFLCCH";
        private string gs_CFLCtBn = "CFLCTBN";
        private string gs_CFLPrjc = "CFLPRJ";
        private string gs_CFLDim1 = "CFLDIM1";
        private string gs_CFLDim2 = "CFLDIM2";
        private string gs_CFLDim3 = "CFLDIM3";
        private string gs_CFLDim4 = "CFLDIM4";
        private string gs_CFLDim5 = "CFLDIM5";
        //Buttons
        private string gs_BtnCrear = "1";

        public Cls_CCH_Apertura()
        {
            this.go_SBOApplication = Cls_Global.go_SBOApplication;
            this.go_SBOCompany = Cls_Global.go_SBOCompany;
        }

        public void sb_FormLoad()
        {
            XmlDocument lo_XMLForm = null;
            SAPbouiCOM.FormCreationParams lo_FrmCrtPrms = null;
            try
            {
                if (go_Form == null)
                {
                    lo_XMLForm = new XmlDocument();
                    lo_FrmCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
                    lo_XMLForm.Load(gs_RutaForm);
                    lo_FrmCrtPrms.XmlData = lo_XMLForm.InnerXml;
                    lo_FrmCrtPrms.UniqueID = gs_NomForm;
                    lo_FrmCrtPrms.FormType = gs_NomForm;
                    go_Form = go_SBOApplication.Forms.AddEx(lo_FrmCrtPrms);
                    sb_DataFormLoad();
                    sb_DataFormLoadAdd();
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
        }

        private void sb_DataFormLoad()
        {
            SAPbobsCOM.DimensionsService lo_DmnsSrv = null;
            SAPbobsCOM.CompanyService lo_CmpSrv = null;
            SAPbobsCOM.Dimension lo_Dim = null;
            try
            {
                go_Form.Freeze(true);
                lo_CmpSrv = go_SBOCompany.GetCompanyService();
                lo_DmnsSrv = lo_CmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.DimensionsService);
                go_Form.DataBrowser.BrowseBy = gs_EdtDocEntr;
                go_OptionButton = go_Form.Items.Item(gs_OptRprt).Specific;
                go_OptionButton.GroupWith(gs_OptAprt);
                go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                foreach (var lo_Item in lo_DmnsSrv.GetDimensionList())
                {
                    lo_Dim = lo_DmnsSrv.GetDimension((SAPbobsCOM.DimensionParams)lo_Item);
                    if (lo_Dim.IsActive == SAPbobsCOM.BoYesNoEnum.tYES)
                    {
                        go_Matrix.Columns.Item("clmDim" + lo_Dim.DimensionCode).TitleObject.Caption = lo_Dim.DimensionDescription;
                    }
                    else
                    {
                        go_Matrix.Columns.Item("clmDim" + lo_Dim.DimensionCode).Visible = false;
                    }
                }
                sb_AddChooseFromListToForm();
                sb_SetAutoManageItemsForm();
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                go_Form.Freeze(false);
            }
        }

        public void sb_DataFormLoadAdd()
        {
            SAPbobsCOM.Recordset lo_RecSet = null;
            string ls_Serie = string.Empty;

            try
            {
                go_Form.Freeze(true);
                go_Form.PaneLevel = 1;
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                go_Combo = go_Form.Items.Item(gs_CmbTpoRnd).Specific;
                go_Combo.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
                go_Combo = go_Form.Items.Item(gs_CmbTpoMnd).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_MonedasSociedad());
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflFchCntb, 0, go_SBOCompany.GetCompanyDate().ToString("yyyyMMdd"));
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflFchDcmn, 0, go_SBOCompany.GetCompanyDate().ToString("yyyyMMdd"));
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflFchVenc, 0, go_SBOCompany.GetCompanyDate().ToString("yyyyMMdd"));
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflChqFchVnc, 0, go_SBOCompany.GetCompanyDate().ToString("yyyyMMdd"));
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMdoPgo, 0, go_Form.PaneLevel.ToString());
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflChqMnl, 0, "N");
                go_Combo = go_Form.Items.Item(gs_CmbCshFlw).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_ListaFlujodeCaja());
                go_Combo.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflTpoAprt, 0, "1");
                go_Combo = go_Form.Items.Item(gs_CmbSeries).Specific;
                go_Combo.ValidValues.LoadSeries(go_Form.BusinessObject.Type, SAPbouiCOM.BoSeriesMode.sf_Add);
                if (go_Combo.Selected == null && go_Combo.ValidValues.Count > 0)
                {
                    ls_Serie = go_Combo.ValidValues.Item(0).Value;
                }
                else
                {
                    ls_Serie = go_Combo.Selected.Value;
                }
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue("Series", 0, ls_Serie);
                this.sb_GetNextDocumentNumber();
                go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                // Addicion de ChooseFromLists al Matrix * * * * * * * * * * * * * * * * * * * 
                go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).ChooseFromListUID = gs_CFLCjCh;
                go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).ChooseFromListAlias = "Code";
                go_Matrix.Columns.Item(gs_ClmMtxCCHNumRap).Visible = false;

                go_Matrix.Columns.Item(gs_ClmMtxCCHProjec).ChooseFromListUID = gs_CFLPrjc;
                go_Matrix.Columns.Item(gs_ClmMtxCCHProjec).ChooseFromListAlias = "PrjCode";

                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen1).ChooseFromListUID = gs_CFLDim1;
                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen1).ChooseFromListAlias = "OcrCode";

                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen2).ChooseFromListUID = gs_CFLDim2;
                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen2).ChooseFromListAlias = "OcrCode";

                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen3).ChooseFromListUID = gs_CFLDim3;
                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen3).ChooseFromListAlias = "OcrCode";

                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen4).ChooseFromListUID = gs_CFLDim4;
                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen4).ChooseFromListAlias = "OcrCode";

                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen5).ChooseFromListUID = gs_CFLDim5;
                go_Matrix.Columns.Item(gs_ClmMtxCCHDimen5).ChooseFromListAlias = "OcrCode";

                go_Matrix.Columns.Item(gs_ClmMtxCCHMntApr).Editable = true;
                go_Matrix.AddRow();
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                lo_RecSet = null;
                go_Form.Freeze(false);
            }
        }

        private void sb_GetNextDocumentNumber()
        {
            string ls_Serie = string.Empty;
            ls_Serie = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue("Series", 0);
            go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue("DocNum", 0, go_Form.BusinessObject.GetNextSerialNumber(ls_Serie, go_Form.BusinessObject.Type).ToString());
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
                    if (lo_Item.UniqueID != gs_EdtSocNegs && lo_Item.UniqueID != gs_EdtFchDcmn && lo_Item.UniqueID != gs_EdtFchCntb && lo_Item.UniqueID != gs_EdtFchVenc && lo_Item.UniqueID != gs_CmbTpoMnd)
                    {
                        lo_Item.SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Find, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                    }
                }
            }
        }

        private void sb_AddChooseFromListToForm()
        {
            SAPbouiCOM.ChooseFromListCreationParams lo_CFLCrtPrms = null;
            SAPbouiCOM.ChooseFromListCollection lo_CFLClltn = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            //Choose From List Cajas-Chicas
            lo_CFLCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams);
            lo_CFLClltn = go_Form.ChooseFromLists;
            lo_CFLCrtPrms.MultiSelection = false;
            lo_CFLCrtPrms.ObjectType = "BPP_CAJASCHICAS";
            lo_CFLCrtPrms.UniqueID = gs_CFLCjCh;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);

            //ChooseFromLista Cuentas 
            lo_CFLCrtPrms.ObjectType = "1";
            lo_CFLCrtPrms.UniqueID = gs_CFLCtBn;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);
            lo_Cnds = lo_CFL.GetConditions();
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "Postable";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = "Y";
            lo_CFL.SetConditions(lo_Cnds);

            // ChooseFromList Proyectos
            lo_CFLCrtPrms.ObjectType = "63";
            lo_CFLCrtPrms.UniqueID = gs_CFLPrjc;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);

            //ChooseFromList Dimencion1
            lo_CFLCrtPrms.ObjectType = "62";
            lo_CFLCrtPrms.UniqueID = gs_CFLDim1;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);
            lo_Cnds = lo_CFL.GetConditions();
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "DimCode";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = "1";
            lo_CFL.SetConditions(lo_Cnds);

            //ChooseFromList Dimencion2
            lo_CFLCrtPrms.UniqueID = gs_CFLDim2;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);
            lo_Cnds = lo_CFL.GetConditions();
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "DimCode";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = "2";
            lo_CFL.SetConditions(lo_Cnds);

            //ChooseFromList Dimencion3
            lo_CFLCrtPrms.UniqueID = gs_CFLDim3;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);
            lo_Cnds = lo_CFL.GetConditions();
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "DimCode";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = "3";
            lo_CFL.SetConditions(lo_Cnds);

            //ChooseFromList Dimencion4
            lo_CFLCrtPrms.UniqueID = gs_CFLDim4;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);
            lo_Cnds = lo_CFL.GetConditions();
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "DimCode";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = "4";
            lo_CFL.SetConditions(lo_Cnds);

            //ChooseFromList Dimencion5
            lo_CFLCrtPrms.UniqueID = gs_CFLDim5;
            lo_CFL = lo_CFLClltn.Add(lo_CFLCrtPrms);
            lo_Cnds = lo_CFL.GetConditions();
            lo_Cnd = lo_Cnds.Add();
            lo_Cnd.Alias = "DimCode";
            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
            lo_Cnd.CondVal = "5";
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
                    lb_Result = this.fn_HandleChooseFromList(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED:
                    lb_Result = this.fn_HandleItemPressed(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT:
                    lb_Result = this.fn_HandleComboSelect(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_VALIDATE:
                    lb_Result = this.fn_HandleValidate(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_CLICK:
                    lb_Result = this.fn_HandleClick(po_ItmEvnt);
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                    this.sb_FixWidthColumn(po_ItmEvnt);
                    break;
            }
            return lb_Result;
        }

        private void sb_FixWidthColumn(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            if (!po_ItmEvnt.BeforeAction && go_Form != null)
            {
                go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                go_Matrix.Columns.Item(gs_ClmMtxCCHSysCta).Width = 15;
            }
        }

        private bool fn_HandleClick(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            string[] lo_ArrCad = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            string ls_Qry = string.Empty;
            SAPbouiCOM.ComboBox lo_ComboAux = null;
            System.Windows.Forms.DialogResult lo_Resultado;

            if (po_ItmEvnt.ItemUID != string.Empty && go_Form != null)
            {
                switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                        if (po_ItmEvnt.ColUID == gs_ClmMtxCCHNumRap)
                        {
                            if (po_ItmEvnt.BeforeAction)
                            {
                                try
                                {
                                    go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                                    lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    lo_ArrCad = Cls_QueriesManager_CCH.NumerosdeCCHActivos.Split(new char[] { '?' });
                                    ls_Qry = lo_ArrCad[0].Trim() + go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHCod, po_ItmEvnt.Row - 1).Trim() + lo_ArrCad[1].Trim();
                                    Cls_Global.WriteToFile(ls_Qry);
                                    lo_RecSet.DoQuery(ls_Qry);
                                    go_Combo = go_Matrix.GetCellSpecific(gs_ClmMtxCCHNumRap, po_ItmEvnt.Row);
                                    Cls_Global.sb_CargarCombo(go_Combo, lo_RecSet, false);
                                }
                                catch (Exception ex)
                                {
                                    Cls_Global.WriteToFile(ex.Message);
                                    go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
                                }
                                finally
                                {
                                    lo_RecSet = null;
                                }
                            }
                        }
                        if (po_ItmEvnt.ColUID == gs_ClmMtxCCHTrsSld)
                        {
                            if (po_ItmEvnt.Before_Action)
                            {
                                try
                                {
                                    go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                                    lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    lo_ArrCad = Cls_QueriesManager_CCH.NumerosdeCCHActivos.Split(new char[] { '?' });
                                    ls_Qry = lo_ArrCad[0].Trim() + go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHCod, po_ItmEvnt.Row - 1).Trim() + lo_ArrCad[1].Trim();
                                    Cls_Global.WriteToFile(ls_Qry);
                                    lo_RecSet.DoQuery(ls_Qry);
                                    go_Combo = go_Matrix.GetCellSpecific(gs_ClmMtxCCHTrsSld, po_ItmEvnt.Row);
                                    //Si es una reapertura, entonces quito de la lista la caja seleccionada en el primer combo
                                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflTpoAprt, 0).Trim() == "2")
                                    {
                                        go_Combo = go_Matrix.GetCellSpecific(gs_ClmMtxCCHNumRap, po_ItmEvnt.Row);
                                        lo_ComboAux = go_Matrix.GetCellSpecific(gs_ClmMtxCCHTrsSld, po_ItmEvnt.Row);
                                        while (lo_ComboAux.ValidValues.Count > 0)
                                        {
                                            lo_ComboAux.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                                        }
                                        if (go_Combo.Value != string.Empty && go_Combo.Value != "---")
                                        {
                                            Cls_Global.sb_CargarCombo(lo_ComboAux, lo_RecSet, false);
                                            for (int i = 0; i < lo_ComboAux.ValidValues.Count; i++)
                                            {
                                                if (go_Combo.Value == lo_ComboAux.ValidValues.Item(i).Value)
                                                {
                                                    lo_ComboAux.ValidValues.Remove(i, SAPbouiCOM.BoSearchKey.psk_Index);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Cls_Global.sb_CargarCombo(go_Combo, lo_RecSet, true);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Cls_Global.WriteToFile(ex.Message);
                                    go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
                                }
                                finally
                                {
                                    lo_RecSet = null;
                                }
                            }
                        }
                        break;
                    case SAPbouiCOM.BoFormItemTypes.it_OPTION_BUTTON:
                        if (po_ItmEvnt.BeforeAction)
                        {
                            lo_Resultado = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("Con las modificaciones se borrarán los datos ingresados.¿Desea continuar?", 1, "Si", "No");
                            if (lo_Resultado != System.Windows.Forms.DialogResult.OK)
                            {
                                lb_Result = false;
                            }
                            else
                            {
                                ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbCshFlw).Specific).Active = true;
                                go_OptionButton = go_Form.Items.Item(po_ItmEvnt.ItemUID).Specific;
                                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflTpoAprt, 0, go_OptionButton.ValOn);
                                sb_Enable_DisableControlsByOption(Convert.ToInt32(go_OptionButton.ValOn));
                                sb_ClearDataForm();
                                ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxAprtCCH).Specific).LoadFromDataSource();
                            }
                        }
                        break;
                }
            }
            return lb_Result;
        }

        private bool fn_HandleValidate(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            int li_CodErr = 0;
            string ls_DscErr = string.Empty;
            if (po_ItmEvnt.ItemUID != string.Empty && go_Form != null)
            {
                switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                        if (po_ItmEvnt.ColUID == gs_ClmMtxCCHCodigo)
                        {
                            if (!po_ItmEvnt.BeforeAction)
                            {
                                this.sb_GenerarCodigoCajaChica(po_ItmEvnt);
                            }
                        }
                        if (po_ItmEvnt.ColUID == gs_ClmMtxCCHMntApr)
                        {
                            if (!po_ItmEvnt.BeforeAction)
                            {
                                try
                                {
                                    go_Form.Freeze(true);
                                    ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxAprtCCH).Specific).FlushToDataSource();
                                    Cls_CCH_Aperturar_BL.sb_CalcularTotalesXLinea(go_Form, po_ItmEvnt.Row, ref li_CodErr, ref ls_DscErr);
                                    Cls_CCH_Aperturar_BL.sb_CalcularTotalesdeApertura(go_Form, ref li_CodErr, ref ls_DscErr);
                                    if (li_CodErr != 0 && ls_DscErr != string.Empty)
                                    {
                                        go_SBOApplication.SetStatusBarMessage(ls_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                        lb_Result = false;
                                    }
                                    else
                                    {
                                        ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxAprtCCH).Specific).LoadFromDataSource();
                                        go_Matrix.Columns.Item(gs_ClmMtxCCHMntApr).Cells.Item(po_ItmEvnt.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Cls_Global.WriteToFile(ex.Message);
                                    go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
                                }
                                finally
                                {
                                    go_Form.Freeze(false);
                                }
                            }
                        }
                        break;
                }
            }
            return lb_Result;
        }

        private bool fn_HandleChooseFromList(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            string ls_CFLId = string.Empty;
            string ls_SelectedValue = string.Empty;
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.IChooseFromListEvent lo_CFLEvnt = null;
            SAPbouiCOM.DataTable lo_DataTable = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbobsCOM.GeneralService lo_GnrSrv = null;
            SAPbobsCOM.GeneralData lo_GnrDta = null;
            SAPbobsCOM.GeneralDataParams lo_GnrDtaPrms = null;
            SAPbobsCOM.CompanyService lo_CmpSrv = null;
            SAPbobsCOM.GeneralDataCollection lo_GnrDtaCll = null;
            SAPbobsCOM.DimensionsService lo_DmnsSrv = null;

            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            int li_CodErr = 0;
            string ls_DscErr = string.Empty;
            string ls_DimNmb = string.Empty;

            lo_CFLEvnt = (SAPbouiCOM.IChooseFromListEvent)po_ItmEvnt;
            go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCjCh)
            {
                if (!lo_CFLEvnt.BeforeAction)
                {
                    lo_DataTable = lo_CFLEvnt.SelectedObjects;
                    if (lo_DataTable != null)
                    {
                        SAPbobsCOM.Recordset lo_RecSet = null;
                        try
                        {
                            go_Form.Freeze(true);
                            lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            lo_CmpSrv = go_SBOCompany.GetCompanyService();
                            lo_GnrSrv = lo_CmpSrv.GetGeneralService("BPP_CAJASCHICAS");
                            lo_GnrDtaPrms = lo_GnrSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                            lo_GnrDtaPrms.SetProperty("Code", lo_DataTable.GetValue("Code", 0));
                            lo_GnrDta = lo_GnrSrv.GetByParams(lo_GnrDtaPrms);
                            go_Matrix.FlushToDataSource();
                            lo_ArrCad = Cls_QueriesManager_CCH.DatosCuentaCCH.Split(new char[] { '?' });
                            ls_Qry = lo_ArrCad[0].Trim() + ((string)lo_DataTable.GetValue("U_BPP_ACCT", 0)).Trim() + lo_ArrCad[1].Trim();
                            Cls_Global.WriteToFile(ls_Qry);
                            lo_RecSet.DoQuery(ls_Qry);

                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmCCHCod, po_ItmEvnt.Row - 1, lo_DataTable.GetValue("Code", 0));
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmCCHDsc, po_ItmEvnt.Row - 1, lo_DataTable.GetValue("Name", 0));
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmCtaSys, po_ItmEvnt.Row - 1, lo_DataTable.GetValue("U_BPP_ACCT", 0));
                            if (!lo_RecSet.EoF)
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmCtaNmb, po_ItmEvnt.Row - 1, lo_RecSet.Fields.Item(0).Value);
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmCtaDsc, po_ItmEvnt.Row - 1, lo_RecSet.Fields.Item(1).Value);
                            }
                            if (li_CodErr != 0 && ls_DscErr != string.Empty)
                            {
                                go_SBOApplication.SetStatusBarMessage(ls_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                return false;
                            }

                            if (lo_GnrDta.GetProperty("U_STR_PRYS") == "Y")
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue("U_CC_PRYC", po_ItmEvnt.Row - 1, lo_GnrDta.GetProperty("U_STR_PRYD"));
                            }
                            else
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue("U_CC_PRYC", po_ItmEvnt.Row - 1, string.Empty);
                            }
                            if (lo_GnrDta.GetProperty("U_STR_DIM") == "Y")
                            {
                                lo_GnrDtaCll = lo_GnrDta.Child("STR_CAJASCHICASDIM");
                                for (int i = 0; i < lo_GnrDtaCll.Count; i++)
                                {
                                    if (lo_GnrDtaCll.Item(i).GetProperty("U_CC_DFLT") != string.Empty)
                                    {
                                        ls_DimNmb = lo_GnrDtaCll.Item(i).GetProperty("U_CC_NMBR");
                                        ls_DimNmb = ls_DimNmb.Substring(ls_DimNmb.Length - 1, 1).Trim();
                                        go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue("U_CC_DIM" + ls_DimNmb, po_ItmEvnt.Row - 1, lo_GnrDtaCll.Item(i).GetProperty("U_CC_DFLT"));
                                    }
                                }
                            }
                            else
                            {
                                lo_DmnsSrv = lo_CmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.DimensionsService);
                                foreach (SAPbobsCOM.DimensionParams lo_DimPrm in lo_DmnsSrv.GetDimensionList())
                                {
                                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue("U_CC_DIM" + lo_DimPrm.DimensionCode, po_ItmEvnt.Row - 1, string.Empty);
                                }
                            }

                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmNmrCCH, po_ItmEvnt.Row - 1, string.Empty);
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmCCHTrs, po_ItmEvnt.Row - 1, string.Empty);
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMntTrs, po_ItmEvnt.Row - 1, string.Empty);
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMntApr, po_ItmEvnt.Row - 1, string.Empty);
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMnTotl, po_ItmEvnt.Row - 1, string.Empty);
                            go_Matrix.LoadFromDataSource();
                            go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).Cells.Item(po_ItmEvnt.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            Cls_CCH_Aperturar_BL.sb_CalcularTotalesdeApertura(go_Form, ref li_CodErr, ref ls_DscErr); ;
                        }
                        catch (Exception ex)
                        {
                            Cls_Global.WriteToFile(ex.Message);
                            go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
                        }
                        finally
                        {
                            lo_RecSet = null;
                            go_Form.Freeze(false);
                        }
                    }
                }
                else
                {
                    lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLCjCh);
                    lo_CFL.SetConditions(null);
                    lo_Cnds = lo_CFL.GetConditions();
                    lo_Cnd = lo_Cnds.Add();
                    lo_Cnd.Alias = "U_BPP_TIPM";
                    lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    lo_Cnd.CondVal = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflMndCaja, 0);
                    lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    lo_Cnd = lo_Cnds.Add();
                    lo_Cnd.Alias = "U_BPP_TIPR";
                    lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    lo_Cnd.CondVal = "CCH";
                    lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    lo_Cnd = lo_Cnds.Add();
                    lo_Cnd.Alias = "U_BPP_STAD";
                    lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    lo_Cnd.CondVal = "A";
                    for (int i = 0; i < go_Matrix.RowCount; i++)
                    {
                        lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                        lo_Cnd = lo_Cnds.Add();
                        lo_Cnd.Alias = "Code";
                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_NOT_EQUAL;
                        lo_Cnd.CondVal = ((SAPbouiCOM.EditText)go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).Cells.Item(i + 1).Specific).Value;
                    }
                    lo_CFL.SetConditions(lo_Cnds);
                }
            }
            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCtBn)
            {
                if (!lo_CFLEvnt.BeforeAction)
                {
                    lo_DataTable = lo_CFLEvnt.SelectedObjects;
                    if (lo_DataTable != null)
                    {
                        go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflCtaCnt, 0, lo_DataTable.GetValue(0, 0));
                    }
                }
            }
            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLPrjc || lo_CFLEvnt.ChooseFromListUID == gs_CFLDim1
                || lo_CFLEvnt.ChooseFromListUID == gs_CFLDim2 || lo_CFLEvnt.ChooseFromListUID == gs_CFLDim3
                || lo_CFLEvnt.ChooseFromListUID == gs_CFLDim4 || lo_CFLEvnt.ChooseFromListUID == gs_CFLDim5)
            {
                if (!lo_CFLEvnt.BeforeAction)
                {
                    try
                    {
                        go_Form.Freeze(true);
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            go_Matrix.FlushToDataSource();
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(go_Matrix.Columns.Item(lo_CFLEvnt.ColUID).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(0, 0));
                            go_Matrix.LoadFromDataSource();
                            go_Matrix.Columns.Item(lo_CFLEvnt.ColUID).Cells.Item(lo_CFLEvnt.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                        }
                    }
                    catch (Exception ex)
                    {
                        Cls_Global.WriteToFile(ex.Message);
                        go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
                    }
                    finally
                    {
                        go_Form.Freeze(false);
                    }
                }
            }
            return true;
        }

        private bool fn_HandleItemPressed(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            int li_CodErr = 0;
            string ls_DscErr = string.Empty;

            if (po_ItmEvnt.ItemUID != string.Empty && go_Form != null)
            {
                switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_FOLDER:
                        if (!po_ItmEvnt.BeforeAction)
                        {
                            sb_SetPaneLevel(po_ItmEvnt.ItemUID);
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
                    case SAPbouiCOM.BoFormItemTypes.it_BUTTON:
                        if (po_ItmEvnt.BeforeAction)
                        {
                            if (po_ItmEvnt.ItemUID == gs_BtnCrear)
                            {
                                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                                {
                                    lb_Result = fn_ValidacionesGenerales();
                                    if (lb_Result)
                                    {
                                        //Elimino la utima linea vacia del Matrix si esta vacia
                                        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
                                        go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                                        if (((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxCCHCodigo, go_Matrix.RowCount)).Value == string.Empty) go_Matrix.DeleteRow(go_Matrix.RowCount);
                                        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
                                        go_Matrix.FlushToDataSource();
                                        if (go_Form.PaneLevel != 4)
                                        {
                                            Cls_CCH_Aperturar_BL.fn_GenerarPagoEfectuado(go_Form, ref li_CodErr, ref ls_DscErr);
                                            if (li_CodErr != 0)
                                            {
                                                if (ls_DscErr != string.Empty)
                                                {
                                                    go_SBOApplication.SetStatusBarMessage(ls_DscErr + " - " + li_CodErr.ToString());
                                                }
                                                lb_Result = false;
                                            }
                                        }
                                        else
                                        {
                                            //Si la accion es solo traspasar el saldo de un numero de caja a otro
                                            for (int i = 0; i < go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).Size; i++)
                                            {
                                                if (go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHTrs, i).Trim() != string.Empty &&
                                                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHTrs, i).Trim() != "---")
                                                {
                                                    Cls_QueriesManager_CCH.sb_ActualizarEstadoySaldoXNroCCH("C", 0.0, go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHCod, i).Trim(), go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHTrs, i).Trim());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            return lb_Result;
        }

        private void sb_Enable_DisableControlsByOption(int pi_SlcOpt)
        {
            go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
            go_Form.Freeze(true);
            try
            {
                if (pi_SlcOpt == 1)
                {
                    go_Matrix.Columns.Item(gs_ClmMtxCCHNumRap).Visible = false;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHNumero).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHSysCta).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHNroCta).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHDscCta).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHMntApr).Editable = true;
                    go_Form.Items.Item(gs_FldCheque).Enabled = true;
                    go_Form.Items.Item(gs_FldTransf).Enabled = true;
                    go_Form.Items.Item(gs_FldSinPgo).Enabled = true;
                    go_Form.Items.Item(gs_FldEfectv).Enabled = true;
                    go_Folder = go_Form.Items.Item(gs_FldCheque).Specific;
                    go_Folder.Select();
                }
                else
                {
                    go_Matrix.Columns.Item(gs_ClmMtxCCHNumRap).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHNumero).Visible = false;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHSysCta).Visible = false;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHNroCta).Visible = false;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHDscCta).Visible = false;
                    go_Matrix.Columns.Item(gs_ClmMtxCCHMntApr).Editable = false;
                    go_Form.Items.Item(gs_FldCheque).Enabled = false;
                    go_Form.Items.Item(gs_FldTransf).Enabled = false;
                    go_Form.Items.Item(gs_FldSinPgo).Enabled = true;
                    go_Form.Items.Item(gs_FldEfectv).Enabled = false;
                    go_Folder = go_Form.Items.Item(gs_FldSinPgo).Specific;
                    go_Folder.Select(); ;
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.SetStatusBarMessage(ex.Message);
            }
            finally
            {
                go_Form.Freeze(false);
            }
        }

        private bool fn_ValidacionesGenerales()
        {
            bool lb_Result = true;
            string ls_MsgErr = string.Empty;
            string ls_CdgCCH = string.Empty;

            try
            {
                go_Combo = go_Form.Items.Item(gs_CmbTpoMnd).Specific;
                if (go_Combo.Value.Trim() == string.Empty)
                {
                    ls_MsgErr = "Seleccione el tipo de moneda para realizar la apertura...";
                    lb_Result = false;
                    go_Combo.Active = true;
                    goto fin;
                }
                go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                go_Edit = go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).Cells.Item(1).Specific;
                if (go_Edit.Value.Trim() == string.Empty)
                {
                    ls_MsgErr = "Debe aperturar números de caja...";
                    lb_Result = false;
                    goto fin;
                }

                for (int i = 0; i < go_Matrix.RowCount; i++)
                {
                    if (((SAPbouiCOM.EditText)go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).Cells.Item(i + 1).Specific).Value == string.Empty) continue;
                    go_Edit = go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).Cells.Item(i + 1).Specific;
                    ls_CdgCCH = go_Edit.Value.Trim();
                    if (!fn_ValidarPermisos(ls_CdgCCH, go_SBOCompany.UserName))
                    {
                        ls_MsgErr = "No tiene permisos para aperturar la caja de la fila marcada...";
                        lb_Result = false;
                        go_Matrix.SelectRow(i + 1, true, false);
                        goto fin;
                    }
                    if (go_Form.PaneLevel != 4)
                    {
                        go_Edit = go_Matrix.Columns.Item(gs_ClmMtxCCHMntApr).Cells.Item(i + 1).Specific;
                        if (Convert.ToDouble(go_Edit.Value) == 0.0 && ((SAPbouiCOM.EditText)go_Matrix.Columns.Item(gs_ClmMtxCCHCodigo).Cells.Item(i + 1).Specific).Value != string.Empty)
                        {
                            ls_MsgErr = "El monto de la fila marcada es 0...";
                            lb_Result = false;
                            go_Matrix.SelectRow(i + 1, true, false);
                            goto fin;
                        }
                    }
                    else
                    {
                        go_Edit = go_Matrix.Columns.Item(gs_ClmMtxCCHMntApr).Cells.Item(i + 1).Specific;
                        if (Convert.ToDouble(go_Edit.Value) > 0.0 && go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflTpoAprt, 0).Trim() != "2")
                        {
                            ls_MsgErr = "Si solo desea realizar un traspaso de saldo, el monto de la linea marcada debe ser 0...";
                            lb_Result = false;
                            go_Matrix.SelectRow(i + 1, true, false);
                            goto fin;
                        }
                        go_Combo = go_Matrix.Columns.Item(gs_ClmMtxCCHTrsSld).Cells.Item(i + 1).Specific;
                        if (go_Combo.Value.Trim() == string.Empty || go_Combo.Value.Trim() == "---")
                        {
                            ls_MsgErr = "Debe seleccionar una caja de origen de traspaso...";
                            lb_Result = false;
                            go_Matrix.SelectRow(i + 1, true, false);
                            goto fin;
                        }
                    }
                    go_Edit = go_Matrix.Columns.Item(gs_ClmMtxCCHMntTAp).Cells.Item(i + 1).Specific;
                    if (!fn_ValidarMontoMaximodeApertura(ls_CdgCCH, Convert.ToDouble(go_Edit.Value.Trim())))
                    {
                        ls_MsgErr = "El monto total de apertura de la fila marcada es mayor que el monto establecido en la definicion de esta caja chica...";
                        lb_Result = false;
                        go_Matrix.SelectRow(i + 1, true, false);
                        goto fin;
                    }

                }
                if (go_Form.PaneLevel == 1) // Cheque
                {
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflChqBnc, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Debe seleccionar un banco...";
                        lb_Result = false;
                        ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbChqBanc).Specific).Active = true;
                        goto fin;
                    }
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflCtaCnt, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione una cuenta bancaria...";
                        lb_Result = false;
                        ((SAPbouiCOM.ComboBox)go_Form.Items.Item(gs_CmbChqCnta).Specific).Active = true;

                        goto fin;
                    }
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflChqMnl, 0).Trim() == "Y")
                    {
                        if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflChqNum, 0).Trim() == string.Empty)
                        {
                            ls_MsgErr = "Ingrese el numero de cheque...";
                            lb_Result = false;
                            ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtChqNmro).Specific).Active = true;
                            goto fin;
                        }
                    }
                }
                if (go_Form.PaneLevel == 2) //Transferencia
                {
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflCtaCnt, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione cuenta de transferencia...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtTrnCta).Specific).Active = true;
                        goto fin;
                    }
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflFchTrn, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Ingrese fecha de transferencia...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item("32").Specific).Active = true;
                        goto fin;
                    }
                }
                if (go_Form.PaneLevel == 3) //Efectivo
                {
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflCtaCnt, 0).Trim() == string.Empty)
                    {
                        ls_MsgErr = "Seleccione cuenta para pagos en efectivo...";
                        lb_Result = false;
                        ((SAPbouiCOM.EditText)go_Form.Items.Item(gs_EdtEfcCta).Specific).Active = true;
                        goto fin;
                    }

                }
                if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflMPSUNAT, 0).Trim() == string.Empty && go_Form.PaneLevel != 4)
                {
                    ls_MsgErr = "Seleccione el medio de pago SUNAT...";
                    lb_Result = false;
                    goto fin;
                }
            fin:
                if (!lb_Result)
                {
                    go_SBOApplication.SetStatusBarMessage(ls_MsgErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                }
                return lb_Result;
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.StatusBar.SetText("Modulo - Validaciones Generales: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return lb_Result = false;
            }

        }

        private bool fn_HandleComboSelect(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            int li_CodErr = 0;
            string ls_DscErr = string.Empty;
            bool lb_Result = true;
            SAPbobsCOM.Recordset lo_RecSet = null;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            System.Windows.Forms.DialogResult lo_Resultado;

            try
            {
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (po_ItmEvnt.ItemUID != string.Empty)
                {
                    switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                    {
                        case SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX:
                            if (po_ItmEvnt.ItemUID == gs_CmbTpoMnd) // Combo Monedas
                            {
                                if (!po_ItmEvnt.BeforeAction)
                                {
                                    go_Combo = go_Form.Items.Item(gs_CmbChqBanc).Specific;
                                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflChqBnc, 0, string.Empty);
                                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflCtaCnt, 0, string.Empty);
                                    Cls_Global.WriteToFile(Cls_QueriesManager_CCH.ListadeBancos);
                                    lo_RecSet.DoQuery(Cls_QueriesManager_CCH.ListadeBancos);
                                    Cls_Global.sb_CargarCombo(go_Combo, lo_RecSet);
                                }
                                else
                                {
                                    go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                                    if (go_Matrix.RowCount > 1)
                                    {
                                        lo_Resultado = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("Con las modificaciones se borrarán los datos ingresados.¿Desea continuar?", 1, "Si", "No");
                                        if (lo_Resultado != System.Windows.Forms.DialogResult.OK)
                                        {
                                            lb_Result = false;
                                        }
                                        else
                                        {
                                            sb_ClearDataForm();
                                        }
                                    }
                                }
                            }
                            if (po_ItmEvnt.ItemUID == gs_CmbChqBanc) // Combo Bancos
                            {

                                string ls_Moneda = string.Empty;
                                string ls_Banco = string.Empty;
                                if (!po_ItmEvnt.BeforeAction)
                                {
                                    ls_Banco = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflChqBnc, 0).Trim();
                                    ls_Moneda = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflMndCaja, 0).Trim();
                                    lo_ArrCad = Cls_QueriesManager_CCH.CuentasdeBanco.Split(new char[] { '?' });
                                    ls_Qry = lo_ArrCad[0] + ls_Banco + lo_ArrCad[1] + ls_Moneda + lo_ArrCad[2];
                                    go_Combo = go_Form.Items.Item(gs_CmbChqCnta).Specific;
                                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflCtaCnt, 0, string.Empty);
                                    Cls_Global.WriteToFile(ls_Qry);
                                    lo_RecSet.DoQuery(ls_Qry);
                                    Cls_Global.sb_CargarCombo(go_Combo, lo_RecSet);
                                }
                            }
                            break;
                        case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                            if (po_ItmEvnt.ColUID == gs_ClmMtxCCHTrsSld)
                            {
                                if (!po_ItmEvnt.BeforeAction)
                                {
                                    try
                                    {
                                        go_Form.Freeze(true);
                                        go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                                        go_Matrix.FlushToDataSource();
                                        lo_ArrCad = Cls_QueriesManager_CCH.SaldoCajaChica.Split(new char[] { '?' });
                                        ls_Qry = lo_ArrCad[0] + go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHTrs, po_ItmEvnt.Row - 1).Trim() + lo_ArrCad[1];
                                        Cls_Global.WriteToFile(ls_Qry);
                                        lo_RecSet.DoQuery(ls_Qry);
                                        if (!lo_RecSet.EoF)
                                        {
                                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMntTrs, po_ItmEvnt.Row - 1, lo_RecSet.Fields.Item(0).Value);
                                        }
                                        else
                                        {
                                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMntTrs, po_ItmEvnt.Row - 1, string.Empty);
                                        }
                                        Cls_CCH_Aperturar_BL.sb_CalcularTotalesXLinea(go_Form, po_ItmEvnt.Row, ref li_CodErr, ref ls_DscErr);
                                        if (li_CodErr != 0 && ls_DscErr != string.Empty)
                                        {
                                            go_SBOApplication.SetStatusBarMessage(ls_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                            lb_Result = false;
                                            break;
                                        }
                                        Cls_CCH_Aperturar_BL.sb_CalcularTotalesdeApertura(go_Form, ref li_CodErr, ref ls_DscErr);
                                        if (li_CodErr != 0 && ls_DscErr != string.Empty)
                                        {
                                            go_SBOApplication.SetStatusBarMessage(ls_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                            lb_Result = false;
                                            break;
                                        }
                                        go_Matrix.LoadFromDataSource();
                                        go_Matrix.SetCellFocus(po_ItmEvnt.Row, 10);
                                    }
                                    catch (Exception ex)
                                    {
                                        Cls_Global.WriteToFile(ex.Message);
                                        go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
                                    }
                                    finally
                                    {
                                        go_Form.Freeze(false);
                                    }
                                }
                            }
                            if (po_ItmEvnt.ColUID == gs_ClmMtxCCHNumRap)
                            {
                                if (!po_ItmEvnt.BeforeAction)
                                {
                                    go_Form.Freeze(true);
                                    go_Matrix.FlushToDataSource();
                                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmCCHTrs, po_ItmEvnt.Row - 1, string.Empty);
                                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMntTrs, po_ItmEvnt.Row - 1, string.Empty);
                                    lo_ArrCad = Cls_QueriesManager_CCH.SaldoCajaChica.Split(new char[] { '?' });
                                    ls_Qry = lo_ArrCad[0] + go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmNmCCHR, po_ItmEvnt.Row - 1).Trim() + lo_ArrCad[1];
                                    Cls_Global.WriteToFile(ls_Qry);
                                    lo_RecSet.DoQuery(ls_Qry);
                                    if (!lo_RecSet.EoF)
                                    {
                                        go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMntApr, po_ItmEvnt.Row - 1, lo_RecSet.Fields.Item(0).Value);
                                    }
                                    else
                                    {
                                        go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmMntApr, po_ItmEvnt.Row - 1, string.Empty);
                                    }
                                    Cls_CCH_Aperturar_BL.sb_CalcularTotalesXLinea(go_Form, po_ItmEvnt.Row, ref li_CodErr, ref ls_DscErr);
                                    if (li_CodErr != 0 && ls_DscErr != string.Empty)
                                    {
                                        go_SBOApplication.SetStatusBarMessage(ls_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                        lb_Result = false;
                                        break;
                                    }
                                    Cls_CCH_Aperturar_BL.sb_CalcularTotalesdeApertura(go_Form, ref li_CodErr, ref ls_DscErr);
                                    if (li_CodErr != 0 && ls_DscErr != string.Empty)
                                    {
                                        go_SBOApplication.SetStatusBarMessage(ls_DscErr, SAPbouiCOM.BoMessageTime.bmt_Short);
                                        lb_Result = false;
                                        break;
                                    }
                                    go_Matrix.LoadFromDataSource();
                                    go_Form.Freeze(false);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                lo_RecSet = null;
            }

            return lb_Result;
        }

        private void sb_SetPaneLevel(string ps_FolderID)
        {
            if (ps_FolderID == gs_FldCheque)
            {
                go_Form.PaneLevel = 1;
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMdoPgo, 0, go_Form.PaneLevel.ToString());
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflCtaCnt, 0, string.Empty);
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMPSUNAT, 0, string.Empty);
            }
            else if (ps_FolderID == gs_FldTransf)
            {
                go_Form.PaneLevel = 2;
                go_Edit = go_Form.Items.Item(gs_EdtTrnCta).Specific;
                go_Edit.ChooseFromListUID = gs_CFLCtBn;
                go_Edit.ChooseFromListAlias = "AcctCode";
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMdoPgo, 0, go_Form.PaneLevel.ToString());
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflCtaCnt, 0, string.Empty);
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMPSUNAT, 0, string.Empty);
            }
            else if (ps_FolderID == gs_FldSinPgo)
            {
                go_Form.PaneLevel = 4;
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMdoPgo, 0, go_Form.PaneLevel.ToString());
            }
            else
            {
                go_Form.PaneLevel = 3;
                go_Edit = go_Form.Items.Item(gs_EdtEfcCta).Specific;
                go_Edit.ChooseFromListUID = gs_CFLCtBn;
                go_Edit.ChooseFromListAlias = "AcctCode";
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMdoPgo, 0, go_Form.PaneLevel.ToString());
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflCtaCnt, 0, string.Empty);
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMPSUNAT, 0, string.Empty);
            }
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
                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflChqNum, 0, "0");
                    go_Form.Items.Item(gs_EdtChqNmro).Enabled = false;
                }
                else
                {
                    go_Form.Items.Item(gs_EdtChqNmro).Enabled = false;
                }
            }
        }

        private void sb_FormUnload()
        {
            go_Form = null;
            Dispose();
        }

        private void sb_GenerarCodigoCajaChica(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            string ls_CodCCH = string.Empty;
            string ls_Qry = string.Empty;
            SAPbobsCOM.Recordset lo_RecSet = null;
            try
            {
                go_Form.Freeze(true);
                go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                go_Matrix.GetLineData(po_ItmEvnt.Row);
                ls_CodCCH = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHCod, po_ItmEvnt.Row - 1).Trim();
                if (ls_CodCCH != string.Empty)
                {
                    //Si es una apertura genero en numero correlativo
                    if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflTpoAprt, 0).Trim() == "1")
                    {
                        go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmNmrCCH, po_ItmEvnt.Row - 1, Cls_QueriesManager_CCH.fn_GenerarCodigoCCH(ls_CodCCH));
                    }
                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).SetValue(gs_UflClmEstado, po_ItmEvnt.Row - 1, "A");
                    go_Matrix.LoadFromDataSource();
                    go_Matrix.SetCellFocus(po_ItmEvnt.Row, 6);
                    if (po_ItmEvnt.Row == go_Matrix.RowCount)
                    {
                        go_Matrix.AddRow();
                    }
                    go_Matrix.ClearRowData(go_Matrix.RowCount);
                    go_Matrix.FlushToDataSource();
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                go_Form.Freeze(false);
                lo_RecSet = null;
            }
        }

        public bool fn_HandleFormDataEvent(SAPbouiCOM.BusinessObjectInfo po_BsnssObjInf)
        {
            bool lb_Result = true;
            switch (po_BsnssObjInf.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                    if (!po_BsnssObjInf.BeforeAction)
                    {
                        if (po_BsnssObjInf.ActionSuccess)
                        {
                            if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).GetValue(gs_UflTpoAprt, 0).Trim() == "2")
                            {
                                for (int i = 0; i < go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).Size; i++)
                                {
                                    if (go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHTrs, i).Trim() != string.Empty &&
                                        go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHTrs, i).Trim() != "---")
                                    {
                                        Cls_QueriesManager_CCH.sb_ActualizarEstadoPorReaperturaCCH(go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmCCHCod, i).Trim(), go_Form.DataSources.DBDataSources.Item(gs_DtdCCHAPRDET).GetValue(gs_UflClmNmCCHR, i).Trim());
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            return lb_Result;
        }

        private bool fn_ValidarPermisos(string ps_NroCCH, string ps_User)
        {
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            bool lb_Result = true;
            SAPbobsCOM.Recordset lo_RecSet = null;

            lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            lo_ArrCad = Cls_QueriesManager_CCH.ValidarPermisosAperturaCCH.Split(new char[] { '?' });
            ls_Qry = lo_ArrCad[0].Trim() + ps_User + lo_ArrCad[1].Trim() + ps_NroCCH + lo_ArrCad[2].Trim();
            Cls_Global.WriteToFile(ls_Qry);
            lo_RecSet.DoQuery(ls_Qry);
            if (!lo_RecSet.EoF)
            {
                if (lo_RecSet.Fields.Item(0).Value != "Y")
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

        private bool fn_ValidarMontoMaximodeApertura(string ps_CodCCH, double ld_MntApr)
        {
            SAPbobsCOM.CompanyService lo_CmpSrv = null;
            SAPbobsCOM.GeneralService lo_GnrSrv = null;
            SAPbobsCOM.GeneralData lo_GnrDta = null;
            SAPbobsCOM.GeneralDataParams lo_GnrDtaPrms = null;
            double ld_MntMax = 0.0;

            lo_CmpSrv = go_SBOCompany.GetCompanyService();
            lo_GnrSrv = lo_CmpSrv.GetGeneralService("BPP_CAJASCHICAS");
            lo_GnrDtaPrms = lo_GnrSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
            lo_GnrDtaPrms.SetProperty("Code", ps_CodCCH);
            lo_GnrDta = lo_GnrSrv.GetByParams(lo_GnrDtaPrms);
            ld_MntMax = lo_GnrDta.GetProperty("U_STR_MMXI");
            if (lo_GnrDta.GetProperty("U_STR_MMXS") == "Y")
            {
                if (ld_MntApr > ld_MntMax)
                {
                    return false;
                }
            }
            return true;
        }

        private void sb_ClearDataForm()
        {
            go_Matrix = go_Form.Items.Item(gs_MtxAprtCCH).Specific;
            go_Matrix.Clear();
            go_Matrix.FlushToDataSource();
            go_Matrix.AddRow();
            go_Matrix.FlushToDataSource();
            go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMntAprCCH, 0, string.Empty);
            go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflMntTotTrs, 0, string.Empty);
            go_Form.DataSources.DBDataSources.Item(gs_DtcCCHAPR).SetValue(gs_UflTotAprCCH, 0, string.Empty);
        }
    }
}
