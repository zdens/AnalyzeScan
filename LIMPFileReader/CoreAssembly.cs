using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LIMPFileReader
{
    public static class CoreAssembly
    {
        public static readonly Assembly Reference = typeof(CoreAssembly).Assembly;
        public static readonly Version Version = Reference.GetName().Version;
    }
}
