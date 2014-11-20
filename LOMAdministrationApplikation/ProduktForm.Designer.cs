namespace LOMAdministrationApplikation
{
	partial class ProduktForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProduktForm));
			this.btnNy = new System.Windows.Forms.Button();
			this.btnTaBort = new System.Windows.Forms.Button();
			this.lblIndex = new System.Windows.Forms.Label();
			this.panelFunktion = new System.Windows.Forms.Panel();
			this.btnSpara = new System.Windows.Forms.Button();
			this.cboxProduktBox = new System.Windows.Forms.ComboBox();
			this.lblID = new System.Windows.Forms.Label();
			this.panelInmatning = new System.Windows.Forms.Panel();
			this.txtRefID = new JRINCCustomControls.currencyTextBox(this.components);
			this.txtID = new JRINCCustomControls.currencyTextBox(this.components);
			this.txtPris = new JRINCCustomControls.currencyTextBox(this.components);
			this.txtNamn = new System.Windows.Forms.RichTextBox();
			this.lblNamn = new System.Windows.Forms.Label();
			this.lblRefID = new System.Windows.Forms.Label();
			this.txtRitningsfil = new System.Windows.Forms.RichTextBox();
			this.lblRitningsfil = new System.Windows.Forms.Label();
			this.txtBildfil = new System.Windows.Forms.RichTextBox();
			this.lblBildfil = new System.Windows.Forms.Label();
			this.txtFarg = new System.Windows.Forms.RichTextBox();
			this.lblFarg = new System.Windows.Forms.Label();
			this.txtTyp = new System.Windows.Forms.RichTextBox();
			this.lblTyp = new System.Windows.Forms.Label();
			this.txtMontering = new System.Windows.Forms.RichTextBox();
			this.txtBeskrivning = new System.Windows.Forms.RichTextBox();
			this.lblMontering = new System.Windows.Forms.Label();
			this.lblBeskrivning = new System.Windows.Forms.Label();
			this.lblPris = new System.Windows.Forms.Label();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.panelFunktion.SuspendLayout();
			this.panelInmatning.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnNy
			// 
			resources.ApplyResources(this.btnNy, "btnNy");
			this.btnNy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNy.Name = "btnNy";
			this.btnNy.UseVisualStyleBackColor = true;
			this.btnNy.Click += new System.EventHandler(this.btnNy_Click);
			// 
			// btnTaBort
			// 
			resources.ApplyResources(this.btnTaBort, "btnTaBort");
			this.btnTaBort.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTaBort.Name = "btnTaBort";
			this.btnTaBort.UseVisualStyleBackColor = true;
			this.btnTaBort.Click += new System.EventHandler(this.btnTaBort_Click);
			// 
			// lblIndex
			// 
			resources.ApplyResources(this.lblIndex, "lblIndex");
			this.lblIndex.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.lblIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIndex.Name = "lblIndex";
			// 
			// panelFunktion
			// 
			resources.ApplyResources(this.panelFunktion, "panelFunktion");
			this.panelFunktion.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
			this.panelFunktion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelFunktion.Controls.Add(this.btnSpara);
			this.panelFunktion.Controls.Add(this.cboxProduktBox);
			this.panelFunktion.Controls.Add(this.lblIndex);
			this.panelFunktion.Controls.Add(this.btnTaBort);
			this.panelFunktion.Controls.Add(this.btnNy);
			this.panelFunktion.Name = "panelFunktion";
			// 
			// btnSpara
			// 
			resources.ApplyResources(this.btnSpara, "btnSpara");
			this.btnSpara.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSpara.Name = "btnSpara";
			this.btnSpara.UseVisualStyleBackColor = true;
			this.btnSpara.Click += new System.EventHandler(this.btnSpara_Click);
			// 
			// cboxProduktBox
			// 
			resources.ApplyResources(this.cboxProduktBox, "cboxProduktBox");
			this.cboxProduktBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
			this.cboxProduktBox.FormattingEnabled = true;
			this.cboxProduktBox.Name = "cboxProduktBox";
			this.cboxProduktBox.SelectedIndexChanged += new System.EventHandler(this.cboxProduktBox_SelectedIndexChanged);
			// 
			// lblID
			// 
			this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblID, "lblID");
			this.lblID.Name = "lblID";
			// 
			// panelInmatning
			// 
			resources.ApplyResources(this.panelInmatning, "panelInmatning");
			this.panelInmatning.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
			this.panelInmatning.Controls.Add(this.txtRefID);
			this.panelInmatning.Controls.Add(this.txtID);
			this.panelInmatning.Controls.Add(this.txtPris);
			this.panelInmatning.Controls.Add(this.txtNamn);
			this.panelInmatning.Controls.Add(this.lblNamn);
			this.panelInmatning.Controls.Add(this.lblRefID);
			this.panelInmatning.Controls.Add(this.txtRitningsfil);
			this.panelInmatning.Controls.Add(this.lblRitningsfil);
			this.panelInmatning.Controls.Add(this.txtBildfil);
			this.panelInmatning.Controls.Add(this.lblBildfil);
			this.panelInmatning.Controls.Add(this.txtFarg);
			this.panelInmatning.Controls.Add(this.lblFarg);
			this.panelInmatning.Controls.Add(this.txtTyp);
			this.panelInmatning.Controls.Add(this.lblTyp);
			this.panelInmatning.Controls.Add(this.txtMontering);
			this.panelInmatning.Controls.Add(this.txtBeskrivning);
			this.panelInmatning.Controls.Add(this.lblMontering);
			this.panelInmatning.Controls.Add(this.lblBeskrivning);
			this.panelInmatning.Controls.Add(this.lblPris);
			this.panelInmatning.Controls.Add(this.lblID);
			this.panelInmatning.Name = "panelInmatning";
			// 
			// txtRefID
			// 
			resources.ApplyResources(this.txtRefID, "txtRefID");
			this.txtRefID.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtRefID.DecimalPlaces = 5;
			this.txtRefID.DecimalsSeparator = '\0';
			this.txtRefID.Name = "txtRefID";
			this.txtRefID.PreFix = "";
			this.txtRefID.ThousandsSeparator = '\0';
			// 
			// txtID
			// 
			resources.ApplyResources(this.txtID, "txtID");
			this.txtID.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtID.DecimalPlaces = 5;
			this.txtID.DecimalsSeparator = '\0';
			this.txtID.Name = "txtID";
			this.txtID.PreFix = "";
			this.txtID.ThousandsSeparator = '\0';
			// 
			// txtPris
			// 
			resources.ApplyResources(this.txtPris, "txtPris");
			this.txtPris.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtPris.DecimalPlaces = 2;
			this.txtPris.DecimalsSeparator = ',';
			this.txtPris.Name = "txtPris";
			this.txtPris.PreFix = "kr";
			this.txtPris.ThousandsSeparator = ' ';
			// 
			// txtNamn
			// 
			resources.ApplyResources(this.txtNamn, "txtNamn");
			this.txtNamn.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtNamn.Name = "txtNamn";
			// 
			// lblNamn
			// 
			this.lblNamn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblNamn, "lblNamn");
			this.lblNamn.Name = "lblNamn";
			// 
			// lblRefID
			// 
			this.lblRefID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblRefID, "lblRefID");
			this.lblRefID.Name = "lblRefID";
			// 
			// txtRitningsfil
			// 
			resources.ApplyResources(this.txtRitningsfil, "txtRitningsfil");
			this.txtRitningsfil.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtRitningsfil.Name = "txtRitningsfil";
			// 
			// lblRitningsfil
			// 
			this.lblRitningsfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblRitningsfil, "lblRitningsfil");
			this.lblRitningsfil.Name = "lblRitningsfil";
			// 
			// txtBildfil
			// 
			resources.ApplyResources(this.txtBildfil, "txtBildfil");
			this.txtBildfil.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtBildfil.Name = "txtBildfil";
			// 
			// lblBildfil
			// 
			this.lblBildfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblBildfil, "lblBildfil");
			this.lblBildfil.Name = "lblBildfil";
			// 
			// txtFarg
			// 
			resources.ApplyResources(this.txtFarg, "txtFarg");
			this.txtFarg.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtFarg.Name = "txtFarg";
			// 
			// lblFarg
			// 
			this.lblFarg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblFarg, "lblFarg");
			this.lblFarg.Name = "lblFarg";
			// 
			// txtTyp
			// 
			resources.ApplyResources(this.txtTyp, "txtTyp");
			this.txtTyp.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtTyp.Name = "txtTyp";
			// 
			// lblTyp
			// 
			this.lblTyp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblTyp, "lblTyp");
			this.lblTyp.Name = "lblTyp";
			// 
			// txtMontering
			// 
			resources.ApplyResources(this.txtMontering, "txtMontering");
			this.txtMontering.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtMontering.Name = "txtMontering";
			// 
			// txtBeskrivning
			// 
			resources.ApplyResources(this.txtBeskrivning, "txtBeskrivning");
			this.txtBeskrivning.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtBeskrivning.Name = "txtBeskrivning";
			// 
			// lblMontering
			// 
			this.lblMontering.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblMontering, "lblMontering");
			this.lblMontering.Name = "lblMontering";
			// 
			// lblBeskrivning
			// 
			this.lblBeskrivning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblBeskrivning, "lblBeskrivning");
			this.lblBeskrivning.Name = "lblBeskrivning";
			// 
			// lblPris
			// 
			this.lblPris.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.lblPris, "lblPris");
			this.lblPris.Name = "lblPris";
			// 
			// ProduktApplikationForm
			// 
			resources.ApplyResources(this, "$this");
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelInmatning);
			this.Controls.Add(this.panelFunktion);
			this.Name = "ProduktApplikationForm";
			this.Load += new System.EventHandler(this.ProduktApplikationForm_Load);
			this.panelFunktion.ResumeLayout(false);
			this.panelInmatning.ResumeLayout(false);
			this.panelInmatning.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnNy;
		private System.Windows.Forms.Button btnTaBort;
		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.Panel panelFunktion;
		private System.Windows.Forms.ComboBox cboxProduktBox;
		private System.Windows.Forms.Button btnSpara;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.Panel panelInmatning;
		private System.Windows.Forms.Label lblMontering;
		private System.Windows.Forms.Label lblBeskrivning;
		private System.Windows.Forms.Label lblPris;
		private System.Windows.Forms.Label lblRefID;
		private System.Windows.Forms.RichTextBox txtRitningsfil;
		private System.Windows.Forms.Label lblRitningsfil;
		private System.Windows.Forms.RichTextBox txtBildfil;
		private System.Windows.Forms.Label lblBildfil;
		private System.Windows.Forms.RichTextBox txtFarg;
		private System.Windows.Forms.Label lblFarg;
		private System.Windows.Forms.RichTextBox txtTyp;
		private System.Windows.Forms.Label lblTyp;
		private System.Windows.Forms.RichTextBox txtMontering;
		private System.Windows.Forms.RichTextBox txtBeskrivning;
		private System.Windows.Forms.RichTextBox txtNamn;
		private System.Windows.Forms.Label lblNamn;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private JRINCCustomControls.currencyTextBox txtPris;
		private JRINCCustomControls.currencyTextBox txtID;
		private JRINCCustomControls.currencyTextBox txtRefID;
	}
}

