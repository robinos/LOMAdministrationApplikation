namespace LOMAdministrationApplikation.Views
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
			this.lblFråga = new System.Windows.Forms.Label();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.panelExit = new System.Windows.Forms.Panel();
			this.btnAvsluta = new System.Windows.Forms.Button();
			this.panelAnvändare.SuspendLayout();
			this.panelProdukt.SuspendLayout();
			this.flowLayoutPanel.SuspendLayout();
			this.panelExit.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblInloggning
			// 
			this.lblInloggning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInloggning.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInloggning.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblInloggning.Location = new System.Drawing.Point(0, 0);
			this.lblInloggning.Name = "lblInloggning";
			this.lblInloggning.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.lblInloggning.Size = new System.Drawing.Size(584, 92);
			this.lblInloggning.TabIndex = 0;
			this.lblInloggning.Text = "Du är inte inloggad som administratör. Kör om programmet som administratör.";
			this.lblInloggning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// panelAnvändare
			// 
			this.panelAnvändare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panelAnvändare.AutoScroll = true;
			this.panelAnvändare.Controls.Add(this.btnAnvändare);
			this.panelAnvändare.Location = new System.Drawing.Point(290, 3);
			this.panelAnvändare.Name = "panelAnvändare";
			this.panelAnvändare.Size = new System.Drawing.Size(278, 256);
			this.panelAnvändare.TabIndex = 2;
			// 
			// btnAnvändare
			// 
			this.btnAnvändare.BackColor = System.Drawing.Color.LightBlue;
			this.btnAnvändare.Enabled = false;
			this.btnAnvändare.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.btnAnvändare.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.btnAnvändare.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.btnAnvändare.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAnvändare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAnvändare.Location = new System.Drawing.Point(61, 72);
			this.btnAnvändare.Name = "btnAnvändare";
			this.btnAnvändare.Size = new System.Drawing.Size(145, 100);
			this.btnAnvändare.TabIndex = 0;
			this.btnAnvändare.Text = "Redigera Användare";
			this.btnAnvändare.UseVisualStyleBackColor = false;
			this.btnAnvändare.Click += new System.EventHandler(this.btnAnvändare_Click);
			// 
			// panelProdukt
			// 
			this.panelProdukt.AutoScroll = true;
			this.panelProdukt.Controls.Add(this.btnProdukter);
			this.panelProdukt.Location = new System.Drawing.Point(3, 3);
			this.panelProdukt.Name = "panelProdukt";
			this.panelProdukt.Size = new System.Drawing.Size(281, 256);
			this.panelProdukt.TabIndex = 3;
			// 
			// btnProdukter
			// 
			this.btnProdukter.BackColor = System.Drawing.Color.DarkSalmon;
			this.btnProdukter.Enabled = false;
			this.btnProdukter.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.btnProdukter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.btnProdukter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LemonChiffon;
			this.btnProdukter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnProdukter.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnProdukter.Location = new System.Drawing.Point(61, 72);
			this.btnProdukter.Name = "btnProdukter";
			this.btnProdukter.Size = new System.Drawing.Size(145, 100);
			this.btnProdukter.TabIndex = 1;
			this.btnProdukter.Text = "Redigera Produkter";
			this.btnProdukter.UseVisualStyleBackColor = false;
			this.btnProdukter.Click += new System.EventHandler(this.btnProdukter_Click);
			// 
			// lblFråga
			// 
			this.lblFråga.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblFråga.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFråga.Location = new System.Drawing.Point(0, 92);
			this.lblFråga.Name = "lblFråga";
			this.lblFråga.Size = new System.Drawing.Size(584, 55);
			this.lblFråga.TabIndex = 0;
			this.lblFråga.Text = "Vill du:";
			this.lblFråga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.flowLayoutPanel.AutoSize = true;
			this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel.Controls.Add(this.panelProdukt);
			this.flowLayoutPanel.Controls.Add(this.panelAnvändare);
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 149);
			this.flowLayoutPanel.MinimumSize = new System.Drawing.Size(575, 250);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(575, 262);
			this.flowLayoutPanel.TabIndex = 5;
			// 
			// panelExit
			// 
			this.panelExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panelExit.AutoSize = true;
			this.panelExit.Controls.Add(this.btnAvsluta);
			this.panelExit.Location = new System.Drawing.Point(0, 414);
			this.panelExit.MinimumSize = new System.Drawing.Size(584, 46);
			this.panelExit.Name = "panelExit";
			this.panelExit.Size = new System.Drawing.Size(584, 46);
			this.panelExit.TabIndex = 6;
			// 
			// btnAvsluta
			// 
			this.btnAvsluta.BackColor = System.Drawing.Color.IndianRed;
			this.btnAvsluta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
			this.btnAvsluta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCoral;
			this.btnAvsluta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAvsluta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAvsluta.Location = new System.Drawing.Point(221, 8);
			this.btnAvsluta.Name = "btnAvsluta";
			this.btnAvsluta.Size = new System.Drawing.Size(130, 28);
			this.btnAvsluta.TabIndex = 0;
			this.btnAvsluta.Text = "Avsluta";
			this.btnAvsluta.UseVisualStyleBackColor = false;
			this.btnAvsluta.Click += new System.EventHandler(this.btnAvsluta_Click);
			// 
			// HuvudApplikationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 462);
			this.Controls.Add(this.panelExit);
			this.Controls.Add(this.lblFråga);
			this.Controls.Add(this.lblInloggning);
			this.Controls.Add(this.flowLayoutPanel);
			this.MinimumSize = new System.Drawing.Size(600, 500);
			this.Name = "HuvudApplikationForm";
			this.Text = "Ljus och Miljö AB - Huvudmeny";
			this.panelAnvändare.ResumeLayout(false);
			this.panelProdukt.ResumeLayout(false);
			this.flowLayoutPanel.ResumeLayout(false);
			this.panelExit.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblInloggning;
		private System.Windows.Forms.Panel panelAnvändare;
		private System.Windows.Forms.Button btnAnvändare;
		private System.Windows.Forms.Panel panelProdukt;
		private System.Windows.Forms.Button btnProdukter;
		private System.Windows.Forms.Label lblFråga;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.Panel panelExit;
		private System.Windows.Forms.Button btnAvsluta;
	}
}