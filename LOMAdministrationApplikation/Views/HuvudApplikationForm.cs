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
	/// (Vy)
	/// 
	/// HuvudApplikationForm är huvudmenyn för LOM Administration Applikation.
	/// Den autentisera användaren (som måste vara inloggad på sin Windows konto)
	/// och tillåter autentiserade användare att öppna produkt och/eller användare
	/// formen.
	///
	/// -Konstruktor-
	/// HuvudApplikationForm - sätter referensen till AdministrationApplikation
	///		controllern och autentisera användaren
	/// 
	/// -Metoder-
	/// btnAnvändare_Click - visar AnvändareForm
	/// btnProdukter_Click - visar ProduktForm
	/// btnAvsluta_Click - stängar formen
	/// AutentiseraAnvändaren - testar om användaren är inloggad för att (o)aktivera
	///		knapparna
	/// 
	/// Version: 0.5
	/// 2014-12-07
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
		///		AdministrationApplikation kontrollern</param>
		public HuvudApplikationForm(AdministrationApplikation administrationApplikation)
		{
			//initialisera referensen till AdministrationApplikation kontrollern
			this.administrationApplikation = administrationApplikation;

			InitializeComponent();

			AutentiseraAnvändaren();
		}

		/// <summary>
		/// btnAnvändare_Click - Visar AnvändareForm när btnProdukter används.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnAnvändare)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnAnvändare_Click(object sender, EventArgs e)
		{
			//Skickar vidare referensen till AdministrationApplikation controllern
			//och referenser till HuvudApplikationForm för att visa den igen
			användareForm = new AnvändareForm(administrationApplikation, this);
			användareForm.Show();
			this.Hide();
		}

		/// <summary>
		/// btnProdukter_Click - Visar ProduktForm när btnProdukter används.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnProdukter)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnProdukter_Click(object sender, EventArgs e)
		{
			//Skickar vidare referensen till AdministrationApplikation controllern
			//och referenser till HuvudApplikationForm för att visa den igen
			produktForm = new ProduktForm(administrationApplikation, this);
			produktForm.Show();
			this.Hide();
		}

		/// <summary>
		/// btnAvsluta_Click stängar formen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnAvsluta)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnAvsluta_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// AutentiseraAnvändaren testar om användaren är inloggad.
		/// Om användaren är inloggad kan den använder knapparna för att komma
		/// vidare och annars kan användaren inte använda knapparna och blir
		/// uppmanad att logga in till sin Windows konto.
		/// </summary>
		public void AutentiseraAnvändaren()
		{
			//Om användaren är inloggad (anropar metoden i Administation Applikation)
			//Välkomna användaren och ger tillgång till knapparna.
			if (administrationApplikation.AnvändarenÄrAdministratör())
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
				lblInloggning.Text = "Du är inte inloggad som administrator. Kör om programmet som administrator.";
				btnAnvändare.BackColor = Color.Gray;
				btnProdukter.BackColor = Color.Gray;
				btnAnvändare.Enabled = false;
				btnProdukter.Enabled = false;
			}
		}
	}
}
