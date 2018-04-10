using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MadalineOCR
{
    public static class VectorExtensions
    {
        public static void NormalizeVector(double[] weights)
        {
            var vectorlength = Math.Sqrt(weights.Sum(x => x * x));

            if (IsNormalized(vectorlength) || vectorlength == 0)
            {
                return;
            }

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] /= vectorlength;
            }
        }

        public static bool IsNormalized(double vectorLength)
        {
            return Math.Abs(vectorLength - 1d) < 0.0001;
        }
    }

    public static class LetterExtensions
    {
        public static double[] ReadLetterFromFile(string filePath)
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
    }

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
            Weights = LetterExtensions.ReadLetterFromFile(filePath);
            VectorExtensions.NormalizeVector(Weights);
            Console.WriteLine("Wczytano szablon litery " + filePath);
        }




    }
}
