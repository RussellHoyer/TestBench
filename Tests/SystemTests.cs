using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Tests
{
    public class SystemTests : TestableTemplate, ITest
    {
        public void ExecuteTest()
        {
            //ListCommonSpecialDirs();
            //DisplayAllConsoleColors();
            base.ExecuteTest(GetType());
        }

        public void DisplayAllConsoleColors()
        {
            Console.WriteLine("Displaying all types in ConsoleColor enum:");
            foreach (var item in Enum.GetValues(typeof(ConsoleColor)))
            {
                Common.WriteLine($"ConsoleColor.{item.ToString()}", (ConsoleColor)item);
            }
            Console.WriteLine();
        }

        public void ListCommonSpecialDirs()
        {
            Console.WriteLine("Displaying common paths in Environment.SpecialFolder enum:");
            foreach (Environment.SpecialFolder sFolder in Enum.GetValues(typeof(Environment.SpecialFolder)))
            {
                Common.WriteParameters($"[{sFolder}]: |{Environment.GetFolderPath(sFolder)}");
            }
            Console.WriteLine();            
        }

        public void ListPathGets()
        {
            string curDir = Environment.CurrentDirectory;
            //string fileName = new DirectoryInfo(curDir).GetFiles("*")[0].FullName;
            string fileName = "Newtonsoft.Json.dll";
            Console.WriteLine(">Static Variables:");
            Console.WriteLine($"curDir = {curDir}");
            Console.WriteLine($"fileName = {fileName}");
            Console.WriteLine();

            Common.WriteParameters($"[Path.GetDirectoryName(curDir)]: |{Path.GetDirectoryName(curDir)}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetExtension(fileName)]: |{Path.GetExtension(fileName)}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetFileName(fileName)]: |{Path.GetFileName(fileName)}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetFileNameWithoutExtension(fileName)]: |{Path.GetFileNameWithoutExtension(fileName)}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetFullPath(fileName)]: |{Path.GetFullPath(fileName)}", ConsoleColor.Green);
            
            Common.WriteParameters($"[Path.GetInvalidFileNameChars()]: |{Common.DoesntWorkRight}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetInvalidPathChars()]: |{Common.DoesntWorkRight}", ConsoleColor.Green);
            
            Common.WriteParameters($"[Path.GetPathRoot(fileName)]: |{Path.GetPathRoot(fileName)}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetRandomFileName()]: |{Path.GetRandomFileName()}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetTempFileName()]: |{Path.GetTempFileName()}", ConsoleColor.Green);
            Common.WriteParameters($"[Path.GetTempPath()]: |{Path.GetTempPath()}", ConsoleColor.Green);

            Console.WriteLine();
        }

        public void DisplayPathing()
        {
            Console.WriteLine($"Environment.CurrentDirectory: {Environment.CurrentDirectory}");
            Console.WriteLine($"UserName: {Environment.UserName}");
            Console.WriteLine($"UserDomainName: {Environment.UserDomainName}");
            Console.WriteLine($"SystemDirectory: {Environment.SystemDirectory}");
            Console.WriteLine($"MachineName: {Environment.MachineName}");

            Console.WriteLine();
        }
    }
}
