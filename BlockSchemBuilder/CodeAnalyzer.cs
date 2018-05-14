using System;
using System.Collections.Generic;
using System.Windows.Forms;

using BlockSchemBuilder.Enum;

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
			string[] keywords = { "if", "while", "for", "foreach", "catch", "using" };
			for (int i = 0; Array.IndexOf(words, "(", i) != -1;)
			{
				int index = Array.IndexOf(words, "(", i);
				if (Array.IndexOf(keywords, words[index - 1]) == -1)
				{
					if (words[getEndBracket(index) + 1] == "{")
					{
						Array.Resize(ref functions, functions.Length + 1);
						string text = "";
						for (int j = index - 2, endBracket = getEndBracket(index); j <= endBracket; j++)
						{
							text += words[j] + " ";
						}
						functions[functions.Length - 1] = new functionBlock(text, words.Subsequence(getEndBracket(index) + 2, getEndBracket(getEndBracket(index) + 1) - getEndBracket(index) - 2));
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
			if (type == "(")
			{
				int newStart = start;
				int count = 1;
				do
				{
					newStart = Array.FindIndex(words, newStart + 1, s => s == "(" || s == ")");
					if (words[newStart] == "(")
					{
						count++;
					}
					if (words[newStart] == ")")
					{
						if (--count == 0) return newStart;
					}
				} while (newStart != -1);
			}
			if (type == "{")
			{
				int newStart = start;
				int count = 1;
				do
				{
					newStart = Array.FindIndex(words, newStart + 1, s => s == "{" || s == "}");
					if (words[newStart] == "{")
					{
						count++;
					}
					if (words[newStart] == "}")
					{
						if (--count == 0) return newStart;
					}
				} while (newStart != -1);
			}
			return -1;
		}

		public Dictionary<ExitTypes, schematicBlock> AnalyzeBlock(int start, int end, schematicBlock lastElement)
		{
			Dictionary<ExitTypes, schematicBlock> dict = new Dictionary<ExitTypes, schematicBlock>();
			schematicBlock prev = lastElement;

			string currentBlockContent = "";
			BlockTypes currentBlockType = BlockTypes.Operator;

			for (int i = start; i < end; i++)
			{
				if (words[i] != ";")
				{
					if (words[i] == "Console.WriteLine" || words[i] == "Console.Write" || words[i] == "Console.Read" || words[i] == "Console.ReadLine")
					{
						currentBlockType = BlockTypes.IO;
					}
					currentBlockContent += words[i] + " ";
				}
				else
				{
					schematicBlock block = new schematicBlock(currentBlockContent, currentBlockType);
					currentBlockContent = "";
					currentBlockType = BlockTypes.Operator;
					prev.links.Add(block);
					prev = block;
				}
			}
			dict[ExitTypes.EndofBlock] = prev;
			return dict;
		}
    }
}
