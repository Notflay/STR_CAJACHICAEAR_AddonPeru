using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace STR_CajaChica_Entregas.UTIL
{
    public static class Cls_Global
    {
        //Declaracion de variables que seran utilizadas por todo el proyecto
        public static SAPbobsCOM.Company go_SBOCompany = null;
        public static SAPbouiCOM.Application go_SBOApplication = null;
        public static bool segmentado = false;

        public static SAPbobsCOM.BoDataServerTypes go_ServerType;

        public static SAPbobsCOM.SBObob go_SBObob = null;
        public static void sb_CargarCombo(SAPbouiCOM.ComboBox po_ComboBox, SAPbobsCOM.Recordset po_RecordSet, bool pb_AddInitValue = false)
        {
            try
            {
                while (po_ComboBox.ValidValues.Count > 0)
                {
                    po_ComboBox.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
                }
                if (pb_AddInitValue)
                {
                    po_ComboBox.ValidValues.Add("---", "---");
                }
                while (!po_RecordSet.EoF)
                {
                    po_ComboBox.ValidValues.Add(po_RecordSet.Fields.Item(0).Value, po_RecordSet.Fields.Item(1).Value);
                    po_RecordSet.MoveNext();
                }
            }
            catch (Exception ex)
            {
                go_SBOApplication.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short);
            }
            finally
            {
                po_RecordSet = null;
                po_ComboBox = null;
            }
        }

        public static string sb_ObtenerMonedaLocal()
        {
            SAPbobsCOM.SBObob lo_SBObob = null;
            lo_SBObob = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);
            string moneda = (string)lo_SBObob.GetLocalCurrency().Fields.Item(0).Value;
            return (string)lo_SBObob.GetLocalCurrency().Fields.Item(0).Value;
        }
        public static double sb_ObtenerTipodeCambioXDia(string ps_CodMnd, DateTime po_Fch, ref int pi_CodErr, ref string ps_DscErr)
        {
            double ld_TpoCmb = 0.0;
            try
            {
                SAPbobsCOM.SBObob lo_SBObob = null;
                lo_SBObob = go_SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);
                ld_TpoCmb = (double)lo_SBObob.GetCurrencyRate(ps_CodMnd, po_Fch).Fields.Item(0).Value;
            }
            catch
            {
                go_SBOCompany.GetLastError(out pi_CodErr, out ps_DscErr);
            }
            return ld_TpoCmb;
        }

        public static void WriteToFile(string Message)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Logs\\Service_Creation_Log_{DateTime.Now.Date.ToShortDateString().Replace('/', '_')}.txt";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString() + " - " + Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString() + " - " + Message);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static SAPbouiCOM.Form fn_CreateForm(string ps_NomForm, string ps_RutaForm)
        {
            System.Xml.XmlDocument lo_XMLForm = null;
            SAPbouiCOM.FormCreationParams lo_FrmCrtPrms = null;
            SAPbouiCOM.Form lo_Form = null;

            try
            {
                lo_XMLForm = new System.Xml.XmlDocument();
                lo_FrmCrtPrms = go_SBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams);
                lo_XMLForm.Load(ps_RutaForm);
                lo_FrmCrtPrms.XmlData = lo_XMLForm.InnerXml;
                lo_FrmCrtPrms.FormType = ps_NomForm;
                lo_FrmCrtPrms.UniqueID = ps_NomForm;
                return lo_Form = go_SBOApplication.Forms.AddEx(lo_FrmCrtPrms);
            }
            catch (Exception ex)
            {
                go_SBOApplication.StatusBar.SetText(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                return null;
            }
        }

        public static void sb_AlinearItem(ref SAPbouiCOM.Item po_Itm, int pi_Top, int pi_Wdt, int pi_Hgh, int pi_Lft, int pi_Fpn, int pi_Tpn)
        {
            po_Itm.Top = pi_Top;
            po_Itm.Width = pi_Wdt;
            po_Itm.Height = pi_Hgh;
            po_Itm.Left = pi_Lft;
            po_Itm.FromPane = pi_Fpn;
            po_Itm.ToPane = pi_Tpn;
        }

    }

    public enum PeruAddon
    {
        Localizacion = 1,
        Sire = 2,
        CCEAR = 3,
        Letras = 4,
        TipoCambio = 5
    }

    public enum CodigoAddon
    {
        RAMOLOCALI = 1,
        RAMOSIRE = 2,
        RAMOEAR = 3,
        RAMOLETRAS = 4,
        RAMOCAMBIO = 5
    }
}
