using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MadalineOCR
{
    public class Neuron
    {
        public double[] Weights { get; private set; }
        public string Letter { get; private set; }
        public Neuron()
        {

        }

        public Neuron(string filePath)
        {
            Letter = Path.GetFileNameWithoutExtension(filePath);
            Weights = ReadLetterFromFile(filePath);
            NormalizeVector(Weights);
            Console.WriteLine("Wczytano szablon litery " + filePath);
        }

        private double[] ReadLetterFromFile(string filePath)
        {
            var letter = File.ReadAllText(filePath)
                .Replace("\r\n", string.Empty)
                .Replace('-', '0')
                .Replace('#', '1');

            if (letter.Any(c => c != '0' && c != '1'))
            {
                var ex = string.Format("Plik {0} jest nieprawidłowy (zawiera znaki poza '-' i '#'", filePath);
                throw new ArgumentException(ex);
            }

            var arrayOfDigits = letter.Select(digit => (double)(digit - '0')).ToArray();
            return arrayOfDigits;
        }

        private static void NormalizeVector(double[] weights)
        {
            var vectorlength = Math.Sqrt(weights.Sum(x => x * x));
            
            if (IsNormalized(vectorlength))
            {
                return;
            }

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] /= vectorlength;
            }
        }

        private static bool IsNormalized(double vectorLength)
        {
            return Math.Abs(vectorLength - 1d) < 0.0001;
        }
    }
}
