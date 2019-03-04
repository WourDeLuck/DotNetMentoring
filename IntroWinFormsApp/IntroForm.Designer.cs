namespace IntroWinFormsApp
{
	partial class IntroForm
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
			this.NameTextBox = new System.Windows.Forms.TextBox();
			this.EnterName = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// NameTextBox
			// 
			this.NameTextBox.Location = new System.Drawing.Point(41, 91);
			this.NameTextBox.Name = "NameTextBox";
			this.NameTextBox.Size = new System.Drawing.Size(177, 20);
			this.NameTextBox.TabIndex = 0;
			this.NameTextBox.GotFocus += new System.EventHandler(this.NameTextBox_GotFocused);
			// 
			// EnterName
			// 
			this.EnterName.Location = new System.Drawing.Point(41, 135);
			this.EnterName.Name = "EnterName";
			this.EnterName.Size = new System.Drawing.Size(75, 23);
			this.EnterName.TabIndex = 1;
			this.EnterName.Text = "Enter";
			this.EnterName.UseVisualStyleBackColor = true;
			this.EnterName.Click += new System.EventHandler(this.EnterName_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(38, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "My name is:";
			// 
			// IntroForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(252, 219);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.EnterName);
			this.Controls.Add(this.NameTextBox);
			this.Name = "IntroForm";
			this.Text = "IntroForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox NameTextBox;
		private System.Windows.Forms.Button EnterName;
		private System.Windows.Forms.Label label1;
	}
}

