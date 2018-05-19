using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockSchemBuilder.Enum;

namespace BlockSchemBuilder
{
	class functionBlock : SchemaBlock
	{
		string[] words;

		public functionBlock(string text, string[] arr) : base(text, BlockTypes.Operator)
		{
			words = arr;
		}

		public void drawSchematic(object sender, EventArgs e)
		{
			SchemaBlock start = new SchemaBlock("Begin", BlockTypes.Start);
			CodeAnalyzer ca = new CodeAnalyzer(ref words);
			Dictionary<ExitTypes, List<SchemaBlock>> dic = ca.AnalyzeBlock(0, words.Length - 1, new List<SchemaBlock> { start });
			SchemaBlock end = new SchemaBlock("End", BlockTypes.Start);
			foreach (var item in dic[ExitTypes.EndofBlock])
				item.links.Add(end);
			foreach (var item in dic[ExitTypes.Return])
				item.links.Add(end);

			frm_Schema form = new frm_Schema(start);
			form.Show();
		}

	}
}
