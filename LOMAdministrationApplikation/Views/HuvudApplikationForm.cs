using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOMAdministrationApplikation.Models;

namespace LOMAdministrationApplikation.Views
{
	/// <summary>
	/// HuvudApplikationForm är huvudmenyn för LOM Administration Applikation.
	/// Den autentisera användaren (som måste vara inloggad på sin Windows konto)
	/// och tillåter autentiserade användare att öppna produkt och/eller användare
	/// formen.
	/// 
	/// -Instansvariabler-
	/// administrationApplikation - referns till AdministrationApplikation controllern
	/// produktForm - referens till ProduktForm
	/// användareForm - referens till AnvändareForm
	///
	/// -Konstruktör-
	/// HuvudApplikationForm - sätter referensen till AdministrationApplikation
	///		controllern och autentisera användaren
	/// 
	/// -Metoder-
	/// btnAnvändare_Click - visar AnvändareForm
	/// btnProdukter_Click - visar ProduktForm
	/// 
	/// Version: 0.3
	/// 2014-11-27
	/// Grupp 2
	/// </summary>
	public partial class HuvudApplikationForm : Form
	{
		//Instansvariabler
		private AdministrationApplikation administrationApplikation = null;
		private ProduktForm produktForm = null;
		private AnvändareForm användareForm = null;

		/// <summary>
		/// Konstruktören till HuvudApplikationForm sätter referensen till
		/// AdministrationApplikation controllern och autentisera användaren.
		/// Om användaren är inloggad kan de använder knapparna för att komma
		/// vidare och annars kan användaren inte använda knapparna och blir
		/// uppmanad att logga in till sin Windows konto.
		/// </summary>
		/// <param name="administrationApplikation">Referens till
		///		AdministrationApplikation controllern</param>
		public HuvudApplikationForm(AdministrationApplikation administrationApplikation)
		{
			//initialisera referensen till AdministrationApplikation controllern
			this.administrationApplikation = administrationApplikation;

			InitializeComponent();

			//Om användaren är inloggad (anropar metoden i Administation Applikation)
			//Välkomna användaren och ger tillgång till knapparna.
			if (administrationApplikation.AnvändarenÄrVanligInloggadAnvändare())
			{
				lblInloggning.Text = "Välkommen!";
				btnAnvändare.BackColor = Color.DarkSalmon;
				btnProdukter.BackColor = Color.LightBlue;
				btnAnvändare.Enabled = true;
				btnProdukter.Enabled = true;
			}
			//Annars användaren är inte inloggad (är som guest eller liknande)
			//Uppmana användaren att logga in och ger INTE tillgång till knapparna.
			else
			{
				lblInloggning.Text = "Du är inte inloggad på din Windows konto.  Logga in på din konto och prova igen.";
				btnAnvändare.BackColor = Color.Gray;
				btnProdukter.BackColor = Color.Gray;				
				btnAnvändare.Enabled = false;
				btnProdukter.Enabled = false;
			}
		}

		/// <summary>
		/// btnAnvändare_Click - Visar AnvändareForm när btnProdukter används.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnAnvändare)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnAnvändare_Click(object sender, EventArgs e)
		{
			//skickar vidare referensen till AdministrationApplikation controllern
			användareForm = new AnvändareForm(administrationApplikation);
			användareForm.Show();
		}

		/// <summary>
		/// btnProdukter_Click - Visar ProduktForm när btnProdukter används.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnProdukter)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnProdukter_Click(object sender, EventArgs e)
		{
			//skickar vidare referensen till AdministrationApplikation controllern
			produktForm = new ProduktForm(administrationApplikation);
			produktForm.Show();
		}
	}
}
