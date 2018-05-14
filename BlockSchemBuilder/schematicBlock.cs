using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockSchemBuilder
{
    class schematicBlock
    {
        string content;
        int x, y;
        blockTypes type;

        public string Content { get => content; set => content = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public blockTypes Type { get => type;}

        public schematicBlock(string text, blockTypes type)
        {
            content = text;
            this.type = type;
        }
    }
}
