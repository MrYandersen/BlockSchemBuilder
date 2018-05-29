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

        private void GetFunctionsButtonClickHandler(object sender, EventArgs e)
        {
            string code = txtBox_code.Text;
            CodePreprocessor.removeComments(ref code);
            string[] words = CodePreprocessor.splitToWords(code);
            CodeAnalyzer ca = new CodeAnalyzer(ref words);
            ca.getFunctions();
            ca.drawFunctions(flp_metodsList);
        }

        private void OpenMenuClickHandler(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog.FileName);
                txtBox_code.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void HelpMenuClickHandler(object sender, EventArgs e)
        {
            Help form = new Help();
            form.Show();
        }

		private void txtBox_code_TextChanged(object sender, EventArgs e)
		{
			flp_metodsList.Controls.Clear();
		}
	}
}
