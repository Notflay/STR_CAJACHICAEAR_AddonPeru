﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STR_CajaChica_Entregas.DL.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Queries_SQL_HANA_EAR {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Queries_SQL_HANA_EAR() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("STR_CajaChica_Entregas.DL.Resources.Queries_SQL_HANA_EAR", typeof(Queries_SQL_HANA_EAR).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE [@STR_EARAPRDET] SET U_ER_STDO = &apos;?&apos;,U_ER_SLDO = &apos;?&apos; WHERE U_ER_EARN = &apos;?&apos; AND U_ER_NMER = &apos;?&apos;|UPDATE &quot;@STR_EARAPRDET&quot; SET &quot;U_ER_STDO&quot; = &apos;?&apos;,&quot;U_ER_SLDO&quot; = &apos;?&apos; WHERE &quot;U_ER_EARN&quot; = &apos;?&apos; AND &quot;U_ER_NMER&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ActEstadoNumerosEAR {
            get {
                return ResourceManager.GetString("ActEstadoNumerosEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE [@STR_EARCRGDET] SET U_ER_DEDC = &apos;?&apos;,U_ER_ESTD = &apos;?&apos; WHERE DocEntry = &apos;?&apos; AND LineId IN (?)|UPDATE &quot;@STR_EARCRGDET&quot; SET &quot;U_ER_DEDC&quot; = &apos;?&apos;,&quot;U_ER_ESTD&quot; = &apos;?&apos; WHERE &quot;DocEntry&quot; = &apos;?&apos; AND &quot;LineId&quot; IN (?).
        /// </summary>
        internal static string ActualizarEstadoCreacion {
            get {
                return ResourceManager.GetString("ActualizarEstadoCreacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_AgruparLineasEAR &apos;?&apos;,&apos;?&apos;,&apos;?&apos;,&apos;?&apos;,&apos;?&apos;|CALL STR_SP_LOC_AgruparLineasEAR( &apos;?&apos;,&apos;?&apos;,&apos;?&apos;,&apos;?&apos;,&apos;?&apos;).
        /// </summary>
        internal static string AgruparLineasEAR {
            get {
                return ResourceManager.GetString("AgruparLineasEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM [?] WHERE  ?  = &apos;?&apos;|DELETE FROM &quot;?&quot; WHERE &quot;?&quot; = &apos;?&apos;.
        /// </summary>
        internal static string EliminarRegistrosTU {
            get {
                return ResourceManager.GetString("EliminarRegistrosTU", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_GenerarCodigo_CCH_EAR &apos;?&apos;,&apos;?&apos;|CALL STR_SP_GenerarCodigo_CCH_EAR(&apos;?&apos;,&apos;?&apos;).
        /// </summary>
        internal static string GenerarCodigoEAR {
            get {
                return ResourceManager.GetString("GenerarCodigoEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT &apos;E&apos;+RIGHT(&apos;0000&apos;+LTRIM(RIGHT(ISNULL(MAX(Code),0),4)+1),4) AS Code FROM [?]$SELECT &apos;E&apos;|| RIGHT(&apos;0000&apos;||LTRIM(RIGHT(IFNULL(MAX(&quot;Code&quot;),&apos;0&apos;),4)+1),4) FROM &quot;?&quot;.
        /// </summary>
        internal static string GenerarCodigoUnicoPorTU {
            get {
                return ResourceManager.GetString("GenerarCodigoUnicoPorTU", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_Create_OPCH_XML&apos;?&apos;,&apos;?&apos;,&apos;?&apos;|CALL STR_SP_CREATE_OPCH_XML(&apos;?&apos;,&apos;?&apos;,&apos;?&apos;).
        /// </summary>
        internal static string GenerarDocumentoXML {
            get {
                return ResourceManager.GetString("GenerarDocumentoXML", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP 1 TransId FROM OJDT WHERE U_BPP_CtaTdoc = &apos;?&apos; AND U_BPP_DocKeyDest = &apos;?&apos;|SELECT TOP 1 &quot;TransId&quot; FROM OJDT WHERE &quot;U_BPP_CtaTdoc&quot; = &apos;?&apos; AND &quot;U_BPP_DocKeyDest&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ObtAsientoCompensacion {
            get {
                return ResourceManager.GetString("ObtAsientoCompensacion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP 1 AcctCode FROM OACT WHERE FormatCode = &apos;?&apos;|SELECT TOP 1 &quot;AcctCode&quot; FROM OACT WHERE &quot;FormatCode&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ObtCodigoCtaPte {
            get {
                return ResourceManager.GetString("ObtCodigoCtaPte", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT GLAccount,(SELECT ActCurr FROM OACT WHERE AcctCode=GLAccount),(SELECT AcctName FROM OACT WHERE AcctCode=GLAccount) FROM DSC1
        ///WHERE BankCode = &apos;?&apos; AND Account = &apos;?&apos;|SELECT &quot;GLAccount&quot;,(SELECT &quot;ActCurr&quot; FROM OACT WHERE &quot;AcctCode&quot;=&quot;GLAccount&quot;),(SELECT &quot;AcctName&quot; FROM OACT WHERE &quot;AcctCode&quot;=&quot;GLAccount&quot;)
        ///FROM DSC1 WHERE &quot;BankCode&quot; = &apos;?&apos; AND &quot;Account&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ObtCuentadeBancoPropio {
            get {
                return ResourceManager.GetString("ObtCuentadeBancoPropio", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT T0.Account,T1.ActCurr FROM DSC1 T0 INNER JOIN OACT T1 ON T0.GLAccount = T1.AcctCode WHERE BankCode =&apos;?&apos; AND ActCurr = &apos;?&apos;|SELECT T0.&quot;Account&quot;,T1.&quot;ActCurr&quot; FROM DSC1 T0 INNER JOIN OACT T1 ON 
        ///T0.&quot;GLAccount&quot; = T1.&quot;AcctCode&quot; WHERE &quot;BankCode&quot; =&apos;?&apos; AND &quot;ActCurr&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ObtCuentasdeBanco {
            get {
                return ResourceManager.GetString("ObtCuentasdeBanco", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT U_ER_DFLT FROM [@STR_HEMEAR2] WHERE U_empID = &apos;?&apos; AND U_ER_NMBR = &apos;?&apos;|SELECT &quot;U_ER_DFLT&quot; FROM &quot;@STR_HEMEAR2&quot; WHERE &quot;U_empID&quot; = &apos;?&apos; AND &quot;U_ER_NMBR&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ObtDimensionesXDefecto {
            get {
                return ResourceManager.GetString("ObtDimensionesXDefecto", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_DocumentosaReconciliar &apos;?&apos;,&apos;?&apos;|CALL STR_SP_LOC_DocumentosaReconciliar(&apos;?&apos;,&apos;?&apos;).
        /// </summary>
        internal static string ObtDocumentosaReconciliar {
            get {
                return ResourceManager.GetString("ObtDocumentosaReconciliar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT DISTINCT  T0.BankCode,BankName FROM ODSC T0 INNER JOIN DSC1 T1 ON T0.BankCode = T1.BankCode inner join OACT T3 ON T1.GLAccount = T3.AcctCode WHERE T3.Postable = &apos;Y&apos;|SELECT DISTINCT  T0.&quot;BankCode&quot;,&quot;BankName&quot; FROM ODSC T0 INNER JOIN DSC1 T1 ON T0.&quot;BankCode&quot; = T1.&quot;BankCode&quot; 
        ///INNER JOIN OACT T3 ON T1.&quot;GLAccount&quot; = T3.&quot;AcctCode&quot; WHERE T3.&quot;Postable&quot; = &apos;Y&apos;.
        /// </summary>
        internal static string ObtListadeBancos {
            get {
                return ResourceManager.GetString("ObtListadeBancos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_ListarSolicitudesEntregaRendir &apos;?&apos;,&apos;?&apos;|CALL STR_SP_LOC_ListarSolicitudesEntregaRendir(&apos;?&apos;,&apos;?&apos;)  .
        /// </summary>
        internal static string ObtListaSolicitudesEAR {
            get {
                return ResourceManager.GetString("ObtListaSolicitudesEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT U_ER_MNAP FROM [@STR_EARAPRDET] WHERE U_ER_EARN = &apos;?&apos; AND U_ER_NMER = &apos;?&apos;|SELECT &quot;U_ER_MNAP&quot; FROM &quot;@STR_EARAPRDET&quot; WHERE &quot;U_ER_EARN&quot; = &apos;?&apos; AND &quot;U_ER_NMER&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ObtMontoApertura {
            get {
                return ResourceManager.GetString("ObtMontoApertura", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_NumerosEntregaaRendirActivos &apos;?&apos;|CALL STR_SP_LOC_NumerosEntregaaRendirActivos(&apos;?&apos;).
        /// </summary>
        internal static string ObtNumerosdeEntregaaRendirActivos {
            get {
                return ResourceManager.GetString("ObtNumerosdeEntregaaRendirActivos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_PagosRealizadosPorNumero_CCH_EAR &apos;?&apos;,&apos;?&apos;,&apos;?&apos;|CALL STR_SP_LOC_PagosRealizadosPorNumero_CCH_EAR(&apos;?&apos;,&apos;?&apos;,&apos;?&apos;).
        /// </summary>
        internal static string ObtPagosPorNumerodeEAR {
            get {
                return ResourceManager.GetString("ObtPagosPorNumerodeEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_SaldoNumerosEntregaaRendir &apos;?&apos;|CALL STR_SP_LOC_SaldoNumerosEntregaaRendir(&apos;?&apos;).
        /// </summary>
        internal static string ObtSaldoEntregasaRendir {
            get {
                return ResourceManager.GetString("ObtSaldoEntregasaRendir", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXEC STR_SP_LOC_InfoTotalesPorCargaDocumentosEAR &apos;?&apos;|CALL STR_SP_LOC_InfoTotalesPorCargaDocumentosEAR (&apos;?&apos;).
        /// </summary>
        internal static string ObtTotalesXCarga {
            get {
                return ResourceManager.GetString("ObtTotalesXCarga", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT T3.TransId FROM [@STR_EARAPR] T0 INNER JOIN [@STR_EARAPRDET] T1 ON T0.DocEntry = T1.DocEntry INNER JOIN OVPM T3 ON T3.DocEntry = U_ER_DEPE
        ///WHERE T1.U_ER_EARN = &apos;?&apos; AND T1.U_ER_NMER = &apos;?&apos;|SELECT T3.&quot;TransId&quot; FROM &quot;@STR_EARAPR&quot; T0 INNER JOIN &quot;@STR_EARAPRDET&quot; T1 ON T0.&quot;DocEntry&quot; = T1.&quot;DocEntry&quot; INNER JOIN OVPM T3 ON T3.&quot;DocEntry&quot; = &quot;U_ER_DEPE&quot;
        ///WHERE T1.&quot;U_ER_EARN&quot; = &apos;?&apos; AND T1.&quot;U_ER_NMER&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ObtTransIdPagoaCuenta {
            get {
                return ResourceManager.GetString("ObtTransIdPagoaCuenta", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT U_ER_SCRG FROM [@STR_HEMEAR] WHERE U_empID = &apos;?&apos; AND U_ER_CDUS  = &apos;?&apos;|SELECT &quot;U_ER_SCRG&quot; FROM &quot;@STR_HEMEAR&quot; WHERE &quot;U_empID&quot; = &apos;?&apos; AND &quot;U_ER_CDUS&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ValidarCargaEAR {
            get {
                return ResourceManager.GetString("ValidarCargaEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT U_ER_SCCR FROM [@STR_HEMEAR] WHERE U_empID = &apos;?&apos; AND U_ER_CDUS  = &apos;?&apos;|SELECT &quot;U_ER_SCCR&quot; FROM &quot;@STR_HEMEAR&quot; WHERE &quot;U_empID&quot; = &apos;?&apos; AND &quot;U_ER_CDUS&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ValidarCerrarCargaEAR {
            get {
                return ResourceManager.GetString("ValidarCerrarCargaEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT U_ER_SCNT FROM [@STR_HEMEAR] WHERE U_ER_CDUS = &apos;?&apos; AND U_empID = &apos;?&apos;|SELECT &quot;U_ER_SCNT&quot; FROM &quot;@STR_HEMEAR&quot; WHERE &quot;U_ER_CDUS&quot; = &apos;?&apos; AND &quot;U_empID&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ValidarContabEAR {
            get {
                return ResourceManager.GetString("ValidarContabEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT U_ER_SRGL FROM [@STR_HEMEAR] WHERE U_empID = &apos;?&apos; AND U_ER_CDUS  = &apos;?&apos;|SELECT &quot;U_ER_SRGL&quot; FROM &quot;@STR_HEMEAR&quot; WHERE &quot;U_empID&quot; = &apos;?&apos; AND &quot;U_ER_CDUS&quot; = &apos;?&apos;.
        /// </summary>
        internal static string ValidarRegularizarSaldosEAR {
            get {
                return ResourceManager.GetString("ValidarRegularizarSaldosEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT COUNT(&apos;A&apos;) FROM [@STR_EARCRG] WHERE U_ER_NMRO = &apos;?&apos;|SELECT COUNT(&apos;A&apos;) FROM &quot;@STR_EARCRG&quot; WHERE &quot;U_ER_NMRO&quot; = &apos;?&apos;.
        /// </summary>
        internal static string VerificarCantidadNrosEAR {
            get {
                return ResourceManager.GetString("VerificarCantidadNrosEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT U_ER_SLDO,U_ER_STDO FROM [@STR_EARAPRDET] WHERE U_ER_EARN = &apos;?&apos; AND U_ER_NMER = &apos;?&apos;|SELECT &quot;U_ER_SLDO&quot;,&quot;U_ER_STDO&quot; FROM &quot;@STR_EARAPRDET&quot; WHERE &quot;U_ER_EARN&quot; = &apos;?&apos; AND &quot;U_ER_NMER&quot; = &apos;?&apos;.
        /// </summary>
        internal static string VerificarEstadoySaldoNroEAR {
            get {
                return ResourceManager.GetString("VerificarEstadoySaldoNroEAR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT empID,U_CE_PVAS FROM OHEM WHERE U_CE_PVAS IS NOT NULL|SELECT &quot;empID&quot;,&quot;U_CE_PVAS&quot; FROM OHEM WHERE &quot;U_CE_PVAS&quot; IS NOT NULL.
        /// </summary>
        internal static string VerificarProveedorAsociado {
            get {
                return ResourceManager.GetString("VerificarProveedorAsociado", resourceCulture);
            }
        }
    }
}
