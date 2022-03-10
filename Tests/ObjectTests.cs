using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench.Tests
{
    public class ObjectTests : TestableTemplate, ITest
    {
        public void ExecuteTest()
        {
            //base.ExecuteTest(GetType());
            var testObj = new CustomTestObject()
            {
                Name = "Test object",
                PrimaryNumber = 27,
                UniqueID = Guid.NewGuid()
            };
            ObjectTypePreservation(testObj);
        }

        public void ObjectTypePreservation(object data)
        {
            Console.WriteLine($"The object that was passed into this method is: {data.GetType()}");
            Console.WriteLine($"The name of that object [via GetType()]: {data.GetType().Name}");
            Console.WriteLine($"The name of that object [via nameof()]: {nameof(data)}");
            Console.WriteLine($"The object contains: {data}");
        }

        public class CustomTestObject
        {
            public string Name { get; set; }
            public int PrimaryNumber { get; set; }
            public Guid UniqueID { get; set; }
            public override string ToString()
            {
                return $"{Name}{Environment.NewLine}{PrimaryNumber}{Environment.NewLine}{UniqueID}";
            }
        }
    }
}
