namespace LOMAdministrationApplikation
{
	partial class HuvudApplikationForm
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
			this.lblInloggning = new System.Windows.Forms.Label();
			this.panelAnvändare = new System.Windows.Forms.Panel();
			this.btnAnvändare = new System.Windows.Forms.Button();
			this.panelProdukt = new System.Windows.Forms.Panel();
			this.btnProdukter = new System.Windows.Forms.Button();
			this.panelFråga = new System.Windows.Forms.Panel();
			this.lblFråga = new System.Windows.Forms.Label();
			this.panelAnvändare.SuspendLayout();
			this.panelProdukt.SuspendLayout();
			this.panelFråga.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblInloggning
			// 
			this.lblInloggning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInloggning.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInloggning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInloggning.Location = new System.Drawing.Point(0, 0);
			this.lblInloggning.Name = "lblInloggning";
			this.lblInloggning.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.lblInloggning.Size = new System.Drawing.Size(784, 50);
			this.lblInloggning.TabIndex = 0;
			this.lblInloggning.Text = "Du är inte inloggad som administratör.  Snälla ändra konto och testa igen.";
			this.lblInloggning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// panelAnvändare
			// 
			this.panelAnvändare.Controls.Add(this.btnAnvändare);
			this.panelAnvändare.Location = new System.Drawing.Point(45, 134);
			this.panelAnvändare.Name = "panelAnvändare";
			this.panelAnvändare.Size = new System.Drawing.Size(266, 214);
			this.panelAnvändare.TabIndex = 2;
			// 
			// btnAnvändare
			// 
			this.btnAnvändare.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.btnAnvändare.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnAnvändare.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.btnAnvändare.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.btnAnvändare.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAnvändare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAnvändare.Location = new System.Drawing.Point(62, 53);
			this.btnAnvändare.Name = "btnAnvändare";
			this.btnAnvändare.Size = new System.Drawing.Size(145, 100);
			this.btnAnvändare.TabIndex = 0;
			this.btnAnvändare.Text = "Redigera Användare";
			this.btnAnvändare.UseVisualStyleBackColor = false;
			this.btnAnvändare.Click += new System.EventHandler(this.btnAnvändare_Click);
			// 
			// panelProdukt
			// 
			this.panelProdukt.Controls.Add(this.btnProdukter);
			this.panelProdukt.Location = new System.Drawing.Point(463, 134);
			this.panelProdukt.Name = "panelProdukt";
			this.panelProdukt.Size = new System.Drawing.Size(266, 214);
			this.panelProdukt.TabIndex = 3;
			// 
			// btnProdukter
			// 
			this.btnProdukter.BackColor = System.Drawing.Color.DarkSalmon;
			this.btnProdukter.FlatAppearance.BorderColor = System.Drawing.Color.Black;
			this.btnProdukter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.btnProdukter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LemonChiffon;
			this.btnProdukter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnProdukter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProdukter.Location = new System.Drawing.Point(61, 57);
			this.btnProdukter.Name = "btnProdukter";
			this.btnProdukter.Size = new System.Drawing.Size(145, 100);
			this.btnProdukter.TabIndex = 1;
			this.btnProdukter.Text = "Redigera Produkter";
			this.btnProdukter.UseVisualStyleBackColor = false;
			this.btnProdukter.Click += new System.EventHandler(this.btnProdukter_Click);
			// 
			// panelFråga
			// 
			this.panelFråga.Controls.Add(this.lblFråga);
			this.panelFråga.Location = new System.Drawing.Point(326, 68);
			this.panelFråga.Name = "panelFråga";
			this.panelFråga.Size = new System.Drawing.Size(129, 44);
			this.panelFråga.TabIndex = 4;
			// 
			// lblFråga
			// 
			this.lblFråga.AutoSize = true;
			this.lblFråga.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFråga.Location = new System.Drawing.Point(38, 12);
			this.lblFråga.Name = "lblFråga";
			this.lblFråga.Size = new System.Drawing.Size(50, 17);
			this.lblFråga.TabIndex = 0;
			this.lblFråga.Text = "Vill du:";
			// 
			// HuvudApplikationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.panelFråga);
			this.Controls.Add(this.panelProdukt);
			this.Controls.Add(this.panelAnvändare);
			this.Controls.Add(this.lblInloggning);
			this.Name = "HuvudApplikationForm";
			this.Text = "Ljus och Miljö AB - Datahantering";
			this.panelAnvändare.ResumeLayout(false);
			this.panelProdukt.ResumeLayout(false);
			this.panelFråga.ResumeLayout(false);
			this.panelFråga.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblInloggning;
		private System.Windows.Forms.Panel panelAnvändare;
		private System.Windows.Forms.Button btnAnvändare;
		private System.Windows.Forms.Panel panelProdukt;
		private System.Windows.Forms.Button btnProdukter;
		private System.Windows.Forms.Panel panelFråga;
		private System.Windows.Forms.Label lblFråga;
	}
}