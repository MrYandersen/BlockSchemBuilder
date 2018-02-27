using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockSchemBuilder
{
    class functionBlock : schematicBlock
    {
        int start, end;
        string[] words;
        public int Start { get => start; set => start = value; }
        public int End { get => end; set => end = value; }

        public functionBlock(string text, int start, int end) : base(text, "rectangle")
        {
            this.start = start;
            this.end = end;
        }

        public void drawSchematic()
        {

        }

    }
}
