using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalEntropyEstimation
{
    class Operation
    {
        public char[] alphabet = new char[]
        {
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й',
            'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф',
            'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
            ' '
        };


        public float One_Letter_Frequency(char x, Text text)
        {
            float frequency = 0;
            for (int i = 0; i < text.txt.Length; i++)
            {
                if (text.txt[i] == x)
                {
                    frequency++;
                }
            }
            return frequency / text.txt.Length;
        }


        public Tuple<char[], float[]> All_Letter_Frequencey(Text text)
        {
            var array = new float[33 + text.space_presence];
            for (int i = 0; i < 33 + text.space_presence; i++)
            {
                array[i] = One_Letter_Frequency(alphabet[i], text);
            }
            if (text.space_presence == 0)
            {
                var new_alphabet = new char[33];
                Array.Copy(alphabet, new_alphabet, 33);
                return new Tuple<char[], float[]>(new_alphabet, array);
            }
            else
            {
                return new Tuple<char[], float[]>(alphabet, array);
            }
        }


        public Tuple<char[], float[]> Sort_for_AlphabetFrequency(Tuple<char[], float[]> tuple, Text text)
        {
            for (int j = 1; j < 33 + text.space_presence; j++)
            {
                var key1 = tuple.Item2[j];
                var key2 = tuple.Item1[j];
                var i = j - 1;
                while (i >= 0 && tuple.Item2[i] > key1)
                {
                    tuple.Item2[i + 1] = tuple.Item2[i];
                    tuple.Item1[i + 1] = tuple.Item1[i];
                    i = i - 1;
                }
                tuple.Item2[i + 1] = key1;
                tuple.Item1[i + 1] = key2;
            }
            Array.Reverse(tuple.Item1);
            Array.Reverse(tuple.Item2);
            return tuple;
        }


        public char[,] Text_To_Adjacent_Bigrams(Text text)
        {
            char[,] array = new char[text.txt.Length - 1, 2];
            for (int i = 0; i < text.txt.Length - 1; i++)
            {
                array[i, 0] = text.txt[i];
                array[i, 1] = text.txt[i + 1];
            }
            return array;
        }


        public char[,] Text_To_Not_Adjacent_Bigrams(Text text)
        {
            char[,] array = new char[text.txt.Length >> 1, 2];
            for (int i = 0; i < (text.txt.Length >> 1) - 1; i++)
            {
                array[i, 0] = text.txt[i << 1];
                array[i, 1] = text.txt[(i << 1) + 1];
            }
            return array;
        }


        public float One_Bigram_Frequency(char x, char y, char[,] text)
        {
            float frequency = 0;
            var bigram_amount = text.Length >> 1;
            for (int i = 0; i < bigram_amount; i++)
            {
                if (text[i, 0] == x && text[i, 1] == y)
                {
                    frequency++;
                }
            }
            return frequency / bigram_amount;
        }


        public float[] All_Bigrams_Frequency(char[,] text, int space_presence)
        {
            char[,] all_possible_bigrams = new char[(33 + space_presence) * (33 + space_presence), 2];
            for (int i = 0; i < 33 + space_presence; i++)
            {
                for (int k = 11 * 3 * i + i * space_presence; k < (i * 3 + 1) * 11 + 22 + (i + 1) * space_presence; k++)
                {
                    all_possible_bigrams[k, 0] = alphabet[i];
                    all_possible_bigrams[k, 1] = alphabet[k % (33 + space_presence)];
                }
            }

            float[] array = new float[(33 + space_presence) * (33 + space_presence)];
            for (int i = 0; i < (33 + space_presence) * (33 + space_presence); i++)
            {
                array[i] = One_Bigram_Frequency(all_possible_bigrams[i, 0], all_possible_bigrams[i, 1], text);
            }
            return array;
        }


        public char[,] Reverse2D(char[,] array)
        {
            char temp1;
            char temp2;
            var start = 0;
            var end = (array.Length >> 1) - 1;
            while (start < end)
            {
                temp1 = array[start, 0];
                temp2 = array[start, 1];
                array[start, 0] = array[end, 0];
                array[start, 1] = array[end, 1];
                array[end, 0] = temp1;
                array[end, 1] = temp2;
                start++;
                end--;
            }
            return array;
        }


        public Tuple<char[,], float[]> Sort_for_BigramFrequency(float[] array, Text text)
        {
            char[,] all_possible_bigrams = new char[(33 + text.space_presence) * (33 + text.space_presence), 2];
            for (int i = 0; i < 33 + text.space_presence; i++)
            {
                for (int k = 11 * 3 * i + (i * text.space_presence); k < (i * 3 + 1) * 11 + 22 + (i + 1) * text.space_presence; k++)
                {
                    all_possible_bigrams[k, 0] = alphabet[i];
                    all_possible_bigrams[k, 1] = alphabet[k % (33 + text.space_presence)];
                }
            }

            for (int j = 1; j < (33 + text.space_presence) * (33 + text.space_presence); j++)
            {
                var key1_1 = all_possible_bigrams[j, 0];
                var key1_2 = all_possible_bigrams[j, 1];
                var key2 = array[j];
                var i = j - 1;
                while (i >= 0 && array[i] > key2)
                {
                    array[i + 1] = array[i];
                    all_possible_bigrams[i + 1, 0] = all_possible_bigrams[i, 0];
                    all_possible_bigrams[i + 1, 1] = all_possible_bigrams[i, 1];
                    i = i - 1;
                }
                array[i + 1] = key2;
                all_possible_bigrams[i + 1, 0] = key1_1;
                all_possible_bigrams[i + 1, 1] = key1_2;
            }
            Array.Reverse(array);
            all_possible_bigrams = Reverse2D(all_possible_bigrams);
            return new Tuple<char[,], float[]>(all_possible_bigrams, array);
        }
    }
}
