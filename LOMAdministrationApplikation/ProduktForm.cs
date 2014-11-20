using System;
using System.Collections.Generic; //Dictionary klass
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOMAdministrationApplikation
{
	/*
	 * (Vy)
	 * ProduktApplikationForm har all kod för att hantera input och skicka
	 * vidare till ProduktApplikation för hantering.
	 * 
	 * Comboboxen blir ett problem med stora mängder produkter.  Det kan vara
	 * bra om istället den bara ta högst 10 värden åt gången och man tillägga
	 * en tillbaka och nästa knapp.
	 * 
	 * btnNy_Click - Vid tryck av "Ny"knappen tömms fälterna
	 * btnTaBort_Click - Vid tryck av "TaBort"knappen anrops ProduktApplikation för
	 *		att ta bort produkten
	 * btnAndra_Click - Vid tryck av "Andra"knappen anrops ProduktApplikation för
	 *		att antingen lägg till en ny produkt, eller ändra en benfintlig
	 * cboxProduktBox_SelectedIndexChanged - Vid ändring i produkt comboboxen, ändras
	 *		även fälterna för att visa produktinformation
	 * SättProdukt - Hjälpmetod för att sätta fälterna till en viss produktsinformation 
	 * Tömma - Tömmer alla fälterna (eller sätter till default värden) 
	 * TestaAttIDExistera - Testar om en ID redan existerar i produktsamlingen
	 * TestaAttNamnExistera - Testar om ett namn redan existerar i produktsamlingen
	 * TestaAttSammaNamnExistera - Testar om samma namn redan existerar för en annan
	 *		produkt
	 * RengörInput - städa input från användaren för säkerhet
	 * 
	 * Version: 1.0
	 * 2014-10-27
	 * Robin Osborne
	 */
	public partial class ProduktForm : Form
	{
		//instansvariabler
		//Referens till ProduktApplikationen (Kontroller)
		private AdministrationApplikation produktApplikation;
		//Behållare för en samling av Produkt värdena (utan nycklar) från en Dictionary
		private Dictionary<string, Produkt>.ValueCollection produktSamling;
		//Den produkt som är aktiv i produkt comboboxen
		private string selectedProduktnamn = "";

		/*
		 * Constructor som tar en inparameter, som är en referens till
		 * en ProduktApplikation objekt.
		 */
		public ProduktForm(AdministrationApplikation produktApplikation)
		{
			InitializeComponent();

			//Sätt produktApplikation objekt
			this.produktApplikation = produktApplikation;

			//Sätt samling till värdena av Dictionary Produkter från ProduktApplikation
			produktSamling = produktApplikation.Produkter.Values;

			//Lägg till "Ny" för nya produkter
			cboxProduktBox.Items.Add("Ny");

			//För varje produkt som finns i samlingen, lägg till namnet i
			//produkt comboboxen
			foreach (Produkt produkt in produktSamling)
			{
				cboxProduktBox.Items.Add(produkt.Namn);
			}

			//Sätt default produkten (om startup) till index 0
			cboxProduktBox.SelectedIndex = 0;
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
			string namn = txtNamn.Text;
			string ID = txtID.Text;

			//Om ID redan existerar
			if (TestaAttIDExistera(ID))
			{
				//Om det lyckas med att ta bort produkten, tar den även
				//bort från comboxen och tömma fälterna
				if (produktApplikation.TaBortProdukt(ID))
				{
					cboxProduktBox.Items.Remove(namn);
					Tömma();
				}
				//annars något gick snett
			}
			else
				MessageBox.Show("ID finns inte!");
		}

		/*
		 * Sparaknappen används för att ändra en befintlig produkt ELLER
		 * lägg till en selectedProdukt är "Ny".
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
			Produkt produkt = new Produkt();

			//ID och Namn kommer att användas för flera testar
			//RengörInput används för att ta bort kod som kan vara skadlig
			//vid insättning till databasen
			string ID = RengörInput(txtID.Text);
			string Namn = RengörInput(txtNamn.Text);

			//Pris måste tar bort PreFix (kr), byta ',' mot '.', och tar bort tusantalstecknet
			string pris = txtPris.Text.Replace((txtPris.PreFix != "") ? txtPris.PreFix : " ", String.Empty)
									  .Replace((txtPris.ThousandsSeparator.ToString() != "") ? txtPris.ThousandsSeparator.ToString() : " ", String.Empty)
									  .Replace((txtPris.DecimalsSeparator.ToString() != "") ? txtPris.DecimalsSeparator.ToString() : " ", ".").Trim();

			//fyller produkter med informationen (med rengöring för strängar)
			produkt.ID = ID;
			produkt.Namn = Namn;
			produkt.Pris = decimal.Parse(pris);
			produkt.Typ = RengörInput(txtTyp.Text);
			produkt.Farg = RengörInput(txtFarg.Text);
			produkt.Bildfilnamn = RengörInput(txtBildfil.Text);
			produkt.Ritningsfilnamn = RengörInput(txtRitningsfil.Text);
			produkt.RefID = RengörInput(txtRefID.Text);
			produkt.Beskrivning = RengörInput(txtBeskrivning.Text);
			produkt.Monteringsbeskrivning = RengörInput(txtMontering.Text);

			//Lyckades är om operationen lyckades, och fås från Produkt Applikation sedan
			bool lyckades = false;

			if (cboxProduktBox.SelectedIndex == 0) //Ny produkt
			{
				//Om id inte redan existerar
				if (!TestaAttIDExistera(ID))
				{
					//Om namnet inte redan existera, kör tillläggning
					if (!TestaAttNamnExistera(Namn))
						lyckades = produktApplikation.LäggTillProdukt(produkt);
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
						lyckades = produktApplikation.UppdateraProdukt(produkt);
				}
				else
					MessageBox.Show("ID finns inte!");
			}

			//Om det lyckades (sann tillbaka från Produkt Applikation)
			if (lyckades)
			{
				//Ifall namnet har ändrats, tas den bort och läggs till igen
				cboxProduktBox.Items.Remove(produkt.Namn);
				cboxProduktBox.Items.Add(produkt.Namn);
				//Fälterna ändras vid behov
				SättProdukt(produkt);
			}
			//annars något gick snett
		}

		/*
		 * Namnet är viktigt för kompilatorn.  SelectedIndexChanged är en event
		 * som händer då något nytt väls i produkt comboboxen.
		 * 
		 * Det blir antigen "Ny" där alla fält tömms, eller en befintligprodukt där
		 * fälten fylls från produkten.
		 * 
		 * in - sender innehåller objektreferens till produkt comboboxen,
		 *		e inehåller argument för event combobox index ändring
		 */
		private void cboxProduktBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			selectedProduktnamn = cboxProduktBox.Items[cboxProduktBox.SelectedIndex].ToString();

			//Om comboxen är på "Ny", tömma fältarna
			if (selectedProduktnamn.Equals("Ny"))
			{
				Tömma();
			}
			else
			{
				//Söker efter namnet från comboboxen i produktsamlingen.  Detta gör att
 				//namn måste vara unik.
				foreach (Produkt produkt in produktSamling)
				{
					//Om namnet i comboBoxen är samma som produkten sätts fälterna
					if (produkt.Namn.Equals(selectedProduktnamn))
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
						txtFarg.Text = produkt.Farg;
						txtBildfil.Text = produkt.Bildfilnamn;
						txtRitningsfil.Text = produkt.Ritningsfilnamn;
						txtRefID.Text = produkt.RefID.ToString();
						txtBeskrivning.Text = produkt.Beskrivning.ToString();
						txtMontering.Text = produkt.Monteringsbeskrivning;
					}
				}
			}
		}

		/*
		 * SättProdukt sätter fälterna till produktens värden.  Den även gör den
		 * till den produkt som visas just nu (och sätter produkt comboboxen till
		 * produkten).
		 * 
		 * in - produkten som innehåller information som ska visas
		 */
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
					txtFarg.Text = produkt.Farg;
					txtBildfil.Text = produkt.Bildfilnamn;
					txtRitningsfil.Text = produkt.Ritningsfilnamn;
					txtRefID.Text = produkt.RefID.ToString();
					txtBeskrivning.Text = produkt.Beskrivning.ToString();
					txtMontering.Text = produkt.Monteringsbeskrivning;

					//sätt nuvarande produktnamn till produkt namnet
					selectedProduktnamn = produkt.Namn;
					cboxProduktBox.SelectedIndex = i;
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

		//Jag kan inte ta bort det här utan att få errors...jävla program
		private void ProduktApplikationForm_Load(object sender, EventArgs e)
		{

		}

		/*
		 * Testar att en ID existera i produktsamlingen.
		 * 
		 * in - id(sträng) av produkten som man vill testa om den redan existera
		 * ut - sann om den existera, annars falsk
		 */
		private bool TestaAttIDExistera(string id)
		{
			bool existera = false;

			//Letar genom alla produkter i samlingen
			foreach (Produkt produkt in produktSamling)
			{
				//Om id redan finns, sätts existera till sann
				if (id.Equals(produkt.ID)) existera = true;
			}

			return existera;
		}

		/*
		 * Testar att en namn existera i produktsamlingen.
		 * 
		 * in - namn(sträng) av produkten som man vill testa om den redan existera
		 * ut - sann om den existera, annars falsk
		 */
		private bool TestaAttNamnExistera(string namn)
		{
			bool existera = false;

			//Letar genom alla produkter i samlingen
			foreach (Produkt produkt in produktSamling)
			{
				//Om namnet redan finns, sätts existera till sann
				if (namn.Equals(produkt.Namn)) existera = true;
			}

			return existera;
		}


		/*
		 * Testar om en annan produkt (annan id) har samma namn.
		 * 
		 * in - id(sträng) av produkten som man vill testa, namn(sträng) som
		 *		man vill testa om en annan produkt också har den
		 * ut - sann om en annan produkt har samma namn, annars falsk
		 */
		private bool TestaAttSammaNamnExistera(string id, string namn)
		{
			bool existera = false;

			//Letar genom alla produkter i samlingen
			foreach (Produkt produkt in produktSamling)
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

	}
}
