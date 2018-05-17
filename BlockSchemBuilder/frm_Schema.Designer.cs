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
			this.label1 = new System.Windows.Forms.Label();
			this.canvas = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			// 
			// canvas
			// 
			this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
			this.ClientSize = new System.Drawing.Size(784, 761);
			this.Controls.Add(this.canvas);
			this.Controls.Add(this.label1);
			this.Name = "frm_Schema";
			this.Text = "Shema";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel canvas;
	}
}