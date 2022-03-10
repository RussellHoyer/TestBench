using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Tools
{
    public class AssemblyTools
    {
        public static System.IO.Stream GetResourceStream(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }
    }
}
