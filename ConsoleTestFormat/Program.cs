using System;
using ThermoFisher.Foundation.IO;

namespace ConsoleTestFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var filePath = @"d:\Xcalibur\examples\data\drugx_01.raw";
            
            try
            {
                var Raw = new RawFile();
                Raw.Open(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
