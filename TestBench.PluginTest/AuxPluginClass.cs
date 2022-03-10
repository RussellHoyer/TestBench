using System;
using System.Collections.Generic;
using System.Text;

namespace TestBench.PluginTest
{
    public class AuxPluginClass
    {
        public void ExecuteTest()
        {
            AuxPluginPrint();
        }

        private void AuxPluginPrint()
        {
            Console.WriteLine($"This line came from the Plugin Aux Test! Assembly Name: {System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName}");
        }
    }
}
