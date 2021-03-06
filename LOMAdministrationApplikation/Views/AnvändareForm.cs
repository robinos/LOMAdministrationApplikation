﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOMAdministrationApplikation.Models;
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
	/// initiatiseraComboBox - initialisera comboboxen för att visa nuvarande sidan
	/// btnFörsta_Click - Visar första sidan (x antal användare) i comboboxen
	/// btnSista_Click - Visar sista sidan (x antal användare) i comboboxen
	/// btnTillbaka_Click - Visar föregående sida (x antal användare) i comboboxen
	/// btnNästa_Click - Visar nästa sida (x antal användare) i comboboxen
	/// txtSök_KeyPress - filtrerar användarlistan efter varje bokstav
	/// HanteraProdukter - filtrerar användarlistan, hämtar listan för nuvarande
	///		sidan och fyller comboboxen
	/// TotallaResultatSidor - räknar ut hur många sidor den filtrerade listan är
	/// btnAvsluta_Click - stängar formen vid knapptryckning
	/// AnvändareForm_FormClosing - när formen stängs visar huvudformen igen
	/// 
	/// Version: 0.5
	/// 2014-12-07
	/// Grupp 2
	/// </summary> 
	public partial class AnvändareForm : Form
	{
		//instansvariabler
		//Referens till ProduktApplikationen (Kontroller)
		private AdministrationApplikation administrationApplikation;
		//Referens till HuvudApplikationenForm (vy)
		private HuvudApplikationForm huvudApplikationForm;
		//Den produkt som är aktiv i produkt comboboxen
		private string valdAnvändarnamn = "";
		//Filtersträngen som används just nu
		private string filterSträng = "";
		//högstaID av alla användare
		private int högstaID = 0;
		//nuvarande sida (börjar på sidan 1)
		private int sida = 1;
		//totall användarlistan efter filtrering (utan sidor)
		private List<Användare> användareFiltreradeLista;
		//användarlistan för nuvarande sida och filtrering 
		private List<Användare> användarSidaLista;

		/// <summary>
		/// Konstruktör AnvändareForm tar emot en referens till
		/// AdministrationApplikation kontrollern, hämtar första combobox sidan
		/// av användare och initialisera comboboxen.  Om där finns fler än en
		/// sida aktiveras nästa knappen.
		/// </summary>
		/// <param name="administrationApplikation">referensen till
		///		AdministrationApplikation kontrollern</param>
		public AnvändareForm(AdministrationApplikation administrationApplikation, HuvudApplikationForm huvudApplikationForm)
		{
			//Initialisera formen
			InitializeComponent();

			//Sätt referensen till AdministrationApplikation objektet
			this.administrationApplikation = administrationApplikation;

			//Sätt referensen till HuvudApplikationForm objektet
			this.huvudApplikationForm = huvudApplikationForm;

			//Från början är filtrerade listan samma som alla användare
			användareFiltreradeLista = administrationApplikation.AnvändarLista;

			//hämta första sidan och sätt högsta användare ID för ID skapelse sedan
			användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida, administrationApplikation.AnvändarLista);
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
			if (administrationApplikation.TestaOmAnvändareIDExistera(ID))
			{
				//Om det lyckas med att ta bort användaren, tar den även
				//bort från comboxen och tömma fälterna
				//AdministrationApplikation även räknar om nummer av sidor
				//och högsta användare id
				if (administrationApplikation.TaBortAnvändare(ID))
				{
					btnNästa.Enabled = false;
					btnTillbaka.Enabled = false;
					//Om där finns fler än en sida nu aktiveras nästa knappen
					if (administrationApplikation.TotallaSidorAnvändare > 1)
					{
						btnNästa.Enabled = true;
					}
					else
						sida = 1;

					if (sida > 1)
						btnTillbaka.Enabled = true;

					högstaID = administrationApplikation.HögstaAnvändareID;
					användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida, administrationApplikation.AnvändarLista);
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
			string användarnamn = administrationApplikation.RengörInput(txtAnvändarnamn.Text);

			//fyller användare med informationen (med rengöring för strängar)
			användare.ID = ID;
			användare.Användarnamn = användarnamn;
			användare.Roll = administrationApplikation.RengörInput(txtRoll.Text);
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
				användare.LösenordHash = Crypto.HashPassword(administrationApplikation.RengörInput(txtLösenord.Text));

				//Om id inte redan existerar
				if (!administrationApplikation.TestaOmAnvändareIDExistera(användare.ID))
				{
					//Om namnet inte redan existera, kör tillläggning
					if (!administrationApplikation.TestaOmAnvändareNamnExistera(användarnamn))
					{
						//Nummer av sidor och högsta ID räknas om av
						//AdministrationApplikation vid tilläggning
						lyckades = administrationApplikation.LäggTillAnvändare(användare);
						//tömma söktext fältet
						txtSök.Text = "";
						btnNästa.Enabled = false;
						btnTillbaka.Enabled = false;
						//Om där finns fler än en sida nu aktiveras nästa knappen
						if (administrationApplikation.TotallaSidorAnvändare > 1)
						{
							btnNästa.Enabled = true;
						}
						else
							sida = 1;

						if (sida > 1)
							btnTillbaka.Enabled = true;

						högstaID = administrationApplikation.HögstaAnvändareID;
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
					användare.LösenordHash = Crypto.HashPassword(administrationApplikation.RengörInput(txtLösenord.Text));
				//Annars använd förre detta lösenordet
				else 					
					användare.LösenordHash = administrationApplikation.HämtaAnvändareMedID(användare.ID).LösenordHash;

				//Om id redan existerar
				if (administrationApplikation.TestaOmAnvändareIDExistera(ID))
				{
					//Om namnet inte redan existera, kör updatering
					if (!administrationApplikation.TestaOmSammaAnvändareNamnExistera(ID, användarnamn))
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
				användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida, administrationApplikation.AnvändarLista);
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

			//Sätt default användare (vid laddning) till index 1 om den finns
			if (cboxAnvändareBox.Items.Count > 1)
				cboxAnvändareBox.SelectedIndex = 1;
			else
				cboxAnvändareBox.SelectedIndex = 0;
		}

		/// <summary>
		/// btnFörsta_Click visar första sidan (x antal användare) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnFörsta)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnFörsta_Click(object sender, EventArgs e)
		{
			int totallaSidor = TotallaResultatSidor();

			//Sätt sidan till första sidan och hämtar användare för den
			sida = 1;
			HanteraAnvändare();

			//sätt comboboxen till det nya innehåll
			initiatiseraComboBox();

			//Om det finns fler än 1 sida stäng av tillbaka knappen och sätt på
			//nästa knappen
			if (totallaSidor > 1)
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
			int totallaSidor = TotallaResultatSidor();

			//Sätt sidan till sista sidan och hämtar produkter för den
			sida = totallaSidor;
			HanteraAnvändare();

			//sätt comboboxen till det nya innehåll
			initiatiseraComboBox();

			//Om det finns fler än 1 sida stäng av nästa knappen och sätt på
			//tillbaka knappen
			if (totallaSidor > 1)
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
				HanteraAnvändare();

				btnNästa.Enabled = true;
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
			int totallaSidor = TotallaResultatSidor();

			//Om det är inte sista sidan öka sida med en och hämta produkter
			//för den nya sian. Tillbaka knappen kan användas nu.
			if (sida < totallaSidor)
			{
				sida++;
				HanteraAnvändare();

				btnTillbaka.Enabled = true;
			}

			//sätt comboboxen till det nya innehåll
			initiatiseraComboBox();

			//Om det är nu sista sidan stäng av nästa knappen
			if (sida == totallaSidor)
			{
				btnNästa.Enabled = false;
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

		/// <summary>
		/// txtSök_KeyPress filtrerar användarlistan efter varje bokstav
		/// då användaren skriver in en sökSträng.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (txtSök)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void txtSök_KeyPress(object sender, KeyPressEventArgs e)
		{
			filterSträng = txtSök.Text;
			sida = 1; //sätt sidan till 1
			HanteraAnvändare();
		}

		/// <summary>
		/// HanteraAnvändare filtrerar användarlistan, hämtar listan för
		/// nuvarande sidan och fyller comboboxen.  Beroende på hur
		/// många sidor den filtrerade listan är kan nästa och tillbaka
		/// knapparna aktiveras.
		/// </summary>
		private void HanteraAnvändare()
		{
			//Filtrera listan
			användareFiltreradeLista = administrationApplikation.FiltreraAnvändareLista(administrationApplikation.AnvändarLista, filterSträng);
			//Hämta nuvarande sida av filtrerad listan
			användarSidaLista = administrationApplikation.HämtaSidaAnvändare(sida, användareFiltreradeLista);
			//Fyll produkt comboboxen
			initiatiseraComboBox();

			//Får tillbaka hur många sidor filtrerade listan blev
			int totallaSidor = TotallaResultatSidor();

			//stäng av nästa och tillbaka knapparna som default
			btnNästa.Enabled = false;
			btnTillbaka.Enabled = false;

			//Om det finns fler än en sida och man är inte på sista sidan
			//tillåt nästa knappen att vara aktiverad
			if (totallaSidor > 1 && sida < totallaSidor)
			{
				btnNästa.Enabled = true;
			}

			//Om det finns fler än en sida och man är inte på första sidan
			//tillåt tillbaka knappen att vara aktiverad
			if (totallaSidor > 1 && sida > 1)
			{
				btnTillbaka.Enabled = true;
			}

		}

		/// <summary>
		/// TotallaResultatSidor räknar ut hur många sidor den filtrerade
		/// listan (filtrerade efter kategori och söksträng) är med hjälp
		/// av AnvändarePerSida i AdministrationApplikation.
		/// </summary>
		/// <returns>en integer med hur många sidor filtrerade listan är på</returns>
		private int TotallaResultatSidor()
		{
			int totallaSidor = användareFiltreradeLista.Count / administrationApplikation.AnvändarePerSida;

			//Om där finns rester lägg till en sida
			if (användareFiltreradeLista.Count % administrationApplikation.AnvändarePerSida > 0)
				totallaSidor += 1;

			return totallaSidor;
		}

		/// <summary>
		/// btnAvsluta_Click avslutar formen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnAvsluta)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnAvsluta_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// När formen stängs visar huvudapplikationsformen igen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (AnvändareForm)</param>
		/// <param name="e">argumenten till eventet</param>
		private void AnvändareForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			huvudApplikationForm.AutentiseraAnvändaren();
			huvudApplikationForm.Show();
		}
	}
}
