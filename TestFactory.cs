using System;
using System.Collections.Generic;
using System.Linq;
using TestBench.Tests;

namespace TestBench
{
    public class TestFactory
    {
        public List<Type> GetTests()
        {
            var searchType = typeof(ITest);
            var foundTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => searchType.IsAssignableFrom(p) && p != searchType);

            return foundTypes.ToList();
        }
    }
}
