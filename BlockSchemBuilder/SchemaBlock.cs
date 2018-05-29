using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlockSchemBuilder.Enum;


namespace BlockSchemBuilder
{
    public class SchemaBlock
    {
		private BlockTypes _type;

		public List<SchemaBlock> links;

        public string Content { get; set; }
		public Point fieldCoord { get; set; }
		public BlockTypes Type { get => _type; }
		public bool isPlaced { get; set; }
		public bool isCycleCondition { get; set; }
		public bool dontMove { get; set; }

		public SchemaBlock(string text, BlockTypes type)
        {
            Content = text;
            _type = type;
			links = new List<SchemaBlock>();
			isCycleCondition = false;
			dontMove = false;
			fieldCoord = new Point(0,0);
        }

		public override string ToString()
		{
			return Content;
		}
	}
}
