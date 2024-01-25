// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

namespace BlApi
{
    internal class program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    }
}