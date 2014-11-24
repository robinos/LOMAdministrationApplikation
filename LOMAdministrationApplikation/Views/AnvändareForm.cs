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
using System.Text.RegularExpressions;
using System.Web.Helpers;
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
		private string valdAnvändarnamn = "";
		private int högstaID = 0;

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
				if (användare.ID > högstaID)
					högstaID = användare.ID;
			}

			//Sätt default produkten (om startup) till index 0
			cboxAnvändareBox.SelectedIndex = 0;
		}

		/*
		 * Nyknappen bara tömmer alla fält.
		 * 
		 * in - sender innehåller objektreferens till knappen,
		 *		e inehåller argument för event knapptryckningen
		 */
		private void btnNy_Click(object sender, EventArgs e)
		{
			Tömma();
		}

		/*
		 * Tabortknappen används för att ta bort en befintlig produkt.
		 * Metoden testar att produkten redan existerar innan TaBortProdukt
		 * kallas i Produkt Applikation.  Om det lyckas, tas bort namnen
		 * från produkt comboboxen och fälten töms.
		 * 
		 * Vid existerande ID, meddelas användaren och ingenting händer.
		 * 
		 * in - sender innehåller objektreferens till knappen,
		 *		e inehåller argument för event knapptryckningen
		 */
		private void btnTaBort_Click(object sender, EventArgs e)
		{
			string namn = txtAnvändarnamn.Text;
			int ID = int.Parse(lblIndex.Text);

			//Om ID redan existerar
			if (TestaAttIDExistera(ID))
			{
				//Om det lyckas med att ta bort produkten, tar den även
				//bort från comboxen och tömma fälterna
				if (administrationApplikation.TaBortAnvändare(ID, namn))
				{
					cboxAnvändareBox.Items.Remove(namn);
					Tömma();
				}
				//annars något gick snett
			}
			else
				MessageBox.Show("ID finns inte!");
		}

		/*
		 * Sparaknappen används för att ändra en befintlig produkt ELLER
		 * lägg till en selectedAnvändare är "Ny".
		 * Metoden testar att produkten redan existerar vid ändring, eller
		 * att den inte redan existerar vid ny insättning.
		 * 
		 * Om ID existerar inte vid ändring, eller existerar vid tilläggning,
		 * meddelas användaren och ingening händer.
		 * 
		 * in - sender innehåller objektreferens till knappen,
		 *		e inehåller argument för event knapptryckningen
		 */
		private void btnSpara_Click(object sender, EventArgs e)
		{
			//Skapa en ny produkt att fylla från fälterna
			Användare användare = new Användare();

			//ID och Namn kommer att användas för flera testar
			//RengörInput används för att ta bort kod som kan vara skadlig
			//vid insättning till databasen
			int ID;
			if(lblIndex.Text.Equals("*"))
			{
				högstaID++;
				ID = högstaID;
			}
			else
				ID = int.Parse(lblIndex.Text);

			string användarnamn = RengörInput(txtAnvändarnamn.Text);

			//fyller produkter med informationen (med rengöring för strängar)
			användare.ID = ID;
			användare.Användarnamn = användarnamn;
			användare.Roll = RengörInput(txtRoll.Text);
			användare.Räknare = int.Parse(txtRäknare.Text);
			användare.LösenordHash = Crypto.HashPassword(RengörInput(txtLösenord.Text));
			if (rbtnLåste.Checked)
				användare.Låste = true;
			else
				användare.Låste = false;

			//Lyckades är om operationen lyckades, och fås från Produkt Applikation sedan
			bool lyckades = false;

			if (cboxAnvändareBox.SelectedIndex == 0) //Ny produkt
			{
				//Om id inte redan existerar
				if (!TestaAttIDExistera(användare.ID))
				{
					//Om namnet inte redan existera, kör tillläggning
					if (!TestaOmNamnExistera(användarnamn))
					{
						lyckades = administrationApplikation.LäggTillAnvändare(användare);
					}
					else
						MessageBox.Show("Användarnamnet existerar redan!");
				}
				else
					MessageBox.Show("ID existerar redan!");
			}
			else //Befintlig produkt
			{
				//Om id redan existerar
				if (TestaAttIDExistera(ID))
				{
					//Om namnet inte redan existera, kör updatering
					if (!TestaOmSammaNamnExistera(ID, användarnamn))
						lyckades = administrationApplikation.UppdateraAnvändare(användare);
					else
						MessageBox.Show("Användarnamnet existerar redan!");
				}
				else
					MessageBox.Show("ID finns inte!");
			}

			//Om det lyckades (sann tillbaka från Produkt Applikation)
			if (lyckades)
			{
				//Ifall namnet har ändrats, tas den bort och läggs till igen
				cboxAnvändareBox.Items.Remove(användare.Användarnamn);
				cboxAnvändareBox.Items.Add(användare.Användarnamn);
				//Fälterna ändras vid behov
				SättAnvändare(användare);
			}
			//annars något gick snett
		}

		private void AnvändareForm_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'lOM_DBDataSet.Anvandare' table. You can move, or remove it, as needed.

		}

		/*
		 * Namnet är viktigt för kompilatorn.  SelectedIndexChanged är en event
		 * som händer då något nytt väls i produkt comboboxen.
		 * 
		 * Det blir antigen "Ny" där alla fält tömms, eller en befintlig användare där
		 * fälten fylls från användaren.
		 * 
		 * in - sender innehåller objektreferens till produkt comboboxen,
		 *		e inehåller argument för event combobox index ändring
		 */
		private void cboxAnvändareBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			valdAnvändarnamn = cboxAnvändareBox.Items[cboxAnvändareBox.SelectedIndex].ToString();

			//Om comboxen är på "Ny", tömma fältarna
			if (valdAnvändarnamn.Equals("Ny"))
			{
				Tömma();
			}
			else
			{
				//Söker efter namnet från comboboxen i produktsamlingen.  Detta gör att
				//namn måste vara unik.
				foreach (Användare användare in användareSamling)
				{
					//Om namnet i comboBoxen är samma som produkten sätts fälterna
					if (användare.Användarnamn.Equals(valdAnvändarnamn))
					{
						//fälterna fylls
						lblIndex.Text = användare.ID.ToString();
						txtAnvändarnamn.Text = användare.Användarnamn;
						txtRoll.Text = användare.Roll;
						txtRäknare.Text = användare.Räknare.ToString();
						if (användare.Låste == true)
							rbtnLåste.Checked = true;
						else
							rbtnOlåste.Checked = true;

						txtLösenord.Text = "";
						txtLösenord.BackColor = Color.Gray;
						txtLösenord.Enabled = false;
					}
				}
			}
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
					txtRäknare.Text = användare.Räknare.ToString();
					if(användare.Låste == true)
						rbtnLåste.Checked = true;
					else
						rbtnOlåste.Checked = true;

					txtLösenord.Text = "";
					txtLösenord.BackColor = Color.Gray;
					txtLösenord.Enabled = false;

					//sätt nuvarande produktnamn till produkt namnet
					valdAnvändarnamn = användare.Användarnamn;
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
			txtLösenord.Text = "";
			txtLösenord.BackColor = Color.White;
			txtLösenord.Enabled = true;
			rbtnOlåste.Checked = true;
		}

		/*
		 * Testar att en ID existera i användarsamlingen.
		 * 
		 * in - id(sträng) av användaren som man vill testa om den redan existera
		 * ut - sann om den existera, annars falsk
		 */
		private bool TestaAttIDExistera(int id)
		{
			bool existera = false;

			//Letar genom alla produkter i samlingen
			foreach (Användare användare in användareSamling)
			{
				//Om id redan finns, sätts existera till sann
				if (id == användare.ID) existera = true;
			}

			return existera;
		}

		/*
		 * Testar att en namn existera i användaresamlingen.
		 * 
		 * in - namn(sträng) av användaren som man vill testa om den redan existera
		 * ut - sann om den existera, annars falsk
		 */
		private bool TestaOmNamnExistera(string namn)
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
		private bool TestaOmSammaNamnExistera(int id, string namn)
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
		 * databasen."  Administratörer kommer att använda programmet så
		 * det behöver inte vara överdriven, bara nog för att undvika misstag
		 * och hålla en minimum nivå.
		 * 
		 * in - input är en sträng
		 * ut - strängen skickas tillbaka (och kan ha ändrats för att ta bort ;)
		 */
		private string RengörInput(string input)
		{
			//Tar bort ogiltiga karaktärer 
			//[^\w\-+*/=£$.,!?:%'½&()#@\\d] matchar vilket karaktär som helst som är
			//inte en bokstav, ett nummer, ett matematiskt tecken, en pund eller dollar
			//symbol, en punkt, en comma, en utrops tecken, en frågatecken, en colon,
			//en procent symbol, en apostrof, en halv symbol, en och symbol, cirkel
			//parenteser, en # symbol, en @ symbol, eller en \ symbol.
			//(allt annat blir ogiltig)
			try
			{
				return Regex.Replace(input, @"[^\w\-+*/=£$.,!?:%'½&()#@\\d]", "",
									 RegexOptions.None, TimeSpan.FromSeconds(1.5));
			}
			//Ifall det tar för mycket tid har något gått fel.  Returnera en
			//tom sträng istället.
			catch (RegexMatchTimeoutException)
			{
				return String.Empty;
			}
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
