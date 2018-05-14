using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockSchemBuilder.Enum;

namespace BlockSchemBuilder
{
    class functionBlock : schematicBlock
    {
        string[] words;

        public functionBlock(string text, string[] arr) : base(text, BlockTypes.Operator)
        {
			words = arr;
        }

        public void drawSchematic(object sender, EventArgs e)
        {
			schematicBlock start = new schematicBlock("Begin", BlockTypes.Start);
			CodeAnalyzer ca = new CodeAnalyzer(ref words);
			Dictionary<ExitTypes, schematicBlock> dic = ca.AnalyzeBlock(0, words.Length, start);
			dic[ExitTypes.EndofBlock].links.Add(new schematicBlock("End", BlockTypes.Start));
        }

    }
}
