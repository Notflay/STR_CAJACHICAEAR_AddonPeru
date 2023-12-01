using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STR_CajaChica_Entregas.UTIL
{
    public class Cls_Global_Controles: IDisposable
    {
        //Declaracion de Variables para los controles de los formularios
        //public static SAPbouiCOM.Form go_Form = null;
        public static SAPbouiCOM.Item go_Item = null;
        public static SAPbouiCOM.StaticText go_Static = null;
        public static SAPbouiCOM.EditText go_Edit = null;
        public static SAPbouiCOM.ComboBox go_Combo = null;
        public static SAPbouiCOM.Button go_Button = null;
        public static SAPbouiCOM.Grid go_Grid = null;
        public static SAPbouiCOM.Matrix go_Matrix = null;
        public static SAPbouiCOM.OptionBtn go_OptionButton = null;
        public static SAPbouiCOM.CheckBox go_CheckBox = null;
        public static SAPbobsCOM.Recordset go_RecordSet = null;
        public static SAPbouiCOM.Folder go_Folder = null;
        public static SAPbouiCOM.LinkedButton go_LinkButton = null;

        public void Dispose()
        {
         //go_Form = null;
         go_Item = null;
         go_Static = null;
         go_Edit = null;
         go_Combo = null;
         go_Button = null;
         go_Grid = null;
         go_Matrix = null;
         go_OptionButton = null;
         go_CheckBox = null;
         go_RecordSet = null;
         go_LinkButton = null;
        }
    }
}
