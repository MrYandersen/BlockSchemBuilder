using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlockSchemBuilder.Enum;


namespace BlockSchemBuilder
{
    class SchemaBlock
    {
		private BlockTypes _type;

		public List<SchemaBlock> links;

        public string Content { get; set; }
        public BlockTypes Type { get => _type; }

        public SchemaBlock(string text, BlockTypes type)
        {
            Content = text;
            _type = type;
			links = new List<SchemaBlock>();
        }

		public override string ToString()
		{
			return Content;
		}
	}
}
