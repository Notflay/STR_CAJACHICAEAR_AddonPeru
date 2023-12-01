using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using STR_CajaChica_Entregas.Metadata;
using STR_CajaChica_Entregas.UTIL;
using STR_CajaChica_Entregas.DL;


namespace STR_CajaChica_Entregas.UL
{
    class Cls_Main
    {

        //Variables sobre objetos
        private SAPbobsCOM.Company go_SBOCompany = null;
        private SAPbouiCOM.Application go_SBOApplication = null;
        private Cls_CCH_Crear_Accesos go_CCH_Crear_Accesos = null;
        private Cls_FormsRelacionados go_Forms_Relacionados = null;
        private Cls_CCH_Apertura go_CCH_Aperturar = null;
        private Cls_CCH_Carga go_CCH_Cargar = null;
        private Cls_CCH_EAR_Init go_CCH_EAR_Init = null;
        private Cls_CCH_Dimensiones go_CCH_Dimensiones = null;
        private Cls_EAR_Crear_Accesos go_EAR_Crear_Accesos = null;
        private Cls_EAR_Apertura go_EAR_Apertura = null;
        private Cls_EAR_Carga go_EAR_Carga = null;
        private Cls_EAR_Regularizacion go_EAR_Regularizar = null;
        private Cls_EAR_Dimensiones go_EAR_Dimensiones = null;
        private const string gs_Mnu_Add = "1282";
        private const string gs_Mnu_Find = "1281";
        private const string gs_Mnu_AddRow = "1292";
        private const string gs_Mnu_DltRow = "1293";

        public Cls_Main()
        {
            //Inicio el objeto Application y luego el objeto Company de la clase globales
            sb_SetApplication();
            if (go_SBOApplication != null && go_SBOCompany != null)
            {
                go_SBOApplication.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(lo_SBOApplication_MenuEvent);
                go_SBOApplication.ItemEvent += new SAPbouiCOM._IApplicationEvents_ItemEventEventHandler(lo_SBOApplication_ItemEvent);
                go_SBOApplication.RightClickEvent += new SAPbouiCOM._IApplicationEvents_RightClickEventEventHandler(lo_SBOApplication_RightClickEvent);
                go_SBOApplication.FormDataEvent += new SAPbouiCOM._IApplicationEvents_FormDataEventEventHandler(lo_SBOApplication_FormDataEvent);
                go_SBOApplication.AppEvent += new SAPbouiCOM._IApplicationEvents_AppEventEventHandler(go_SBOApplication_AppEvent);
                sb_InitObjects();
                sb_SetFilters();
                go_CCH_EAR_Init.sb_VerificarInstalacion();
                sb_AddMenu();
            }
        }

        public static void sb_CargarHardwareKey(string hardware)
        {
            Cls_Global.gs_hardwarek = hardware;
        }

        private void sb_InitObjects()
        {
            go_CCH_Crear_Accesos = new Cls_CCH_Crear_Accesos();
            go_Forms_Relacionados = new Cls_FormsRelacionados();
            go_CCH_Aperturar = new Cls_CCH_Apertura();
            go_CCH_Cargar = new Cls_CCH_Carga();
            go_CCH_Dimensiones = new Cls_CCH_Dimensiones();
            go_EAR_Crear_Accesos = new Cls_EAR_Crear_Accesos();
            go_EAR_Apertura = new Cls_EAR_Apertura();
            go_EAR_Carga = new Cls_EAR_Carga();
            go_EAR_Regularizar = new Cls_EAR_Regularizacion();
            go_EAR_Dimensiones = new Cls_EAR_Dimensiones();
            go_CCH_EAR_Init = new Cls_CCH_EAR_Init();
        }

        private void sb_SetApplication()
        {
            string ls_ConectionString = string.Empty;
            SAPbouiCOM.SboGuiApi lo_SBOGUIAPI = null;
            try
            {
                lo_SBOGUIAPI = new SAPbouiCOM.SboGuiApi();
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    ls_ConectionString = Convert.ToString(Environment.GetCommandLineArgs().GetValue(1));
                }
                else
                {
                    ls_ConectionString = Convert.ToString(Environment.GetCommandLineArgs().GetValue(0));
                }
                lo_SBOGUIAPI.Connect(ls_ConectionString);
                if (Cls_Global.go_SBOApplication == null)
                {
                    Cls_Global.go_SBOApplication = lo_SBOGUIAPI.GetApplication(-1);
                    go_SBOApplication = Cls_Global.go_SBOApplication;
                }
                if (Cls_Global.go_SBOApplication != null) sb_SetCompany();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sb_SetCompany()
        {
            try
            {
                Cls_Global.go_SBOCompany = Cls_Global.go_SBOApplication.Company.GetDICompany();
                go_SBOCompany = Cls_Global.go_SBOCompany;
                go_SBOApplication.StatusBar.SetText("Iniciando el Addon de Caja Chica - Entregas a Rendir", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                if (Cls_Global.go_SBOCompany != null)
                {   //Se guarda el tipo de BD
                    //Clase que maneja los queries x BD
                    Cls_QueriesManager_CCH.go_ServerType = go_SBOCompany.DbServerType;
                    Cls_QueriesManager_EAR.go_ServerType = go_SBOCompany.DbServerType;
                    //Clase Global
                    Cls_Global.go_ServerType = go_SBOCompany.DbServerType;
                    Cls_Global.go_SBObob = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);

                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        private void sb_SetFilters()
        {
            SAPbouiCOM.EventFilters lo_EventFilters = new SAPbouiCOM.EventFilters();
            SAPbouiCOM.EventFilter lo_EventFilter = null;
            lo_EventFilter = lo_EventFilters.Add(SAPbouiCOM.BoEventTypes.et_ALL_EVENTS);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormPlanCuentas);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormSolicCompra);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormSocioNegocio);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormPagoEfectuado);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormPagoRecibido);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormMediosdePago_PE);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormMediosdePago_PR);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormMaestroEmpleados);
            lo_EventFilter.AddEx(Cls_CCH_Crear_Accesos.gs_NomForm);
            lo_EventFilter.AddEx(Cls_CCH_Apertura.gs_NomForm);
            lo_EventFilter.AddEx(Cls_CCH_Carga.gs_NomForm);
            lo_EventFilter.AddEx(Cls_CCH_Dimensiones.gs_NomForm);
            lo_EventFilter.AddEx(Cls_EAR_Crear_Accesos.gs_NomForm);
            lo_EventFilter.AddEx(Cls_EAR_Apertura.gs_NomForm);
            lo_EventFilter.AddEx(Cls_EAR_Carga.gs_NomForm);
            lo_EventFilter.AddEx(Cls_EAR_Regularizacion.gs_NomForm);
            lo_EventFilter.AddEx(Cls_EAR_Dimensiones.gs_NomForm);
            go_SBOApplication.SetFilter(lo_EventFilters);
            lo_EventFilter = lo_EventFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
            lo_EventFilter.AddEx(Cls_FormsRelacionados.gs_NomFormMsgBxReconciliacion);
            go_SBOApplication.SetFilter(lo_EventFilters);
        }

        private void sb_AddMenu()
        {
            XmlDocument oMnuXML = new XmlDocument();
            go_SBOApplication.StatusBar.SetText("CCH-EAR: Cargando opciones de menu...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            go_SBOApplication.Forms.GetFormByTypeAndCount(169, 1).Freeze(true);
            try
            {
                oMnuXML.LoadXml(Properties.Resources.Menu);
                go_SBOApplication.LoadBatchActions(oMnuXML.InnerXml);
                go_SBOApplication.StatusBar.SetText("El menu del Addon Caja Chica - Entregas a Rendir fue cargado correctamente... ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            }
            catch (System.IO.FileNotFoundException fnfex)
            {
                go_SBOApplication.StatusBar.SetText("El recurso: Menu.xml, no fue encontrado...", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
            finally
            {
                go_SBOApplication.Forms.GetFormByTypeAndCount(169, 1).Freeze(false);
                go_SBOApplication.Forms.GetFormByTypeAndCount(169, 1).Update();
                oMnuXML = null;
            }
        }

        void go_SBOApplication_AppEvent(SAPbouiCOM.BoAppEventTypes AppEvent)
        {
            switch (AppEvent)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    go_SBOCompany.Disconnect();
                    Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    go_SBOCompany.Disconnect();
                    Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    go_SBOCompany.Disconnect();
                    Application.Exit();
                    break;
            }
        }

        void lo_SBOApplication_MenuEvent(ref SAPbouiCOM.MenuEvent po_MnuEvent, out bool pb_BubbleEvent)
        {
            pb_BubbleEvent = true;
            SAPbouiCOM.Form lo_FrmAux = null;
            string ls_NmbFrm = string.Empty;
            try
            {
                switch (po_MnuEvent.MenuUID)
                {
                    case gs_Mnu_Add:
                        if (!po_MnuEvent.BeforeAction)
                        {
                            lo_FrmAux = go_SBOApplication.Forms.ActiveForm;
                            ls_NmbFrm = lo_FrmAux.TypeEx;
                            if (ls_NmbFrm.StartsWith("-")) ls_NmbFrm = ls_NmbFrm.Remove(0, 1);
                            switch (ls_NmbFrm)
                            {
                                case Cls_CCH_Crear_Accesos.gs_NomForm:
                                    go_CCH_Crear_Accesos.sb_DataFormLoad();
                                    break;
                                case Cls_CCH_Apertura.gs_NomForm:
                                    go_CCH_Aperturar.sb_DataFormLoadAdd();
                                    break;
                                case Cls_CCH_Carga.gs_NomForm:
                                    go_CCH_Cargar.sb_DataFormLoadAdd();
                                    break;
                                case Cls_EAR_Crear_Accesos.gs_NomForm:
                                    go_EAR_Crear_Accesos.sb_DataFormLoad();
                                    break;
                                case Cls_EAR_Apertura.gs_NomForm:
                                    go_EAR_Apertura.sb_DataFormLoadAdd();
                                    break;
                                case Cls_EAR_Carga.gs_NomForm:
                                    go_EAR_Carga.sb_DataFormLoadAdd();
                                    break;
                                case Cls_FormsRelacionados.gs_NomFormMaestroEmpleados:
                                    go_Forms_Relacionados.sb_DataFormLoadAdd();
                                    break;
                            }
                        }
                        break;
                    case gs_Mnu_Find:
                        if (!po_MnuEvent.BeforeAction)
                        {
                            lo_FrmAux = go_SBOApplication.Forms.ActiveForm;
                            ls_NmbFrm = lo_FrmAux.TypeEx;
                            if (ls_NmbFrm.StartsWith("-")) ls_NmbFrm = ls_NmbFrm.Remove(0, 1);
                            switch (ls_NmbFrm)
                            {
                                case Cls_CCH_Crear_Accesos.gs_NomForm:
                                    go_CCH_Crear_Accesos.sb_EnableItemsByFindMode();
                                    break;
                                case Cls_EAR_Crear_Accesos.gs_NomForm:
                                    go_EAR_Crear_Accesos.sb_EnableItemsByFindMode();
                                    break;
                            }

                        }
                        break;
                    case gs_Mnu_AddRow:
                        if (po_MnuEvent.BeforeAction)
                        {
                            lo_FrmAux = go_SBOApplication.Forms.ActiveForm;
                            ls_NmbFrm = lo_FrmAux.TypeEx;
                            if (ls_NmbFrm.StartsWith("-")) ls_NmbFrm = ls_NmbFrm.Remove(0, 1);
                            switch (ls_NmbFrm)
                            {
                                case Cls_CCH_Carga.gs_NomForm:
                                    go_CCH_Cargar.sb_AddNewRowMatrix();
                                    break;
                                case Cls_EAR_Carga.gs_NomForm:
                                    go_EAR_Carga.sb_AddNewRowMatrix();
                                    break;
                            }
                        }
                        break;
                    case gs_Mnu_DltRow:
                        if (po_MnuEvent.BeforeAction)
                        {
                            lo_FrmAux = go_SBOApplication.Forms.ActiveForm;
                            ls_NmbFrm = lo_FrmAux.TypeEx;
                            if (ls_NmbFrm.StartsWith("-")) ls_NmbFrm = ls_NmbFrm.Remove(0, 1);
                            switch (ls_NmbFrm)
                            {
                                case Cls_CCH_Carga.gs_NomForm:
                                    pb_BubbleEvent = go_CCH_Cargar.fn_DeleteRowMatrix();
                                    break;
                                case Cls_EAR_Carga.gs_NomForm:
                                    pb_BubbleEvent = go_EAR_Carga.fn_DeleteRowMatrix();
                                    break;
                            }
                        }
                        break;
                    case Cls_CCH_Crear_Accesos.gs_MenuCCH:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_CCH_Crear_Accesos.sb_FormLoad("CCH");
                        }
                        break;
                    case Cls_CCH_Crear_Accesos.gs_MnuAñadirFila:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_CCH_Crear_Accesos.sb_AddNewRowMatrix();
                        }
                        break;
                    case Cls_CCH_Crear_Accesos.gs_MnuBorrarFila:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_CCH_Crear_Accesos.sb_DeleteRowMatrix();
                        }
                        break;
                    case Cls_CCH_Apertura.gs_MnuAprCCH:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_CCH_Aperturar.sb_FormLoad();
                        }
                        break;
                    case Cls_CCH_Carga.gs_MnuCrgCCH:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_CCH_Cargar.sb_FormLoad();
                        }
                        break;
                    case Cls_CCH_Carga.gs_MnuCerrarCarga:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_CCH_Cargar.sb_CerrarCarga();
                        }
                        break;
                    case Cls_EAR_Crear_Accesos.gs_MenuEAR:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_EAR_Crear_Accesos.sb_FormLoad();
                        }
                        break;
                    case Cls_EAR_Apertura.gs_MnuAprEAR:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_EAR_Apertura.sb_FormLoad();
                        }
                        break;
                    case Cls_EAR_Carga.gs_MnuCrgEAR:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_EAR_Carga.sb_FormLoad();
                        }
                        break;
                    case Cls_EAR_Carga.gs_MnuCerrarCarga:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_EAR_Carga.sb_CerrarCarga();
                        }
                        break;
                    case Cls_FormsRelacionados.gs_MnuAñadirFila:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_Forms_Relacionados.sb_AddNewRowMatrix();
                        }
                        break;
                    case Cls_FormsRelacionados.gs_MnuBorrarFila:
                        if (po_MnuEvent.BeforeAction)
                        {
                            go_Forms_Relacionados.sb_DeleteRowMatrix();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText("MnuEvnt: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        void lo_SBOApplication_ItemEvent(string ps_FormUID, ref SAPbouiCOM.ItemEvent po_ItemEvent, out bool pb_BubbleEvent)
        {
            pb_BubbleEvent = true;
            try
            {
                switch (po_ItemEvent.FormTypeEx)
                {
                    case Cls_CCH_Crear_Accesos.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Crear_Accesos.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_CCH_Apertura.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Aperturar.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_CCH_Carga.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Cargar.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_EAR_Crear_Accesos.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Crear_Accesos.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_CCH_Dimensiones.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Dimensiones.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormPlanCuentas:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormSolicCompra:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormSocioNegocio:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormMediosdePago_PE:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormMediosdePago_PR:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormMaestroEmpleados:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormMsgBxReconciliacion:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_EAR_Apertura.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Apertura.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_EAR_Carga.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Carga.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_EAR_Regularizacion.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Regularizar.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                    case Cls_EAR_Dimensiones.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Dimensiones.fn_HandleItemEvent(ps_FormUID, ref po_ItemEvent);
                        break;
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText("ItmEvnt: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                pb_BubbleEvent = false;
            }
        }

        void lo_SBOApplication_RightClickEvent(ref SAPbouiCOM.ContextMenuInfo po_RghClkEvent, out bool pb_BubbleEvent)
        {
            pb_BubbleEvent = true;
            SAPbouiCOM.Form lo_FrmAux = null;
            string ls_NmbFrm = string.Empty;

            try
            {
                lo_FrmAux = go_SBOApplication.Forms.Item(po_RghClkEvent.FormUID);
                lo_FrmAux = go_SBOApplication.Forms.ActiveForm;
                ls_NmbFrm = lo_FrmAux.TypeEx;
                if (ls_NmbFrm.StartsWith("-")) ls_NmbFrm = ls_NmbFrm.Remove(0, 1);
                switch (ls_NmbFrm)
                {
                    case Cls_CCH_Crear_Accesos.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Crear_Accesos.fn_HandleRightClickEvent(po_RghClkEvent);
                        break;
                    case Cls_CCH_Carga.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Cargar.fn_HandleRightClickEvent(po_RghClkEvent);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormMaestroEmpleados:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleRightClickEvent(po_RghClkEvent);
                        break;
                    case Cls_EAR_Carga.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Carga.fn_HandleRightClickEvent(po_RghClkEvent);
                        break;
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText("RghClcEvnt: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

        void lo_SBOApplication_FormDataEvent(ref SAPbouiCOM.BusinessObjectInfo po_BsnssObjInf, out bool pb_BubbleEvent)
        {
            pb_BubbleEvent = true;
            try
            {
                switch (po_BsnssObjInf.FormTypeEx)
                {
                    case Cls_CCH_Crear_Accesos.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Crear_Accesos.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                    case Cls_CCH_Apertura.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Aperturar.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                    case Cls_CCH_Carga.gs_NomForm:
                        pb_BubbleEvent = go_CCH_Cargar.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                    case Cls_EAR_Crear_Accesos.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Crear_Accesos.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                    case Cls_EAR_Carga.gs_NomForm:
                        pb_BubbleEvent = go_EAR_Carga.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormSolicCompra:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormPagoEfectuado:
                    case Cls_FormsRelacionados.gs_NomFormPagoRecibido:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                    case Cls_FormsRelacionados.gs_NomFormMaestroEmpleados:
                        pb_BubbleEvent = go_Forms_Relacionados.fn_HandleFormDataEvent(po_BsnssObjInf);
                        break;
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText("FrmDtaEvnt: " + ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }
        }

    }
}
