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
			drawer = new SchemaDrawer(start);
			drawer.Draw(panel1);
		}

		private void panel1_Resize(object sender, EventArgs e)
		{
			drawer.Draw(panel1);
			panel1.Invalidate();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			drawer.Draw(panel1);
		}
	}
}
