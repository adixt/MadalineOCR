using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MadalineOCR
{
    public class Network
    {
        public Network()
        {

        }

        public Network(string lettersDirectory)
        {
            var letterFilePaths = Directory.EnumerateFiles(lettersDirectory, "*.txt").ToArray();

            var filesParsed = new List<int[]>();

            foreach (var filePath in letterFilePaths)
            {
                var letter = File.ReadAllText(filePath)
                    .Replace("\r\n", string.Empty)
                    .Replace('-', '0')
                    .Replace('#', '1');
                if (letter.Any(c => c != '0' && c != '1'))
                {
                    Console.WriteLine(string.Format("Plik {0} jest nieprawidłowy (zawiera znaki poza '-' i '#'", filePath));
                    continue;
                }

                var arrayOfDigits = letter.Select(digit => (digit - '0')).ToArray();
                filesParsed.Add(arrayOfDigits);
                Console.WriteLine("Wczytano szablon litery " + filePath);
            }

        }
    }
}
