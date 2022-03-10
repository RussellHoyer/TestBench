using System;
using System.Reflection;

namespace TestBench.Tests
{
    /// <summary>
    /// Creates a base Execute method that can be used to invoke all methods for a given class type.
    /// </summary>
    public class TestableTemplate
    {
        public TestableTemplate()
        {
            //https://docs.microsoft.com/en-us/dotnet/api/system.type.getmethods?view=netframework-4.8
            //Type myType = (typeof(AssemblyTests));

            //MethodInfo[] myArrayMethodInfo = myType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            //----------------------------------------------------------------------------
            //https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase.invoke?view=netframework-4.8
            /*
            Type magicType = Type.GetType("TestBench.Tests.AssemblyTests");
            ConstructorInfo magicConstructor = magicType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = magicConstructor.Invoke(new object[] { });

            MethodInfo[] magicMethods = magicType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach(var magicMethod in magicMethods)
            {
                Console.WriteLine($"Magically invoking: {magicMethod.Name}");
                magicMethod.Invoke(magicClassObject, new object[] { });
            }
            */
            //----------------------------------------------------------------------------
        }

        /// <summary>
        /// Invokes all public and non-inherited methods declared on the class passed in. There is no specific order of execution due to .Net implementation (check MSDN on invoking methods).
        /// </summary>
        /// <param name="type">Type object for the class.</param>
        /// <remarks>Note about the logic here:
        /// Fundamentally this is going to be slower and potentially inaccurate in more complex situations. This is because we're calling this method from class
        /// objects that have already been instantiated(from the Program class finding all classes that implement ITest and instantiating them)... so that means
        /// we're instantiating an entirely new object that is calling this method (which will undoubtedly lead to bottlenecks in heavy loads).
        /// 
        /// As it's currently implemented this will only ever be able to run Public void methods with no parameters (in theory it should still invoke
        /// methods with a return value, but that value would be lost to the void), you would have to know the parameter(s) order and value(s) ahead of time in order to
        /// properly implement that.
        ///
        /// With that being said, the purpose of this is not to be efficient but more to be an example of how reflection can be used to run things dynamically.
        /// As with any use of reflection the performance hit on heavier operations/loads should be considered as it may affect performance negatively, or even
        /// complicate things (such as having multiple instances of a class, which could cause problems if implementing something like resource locking).
        ///
        /// All that aside, the TestBench isn't meant to be a tangible product so much as it's meant to be a playground to test out the function or logic of something
        /// without having to create a whole new project for it.
        /// </remarks>
        public void ExecuteTest(Type type)
        {
            try
            {
                // Get the ctor info so that we can invoke a new instance of the class
                ConstructorInfo magicConstructor = type.GetConstructor(Type.EmptyTypes);
                object magicClassObject = magicConstructor.Invoke(new object[] { });

                // Give me all of the methods that are public, instanced (think "non-static"), and declared only (i.e. only methods declared, not inherited from other classes)
                MethodInfo[] magicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                Common.WriteLine($"//-> Beginning Magic Invoke of {magicMethods.Length} methods... <-//", ConsoleColor.Cyan);

                // Iterate through the methods
                foreach (var method in magicMethods)
                {
                    try
                    {
                        // Don't execute yourself otherwise you'll end up in a recursive loop and overflow the stack
                        if (method.Name == "ExecuteTest") continue;

                        // Invoke the method, the object array here could contain parameters...however that's outside the scope of this implementation
                        Common.WriteLine($"//-> Magically invoking: [{type.Name}].{method.Name}()", ConsoleColor.Cyan);
                        method.Invoke(magicClassObject, new object[] { });
                    }
                    catch (Exception ex)
                    {
                        Common.WriteLine($"!!Error invoking: {ex.Message}", ConsoleColor.Red);
                    }
                }
            }
            catch(Exception ex)
            {
                Common.WriteLine($"!!Error constructing magic: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
