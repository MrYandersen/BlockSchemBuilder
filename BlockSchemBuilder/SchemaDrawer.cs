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

		private int _height = 100;
		private int _width = 50;

		private SchemaBlock _start;

		private SchemaBlock[,] Matrix;

		public SchemaDrawer(SchemaBlock start)
		{
			_start = start;
			Matrix = new SchemaBlock[_height, _width];
			PlaceBlocks();
		}

		private void PlaceBlocks()
		{
			int currentX = 0, currentY = 0;
			Stack<SchemaBlock> stack = new Stack<SchemaBlock>();
			stack.Push(_start);
			while (stack.Count != 0)
			{
				SchemaBlock current = stack.Pop();
				if (!current.isPlaced)
				{
					if (currentX < current.fieldCoord.X)
					{
						currentX = current.fieldCoord.X;
						currentY = current.fieldCoord.Y;
						foreach (SchemaBlock item in stack)
						{
							item.fieldCoord += new Size(1, 0);
						}
					}

					if (current.Type != BlockTypes.Condition)
					{
						current.fieldCoord = new Point(currentX, currentY++);
						Matrix[current.fieldCoord.Y, current.fieldCoord.X] = current;
						current.isPlaced = true;
						if (current.links.Count > 0)
						{
							if (current.links[0].isPlaced && current.links[0].fieldCoord.Y <= current.fieldCoord.Y)
							{
								int shift = current.fieldCoord.Y - current.links[0].fieldCoord.Y + 1;
								for (int i = 0; i < current.fieldCoord.X; i++)
								{
									for (int j = _height - 1; j >= current.fieldCoord.Y + shift; j--)
									{
										Matrix[j, i] = Matrix[j - shift, i];
										if (Matrix[j, i] != null)
											Matrix[j, i].fieldCoord += new Size(0, shift);
									}
									Matrix[current.links[0].fieldCoord.Y - shift, i] = null;
								}
							}
							if (!stack.Contains(current.links[0]))
								stack.Push(current.links[0]);
						}
					}
					else
					{
						current.fieldCoord = new Point(currentX, currentY++);
						Matrix[current.fieldCoord.Y, current.fieldCoord.X] = current;
						current.isPlaced = true;
						for (int i = current.links.Count - 1; i >= 0; i--)
						{
							if (!stack.Contains(current.links[i]))
							{
								stack.Push(current.links[i]);
								current.links[i].fieldCoord += new Size(i + currentX, currentY);
							}	
						}
					}
				}
			}
		}

		public void Draw(Control canvas)
		{
			using (Graphics g = canvas.CreateGraphics())
			using (Pen p = new Pen(Color.Black, 3))
			using (Font f = new Font("Consolas", 16, FontStyle.Bold))
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;
				foreach (SchemaBlock item in Matrix)
				{
					if (item != null)
					{
						switch (item.Type)
						{
							case BlockTypes.Operator:
								{
									Rectangle rec = new Rectangle(_fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * item.fieldCoord.Y + _shift, _fieldWidth - 2 * _shift, _fieldHeight - 2 * _shift);
									g.DrawRectangle(p, rec);
									g.DrawString(item.Content, f, Brushes.Black, rec, stringFormat);
									break;
								}
							case BlockTypes.Start:
								{
									Rectangle rec = new Rectangle(_fieldWidth * item.fieldCoord.X + _shift + _IOshift, _fieldHeight * item.fieldCoord.Y + _shift + _IOshift, _fieldWidth - 2 * (_shift + _IOshift), _fieldHeight - 2 * (_shift + _IOshift));
									g.DrawEllipse(p, _fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * item.fieldCoord.Y + _shift, 260, 60);
									g.DrawString(item.Content, f, Brushes.Black, rec, stringFormat);
									break;
								}
							case BlockTypes.IO:
								{
									Rectangle rec = new Rectangle(_fieldWidth * item.fieldCoord.X + _shift + _IOshift, _fieldHeight * item.fieldCoord.Y + _shift, _fieldWidth - 2 * (_shift + _IOshift), _fieldHeight - 2 * _shift);
									g.DrawPolygon(p, new Point[] {  new Point(_fieldWidth * item.fieldCoord.X + _shift + _IOshift, _fieldHeight * item.fieldCoord.Y + _shift),
																new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift, _fieldHeight * item.fieldCoord.Y + _shift),
																new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift - _IOshift, _fieldHeight * (item.fieldCoord.Y + 1) - _shift),
																new Point(_fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * (item.fieldCoord.Y + 1) - _shift) });
									g.DrawString(item.Content, f, Brushes.Black, rec, stringFormat);
									break;
								}
							case BlockTypes.Condition:
								{
									Rectangle rec = new Rectangle(_fieldWidth * item.fieldCoord.X + _shift + _IOshift, _fieldHeight * item.fieldCoord.Y + _IOshift + _shift, _fieldWidth - 2 * (_shift + _IOshift), _fieldHeight - 2 * (_shift + _IOshift));
									g.DrawPolygon(p, new Point[] {  new Point(_fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * item.fieldCoord.Y + _fieldHeight / 2),
															new Point(_fieldWidth * item.fieldCoord.X + _fieldWidth / 2, _fieldHeight * item.fieldCoord.Y + _shift),
															new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift, _fieldHeight * item.fieldCoord.Y + _fieldHeight / 2),
															new Point(_fieldWidth * item.fieldCoord.X + _fieldWidth / 2, _fieldHeight * (item.fieldCoord.Y + 1) - _shift) });
									g.DrawString(item.Content, f, Brushes.Black, rec, stringFormat);
									break;
								}
							case BlockTypes.Procedure:
								{
									Rectangle rec = new Rectangle(_fieldWidth * item.fieldCoord.X + _shift + (_IOshift / 2), _fieldHeight * item.fieldCoord.Y + _shift, _fieldWidth - 2 * (_shift + (_IOshift / 2)), _fieldHeight - 2 * _shift);
									g.DrawRectangle(p, new Rectangle(_fieldWidth * item.fieldCoord.X + _shift, _fieldHeight * item.fieldCoord.Y + _shift, _fieldWidth - 2 * _shift, _fieldHeight - 2 * _shift));
									g.DrawLine(p, new Point(_fieldWidth * item.fieldCoord.X + _shift + (_IOshift / 2), _fieldHeight * item.fieldCoord.Y + _shift), new Point(_fieldWidth * item.fieldCoord.X + _shift + (_IOshift / 2), _fieldHeight * (item.fieldCoord.Y + 1) - _shift));
									g.DrawLine(p, new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift - (_IOshift / 2), _fieldHeight * item.fieldCoord.Y + _shift), new Point(_fieldWidth * (item.fieldCoord.X + 1) - _shift - (_IOshift / 2), _fieldHeight * (item.fieldCoord.Y + 1) - _shift));
									g.DrawString(item.Content, f, Brushes.Black, rec, stringFormat);
									break;
								}
							default:
								break;
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
