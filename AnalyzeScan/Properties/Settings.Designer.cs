﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnalyzeScan.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AnalyzeScan")]
        public string Name {
            get {
                return ((string)(this["Name"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableCdf {
            get {
                return ((bool)(this["EnableCdf"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool EnableXcalibur {
            get {
                return ((bool)(this["EnableXcalibur"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Accumulation Time</string>
  <string>Accumulation Time Averaged</string>
  <string>Auto MS/MS</string>
  <string>Averages</string>
  <string>Averages Made</string>
  <string>Compound Stability</string>
  <string>Count of Scans</string>
  <string>ICC Actual</string>
  <string>ICC Target</string>
  <string>Ion Polarity</string>
  <string>Manual MS(n)</string>
  <string>Mass Range Mode</string>
  <string>MRM</string>
  <string>Multiplier Gain Target</string>
  <string>Ready Polarity</string>
  <string>Scan Begin</string>
  <string>Scan End</string>
  <string>Start Polarity</string>
  <string>Stop Polarity</string>
  <string>Target Mass</string>
  <string>Trap Drive</string>
  <string>Trap Drive Level</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection AdditionalParamsToShow {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["AdditionalParamsToShow"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Adobe Acrobat PDF files (*.pdf)|*.pdf|All files (*.*)|*.*")]
        public string SaveAsPdfFilter {
            get {
                return ((string)(this["SaveAsPdfFilter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MzXML files (*.mzxml)|*.mzxml|All files (*.*)|*.*")]
        public string SaveAsMzXmlFilter {
            get {
                return ((string)(this["SaveAsMzXmlFilter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.1")]
        public decimal IntensityCuttoffPercentage {
            get {
                return ((decimal)(this["IntensityCuttoffPercentage"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Accumulation_Time</string>
  <string>Accumulation_Time_Averaged</string>
  <string>Averages_Made</string>
  <string>Mass_Range_Mode</string>
  <string>Ion_Polarity</string>
  <string>Manual_MSn</string>
  <string>MRM</string>
  <string>Auto_MSMS</string>
  <string>Averages</string>
  <string>Start_Polarity</string>
  <string>Stop_Polarity</string>
  <string>Ready_Polarity</string>
  <string>ICC_Target</string>
  <string>Multiplier_Gain_Target</string>
  <string>Scan_Begin</string>
  <string>Scan_End</string>
  <string>Count_of_Scans</string>
  <string>Target_Mass</string>
  <string>Compound_Stability</string>
  <string>Trap_Drive_Level</string>
  <string>Trap_Drive</string>
  <string>ICC_Actual</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection ReportData {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["ReportData"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Accumulation Time Averaged,Accumulation_Time_Averaged;Footer</string>
  <string>Averages Made,Averages_Made;Footer</string>
  <string>Mass Range Mode,Mass_Range_Mode;Footer</string>
  <string>Ion Polarity,Ion_Polarity;Header</string>
  <string>Manual MSN,Manual_MSn;Footer</string>
  <string>MRM,MRM;Footer</string>
  <string>Auto MS/MS,Auto_MSMS;Footer</string>
  <string>Averages,Averages;Footer</string>
  <string>Start Polarity,Start_Polarity;Footer</string>
  <string>Stop Polarity,Stop_Polarity;Footer</string>
  <string>Ready Polarity,Ready_Polarity;Footer</string>
  <string>ICC Target,ICC_Target;Footer</string>
  <string>Multiplier Gain Target,Multiplier_Gain_Target;Footer</string>
  <string>Scan Begin,Scan_Begin;Footer</string>
  <string>Scan End,Scan_End;Footer</string>
  <string>Target Mass,Target_Mass;Footer</string>
  <string>Compound Stability,Compound_Stability;Footer</string>
  <string>Trap Drive Level,Trap_Drive_Level;Footer</string>
  <string>Trap Drive,Trap_Drive;Footer</string>
  <string>ICC Actual,ICC_Actual;Footer</string>
  <string>Count of Scans,Scan_count;Footer</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection LimpReportData {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["LimpReportData"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Users\\den\\source\\repos\\AnalyzeScan\\AnalyzeScan\\Templates\\ReportTemplate.pdf")]
        public string ReportTemplatePath {
            get {
                return ((string)(this["ReportTemplatePath"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>AnalysisDate</string>
  <string>AnalysisTime</string>
  <string>ReportDate</string>
  <string>ReportTime</string>
  <string>PatientId</string>
  <string>Tissue</string>
  <string>MedicalComments</string>
  <string>TechInfo</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection ReportFields {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["ReportFields"]));
            }
            set {
                this["ReportFields"] = value;
            }
        }
    }
}
