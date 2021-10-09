using System;
using System.IO;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome5437();
            Console.ReadKey();
        }

        static partial void welcome1264();
        private static void welcome5437()
        {
            string name;
            Console.WriteLine("Enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
