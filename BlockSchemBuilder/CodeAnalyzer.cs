using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using BlockSchemBuilder.Enum;

namespace BlockSchemBuilder
{
	class CodeAnalyzer
	{
		string[] words;
		functionBlock[] functions;

		private readonly string[] IOtags = { "Console.WriteLine", "Console.Write", "Console.Read", "Console.ReadLine" };
		private readonly string[] ProcedureTags = { "(", ")", "new" };
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

		public Dictionary<ExitTypes, List<SchemaBlock>> AnalyzeBlock(int start, int end, List<SchemaBlock> lastElement)
		{
			Dictionary<ExitTypes, List<SchemaBlock>> dict = new Dictionary<ExitTypes, List<SchemaBlock>>
			{
				[ExitTypes.EndofBlock] = new List<SchemaBlock>(),
				[ExitTypes.Return] = new List<SchemaBlock>(),
				[ExitTypes.Continue] = new List<SchemaBlock>(),
				[ExitTypes.Break] = new List<SchemaBlock>()
			};
			List<SchemaBlock> prev = new List<SchemaBlock>(lastElement);

			string currentBlockContent = "";
			BlockTypes currentBlockType = BlockTypes.Operator;

			for (int i = start; i <= end; i++)
			{
				// Conditions handling
				//
				if (words[i] == "if")
				{
					#region "if" analyze
					i++;
					while (words[++i] != ")")
						currentBlockContent += words[i] + " ";

					SchemaBlock block = new SchemaBlock(currentBlockContent, BlockTypes.Condition);
					currentBlockContent = "";
					foreach (SchemaBlock item in prev)
					{
						item.links.Add(block);
					}
					prev.Clear();
					prev.Add(block);

					if (words[++i] == "{") { dict = DictMix(dict, AnalyzeBlock(i + 1, i = getEndBracket(i) - 1, prev)); i++; }
					else dict = DictMix(dict, AnalyzeBlock(i, i = Array.IndexOf(words, ";", i), prev));
					#endregion

					#region "else if" analyze 
					while (words[i+1] == "else" && words[i + 2] == "if")
					{
						i += 3;
						while (words[++i] != ")")
							currentBlockContent += words[i] + " ";

						block = new SchemaBlock(currentBlockContent, BlockTypes.Condition);
						currentBlockContent = "";
						foreach (SchemaBlock item in prev)
						{
							item.links.Add(block);
						}
						prev.Clear();
						prev.Add(block);

						if (words[++i] == "{") { dict = DictMix(dict, AnalyzeBlock(i + 1, i = getEndBracket(i) - 1, prev)); i++; }
						else dict = DictMix(dict, AnalyzeBlock(i, i = Array.IndexOf(words, ";", i), prev));
					}
					#endregion

					#region "else" analyze
					if (words[++i] == "else")
					{
						if (words[++i] == "{") { dict = DictMix(dict, AnalyzeBlock(i + 1, i = getEndBracket(i) - 1, prev)); i++; }
						else dict = DictMix(dict, AnalyzeBlock(i, i = Array.IndexOf(words, ";", i), prev));
					}
					else
					{
						dict[ExitTypes.EndofBlock] = prev.Union(dict[ExitTypes.EndofBlock]).ToList();
						currentBlockContent += words[i] + " ";
					}
					#endregion

					prev = new List<SchemaBlock>(dict[ExitTypes.EndofBlock]);
					dict[ExitTypes.EndofBlock].Clear();
					continue;
				}

				if (words[i] == "while")
				{
					i++;
					while (words[++i] != ")")
						currentBlockContent += words[i] + " ";

					SchemaBlock block = new SchemaBlock(currentBlockContent, BlockTypes.Condition);
					currentBlockContent = "";
					foreach (SchemaBlock item in prev)
					{
						item.links.Add(block);
					}
					prev.Clear();
					prev.Add(block);

					if (words[++i] == "{") { dict = DictMix(dict, AnalyzeBlock(i + 1, i = getEndBracket(i) - 1, prev)); i++; }
					else dict = DictMix(dict, AnalyzeBlock(i, i = Array.IndexOf(words, ";", i), prev));

					foreach (SchemaBlock item in dict[ExitTypes.EndofBlock])
					{
						item.links.Add(block);
					}
					dict[ExitTypes.EndofBlock].Clear();

					foreach (SchemaBlock item in dict[ExitTypes.Continue])
					{
						item.links.Add(block);
					}
					dict[ExitTypes.Continue].Clear();

					prev = prev.Union(dict[ExitTypes.Break]).ToList();
					continue;
				}

				//Not working correct!!!
				if(words[i] == "do")
				{
					if (words[++i] == "{") { dict = DictMix(dict, AnalyzeBlock(++i, i = getEndBracket(i - 1) - 1, prev)); i++; }
					else dict = DictMix(dict, AnalyzeBlock(++i, i = Array.IndexOf(words, ";", i), prev));

					i+=2;
					while (words[++i] != ")")
						currentBlockContent += words[i] + " ";

					SchemaBlock block = new SchemaBlock(currentBlockContent, BlockTypes.Condition);
					currentBlockContent = "";

					SchemaBlock startBlock = prev[0].links[prev[0].links.Count - 1];
					block.links.Add(startBlock);

					foreach (SchemaBlock item in dict[ExitTypes.EndofBlock])
					{
						item.links.Add(block);
					}
					dict[ExitTypes.EndofBlock].Clear();

					foreach (SchemaBlock item in dict[ExitTypes.Continue])
					{
						item.links.Add(block);
					}
					dict[ExitTypes.Continue].Clear();

					prev.Clear();
					prev.Add(block);
					prev = prev.Union(dict[ExitTypes.Break]).ToList();

					dict[ExitTypes.EndofBlock] = prev;
					continue;
				}

				if(words[i] == "break")
				{
					dict[ExitTypes.Break] = dict[ExitTypes.Break].Union(prev).ToList();
					return dict;
				}

				if(words[i] == "continue")
				{
					dict[ExitTypes.Continue] = dict[ExitTypes.Continue].Union(prev).ToList();
					return dict;
				}

				if(words[i] == "return;")
				{
					dict[ExitTypes.Return] = dict[ExitTypes.Return].Union(prev).ToList();
					return dict;
				}

				if (words[i] != ";")
				{
					if (Array.IndexOf(IOtags, words[i]) != -1)
					{
						currentBlockType = BlockTypes.IO;
					}
					else if (Array.IndexOf(ProcedureTags, words[i]) != -1)
					{
						currentBlockType = currentBlockType == BlockTypes.Operator ? BlockTypes.Procedure : currentBlockType;
					}
					currentBlockContent += words[i] + " ";
				}
				else
				{
					SchemaBlock block = new SchemaBlock(currentBlockContent, currentBlockType);
					currentBlockContent = "";
					currentBlockType = BlockTypes.Operator;
					foreach (SchemaBlock item in prev)
					{
						item.links.Add(block);
					}
					prev.Clear();
					prev.Add(block);
				}
			}
			dict[ExitTypes.EndofBlock] = prev.Union(dict[ExitTypes.EndofBlock]).ToList();
			return dict;
		}

		Dictionary<ExitTypes, List<SchemaBlock>> DictMix(Dictionary<ExitTypes, List<SchemaBlock>> a, Dictionary<ExitTypes, List<SchemaBlock>> b)
		{
			Dictionary<ExitTypes, List<SchemaBlock>> temp = new Dictionary<ExitTypes, List<SchemaBlock>>();
			foreach (var itemA in a)
			{
				foreach (var itemB in b)
				{
					if(itemA.Key == itemB.Key)
					{
						temp[itemA.Key] = itemA.Value.Union(itemB.Value).ToList();
					}
				}
			}
			return temp;
		}
	}
}
