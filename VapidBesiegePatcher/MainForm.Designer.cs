namespace Vapid.Patcher
{
	partial class MainForm
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
			this.textGamePath = new System.Windows.Forms.TextBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.labelGamePath = new System.Windows.Forms.Label();
			this.buttonInject = new System.Windows.Forms.Button();
			this.textLog = new System.Windows.Forms.RichTextBox();
			this.labelLog = new System.Windows.Forms.Label();
			this.buttonResetBackup = new System.Windows.Forms.Button();
			this.buttonRestore = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textGamePath
			// 
			this.textGamePath.Location = new System.Drawing.Point(15, 25);
			this.textGamePath.Name = "textGamePath";
			this.textGamePath.Size = new System.Drawing.Size(402, 20);
			this.textGamePath.TabIndex = 0;
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.Location = new System.Drawing.Point(423, 24);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(75, 22);
			this.buttonBrowse.TabIndex = 1;
			this.buttonBrowse.Text = "Browse";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
			// 
			// labelGamePath
			// 
			this.labelGamePath.AutoSize = true;
			this.labelGamePath.Location = new System.Drawing.Point(12, 9);
			this.labelGamePath.Name = "labelGamePath";
			this.labelGamePath.Size = new System.Drawing.Size(102, 13);
			this.labelGamePath.TabIndex = 2;
			this.labelGamePath.Text = "Path to Besiege.exe";
			// 
			// buttonInject
			// 
			this.buttonInject.Location = new System.Drawing.Point(14, 51);
			this.buttonInject.Name = "buttonInject";
			this.buttonInject.Size = new System.Drawing.Size(352, 53);
			this.buttonInject.TabIndex = 4;
			this.buttonInject.Text = "Inject Vapid\'s ModLoader";
			this.buttonInject.UseVisualStyleBackColor = true;
			this.buttonInject.Click += new System.EventHandler(this.buttonInject_Click);
			// 
			// textLog
			// 
			this.textLog.Location = new System.Drawing.Point(15, 133);
			this.textLog.Name = "textLog";
			this.textLog.Size = new System.Drawing.Size(483, 160);
			this.textLog.TabIndex = 5;
			this.textLog.Text = "";
			// 
			// labelLog
			// 
			this.labelLog.AutoSize = true;
			this.labelLog.Location = new System.Drawing.Point(12, 117);
			this.labelLog.Name = "labelLog";
			this.labelLog.Size = new System.Drawing.Size(25, 13);
			this.labelLog.TabIndex = 6;
			this.labelLog.Text = "Log";
			// 
			// buttonResetBackup
			// 
			this.buttonResetBackup.Location = new System.Drawing.Point(372, 79);
			this.buttonResetBackup.Name = "buttonResetBackup";
			this.buttonResetBackup.Size = new System.Drawing.Size(126, 25);
			this.buttonResetBackup.TabIndex = 7;
			this.buttonResetBackup.Text = "Delete Backup";
			this.buttonResetBackup.UseVisualStyleBackColor = true;
			this.buttonResetBackup.Click += new System.EventHandler(this.buttonResetBackup_Click);
			// 
			// buttonRestore
			// 
			this.buttonRestore.Location = new System.Drawing.Point(372, 51);
			this.buttonRestore.Name = "buttonRestore";
			this.buttonRestore.Size = new System.Drawing.Size(126, 25);
			this.buttonRestore.TabIndex = 8;
			this.buttonRestore.Text = "Restore Original";
			this.buttonRestore.UseVisualStyleBackColor = true;
			this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(510, 305);
			this.Controls.Add(this.buttonRestore);
			this.Controls.Add(this.buttonResetBackup);
			this.Controls.Add(this.labelLog);
			this.Controls.Add(this.textLog);
			this.Controls.Add(this.buttonInject);
			this.Controls.Add(this.labelGamePath);
			this.Controls.Add(this.buttonBrowse);
			this.Controls.Add(this.textGamePath);
			this.Name = "MainForm";
			this.Text = "Vapid\'s ModLoader Injector";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textGamePath;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.Label labelGamePath;
		private System.Windows.Forms.Button buttonInject;
		private System.Windows.Forms.Label labelLog;
		internal System.Windows.Forms.RichTextBox textLog;
		private System.Windows.Forms.Button buttonResetBackup;
		private System.Windows.Forms.Button buttonRestore;
	}
}

