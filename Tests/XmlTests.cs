using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestBench.Tools;

namespace TestBench.Tests
{
    public class XmlTests : TestableTemplate, ITest
    {
        private XmlDocument _xDoc = new XmlDocument();
        private readonly string xmlResourceLocation = @"TestBench._build.Debug_manifest.xml";

        public void ExecuteTest()
        {
            //ReadDocumentWithXml();
            //ReadDocumentWithXmlLinq();
            base.ExecuteTest(GetType());
        }

        public void ReadDocumentWithXmlLinq()
        {
            Common.WriteLine("Reading XML with Linq pattern...", ConsoleColor.Blue);
            XDocument document = XDocument.Load(AssemblyTools.GetResourceStream(xmlResourceLocation));
            var root = document.Element("Package");
            var installationConfiguration = root.Element("InstallConfiguration");
            var requiredFiles = root.Element("RequiredFiles").Elements("RequiredFile");

            foreach (var installElement in installationConfiguration.Elements())
            {
                Common.WriteParameters($"[InstallNode]::Name-|{installElement.Name}");
                Common.WriteParameters($"\t::Value-|{installElement.Value}");                
            }

            foreach (var reqFile in requiredFiles)
            {
                if (!reqFile.HasAttributes)
                {
                    Common.Write($"[RequiredFile]::Name-");
                    Console.Write($"{reqFile.Name}");
                    Common.Write(", Value-");
                    Console.Write($"{reqFile.Value}");
                    Console.WriteLine();
                }
                else
                {
                    Common.Write($"[RequiredFile]::Name-");
                    Console.Write($"{reqFile.Name}");
                    Common.Write(", Attr-Name ", ConsoleColor.Yellow);
                    Console.Write($"{reqFile.FirstAttribute.Name}");
                    Common.Write(" Attr-Value ", ConsoleColor.Yellow);
                    Console.Write($"{reqFile.FirstAttribute.Value}");
                    Common.Write(", Value-");
                    Console.WriteLine($"{reqFile.Value}");
                }
            }

            Common.WriteLine("Method complete.", ConsoleColor.Blue);
        }

        public void ReadDocumentWithXml()
        {
            // TODO: replace with dynamic pathing to resource file(s)
            _xDoc.Load(AssemblyTools.GetResourceStream(xmlResourceLocation));
            var xmlItems = _xDoc.SelectNodes("Package");

            var affectedFiles = _xDoc.SelectNodes("/Package/RequiredFiles/RequiredFile");
            foreach(XmlElement element in affectedFiles)
            {
                Console.WriteLine($"Node: {element.Name}, Value: {element.InnerText}");
                if (element.HasAttributes)
                {
                    foreach(XmlAttribute attr in element.Attributes)
                    {
                        Console.WriteLine($"\tAttr Name: {attr.Name}, Value: {attr.Value}");
                    }
                }
            }
        }

        public void SerializeXmlTests()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestXmlDataClass));
            
            TestXmlDataClass testClass = new TestXmlDataClass();
            testClass.StringTest = "This is a string";
            testClass.ListTest = new List<string> { "Test item 1", "Test item 2", "Test item 3" };

            xmlSerializer.Serialize(Console.Out, testClass);
            Console.WriteLine();
        }

        /// <summary>
        /// Nope. Multi-dimensial arrays aren't allowed to be serialized.
        /// </summary>
        public void AdditionalSerializeTests()
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(string[,]));
            //string[,] test = new string[2,2] { 
            //    { "x", "y" },
            //    { "a", "b" } 
            //};

            //serializer.Serialize(Console.Out, test);
        }

        public class TestXmlDataClass
        {
            public List<string> ListTest { get; set; }
            public string StringTest { get; set; }
        }
    }
}
