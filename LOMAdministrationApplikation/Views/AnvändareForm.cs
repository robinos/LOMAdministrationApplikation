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
using System.Web.Helpers; //För Crypto.HashPassword

namespace LOMAdministrationApplikation.Views
{
	/// <summary>
	/// (Vy)
	/// 
	/// AnvändareForm har all kod för att hantera användare input och skicka vidare
	/// till AdministrationApplikation för hantering.
	/// 
	/// -Konstruktör-
	/// AnvändareForm - sätter referensen till AdministrationApplikation kontrollern
	///		och fyller comboboxen med datan för första sidan
	/// 
	/// -Metoder-
	/// btnNy_Click - "Ny" knappen tömmer fälterna och sätter användaren till "Ny"
	/// btnTaBort_Click - "TaBort" knappen används för att ta bort en befintlig
	///		användare från databasen
	///	btnSpara_Click - "Spara" knappen används antingen för att lägga till en
	///		ny användare eller för att ändra en benfintlig	
	/// cboxProduktBox_SelectedIndexChanged - Vid ändring i användre comboboxen
	///		ändras även fälterna för att visa information om användaren
	/// SättProdukt - Hjälpmetod för att sätta fälterna till en viss användares
	///		information
	/// Tömma - Tömmer alla fälterna (eller sätter till default värden) 
	/// TestaAttIDExistera - Testar om en ID redan existerar i listan över alla
	///		användare / databas
	/// TestaAttNamnExistera - Testar om ett namn redan existerar i listan över
	///		alla användare / databas
	/// TestaAttSammaNamnExistera - Testar om samma namn redan existerar för någon
	///		annan användare i listan över alla användare / databas
	///	RengörInput - städa input från användaren för lite säkerhet
	/// initiatiseraComboBox - initialisera comboboxen för att visa nuvarande sidan
	/// btnFörsta_Click - Visar första sidan (x antal användare) i comboboxen
	/// btnSista_Click - Visar sista sidan (x antal användare) i comboboxen
	/// btnTillbaka_Click - Visar föregående sida (x antal användare) i comboboxen
	/// btnNästa_Click - Visar nästa sida (x antal användare) i comboboxen
	/// 
	/// Version: 0.3
	/// 2014-11-28
	/// Grupp 2
	/// </summary> 
	public partial class AnvändareForm : Form
	{
		//instansvariabler
		//Referens till ProduktApplikationen (Kontroller)
		private AdministrationApplikation administrationApplikation;
		//Behållare för en samling av Produkt värdena (utan nycklar) från en Dictionary
		private List<Användare> användarSidaLista;
		//Den produkt som är aktiv i produkt comboboxen
		private string valdAnvändarnamn = "";
		private int högstaID = 0;
		private int sida = 1;

		/// <summary>
		/// Konstruktör AnvändareForm tar emot en referens till
		/// AdministrationApplikation kontrollern, hämtar första combobox sidan
		/// av användare och initialisera comboboxen.  Om där finns fler än en
		/// sida aktiveras nästa knappen.
		/// </summary>
		/// <param name="administrationApplikation">referensen till
		///		AdministrationApplikation kontrollern</param>
		public AnvändareForm(AdministrationApplikation administrationApplikation)
		{
			//Initialisera formen
			InitializeComponent();

			//Sätt referensen till AdministrationApplikation objektet
			this.administrationApplikation = administrationApplikation;

			//hämta första sidan och sätt högsta användare ID för ID skapelse sedan
			användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida);
			högstaID = administrationApplikation.HögstaAnvändareID;

			//initialiser innehållet i comboboxen
			initiatiseraComboBox();

			//Om där finns fler än en sida aktiveras nästa knappen
			if (administrationApplikation.TotallaSidorAnvändare > 1)
			{
				btnNästa.Enabled = true;
				btnTillbaka.Enabled = false;
			}
		}

		/// <summary>
		/// btnNy_Click bara anroper tömma för att tömmer alla fält och
		/// sätt det till en "Ny" användare i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnTaBort)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnNy_Click(object sender, EventArgs e)
		{
			Tömma();
		}

		/// <summary>
		/// btnTaBort_Click används för att ta bort en befintlig användare.
		/// Metoden testar att användaren redan existerar innan borttagning.
		/// Om det lyckas tas bort namnet och fälten töms då sidan laddas om.
		/// Vid existerande ID meddelas användaren och ingenting händer.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnTaBort)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnTaBort_Click(object sender, EventArgs e)
		{
			string namn = txtAnvändarnamn.Text;
			int ID = int.Parse(lblIndex.Text);

			//Om ID redan existerar
			if (TestaAttIDExistera(ID))
			{
				//Om det lyckas med att ta bort användaren, tar den även
				//bort från comboxen och tömma fälterna
				if (administrationApplikation.TaBortAnvändare(ID))
				{
					användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida);
					initiatiseraComboBox();
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
		/// <summary>
		/// btnSpara_Click används för att ändra en befintlig användare ELLER
		/// för att lägga till en användare vid "Ny".
		/// Metoden testar att användaren redan existerar vid ändring eller
		/// att den inte redan existerar vid ny insättning.
		/// Om en ID inte existerar vid ändring eller existerar vid tilläggning
		/// meddelas användaren och ingening händer.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnSpara)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnSpara_Click(object sender, EventArgs e)
		{
			//Skapa en ny produkt att fylla från fälterna
			Användare användare = new Användare();

			//ID och Namn kommer att användas för flera testar
			
			//Skapar ny ID vid insättning
			int ID;
			if(lblIndex.Text.Equals("*"))
			{
				högstaID++;
				ID = högstaID;
			}
			else
				ID = int.Parse(lblIndex.Text);

			//RengörInput används för att ta bort kod som kan vara skadlig
			//vid insättning till databasen
			string användarnamn = RengörInput(txtAnvändarnamn.Text);

			//fyller användare med informationen (med rengöring för strängar)
			användare.ID = ID;
			användare.Användarnamn = användarnamn;
			användare.Roll = RengörInput(txtRoll.Text);
			användare.Räknare = int.Parse(txtRäknare.Text);
			if (rbtnLåst.Checked)
				användare.Låst = true;
			else
				användare.Låst = false;

			//Lyckades är om operationen lyckades, och fås från Administration
			//Applikation sedan
			bool lyckades = false;

			//Ny användare
			if (cboxAnvändareBox.SelectedIndex == 0) 
			{
				användare.LösenordHash = Crypto.HashPassword(RengörInput(txtLösenord.Text));

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
			//Befintlig användare
			else 
			{
				//Om ny lösenord är inbockade, använde det nya lösenordet
				if(checkNyLösenord.Checked)
					användare.LösenordHash = Crypto.HashPassword(RengörInput(txtLösenord.Text));
				//Annars använd förre detta lösenordet
				else 					
					användare.LösenordHash = administrationApplikation.HämtaAnvändareMedID(användare.ID).LösenordHash;

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

			//Om det lyckades (sann tillbaka från Administration Applikation)
			if (lyckades)
			{
				//hämta värden för sidan
				användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida);
				//gör om innehållet i comboboxen
				initiatiseraComboBox();
				//Fälterna ändras vid behov
				SättAnvändare(användare);
			}
			//annars något gick snett
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
		/// <summary>
		/// cboxAnvändareBox_SelectedIndexChanged händer när comboboxen sätts
		/// till ett nytt värde. Det blir antigen "Ny" där alla fält tömms
		/// eller en befintlig användare där fälten fylls från användaren.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (cboxProduktBox)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void cboxAnvändareBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			valdAnvändarnamn = cboxAnvändareBox.Items[cboxAnvändareBox.SelectedIndex].ToString();

			//Om comboxen är på "Ny", tömma fältarna
			if (valdAnvändarnamn.Equals("Ny"))
			{
				Tömma();
				checkNyLösenord.Enabled = false;
			}
			else
			{
				//Söker efter namnet från comboboxen i användarlistan.  Detta gör att
				//namn måste vara unik.
				foreach (Användare användare in användarSidaLista)
				{
					//Om namnet i comboBoxen är samma som produkten sätts fälterna
					if (användare.Användarnamn.Equals(valdAnvändarnamn))
					{
						//fälterna fylls
						lblIndex.Text = användare.ID.ToString();
						txtAnvändarnamn.Text = användare.Användarnamn;
						txtRoll.Text = användare.Roll;
						txtRäknare.Text = användare.Räknare.ToString();
						if (användare.Låst == true)
							rbtnLåst.Checked = true;
						else
							rbtnUpplåst.Checked = true;

						txtLösenord.Text = "";
						txtLösenord.BackColor = Color.Gray;
						txtLösenord.Enabled = false;

						checkNyLösenord.Enabled = true;
						checkNyLösenord.Checked = false;
					}
				}
			}
		}

		/// <summary>
		/// SättAnvändare sätter fälterna till användarens värden. Den även
		/// gör den till den användare som visas just nu i comboboxen. 
		/// </summary>
		/// <param name="användare">användare som ska visas</param>
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
					if(användare.Låst == true)
						rbtnLåst.Checked = true;
					else
						rbtnUpplåst.Checked = true;

					//stäng ändring av lösenord igen till det blir vald
					checkNyLösenord.Enabled = true;
					txtLösenord.Text = "";
					txtLösenord.BackColor = Color.Gray;
					txtLösenord.Enabled = false;

					//sätt nuvarande produktnamn till produkt namnet
					valdAnvändarnamn = användare.Användarnamn;
					cboxAnvändareBox.SelectedIndex = i;
				}
			}
		}

		/// <summary>
		/// Tömma tömmer alla fält / sätter de till ett default värde. Index
		/// blir * istället för någon ID värde och "Ny" visas i produkt
		/// comboboxen.
		/// </summary>
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
			rbtnUpplåst.Checked = true;
		}

		/// <summary>
		/// TestaAttIDExistera testar om någon användare har angiven id.
		/// </summary>
		/// <param name="id">id av en användare</param>
		/// <returns>sann om id existerar och annars falsk</returns>
		private bool TestaAttIDExistera(int id)
		{
			bool existera = false;

			//Letar genom alla användare i användarlistan över alla användare
			foreach (Användare användare in administrationApplikation.AnvändarLista)
			{
				//Om id redan finns, sätts existera till sann
				if (id == användare.ID) existera = true;
			}

			return existera;
		}

		/// <summary>
		/// TestaAttNamnExistera testar om någon användare har angiven namn.
		/// </summary>
		/// <param name="namn">namn av en användare</param>
		/// <returns>sann om namnet existerar och annars falsk</returns>
		private bool TestaOmNamnExistera(string namn)
		{
			bool existera = false;

			//Letar genom alla användare i användarlistan över alla användare
			foreach (Användare användare in administrationApplikation.AnvändarLista)
			{
				//Om namnet redan finns, sätts existera till sann
				if (namn.Equals(användare.Användarnamn)) existera = true;
			}

			return existera;
		}

		/// <summary>
		/// TestaOmSammaNamnExistera testar om en annan användare har samma
		/// namn då alla namn ska vara unikt.
		/// </summary>
		/// <param name="id">id av en användare</param>
		/// <param name="namn">namn av en användare</param>
		/// <returns>sann om samma namn existerar och annars falsk</returns>
		private bool TestaOmSammaNamnExistera(int id, string namn)
		{
			bool existera = false;

			//Letar genom alla användare i användarlistan över alla användare
			foreach (Användare användare in administrationApplikation.AnvändarLista)
			{
				//Om namnet hittas
				if (namn.Equals(användare.Användarnamn))
				{
					//Om namnet är inte till första användare, finns det en
					//annan som också har namnet.  Existera sätts till sann
					if (!id.Equals(användare.ID))
						existera = true;
				}
			}

			return existera;
		}

		/// <summary>
		/// RengörInput tar bort oönskade karaktärer från inmatningen så man inte
		/// får en situation som Farg = "blå;Drop Table Produkter;" när man skickar
		/// till databasen.  Inloggade anställda kommer att använda programmet så
		/// säkerheten i administationsprogrammet behöver inte vara äverdriven men
		/// det här är bara för minimal säkerhet.
		/// *Ett bättre sätt skulle vara att encode datan innan det skrivs till
		/// databasen. Om tiden tillåter skulle det vara en bra idé men skulle
		/// kräver ändringar i webbsidan också.
		/// </summary>
		/// <param name="input">angiven sträng som ska rensas</param>
		/// <returns>en rensad sträng</returns>
		private string RengörInput(string input)
		{
			//Tar bort ogiltiga karaktärer 
			//[^\w\-+*/=£$.,!?:%'½&()#@\\d] matchar vilket karaktär som helst som är
			//inte en bokstav, ett nummer, ett matematiskt tecken, en pund eller dollar
			//symbol, en punkt, en utrops tecken, en frågatecken, en colon,
			//en procent symbol, en apostrof, en halv symbol, en och symbol, cirkel
			//parenteser, en # symbol, en @ symbol, en \ symbol, eller blanksteg.
			//(allt annat blir ogiltig)
			try
			{
				return Regex.Replace(input, @"[^\w\-+*/=£$.!?:%'½&()#@\s+\\d]", "",
									 RegexOptions.None, TimeSpan.FromSeconds(1.5));
			}
			//Ifall det tar för mycket tid har något gått fel.  Returnera en
			//tom sträng istället.
			catch (RegexMatchTimeoutException)
			{
				return String.Empty;
			}
		}

		//Encode och Decode används inte.  Det är bara ett annat sätt vi kunde göra det
		//och undvik att ha lagt till System.Web.Helpers till projektet.
		/*
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
		}*/

		/// <summary>
		/// initiatiseraComboBox initialisera comboboxen för att visa 
		/// nuvarande sidan. Från början sätt det till index 1 (inte ny).
		/// </summary>
		private void initiatiseraComboBox()
		{
			cboxAnvändareBox.Items.Clear();

			//Lägg till "Ny" för nya produkter
			cboxAnvändareBox.Items.Add("Ny");

			//För varje användare som finns i användarlistan för sidan
			//lägg till namnet i användare comboboxen
			foreach (Användare användare in användarSidaLista)
			{
				cboxAnvändareBox.Items.Add(användare.Användarnamn);
			}

			//Sätt default användare (vid laddning) till index 0
			cboxAnvändareBox.SelectedIndex = 1;
		}

		/// <summary>
		/// btnFörsta_Click visar första sidan (x antal användare) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnFörsta)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnFörsta_Click(object sender, EventArgs e)
		{
			//Sätt sidan till första sidan och hämtar användare för den
			sida = 1;
			användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida);

			//sätt comboboxen till det nya innehåll
			initiatiseraComboBox();

			//Om det finns fler än 1 sida stäng av tillbaka knappen och sätt på
			//nästa knappen
			if (administrationApplikation.TotallaSidorAnvändare > 1)
			{
				btnNästa.Enabled = true;
				btnTillbaka.Enabled = false;
			}
		}

		/// <summary>
		/// btnSista_Click visar sista sidan (x antal användare) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnSista)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnSista_Click(object sender, EventArgs e)
		{
			//Sätt sidan till sista sidan och hämtar produkter för den
			sida = administrationApplikation.TotallaSidorAnvändare;
			användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida);

			//sätt comboboxen till det nya innehåll
			initiatiseraComboBox();

			//Om det finns fler än 1 sida stäng av nästa knappen och sätt på
			//tillbaka knappen
			if (administrationApplikation.TotallaSidorAnvändare > 1)
			{
				btnNästa.Enabled = false;
				btnTillbaka.Enabled = true;
			}
		}

		/// <summary>
		/// btnTillbaka_Click visar förre sidan (x antal användare) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnTillbaka)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnTillbaka_Click(object sender, EventArgs e)
		{
			//Om det är inte första sidan ta bort en från sida och hämta
			//användare för den nya sidan.  Nästa knappen kan användas nu.
			if (sida > 1)
			{
				sida--;
				användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida);
			}

			//sätt comboboxen till det nya innehåll
			initiatiseraComboBox();

			//Om det är nu första sidan stäng av tillbaka knappen
			if (sida == 1)
			{
				btnTillbaka.Enabled = false;
			}
		}

		/// <summary>
		/// btnNästa_Click visar nästa sida (x antal användare) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnNästa)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnNästa_Click(object sender, EventArgs e)
		{
			//Om det är inte sista sidan öka sida med en och hämta produkter
			//för den nya sian. Tillbaka knappen kan användas nu.
			if (sida < administrationApplikation.TotallaSidorAnvändare)
			{
				sida++;
				användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida);
			}

			//sätt comboboxen till det nya innehåll
			initiatiseraComboBox();

			//Om det är nu sista sidan stäng av nästa knappen
			if (sida == administrationApplikation.TotallaSidorAnvändare)
			{
				btnNästa.Enabled = false;
				btnTillbaka.Enabled = true;
			}
		}

		/// <summary>
		/// checkNyLösenord_CheckedChanged aktivera lösenordsfältet för
		/// ändringar
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (checkNyLösenord)</param>
		/// <param name="e">argumenten till eventet</param>
		private void checkNyLösenord_CheckedChanged(object sender, EventArgs e)
		{
			//Aktivera om inbokade
			if (checkNyLösenord.Checked)
			{
				txtLösenord.Text = "";
				txtLösenord.BackColor = Color.White;
				txtLösenord.Enabled = true;
			}
			//Annars stäng av det
			else
			{
				txtLösenord.Text = "";
				txtLösenord.BackColor = Color.Gray;
				txtLösenord.Enabled = false;
			}
		}
		
	}
}
