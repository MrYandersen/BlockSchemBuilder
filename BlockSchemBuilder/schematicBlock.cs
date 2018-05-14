using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlockSchemBuilder.Enum;


namespace BlockSchemBuilder
{
    class schematicBlock
    {
		private BlockTypes _type;

		public List<schematicBlock> links;

        public string Content { get; set; }
        public BlockTypes Type { get => _type; }

        public schematicBlock(string text, BlockTypes type)
        {
            Content = text;
            _type = type;
			links = new List<schematicBlock>();
        }
    }
}
