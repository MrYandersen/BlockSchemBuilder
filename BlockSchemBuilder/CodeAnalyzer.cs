using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockSchemBuilder
{
    class CodeAnalyzer
    {
        string[] words;
        functionBlock[] functions;

        public CodeAnalyzer(ref string[] words)
        {
            this.words = words;
            functions = new functionBlock[0];
        }

        //получение списка функций
        public void getFunctions()
        {
            string[] keywords = {"if", "while", "for", "foreach"};
            for (int i = 0; Array.IndexOf(words, "(", i) != -1;)
            {
                int index = Array.IndexOf(words, "(", i);
                if(Array.IndexOf(keywords, words[index - 1]) == -1)
                {
                    if(words[getEndBracket(index) + 1] == "{")
                    {
                        Array.Resize(ref functions, functions.Length + 1);
                        string text = "";
                        for (int j = index - 2; j <= getEndBracket(index); j++)
                        {
                            text += words[j] + " ";
                        }
                        functions[functions.Length - 1] = new functionBlock(text, getEndBracket(index) + 1, getEndBracket(getEndBracket(index) + 1));
                    }
                }
                i = index + 1;
            }
        }

        //отображение на форме кнопок для каждой функции
        public void drawFunctions(FlowLayoutPanel panel)
        {
            panel.Controls.Clear();
            for (int i = 0; i < functions.Length; i++)
            {
                Button newBut = new Button();
                newBut.AutoSize = true;
                newBut.Click += functions[i].drawSchematic;
                newBut.Text = functions[i].Content;
                panel.Controls.Add(newBut);
            }
        }

        //метод возвращает позицию закрывающей скобки
        int getEndBracket(int start)
        {
            string type = words[start];
            if(type == "(")
            {
                if(Array.IndexOf(words, "(", start) < Array.IndexOf(words, ")", start))
                {
                    int newStart = Array.IndexOf(words, "(", start) + 1;
                    int count = 0;
                    while(Array.IndexOf(words, "(", newStart) < Array.IndexOf(words, ")", newStart))
                    {
                        count++;
                        newStart = Array.IndexOf(words, "(", newStart) + 1;
                    }
                    for (int i = 0; i <= count; i++)
                        newStart = Array.IndexOf(words, ")", newStart) + 1;
                    return newStart - 1;
                }
                else return Array.IndexOf(words, ")", start);
            }
            if(type == "{")
            {
                if (Array.IndexOf(words, "{", start) < Array.IndexOf(words, "}", start))
                {
                    int newStart = Array.IndexOf(words, "{", start) + 1;
                    int count = 0;
                    while (Array.IndexOf(words, "{", newStart) < Array.IndexOf(words, "}", newStart))
                    {
                        count++;
                        newStart = Array.IndexOf(words, "{", newStart) + 1;
                    }
                    for (int i = 0; i <= count; i++)
                        newStart = Array.IndexOf(words, "}", newStart) + 1;
                    return newStart - 1;
                }
                else return Array.IndexOf(words, "}", start);
            }
            return -1;
        }


    }
}
