using System;
using System.IO;
using System.Linq;

namespace MadalineOCR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from the MADELINE Netrowk!");
            try
            {
                var network = new Network(@"../../../Letters");
                var samplesPath = Directory.EnumerateFiles(@"../../../Samples", "*.txt").ToArray();

                foreach (var samplePath in samplesPath)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("Processing " + samplePath);
                    Console.WriteLine("==========================================");

                    var sampleArray = LetterExtensions.ReadLetterFromFile(samplePath);
                    network.Process(sampleArray);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.Read();
        }
    }
}
