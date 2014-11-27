using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOMAdministrationApplikation.Controllers;
using LOMAdministrationApplikation.Models;
using LOMAdministrationApplikation.Views;
using System.Security.Principal;

namespace LOMAdministrationApplikation
{
	/// <summary>
	/// (Kontroller - Main)
	/// 
	/// AdministrationApplikation har hand om all kommunikation mellan Databas
	/// och formerna HuvudApplikationForm, AnvändareForm och ProduktForm.
	/// 
	/// 
	/// -Instansvariabler-
	/// databas - referens till Databas objekten
	/// produkter - referens till en Dictionary av alla produkter med ID som
	///		nyckel
	/// allaAnvändare - referens till en Dictionary av alla användare med
	///		Användarnamn som nyckel
	///	totallaSidorProdukter - totalla sidor av produkter (med x element per
	///		sida)
	/// totallaSidorAnvändare - totalla sidor av användare (med x element per
	///		sida)
	/// högstaAnvändareID -högsta användare ID för skapning av nya användare
	/// 
	/// 
	/// -Metoder-
	/// Main - Kör programmet (startar HuvudApplikationForm)
	/// LäsaFrånDatabas - läser in data från databasen (använder Databas
	///		klassen)
	/// LäggTillProdukt - lägger till en produkt i databasen (använder
	///		Databas klassen)
	/// TaBortProdukt - ta bort en produkt från databasen (använder Databas
	///		klassen)
	/// UppdateraProdukt - uppdatera en produkt i databasen (använder Databas
	///		klassen)
	/// LäggTillAnvändare - lägger till en användare i databasen (använder
	///		Databas klassen)
	/// TaBortAnvändare - ta bort en användare från databasen (använder
	///		Databas klassen)
	/// UppdateraAnvändare - uppdatera en användare i databasen (använder
	///		Databas klassen)
	/// AnvändarenFinnsIRoll - Testar om användaren har en specifik roll
	///		enligt Windows systemet
	/// AnvändarenÄrAdministratör - Testar om användaren är en administratör
	/// AnvändarenÄrVanligInloggadAnvändare - Testar om användaren är en vanlig
	///		inloggad användare
	/// 
	/// 
	/// Version: 0.3
	/// 2014-11-27
	/// Grupp 2
	/// </summary>
	public class AdministrationApplikation
	{
		//instansvariabler
		//Referens till Databas 
		private Databas databas = null;
		//Dictionary av produkter
		private Dictionary<string, Produkt> produkter;
		//Dictionary av användare
		private Dictionary<string, Användare> allaAnvändare;
		//För olika Produkt sidor
		private int totallaSidorProdukter = 1;
		//För olika Användare sidor
		private int totallaSidorAnvändare = 1;
		//För att skapa nya användare ID
		private int högstaAnvändareID = 0;

		/// <summary>
		/// Konstruktören för AdministrationApplikation tilldelar databas
		/// objektet och initialisera produkter och allaAnvändare att vara
		/// samma referens som de i databas klassen.
		/// </summary>
		public AdministrationApplikation()
		{
			//initialisera produktDatabas
			databas = new Databas();
			//initialiser Dictionary av produkter (samma referens som
			//produkter i produktDatabas)
			produkter = databas.Produkter;
			allaAnvändare = databas.AllaAnvändare;
		}

		/// <summary>
		/// Get och Set egenskap till Dictionary produkter
		/// </summary>
		public Dictionary<string, Produkt> Produkter
		{
			get
			{
				return produkter;
			}
			set
			{
				produkter = value;
			}
		}

		/// <summary>
		/// Get och Set egenskap till Dictionary allaAnvändare
		/// </summary>
		public Dictionary<string, Användare> AllaAnvändare
		{
			get
			{
				return allaAnvändare;
			}
			set
			{
				allaAnvändare = value;
			}
		}

		/// <summary>
		/// Get och Set egenskap för totalla sidor av produkter
		/// </summary>
		public int TotallaSidorProdukter
		{
			get
			{
				return totallaSidorProdukter;
			}
		}

		/// <summary>
		/// Get och Set egenskap för totalla sidor av användare
		/// </summary>
		public int TotallaSidorAnvändare
		{
			get
			{
				return totallaSidorAnvändare;
			}
		}

		/// <summary>
		/// Get och Set egenskap för högsta ID värde av en användare
		/// </summary>
		public int HögstaAnvändareID
		{
			get
			{
				return högstaAnvändareID;
			}
		}

		/// <summary>
		/// Main startar och kör huvudprogrammet.  Den initialiserar en instans
		/// av AdministrationApplikation och läser från databasen.  Om det lyckas
		/// startas HuvudApplikationForm (UI Menyn) med instansen av
		/// AdministrationApplikation som parameter och programmet börjar för
		/// användaren och annars stängs det ner.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//Med administrationApplikation undviker man att behöver ha statisk
			//metoder och kan skickar det som referens till HuvudApplikationForm
			AdministrationApplikation administrationApplikation = new AdministrationApplikation();

			//Läsa in data från databasen. Om det lyckas körs applikationen
			if (administrationApplikation.LäsaFrånDatabas())
			{
				//Visar Form
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//AdministrationApplikation skickas med för kontakt med metoder så
				//man behöver inte ha logiken i formena där det är svårt att unit
				//testa
				Application.Run(new HuvudApplikationForm(administrationApplikation));
			}
		}

		/// <summary>
		/// LäsaFrånDatabas läser in data från databasen med hjälp av Databas
		///		objektet och sätter Dictionary produkter och allaAnvändare
		///		till alla datan som läsas in.
		/// </summary>
		/// <returns>Sann om läsningen av både tabeller i databasen lyckades
		///		och annars falsk</returns>
		public bool LäsaFrånDatabas()
		{
			bool lyckades = false;

			//Om ProduktLasare metoden i Databas returnerar sann, lyckades
			//blir sann
			if (databas.LäsaProdukter() && databas.LäsaAnvändare())
			{
				HämtaSidaProdukter(1);
				totallaSidorProdukter = databas.TotallaSidorProdukter;
				HämtaSidaAnvändare(1);
				totallaSidorAnvändare = databas.TotallaSidorAnvändare;
				högstaAnvändareID = databas.HögstaID;
				lyckades = true;
			}
			else
			{
				MessageBox.Show("Det gick inte att läsa från en eller fler databas tabeller!");
			}

			return lyckades;
		}

		/// <summary>
		/// HämtaSidaProdukter hämtar en sida av produkter (x element) för
		/// angiven sida. 
		/// *Jag vill ändra det som returneras till en lista
		/// </summary>
		/// <param name="sida">Sidan man ska hämta produkter för</param>
		/// <returns>En Dictionary objekt med produkter med nyckel ID
		///		som sträng</returns>
		public Dictionary<string, Produkt> HämtaSidaProdukter(int sida)
		{
			produkter = databas.HämtaSidaProdukter(sida);
			return produkter;
		}

		/// <summary>
		/// HämtaSidaAnvändare hämtar en sida av användare (x element) för
		/// angiven sida. 
		/// *Jag vill ändra det som returneras till en lista
		/// </summary>
		/// <param name="sida">Sidan man ska hämta användare för</param>
		/// <returns>En Dictionary objekt med användare med nyckel
		///		användarnamn som sträng</returns>
		public Dictionary<string, Användare> HämtaSidaAnvändare(int sida)
		{
			allaAnvändare = databas.HämtaSidaAnvändare(sida);
			return allaAnvändare;
		}

		/// <summary>
		/// LäggTillProdukt lägger till en produkt till databasen med hjälp av
		/// Databas objekten.
		/// </summary>
		/// <param name="produkt">Produkt objektet som ska läggas till</param>
		/// <returns>Sann om produkten lades till och annars falsk</returns>
		public bool LäggTillProdukt(Produkt produkt)
		{
			//InsättProdukt metoden i Databas returnerar sann eller falsk
			bool lyckades = databas.InsättProdukt(produkt);

			return lyckades;
		}

		/// <summary>
		/// TaBortProdukt tar bort en produkt från databasen med hjälp av
		/// Databas objekten.
		/// </summary>
		/// <param name="produkt">Produkt objektet som ska tas bort</param>
		/// <returns>Sann om produkten har tagits bort och annars falsk</returns>
		public bool TaBortProdukt(string id)
		{
			//Delete metoden i Databas returnerar sann eller falsk
			bool success = databas.TaBortProdukt(id);

			return success;
		}

		/// <summary>
		/// UppdateraProdukt ändrar om en produkt i databasen med hjälp av
		/// Databas objekten.
		/// </summary>
		/// <param name="produkt">Produkt objektet som ska ändras</param>
		/// <returns>Sann om produkten ändrades och annars falsk</returns>
		public bool UppdateraProdukt(Produkt produkt)
		{
			//Update metoden i Databas returnerar sann eller falsk
			bool success = databas.UppdateraProdukt(produkt);

			//Databasen läsas om och Produkt sätts till den nya innehåll
			databas.LäsaProdukter();

			return success;
		}

		/// LaggTillAnvändare lägger till en användare till databasen med
		/// hjälp av Databas objekten.
		/// </summary>
		/// <param name="användare">Användare objektet som ska läggas till</param>
		/// <returns>Sann om användare lades till och annars falsk</returns>
		public bool LäggTillAnvändare(Användare användare)
		{
			//InsättProdukt metoden i Databas returnerar sann eller falsk
			bool lyckades = databas.InsättAnvändare(användare);

			return lyckades;
		}

		/// <summary>
		/// TaBortAnvändare tar bort en användare från databasen med hjälp av
		/// Databas objekten.
		/// </summary>
		/// <param name="produkt">Användare objektet som ska tas bort</param>
		/// <returns>Sann om användaren har tagits bort och annars falsk</returns>
		public bool TaBortAnvändare(int id, string användarnamn)
		{
			//Delete metoden i Databas returnerar sann eller falsk
			bool success = databas.TaBortAnvändare(id, användarnamn);

			//Databasen läsas om och AllaAnvändare sätts till den nya innehåll
			databas.LäsaAnvändare();

			return success;
		}

		/// <summary>
		/// UppdateraAnvändare ändrar om en användare i databasen med hjälp av
		/// Databas objekten.
		/// </summary>
		/// <param name="produkt">Användare objektet som ska ändras</param>
		/// <returns>Sann om användaren ändrades och annars falsk</returns>
		public bool UppdateraAnvändare(Användare användare)
		{
			//Update metoden i Databas returnerar sann eller falsk
			bool success = databas.UppdateraAnvändare(användare);

			//Databasen läsas om och AllaAnvändare sätts till den nya innehåll
			databas.LäsaAnvändare();

			return success;
		}

		/// <summary>
		/// AnvändarenFinnsIRoll testar om användaren har en specifik roll
		/// enligt Windows systemet. 
		/// </summary>
		/// <param name="roll">Windows system roll</param>
		/// <returns>Sann om användaren har den specifika rollen och annars
		///		falsk</returns>
		public bool AnvändarenFinnsIRoll(WindowsBuiltInRole roll)
		{
			WindowsIdentity identitet = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identitet);
			return principal.IsInRole(roll);
		}

		/// <summary>
		/// AnvändarenÄrAdministratör testar om användaren är en
		///	administratör. 
		/// </summary>
		/// <returns>Sann om användaren är administratör och annars falsk</returns>
		public bool AnvändarenÄrAdministratör()
		{
			return AnvändarenFinnsIRoll(WindowsBuiltInRole.Administrator);
		}

		/// <summary>
		/// AnvändarenÄrVanligInloggadAnvändare testar om användaren är en
		/// vanlig inloggad användare. 
		/// </summary>
		/// <returns>Sann om användaren är en vanlig inloggad användare och
		///		annars falsk</returns>
		public bool AnvändarenÄrVanligInloggadAnvändare()
		{
			return AnvändarenFinnsIRoll(WindowsBuiltInRole.User);
		}
	}
}
