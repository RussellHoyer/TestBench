using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Tests
{
    public class StringTests : TestableTemplate, ITest
    {
        public void ExecuteTest()
        {
            //RunStripNameTest();
            //InitVsDeclaredStrings();
            //FormatPhoneNumber();
            base.ExecuteTest(GetType());
        }

        public void FormatPhoneNumber()
        {
            Console.WriteLine("Formatting phone #s:");
            string[] testData = { "6502530000", "8886246432", "5106578747" };
            foreach (string phone in testData)
            {
                Console.Write($"Formatting: {phone} - ");
                Console.WriteLine(FormatPhone(phone));
            }
        }
        internal string FormatPhone(string inPhone)
        {
            return $"({inPhone.Substring(0, 3)}) {inPhone.Substring(3, 3)}-{inPhone.Substring(6, 4)}";
        }

        public void AnonObjectTest()
        {
            var anon = new { Id = 100 };
            int id = 101;
            Console.WriteLine($"Anon object: {anon}");
            Console.WriteLine($"Anon object serialized: {JsonConvert.SerializeObject(anon)}");
            Console.WriteLine($"Serlized param for comparison: {JsonConvert.SerializeObject(id)}");
        }

        public void InitVsDeclaredStrings()
        {
            string initString = "";
            //string declaredString1, declaredString2;

            Console.WriteLine($"This string was initialized: |{initString}|");
            //Console.WriteLine($"This string was declared: |{declaredString1}|");
            Console.WriteLine("Declared strings need to be assigned a value or they won't compile.");
        }

        public void LengthVsCount()
        {
            string[] s1 = new string[2];
            List<string> s2 = new List<string> { "One", "Two" };
            Console.WriteLine($"s1 was initialized with [2]");
            Console.WriteLine($"s1.Count() - {s1.Count()}");
            Console.WriteLine($"s1.Length - {s1.Length}");
            Console.WriteLine($"s2 was initialized with two elements");
            Console.WriteLine($"s2.Count - {s2.Count}");
        }

        public void SearchListAny()
        {
            List<TestStringClass> testStrings = new List<TestStringClass>() 
            { 
                new TestStringClass("test", true),
                new TestStringClass("test", true),
                new TestStringClass("test", false)
            };
            var result = testStrings.Any(a => a.BoolProperty == false);
            Console.WriteLine($"Result when searching == false (with a false item in the collection): {result}");
            testStrings.RemoveAt(2);
            result = testStrings.Any(a => a.BoolProperty == false);
            Console.WriteLine($"Result when searching == false (with zero false items in the collection): {result}");
        }

        public class TestStringClass
        {
            public string MyProperty { get; set; }
            public bool BoolProperty { get; set; }
            public TestStringClass(string myProperty, bool boolProperty)
            {
                MyProperty = myProperty;
                BoolProperty = boolProperty;
            }
        }
    }
}
