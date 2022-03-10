using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestBench.Tests;

namespace TestBench
{
    class Program
    {
        static void Main(string[] args)
        {
            Common.WriteLine($"||{Assembly.GetExecutingAssembly().GetName().Name} - Version {Assembly.GetExecutingAssembly().GetName().Version}||", ConsoleColor.Magenta);

            //--------Debugging interrupt--------------------------
            //TestableTemplate testable = new TestableTemplate();
            //-----------------------------------------------------

            List<Type> testList = new TestFactory().GetTests();
            bool pauseExection = (args != null ? args.Contains("-p") : false);
            if (args != null && args.Contains("-h"))
            {
                ListHelpCommands();
                Environment.Exit(0);
            }

            if (args.Length >= 1)
            {
                Common.WriteLine("//-> Parsing command line arguments...", ConsoleColor.Magenta);
                List<Type> clTestList = ParseTestCLArgs(args, testList);

                if (clTestList.Count > 0)
                {
                    Common.WriteLine("//-> Running specified tests...", ConsoleColor.Magenta);
                    testList = clTestList;
                }
                else
                {
                    Common.WriteLine("//-> There were no tests that matched your specifications, running default test list.", ConsoleColor.Yellow);
                }
            }

            int counter = 0;
            int total = testList.Count;

            foreach (var testObj in testList)
            {
                try
                {
                    counter++;
                    ITest test = Activator.CreateInstance(testObj) as ITest;
                    if (test == null)
                    {
                        Common.WriteLine($"//->WARN: '{testObj.Name}' object could not be instantiated.", ConsoleColor.Yellow);
                    }

                    Common.WriteLine($"//-> Running test: {testObj.Name} - {counter} out of {total} <-//", ConsoleColor.Blue);
                    test.ExecuteTest();
                    Common.WriteLine(@"//-> Test execution complete. <-//", ConsoleColor.Blue);

                    if (pauseExection)
                    {
                        Common.WriteLine("//-> Paused, press enter to continue to the next test.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Common.WriteLine($"!!An error occurred: {ex.Message}", ConsoleColor.Red);
                    Common.WriteLine(@"//-> Test execution aborted. <-//", ConsoleColor.Blue);
                }
            }

            Console.WriteLine("Program complete.");
#if DEBUG
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
#endif
        }

        private static List<Type> ParseTestCLArgs(string[] args, List<Type> types)
        {
            List<Type> validTypes = new List<Type>();

            foreach (var clArg in args)
            {
                Common.Write($"Validating: ", ConsoleColor.Magenta);
                Common.Write(clArg, ConsoleColor.White);

                if (types.Exists(t => t.Name == clArg))
                {
                    Common.WriteLine(" - Valid");
                    validTypes.Add(types.First(t => t.Name == clArg));
                }
                else if (Common.CommandLineFlags.ContainsKey(clArg))
                {
                    Common.WriteLine($" - Special: command line flag, {Common.CommandLineFlags[clArg]}", ConsoleColor.Cyan);
                }
                else
                {
                    Common.WriteLine(" - Invalid: there are no classes by this name that implement ITest.", ConsoleColor.Red);
                }
            }

            Common.WriteLine("Finished command line parsing.", ConsoleColor.Magenta);
            return validTypes;
        }

        private static void ListHelpCommands()
        {
            Common.WriteLine("The following are parameters that can be used in the command line:", ConsoleColor.Blue);
            foreach (var item in Common.CommandLineFlags)
            {
                Common.WriteParameters($"Flag: {item.Key} - |{item.Value}", ConsoleColor.Cyan);
            }
        }
    }
}
