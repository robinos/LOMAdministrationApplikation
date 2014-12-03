using System;
using System.Collections.Generic; //Dictionary klass
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOMAdministrationApplikation.Models;
using System.Text.RegularExpressions;
using System.Globalization;

namespace LOMAdministrationApplikation.Views
{
	/// <summary>
	/// (Vy)
	/// 
	/// ProduktForm har all kod för att hantera produkt input och skicka vidare
	/// till AdministrationApplikation för hantering.
	/// 
	/// -Konstruktör-
	/// ProduktForm - sätter referensen till AdministrationApplikation kontrollern
	///		och fyller comboboxen med datan för första sidan
	/// 
	/// -Metoder-
	/// btnNy_Click - "Ny" knappen tömmer fälterna och sätter produkten till "Ny"
	/// btnTaBort_Click - "TaBort" knappen används för att ta bort en befintlig
	///		produkt från databasen
	///	btnSpara_Click - "Spara" knappen används antingen för att lägga till en
	///		ny produkt eller för att ändra en benfintlig	
	/// cboxProduktBox_SelectedIndexChanged - Vid ändring i produkt comboboxen ändras
	///		även fälterna för att visa produktinformation
	/// SättProdukt - Hjälpmetod för att sätta fälterna till en viss produkts
	///		information
	/// Tömma - Tömmer alla fälterna (eller sätter till default värden) 
	/// TestaAttIDExistera - Testar om en ID redan existerar i listan över alla
	///		produkter / databas
	/// TestaAttNamnExistera - Testar om ett namn redan existerar i listan över
	///		alla produkter / databas
	/// TestaAttSammaNamnExistera - Testar om samma namn redan existerar för någon
	///		annan produkt i listan över alla produkter / databas
	///	RengörInput - städa input från användaren för lite säkerhet
	///	initiatiseraKategoriComboBox - initialisera kategori comboboxen med alla
	///		kategorier
	/// initiatiseraProduktComboBox - initialisera comboboxen för att visa nuvarande
	///		sidan
	/// btnFörsta_Click - Visar första sidan (x antal produkter) i comboboxen
	/// btnSista_Click - Visar sista sidan (x antal produkter) i comboboxen
	/// btnTillbaka_Click - Visar föregående sida (x antal produkter) i comboboxen
	/// btnNästa_Click - Visar nästa sida (x antal produkter) i comboboxen
	/// cboxKategori_SelectedIndexChanged - sätter valdKategori till valet
	///		från comboboxen och filterar och hämtar produktlistan
	/// txtSök_KeyPress - filtrerar produktlistan efter varje bokstav
	/// HanteraProdukter - filtrerar produktlistan, hämtar listan för nuvarande
	///		sidan och fyller comboboxen
	/// TotallaResultatSidor - räknar ut hur många sidor den filtrerade listan är
	/// 
	/// Version: 0.4
	/// 2014-12-01
	/// Grupp 2
	/// </summary>
	public partial class ProduktForm : Form
	{
		//instansvariabler
		//Referens till ProduktApplikationen (kontroller)
		private AdministrationApplikation administrationApplikation;
		//Produkten som är aktiv i produkt comboboxen
		private string valdProduktnamn = "";
		//Kategorin som är aktiv i kategori comboxen
		private string valdKategori = "";
		//Filtersträngen som används just nu
		private string filterSträng = "";
		//nuvarande sida (börjar på sidan 1)
		private int sida = 1;
		//totall produktlistan efter filtrering (utan sidor)
		private List<Produkt> produktFiltreradeLista;
		//produktlistan för nuvarande sida och filtrering
		private List<Produkt> produktSidaLista;

		/// <summary>
		/// Konstruktör ProduktForm tar emot en referens till
		/// AdministrationApplikation kontrollern, hämtar första combobox sidan
		/// av produkter och initialisera comboboxen.  Om där finns fler än en
		/// sida aktiveras nästa knappen.
		/// </summary>
		/// <param name="administrationApplikation">referensen till
		///		AdministrationApplikation kontrollern</param>
		public ProduktForm(AdministrationApplikation administrationApplikation)
		{
			//Initialisera formen
			InitializeComponent();

			//Sätt referensen till AdministrationApplikation objektet
			this.administrationApplikation = administrationApplikation;

			//Från början är filtrerade listan samma som alla produkter
			produktFiltreradeLista = administrationApplikation.ProduktLista;

			//hämta första sidan
			produktSidaLista = administrationApplikation.HämtaSidaProdukter(sida, administrationApplikation.ProduktLista);

			//initialiser innehållet i kategori comboboxen
			initiatiseraKategoriComboBox();

			//initialiser innehållet i produkt comboboxen
			initiatiseraProduktComboBox();

			//Om där finns fler än en sida aktiveras nästa knappen
			if (administrationApplikation.TotallaSidorProdukter > 1)
			{
				btnNästa.Enabled = true;
				btnTillbaka.Enabled = false;
			}
		}

		/// <summary>
		/// btnNy_Click bara anroper tömma för att tömmer alla fält och
		/// sätt det till en "Ny" produkt i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnTaBort)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnNy_Click(object sender, EventArgs e)
		{
			Tömma();
		}

		/// <summary>
		/// btnTaBort_Click används för att ta bort en befintlig produkt.
		/// Metoden testar att produkten redan existerar innan borttagning.
		/// Om det lyckas tas bort namnet och fälten töms då sidan laddas om.
		/// Vid existerande ID meddelas användaren och ingenting händer.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnTaBort)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnTaBort_Click(object sender, EventArgs e)
		{
			string namn = RengörInput(txtNamn.Text);
			string ID = RengörInput(txtID.Text);

			//Om ID redan existerar
			if (TestaAttIDExistera(ID))
			{
				//Om det lyckas med att ta bort produkten, tar den även
				//bort från comboxen och tömma fälterna
				if (administrationApplikation.TaBortProdukt(ID))
				{
					produktSidaLista = administrationApplikation.HämtaSidaProdukter(sida, administrationApplikation.ProduktLista);
					initiatiseraProduktComboBox();
					//cboxProduktBox.Items.Remove(namn);
					Tömma();
				}
				//annars något gick snett
			}
			else
				MessageBox.Show("ID finns inte!");
		}

		/// <summary>
		/// btnSpara_Click används för att ändra en befintlig produkt ELLER
		/// för att lägga till en produkt vid "Ny".
		/// Metoden testar att produkten redan existerar vid ändring eller
		/// att den inte redan existerar vid ny insättning.
		/// Om en ID inte existerar vid ändring eller existerar vid tilläggning
		/// meddelas användaren och ingening händer.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnSpara)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnSpara_Click(object sender, EventArgs e)
		{
			//Skapa en ny produkt att fylla från fälterna
			Produkt produkt = new Produkt();

			//ID och Namn kommer att användas för flera testar
			//RengörInput används för att ta bort kod som kan vara skadlig
			//vid insättning till databasen
			string ID = RengörInput(txtID.Text);
			string Namn = RengörInput(txtNamn.Text);

			//Pris måste tar bort PreFix (kr), byta ',' mot '.', och tar bort tusantalstecknet
			string pris = txtPris.Text.Replace((txtPris.PreFix != "") ? txtPris.PreFix : " ", String.Empty)
									  .Replace((txtPris.ThousandsSeparator.ToString() != "") ? txtPris.ThousandsSeparator.ToString() : " ", String.Empty)
									  .Replace((txtPris.DecimalsSeparator.ToString() != "") ? txtPris.DecimalsSeparator.ToString() : ",", ".").Trim();

			//fyller produkter med informationen (med rengöring för strängar)
			produkt.ID = ID;
			produkt.Namn = Namn;
			produkt.Pris = Decimal.Parse(pris, CultureInfo.InvariantCulture);
			produkt.Typ = RengörInput(txtTyp.Text);
			produkt.Färg = RengörInput(txtFarg.Text);
			produkt.Bildfilnamn = RengörInput(txtBildfil.Text);
			produkt.Ritningsfilnamn = RengörInput(txtRitningsfil.Text);
			produkt.RefID = RengörInput(txtRefID.Text);
			produkt.Beskrivning = RengörInput(txtBeskrivning.Text);
			produkt.Monteringsbeskrivning = RengörInput(txtMontering.Text);

			//Lyckades är om operationen lyckades, och fås från Administration
			//Applikation sedan
			bool lyckades = false;

			if (cboxProduktBox.SelectedIndex == 0) //Ny produkt
			{
				//Om id inte redan existerar
				if (!TestaAttIDExistera(ID))
				{
					//Om namnet inte redan existera, kör tillläggning
					if (!TestaAttNamnExistera(Namn))
						lyckades = administrationApplikation.LäggTillProdukt(produkt);
					else
						MessageBox.Show("Namn existerar redan!");
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
					if (!TestaAttSammaNamnExistera(ID, Namn))
						lyckades = administrationApplikation.UppdateraProdukt(produkt);
				}
				else
					MessageBox.Show("ID finns inte!");
			}

			//Om det lyckades (sann tillbaka från Administration Applikation)
			if (lyckades)
			{
				//hämta nuvarande sida och initialisera comboboxen med det
				produktSidaLista = administrationApplikation.HämtaSidaProdukter(sida, administrationApplikation.ProduktLista);
				initiatiseraProduktComboBox();
				//Fälterna ändras vid behov
				SättProdukt(produkt);
			}
			//annars något gick snett
		}

		/// <summary>
		/// cboxProduktBox_SelectedIndexChanged händer när comboboxen sätts
		/// till ett nytt värde. Det blir antigen "Ny" där alla fält tömms
		/// eller en befintligprodukt där fälten fylls från produkten.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (cboxProduktBox)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void cboxProduktBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			valdProduktnamn = cboxProduktBox.Items[cboxProduktBox.SelectedIndex].ToString();

			//Om comboxen är på "Ny", tömma fältarna
			if (valdProduktnamn.Equals("Ny"))
			{
				Tömma();
			}
			else
			{
				//Söker efter namnet från comboboxen i produktsamlingen.  Detta gör att
 				//namn måste vara unik.
				foreach (Produkt produkt in produktSidaLista)
				{
					//Om namnet i comboBoxen är samma som produkten sätts fälterna
					if (produkt.Namn.Equals(valdProduktnamn))
					{
						//pris ändras från '.' till ',' för svenska priser och
						//formatteras av currencyTextBox
						string pris = produkt.Pris.ToString().Replace(".", txtPris.DecimalsSeparator.ToString());
						pris = txtPris.formatText(pris);

						//fälterna fylls
						lblIndex.Text = produkt.ID.ToString();
						txtID.Text = produkt.ID.ToString();
						txtNamn.Text = produkt.Namn;
						txtPris.Text = pris;
						txtTyp.Text = produkt.Typ;
						txtFarg.Text = produkt.Färg;
						txtBildfil.Text = produkt.Bildfilnamn;
						txtRitningsfil.Text = produkt.Ritningsfilnamn;
						txtRefID.Text = produkt.RefID.ToString();
						txtBeskrivning.Text = produkt.Beskrivning.ToString();
						txtMontering.Text = produkt.Monteringsbeskrivning;
					}
				}
			}
		}

		/// <summary>
		/// SättProdukt sätter fälterna till produktens värden. Den även
		/// gör den till den produkt som visas just nu i comboboxen. 
		/// </summary>
		/// <param name="produkt">produkten som ska visas</param>
		private void SättProdukt(Produkt produkt)
		{
			//Söker efter namnet från produkten.  Detta gör att namn måste
			//vara unik.
			for (int i = 0; i < cboxProduktBox.Items.Count;i++)
			{
				//Om namnet är samma som den i comboboxen, sätt fälterna
				//efter dens variabler
				if (produkt.Namn.Equals(cboxProduktBox.Items[i].ToString()))
				{
					lblIndex.Text = produkt.ID.ToString();
					txtID.Text = produkt.ID.ToString();
					txtNamn.Text = produkt.Namn;
					txtPris.Text = produkt.Pris.ToString();
					txtTyp.Text = produkt.Typ;
					txtFarg.Text = produkt.Färg;
					txtBildfil.Text = produkt.Bildfilnamn;
					txtRitningsfil.Text = produkt.Ritningsfilnamn;
					txtRefID.Text = produkt.RefID.ToString();
					txtBeskrivning.Text = produkt.Beskrivning.ToString();
					txtMontering.Text = produkt.Monteringsbeskrivning;

					//sätt nuvarande produktnamn till produkt namnet
					valdProduktnamn = produkt.Namn;
					cboxProduktBox.SelectedIndex = i;
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
			cboxProduktBox.SelectedIndex = 0; //Ny
			txtID.Text = "00000";
			txtNamn.Text = "";
			txtPris.Text = "0,00";
			txtTyp.Text = "";
			txtFarg.Text = "";
			txtBildfil.Text = "";
			txtRitningsfil.Text = "";
			txtRefID.Text = "00000";
			txtBeskrivning.Text = "";
			txtMontering.Text = "";
		}

		/// <summary>
		/// TestaAttIDExistera testar om någon produkt har angiven id.
		/// </summary>
		/// <param name="id">id av en produkt</param>
		/// <returns>sann om id existerar och annars falsk</returns>
		private bool TestaAttIDExistera(string id)
		{
			bool existera = false;

			//Letar genom alla produkter från databasen
			foreach (Produkt produkt in administrationApplikation.ProduktLista)
			{
				//Om id redan finns, sätts existera till sann
				if (id.Equals(produkt.ID)) existera = true;
			}

			return existera;
		}

		/// <summary>
		/// TestaAttNamnExistera testar om någon produkt har angiven namn.
		/// </summary>
		/// <param name="namn">namn av en produkt</param>
		/// <returns>sann om namnet existerar och annars falsk</returns>
		private bool TestaAttNamnExistera(string namn)
		{
			bool existera = false;

			//Letar genom alla produkter från databasen
			foreach (Produkt produkt in administrationApplikation.ProduktLista)
			{
				//Om namnet redan finns sätts existera till sann
				if (namn.Equals(produkt.Namn)) existera = true;
			}

			return existera;
		}

		/// <summary>
		/// TestaAttSammaNamnExistera testar om en annan produkt har samma
		/// namn då alla namn ska vara unikt.
		/// </summary>
		/// <param name="id">id av en produkt</param>
		/// <param name="namn">namn av en produkt</param>
		/// <returns>sann om samma namn existerar och annars falsk</returns>
		private bool TestaAttSammaNamnExistera(string id, string namn)
		{
			bool existera = false;

			//Letar genom alla produkter från databasen
			foreach (Produkt produkt in administrationApplikation.ProduktLista)
			{
				//Om namnet hittas
				if (namn.Equals(produkt.Namn))
				{
					//Om namnet är inte till första produkten, finns det en
					//annan som också har namnet.  Existera sätts till sann
					if (!id.Equals(produkt.ID))
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

		/// <summary>
		/// initiatiseraProduktComboBox initialisera produkt comboboxen för att
		/// visa nuvarande sidan. Från början sätt det till index 1 (inte ny).
		/// </summary>
		private void initiatiseraProduktComboBox()
		{
			//tömma comboboxen
			cboxProduktBox.Items.Clear();

			//Lägg till "Ny" för nya produkter
			cboxProduktBox.Items.Add("Ny");

			//För varje produkt som finns i användarlistan för sidan
			//lägg till namnet i produkt comboboxen
			foreach (Produkt produkt in produktSidaLista)
			{
				cboxProduktBox.Items.Add(produkt.Namn);
			}

			//Sätt default produkten (vid laddning) till index 1 om den finns
			if(cboxProduktBox.Items.Count > 1)
				cboxProduktBox.SelectedIndex = 1;
			else
				cboxProduktBox.SelectedIndex = 0;
		}

		/// <summary>
		/// initiatiseraKategoriComboBox initialisera kategori comboboxen 
		/// med alla kategorier. Från början sätt det till "".
		/// </summary>
		private void initiatiseraKategoriComboBox()
		{
			//tömma comboboxen
			cboxProduktBox.Items.Clear();

			//Lägg till "Ny" för nya produkter
			cboxKategori.Items.Add("");

			List<string> typLista = administrationApplikation.HämtaProduktKategoriLista(administrationApplikation.ProduktLista);

			//För varje produkt som finns i användarlistan för sidan
			//lägg till namnet i produkt comboboxen
			foreach (string kategori in typLista)
			{
				cboxKategori.Items.Add(kategori);
			}

			//Sätt default produkten (vid laddning) till index 1
			cboxKategori.SelectedIndex = 0;
		}

		/// <summary>
		/// btnFörsta_Click visar första sidan (x antal produkter) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnFörsta)</param>
		/// <param name="e">argumenten till eventet</param>
		private void btnFörsta_Click(object sender, EventArgs e)
		{
			int totallaSidor = TotallaResultatSidor();

			//Sätt sidan till första sidan och hämtar produkter för den
			sida = 1;
			HanteraProdukter();

			//sätt comboboxen till det nya innehåll
			initiatiseraProduktComboBox();

			//Om det finns fler än 1 sida stäng av tillbaka knappen och sätt på
			//nästa knappen
			if (totallaSidor > 1)
			{
				btnNästa.Enabled = true;
				btnTillbaka.Enabled = false;
			}
		}

		/// <summary>
		/// btnSista_Click visar sista sidan (x antal produkter) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnSista)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnSista_Click(object sender, EventArgs e)
		{
			int totallaSidor = TotallaResultatSidor();

			//Sätt sidan till sista sidan och hämtar produkter för den
			sida = totallaSidor;
			HanteraProdukter();

			//sätt comboboxen till det nya innehåll
			initiatiseraProduktComboBox();

			//Om det finns fler än 1 sida stäng av nästa knappen och sätt på
			//tillbaka knappen
			if (totallaSidor > 1)
			{
				btnNästa.Enabled = false;
				btnTillbaka.Enabled = true;
			}
		}

		/// <summary>
		/// btnTillbaka_Click visar förre sidan (x antal produkter) i comboboxen.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (btnTillbaka)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void btnTillbaka_Click(object sender, EventArgs e)
		{
			//Om det är inte första sidan ta bort en från sida och hämta
			//produkter för den nya sidan.  Nästa knappen kan användas nu.
			if (sida > 1)
			{
				sida--;
				HanteraProdukter();

				btnNästa.Enabled = true;
			}

			//sätt comboboxen till det nya innehåll
			initiatiseraProduktComboBox();

			//Om det är nu första sidan stäng av tillbaka knappen
			if (sida == 1)
			{
				btnTillbaka.Enabled = false;
			}
		}

		/// <summary>
		/// btnNästa_Click visar nästa sida (x antal produkter) i comboboxen.
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
				HanteraProdukter();

				btnTillbaka.Enabled = true;
			}

			//sätt comboboxen till det nya innehåll
			initiatiseraProduktComboBox();

			//Om det är nu sista sidan stäng av nästa knappen
			if (sida == totallaSidor)
			{
				btnNästa.Enabled = false;
			}
		}

		/// <summary>
		/// cboxKategori_SelectedIndexChanged sätter valdKategori till valet
		/// från comboboxen och filterar och hämtar produktlistan därefter.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (cboxKategori)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void cboxKategori_SelectedIndexChanged(object sender, EventArgs e)
		{
			valdKategori = cboxKategori.Items[cboxKategori.SelectedIndex].ToString();
			sida = 1; //sätt sidan till 1
			HanteraProdukter();
		}

		/// <summary>
		/// txtSök_KeyPress filtrerar produktlistan efter varje bokstav
		/// då användaren skriver in en sökSträng.
		/// </summary>
		/// <param name="sender">objekten som skickar eventet (txtSök)</param>
		/// <param name="e">argumenten till eventet</param> 
		private void txtSök_KeyPress(object sender, KeyPressEventArgs e)
		{
			filterSträng = txtSök.Text;
			sida = 1; //sätt sidan till 1
			HanteraProdukter();
		}

		/// <summary>
		/// HanteraProdukter filtrerar produktlistan, hämtar listan för
		/// nuvarande sidan och fyller comboboxen.  Beroende på hur
		/// många sidor den filtrerade listan är kan nästa och tillbaka
		/// knapparna aktiveras.
		/// </summary>
		private void HanteraProdukter()
		{
			//Filtrera listan
			produktFiltreradeLista = administrationApplikation.FiltreraProduktLista(administrationApplikation.ProduktLista, filterSträng, valdKategori);
			//Hämta nuvarande sida av filtrerad listan
			produktSidaLista = administrationApplikation.HämtaSidaProdukter(sida, produktFiltreradeLista);
			//Fyll produkt comboboxen
			initiatiseraProduktComboBox();

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
		/// av ProdukterPerSida i AdministrationApplikation.
		/// </summary>
		/// <returns>en integer med hur många sidor filtrerade listan är på</returns>
		private int TotallaResultatSidor()
		{
			int totallaSidor = produktFiltreradeLista.Count / administrationApplikation.ProdukterPerSida;

			//Om där finns rester lägg till en sida
			if (produktFiltreradeLista.Count % administrationApplikation.ProdukterPerSida > 0)
				totallaSidor += 1;

			return totallaSidor;
		}
	}
}
