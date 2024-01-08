using System;

namespace Stage0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome1929();
            Welcome5247();
            Console.ReadKey();
        }

        private static void Welcome5247()
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("{0},Welcome to my first console application", name);
        }
        static partial void Welcome1929();
        
    }
}
