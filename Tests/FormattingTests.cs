using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Tests
{
    public class FormattingTests : TestableTemplate, ITest
    {
        public void ExecuteTest()
        {
            base.ExecuteTest(GetType());
        }

        public void DateFormatISOStandard()
        {
            var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
            Console.WriteLine("Date variable: March 1st, 2008 @ 7am");
            Console.WriteLine($"ISO 8601 standard: {date1:yyyyMMddTHH:mm:ssZ}");
            Console.WriteLine($"File friendly format: {date1:yyyyMMdd_hh-mm-ss_fff}.txt");
            Console.WriteLine($"date1.ToFileTime(): {date1.ToFileTime()}");

            const string formatString = "yyyyMMdd_hh-mm-ss_fff";
            Console.WriteLine($"Using a const string for formatting: {date1.ToString(formatString)}");
        }
    }
}
