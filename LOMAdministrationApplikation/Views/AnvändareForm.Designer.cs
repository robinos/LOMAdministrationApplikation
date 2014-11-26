namespace LOMAdministrationApplikation.Views
{
	partial class AnvändareForm
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
			this.panelFunktion = new System.Windows.Forms.Panel();
			this.btnFörsta = new System.Windows.Forms.Button();
			this.btnSista = new System.Windows.Forms.Button();
			this.btnNästa = new System.Windows.Forms.Button();
			this.btnTillbaka = new System.Windows.Forms.Button();
			this.btnSpara = new System.Windows.Forms.Button();
			this.cboxAnvändareBox = new System.Windows.Forms.ComboBox();
			this.lblIndex = new System.Windows.Forms.Label();
			this.btnTaBort = new System.Windows.Forms.Button();
			this.btnNy = new System.Windows.Forms.Button();
			this.panelInmatning = new System.Windows.Forms.Panel();
			this.rbtnLåste = new System.Windows.Forms.RadioButton();
			this.rbtnOlåste = new System.Windows.Forms.RadioButton();
			this.txtRäknare = new System.Windows.Forms.RichTextBox();
			this.txtAnvändarnamn = new System.Windows.Forms.RichTextBox();
			this.lblAnvändarnamn = new System.Windows.Forms.Label();
			this.lblLåste = new System.Windows.Forms.Label();
			this.lblRäknare = new System.Windows.Forms.Label();
			this.txtRoll = new System.Windows.Forms.RichTextBox();
			this.lblRoll = new System.Windows.Forms.Label();
			this.txtLösenord = new System.Windows.Forms.RichTextBox();
			this.lblHash = new System.Windows.Forms.Label();
			this.lblLösenord = new System.Windows.Forms.Label();
			this.lblID = new System.Windows.Forms.Label();
			this.checkNyLösenord = new System.Windows.Forms.CheckBox();
			this.panelFunktion.SuspendLayout();
			this.panelInmatning.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelFunktion
			// 
			this.panelFunktion.AccessibleDescription = "Funktionpanel";
			this.panelFunktion.AccessibleName = "Funktionpanel";
			this.panelFunktion.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
			this.panelFunktion.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panelFunktion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelFunktion.Controls.Add(this.btnFörsta);
			this.panelFunktion.Controls.Add(this.btnSista);
			this.panelFunktion.Controls.Add(this.btnNästa);
			this.panelFunktion.Controls.Add(this.btnTillbaka);
			this.panelFunktion.Controls.Add(this.btnSpara);
			this.panelFunktion.Controls.Add(this.cboxAnvändareBox);
			this.panelFunktion.Controls.Add(this.lblIndex);
			this.panelFunktion.Controls.Add(this.btnTaBort);
			this.panelFunktion.Controls.Add(this.btnNy);
			this.panelFunktion.Location = new System.Drawing.Point(37, 30);
			this.panelFunktion.Name = "panelFunktion";
			this.panelFunktion.Size = new System.Drawing.Size(673, 27);
			this.panelFunktion.TabIndex = 6;
			// 
			// btnFörsta
			// 
			this.btnFörsta.Location = new System.Drawing.Point(248, 1);
			this.btnFörsta.Name = "btnFörsta";
			this.btnFörsta.Size = new System.Drawing.Size(31, 23);
			this.btnFörsta.TabIndex = 15;
			this.btnFörsta.Text = "|<";
			this.btnFörsta.UseVisualStyleBackColor = true;
			this.btnFörsta.Click += new System.EventHandler(this.btnFörsta_Click);
			// 
			// btnSista
			// 
			this.btnSista.Location = new System.Drawing.Point(285, 1);
			this.btnSista.Name = "btnSista";
			this.btnSista.Size = new System.Drawing.Size(31, 23);
			this.btnSista.TabIndex = 14;
			this.btnSista.Text = ">|";
			this.btnSista.UseVisualStyleBackColor = true;
			this.btnSista.Click += new System.EventHandler(this.btnSista_Click);
			// 
			// btnNästa
			// 
			this.btnNästa.Enabled = false;
			this.btnNästa.Location = new System.Drawing.Point(578, 1);
			this.btnNästa.Name = "btnNästa";
			this.btnNästa.Size = new System.Drawing.Size(85, 23);
			this.btnNästa.TabIndex = 13;
			this.btnNästa.Text = "Nästa 5 >>";
			this.btnNästa.UseVisualStyleBackColor = true;
			this.btnNästa.Click += new System.EventHandler(this.btnNästa_Click);
			// 
			// btnTillbaka
			// 
			this.btnTillbaka.Enabled = false;
			this.btnTillbaka.Location = new System.Drawing.Point(487, 1);
			this.btnTillbaka.Name = "btnTillbaka";
			this.btnTillbaka.Size = new System.Drawing.Size(85, 23);
			this.btnTillbaka.TabIndex = 12;
			this.btnTillbaka.Text = "<< Tillbaka 5";
			this.btnTillbaka.UseVisualStyleBackColor = true;
			this.btnTillbaka.Click += new System.EventHandler(this.btnTillbaka_Click);
			// 
			// btnSpara
			// 
			this.btnSpara.AccessibleDescription = "Spara knappen";
			this.btnSpara.AccessibleName = "Spara";
			this.btnSpara.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSpara.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSpara.Location = new System.Drawing.Point(174, -1);
			this.btnSpara.Name = "btnSpara";
			this.btnSpara.Size = new System.Drawing.Size(50, 25);
			this.btnSpara.TabIndex = 7;
			this.btnSpara.Text = "Spara";
			this.btnSpara.UseVisualStyleBackColor = true;
			this.btnSpara.Click += new System.EventHandler(this.btnSpara_Click);
			// 
			// cboxAnvändareBox
			// 
			this.cboxAnvändareBox.AccessibleDescription = "Användare Combobox";
			this.cboxAnvändareBox.AccessibleName = "Användarlista";
			this.cboxAnvändareBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
			this.cboxAnvändareBox.FormattingEnabled = true;
			this.cboxAnvändareBox.Location = new System.Drawing.Point(322, 2);
			this.cboxAnvändareBox.Name = "cboxAnvändareBox";
			this.cboxAnvändareBox.Size = new System.Drawing.Size(159, 21);
			this.cboxAnvändareBox.TabIndex = 6;
			this.cboxAnvändareBox.SelectedIndexChanged += new System.EventHandler(this.cboxAnvändareBox_SelectedIndexChanged);
			// 
			// lblIndex
			// 
			this.lblIndex.AccessibleDescription = "ID Index";
			this.lblIndex.AccessibleName = "Index";
			this.lblIndex.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.lblIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIndex.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblIndex.Location = new System.Drawing.Point(3, 0);
			this.lblIndex.Name = "lblIndex";
			this.lblIndex.Size = new System.Drawing.Size(50, 23);
			this.lblIndex.TabIndex = 4;
			this.lblIndex.Text = "0";
			this.lblIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnTaBort
			// 
			this.btnTaBort.AccessibleDescription = "Ta bort knappen";
			this.btnTaBort.AccessibleName = "Ta bort";
			this.btnTaBort.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTaBort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnTaBort.Location = new System.Drawing.Point(118, -1);
			this.btnTaBort.Name = "btnTaBort";
			this.btnTaBort.Size = new System.Drawing.Size(50, 25);
			this.btnTaBort.TabIndex = 3;
			this.btnTaBort.Text = "Ta bort";
			this.btnTaBort.UseVisualStyleBackColor = true;
			this.btnTaBort.Click += new System.EventHandler(this.btnTaBort_Click);
			// 
			// btnNy
			// 
			this.btnNy.AccessibleDescription = "Ny knapp";
			this.btnNy.AccessibleName = "Ny";
			this.btnNy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnNy.Location = new System.Drawing.Point(62, -1);
			this.btnNy.Name = "btnNy";
			this.btnNy.Size = new System.Drawing.Size(50, 25);
			this.btnNy.TabIndex = 0;
			this.btnNy.Text = "Ny";
			this.btnNy.UseVisualStyleBackColor = true;
			this.btnNy.Click += new System.EventHandler(this.btnNy_Click);
			// 
			// panelInmatning
			// 
			this.panelInmatning.AccessibleDescription = "Inmatningspanel";
			this.panelInmatning.AccessibleName = "Inmatning";
			this.panelInmatning.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
			this.panelInmatning.Controls.Add(this.checkNyLösenord);
			this.panelInmatning.Controls.Add(this.rbtnLåste);
			this.panelInmatning.Controls.Add(this.rbtnOlåste);
			this.panelInmatning.Controls.Add(this.txtRäknare);
			this.panelInmatning.Controls.Add(this.txtAnvändarnamn);
			this.panelInmatning.Controls.Add(this.lblAnvändarnamn);
			this.panelInmatning.Controls.Add(this.lblLåste);
			this.panelInmatning.Controls.Add(this.lblRäknare);
			this.panelInmatning.Controls.Add(this.txtRoll);
			this.panelInmatning.Controls.Add(this.lblRoll);
			this.panelInmatning.Controls.Add(this.txtLösenord);
			this.panelInmatning.Controls.Add(this.lblHash);
			this.panelInmatning.Controls.Add(this.lblLösenord);
			this.panelInmatning.Controls.Add(this.lblID);
			this.panelInmatning.Location = new System.Drawing.Point(109, 111);
			this.panelInmatning.Name = "panelInmatning";
			this.panelInmatning.Size = new System.Drawing.Size(502, 255);
			this.panelInmatning.TabIndex = 8;
			// 
			// rbtnLåste
			// 
			this.rbtnLåste.AutoSize = true;
			this.rbtnLåste.Location = new System.Drawing.Point(267, 187);
			this.rbtnLåste.Name = "rbtnLåste";
			this.rbtnLåste.Size = new System.Drawing.Size(51, 17);
			this.rbtnLåste.TabIndex = 33;
			this.rbtnLåste.TabStop = true;
			this.rbtnLåste.Text = "Låste";
			this.rbtnLåste.UseVisualStyleBackColor = true;
			// 
			// rbtnOlåste
			// 
			this.rbtnOlåste.AutoSize = true;
			this.rbtnOlåste.Location = new System.Drawing.Point(176, 188);
			this.rbtnOlåste.Name = "rbtnOlåste";
			this.rbtnOlåste.Size = new System.Drawing.Size(55, 17);
			this.rbtnOlåste.TabIndex = 32;
			this.rbtnOlåste.TabStop = true;
			this.rbtnOlåste.Text = "Olåste";
			this.rbtnOlåste.UseVisualStyleBackColor = true;
			// 
			// txtRäknare
			// 
			this.txtRäknare.AccessibleDescription = "Misslyckade inloggningar";
			this.txtRäknare.AccessibleName = "Räknare";
			this.txtRäknare.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtRäknare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtRäknare.Location = new System.Drawing.Point(164, 151);
			this.txtRäknare.MaxLength = 30;
			this.txtRäknare.Name = "txtRäknare";
			this.txtRäknare.Size = new System.Drawing.Size(194, 30);
			this.txtRäknare.TabIndex = 31;
			this.txtRäknare.Text = "";
			// 
			// txtAnvändarnamn
			// 
			this.txtAnvändarnamn.AccessibleDescription = "Användarnamn";
			this.txtAnvändarnamn.AccessibleName = "Användarnamn";
			this.txtAnvändarnamn.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtAnvändarnamn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtAnvändarnamn.Location = new System.Drawing.Point(164, 30);
			this.txtAnvändarnamn.MaxLength = 30;
			this.txtAnvändarnamn.Name = "txtAnvändarnamn";
			this.txtAnvändarnamn.Size = new System.Drawing.Size(194, 30);
			this.txtAnvändarnamn.TabIndex = 30;
			this.txtAnvändarnamn.Text = "";
			// 
			// lblAnvändarnamn
			// 
			this.lblAnvändarnamn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblAnvändarnamn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblAnvändarnamn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblAnvändarnamn.Location = new System.Drawing.Point(3, 30);
			this.lblAnvändarnamn.Name = "lblAnvändarnamn";
			this.lblAnvändarnamn.Size = new System.Drawing.Size(164, 30);
			this.lblAnvändarnamn.TabIndex = 29;
			this.lblAnvändarnamn.Text = "Användarnamn:";
			this.lblAnvändarnamn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLåste
			// 
			this.lblLåste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblLåste.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblLåste.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLåste.Location = new System.Drawing.Point(3, 180);
			this.lblLåste.Name = "lblLåste";
			this.lblLåste.Size = new System.Drawing.Size(164, 30);
			this.lblLåste.TabIndex = 25;
			this.lblLåste.Text = "Låste:";
			this.lblLåste.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRäknare
			// 
			this.lblRäknare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblRäknare.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblRäknare.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRäknare.Location = new System.Drawing.Point(3, 150);
			this.lblRäknare.Name = "lblRäknare";
			this.lblRäknare.Size = new System.Drawing.Size(164, 30);
			this.lblRäknare.TabIndex = 23;
			this.lblRäknare.Text = "Räknare:";
			this.lblRäknare.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRoll
			// 
			this.txtRoll.AccessibleDescription = "Användare Roll";
			this.txtRoll.AccessibleName = "Roll";
			this.txtRoll.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtRoll.Location = new System.Drawing.Point(164, 120);
			this.txtRoll.MaxLength = 30;
			this.txtRoll.Name = "txtRoll";
			this.txtRoll.Size = new System.Drawing.Size(194, 30);
			this.txtRoll.TabIndex = 22;
			this.txtRoll.Text = "";
			// 
			// lblRoll
			// 
			this.lblRoll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblRoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblRoll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblRoll.Location = new System.Drawing.Point(3, 120);
			this.lblRoll.Name = "lblRoll";
			this.lblRoll.Size = new System.Drawing.Size(164, 30);
			this.lblRoll.TabIndex = 21;
			this.lblRoll.Text = "Roll:";
			this.lblRoll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtLösenord
			// 
			this.txtLösenord.AccessibleDescription = "Lösenord";
			this.txtLösenord.AccessibleName = "Lösenord";
			this.txtLösenord.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtLösenord.BackColor = System.Drawing.Color.White;
			this.txtLösenord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtLösenord.Location = new System.Drawing.Point(164, 60);
			this.txtLösenord.MaxLength = 30;
			this.txtLösenord.Name = "txtLösenord";
			this.txtLösenord.Size = new System.Drawing.Size(194, 30);
			this.txtLösenord.TabIndex = 20;
			this.txtLösenord.Text = "";
			// 
			// lblHash
			// 
			this.lblHash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblHash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblHash.Location = new System.Drawing.Point(3, 90);
			this.lblHash.Name = "lblHash";
			this.lblHash.Size = new System.Drawing.Size(355, 30);
			this.lblHash.TabIndex = 19;
			this.lblHash.Text = "Konverteras till lösenord hash!";
			this.lblHash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLösenord
			// 
			this.lblLösenord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblLösenord.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblLösenord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblLösenord.Location = new System.Drawing.Point(3, 60);
			this.lblLösenord.Name = "lblLösenord";
			this.lblLösenord.Size = new System.Drawing.Size(164, 30);
			this.lblLösenord.TabIndex = 8;
			this.lblLösenord.Text = "Lösenord:";
			this.lblLösenord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblID
			// 
			this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblID.Location = new System.Drawing.Point(3, 0);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(164, 30);
			this.lblID.TabIndex = 6;
			this.lblID.Text = "ID:";
			this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkNyLösenord
			// 
			this.checkNyLösenord.AutoSize = true;
			this.checkNyLösenord.Enabled = false;
			this.checkNyLösenord.Location = new System.Drawing.Point(364, 68);
			this.checkNyLösenord.Name = "checkNyLösenord";
			this.checkNyLösenord.Size = new System.Drawing.Size(39, 17);
			this.checkNyLösenord.TabIndex = 34;
			this.checkNyLösenord.Text = "Ny";
			this.checkNyLösenord.UseVisualStyleBackColor = true;
			this.checkNyLösenord.CheckedChanged += new System.EventHandler(this.checkNyLösenord_CheckedChanged);
			// 
			// AnvändareForm
			// 
			this.AccessibleDescription = "Användare redigerare";
			this.AccessibleName = "Användare hanterare";
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.panelInmatning);
			this.Controls.Add(this.panelFunktion);
			this.Name = "AnvändareForm";
			this.Text = "Ljus och Miljö AB - Användare Hanterare";
			this.Load += new System.EventHandler(this.AnvändareForm_Load);
			this.panelFunktion.ResumeLayout(false);
			this.panelInmatning.ResumeLayout(false);
			this.panelInmatning.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelFunktion;
		private System.Windows.Forms.Button btnSpara;
		private System.Windows.Forms.ComboBox cboxAnvändareBox;
		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.Button btnTaBort;
		private System.Windows.Forms.Button btnNy;
		private System.Windows.Forms.Panel panelInmatning;
		private System.Windows.Forms.RichTextBox txtAnvändarnamn;
		private System.Windows.Forms.Label lblAnvändarnamn;
		private System.Windows.Forms.Label lblLåste;
		private System.Windows.Forms.Label lblRäknare;
		private System.Windows.Forms.RichTextBox txtRoll;
		private System.Windows.Forms.Label lblRoll;
		private System.Windows.Forms.RichTextBox txtLösenord;
		private System.Windows.Forms.Label lblHash;
		private System.Windows.Forms.Label lblLösenord;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.RichTextBox txtRäknare;
		private System.Windows.Forms.RadioButton rbtnLåste;
		private System.Windows.Forms.RadioButton rbtnOlåste;
		private System.Windows.Forms.Button btnFörsta;
		private System.Windows.Forms.Button btnSista;
		private System.Windows.Forms.Button btnNästa;
		private System.Windows.Forms.Button btnTillbaka;
		private System.Windows.Forms.CheckBox checkNyLösenord;

	}
}

