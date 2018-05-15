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
			foreach(var item in dic[ExitTypes.EndofBlock])
			{
				item.links.Add(new SchemaBlock("End", BlockTypes.Start));
			}

		}

    }
}
