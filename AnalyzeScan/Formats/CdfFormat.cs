using System;
using System.IO;
using System.Runtime.InteropServices;
using AnalyzeScan.Tools;
using CDF;

namespace AnalyzeScan.Formats
{
    public class CdfFormat : FormatsCheck
    {
        private string CsharpCdfDir { get; set; }
        public string LastMessage { get; set; }
        public int status { get; set; }         /* Returned status code. */
        public long id { get; set; }
        public string FileVersion { get; set; }

        public CdfFormat()
        {
            Check();
        }
        public static void PrepareCdf()
        {

        }

        public void Check()
        {
            CsharpCdfDir = Environment.GetEnvironmentVariable("CsharpCDFDir");
            Enabled = !String.IsNullOrWhiteSpace(CsharpCdfDir);
            if (!Enabled)
            {
                Description = "CDf installation directory is not defined in Environment variable";
                return;
            }

            if (!Directory.Exists(CsharpCdfDir))
            {
                Description = $"CDf installation directory \"{CsharpCdfDir}\" is not exist";
                Enabled = false;
                return;
            }

            if (!File.Exists(Path.Combine(CsharpCdfDir, "dllcdfcsharp.dll")))
            {
                Description = $"CDf installation directory \"{CsharpCdfDir}\" doesn't contain the file dllcdfcsharp.dll";
                Enabled = false;
                return;
            }

            Enabled = true;
            Description = GetLibVersion();

        }

        private string GetLibVersion()
        {
            
            try
            {
                int release; /* CDF library release number. */
                int increment; /* CDF library incremental number. */
                string subIncrement; /* CDF library sub-incremental character. */
                int version; /* CDF library version number. */
                status = CDFAPIs.CDFgetLibraryVersion(out version, out release, out increment, out subIncrement);
                return $"CDf is ready, library version: {version}.{release}.{increment}.{subIncrement}";
            }
            catch (CDFException ex)
            {
                Enabled = false;
                return $"CDf isn't ready, {ex.Message}";
            }
        }

        public string GetFileVersion()
        {

            try
            {
                int release; /* CDF library release number. */
                int increment; /* CDF library incremental number. */
                int version; /* CDF library version number. */
                status = CDFAPIs.CDFgetVersion(id, out version, out release, out increment);
                return $"CDf file version: {version}.{release}.{increment}";
            }
            catch (CDFException ex)
            {
                Enabled = false;
                return $"CDf file isn't ready, {ex.Message}";
            }
        }

        public void ReadFile(string fileName)
        {
            var fileNameWithoutExt = fileName.Replace(Path.GetExtension(fileName), string.Empty);
            try
            {
                var cdfInfo = CDFAPIs.CDFopenCDF(fileNameWithoutExt, out var locId);
                id = locId;
                FileVersion = GetFileVersion();
            }
            catch (Exception exception)
            {
                LastMessage = exception.Message;
            }
        }
    }
}
