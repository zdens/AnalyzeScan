using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
//using EDAL;
//using Agilent.MassSpectrometry.DataAnalysis;
//using MSFileReaderLib;
//using EDAL;
using LIMPFileReader;
using Net.CompassXport;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Factory;
using Microsoft.Research.Science.Data.Imperative;
using NLog;
using NLog.Config;
using NLog.Targets;
using LIMPFileReader.Formats.Mzxml;
using System.Xml.Serialization;
using System.IO;
using LIMPFileReader.Formats.Bruker;
using AnalyzeScan;
using TestApp.Properties;
using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;
using LIMPFileReader.Helpers;

namespace TestApp
{
    class Program //: CDFAPIs
    {
        unsafe static void Main(string[] args)
        {
            #region CDF example var
        //    void* id;
        //    int status;

        //    int N_DIMS = 2;
        //    int DIM_0_SIZE = 2;
        //    int DIM_1_SIZE = 3;

        //    int zN_DIMSa = 1;
        //    int zDIM_0_SIZEa = 5;
        //    int zNUM_ELEMSa = 8;

        //    int dim_n;
        //    int encoding = IBMPC_ENCODING;
        //    int actual_encoding = IBMPC_ENCODING;
        //    int majority = ROW_MAJOR;
        //    int numDims = N_DIMS;
        //    int[] dimSizes = new int[2] { 2, 3 };
        //    int zNumDimsA = zN_DIMSa;
        //    int[] zDimSizesA = new int[1] { 5 };
        //    int var1DataType = CDF_INT2;
        //    int var1DataTypeNew = CDF_UINT2;
        //    int var2DataType = CDF_REAL4;
        //    int var3DataType = CDF_CHAR;
        //    int var3DataTypeNew = CDF_UCHAR;
        //    int var1DataType_out, var2DataType_out, var3DataType_out;
        //    int var1NumElements = 1;
        //    int var2NumElements = 1;
        //    int var3NumElements = zNUM_ELEMSa;
        //    int var3NumElementsNew = zNUM_ELEMSa;
        //    int var1NumElements_out, var2NumElements_out, var3NumElements_out;
        //    int var1Num_out, var2Num_out, var3Num_out, varNum_out1, varNum_out2;
        //    short[,] var1Values = new short[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
        //    float[,] var2Values = new float[2, 3] {{1.0F,2.0F,3.0F},
        //                                {4.0F,5.0F,6.0F}};
        //    short[,] var1Values_out = new short[2, 3];
        //    float[,] var2Values_out = new float[2, 3];
        //    string[] var3Values = new string[5] {
        //  "11111111","22222222","33333333","44444444","55555555"};
        //    short var1Value_out;
        //    float var2Value_out;
        //    string var3Value_out;
        //    int recNum = 0;
        //    int recNumOut;
        //    int recStart = 2;
        //    int recCount = 1;
        //    int recInterval = 1;
        //    int[] indices = new int[2];
        //    int[] counts = new int[2] { 2, 3 };
        //    int[] intervals = new int[2] { 1, 1 };
        //    int[] zIndicesA = new int[1];
        //    int[] zCounts = new int[1] { 5 };
        //    int[] zIntervals = new int[1] { 1 };
        //    short[,] var1Buffer_out = new short[2, 3];
        //    float[,] var2Buffer_out = new float[2, 3];
        //    //  string[] var3Buffer_out = new string[5];
        //    string[] var3Buffer_out;
        //    int attrNum_out;
        //    int entryNum = 2;
        //    int maxEntry_out;
        //    int attrScope = GLOBAL_SCOPE;
        //    int attrScope2 = VARIABLE_SCOPE;
        //    int attrScope3 = VARIABLE_SCOPE;
        //    int attrScope_out;
        //    int entryDataType = CDF_INT2;
        //    int entryDataTypeNew = CDF_UINT2;
        //    int entryDataType_out;
        //    int entryNumElems = 1;
        //    int entryNumElems_out;
        //    short entryValue = 1;
        //    short entryValue_out;
        //    int encoding_out;
        //    int majority_out;
        //    int[] dimSizes_out = new int[2];
        //    int zNumDimsA_out;
        //    int[] zDimSizesA_out = new int[1];
        //    int maxRec_out;
        //    int numAttrs_out;
        //    int version_out;
        //    int release_out;
        //    int increment_out;
        //    string subincrement_out;
        //    int numDimsX, encodingX, majorityX, maxrRecX, nrVarsX, nAttrsX;
        //    int numDimsY, encodingY, majorityY, maxrRecY, nrVarsY, nzVarsY,
        //           nAttrsY, maxzRecY;
        //    int[] dimSizesX = new int[2], dimSizesY = new int[2];
        //    int x0, x1;
        //    int var1RecVariance = VARY;
        //    int var1RecVarianceNew = NOVARY;
        //    int var2RecVariance = VARY;
        //    int var3RecVariance = VARY;
        //    int var3RecVarianceNew = NOVARY;
        //    int var1RecVariance_out, var2RecVariance_out, var3RecVariance_out;
        //    int[] var1DimVariances = new int[2] { VARY, VARY };
        //    int[] var1DimVariancesNew = new int[2] { NOVARY, NOVARY };
        //    int[] var2DimVariances = new int[2] { VARY, VARY };
        //    int[] var3DimVariances = new int[1] { VARY };
        //    int[] var3DimVariancesNew = new int[1] { NOVARY };
        //    int[] var1DimVariances_out = new int[2],
        //           var2DimVariances_out = new int[2],
        //           var3DimVariances_out = new int[1];
        //    string var1Name = "VAR1a";
        //    string var2Name = "VAR2a";
        //    string var3Name = "zVARa1";
        //    string new_var1Name = "VAR1b";
        //    string new_var2Name = "VAR2b";
        //    string new_var3Name = "zVARa2";
        //    string var1Name_out,
        //           var2Name_out,
        //           var3Name_out;
        //    string attrName = "ATTR1";
        //    string attrName2 = "ATTR2";
        //    string attrName3 = "ATTR3";
        //    string new_attrName = "ATTR1a";
        //    string attrName_out;
        //    string CopyrightText;
        //    string errorText;
        //    byte zEntryValue1 = 4;
        //    byte zEntryValue1Out;
        //    double zEntryValue2 = 4.0;
        //    double zEntryValue2Out;
        //    int numZvars, maxGentry, numGentries,
        //           maxZentry, numZentries, numGattrs, numVattrs;
        //    int cacheOut1, cacheOut2, cacheOut3;
        //    short pad1 = -999;
        //    float pad2 = -8.0F;
        //    string pad3 = "********";
        //    short pad1out;
        //    float pad2out;
        //    string pad3out;
        //    int blockingfactor1 = 3;
        //    int blockingfactor2 = 4;
        //    int blockingfactor3 = 5;
        //    int blockingfactorOut1, blockingfactorOut2, blockingfactorOut3;
        //    int[] indicesOut = new int[10],
        //           countsOut = new int[10],
        //           intervalsOut = new int[10];
        //    int formatOut;
        //    int maxAllocOut1, maxAllocOut2, maxAllocOut3;
        //    int maxRecOut1, maxRecOut2, maxRecOut3, maxRecOut;
        //    int allocRecs1 = 10;
        //    int allocRecs2 = 15;
        //    int allocRecs3 = 8;
        //    int[] varNs1 = new int[1] { 0 }, varNs2 = new int[1] { 2 };
        //    string[] varsRecBuffer2 = new string[5] {
        //  "%%%%%%%%","%%%%%%%%","%%%%%%%%","%%%%%%%%","%%%%%%%%"
        //};
        //    string[] varsRecBuffer2Out = new string[5];


            #endregion

            #region CDF example create/open file
            //id = (void*)0;
            //try
            //{
            //    status = CDFcreate("TEST", numDims, dimSizes, encoding, majority, &id);
            //    //status = CDFopen("TEST", numDims, dimSizes, encoding, majority, &id);
            //}
            //catch (CDFException ex)
            //{
            //    int stat = ex.CDFgetCurrentStatus();
            //    if (stat < CDF_OK)
            //    {
            //        id = null;
            //        if (stat == CDF_EXISTS)
            //        {
            //            status = CDFopen("TEST", &id);
            //            if (status < CDF_OK) QuitCDF("1.0", status, id);

            //            status = CDFdeleteCDF(id);
            //            if (status < CDF_OK) QuitCDF("1.1", status, id);

            //            status = CDFcreate("TEST", numDims, dimSizes, encoding, majority, &id);
            //            if (status < CDF_OK) QuitCDF("1.2", status, id);
            //            status = CDFsetFormat(id, MULTI_FILE);
            //            if (status < CDF_OK) QuitCDF("1.3", status, id);
            //        }
            //        else
            //            QuitCDF("1.4", stat, id);
            //    }
            //}

            //try
            //{
            //    status = CDFsetFormat(id, MULTI_FILE);

            //    /******************************************************************************
            //    * Create variables and set/confirm cache sizes,etc.
            //    ******************************************************************************/

            //    status = CDFcreatezVar(id, var1Name, var1DataType, var1NumElements,
            //                           numDims, dimSizes, var1RecVariance,
            //                           var1DimVariances, &var1Num_out);

            //    status = CDFputzVarPadValue(id, var1Num_out, &pad1);

            //    status = CDFcreatezVar(id, var2Name, var2DataType, var2NumElements,
            //                           numDims, dimSizes, var2RecVariance,
            //                           var2DimVariances, &var2Num_out);

            //    status = CDFputzVarPadValue(id, var2Num_out, &pad2);

            //    status = CDFcreatezVar(id, var3Name, var3DataType, var3NumElements,
            //                   zNumDimsA, zDimSizesA, var3RecVariance,
            //                   var3DimVariances, &var3Num_out);

            //    status = CDFputzVarPadValue(id, var3Num_out, pad3);

            //    status = CDFsetzVarsCacheSize(id, 5);

            //    status = CDFgetzVarCacheSize(id, 0, &cacheOut1);

            //    status = CDFgetzVarPadValue(id, 0, &pad1out);

            //    status = CDFgetzVarCacheSize(id, 1, &cacheOut2);

            //    status = CDFgetzVarPadValue(id, 1, &pad2out);

            //    status = CDFgetzVarCacheSize(id, 2, &cacheOut3);

            //    status = CDFgetzVarPadValue(id, 2, out pad3out);

            //    if (cacheOut1 != 5) QuitCDF("2.14", status, id);
            //    if (cacheOut2 != 5) QuitCDF("2.15", status, id);
            //    if (cacheOut3 != 5) QuitCDF("2.16", status, id);
            //    if (pad1out != pad1) QuitCDF("2.17", status, id);
            //    if (pad2out != pad2) QuitCDF("2.18", status, id);
            //    if (String.Compare(pad3out, pad3) != 0) QuitCDF("2.19", status, id);

            //    status = CDFsetzVarCacheSize(id, 0, 4);

            //    status = CDFsetzVarCacheSize(id, 2, 8);

            //    status = CDFgetzVarCacheSize(id, 0, &cacheOut1);

            //    status = CDFgetzVarCacheSize(id, 1, &cacheOut2);

            //    status = CDFgetzVarCacheSize(id, 2, &cacheOut3);

            //    if (cacheOut1 != 4) QuitCDF("2.25", status, id);
            //    if (cacheOut2 != 5) QuitCDF("2.26", status, id);
            //    if (cacheOut3 != 8) QuitCDF("2.27", status, id);

            //    /******************************************************************************
            //    * Modify variables.
            //    ******************************************************************************/

            //    status = CDFsetzVarDataSpec(id, 0, var1DataTypeNew);

            //    status = CDFsetzVarRecVariance(id, 0, var1RecVarianceNew);

            //    status = CDFsetzVarDimVariances(id, 0, var1DimVariancesNew);

            //    status = CDFsetzVarInitialRecs(id, 0, 1);

            //    status = CDFsetzVarDataSpec(id, 2, var3DataTypeNew);

            //    status = CDFsetzVarRecVariance(id, 2, var3RecVarianceNew);

            //    status = CDFsetzVarDimVariances(id, 2, var3DimVariancesNew);

            //    status = CDFsetzVarInitialRecs(id, 2, 1);

            //    /******************************************************************************
            //    * Close CDF.
            //    ******************************************************************************/

            //    status = CDFcloseCDF(id);

            //    /******************************************************************************
            //    * Reopen CDF.
            //    ******************************************************************************/
            //    status = CDF_VERSION_;
            //    //status = CDFopen("TEST";


            //    var cdfFilePath = @"test1";

            //    status = CDFopen(cdfFilePath, &id);

            //    status = CDFsetDecoding(id, HOST_DECODING);

                #endregion

                Console.WriteLine("Hello World!");


            var rtDouble = 0.030618333333;

            var rtTS = TimeSpan.FromMinutes(rtDouble);

            var dt = new DateTime();
            dt = DateTime.Now;
            var s = dt.ToString("dd-MM-yyyy HH:mm:ss");

            //var rtString = rtTS.ToString("")

            //ParseTime("2,922496");
            //var testStr = @"PT0.27110400S";
            //var testStr2 = @"P3Y6M4DT12H30M5S";
            //var regEx = new Regex(@"P(\d+Y)?(\d+M)?(\d+D)?T(\d+H)?(\d+M)?(\d+\.\d+S)?");
            //var groups = regEx.Split(testStr);
            //var groups2 = regEx.Split(testStr2);

            //TimeSpan sec, min, hour;

            //foreach (var group in groups)
            //{
            //    if (string.IsNullOrEmpty(group))
            //        continue;
            //    if (group.Contains("S"))
            //        sec = TimeSpan.FromSeconds(GetDigit(group, "S"));
            //    if (group.Contains("M"))
            //        min = TimeSpan.FromMinutes(GetDigit(group, "M"));
            //    if (group.Contains("H"))
            //        hour = TimeSpan.FromHours(GetDigit(group, "H"));
            //}

            //var ttt = new 

            //var filePath = @"c:\Users\den\Documents\ms_mzml\Analysis_1.mzXML";
            //var filePath = @"c:\Users\den\temp\2'_MeOH_neg_after_drying_auto-msms.raw";
            var filePath = @"c:\Users\den\temp\INTACT_747.d";

            //var outFilePath = @"c:\Users\den\Documents\ms_mzml\Analysis.mzxml";
            //var ggg = Microsoft.Research.Science.Data.DataSet.d
            //DataSetFactory.SearchFolder(@"c:\Users\den\source\repos\ScientificDataSet\sln\CodePlexInstaller\Release\bin\");
            //    var cdfFilePath = @"c:\Users\den\Documents\ms_cdf\0_00_1178-15_FT100k.cdf";
            //var factory = Microsoft.Research.Science.Data.Factory.DataSetFactory.Create(cdfFilePath);

            //var fac = Microsoft.Research.Science.Data.Factory.DataSetFactory.GetRegisteredProviders();

            //var fac1 = new System.Type()

            //var factory = Microsoft.Research.Science.Data.Factory.DataSetFactory.Create(cdfFilePath);
            Logger logger;
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.FileName = @"log13.txt";
            //fileTarget.Layout = "${logger};${message}";
            var rule2 = new LoggingRule("*", LogLevel.Info, fileTarget);
            var excRule = new LoggingRule("${longdate} ${message} ${exception:format=tostring}", LogLevel.Error, fileTarget);
            config.LoggingRules.Add(rule2);


            LogManager.Configuration = config;
            logger = LogManager.GetLogger("Main");
            //DataSetFactory.SearchFolder(Environment.CurrentDirectory);

            //var cdf = DataSet.Open(cdfFilePath);

            //var ttt1 = cdf.Metadata;
            //logger = LogManager.GetLogger("Metadata");
            //foreach (var tt3 in ttt1)
            //{
            //    var t4 = tt3.Key;
            //    var t5 = tt3.Value;
            //    logger.Info("{0};{1}", t4, t5);
            //}

            //var var2 = cdf.Dimensions;
            //var variables = cdf.Variables;
            //logger = LogManager.GetLogger("Variables");
            //foreach (var varel in variables)
            //{
            //    Type t = varel.TypeOfData;
            //    var val = string.Empty;

            //    //if (t.FullName.Contains("Int32"))
            //    //{
            //    //    var ddd = cdf.GetData<int[]>(varel.ID);
            //    //}
            //    //if (t.FullName.Contains("Double"))
            //    //{
            //    //    var ddd = cdf.GetData<double[]>(varel.ID);
            //    //}
            //    if (t.FullName.Contains("SByte"))
            //    {
            //        val = cdf.GetData<SByte>(varel.ID).ToString();
            //    }
            //    logger.Info("{0};{1};{2};{3}", varel.Name, varel.TypeOfData, varel.Rank, val);
            //}

            //var var3 = cdf.GetMultipleData(dr);
            //var filePath = @"d:\Xcalibur\data\Nano_test\test_sp_a2_20170829.raw";
            //IsAlreadyRegistered();
            //var analysisold = new EDAL.MSAnalysis();

            //EDAL.IMSAnalysis2 anal2 = (EDAL.IMSAnalysis2)analysisold;

            //var xxx = new mzXMLConverter();
            //xxx.ConvertAndLog2(filePath, outFilePath, 2, false);

            //anal2.Open(filePath);


            //LoadAssembly();
            string outPath = "test.mzxml";
            string outpdf = "test_out2.pdf";
            string templatePath = @"c:\Users\den\source\repos\AnalyzeScan\Scribus\ReportTemplate.pdf";
            //xraw.IXRawfile2


            double tt = 0;
                try
                {
                
                    var msFile = new MsData(filePath, logger, Settings.Default.LimpReportData);

                    var fileType = msFile.ScanType;
                    var ttt = (LimpXml)msFile.ScanData;
                Spectrum spectrum = ttt.GetSpectrum();

                object o1;
                StatConnector sc1 = new StatConnector();
                try
                {
                    sc1.Init("R");
                    sc1.SetSymbol("masses", spectrum.Masses);
                    sc1.SetSymbol("intens", spectrum.Intensities);

                    sc1.EvaluateNoReturn("library('MALDIquant')");
                    sc1.Evaluate("s <- createMassSpectrum(mass=masses, intensity = intens, metaData = list(name='Spectrum1'))");
                    o1 = sc1.GetSymbol("s");
                }
                catch (Exception exc)
                {
                    var strm = exc.Message;
                    var strm2 = sc1.GetErrorText();
                    var str = strm2;
                }
                //var dataToExport = new Exports(ttt);
                //var res = Exports.ToPdf(dataToExport, outpdf, null, null, templatePath);

                //var res = Exports.ToPdf(dataToExport, saveFileDialog1.FileName, this);
                //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                //{

                //    if (!res.Equals("OK"))
                //        MessageBox.Show(res, "Error", MessageBoxButtons.OK);
                //    else
                //        MessageBox.Show("Export is finished", "Ready", MessageBoxButtons.OK);
                //}
                //var t2 = (LIMPFileReader.Formats.Bruker.MsXCompass)ttt;

                //XmlSerializer ser = new XmlSerializer(ttt.GetType());

                //TextWriter writer = new StreamWriter(outPath);
                //ser.Serialize(writer, (mzXML)msFile.ScanData);
                //writer.Close();

                //Console.WriteLine("opened file: " + t2);
            }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            //}
            //catch (CDFException ex)
            //{
            //    Console.WriteLine("Error... " + ex);
            //}
            //finally
            //{
            //    try
            //    {
            //        status = CDFselect(id);
            //        status = CDFcloseCDF(id);
            //    }
            //    catch (CDFException ex) { }
            //}
        }

        //unsafe static void QuitCDF(string where, int status, void* id)
        //{
        //    string text;
        //    Console.WriteLine("Aborting at " + where);
        //    if (status < 0)
        //    {
        //        status = CDFgetStatusText(status, out text);
        //        Console.WriteLine(" " + text);
        //    }
        //    status = CDFcloseCDF(id);
        //    Console.WriteLine("...test aborted.");
        //}



        //public class Analyzis : MSAnalysis
        //{
        //    [DllImport(@"c:\Program Files\Bruker Daltonik\CompassXtract\CompassXtractMS.dll")]
        //    public static extern void Open(string DataFilePath);

        //    public void Open(string DataFilePath)
        //    {
        //        return Open(DataFilePath);
        //    }

        //    public void Refresh()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public bool HasAnalysisData(ref string pIn)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public dynamic GetAnalysisData(ref string pIn)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public MSSpectrumCollection MSSpectrumCollection => throw new NotImplementedException();

        //    public string OperatorName => throw new NotImplementedException();

        //    public string AnalysisName => throw new NotImplementedException();

        //    public DateTime AnalysisDateTime => throw new NotImplementedException();

        //    public string AnalysisDateTimeIsoString => throw new NotImplementedException();

        //    public string SampleName => throw new NotImplementedException();

        //    public string MethodName => throw new NotImplementedException();

        //    public InstrumentFamily InstrumentFamily => throw new NotImplementedException();

        //    public string InstrumentDescription => throw new NotImplementedException();

        //    public string AnalysisDescription => throw new NotImplementedException();
        //}
    }
}
