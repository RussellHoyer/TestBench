using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench
{
    public static class Common
    {
        /// <summary>
        /// Writes a line to the console using the color provided.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="consoleColor"></param>
        public static void WriteLine(string message, ConsoleColor consoleColor = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        /// <summary>
        /// Writes a line (with no line break at the end) to the console using the color provided).
        /// </summary>
        /// <param name="message"></param>
        /// <param name="consoleColor"></param>
        public static void Write(string message, ConsoleColor consoleColor = ConsoleColor.DarkGreen)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the messages in question in a colorized fashion.
        /// </summary>
        /// <param name="messages">Collection of strings to be printed, split with a pipe character. First element will be colorized.</param>
        /// <param name="consoleColor"></param>
        public static void WriteParameters(IEnumerable<string> messages, ConsoleColor consoleColor = ConsoleColor.White)
        {
            foreach (var message in messages)
            {
                var splitMsg = message.Split('|');
                Write(splitMsg[0], consoleColor);
                Console.Write(splitMsg[1]);
                Console.WriteLine();
            }
        }
        public static void WriteParameters(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            WriteParameters(new List<string> { message }, consoleColor);
        }
        public static void WriteParameters(Dictionary<string, string> paramPairs, ConsoleColor color = ConsoleColor.White)
        {
            foreach (KeyValuePair<string, string> pair in paramPairs)
            {
                Console.ForegroundColor = color;
                Console.Write($"[Param]: ");
                Console.ResetColor();
                Console.Write( pair.Key );

                Console.ForegroundColor = color;
                Console.Write($"[Value]: ");
                Console.ResetColor();
                Console.Write(pair.Value);
            }
        }

        /// <summary>
        /// Reference for all implemented command line options.
        /// </summary>
        public static Dictionary<string, string> CommandLineFlags
        {
            get
            {
                return new Dictionary<string, string> {
                    { "<TestName>", "No dash required, specify the class name that implements ITest for specific testing (for multiple separate with a space)." },
                    { "-p", "Pauses execution after each test has completed." },
                    { "-h", "List all of the flags that can be used, this also exits the program without running any tests." }
                };
            }
        }

        public static string DoesntWorkRight = "*chuckles* this breaks stuff :)";
    }
}
