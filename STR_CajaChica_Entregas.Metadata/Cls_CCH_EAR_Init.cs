using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using STR_CajaChica_Entregas.UTIL;


namespace STR_CajaChica_Entregas.Metadata
{
    public class Cls_CCH_EAR_Init
    {
        private SAPbouiCOM.Application go_SBOApplication = null;
        private SAPbobsCOM.Company go_SBOCompany = null;
        private string ls_Path = string.Empty;
        private int li_IndInstal = 18; //Version 2.8
        public Cls_CCH_EAR_Init()
        {
            go_SBOApplication = Cls_Global.go_SBOApplication;
            go_SBOCompany = Cls_Global.go_SBOCompany;
        }

    }
}
