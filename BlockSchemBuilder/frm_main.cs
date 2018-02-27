using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BlockSchemBuilder
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string code = txtBox_code.Text;
            CodePreprocessor.removeComments(ref code);
            string[] words = CodePreprocessor.splitToWords(code);
            CodeAnalyzer ca = new CodeAnalyzer(ref words);
            ca.getFunctions();
            ca.drawFunctions(flp_metodsList);
        }

        private void открытьcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog.FileName);
                txtBox_code.Text = reader.ReadToEnd();
                reader.Close();
            }
        }
    }
}
