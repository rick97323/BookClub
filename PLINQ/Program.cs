using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            new Plinq_Basis().Run();
            new Plinq_Advanced().Run();
        }

        public static void PrintEnumerable<T>(IEnumerable<T> source, [CallerMemberName] string methodName = "")
        {
            Console.WriteLine($"Method {methodName}\n");
            foreach (T item in source)
            {
                Console.WriteLine(item);
            }
        }

        public static void CustomPrint<T>(IEnumerable<T> source, Func<T, string> format, [CallerMemberName] string methodName = "")
        {
            Console.WriteLine($"Method {methodName}\n");
            foreach (T item in source)
            {
                Console.WriteLine(format(item));
            }
        }

        public static void WriteSpace()
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine();
        }
    }
}
