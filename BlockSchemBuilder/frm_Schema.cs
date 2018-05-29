using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockSchemBuilder
{
	public partial class frm_Schema : Form
	{
		SchemaDrawer drawer;

		public frm_Schema(SchemaBlock start)
		{
			InitializeComponent();
			drawer = new SchemaDrawer( start);
			drawer.DrawBlock(canvas);
			drawer.DrawArrows(canvas);
		}

		private void ResizeHandler(object sender, EventArgs e)
		{
			drawer.DrawBlock(canvas);
			drawer.DrawArrows(canvas);
			canvas.Invalidate();
		}

		private void PaintHandler(object sender, PaintEventArgs e)
		{
			drawer.DrawBlock(canvas);
			drawer.DrawArrows(canvas);
		}
	}
}
