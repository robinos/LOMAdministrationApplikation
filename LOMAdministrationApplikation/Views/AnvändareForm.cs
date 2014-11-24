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
using System.Security.Cryptography;

namespace LOMAdministrationApplikation.Views
{
	public partial class AnvändareForm : Form
	{
		//instansvariabler
		//Referens till ProduktApplikationen (Kontroller)
		private AdministrationApplikation administrationApplikation;
		//Behållare för en samling av Produkt värdena (utan nycklar) från en Dictionary
		private Dictionary<string, Användare>.ValueCollection användareSamling;
		//Den produkt som är aktiv i produkt comboboxen
		private string selectedProduktnamn = "";

		public AnvändareForm(AdministrationApplikation administrationApplikation)
		{
			InitializeComponent();

			//Sätt produktApplikation objekt
			this.administrationApplikation = administrationApplikation;

			//Sätt samling till värdena av Dictionary Produkter från ProduktApplikation
			användareSamling = administrationApplikation.AllaAnvändare.Values;

			//Lägg till "Ny" för nya produkter
			cboxAnvändareBox.Items.Add("Ny");

			//För varje produkt som finns i samlingen, lägg till namnet i
			//produkt comboboxen
			foreach (Användare användare in användareSamling)
			{
				cboxAnvändareBox.Items.Add(användare.Användarnamn);
			}

			//Sätt default produkten (om startup) till index 0
			cboxAnvändareBox.SelectedIndex = 0;
		}

		private void AnvändareForm_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'lOM_DBDataSet.Anvandare' table. You can move, or remove it, as needed.

		}

		private void cboxAnvändareBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		/*
		 * SättAnvändare sätter fälterna till användarens värden.  Den även gör den
		 * till den användaren som visas just nu (och sätter användare comboboxen till
		 * användaren).
		 * 
		 * in - användaren som innehåller information som ska visas
		 */
		private void SättAnvändare(Användare användare)
		{
			//Söker efter namnet från produkten.  Detta gör att namn måste
			//vara unik.
			for (int i = 0; i < cboxAnvändareBox.Items.Count; i++)
			{
				//Om namnet är samma som den i comboboxen, sätt fälterna
				//efter dens variabler
				if (användare.Användarnamn.Equals(cboxAnvändareBox.Items[i].ToString()))
				{
					lblIndex.Text = användare.ID.ToString();
					txtAnvändarnamn.Text = användare.Användarnamn;
					txtRoll.Text = användare.Roll;
					txtRäknare.Text = användare.Roll.ToString();
					if(användare.Låste == true)
						rbtnLåste.Checked = true;
					else
						rbtnOlåste.Checked = true;

					//sätt nuvarande produktnamn till produkt namnet
					selectedProduktnamn = användare.Användarnamn;
					cboxAnvändareBox.SelectedIndex = i;
				}
			}
		}

		/*
		 * Fälterna tömms eller sätts till en default värde.  Index blir *
		 * istället för någon ID värde och "Ny" visas i produkt comboboxen.
		 */
		private void Tömma()
		{
			lblIndex.Text = "*";
			cboxAnvändareBox.SelectedIndex = 0; //Ny
			txtAnvändarnamn.Text = "";
			txtRoll.Text = "kund";
			txtRäknare.Text = "0";
			rbtnOlåste.Checked = true;
		}

		/*
		 * Testar att en ID existera i användarsamlingen.
		 * 
		 * in - id(sträng) av användaren som man vill testa om den redan existera
		 * ut - sann om den existera, annars falsk
		 */
		private bool TestaAttIDExistera(string id)
		{
			bool existera = false;

			//Letar genom alla produkter i samlingen
			foreach (Användare användare in användareSamling)
			{
				//Om id redan finns, sätts existera till sann
				if (id.Equals(användare.ID)) existera = true;
			}

			return existera;
		}

		/*
		 * Testar att en namn existera i användaresamlingen.
		 * 
		 * in - namn(sträng) av användaren som man vill testa om den redan existera
		 * ut - sann om den existera, annars falsk
		 */
		private bool TestaAttNamnExistera(string namn)
		{
			bool existera = false;

			//Letar genom alla produkter i samlingen
			foreach (Användare användare in användareSamling)
			{
				//Om namnet redan finns, sätts existera till sann
				if (namn.Equals(användare.Användarnamn)) existera = true;
			}

			return existera;
		}


		/*
		 * Testar om en annan produkt (annan id) har samma namn.
		 * 
		 * in - id(sträng) av användaren som man vill testa, namn(sträng) som
		 *		man vill testa om en annan användare också har den
		 * ut - sann om en annan användare har samma namn, annars falsk
		 */
		private bool TestaAttSammaAnvändarnamnExistera(string id, string namn)
		{
			bool existera = false;

			//Letar genom alla produkter i samlingen
			foreach (Användare användare in användareSamling)
			{
				//Om namnet hittas
				if (namn.Equals(användare.Användarnamn))
				{
					//Om namnet är inte till första produkten, finns det en
					//annan som också har namnet.  Existera sätts till sann
					if (!id.Equals(användare.ID))
						existera = true;
				}
			}

			return existera;
		}

		/*
		 * Rengör användarinput så det blir ingen situation som
		 * Farg = "blå;Drop Table Produkter;" när man skickar det till
		 * databasen."
		 * 
		 * in - input är en sträng
		 * ut - strängen skickas tillbaka (och kan ha ändrats för att ta bort ;)
		 */
		private string RengörInput(string input)
		{
			//.Replace är en sträng metod som ersätter en substräng med en annan
			return (input.Replace(";", ""));
		}

		public string EncodePassword(string password)
		{
			SHA256 mySHA256 = SHA256Managed.Create();
			byte[] bytes = Encoding.Unicode.GetBytes(password);
			byte[] hashValue = mySHA256.ComputeHash(bytes);
			byte[] inArray = HashAlgorithm.Create("SHA256").ComputeHash(bytes);
			return Convert.ToBase64String(inArray);
		}

		public string DecodePassword(string passwordHash)
		{
			SHA256 mySHA256 = SHA256Managed.Create();
			byte[] bytes = Encoding.Unicode.GetBytes(passwordHash);
			byte[] hashValue = mySHA256.ComputeHash(bytes);
			byte[] inArray = HashAlgorithm.Create("SHA256").ComputeHash(bytes);
			return Convert.ToBase64String(inArray);
		}
		
	}
}
