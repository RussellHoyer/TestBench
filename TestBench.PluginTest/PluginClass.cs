using System;

namespace TestBench.PluginTest
{
    public class PluginClass1
    {
        public void ExecuteTest()
        {
            PluginTestPrint();
        }

        public void PluginTestPrint()
        {
            Console.WriteLine("This line came from the Plugin Test!");
            Console.WriteLine($"Assembly Name: {System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName}");
        }
    }

    public class PluginClass2
    {
        public void ExecuteTest()
        {
            PluginTestPrint2();
        }

        private void PluginTestPrint2()
        {
            Console.WriteLine($"This line came from the Plugin Test! Assembly Name: {System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName}");
        }
    }
}
