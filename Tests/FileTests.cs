using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Tests
{
    public class FileTests : TestableTemplate, ITest
    {
        public void ExecuteTest()
        {
            //ListFileVersions();
            base.ExecuteTest(GetType());
        }

        public void ListFileVersions()
        {
            Console.WriteLine("Getting file versions...");
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory); //(@"C:\Temp\DesktopOld\Bin");
            foreach (var item in directory.GetFiles("*.dll"))
            {
                var version = FileVersionInfo.GetVersionInfo(item.FullName);
                Console.WriteLine($"Filename: {item.Name}, Version: {version.FileVersion}");
            }
        }

        public void GetFileExtension()
        {
            Console.WriteLine("Getting files for testing...");
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory); //(@"C:\Temp\DesktopOld\Bin");
            foreach (var item in directory.GetFiles("*"))
            {
                Console.WriteLine($"The file '{item.Name}' has extension '{item.Extension}'.");
            }
        }

        public void ExtensionTest()
        {
            FileInfo tester = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*")[0];
            Console.WriteLine($"Using normal 'Name' property: {tester.Name}");
            Console.WriteLine($"Using 'ShortName' extension: {tester.ShortName()}");
        }
    }
}
