namespace LOMAdministrationApplikation.Views
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
			this.btnNy = new System.Windows.Forms.Button();
			this.btnTaBort = new System.Windows.Forms.Button();
			this.lblIndex = new System.Windows.Forms.Label();
			this.panelFunktion = new System.Windows.Forms.Panel();
			this.btnAvsluta = new System.Windows.Forms.Button();
			this.cboxKategori = new System.Windows.Forms.ComboBox();
			this.txtSök = new System.Windows.Forms.RichTextBox();
			this.btnFörsta = new System.Windows.Forms.Button();
			this.btnSista = new System.Windows.Forms.Button();
			this.btnNästa = new System.Windows.Forms.Button();
			this.btnTillbaka = new System.Windows.Forms.Button();
			this.btnSpara = new System.Windows.Forms.Button();
			this.cboxProduktBox = new System.Windows.Forms.ComboBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
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
			this.lblID = new System.Windows.Forms.Label();
			this.panelFunktion.SuspendLayout();
			this.panelInmatning.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnNy
			// 
			this.btnNy.AccessibleDescription = "Ny knapp";
			this.btnNy.AccessibleName = "Ny";
			this.btnNy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNy.Location = new System.Drawing.Point(100, 3);
			this.btnNy.Name = "btnNy";
			this.btnNy.Size = new System.Drawing.Size(50, 25);
			this.btnNy.TabIndex = 1;
			this.btnNy.Text = "Ny";
			this.btnNy.UseVisualStyleBackColor = true;
			this.btnNy.Click += new System.EventHandler(this.btnNy_Click);
			// 
			// btnTaBort
			// 
			this.btnTaBort.AccessibleDescription = "Ta bort knappen";
			this.btnTaBort.AccessibleName = "Ta bort";
			this.btnTaBort.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTaBort.Location = new System.Drawing.Point(156, 3);
			this.btnTaBort.Name = "btnTaBort";
			this.btnTaBort.Size = new System.Drawing.Size(50, 25);
			this.btnTaBort.TabIndex = 2;
			this.btnTaBort.Text = "Ta bort";
			this.btnTaBort.UseVisualStyleBackColor = true;
			this.btnTaBort.Click += new System.EventHandler(this.btnTaBort_Click);
			// 
			// lblIndex
			// 
			this.lblIndex.AccessibleDescription = "ID Index";
			this.lblIndex.AccessibleName = "Index";
			this.lblIndex.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.lblIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIndex.Location = new System.Drawing.Point(3, 4);
			this.lblIndex.Name = "lblIndex";
			this.lblIndex.Size = new System.Drawing.Size(91, 23);
			this.lblIndex.TabIndex = 0;
			this.lblIndex.Text = "0";
			this.lblIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panelFunktion
			// 
			this.panelFunktion.AccessibleDescription = "Funktionpanel";
			this.panelFunktion.AccessibleName = "Funktionpanel";
			this.panelFunktion.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
			this.panelFunktion.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panelFunktion.AutoSize = true;
			this.panelFunktion.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelFunktion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelFunktion.Controls.Add(this.btnAvsluta);
			this.panelFunktion.Controls.Add(this.cboxKategori);
			this.panelFunktion.Controls.Add(this.txtSök);
			this.panelFunktion.Controls.Add(this.btnFörsta);
			this.panelFunktion.Controls.Add(this.btnSista);
			this.panelFunktion.Controls.Add(this.btnNästa);
			this.panelFunktion.Controls.Add(this.btnTillbaka);
			this.panelFunktion.Controls.Add(this.btnSpara);
			this.panelFunktion.Controls.Add(this.cboxProduktBox);
			this.panelFunktion.Controls.Add(this.lblIndex);
			this.panelFunktion.Controls.Add(this.btnTaBort);
			this.panelFunktion.Controls.Add(this.btnNy);
			this.panelFunktion.Location = new System.Drawing.Point(12, 12);
			this.panelFunktion.MinimumSize = new System.Drawing.Size(720, 60);
			this.panelFunktion.Name = "panelFunktion";
			this.panelFunktion.Size = new System.Drawing.Size(758, 61);
			this.panelFunktion.TabIndex = 5;
			// 
			// btnAvsluta
			// 
			this.btnAvsluta.AccessibleDescription = "Avsluta knappen";
			this.btnAvsluta.AccessibleName = "Avsluta";
			this.btnAvsluta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnAvsluta.BackColor = System.Drawing.Color.LightCoral;
			this.btnAvsluta.Location = new System.Drawing.Point(268, 3);
			this.btnAvsluta.Name = "btnAvsluta";
			this.btnAvsluta.Size = new System.Drawing.Size(62, 25);
			this.btnAvsluta.TabIndex = 12;
			this.btnAvsluta.Text = "Avsluta";
			this.btnAvsluta.UseVisualStyleBackColor = false;
			this.btnAvsluta.Click += new System.EventHandler(this.btnAvsluta_Click);
			// 
			// cboxKategori
			// 
			this.cboxKategori.AccessibleDescription = "Kategori filter";
			this.cboxKategori.AccessibleName = "Kategori filter";
			this.cboxKategori.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
			this.cboxKategori.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboxKategori.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboxKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxKategori.FormattingEnabled = true;
			this.cboxKategori.Location = new System.Drawing.Point(3, 33);
			this.cboxKategori.Name = "cboxKategori";
			this.cboxKategori.Size = new System.Drawing.Size(136, 21);
			this.cboxKategori.TabIndex = 11;
			this.cboxKategori.SelectedIndexChanged += new System.EventHandler(this.cboxKategori_SelectedIndexChanged);
			// 
			// txtSök
			// 
			this.txtSök.AccessibleDescription = "Sökruta";
			this.txtSök.AccessibleName = "Sökruta";
			this.txtSök.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtSök.Location = new System.Drawing.Point(145, 33);
			this.txtSök.MaxLength = 30;
			this.txtSök.Multiline = false;
			this.txtSök.Name = "txtSök";
			this.txtSök.Size = new System.Drawing.Size(127, 21);
			this.txtSök.TabIndex = 9;
			this.txtSök.Text = "";
			this.txtSök.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSök_KeyPress);
			// 
			// btnFörsta
			// 
			this.btnFörsta.Location = new System.Drawing.Point(336, 3);
			this.btnFörsta.Name = "btnFörsta";
			this.btnFörsta.Size = new System.Drawing.Size(31, 23);
			this.btnFörsta.TabIndex = 4;
			this.btnFörsta.Text = "|<";
			this.btnFörsta.UseVisualStyleBackColor = true;
			this.btnFörsta.Click += new System.EventHandler(this.btnFörsta_Click);
			// 
			// btnSista
			// 
			this.btnSista.Location = new System.Drawing.Point(373, 3);
			this.btnSista.Name = "btnSista";
			this.btnSista.Size = new System.Drawing.Size(31, 23);
			this.btnSista.TabIndex = 5;
			this.btnSista.Text = ">|";
			this.btnSista.UseVisualStyleBackColor = true;
			this.btnSista.Click += new System.EventHandler(this.btnSista_Click);
			// 
			// btnNästa
			// 
			this.btnNästa.Enabled = false;
			this.btnNästa.Location = new System.Drawing.Point(666, 3);
			this.btnNästa.Name = "btnNästa";
			this.btnNästa.Size = new System.Drawing.Size(85, 23);
			this.btnNästa.TabIndex = 8;
			this.btnNästa.Text = "Nästa 5 >>";
			this.btnNästa.UseVisualStyleBackColor = true;
			this.btnNästa.Click += new System.EventHandler(this.btnNästa_Click);
			// 
			// btnTillbaka
			// 
			this.btnTillbaka.Enabled = false;
			this.btnTillbaka.Location = new System.Drawing.Point(575, 3);
			this.btnTillbaka.Name = "btnTillbaka";
			this.btnTillbaka.Size = new System.Drawing.Size(85, 23);
			this.btnTillbaka.TabIndex = 7;
			this.btnTillbaka.Text = "<< Tillbaka 5";
			this.btnTillbaka.UseVisualStyleBackColor = true;
			this.btnTillbaka.Click += new System.EventHandler(this.btnTillbaka_Click);
			// 
			// btnSpara
			// 
			this.btnSpara.AccessibleDescription = "Spara knappen";
			this.btnSpara.AccessibleName = "Spara";
			this.btnSpara.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSpara.Location = new System.Drawing.Point(212, 3);
			this.btnSpara.Name = "btnSpara";
			this.btnSpara.Size = new System.Drawing.Size(50, 25);
			this.btnSpara.TabIndex = 3;
			this.btnSpara.Text = "Spara";
			this.btnSpara.UseVisualStyleBackColor = true;
			this.btnSpara.Click += new System.EventHandler(this.btnSpara_Click);
			// 
			// cboxProduktBox
			// 
			this.cboxProduktBox.AccessibleDescription = "Produkt Combobox";
			this.cboxProduktBox.AccessibleName = "Produktlista";
			this.cboxProduktBox.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
			this.cboxProduktBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboxProduktBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboxProduktBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxProduktBox.FormattingEnabled = true;
			this.cboxProduktBox.Location = new System.Drawing.Point(410, 5);
			this.cboxProduktBox.Name = "cboxProduktBox";
			this.cboxProduktBox.Size = new System.Drawing.Size(159, 21);
			this.cboxProduktBox.TabIndex = 6;
			this.cboxProduktBox.SelectedIndexChanged += new System.EventHandler(this.cboxProduktBox_SelectedIndexChanged);
			// 
			// panelInmatning
			// 
			this.panelInmatning.AccessibleDescription = "Inmatningspanel";
			this.panelInmatning.AccessibleName = "Inmatning";
			this.panelInmatning.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
			this.panelInmatning.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panelInmatning.AutoSize = true;
			this.panelInmatning.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
			this.panelInmatning.Location = new System.Drawing.Point(30, 90);
			this.panelInmatning.MinimumSize = new System.Drawing.Size(725, 361);
			this.panelInmatning.Name = "panelInmatning";
			this.panelInmatning.Size = new System.Drawing.Size(725, 361);
			this.panelInmatning.TabIndex = 8;
			// 
			// txtRefID
			// 
			this.txtRefID.AccessibleDescription = "Produkt referens ID";
			this.txtRefID.AccessibleName = "RefID";
			this.txtRefID.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtRefID.DecimalPlaces = 10;
			this.txtRefID.DecimalsSeparator = '\0';
			this.txtRefID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtRefID.Location = new System.Drawing.Point(528, 0);
			this.txtRefID.MaxLength = 10;
			this.txtRefID.Multiline = true;
			this.txtRefID.Name = "txtRefID";
			this.txtRefID.PreFix = "";
			this.txtRefID.Size = new System.Drawing.Size(194, 30);
			this.txtRefID.TabIndex = 33;
			this.txtRefID.Text = "0000000000";
			this.txtRefID.ThousandsSeparator = '\0';
			// 
			// txtID
			// 
			this.txtID.AccessibleDescription = "Produkt ID";
			this.txtID.AccessibleName = "ID";
			this.txtID.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtID.DecimalPlaces = 10;
			this.txtID.DecimalsSeparator = '\0';
			this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtID.Location = new System.Drawing.Point(164, 0);
			this.txtID.MaxLength = 10;
			this.txtID.Multiline = true;
			this.txtID.Name = "txtID";
			this.txtID.PreFix = "";
			this.txtID.Size = new System.Drawing.Size(194, 30);
			this.txtID.TabIndex = 9;
			this.txtID.Text = "0000000000";
			this.txtID.ThousandsSeparator = '\0';
			// 
			// txtPris
			// 
			this.txtPris.AccessibleDescription = "Produkt pris";
			this.txtPris.AccessibleName = "Pris";
			this.txtPris.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtPris.DecimalPlaces = 2;
			this.txtPris.DecimalsSeparator = ',';
			this.txtPris.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtPris.Location = new System.Drawing.Point(164, 60);
			this.txtPris.MaxLength = 10;
			this.txtPris.Multiline = true;
			this.txtPris.Name = "txtPris";
			this.txtPris.PreFix = "kr";
			this.txtPris.Size = new System.Drawing.Size(194, 30);
			this.txtPris.TabIndex = 11;
			this.txtPris.Text = "0,00";
			this.txtPris.ThousandsSeparator = ' ';
			// 
			// txtNamn
			// 
			this.txtNamn.AccessibleDescription = "Produktnamn";
			this.txtNamn.AccessibleName = "Namn";
			this.txtNamn.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtNamn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtNamn.Location = new System.Drawing.Point(164, 30);
			this.txtNamn.MaxLength = 30;
			this.txtNamn.Name = "txtNamn";
			this.txtNamn.Size = new System.Drawing.Size(194, 30);
			this.txtNamn.TabIndex = 10;
			this.txtNamn.Text = "";
			// 
			// lblNamn
			// 
			this.lblNamn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblNamn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblNamn.Location = new System.Drawing.Point(3, 30);
			this.lblNamn.Name = "lblNamn";
			this.lblNamn.Size = new System.Drawing.Size(164, 30);
			this.lblNamn.TabIndex = 29;
			this.lblNamn.Text = "Namn:";
			this.lblNamn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRefID
			// 
			this.lblRefID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblRefID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblRefID.Location = new System.Drawing.Point(364, 0);
			this.lblRefID.Name = "lblRefID";
			this.lblRefID.Size = new System.Drawing.Size(164, 30);
			this.lblRefID.TabIndex = 27;
			this.lblRefID.Text = "RefID:";
			this.lblRefID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRitningsfil
			// 
			this.txtRitningsfil.AccessibleDescription = "Produkt ritningsfilnamn";
			this.txtRitningsfil.AccessibleName = "Ritningsfilnamn";
			this.txtRitningsfil.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtRitningsfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtRitningsfil.Location = new System.Drawing.Point(164, 180);
			this.txtRitningsfil.MaxLength = 30;
			this.txtRitningsfil.Name = "txtRitningsfil";
			this.txtRitningsfil.Size = new System.Drawing.Size(194, 30);
			this.txtRitningsfil.TabIndex = 15;
			this.txtRitningsfil.Text = "";
			// 
			// lblRitningsfil
			// 
			this.lblRitningsfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblRitningsfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblRitningsfil.Location = new System.Drawing.Point(3, 180);
			this.lblRitningsfil.Name = "lblRitningsfil";
			this.lblRitningsfil.Size = new System.Drawing.Size(164, 30);
			this.lblRitningsfil.TabIndex = 25;
			this.lblRitningsfil.Text = "Ritningsfilnamn:";
			this.lblRitningsfil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtBildfil
			// 
			this.txtBildfil.AccessibleDescription = "Produkt bildfilnamn";
			this.txtBildfil.AccessibleName = "Bildfilnamn";
			this.txtBildfil.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtBildfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtBildfil.Location = new System.Drawing.Point(164, 150);
			this.txtBildfil.MaxLength = 30;
			this.txtBildfil.Name = "txtBildfil";
			this.txtBildfil.Size = new System.Drawing.Size(194, 30);
			this.txtBildfil.TabIndex = 14;
			this.txtBildfil.Text = "";
			// 
			// lblBildfil
			// 
			this.lblBildfil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblBildfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblBildfil.Location = new System.Drawing.Point(3, 150);
			this.lblBildfil.Name = "lblBildfil";
			this.lblBildfil.Size = new System.Drawing.Size(164, 30);
			this.lblBildfil.TabIndex = 23;
			this.lblBildfil.Text = "Bildfilnamn:";
			this.lblBildfil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtFarg
			// 
			this.txtFarg.AccessibleDescription = "Produkt färg";
			this.txtFarg.AccessibleName = "Färg";
			this.txtFarg.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtFarg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtFarg.Location = new System.Drawing.Point(164, 120);
			this.txtFarg.MaxLength = 30;
			this.txtFarg.Name = "txtFarg";
			this.txtFarg.Size = new System.Drawing.Size(194, 30);
			this.txtFarg.TabIndex = 13;
			this.txtFarg.Text = "";
			// 
			// lblFarg
			// 
			this.lblFarg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFarg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblFarg.Location = new System.Drawing.Point(3, 120);
			this.lblFarg.Name = "lblFarg";
			this.lblFarg.Size = new System.Drawing.Size(164, 30);
			this.lblFarg.TabIndex = 21;
			this.lblFarg.Text = "Färg:";
			this.lblFarg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtTyp
			// 
			this.txtTyp.AccessibleDescription = "Produkttyp";
			this.txtTyp.AccessibleName = "Typ";
			this.txtTyp.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtTyp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtTyp.Location = new System.Drawing.Point(164, 90);
			this.txtTyp.MaxLength = 30;
			this.txtTyp.Name = "txtTyp";
			this.txtTyp.Size = new System.Drawing.Size(194, 30);
			this.txtTyp.TabIndex = 12;
			this.txtTyp.Text = "";
			// 
			// lblTyp
			// 
			this.lblTyp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTyp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblTyp.Location = new System.Drawing.Point(3, 90);
			this.lblTyp.Name = "lblTyp";
			this.lblTyp.Size = new System.Drawing.Size(164, 30);
			this.lblTyp.TabIndex = 19;
			this.lblTyp.Text = "Typ:";
			this.lblTyp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMontering
			// 
			this.txtMontering.AccessibleDescription = "Produkt monteringsbeskrivning";
			this.txtMontering.AccessibleName = "Monteringsbeskrivning";
			this.txtMontering.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtMontering.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtMontering.Location = new System.Drawing.Point(3, 240);
			this.txtMontering.MaxLength = 300;
			this.txtMontering.Name = "txtMontering";
			this.txtMontering.Size = new System.Drawing.Size(355, 118);
			this.txtMontering.TabIndex = 16;
			this.txtMontering.Text = "";
			// 
			// txtBeskrivning
			// 
			this.txtBeskrivning.AccessibleDescription = "Produktbeskrivning";
			this.txtBeskrivning.AccessibleName = "Beskrivning";
			this.txtBeskrivning.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
			this.txtBeskrivning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.txtBeskrivning.Location = new System.Drawing.Point(364, 60);
			this.txtBeskrivning.MaxLength = 800;
			this.txtBeskrivning.Name = "txtBeskrivning";
			this.txtBeskrivning.Size = new System.Drawing.Size(355, 298);
			this.txtBeskrivning.TabIndex = 17;
			this.txtBeskrivning.Text = "";
			// 
			// lblMontering
			// 
			this.lblMontering.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblMontering.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblMontering.Location = new System.Drawing.Point(3, 210);
			this.lblMontering.Name = "lblMontering";
			this.lblMontering.Size = new System.Drawing.Size(164, 30);
			this.lblMontering.TabIndex = 10;
			this.lblMontering.Text = "Monteringsbeskrivning:";
			this.lblMontering.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBeskrivning
			// 
			this.lblBeskrivning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblBeskrivning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblBeskrivning.Location = new System.Drawing.Point(364, 30);
			this.lblBeskrivning.Name = "lblBeskrivning";
			this.lblBeskrivning.Size = new System.Drawing.Size(164, 30);
			this.lblBeskrivning.TabIndex = 9;
			this.lblBeskrivning.Text = "Beskrivning:";
			this.lblBeskrivning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPris
			// 
			this.lblPris.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPris.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblPris.Location = new System.Drawing.Point(3, 60);
			this.lblPris.Name = "lblPris";
			this.lblPris.Size = new System.Drawing.Size(164, 30);
			this.lblPris.TabIndex = 8;
			this.lblPris.Text = "Pris (sek):";
			this.lblPris.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblID
			// 
			this.lblID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.lblID.Location = new System.Drawing.Point(3, 0);
			this.lblID.Name = "lblID";
			this.lblID.Size = new System.Drawing.Size(164, 30);
			this.lblID.TabIndex = 6;
			this.lblID.Text = "ID:";
			this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ProduktForm
			// 
			this.AccessibleDescription = "Produktredigering";
			this.AccessibleName = "Produktredigering";
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 462);
			this.Controls.Add(this.panelInmatning);
			this.Controls.Add(this.panelFunktion);
			this.MinimumSize = new System.Drawing.Size(800, 500);
			this.Name = "ProduktForm";
			this.Text = "Ljus och Miljö AB - Produktredigering";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProduktForm_FormClosing);
			this.panelFunktion.ResumeLayout(false);
			this.panelInmatning.ResumeLayout(false);
			this.panelInmatning.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnNy;
		private System.Windows.Forms.Button btnTaBort;
		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.Panel panelFunktion;
		private System.Windows.Forms.ComboBox cboxProduktBox;
		private System.Windows.Forms.Button btnSpara;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Button btnNästa;
		private System.Windows.Forms.Button btnTillbaka;
		private System.Windows.Forms.Button btnFörsta;
		private System.Windows.Forms.Button btnSista;
		private System.Windows.Forms.Panel panelInmatning;
		private JRINCCustomControls.currencyTextBox txtRefID;
		private JRINCCustomControls.currencyTextBox txtID;
		private JRINCCustomControls.currencyTextBox txtPris;
		private System.Windows.Forms.RichTextBox txtNamn;
		private System.Windows.Forms.Label lblNamn;
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
		private System.Windows.Forms.Label lblMontering;
		private System.Windows.Forms.Label lblBeskrivning;
		private System.Windows.Forms.Label lblPris;
		private System.Windows.Forms.Label lblID;
		private System.Windows.Forms.ComboBox cboxKategori;
		private System.Windows.Forms.RichTextBox txtSök;
		private System.Windows.Forms.Button btnAvsluta;
	}
}

