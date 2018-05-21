using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockSchemBuilder
{
    static class CodePreprocessor
    {
        //удаление комментариев из кода
        public static void removeComments(ref string code)
        {
            while (code.IndexOf("//") != -1)
            {
                int StartPosition = code.IndexOf("//");
                int EndPosition = code.IndexOf("\r\n", StartPosition);
                code = code.Remove(StartPosition, EndPosition - StartPosition);
            }
            while (code.IndexOf("/*") != -1)
            {
                int StartPosition = code.IndexOf("/*");
                int EndPosition = code.IndexOf("*/", StartPosition);
                if (EndPosition == -1) code = code.Remove(StartPosition);
                else code = code.Remove(StartPosition, EndPosition - StartPosition);
            }
        }

        //разбиение кода на "слова"
        public static string[] splitToWords(string code)
        {
            for (int i = 0; code.IndexOfAny(new char[] { '(', ')', '"', ';' }, i + 1) != -1; i += 2)
            {
                i = code.IndexOfAny(new char[] { '(', ')', '"', ';' }, i);
                code = code.Insert(i + 1, " ");
                code = code.Insert(i, " ");
            }
            string[] words = code.Split(new string[] { " ", "\r\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            mergeStrings(ref words);
            return words;
        }

        //Объединение константной строки в коде в одно "Слово"
        static void mergeStrings(ref string[] words)
        {
            while(Array.IndexOf(words, "\"") != -1)
            {
                int start = Array.IndexOf(words, "\"");
                int end = Array.IndexOf(words, "\"", start + 1);
                for (int i = start + 1; i <= end; i++)
                    words[start] += words[i] + " ";
				words[start] = words[start].TrimEnd(' ');
                for (int i = end + 1; i < words.Length; i++)
                {
                    words[i - end + start] = words[i];
                }
                Array.Resize(ref words, words.Length - end + start);
            }
        }
    }
}
