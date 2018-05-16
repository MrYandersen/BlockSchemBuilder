using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockSchemBuilder.Enum;

namespace BlockSchemBuilder
{
	class SchemaDrawer
	{
		private const int _fieldHeight = 100;
		private const int _fieldWidth = 300;
		private const int _shift = 20;
		private const int _IOshift = 30;

		private SchemaBlock _start;

		private SchemaBlock[,] Matrix;

		public SchemaDrawer(SchemaBlock start)
		{
			_start = start;
			Matrix = new SchemaBlock[100, 50];
			FillMatrix();
		}

		private void FillMatrix()
		{
			int currentX = 0, currentY = -1;
			Stack<SchemaBlock> stack = new Stack<SchemaBlock>();
			stack.Push(_start);
			while(stack.Count != 0)
			{
				SchemaBlock current = stack.Pop();
				if(current.Type != BlockTypes.Condition)
				{
					Matrix[currentX, ++currentY] = current;
					current.fieldCoord = new Point(currentX, currentY);
					if(current.links.Count > 0)
						stack.Push(current.links[0]);
				}
				else
				{

				}
			}
		}

		public void Draw(Control canvas)
		{
			using (Graphics g = canvas.CreateGraphics())
			using (Pen p = new Pen(Color.Black, 3))
			{
				foreach(SchemaBlock item in Matrix)
				{
					if(item != null)
					{
						if(item.Type == BlockTypes.Operator)
						{
							g.DrawRectangle(p, _fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * item.fieldCoord.Y + _shift, _fieldWidth - 2*_shift, _fieldHeight - 2 * _shift);
						}
						else if(item.Type == BlockTypes.Start)
						{
							g.DrawEllipse(p, _fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * item.fieldCoord.Y + _shift, 260, 60);
						}
						else if(item.Type == BlockTypes.IO)
						{
							g.DrawPolygon(p, new Point[] {	new Point(_fieldWidth * item.fieldCoord.X + _shift + _IOshift, _fieldHeight * item.fieldCoord.Y + _shift),
															new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift, _fieldHeight * item.fieldCoord.Y + _shift),
															new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift - _IOshift, _fieldHeight * (item.fieldCoord.Y + 1) - _shift),
															new Point(_fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * (item.fieldCoord.Y + 1) - _shift) });
						}
						else if(item.Type == BlockTypes.Condition)
						{
							g.DrawPolygon(p, new Point[] {  new Point(_fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * item.fieldCoord.Y + _fieldHeight / 2),
															new Point(_fieldWidth * item.fieldCoord.X + _fieldWidth / 2, _fieldHeight * item.fieldCoord.Y + _shift),
															new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift, _fieldHeight * item.fieldCoord.Y + _fieldHeight / 2),
															new Point(_fieldWidth * item.fieldCoord.X + _fieldWidth / 2, _fieldHeight * (item.fieldCoord.Y + 1) - _shift) });
						}
					}
				}
			}
		}

		private void PlaceText()
		{

		}
	}
}
