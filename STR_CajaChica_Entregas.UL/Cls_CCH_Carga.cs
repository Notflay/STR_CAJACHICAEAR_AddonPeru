using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;
using STR_CajaChica_Entregas.BL;

namespace STR_CajaChica_Entregas.UL
{
    class Cls_CCH_Carga : Cls_Global_Controles
    {
        private SAPbouiCOM.Form go_Form = null;
        private SAPbouiCOM.Application go_SBOApplication = null;
        private SAPbobsCOM.Company go_SBOCompany = null;
        //Ruta del Formulario
        private string gs_RutaForm = "Resources/CajaChicaEAR/FrmCargarDocumentos.srf";
        //Nombre unico del formulario
        public const string gs_NomForm = "FrmCrgCCH";
        //* * * * * * * * * * * * * * * Menus* * * * * * * * * * * * * * * * * 
        public const string gs_MnuCrgCCH = "MNU_CCH_CARGAR";
        public const string gs_MnuCerrarCarga = "MNU_CERCRG";
        private string gs_MnuAñadirFila = "1292";
        private string gs_MnuBorrarFila = "1293";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * * DataSources* * * * * * * * * * * * * * *
        private const string gs_DtcCCHCRG = "@STR_CCHCRG";
        private const string gs_DtdCCHCRGDET = "@STR_CCHCRGDET";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        //* * * * * * * * * * * * * User Fields - @STR_CCHCRG* * * * * * * * * *
        private string gs_UflFchCre = "U_CC_FCRG";
        private string gs_UflCCHNmb = "U_CC_NMBR";
        private string gs_UflCCHNmr = "U_CC_NMRO";
        private string gs_UflSldIni = "U_CC_SLDI";
        private string gs_UflCCHMnd = "U_CC_MNDA";
        private string gs_UflCCHTtDc = "U_CC_TTDC";
        //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        //* * * * * * * * * * * * * User Fields - @STR_CCHCRGDET* * * * * * * * * *
        private string gs_UflDetSelecc = "U_CC_SLCC";
        private string gs_UflDetCdgPrv = "U_CC_CDPV";
        private string gs_UflDetDocFch = "U_CC_FCDC";
        private string gs_UflDetCntFch = "U_CC_FCCT";
        private string gs_UflDetDocMnd = "U_CC_MNDC";
        private string gs_UflDetDocTpo = "U_CC_TDOC";
        private string gs_UflDetDocSri = "U_CC_SDOC";
        private string gs_UflDetDocCor = "U_CC_CDOC";
        private string gs_UflDetDocCls = "U_CC_CLDC";
        private string gs_UflDetCdgDscArt = "U_CC_DSAR";
        private string gs_UflDetCdgArt = "U_CC_CDAR";
        private string gs_UflDetCntArt = "U_CC_CNAR";
        private string gs_UflDetPrcUni = "U_CC_PRPU";
        private string gs_UflDetAlmArt = "U_CC_ALAR";
        private string gs_UflDetCodCta = "U_CC_CSYS";
        private string gs_UflDetDocImp = "U_CC_IMPD";
        private string gs_UflDetDocRtn = "U_CC_RTNC";
        private string gs_UflDetDocEst = "U_CC_ESTD";
        private string gs_UflDetTotLin = "U_CC_TTLN";
        private string gs_UflDetDsct = "U_CC_DSCT";
        // Seervicio
        private string gs_UflDetUndMed = "U_CC_UMAR";
        private string gs_UflDetCdgServ = "U_CC_DSSR";
        private string gs_UflCuntSegmen = "U_CC_NMCT";
        private string gs_UflNomCuenta = "U_CC_DSCT";
        private string gs_UflCodCuenta = "U_CC_CSYS";
        ////* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

        ////* * * * * * * * * * * * User Data Sources * * * * * * * * * * * * * *
        private String gs_UDSTotSinImp = "DSTSI";
        private String gs_UDSTotImpsts = "DSTIM";
        private String gs_UDSTotPorCnt = "DSTTC";
        private String gs_UDSSaldoCaja = "DSSLD";
        private String gs_UDSSaldoAprt = "DSSAP";
        private String gs_UDSMntTtPgos = "DSTTPG";
        private String gs_UDSSaldCaja2 = "DSSLD2";
        ////* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

        //Controles UI
        //Combobox
        private string gs_CmbCCHNmr = "cmbNmro";
        private string gs_CmbSerie = "cmbSerie";
        //Matrix 
        private string gs_MtxDocs = "MtxDocs";
        //Grid
        private string gs_GrdDocs = "GrdDocs";
        //EditText
        private string gs_EdtNmbCCH = "txtNmbr";
        private string gs_EdtDocEnt = "txtDocEnt";
        private string gs_EdtFchCnt = "txtFchCrg";
        //Columnas Matrix
        private string gs_ClmMtxCodPrv = "clmCodPrv";
        private string gs_ClmMtxNomPrv = "clmNomPrv";
        private string gs_ClmMtxCodArt = "clmCodArt";
        private string gs_ClmMtxNomArt = "clmDscArt";
        private string gs_ClmMtxAlmArt = "clmAlmArt";
        private string gs_ClmMtxUniMed = "clmUniMed";
        private string gs_ClmMtxTpoDoc = "clmTpoDcm";
        private string gs_ClmMtxSreDoc = "clmSreDcm";
        private string gs_ClmMtxCorDoc = "clmCorDcm";
        private string gs_ClmMtxClsDcm = "clmClsDcm";
        private string gs_ClmMtxCodCta = "clmCodCta";
        private string gs_ClmMtxNroCta = "clmNroCta";
        private string gs_ClmMtxNmbCta = "clmNmbCta";
        private string gs_ClmMtxDscSrv = "clmDscSrv";
        private string gs_ClmMtxPartid = "clmCmp1";
        private string gs_ClmMtxMoneda = "clmMnds";
        private string gs_ClmMtxFchDcm = "clmFchDcm";
        private string gs_ClmMtxPrcUni = "clmPrcUni";
        private string gs_ClmMtxEstCre = "clmEstCre";
        private string gs_ClmMtxChsFlw = "clmCshFlw";
        private string gs_ClmMtxTotLna = "clmTtLn";
        private string gs_ClmMtxImpDcm = "clmImpDcm";
        private string gs_ClmMtxCntArt = "clmCntArt";
        //ChooseFromList
        private string gs_CFLNmmCCH = "CFLCCH";
        private string gs_CFLCodPrv = "CFLCODPRV";
        private string gs_CFLNomPrv = "CFLNOMPRV";
        private string gs_CFLCodArt = "CFLCODART";
        private string gs_CFLNomArt = "CFLNOMART";
        private string gs_CFLCodImp = "CFLCODIMP";
        private string gs_CFLCodAlm = "CFLCODALM";
        private string gs_CFLCodCta = "CFLCODCTA";
        private string gs_CFLProyec = "CFLCODPRY";
        private string gs_CFLDimen1 = "CFLDIM1";
        private string gs_CFLDimen2 = "CFLDIM2";
        private string gs_CFLDimen3 = "CFLDIM3";
        private string gs_CFLDimen4 = "CFLDIM4";
        private string gs_CFLDimen5 = "CFLDIM5";
        //Button
        private string gs_BtnAñadir = "1";
        private string gs_BtnContab = "btnCntb";
        //Static
        private string gs_SttMndCCH = "lblMndCCH";
        private string gs_SttTpoSld = "lblTpoSld";
        private string gs_SttDscCCH = "lblDscCCH";

        //Variables Globales
        string ls_ImpDcm = string.Empty;
        int gi_RowRightClick = -1;

        public Cls_CCH_Carga()
        {
            go_SBOApplication = Cls_Global.go_SBOApplication;
            go_SBOCompany = Cls_Global.go_SBOCompany;
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
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;


            string ls_Qry = string.Empty;
            try
            {
                go_Form.Freeze(true);

                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                lo_CmpSrv = go_SBOCompany.GetCompanyService();
                lo_DmnsSrv = lo_CmpSrv.GetBusinessService(SAPbobsCOM.ServiceTypes.DimensionsService);

                go_Form.DataBrowser.BrowseBy = gs_EdtDocEnt;
                go_Form.EnableMenu("1283", false);

                go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
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
                //go_Matrix.Columns.Item("clmPrcUni").Visible = false;
                go_Matrix.Columns.Item("clmDocEnt").Visible = false;
                go_Matrix.Columns.Item("clmEstCre").Visible = false;
                //go_Matrix.Columns.Item("clmTtLn").Visible = false;
                //go_Matrix.Columns.Item("Col_19").Visible = false;
                go_Matrix.Columns.Item("clmCodCta").Visible = false;
                //go_Matrix.Columns.Item("clmNroCta").Visible = false;
                //go_Matrix.Columns.Item("clmCmp1").Visible = true;
                //go_Matrix.Columns.Item("clmNmbCta").Visible = true;
                go_Matrix.Columns.Item("clmDscSrv").Visible = false;
                go_Matrix.Columns.Item("clmClsDcm").Editable = true;
                go_Matrix.Columns.Item("clmCntArt").Editable = true;
                sb_AddUserColumnsToMatrix();
                go_Matrix.AddRow();

                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflFchCre, 0, go_SBOCompany.GetCompanyDate().ToString("yyyyMMdd"));
                ls_Qry = Cls_QueriesManager_CCH.TiposDeDocumentos;
                Cls_Global.WriteToFile(ls_Qry);
                lo_RecSet.DoQuery(ls_Qry);
                go_Combo = go_Matrix.Columns.Item(gs_ClmMtxTpoDoc).Cells.Item(1).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, lo_RecSet);
                go_Combo = go_Matrix.Columns.Item(gs_ClmMtxMoneda).Cells.Item(1).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_MonedasSociedad());
                go_Combo = go_Matrix.Columns.Item(gs_ClmMtxChsFlw).Cells.Item(1).Specific;
                Cls_Global.sb_CargarCombo(go_Combo, Cls_QueriesManager_CCH.fn_ListaFlujodeCaja());
                go_Combo.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
                sb_AddStandarDataToNewRow(go_Matrix.RowCount);
                go_Matrix.LoadFromDataSource();

                #region ChooseFromList
                if (gs_CFLNmmCCH == "CFLCCH")
                {
                    lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLNmmCCH);
                    lo_Cnds = lo_CFL.GetConditions();
                    lo_Cnd = lo_Cnds.Add();
                    lo_Cnd.Alias = "U_BPP_TIPR";
                    lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    lo_Cnd.CondVal = "CCH";
                    lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                    lo_Cnd = lo_Cnds.Add();
                    lo_Cnd.Alias = "U_BPP_STAD";
                    lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                    lo_Cnd.CondVal = "A";
                    lo_CFL.SetConditions(lo_Cnds);
                }

                //ChooseFromList Dimencion1
                lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLDimen1);
                lo_Cnds = lo_CFL.GetConditions();
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DimCode";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "1";
                lo_CFL.SetConditions(lo_Cnds);

                //ChooseFromList Dimencion2
                lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLDimen2);
                lo_Cnds = lo_CFL.GetConditions();
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DimCode";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "2";
                lo_CFL.SetConditions(lo_Cnds);

                //ChooseFromList Dimencion3
                lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLDimen3);
                lo_Cnds = lo_CFL.GetConditions();
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DimCode";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "3";
                lo_CFL.SetConditions(lo_Cnds);

                //ChooseFromList Dimencion4
                lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLDimen4);
                lo_Cnds = lo_CFL.GetConditions();
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DimCode";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "4";
                lo_CFL.SetConditions(lo_Cnds);

                //ChooseFromList Dimencion5
                lo_CFL = go_Form.ChooseFromLists.Item(gs_CFLDimen5);
                lo_Cnds = lo_CFL.GetConditions();
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DimCode";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "5";
                lo_CFL.SetConditions(lo_Cnds);
                #endregion

                go_Form.Items.Item(gs_BtnContab).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_True);
                go_Form.Items.Item(gs_EdtNmbCCH).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
                go_Form.Items.Item(gs_CmbCCHNmr).SetAutoManagedAttribute(SAPbouiCOM.BoAutoManagedAttr.ama_Editable, (int)SAPbouiCOM.BoAutoFormMode.afm_Ok, SAPbouiCOM.BoModeVisualBehavior.mvb_False);
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

        public void sb_DataFormLoadAdd()
        {
            string ls_Serie = string.Empty;

            go_Combo = go_Form.Items.Item(gs_CmbSerie).Specific;
            go_Combo.ValidValues.LoadSeries(go_Form.BusinessObject.Type, SAPbouiCOM.BoSeriesMode.sf_Add);
            if (go_Combo.Selected == null && go_Combo.ValidValues.Count > 0)
            {
                ls_Serie = go_Combo.ValidValues.Item(0).Value;
            }
            else
            {
                ls_Serie = go_Combo.Selected.Value;
            }
            go_Form.Items.Item(gs_MtxDocs).Enabled = false;
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(0).TableName).SetValue("Series", 0, ls_Serie);
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(0).TableName).SetValue(gs_UflFchCre, 0, go_SBOCompany.GetCompanyDate().ToString("yyyyMMdd"));
            ((SAPbouiCOM.StaticText)go_Form.Items.Item("lblDscCCH").Specific).Caption = string.Empty;
            go_Form.DataSources.UserDataSources.Item(gs_UDSSaldoAprt).Value = string.Empty;
            go_Form.DataSources.UserDataSources.Item(gs_UDSSaldoCaja).Value = string.Empty;
            this.sb_GetNextDocumentNumber();
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
                case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE:
                    lb_Result = this.fn_HandleFormResize(po_ItmEvnt);
                    break;
                    //case SAPbouiCOM.BoEventTypes.et_CLICK:
                    //  lb_Result = this.fn_HandleClick(po_ItmEvnt);
                    //   break;

            }
            return lb_Result;
        }

        private bool fn_HandleFormResize(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            if (!po_ItmEvnt.BeforeAction && go_Form != null)
            {
                try
                {
                    go_Form.Freeze(true);
                    go_Form.Items.Item("Item_0").Width = go_Form.Width - 40;
                    go_Form.Items.Item("Item_0").Height = go_Form.Height - 160;
                }
                catch (Exception ex)
                {
                    Cls_Global.WriteToFile(ex.Message);
                }
                finally
                {
                    go_Form.Freeze(false);
                }
            }
            return true;
        }

        private bool fn_HandleValidate(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            SAPbobsCOM.SalesTaxCodes lo_SlTxCds = null;
            double ld_CntArt = 0.0;
            double ld_PrcUni = 0.0;
            double ld_TotLna = 0.0;
            double ld_PrcTot = 0.0;
            bool lb_Result = true;
            string ls_Tpo = string.Empty;
            string ls_Sre = string.Empty;
            string ls_Cor = string.Empty;
            string ls_NumUni = string.Empty;
            string ls_CodPrv = string.Empty;

            if (po_ItmEvnt.ItemUID != string.Empty && go_Form != null)
            {
                switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                        if (po_ItmEvnt.BeforeAction)
                        {
                            if (po_ItmEvnt.ColUID == gs_ClmMtxCorDoc)
                            {
                                go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
                                go_Matrix.FlushToDataSource();
                                ls_CodPrv = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetCdgPrv, po_ItmEvnt.Row - 1).Trim();
                                ls_Tpo = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocTpo, po_ItmEvnt.Row - 1).Trim();
                                ls_Sre = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocSri, po_ItmEvnt.Row - 1).Trim();
                                ls_Cor = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocCor, po_ItmEvnt.Row - 1).Trim();
                                ls_NumUni = ls_Tpo.PadLeft(2, '0');
                                ls_NumUni += ls_Sre.PadLeft(4, '0');
                                ls_NumUni += ls_Cor.PadLeft(15, '0');
                                lb_Result = fn_ValidarNumeroUnicoDocumento(ls_CodPrv, ls_NumUni);
                            }
                            if (po_ItmEvnt.ColUID == gs_ClmMtxPrcUni || po_ItmEvnt.ColUID == gs_ClmMtxCntArt)
                            {
                                //lo_SlTxCds = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oSalesTaxCodes);
                                ld_CntArt = Convert.ToDouble(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxCntArt, po_ItmEvnt.Row)).Value.Trim());
                                ld_PrcUni = Convert.ToDouble(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxPrcUni, po_ItmEvnt.Row)).Value.Trim());
                                ///lo_SlTxCds.GetByKey(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxImpDcm, po_ItmEvnt.Row)).Value.Trim());
                                //ld_PrcImp = lo_SlTxCds.Rate / 100;
                                ld_PrcTot = ld_PrcUni * ld_CntArt;
                                go_Matrix.SetCellWithoutValidation(po_ItmEvnt.Row, gs_ClmMtxTotLna, ld_PrcTot.ToString());
                                sb_LoadAmountGrid();
                            }
                            if (po_ItmEvnt.ColUID == gs_ClmMtxTotLna)
                            {
                                ld_CntArt = Convert.ToDouble(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxCntArt, po_ItmEvnt.Row)).Value.Trim());
                                ld_TotLna = Convert.ToDouble(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxTotLna, po_ItmEvnt.Row)).Value.Trim());
                                ld_PrcUni = ld_TotLna / ld_CntArt;
                                go_Matrix.SetCellWithoutValidation(po_ItmEvnt.Row, gs_ClmMtxPrcUni, ld_PrcUni.ToString());
                                sb_LoadAmountGrid();
                            }
                        }
                        break;
                }
            }
            return lb_Result;
        }

        private bool fn_HandleItemPressed(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            int li_CodErr = 0;
            string ls_DscErr = string.Empty;
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null; ;

            if (po_ItmEvnt.ItemUID != string.Empty && go_Form != null)
            {
                switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                {
                    case SAPbouiCOM.BoFormItemTypes.it_BUTTON:

                        if (po_ItmEvnt.ItemUID == gs_BtnContab)
                        {
                            if (po_ItmEvnt.BeforeAction)
                            {
                                if (Cls_QueriesManager_CCH.fn_ValidarPermisosContabilizarCCH(go_SBOCompany.UserName, go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim()) != "Y")
                                {
                                    go_SBOApplication.StatusBar.SetText("No tiene permiso para relizar la contabilizacion de estos documentos...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                                }
                                else
                                {
                                    try
                                    {
                                        lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                        lo_Cnds = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
                                        lo_Cnd = lo_Cnds.Add();
                                        lo_Cnd.Alias = "DocEntry";
                                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                                        lo_Cnd.CondVal = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("DocEntry", 0);
                                        (go_Form.Items.Item(gs_MtxDocs).Specific as SAPbouiCOM.Matrix).FlushToDataSource();
                                        if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                                        {
                                            go_Form.Items.Item(gs_BtnAñadir).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                            return lb_Result = true;
                                        }
                                        (go_Form.Items.Item(gs_MtxDocs).Specific as SAPbouiCOM.Matrix).FlushToDataSource();
                                        for (int i = 0; i < go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).Size; i++)
                                        {
                                            if (go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocEst, i).Trim().ToUpper() != "ERR") continue;
                                            lo_ArrCad = Cls_QueriesManager_CCH.ActualizarEstadodeCreacion.Split(new char[] { '?' });
                                            ls_Qry = lo_ArrCad[0].Trim() + string.Empty + lo_ArrCad[1].Trim() + "CRE" + lo_ArrCad[2].Trim() + go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("DocEntry", 0).Trim() + lo_ArrCad[3].Trim() + go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue("LineId", i).Trim() + lo_ArrCad[4].Trim();
                                            Cls_Global.WriteToFile(ls_Qry);
                                            lo_RecSet.DoQuery(ls_Qry);
                                        }
                                        go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).Query(lo_Cnds);
                                        Cls_CCH_Cargar_BL.sb_GenerarDocumentosyPagos(ref go_Form, ref li_CodErr, ref ls_DscErr);
                                        sb_SaldoCajaChica();
                                        sb_UpdateDataMatrix();
                                        //Si el saldo es 0 entonces se cierra la caja
                                        if (Convert.ToDouble(go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflSldIni, 0)) == 0.0)
                                        {
                                            go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue("Status", 0, "C");
                                            Cls_QueriesManager_CCH.sb_ActualizarEstadoySaldoXNroCCH("C", 0.0, go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim(),
                                                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmr, 0).Trim());
                                            if (go_Form.Mode != SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                                            {
                                                go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                                go_Form.Items.Item(gs_BtnAñadir).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                            }
                                            go_Form.Items.Item(gs_MtxDocs).Enabled = false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Cls_Global.WriteToFile(ex.Message);
                                        go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
                                    }
                                }
                            }
                            else
                            {
                                sb_SetRowFontColor();
                            }
                        }
                        if (po_ItmEvnt.ItemUID == gs_BtnAñadir)
                        {
                            if (po_ItmEvnt.BeforeAction && (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_UPDATE_MODE || go_Form.Mode == SAPbouiCOM.BoFormMode.fm_ADD_MODE))
                            {
                                lb_Result = fn_ValidacionesGenerales();
                                if (lb_Result)
                                {
                                    go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
                                    if (go_Matrix.RowCount > 1)
                                    {
                                        if (((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxCodPrv, go_Matrix.RowCount)).Value == string.Empty)
                                        {
                                            go_Matrix.DeleteRow(go_Matrix.RowCount);
                                        }
                                    }
                                    go_Matrix.FlushToDataSource();
                                }
                            }
                        }
                        break;
                    case SAPbouiCOM.BoFormItemTypes.it_FOLDER:
                        if (!po_ItmEvnt.BeforeAction)
                        {
                            if (go_Form.PaneLevel == 2)
                            {
                                go_Form.Items.Item(gs_BtnContab).Visible = false;
                                go_Form.Items.Item("lblSldIni").Visible = false;
                                go_Form.Items.Item("txtSldIni").Visible = false;
                                sb_LoadDataGrid();
                            }
                            if (go_Form.PaneLevel == 1)
                            {
                                if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("DocEntry", 0).Trim() != string.Empty)
                                    this.sb_InfoTotalesPorCarga();
                                go_Form.Items.Item(gs_BtnContab).Visible = true;
                                go_Form.Items.Item("lblSldIni").Visible = true;
                                go_Form.Items.Item("txtSldIni").Visible = true;
                            }
                        }
                        break;
                }
            }
            return lb_Result;
        }

        private void sb_LoadAmountGrid()
        {
            try
            {
                double ld_mntTotDoc = 0.0;
                double ld_mntTotSinImp = 0.0;
                double ld_mntTotImpsts = 0.0;

                string monedaForm = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHMnd, 0);

                for (int i = 0; i < go_Matrix.RowCount; i++)
                {
                    string ls_impuesto = ((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxImpDcm, i + 1)).Value;
                    double valorTotalLinea = Convert.ToDouble(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxTotLna, i + 1)).Value);
                    string moneda = ((SAPbouiCOM.ComboBox)go_Matrix.GetCellSpecific(gs_ClmMtxMoneda, i + 1)).Value;
                    string fechaContabilzia = DateTime.ParseExact(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxFchDcm, i + 1)).Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                    if (!string.IsNullOrEmpty(ls_impuesto) && valorTotalLinea != 0 && !string.IsNullOrEmpty(moneda) && !string.IsNullOrEmpty(fechaContabilzia))
                    {
                        try
                        {
                            (double a, double b, double c) = Fn_InfoTotalPorActualizar(ls_impuesto, valorTotalLinea, moneda, fechaContabilzia, monedaForm);
                            ld_mntTotSinImp += a;
                            ld_mntTotImpsts += b;
                            ld_mntTotDoc += c;
                        }
                        catch (Exception ex)
                        {
                            Cls_Global.WriteToFile($"sb_LoadAmountGrid - {ex.Message}");

                            //throw;
                        }
                    }
                    //string ls_impuesto = ((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxImpDcm, i + 1)).Value;
                    //string moneda = ((SAPbouiCOM.ComboBox)go_Matrix.GetCellSpecific(gs_ClmMtxMoneda, i + 1)).Value;
                    //double valorTotalLinea = Convert.ToDouble(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxTotLna, i + 1)).Value);
                    //string fechaContabilzia = DateTime.ParseExact(((SAPbouiCOM.EditText)go_Matrix.GetCellSpecific(gs_ClmMtxFchDcm, i + 1)).Value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

                    //double tipoCambioMoneda = moneda == "SOL" ? 1.0 : Convert.ToDouble(Cls_QueriesManager_EAR.fn_obtieneTipoCambio(moneda, fechaContabilzia).Fields.Item(0).Value);
                    //double tipoCambioMonedaForm = monedaForm == "SOL" ? 1.0 : Convert.ToDouble(Cls_QueriesManager_EAR.fn_obtieneTipoCambio(monedaForm, fechaContabilzia).Fields.Item(0).Value);

                    //if (ls_impuesto == "EXO")
                    //{
                    //    ld_mntTotSinImp += valorTotalLinea * tipoCambioMoneda / tipoCambioMonedaForm;
                    //}
                    //else
                    //{
                    //    double impuesto = Convert.ToDouble(Cls_QueriesManager_EAR.fn_obtieneImpuesto(ls_impuesto).Fields.Item(0).Value);
                    //    double valorImpuesto = (valorTotalLinea * impuesto) / (impuesto + 100);
                    //    ld_mntTotSinImp += (valorTotalLinea - valorImpuesto) * tipoCambioMoneda / tipoCambioMonedaForm;
                    //    ld_mntTotImpsts += valorImpuesto * tipoCambioMoneda / tipoCambioMonedaForm;
                    //}

                    //ld_mntTotDoc += valorTotalLinea;
                }

                go_Form.Items.Item("Item_9").Specific.Value = ld_mntTotSinImp;      // Sin impuestos
                go_Form.Items.Item("Item_5").Specific.Value = ld_mntTotImpsts;      // Con impuestos
                go_Form.Items.Item("txtTotCnt").Specific.Value = ld_mntTotDoc.ToString();
                go_Form.Items.Item("txtSldFin").Specific.Value = (Convert.ToDouble(go_Form.Items.Item("txtSldIni").Specific.Value) - ld_mntTotDoc).ToString();
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile($"sb_LoadAmountGrid - {ex.Message}");
            }
        }

        private void sb_LoadDataGrid()
        {
            string ls_CodCCH = string.Empty;
            string ls_NmrCCH = string.Empty;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            double ld_MntTotDoc = 0.0;
            SAPbouiCOM.EditTextColumn lo_EdtTxtClm = null;
            try
            {
                go_Form.Freeze(true);
                ls_CodCCH = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim();
                ls_NmrCCH = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmr, 0).Trim();
                go_Grid = go_Form.Items.Item(gs_GrdDocs).Specific;
                lo_ArrCad = Cls_QueriesManager_CCH.PagosPorNrosCCH.Split(new char[] { '?' });
                ls_Qry = lo_ArrCad[0].Trim() + ls_CodCCH + lo_ArrCad[1].Trim() + ls_NmrCCH + lo_ArrCad[2].Trim() + "CCH" + lo_ArrCad[3].Trim();
                Cls_Global.WriteToFile(ls_Qry);
                go_Grid.DataTable.ExecuteQuery(ls_Qry);
                lo_EdtTxtClm = (SAPbouiCOM.EditTextColumn)go_Grid.Columns.Item("DED");
                lo_EdtTxtClm.LinkedObjectType = "18";
                lo_EdtTxtClm = (SAPbouiCOM.EditTextColumn)go_Grid.Columns.Item("DEP");
                lo_EdtTxtClm.LinkedObjectType = "46";
                for (int i = 0; i < go_Grid.DataTable.Rows.Count; i++)
                {
                    ld_MntTotDoc += go_Grid.DataTable.Columns.Item("Importe Pagado").Cells.Item(i).Value;
                }
                go_Form.DataSources.UserDataSources.Item(gs_UDSMntTtPgos).Value = ld_MntTotDoc.ToString();
                go_Form.DataSources.UserDataSources.Item(gs_UDSSaldCaja2).Value = (Convert.ToDouble(go_Form.DataSources.UserDataSources.Item(gs_UDSSaldoAprt).Value) - ld_MntTotDoc).ToString();
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

        private bool fn_HandleComboSelect(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            SAPbobsCOM.Recordset lo_RecSet = null;
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;

            try
            {
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                if (po_ItmEvnt.ItemUID != string.Empty && go_Form != null)
                {
                    switch (go_Form.Items.Item(po_ItmEvnt.ItemUID).Type)
                    {
                        case SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX:
                            if (po_ItmEvnt.ItemUID == gs_CmbCCHNmr)
                            {
                                if (!po_ItmEvnt.BeforeAction)
                                {
                                    go_Combo = go_Form.Items.Item(gs_CmbCCHNmr).Specific;
                                    if (fn_ValidarCantidadNrosCCH(go_Combo.Value.Trim()))
                                    {
                                        lo_ArrCad = Cls_QueriesManager_CCH.SaldoCajaChica.Split(new char[] { '?' });
                                        ls_Qry = lo_ArrCad[0].Trim() + go_Combo.Value.Trim() + lo_ArrCad[1].Trim();
                                        Cls_Global.WriteToFile(ls_Qry);
                                        lo_RecSet.DoQuery(ls_Qry);
                                        if (!lo_RecSet.EoF)
                                        {
                                            go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflSldIni, 0, lo_RecSet.Fields.Item(0).Value);
                                            string VALOR = lo_RecSet.Fields.Item(1).Value;
                                            go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflCCHMnd, 0, lo_RecSet.Fields.Item(1).Value);
                                            go_Static = go_Form.Items.Item(gs_SttMndCCH).Specific;
                                            go_Static.Caption = lo_RecSet.Fields.Item(1).Value;
                                            go_Form.Items.Item(gs_MtxDocs).Enabled = true;
                                        }
                                        else
                                        {
                                            go_SBOApplication.SetStatusBarMessage("Consulta (STR_SP_SaldoNumerosCajaChica) sin resultados...", SAPbouiCOM.BoMessageTime.bmt_Short);
                                        }
                                        lo_RecSet = Cls_QueriesManager_CCH.fn_MontodeAperturaNmroCCH(go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim(), go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmr, 0).Trim());
                                        if (lo_RecSet != null) go_Form.DataSources.UserDataSources.Item(gs_UDSSaldoAprt).Value = Convert.ToString(lo_RecSet.Fields.Item(0).Value);
                                    }
                                    else
                                    {
                                        go_Form.Items.Item(gs_MtxDocs).Enabled = false;
                                    }
                                }
                            }
                            break;
                        case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                            if (po_ItmEvnt.ColUID == gs_ClmMtxClsDcm)
                            {
                                if (!po_ItmEvnt.BeforeAction)
                                {
                                    sb_OcultarMostrarColumnasXClaseDoc(po_ItmEvnt.Row);
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

        private bool fn_HandleChooseFromList(SAPbouiCOM.ItemEvent po_ItmEvnt)
        {
            bool lb_Result = true;
            System.Windows.Forms.DialogResult lo_Resultado;
            SAPbouiCOM.ChooseFromListEvent lo_CFLEvnt = null;
            SAPbouiCOM.ChooseFromList lo_CFL = null;
            SAPbouiCOM.DataTable lo_DataTable = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbobsCOM.Items lo_Item = null;

            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;

            try
            {
                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE) return lb_Result;
                go_Form.Freeze(true);
                go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
                lo_CFLEvnt = (SAPbouiCOM.ChooseFromListEvent)po_ItmEvnt;
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                lo_Item = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

                if (lo_CFLEvnt.ChooseFromListUID == gs_CFLNmmCCH)
                {
                    if (!lo_CFLEvnt.BeforeAction)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            if (fn_ValidarPermisos(lo_DataTable.GetValue(0, 0), go_SBOCompany.UserName))
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflCCHNmb, 0, lo_DataTable.GetValue(0, 0));
                                go_Combo = go_Form.Items.Item(gs_CmbCCHNmr).Specific;
                                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflCCHNmr, 0, string.Empty);
                                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflSldIni, 0, string.Empty);
                                ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttDscCCH).Specific).Caption = lo_DataTable.GetValue(1, 0);
                                go_Static = go_Form.Items.Item(gs_SttMndCCH).Specific;
                                go_Static.Caption = string.Empty;
                                lo_ArrCad = Cls_QueriesManager_CCH.NumerosdeCCHActivos.Split(new char[] { '?' });
                                ls_Qry = lo_ArrCad[0].Trim() + lo_DataTable.GetValue(0, 0) + lo_ArrCad[1].Trim();
                                Cls_Global.WriteToFile(ls_Qry);
                                lo_RecSet.DoQuery(ls_Qry);
                                Cls_Global.sb_CargarCombo(go_Combo, lo_RecSet);
                                sb_AddStandarDataToNewRow(1);
                                ((SAPbouiCOM.Matrix)go_Form.Items.Item(gs_MtxDocs).Specific).LoadFromDataSource();
                            }
                            else
                            {
                                go_SBOApplication.SetStatusBarMessage("No tiene permisos para realizar operaciones con esta caja...", SAPbouiCOM.BoMessageTime.bmt_Short);
                                go_Form.Items.Item(gs_MtxDocs).Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        if (go_Matrix.RowCount > 1)
                        {
                            go_Form.Freeze(false);
                            lo_Resultado = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("Con las modificaciones se borrarán los datos ingresados.¿Desea continuar?", 1, "Si", "No");
                            go_Form.Freeze(true);
                            if (lo_Resultado == System.Windows.Forms.DialogResult.OK)
                            {
                                go_Matrix.Clear();
                                go_Matrix.FlushToDataSource();
                            }
                            else return lb_Result = false;
                        }
                    }
                }
                if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCodAlm || lo_CFLEvnt.ChooseFromListUID == gs_CFLCodImp || lo_CFLEvnt.ChooseFromListUID == gs_CFLProyec
                    || lo_CFLEvnt.ChooseFromListUID == gs_CFLDimen1 || lo_CFLEvnt.ChooseFromListUID == gs_CFLDimen2 || lo_CFLEvnt.ChooseFromListUID == gs_CFLDimen3
                    || lo_CFLEvnt.ChooseFromListUID == gs_CFLDimen4 || lo_CFLEvnt.ChooseFromListUID == gs_CFLDimen5)
                {
                    if (!lo_CFLEvnt.BeforeAction)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            go_Matrix.FlushToDataSource();
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(lo_CFLEvnt.ColUID).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(0, 0));
                            go_Matrix.LoadFromDataSource();
                            go_Matrix.Columns.Item(lo_CFLEvnt.ColUID).Cells.Item(lo_CFLEvnt.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE) go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                        }
                    }
                }
                if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCodPrv || lo_CFLEvnt.ChooseFromListUID == gs_CFLNomPrv || lo_CFLEvnt.ChooseFromListUID == gs_CFLCodArt
                    || lo_CFLEvnt.ChooseFromListUID == gs_CFLNomArt)
                {
                    if (!lo_CFLEvnt.BeforeAction)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            go_Matrix.FlushToDataSource();

                            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCodPrv || lo_CFLEvnt.ChooseFromListUID == gs_CFLNomPrv)
                            {
                                if (po_ItmEvnt.Row == go_Matrix.RowCount)
                                {
                                    this.sb_AddNewRowMatrix();
                                }
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxCodPrv).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(0, 0));
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxNomPrv).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(1, 0));
                                ls_ImpDcm = lo_DataTable.GetValue("VatGroup", 0);
                            }
                            if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCodArt || lo_CFLEvnt.ChooseFromListUID == gs_CFLNomArt)
                            {
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxCodArt).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(0, 0));
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxNomArt).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(1, 0));
                                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxUniMed).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue("BuyUnitMsr", 0));
                                //go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(gs_UflDetDocImp, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue("TaxCodeAP", 0));
                                //lo_Item.GetByKey(Convert.ToString(lo_DataTable.GetValue(0, 0)).Trim());
                                //if (lo_Item.WhsInfo.WarehouseCode != string.Empty)
                                //{
                                //    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(gs_UflDetAlmArt, lo_CFLEvnt.Row - 1, lo_Item.WhsInfo.WarehouseCode);
                                //}
                                if (lo_DataTable.GetValue("TaxCodeAP", 0) == string.Empty)
                                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(gs_UflDetDocImp, lo_CFLEvnt.Row - 1, ls_ImpDcm);
                                else
                                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(gs_UflDetDocImp, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue("TaxCodeAP", 0));
                                lo_Item.GetByKey(Convert.ToString(lo_DataTable.GetValue(0, 0)).Trim());
                                if (lo_Item.DefaultWarehouse != string.Empty)
                                {
                                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(gs_UflDetAlmArt, lo_CFLEvnt.Row - 1, lo_Item.DefaultWarehouse);
                                }
                            }
                            go_Matrix.LoadFromDataSource();
                            go_Matrix.Columns.Item(lo_CFLEvnt.ColUID).Cells.Item(lo_CFLEvnt.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE) go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                        }
                    }
                    else
                    {
                        if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCodArt || lo_CFLEvnt.ChooseFromListUID == gs_CFLNomArt)
                        {
                            lo_CFL = go_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                            lo_CFL.SetConditions(null);
                            lo_Cnds = lo_CFL.GetConditions();
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.Alias = "PrchseItem";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            lo_Cnd.CondVal = "Y";
                            lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.Alias = "U_BPP_ArCE";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            lo_Cnd.CondVal = "Y";
                            lo_CFL.SetConditions(lo_Cnds);
                        }
                        if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCodPrv || lo_CFLEvnt.ChooseFromListUID == gs_CFLNomPrv)
                        {
                            lo_CFL = go_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                            lo_CFL.SetConditions(null);
                            lo_Cnds = lo_CFL.GetConditions();
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.Alias = "CardType";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            lo_Cnd.CondVal = "S";
                            lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.BracketOpenNum = 1;
                            lo_Cnd.Alias = "U_CE_NOCE";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            lo_Cnd.CondVal = "N";
                            lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                            lo_Cnd = lo_Cnds.Add();
                            lo_Cnd.Alias = "U_CE_NOCE";
                            lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_IS_NULL;
                            lo_Cnd.BracketCloseNum = 1;
                            lo_CFL.SetConditions(lo_Cnds);
                        }
                    }
                }
                if (lo_CFLEvnt.ChooseFromListUID == gs_CFLCodCta)
                {
                    if (!lo_CFLEvnt.BeforeAction)
                    {
                        lo_DataTable = lo_CFLEvnt.SelectedObjects;
                        if (lo_DataTable != null)
                        {
                            go_Matrix.FlushToDataSource();
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxCodCta).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(0, 0));
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxNroCta).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue("FormatCode", 0));
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(go_Matrix.Columns.Item(gs_ClmMtxNmbCta).DataBind.Alias, lo_CFLEvnt.Row - 1, lo_DataTable.GetValue(1, 0));
                            go_Matrix.LoadFromDataSource();
                            go_Matrix.Columns.Item(lo_CFLEvnt.ColUID).Cells.Item(lo_CFLEvnt.Row).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                            if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE) go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                        }
                    }
                    else
                    {
                        lo_CFL = go_Form.ChooseFromLists.Item(lo_CFLEvnt.ChooseFromListUID);
                        lo_CFL.SetConditions(null);
                        lo_Cnds = lo_CFL.GetConditions();
                        lo_Cnd = lo_Cnds.Add();
                        lo_Cnd.Alias = "Postable";
                        lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        lo_Cnd.CondVal = "Y";
                        lo_CFL.SetConditions(lo_Cnds);
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
                go_Form.Freeze(false);
                lo_RecSet = null;
            }
            return lb_Result;
        }

        public bool fn_HandleRightClickEvent(SAPbouiCOM.ContextMenuInfo po_RghClkEvent)
        {
            bool lb_Result = true;
            try
            {
                if (po_RghClkEvent.ItemUID != string.Empty && go_Form != null)
                {
                    go_Form.EnableMenu(gs_MnuAñadirFila, false);
                    go_Form.EnableMenu(gs_MnuBorrarFila, false);
                    gi_RowRightClick = po_RghClkEvent.Row;
                    switch (go_Form.Items.Item(po_RghClkEvent.ItemUID).Type)
                    {
                        case SAPbouiCOM.BoFormItemTypes.it_MATRIX:
                            if (po_RghClkEvent.BeforeAction && go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("Status", 0) != "C")
                            {
                                go_Form.EnableMenu(gs_MnuAñadirFila, true);

                                if (po_RghClkEvent.Row > 0)
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
                if (go_Form != null && go_Form.Mode != SAPbouiCOM.BoFormMode.fm_ADD_MODE)
                {
                    sb_AddMenuCerrarCarga(po_RghClkEvent);
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            return lb_Result;
        }

        public void sb_AddNewRowMatrix()
        {
            try
            {
                go_Form.Freeze(true);
                go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
                go_Matrix.AddRow();
                go_Matrix.ClearRowData(go_Matrix.RowCount);
                go_Matrix.FlushToDataSource();
                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue("LineId", go_Matrix.RowCount - 1, string.Empty);
                sb_AddStandarDataToNewRow(go_Matrix.RowCount);
                go_Matrix.LoadFromDataSource();
                sb_SetRowFontColor();
                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                {
                    go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
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

        public bool fn_DeleteRowMatrix()
        {
            System.Windows.Forms.DialogResult lo_Resultado;
            lo_Resultado = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("¿Desea eliminar esta fila?", 1, "Si", "No");
            if (lo_Resultado == System.Windows.Forms.DialogResult.OK)
            {
                (go_Form.Items.Item(gs_MtxDocs).Specific as SAPbouiCOM.Matrix).DeleteRow(gi_RowRightClick);
                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                {
                    go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                }
                gi_RowRightClick = -1;
            }
            return false;
        }

        private void sb_FormUnload()
        {
            go_Form = null;
            Dispose();
        }

        public bool fn_HandleFormDataEvent(SAPbouiCOM.BusinessObjectInfo po_BsnssObjInf)
        {
            bool lb_Result = true;
            SAPbobsCOM.Recordset lo_RecSet = null;
            string ls_EstCCH = string.Empty;
            double ld_sldCCH = 0.0;

            switch (po_BsnssObjInf.EventType)
            {
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD:
                    if (!po_BsnssObjInf.BeforeAction)
                    {
                        if (po_BsnssObjInf.ActionSuccess)
                        {
                            SAPbobsCOM.CompanyService lo_CmpSrv = null;
                            SAPbobsCOM.GeneralService lo_GnrSrv = null;
                            SAPbobsCOM.GeneralData lo_GnrDta = null;
                            SAPbobsCOM.GeneralDataParams lo_GnrDtaPrms = null;

                            lo_CmpSrv = go_SBOCompany.GetCompanyService();
                            lo_GnrSrv = lo_CmpSrv.GetGeneralService("BPP_CAJASCHICAS");
                            lo_GnrDtaPrms = lo_GnrSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                            lo_GnrDtaPrms.SetProperty("Code", go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim());
                            lo_GnrDta = lo_GnrSrv.GetByParams(lo_GnrDtaPrms);
                            ((SAPbouiCOM.StaticText)go_Form.Items.Item(gs_SttDscCCH).Specific).Caption = lo_GnrDta.GetProperty("Name");
                            lo_RecSet = Cls_QueriesManager_CCH.fn_MontodeAperturaNmroCCH(go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim(), go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmr, 0).Trim());
                            if (lo_RecSet != null) go_Form.DataSources.UserDataSources.Item(gs_UDSSaldoAprt).Value = Convert.ToString(lo_RecSet.Fields.Item(0).Value);
                            if (go_Form.PaneLevel == 2)
                            {
                                sb_LoadDataGrid();
                            }
                            sb_UpdateDataMatrix();
                            sb_SetRowFontColor();
                            lo_RecSet = Cls_QueriesManager_CCH.fn_VerificarEstadoYSaldoXNroCCH(go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim(),
                                         go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmr, 0).Trim());
                            ld_sldCCH = lo_RecSet.Fields.Item(0).Value;
                            ls_EstCCH = lo_RecSet.Fields.Item(1).Value;
                            if (ls_EstCCH != "C" && ld_sldCCH > 0.0)
                            {
                                sb_SaldoCajaChica();
                                if (go_Form.Mode == SAPbouiCOM.BoFormMode.fm_OK_MODE)
                                {
                                    go_Form.Items.Item(gs_BtnContab).Enabled = true;
                                }
                            }
                            else
                            { //La Caja Chica fue traspasada se procede a cerrarla
                                if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("Status", 0).Trim() != "C")
                                {
                                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflSldIni, 0, string.Empty);
                                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue("Status", 0, "C");
                                    if (go_Form.Mode != SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                                    {
                                        go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                                        go_Form.Items.Item(gs_BtnAñadir).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                                    }
                                }
                            }
                            if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("Status", 0).Trim() == "C")
                            {
                                go_Form.Items.Item(gs_MtxDocs).Enabled = false;
                                go_Form.Items.Item(gs_EdtFchCnt).Enabled = false;
                                go_Form.Items.Item(gs_BtnContab).Enabled = false;
                            }
                            else
                            {
                                go_Form.Items.Item(gs_MtxDocs).Enabled = true;
                                go_Form.Items.Item(gs_EdtFchCnt).Enabled = true;
                                go_Form.Items.Item(gs_BtnContab).Enabled = true;
                            }
                            this.sb_InfoTotalesPorCarga();
                            go_Static = go_Form.Items.Item(gs_SttMndCCH).Specific;
                            go_Static.Caption = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHMnd, 0);

                        }
                    }
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_UPDATE:
                    if (po_BsnssObjInf.ActionSuccess && po_BsnssObjInf.BeforeAction != true)
                    {
                        sb_UpdateDataMatrix();
                        this.sb_InfoTotalesPorCarga();
                    }
                    break;
                case SAPbouiCOM.BoEventTypes.et_FORM_DATA_ADD:
                    if (po_BsnssObjInf.ActionSuccess && po_BsnssObjInf.BeforeAction != true)
                    {
                        this.sb_InfoTotalesPorCarga();
                    }
                    break;
            }
            return lb_Result;
        }

        private void sb_GetNextDocumentNumber()
        {
            string ls_Srie = string.Empty;
            ls_Srie = go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(0).TableName).GetValue("Series", 0);
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(0).TableName).SetValue("DocNum", 0, go_Form.BusinessObject.GetNextSerialNumber(ls_Srie, go_Form.BusinessObject.Type).ToString());
        }

        private void sb_AddStandarDataToNewRow(int pi_Row)
        {
            SAPbobsCOM.AdminInfo lo_AdmInf = null;
            SAPbobsCOM.CompanyService lo_CmpSrv = null;
            SAPbobsCOM.GeneralService lo_GnrSrv = null;
            SAPbobsCOM.GeneralData lo_GnrDta = null;
            SAPbobsCOM.GeneralDataParams lo_GnrDtaPrms = null;
            SAPbobsCOM.GeneralDataCollection lo_GnrDtaCll = null;
            SAPbobsCOM.Items lo_Item = null;
            string ls_DimNmb = string.Empty;

            lo_CmpSrv = go_SBOCompany.GetCompanyService();
            lo_AdmInf = lo_CmpSrv.GetAdminInfo();
            lo_Item = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetSelecc, pi_Row - 1, "Y");
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocFch, pi_Row - 1, DateTime.Now.ToString("yyyyMMdd"));
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetCntFch, pi_Row - 1, go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflFchCre, 0).Trim());
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocMnd, pi_Row - 1, lo_AdmInf.LocalCurrency);
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetCntArt, pi_Row - 1, "1");
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocTpo, pi_Row - 1, "01");
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocTpo, pi_Row - 1, "01");
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocCls, pi_Row - 1, "I");
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocImp, pi_Row - 1, "EXO");
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetAlmArt, pi_Row - 1, lo_AdmInf.DefaultWarehouse);
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocRtn, pi_Row - 1, "N");
            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(1).TableName).SetValue(gs_UflDetDocEst, pi_Row - 1, "CRE");
            if (go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(0).TableName).GetValue(gs_UflCCHNmb, 0).Trim() != string.Empty)
            {
                lo_GnrSrv = lo_CmpSrv.GetGeneralService("BPP_CAJASCHICAS");
                lo_GnrDtaPrms = lo_GnrSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
                lo_GnrDtaPrms.SetProperty("Code", go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim());
                lo_GnrDta = lo_GnrSrv.GetByParams(lo_GnrDtaPrms);
                if (lo_GnrDta.GetProperty("U_STR_DIM") == "Y")
                {
                    lo_GnrDtaCll = lo_GnrDta.Child("STR_CAJASCHICASDIM");
                    for (int i = 0; i < lo_GnrDtaCll.Count; i++)
                    {
                        if (lo_GnrDtaCll.Item(i).GetProperty("U_CC_DFLT") != string.Empty)
                        {
                            ls_DimNmb = lo_GnrDtaCll.Item(i).GetProperty("U_CC_NMBR");
                            ls_DimNmb = ls_DimNmb.Substring(ls_DimNmb.Length - 1, 1).Trim();
                            go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue("U_CC_DIM" + ls_DimNmb, pi_Row - 1, lo_GnrDtaCll.Item(i).GetProperty("U_CC_DFLT"));
                        }
                    }
                }
                if (lo_GnrDta.GetProperty("U_STR_PRYS") == "Y")
                {
                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue("U_CC_PRYC", pi_Row - 1, lo_GnrDta.GetProperty("U_STR_PRYD"));
                }
            }
        }

        private void sb_SetRowFontColor()
        {
            try
            {
                go_Form.Freeze(true);
                int li_Red = 255;
                go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
                for (int i = 0; i < go_Matrix.RowCount; i++)
                {
                    go_Edit = go_Matrix.Columns.Item(gs_ClmMtxEstCre).Cells.Item(i + 1).Specific;
                    if (go_Edit.Value.ToUpper() == "ERR")
                    {
                        go_Matrix.CommonSetting.SetRowFontColor(i + 1, li_Red);
                    }
                    else
                    {
                        go_Matrix.CommonSetting.SetRowFontColor(i + 1, 0);
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
                go_Form.Freeze(false);
            }
        }

        private bool fn_ValidacionesGenerales()
        {
            bool lb_Result = true;
            string ls_MsgErr = string.Empty;
            string ls_CdgPrv = string.Empty;
            string ls_CdgArt = string.Empty;
            string ls_FchDcm = string.Empty;
            string ls_TpoDcm = string.Empty;
            string ls_SerDcm = string.Empty;
            string ls_CorDcm = string.Empty;
            string ls_ClsDcm = string.Empty;
            string ls_AlmArt = string.Empty;
            string ls_CdgCta = string.Empty;
            double ld_CntArt = 0.0;
            double ld_PrcUni = 0.0;

            if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("Status", 0).Trim() == "C") return lb_Result;
            //Validaciones de Cabecera
            go_Edit = go_Form.Items.Item(gs_EdtNmbCCH).Specific;
            if (go_Edit.Value.Trim() == string.Empty)
            {
                ls_MsgErr = "Seleccione una caja chica...";
                lb_Result = false;
                go_Edit.Active = true;
                goto fin;
            }
            go_Combo = go_Form.Items.Item(gs_CmbCCHNmr).Specific;
            if (go_Combo.Value.Trim() == string.Empty)
            {
                ls_MsgErr = "Seleccione un número de caja chica...";
                lb_Result = false;
                go_Combo.Active = true;
                goto fin;
            }
            go_Edit = go_Form.Items.Item(gs_EdtFchCnt).Specific;
            if (go_Edit.Value.Trim() == string.Empty)
            {
                ls_MsgErr = "Seleccione la fecha de carga de documentos...";
                lb_Result = false;
                go_Edit.Active = true;
                goto fin;
            }
            go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
            //if (go_Matrix.RowCount == 0)
            //{
            //    ls_MsgErr = "No se ha agregado ningun documento...";
            //    lb_Result = false;
            //    goto fin;
            //}

            // validaciones de detalle
            go_Matrix.FlushToDataSource();
            for (int i = 0; i < go_Matrix.RowCount; i++)
            {
                if (go_Matrix.RowCount == i + 1 && i + 1 > 1) continue;
                ls_CdgPrv = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetCdgPrv, i).Trim();
                if (string.IsNullOrEmpty(ls_CdgPrv))
                {
                    go_Matrix.SelectRow(i + 1, true, false);
                    ls_MsgErr = "No se ha ingresado el proveedor en la fila marcada...";
                    lb_Result = false;
                    goto fin;
                }

                ls_SerDcm = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocSri, i).Trim();
                if (string.IsNullOrEmpty(ls_SerDcm))
                {
                    go_Matrix.SelectRow(i + 1, true, false);
                    ls_MsgErr = "No se ha ingresado la serie del documento en la fila marcada...";
                    lb_Result = false;
                    goto fin;
                }

                ls_CorDcm = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocCor, i).Trim();
                if (string.IsNullOrEmpty(ls_CorDcm))
                {
                    go_Matrix.SelectRow(i + 1, true, false);
                    ls_MsgErr = "No se ha ingresado el correlativo del documento en la fila marcada...";
                    lb_Result = false;
                    goto fin;
                }

                ls_ClsDcm = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocCls, i).Trim();
                if (string.IsNullOrEmpty(ls_ClsDcm))
                {
                    go_Matrix.SelectRow(i + 1, true, false);
                    ls_MsgErr = "No se ha seleccionado la clase del documento en la fila marcada...";
                    lb_Result = false;
                    goto fin;
                }

                if (ls_ClsDcm == "S")
                {
                    ls_CdgCta = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetCodCta, i).Trim();
                    if (string.IsNullOrEmpty(ls_CdgCta))
                    {
                        go_Matrix.SelectRow(i + 1, true, false);
                        ls_MsgErr = "No se ha ingresado la cuenta de servicios en la fila marcada...";
                        lb_Result = false;
                        goto fin;
                    }

                    ld_PrcUni = Convert.ToDouble(go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetPrcUni, i).Trim());
                    if (ld_PrcUni == 0.0)
                    {
                        go_Matrix.SelectRow(i + 1, true, false);
                        ls_MsgErr = "El precio por unidad para la fila marcada es 0...";
                        lb_Result = false;
                        goto fin;
                    }
                }

                if (ls_ClsDcm == "I")
                {
                    ls_CdgArt = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetCdgArt, i).Trim();
                    if (string.IsNullOrEmpty(ls_CdgArt))
                    {
                        go_Matrix.SelectRow(i + 1, true, false);
                        ls_MsgErr = "No se ha ingresado el articulo en la fila marcada...";
                        lb_Result = false;
                        goto fin;
                    }

                    ls_AlmArt = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetAlmArt, i).Trim();
                    if (string.IsNullOrEmpty(ls_AlmArt))
                    {
                        go_Matrix.SelectRow(i + 1, true, false);
                        ls_MsgErr = "No se ha ingresado el almacen en la fila marcada...";
                        lb_Result = false;
                        goto fin;
                    }

                    ld_CntArt = Convert.ToDouble(go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetCntArt, i).Trim());
                    if (ld_CntArt == 0.0)
                    {
                        go_Matrix.SelectRow(i + 1, true, false);
                        ls_MsgErr = "La cantidad en la fila marcada es 0...";
                        lb_Result = false;
                        goto fin;
                    }

                    //ld_PrcUni = Convert.ToDouble(go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetTotLin, i).Trim());
                    //if (ld_PrcUni == 0.0)
                    //{
                    //    go_Matrix.SelectRow(i + 1, true, false);
                    //    ls_MsgErr = "Total por linea para la fila marcada es 0...";
                    //    lb_Result = false;
                    //    goto fin;
                    //}
                }
            }

        fin:
            if (!lb_Result)
            {
                go_SBOApplication.SetStatusBarMessage(ls_MsgErr, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            return lb_Result;
        }

        private void sb_OcultarMostrarColumnasXClaseDoc(int pi_Row)
        {
            string ls_ClsDcm = string.Empty;

            try
            {
                go_Form.Freeze(true);
                go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
                go_Matrix.FlushToDataSource();
                ls_ClsDcm = go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).GetValue(gs_UflDetDocCls, pi_Row - 1).Trim();
                if (ls_ClsDcm.ToUpper() == "I")
                {
                    go_Matrix.Columns.Item(gs_ClmMtxCodArt).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxNomArt).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxAlmArt).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxUniMed).Visible = true;

                    //go_Matrix.Columns.Item(gs_ClmMtxDscSrv).Visible = false;
                    //go_Matrix.Columns.Item(gs_ClmMtxCodCta).Visible = false;
                    //go_Matrix.Columns.Item(gs_ClmMtxNroCta).Visible = false;
                    //go_Matrix.Columns.Item(gs_ClmMtxNmbCta).Visible = false;
                    sb_EnabledCeldas(true, pi_Row);
                }
                else
                {
                    //go_Matrix.Columns.Item(gs_ClmMtxCodArt).Visible = false;
                    //go_Matrix.Columns.Item(gs_ClmMtxNomArt).Visible = false;
                    //go_Matrix.Columns.Item(gs_ClmMtxAlmArt).Visible = false;
                    //go_Matrix.Columns.Item(gs_ClmMtxUniMed).Visible = false;

                    go_Matrix.Columns.Item(gs_ClmMtxDscSrv).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxCodCta).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxNroCta).Visible = true;
                    go_Matrix.Columns.Item(gs_ClmMtxNmbCta).Visible = true;

                    // Limitar cantidad a 1
                    sb_EnabledCeldas(false,pi_Row);
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
        private void sb_EnabledCeldas(bool pb_Articulo, int pi_row)
        {
            try
            {
                
                 go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(!pb_Articulo ? gs_UflDetCdgArt : gs_UflDetCdgServ, pi_row - 1, string.Empty);
                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(!pb_Articulo ?  gs_UflDetCdgDscArt : gs_UflCuntSegmen, pi_row - 1, string.Empty);
                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(!pb_Articulo ? gs_UflDetAlmArt : gs_UflNomCuenta, pi_row - 1, string.Empty);
                    go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(!pb_Articulo ? gs_UflDetCntArt : gs_UflCodCuenta, pi_row - 1, !pb_Articulo ? "1" : string.Empty); // Cantidad

                if (!pb_Articulo) go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).SetValue(gs_UflDetUndMed, pi_row - 1, string.Empty);

                go_Matrix.LoadFromDataSource();

                go_Matrix.CommonSetting.SetCellEditable(pi_row, 12, pb_Articulo);
                go_Matrix.CommonSetting.SetCellEditable(pi_row, 13, pb_Articulo);
                go_Matrix.CommonSetting.SetCellEditable(pi_row, 14, pb_Articulo);
                go_Matrix.CommonSetting.SetCellEditable(pi_row, 16, pb_Articulo);

               // go_Matrix.CommonSetting.SetCellEditable(pi_row, 20, !pb_Articulo);
                go_Matrix.CommonSetting.SetCellEditable(pi_row, 21, !pb_Articulo);
                go_Matrix.CommonSetting.SetCellEditable(pi_row, 22, false);
                go_Matrix.CommonSetting.SetCellEditable(pi_row, 23, !pb_Articulo);
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

        private void sb_UpdateDataMatrix()
        {
            SAPbouiCOM.Conditions lo_Cnds = null;
            SAPbouiCOM.Condition lo_Cnd = null;
            SAPbouiCOM.Matrix lo_Matrix = null;

            try
            {
                go_Form.Freeze(true);
                lo_Cnds = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_Conditions);
                lo_Matrix = go_Form.Items.Item("MtxDocs").Specific;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.Alias = "DocEntry";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("DocEntry", 0);
                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.BracketOpenNum = 2;
                lo_Cnd.Alias = "U_CC_ESTD";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "ERR";
                lo_Cnd.BracketCloseNum = 1;
                lo_Cnd.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR;
                lo_Cnd = lo_Cnds.Add();
                lo_Cnd.BracketOpenNum = 1;
                lo_Cnd.Alias = "U_CC_ESTD";
                lo_Cnd.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                lo_Cnd.CondVal = "CRE";
                lo_Cnd.BracketCloseNum = 2;
                go_Form.DataSources.DBDataSources.Item(gs_DtdCCHCRGDET).Query(lo_Cnds);
                lo_Matrix.LoadFromDataSource();
                lo_Matrix.Columns.Item(0).TitleObject.Sort(SAPbouiCOM.BoGridSortType.gst_Ascending);
                lo_Matrix.FlushToDataSource();
                lo_Matrix.LoadFromDataSource();
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

        private bool fn_ValidarNumeroUnicoDocumento(string ps_CardCode, string ps_NumUni)
        {
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            SAPbobsCOM.Recordset lo_RecSet = null;
            bool lb_Result = true;

            lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            lo_ArrCad = Cls_QueriesManager_CCH.VerificarDocumentoExistente.Split(new char[] { '?' });
            ls_Qry = lo_ArrCad[0].Trim() + ps_NumUni + lo_ArrCad[1].Trim() + ps_CardCode + lo_ArrCad[2].Trim();
            Cls_Global.WriteToFile(ls_Qry);
            lo_RecSet.DoQuery(ls_Qry);
            if (!lo_RecSet.EoF)
            {
                if (Convert.ToInt32(lo_RecSet.Fields.Item(0).Value) != 0)
                {
                    go_SBOApplication.SetStatusBarMessage("El numero de documento SUNAT ya existe...", SAPbouiCOM.BoMessageTime.bmt_Short);
                    lb_Result = false;
                }
            }
            return lb_Result;
        }

        private bool fn_ValidarPermisos(string ps_NroCCH, string ps_User)
        {
            string ls_Qry = string.Empty;
            string[] lo_ArrCad = null;
            bool lb_Result = true;
            SAPbobsCOM.Recordset lo_RecSet = null;

            go_Form.DataSources.DBDataSources.Item(go_Form.DataSources.DBDataSources.Item(0).TableName).SetValue(gs_UflCCHNmb, 0, string.Empty);
            lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            lo_ArrCad = Cls_QueriesManager_CCH.ValidarPermisosCargaCCH.Split(new char[] { '?' });
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

        private bool fn_ValidarCantidadNrosCCH(string ps_NroCCH)
        {
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbobsCOM.CompanyService lo_CmpSrv = null;
            SAPbobsCOM.GeneralService lo_GnrSrv = null;
            SAPbobsCOM.GeneralData lo_GnrDta = null;
            SAPbobsCOM.GeneralDataParams lo_GnrDtaPrms = null;
            bool lb_Result = true;
            int li_CntRndc = 0;

            lo_CmpSrv = go_SBOCompany.GetCompanyService();
            lo_GnrSrv = lo_CmpSrv.GetGeneralService("BPP_CAJASCHICAS");
            lo_GnrDtaPrms = lo_GnrSrv.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
            lo_GnrDtaPrms.SetProperty("Code", go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim());
            lo_GnrDta = lo_GnrSrv.GetByParams(lo_GnrDtaPrms);
            li_CntRndc = lo_GnrDta.GetProperty("U_STR_RNDC");

            lo_RecSet = Cls_QueriesManager_CCH.fn_VerificarCantidadNrosCCH(ps_NroCCH);
            if (!lo_RecSet.EoF)
            {
                if (Convert.ToInt32(lo_RecSet.Fields.Item(0).Value) == li_CntRndc)
                {
                    go_SBOApplication.StatusBar.SetText("Se ha alcanzado el limite maximo de rendiciones para el numero de caja chica seleccionado(" + li_CntRndc + ")...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    lb_Result = false;
                }
                else
                {
                    lb_Result = true;
                }
            }
            return lb_Result;
        }

        private void sb_SaldoCajaChica()
        {
            string[] lo_ArrCad = null;
            string ls_Qry = string.Empty;
            SAPbobsCOM.Recordset lo_RecSet = null;

            lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            lo_ArrCad = Cls_QueriesManager_CCH.SaldoCajaChica.Split(new char[] { '?' });
            ls_Qry = lo_ArrCad[0].Trim() + go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmr, 0).Trim() + lo_ArrCad[1].Trim();
            Cls_Global.WriteToFile(ls_Qry);
            lo_RecSet.DoQuery(ls_Qry);
            if (!lo_RecSet.EoF)
            {
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflSldIni, 0, lo_RecSet.Fields.Item(0).Value);
            }
            go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
            if (go_Matrix.RowCount == 0)
            {
                go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflCCHTtDc, 0, string.Empty);
            }
        }

        private void sb_AddUserColumnsToMatrix()
        {
            SAPbobsCOM.Recordset lo_RecSet = null;
            SAPbobsCOM.Recordset lo_RecSetAux = null;
            SAPbobsCOM.Recordset lo_RecSetAux2 = null;
            SAPbouiCOM.Column lo_ClmMtx = null;
            string ls_Qry = string.Empty;
            try
            {
                lo_RecSet = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                lo_RecSetAux = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                lo_RecSetAux2 = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
                if (Cls_Global.go_ServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
                {
                    ls_Qry = @"SELECT ""TableID"",'U_'||""AliasID"",""FieldID"",""RTable"" FROM CUFD WHERE ""TableID"" = '@STR_CCHCRGDET' AND LEFT(""AliasID"",2) = 'CU'";
                }
                else
                {
                    ls_Qry = @"SELECT TableID,'U_'+AliasID,FieldID,RTable FROM CUFD WHERE TableID = '@STR_CCHCRGDET' AND LEFT(AliasID,2) = 'CU'";
                }
                Cls_Global.WriteToFile(ls_Qry);
                lo_RecSet.DoQuery(ls_Qry);
                while (!lo_RecSet.EoF)
                {
                    //Verifico si el campo de usuario tiene valores validos
                    ls_Qry = @"SELECT COUNT('A') FROM UFD1 WHERE ""TableID"" = '" + lo_RecSet.Fields.Item(0).Value + @"' AND ""FieldID"" = " + lo_RecSet.Fields.Item(2).Value;
                    Cls_Global.WriteToFile(ls_Qry);
                    lo_RecSetAux.DoQuery(ls_Qry);
                    if (Convert.ToInt32(lo_RecSetAux.Fields.Item(0).Value) != 0)
                    {
                        lo_ClmMtx = go_Matrix.Columns.Add(lo_RecSet.Fields.Item(1).Value, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                        lo_ClmMtx.DataBind.SetBound(true, lo_RecSet.Fields.Item(0).Value, lo_RecSet.Fields.Item(1).Value);
                    }
                    else if (lo_RecSet.Fields.Item(3).Value != string.Empty)
                    {
                        lo_ClmMtx = go_Matrix.Columns.Add(lo_RecSet.Fields.Item(1).Value, SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                        lo_ClmMtx.DataBind.SetBound(true, lo_RecSet.Fields.Item(0).Value, lo_RecSet.Fields.Item(1).Value);
                        ls_Qry = @"SELECT ""Code"",""Name"" FROM ""@" + lo_RecSet.Fields.Item(3).Value + @"""";
                        Cls_Global.WriteToFile(ls_Qry);
                        lo_RecSetAux2.DoQuery(ls_Qry);
                        while (!lo_RecSetAux2.EoF)
                        {
                            lo_ClmMtx.ValidValues.Add(lo_RecSetAux2.Fields.Item(0).Value, lo_RecSetAux2.Fields.Item(1).Value);
                            lo_RecSetAux2.MoveNext();
                        }
                    }
                    else
                    {
                        lo_ClmMtx = go_Matrix.Columns.Add(lo_RecSet.Fields.Item(1).Value, SAPbouiCOM.BoFormItemTypes.it_EDIT);
                        lo_ClmMtx.DataBind.SetBound(true, lo_RecSet.Fields.Item(0).Value, lo_RecSet.Fields.Item(1).Value);
                    }
                    lo_ClmMtx.TitleObject.Caption = lo_RecSet.Fields.Item(1).Value;
                    lo_ClmMtx.Editable = true;
                    lo_ClmMtx.DisplayDesc = true;
                    lo_ClmMtx.Width = 100;
                    lo_RecSet.MoveNext();
                }
            }
            catch (Exception ex)
            {
                Cls_Global.WriteToFile(ex.Message);
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                lo_RecSet = null;
                lo_RecSetAux = null;
                lo_RecSetAux2 = null;
            }
        }

        public void sb_CerrarCarga()
        {
            go_Matrix = go_Form.Items.Item(gs_MtxDocs).Specific;
            if (Cls_QueriesManager_CCH.fn_ValidarPermisosCerrarCargaCCH(go_SBOCompany.UserName, go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflCCHNmb, 0).Trim()) != "Y")
            {
                go_SBOApplication.StatusBar.SetText("No tiene permiso para cerrar esta carga de documentos...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            else if (go_Matrix.VisualRowCount > 0)
            {
                go_SBOApplication.StatusBar.SetText("Existen documentos pendientes de contabilización, no se puede proceder con esta acción...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            else
            {
                System.Windows.Forms.DialogResult lo_Resultado;
                lo_Resultado = (System.Windows.Forms.DialogResult)go_SBOApplication.MessageBox("¿Desea continuar con esta acción?", 1, "Si", "No");
                if (lo_Resultado == System.Windows.Forms.DialogResult.OK)
                {
                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue("Status", 0, "C");
                    if (go_Form.Mode != SAPbouiCOM.BoFormMode.fm_UPDATE_MODE)
                    {
                        go_Form.Mode = SAPbouiCOM.BoFormMode.fm_UPDATE_MODE;
                        go_Form.Items.Item(gs_BtnAñadir).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                    }
                    go_Form.Items.Item(gs_MtxDocs).Enabled = false;
                }
            }
        }

        private void sb_AddMenuCerrarCarga(SAPbouiCOM.ContextMenuInfo po_RghClkEvent)
        {

            if (po_RghClkEvent.BeforeAction)
            {
                if (go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("Status", 0).Trim() != "C")
                {
                    SAPbouiCOM.Menus lo_Menus = null;
                    SAPbouiCOM.IMenuItem lo_MnuItm = null;
                    SAPbouiCOM.MenuCreationParams lo_MnuCrtPrms = null;
                    lo_MnuItm = go_SBOApplication.Menus.Item("1280");
                    lo_MnuCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);
                    lo_Menus = lo_MnuItm.SubMenus;
                    lo_MnuCrtPrms.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    lo_MnuCrtPrms.UniqueID = gs_MnuCerrarCarga;
                    lo_MnuCrtPrms.String = "Cerrar Carga";
                    lo_MnuCrtPrms.Enabled = true;
                    lo_Menus.AddEx(lo_MnuCrtPrms);
                }
            }
            else
            {
                if (go_SBOApplication.Menus.Exists(gs_MnuCerrarCarga))
                {
                    go_SBOApplication.Menus.RemoveEx(gs_MnuCerrarCarga);
                }
            }
        }
        private (double ld_ttlsm, double ld_ttlimp, double ld_ttl) Fn_InfoTotalPorActualizar(string ps_impuesto, double pd_ttlinea, string ps_monedaDet, string fechaDoc, string ps_monedaCab)
        {
            SAPbobsCOM.Recordset lo_RecSet = null;
            double ld_ttlsm = 0;
            double ld_ttlimp = 0;
            double ld_ttl = 0;

            try
            {
                go_Form.Freeze(true);
                (go_Form.Items.Item(gs_MtxDocs).Specific as SAPbouiCOM.Matrix).FlushToDataSource();
                lo_RecSet = Cls_QueriesManager_EAR.fn_InfoTotalesPorActualizacion(ps_impuesto, pd_ttlinea, ps_monedaDet, fechaDoc, ps_monedaCab);

                if (lo_RecSet != null)
                {
                    ld_ttlsm = Convert.ToDouble(lo_RecSet.Fields.Item(0).Value);
                    ld_ttlimp = Convert.ToDouble(lo_RecSet.Fields.Item(1).Value);
                    ld_ttl = Convert.ToDouble(lo_RecSet.Fields.Item(2).Value);
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

            return (ld_ttlsm, ld_ttlimp, ld_ttl);
        }
        private void sb_InfoTotalesPorCarga()
        {
            SAPbobsCOM.Recordset lo_RecSet = null;

            try
            {
                go_Form.Freeze(true);
                (go_Form.Items.Item(gs_MtxDocs).Specific as SAPbouiCOM.Matrix).FlushToDataSource();
                lo_RecSet = Cls_QueriesManager_CCH.fn_InfoTotalesPorCarga(go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue("DocEntry", 0).Trim());
                if (lo_RecSet != null)
                {
                    go_Form.DataSources.UserDataSources.Item(gs_UDSTotSinImp).Value = Convert.ToString(lo_RecSet.Fields.Item(0).Value);
                    go_Form.DataSources.UserDataSources.Item(gs_UDSTotImpsts).Value = Convert.ToString(lo_RecSet.Fields.Item(1).Value);
                    go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).SetValue(gs_UflCCHTtDc, 0, Convert.ToString(lo_RecSet.Fields.Item(2).Value));
                    go_Form.DataSources.UserDataSources.Item(gs_UDSSaldoCaja).Value = Convert.ToString(Convert.ToDouble(go_Form.DataSources.DBDataSources.Item(gs_DtcCCHCRG).GetValue(gs_UflSldIni, 0)) - Convert.ToDouble(lo_RecSet.Fields.Item(2).Value));
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
}
