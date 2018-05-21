namespace BlockSchemBuilder
{
	partial class frm_Schema
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.canvas = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.AutoScroll = true;
			this.canvas.AutoSize = true;
			this.canvas.Location = new System.Drawing.Point(0, 0);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(2983, 2000);
			this.canvas.TabIndex = 2;
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			this.canvas.Resize += new System.EventHandler(this.panel1_Resize);
			// 
			// frm_Schema
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(784, 761);
			this.Controls.Add(this.canvas);
			this.Name = "frm_Schema";
			this.Text = "Shema";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Panel canvas;
	}
}