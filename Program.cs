using System;

namespace MadalineOCR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var network = new Network(@"../../../Letters");
            Console.Read();
        }
    }
}
