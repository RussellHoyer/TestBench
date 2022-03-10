using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Tests
{
    public class AssemblyTests : TestableTemplate, ITest
    {
        private Assembly currentAssembly = Assembly.GetExecutingAssembly();

        public void ExecuteTest()
        {
            //GetCurrentVersion();
            //GetSpecificVersions();
            //GetResourceList();
            base.ExecuteTest(GetType());
        }

        public void GetCurrentVersion()
        {
            Console.WriteLine($"Assembly.GetExecutingAssembly().GetName().Version.ToString(): {Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
        }

        public void GetMethodName()
        {
            Console.WriteLine($"The current method [using MethodBase] is: {MethodBase.GetCurrentMethod().Name}");
            Console.WriteLine($"The current Assembly name is: {Assembly.GetExecutingAssembly().GetName().Name}");
            Console.WriteLine($"The calling Assembly is: {Assembly.GetCallingAssembly().GetName().Name}");
        }

        public void GetSpecificVersions()
        {
            Console.WriteLine($"Major: {Assembly.GetExecutingAssembly().GetName().Version.Major}");
            Console.WriteLine($"Minor: {Assembly.GetExecutingAssembly().GetName().Version.Minor}");
            Console.WriteLine($"Build: {Assembly.GetExecutingAssembly().GetName().Version.Build}");
            Console.WriteLine($"Revision: {Assembly.GetExecutingAssembly().GetName().Version.Revision}");
            Console.WriteLine();
            Console.WriteLine($"MajorRevision: {Assembly.GetExecutingAssembly().GetName().Version.MajorRevision}");
            Console.WriteLine($"MinorRevision: {Assembly.GetExecutingAssembly().GetName().Version.MinorRevision}");
        }

        public void GetResourceList()
        {            
            var assemblyList = currentAssembly.GetManifestResourceNames();
            foreach (var item in assemblyList)
            {
                Console.WriteLine($"Resource item [name]: {item}");
            }
        }

        public void LoadPlugins()
        {
            List<string> pluginList = Directory.GetFiles(Environment.CurrentDirectory, "TestBench.Plugin*.dll").ToList();
            if (pluginList.Count < 1)
            {
                Console.WriteLine($"Could not find any valid plugins (path: '{Environment.CurrentDirectory}').");
                return;
            }

            foreach (string pluginFile in pluginList)
            {
                Common.WriteLine($"Loading plugin file: {pluginFile.Split('\\').Last()}", ConsoleColor.Green);
                try
                {
                    Assembly pluginAsm = Assembly.LoadFrom(pluginFile);

                    //Type t = pluginAsm.GetType("TestBench.PluginTest.PluginClass");
                    var pluginTypes = pluginAsm.GetTypes();
                    
                    foreach (Type t in pluginTypes)
                    {
                        var methodInfo = t.GetMethod("ExecuteTest");
                        if (methodInfo == null)
                        {
                            Console.WriteLine($"Cannot load plugin method from: {pluginFile}");
                            Console.WriteLine("Error, cannot find ExecuteTest method or none implemented.");
                        }

                        var obj = Activator.CreateInstance(t);
                        var result = methodInfo.Invoke(obj, new object[] { });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while executing plugins! {ex.Message}");
                }
            }
        }

        private void PrintPropertyValues(object data)
        {
            string typeName = data.GetType().Name;

            Common.Write($"Displaying Property Information for: ", ConsoleColor.White);
            Common.WriteLine($"{typeName}");

            foreach (var propItem in data.GetType().GetProperties())
            {
                Common.Write($"[{propItem.Name} ");
                Common.Write($"({propItem.PropertyType})", ConsoleColor.Blue);
                Common.Write("]");
                
                object itemValue = propItem.GetValue(data);
                if (itemValue is IEnumerable<string>)
                {
                    string output = "";
                    foreach (var stringItem in (IEnumerable<string>)itemValue)
                    {
                        output += $"{stringItem};";
                    }
                    Common.WriteLine($"::{output}", ConsoleColor.Yellow);
                }
                else
                {
                    Common.WriteLine($"::{itemValue}", ConsoleColor.Yellow);
                }
            }
        }

        public void ExecutePopulateObjectTest()
        {
            // This would be a parameter of the method in any other situation
            Dictionary<string, object> inputData = new Dictionary<string, object>();
            var concreteObject = new ConcreteObject()
            {
                Prop1 = "This is a property",
                Prop2 = "This is another property",
                PropNumerical = 42,
                PropBool = false,
                PropEnumerable = new List<string>() { "Element one", "Element two" }
            };

            PrintPropertyValues(concreteObject);

            inputData.Add("Prop1", "This is a value that was set dynamically.");
            //inputData.Add("Prop2", "This was done through refleciton.");
            inputData.Add("PropNumerical", 24);
            inputData.Add("PropBool", true);
            inputData.Add("PropEnumerable", new List<string> { "Element 1x1", "Element 2x2", "Element 5x5" });
            inputData.Add("IntentionallyMissingElement", null);

            PopulateObjectFromDictionary(inputData, concreteObject);

            PrintPropertyValues(concreteObject);
        }

        private void PopulateObjectFromDictionary(Dictionary<string, object> inputData, object target)
        {
            Common.WriteLine($"[Setting data for: {target.GetType().Name}]", ConsoleColor.Magenta);
            int count = 0;
            foreach (var propItem in target.GetType().GetProperties())
            {
                count++;
                object dataItem;
                var dataHasProperty = inputData.TryGetValue(propItem.Name, out dataItem);
                if (dataHasProperty)
                {
                    // Note: You cannot do this with an anonymous object. Drat.
                    Common.Write($"Setting value for: ", ConsoleColor.White);
                    Common.WriteLine($"{propItem.Name}", ConsoleColor.Green);
                    propItem.SetValue(target, dataItem);
                }
                else
                {
                    Common.WriteLine($"No data found for property: '{propItem.Name}'", ConsoleColor.Red);
                }
            }
            Common.WriteLine($"[Processed {count} objects.]", ConsoleColor.Magenta);
        }
    }

    internal class ConcreteObject
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; set; }
        public int PropNumerical { get; set; }
        public bool PropBool { get; set; }
        public IEnumerable<string> PropEnumerable { get; set; }
    }
}
