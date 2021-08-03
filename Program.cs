using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace ExperimentalEntropyEstimation
{
    class Program
    {
        public static void Frequency_of_letters_and_H1_in_SolidText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FREQUENCY OF LETTERS IN TEXT WITHOUT SPACES");
            Console.ResetColor();

            Text text = new Text("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_without_spaces.txt", 0);
            Operation operation = new Operation();

            var frequency = operation.All_Letter_Frequencey(text);
            var result = operation.Sort_for_AlphabetFrequency(frequency, text);
            for (int i = 0; i < 33; i++)
            {
                Console.WriteLine(result.Item1[i] + ":" + result.Item2[i]);
            }

            int k = 0;
            double sum = 0;
            while (result.Item2[k] != 0)
            {
                sum += result.Item2[k] * Math.Log(result.Item2[k], 2);
                k++;
            }
            sum *= -1;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Probability sum = " + result.Item2.Sum() + " ~ 1");
            Console.WriteLine("H1 = " + sum);
            Console.ResetColor();
        }


        public static void Frequency_of_letters_and_H1_in_SpaceText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FREQUENCY OF LETTERS IN TEXT WITH SPACES");
            Console.ResetColor();

            Text text = new Text("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_with_spaces.txt", 1);
            Operation operation = new Operation();

            var frequency = operation.All_Letter_Frequencey(text);
            var result = operation.Sort_for_AlphabetFrequency(frequency, text);

            for (int i = 0; i < 34; i++)
            {
                Console.WriteLine(result.Item1[i] + ":" + result.Item2[i]);
            }

            int k = 0;
            double sum = 0;
            while (result.Item2[k] != 0)
            {
                sum += result.Item2[k] * Math.Log(result.Item2[k], 2);
                k++;
            }
            sum *= -1;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Probability sum = " + result.Item2.Sum() + " ~ 1");
            Console.WriteLine("H1 = " + sum);
            Console.ResetColor();
        }


        public static void Print_Function_For_Bigrams_One(int k1, float[] result, int row_incr, int letter_correction)
        {
            Operation operation = new Operation();
            Console.Write("     ");
            for (int j = 0; j < 11; j++)
            {
                Console.Write(operation.alphabet[j] + "        ");
            }
            Console.WriteLine();
            for (int i = k1; i < k1 + 11 + letter_correction; i++)
            {
                Console.Write(operation.alphabet[i] + " ");
                for (int k = i * 11 * 3; k < (i * 3 + 1) * 11 + row_incr; k++)
                {
                    var num = string.Format("{0:0.000000}", result[k]);
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
        }


        public static void Print_Function_For_Bigrams_Two(int k2, float[] result, int row_incr, int letter_correction)
        {
            Operation operation = new Operation();
            Console.Write("     ");
            for (int j = 11; j < 22; j++)
            {
                Console.Write(operation.alphabet[j] + "        ");
            }
            Console.WriteLine();
            for (int i = k2; i < k2 + 11 + letter_correction; i++)
            {
                Console.Write(operation.alphabet[i] + " ");
                for (int k = (i * 3 + 1) * 11; k < (i * 3 + 1) * 11 + 11 + row_incr; k++)
                {
                    var num = string.Format("{0:0.000000}", result[k]);
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
        }


        public static void Print_Function_For_Bigrams_Three(int k3, float[] result, int row_incr, int letter_correction)
        {
            Operation operation = new Operation();
            Console.Write("     ");
            for (int j = 22; j < 33; j++)
            {
                Console.Write(operation.alphabet[j] + "        ");
            }
            Console.WriteLine();
            for (int i = k3; i < k3 + 11 + letter_correction; i++)
            {
                Console.Write(operation.alphabet[i] + " ");
                for (int k = (i * 3 + 1) * 11 + 11; k < (i * 3 + 1) * 11 + 22 + row_incr; k++)
                {
                    var num = string.Format("{0:0.000000}", result[k]);
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
        }


        public static void Frequency_of_adjacent_bigrams_and_H2_in_SolidText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FREQUENCY OF ADJACENT BIGRAMS IN TEXT WITHOUT SPACES");
            Console.ResetColor();

            Text text = new Text("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_without_spaces.txt", 0);
            Operation operation = new Operation();

            var bigrams_with_repetitions = operation.Text_To_Adjacent_Bigrams(text);
            var result = operation.All_Bigrams_Frequency(bigrams_with_repetitions, 0);

            for (int k = 0; k < 33; k += 11)
            {
                Print_Function_For_Bigrams_One(k, result, 0, 0);
                Print_Function_For_Bigrams_Two(k, result, 0, 0);
                Print_Function_For_Bigrams_Three(k, result, 0, 0);
            }

            double sum = 0;
            for (int k = 0; k < result.Length; k++)
            {
                if (result[k] != 0)
                {
                    sum += result[k] * Math.Log(result[k], 2);
                }
            }
            sum *= -1;
            sum = sum / 2;

            var sorted = operation.Sort_for_BigramFrequency(result, text);
            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
            for (int i = 0; i < sorted.Item2.Length; i++)
            {
                Console.WriteLine("frequancy of " + sorted.Item1[i, 0] + "" + sorted.Item1[i, 1] + " = " + sorted.Item2[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Probability sum = " + result.Sum() + " ~ 1");
            Console.WriteLine("H2 = " + sum);
            Console.ResetColor();
        }


        public static void Frequency_of_adjacent_bigrams_and_H2_in_SpaceText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FREQUENCY OF ADJACENT BIGRAMS IN TEXT WITH SPACES");
            Console.ResetColor();

            Text text = new Text("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_with_spaces.txt", 1);
            Operation operation = new Operation();

            var bigrams_with_repetitions = operation.Text_To_Adjacent_Bigrams(text);
            var result = operation.All_Bigrams_Frequency(bigrams_with_repetitions, 1);

            for (int k = 0; k < 22; k += 11)
            {
                Print_Function_For_Bigrams_One(k, result, 0, 0);
                Print_Function_For_Bigrams_Two(k, result, 0, 0);
                Print_Function_For_Bigrams_Three(k, result, 1, 0);
            }
            Print_Function_For_Bigrams_One(22, result, 0, 1);
            Print_Function_For_Bigrams_Two(22, result, 0, 1);
            Print_Function_For_Bigrams_Three(22, result, 1, 1);

            double sum = 0;
            for (int k = 0; k < result.Length; k++)
            {
                if (result[k] != 0)
                {
                    sum += result[k] * Math.Log(result[k], 2);
                }
            }
            sum *= -1;
            sum = sum / 2;

            var sorted = operation.Sort_for_BigramFrequency(result, text);
            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
            for (int i = 0; i < sorted.Item2.Length; i++)
            {
                Console.WriteLine("frequancy of " + sorted.Item1[i, 0] + "" + sorted.Item1[i, 1] + " = " + sorted.Item2[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Probability sum = " + result.Sum() + " ~ 1");
            Console.WriteLine("H2 = " + sum);
            Console.ResetColor();
        }


        public static void Frequency_of_not_adjacent_bigrams_and_H2_in_SolidText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FREQUENCY OF NOT ADJACENT BIGRAMS IN TEXT WITHOUT SPACES");
            Console.ResetColor();

            Text text = new Text("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_without_spaces.txt", 0);
            Operation operation = new Operation();

            var bigrams_with_repetitions = operation.Text_To_Not_Adjacent_Bigrams(text);
            var result = operation.All_Bigrams_Frequency(bigrams_with_repetitions, 0);

            for (int k = 0; k < 33; k += 11)
            {
                Print_Function_For_Bigrams_One(k, result, 0, 0);
                Print_Function_For_Bigrams_Two(k, result, 0, 0);
                Print_Function_For_Bigrams_Three(k, result, 0, 0);
            }

            double sum = 0;
            for (int k = 0; k < result.Length; k++)
            {
                if (result[k] != 0)
                {
                    sum += result[k] * Math.Log(result[k], 2);
                }
            }
            sum *= -1;
            sum = sum / 2;

            var sorted = operation.Sort_for_BigramFrequency(result, text);
            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
            for (int i = 0; i < sorted.Item2.Length; i++)
            {
                Console.WriteLine("frequancy of " + sorted.Item1[i, 0] + "" + sorted.Item1[i, 1] + " = " + sorted.Item2[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Probability sum = " + result.Sum() + " ~ 1");
            Console.WriteLine("H2 = " + sum);
            Console.ResetColor();
        }


        public static void Frequency_of_not_adjacent_bigrams_and_H2_in_SpaceText()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("FREQUENCY OF NOT ADJACENT BIGRAMS IN TEXT WITH SPACES");
            Console.ResetColor();

            Text text = new Text("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_with_spaces.txt", 1);
            Operation operation = new Operation();

            var bigrams_with_repetitions = operation.Text_To_Not_Adjacent_Bigrams(text);
            var result = operation.All_Bigrams_Frequency(bigrams_with_repetitions, 1);

            for (int k = 0; k < 22; k += 11)
            {
                Print_Function_For_Bigrams_One(k, result, 0, 0);
                Print_Function_For_Bigrams_Two(k, result, 0, 0);
                Print_Function_For_Bigrams_Three(k, result, 1, 0);
            }
            Print_Function_For_Bigrams_One(22, result, 0, 1);
            Print_Function_For_Bigrams_Two(22, result, 0, 1);
            Print_Function_For_Bigrams_Three(22, result, 1, 1);

            double sum = 0;
            for (int k = 0; k < result.Length; k++)
            {
                if (result[k] != 0)
                {
                    sum += result[k] * Math.Log(result[k], 2);
                }
            }
            sum *= -1;
            sum = sum / 2;

            var sorted = operation.Sort_for_BigramFrequency(result, text);
            Console.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
            for (int i = 0; i < sorted.Item2.Length; i++)
            {
                Console.WriteLine("frequancy of " + sorted.Item1[i, 0] + "" + sorted.Item1[i, 1] + " = " + sorted.Item2[i]);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Probability sum = " + result.Sum() + " ~ 1");
            Console.WriteLine("H2 = " + sum);
            Console.ResetColor();
        }


        public static void Parsing()
        {
            var txt1 = File.ReadAllText("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings.txt", Encoding.Default);
            txt1 = new string(txt1.Where(c => char.IsLetter(c) || c == ' ').ToArray()).ToLower();
            txt1 = Regex.Replace(txt1, "[A-Za-z ]", " ");
            txt1 = Regex.Replace(txt1, @"\s+", " ");
            txt1 = txt1.Trim();

            StreamWriter reader1 = new StreamWriter("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_with_spaces.txt");
            reader1.Write(txt1);
            reader1.Close();

            var txt2 = File.ReadAllText("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings.txt", Encoding.Default);
            txt2 = new string(txt2.Where(c => char.IsLetter(c)).ToArray()).ToLower();
            txt2 = Regex.Replace(txt2, "[A-Za-z ]", "");

            StreamWriter reader2 = new StreamWriter("C:/Users/iptku/source/repos/ExperimentalEntropyEstimation/ExperimentalEntropyEstimation/LordOfRings_without_spaces.txt");
            reader2.Write(txt2);
            reader2.Close();
        }


        static void Main(string[] args)
        {
            Parsing();
            Frequency_of_letters_and_H1_in_SolidText();
            Frequency_of_letters_and_H1_in_SpaceText();
            Frequency_of_adjacent_bigrams_and_H2_in_SolidText();
            Frequency_of_adjacent_bigrams_and_H2_in_SpaceText();
            Frequency_of_not_adjacent_bigrams_and_H2_in_SolidText();
            Frequency_of_not_adjacent_bigrams_and_H2_in_SpaceText();
        }
    }
}

