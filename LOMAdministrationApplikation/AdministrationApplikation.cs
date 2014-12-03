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
	/// -Egenskaper-
	/// ProduktLista - get - lista av alla produkter i databasen
	/// AnvändarLista - get - lista av alla användare i databasen
	/// ProdukterPerSida - get - antal element (produkter) på en sida
	/// TotallaSidorProdukter - get - antal sidor av element för produkter
	/// ProdukterPerSida - get - antal element (användare) på en sida
	/// TotallaSidorAnvändare - get - antal sidor av element för användare
	/// HögstaAnvändareID - get - högsta id för alla användare i databasen/listan
	/// 
	/// -Konstruktör-
	/// AdministrationApplikation - tilldelar databas objektet och initialisera
	/// produkt och användare listor
	/// 
	/// -Metoder-
	/// Main - Kör programmet (startar HuvudApplikationForm)
	/// RäknarTotallaSidor - räknar totalla sidor (totall element / element per
	///		sida)
	/// LäsaFrånDatabas - läser in data från databasen (använder Databas
	///		klassen)
	///	HämtaSidaProdukter - Hämtar en angiven sida av produkter (produkterPerSida
	///		element)
	///	HämtaProduktKategoriLista - hittar och returnera alla unika produktkategorier
	///	FiltreraProduktLista - filtrera angiven produktlistan på söksträng och kategori
	/// LäggTillProdukt - lägger till en produkt i databasen (använder
	///		Databas klassen)
	/// TaBortProdukt - ta bort en produkt från databasen (använder Databas
	///		klassen)
	/// UppdateraProdukt - uppdatera en produkt i databasen (använder Databas
	///		klassen)
	///	HämtaAnvändareMedID - hämtar en användare från användarLista med angiven id	
	///	HämtaSidaAnvändare - Hämtar en angiven sida av användare (användarePerSida
	///		element)
	///	FiltreraAnvändareLista - filtrera angiven användarlistan på söksträng
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
	/// Version: 0.4
	/// 2014-12-01
	/// Grupp 2
	/// </summary>
	public class AdministrationApplikation
	{
		//instansvariabler
		//Referens till Databas 
		private Databas databas = null;
		//Referens till produkt och användare listor
		private List<Produkt> produktLista;
		private List<Användare> användarLista;
		//antal produkter per sida och totalla sidor
		private int produkterPerSida = 5;
		private int totallaSidorProdukter = 1;
		//antal användare per sida och totalla sidor
		private int användarePerSida = 5;
		private int totallaSidorAnvändare = 1;
		//Högsta nuvarande användare ID att skapa nya användare
		private int högstaAnvändareID = 0;

		/// <summary>
		/// Get egenskap för listan av alla produkter i databasen
		/// </summary>
		public List<Produkt> ProduktLista
		{
			get { return produktLista; }
		}

		/// <summary>
		/// Get egenskap för listan av alla ánvändare i databasen
		/// </summary>
		public List<Användare> AnvändarLista
		{
			get { return användarLista; }
		}

		/// <summary>
		/// Get egenskap för produkter per sida
		/// </summary>
		public int ProdukterPerSida
		{
			get { return produkterPerSida; }
		}

		/// <summary>
		/// Get egenskap för totalla sidor av produkter
		/// </summary>
		public int TotallaSidorProdukter
		{
			get { return totallaSidorProdukter; }
		}

		/// <summary>
		/// Get egenskap för användare per sida
		/// </summary>
		public int AnvändarePerSida
		{
			get { return användarePerSida; }
		}

		/// <summary>
		/// Get egenskap för totalla sidor av användare
		/// </summary>
		public int TotallaSidorAnvändare
		{
			get { return totallaSidorAnvändare; }
		}

		/// <summary>
		/// Get egenskap för högsta ID värde av en användare
		/// </summary>
		public int HögstaAnvändareID
		{
			get { return högstaAnvändareID; }
		}

		/// <summary>
		/// Konstruktör för AdministrationApplikation tilldelar databas
		/// objektet och initialisera produkt och användare listor till att
		/// vara samma referens som de i databas klassen.
		/// </summary>
		public AdministrationApplikation()
		{
			//koppla referenser för databas och listor
			databas = new Databas();
			produktLista = databas.ProduktLista;
			användarLista = databas.AnvändarLista;
		}

		/// <summary>
		/// Main startar och kör huvudprogrammet.  Den initialiserar en instans
		/// av AdministrationApplikation och läser från databasen.  Om det lyckas
		/// startas HuvudApplikationForm (UI Menyn) med instansen av
		/// AdministrationApplikation som parameter och programmet börjar för
		/// användaren.  Vid misslyckade läsning från databas stängs programmet ner.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//AdministrationApplikation objekt för att skickar som referens till
			//HuvudApplikationForm
			AdministrationApplikation administrationApplikation = new AdministrationApplikation();

			//Läsa in data från databasen. Om det lyckas kör applikationen.
			if (administrationApplikation.LäsaFrånDatabas())
			{
				//Visar HuvudApplikationForm
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//AdministrationApplikation skickas med för kontakt med metoder så
				//man behöver inte ha logiken i formena där det är svårt att unit
				//testa
				Application.Run(new HuvudApplikationForm(administrationApplikation));
			}
		}

		/// <summary>
		/// RäknarTotallaSidor räknar totalla sidor genom att dela totall antal
		/// element med element per sida.  Om det finns rester läggs de till
		/// en sida till.  
		/// </summary>
		/// <param name="totallAntalElement">Totall antal element</param>
		/// <param name="elementPerSida">Element som borde finnas per sida</param> 
		/// <returns>integer värdet för totalla sidor</returns>
		private int RäknarTotallaSidor(int totallAntalElement, int elementPerSida)
		{
			int totallaSidor = totallAntalElement / elementPerSida;

			//Om där finns rester lägg till en sida
			if (totallAntalElement % elementPerSida > 0)
				totallaSidor += 1;

			//if (totallAntalElement < elementPerSida)
			//	elementPerSida = totallAntalElement;

			return totallaSidor;
		}

		/// <summary>
		/// LäsaFrånDatabas läser in data från både tabeller (Produkt och Anvandare)
		/// med hjälp av Databas objektet.  Om det lyckas räknar den ut totalla sidor
		/// för visningen av produkter och användare.  Högsta användare ID också sätts
		/// och lyckades returneras.  Om läsning från databasen inte lyckades visas
		/// en varning i en Message Box och falsk returneras.
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
				//Räknar hur många sidor av produkter det blir totallt
				totallaSidorProdukter = RäknarTotallaSidor(produktLista.Count, produkterPerSida);
				//Räknar hur många sidor av användare det blir totallt
				totallaSidorAnvändare = RäknarTotallaSidor(användarLista.Count, användarePerSida);
				//Få fram den högsta nuvarande användare ID
				högstaAnvändareID = databas.HögstaAnvändareID;
				lyckades = true;
			}
			else
			{
				MessageBox.Show("Det gick inte att läsa från en eller fler databas tabeller!");
			}

			return lyckades;
		}

		/// <summary>
		/// HämtaSidaProdukter hämtar en sida av produkter (produkterPerSida
		/// element) för angiven sidan. 
		/// </summary>
		/// <param name="sida">Sidan man ska hämta produkter för</param>
		/// <returns>En Lista av Produkt objekt för en sida</returns>
		public List<Produkt> HämtaSidaProdukter(int sida, List<Produkt> produkter)
		{
			List<Produkt> tempProduktLista;

			//Vill man titta på sida 1 tar man första antal produkter (enligt produkter per sida)
			if (sida == 1)
			{
				tempProduktLista = new List<Produkt>((from m in produkter select m).Take(produkterPerSida));
				tempProduktLista.OrderBy(n => n.Namn);
			}
			//Annars söker man efter sida 2 eller högre
			else
			{
				int tidigareSidorProdukter = (sida - 1) * produkterPerSida;

				//Titta på topp antal produkter minus de som var på tidigare sidor
				List<Produkt> excludeLista = new List<Produkt>((from z in produkter select z).Take(tidigareSidorProdukter));
				tempProduktLista = new List<Produkt>((produkter.Except(excludeLista).Take(produkterPerSida)));
				tempProduktLista.OrderBy(n => n.Namn);
			}

			return tempProduktLista;
		}

		/// <summary>
		/// HämtaProduktKategoriLista hittar alla unika produktkategorier och
		/// skickar tillbaka de som en lista av strängar.
		/// </summary>
		/// <param name="produkter">En lista av produkt objekt</param>
		/// <returns>En lista av strängar (med alla unika kategorier)</returns> 
		public List<string> HämtaProduktKategoriLista(List<Produkt> produkter)
		{
			List<string> kategoriLista = new List<string>();

			//En fråga efter alla typer av produkter i typ ordning
			var TypQry = from d in produkter
						 orderby d.Typ
						 select d.Typ;

			//Listan fylls med alla unika typer som finns 
			kategoriLista.AddRange(TypQry.Distinct());

			return kategoriLista;
		}

		/// <summary>
		/// FiltreraProduktLista filtrera angiven listan på söksträng om angiven söksträng
		/// är inte tom eller null och på kategori om angiven kategoristräng är inte tom
		/// eller null.
		/// </summary>
		/// <param name="produkter">En lista av produkt objekt</param>
		/// <param name="sök">En sträng med möjligt sökord för filtrering av listan</param>
		/// <param name="kategori">En sträng med möjligt kategori för filtrering av listan</param> 
		/// <returns>En lista av produkt objekt (den filtrerade lista)</returns>
		public List<Produkt> FiltreraProduktLista(List<Produkt> produkter, string sök, string kategori)
		{
			//Om där finns en söksträng, filtrera produkter efter namn som innehåller
			//söksträngen
			if (!String.IsNullOrEmpty(sök))
			{
				produkter = new List<Produkt>(produkter.Where(s => s.Namn.ToUpper().Contains(sök.ToUpper())));
			}

			//Om där finns en vald kategori filtreras produkter efter vald typ
			if (!string.IsNullOrEmpty(kategori))
			{
				produkter = new List<Produkt>(produkter.Where(x => x.Typ == kategori));
			}

			return produkter;
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
		/// <param name="id">id av produkt objektet som ska tas bort</param>
		/// <returns>Sann om produkten har tagits bort och annars falsk</returns>
		public bool TaBortProdukt(string id)
		{
			//Delete metoden i Databas returnerar sann eller falsk
			bool lyckades = databas.TaBortProdukt(id);

			return lyckades;
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
			bool lyckades = databas.UppdateraProdukt(produkt);

			return lyckades;
		}

		/// <summary>
		/// HämtaAnvändareMedID hämtar en användare från användarLista som
		/// har samma id som angiven eller null vid ingen träff.
		/// </summary>
		/// <param name="id">id av användaren man ska hämta</param>
		/// <returns>Användare objektet som hade samma id eller null</returns>
		public Användare HämtaAnvändareMedID(int id)
		{
			Användare tempAnvändare = null;

			//för varje användare i listan jämför dens id med angiven id
			foreach (Användare användare in användarLista)
			{
				if (användare.ID.Equals(id))
					tempAnvändare = användare;
			}

			return tempAnvändare;
		}

		/// <summary>
		/// HämtaSidaAnvändare hämtar en sida av användare (användarePerSida
		/// element) för angiven sidan. 
		/// </summary>
		/// <param name="sida">Sidan man ska hämta användare för</param>
		/// <returns>En Lista av Användare objekt för en sida</returns>
		public List<Användare> HämtaSidaAnvändare(int sida, List<Användare> användare)
		{
			List<Användare> tempAnvändarLista;

			//Vill man titta på sida 1 tar man första antal användare
			//(enligt användare per sida)
			if (sida == 1)
			{
				tempAnvändarLista = new List<Användare>((from m in användare select m).Take(användarePerSida));
				tempAnvändarLista.OrderBy(n => n.Användarnamn);
			}
			//Annars söker man efter sida 2 eller högre
			else
			{
				int tidigareSidorAnvändare = (sida - 1) * användarePerSida;

				//Titta på topp antal användare minus de som var på tidigare sidor
				List<Användare> excludeLista = new List<Användare>((from z in användare select z).Take(tidigareSidorAnvändare));
				tempAnvändarLista = new List<Användare>((användare.Except(excludeLista).Take(användarePerSida)));
				tempAnvändarLista.OrderBy(n => n.Användarnamn);
			}

			return tempAnvändarLista;
		}

		/// <summary>
		/// FiltreraProduktLista filtrera angiven listan på söksträng om angiven söksträng
		/// är inte tom eller null och på kategori om angiven kategoristräng är inte tom
		/// eller null.
		/// </summary>
		/// <param name="produkter">En lista av produkt objekt</param>
		/// <param name="sök">En sträng med möjligt sökord för filtrering av listan</param>
		/// <param name="kategori">En sträng med möjligt kategori för filtrering av listan</param> 
		/// <returns>En lista av produkt objekt (den filtrerade lista)</returns>
		public List<Användare> FiltreraAnvändareLista(List<Användare> användare, string sök)
		{
			//Om där finns en söksträng, filtrera produkter efter namn som innehåller
			//söksträngen
			if (!String.IsNullOrEmpty(sök))
			{
				användare = new List<Användare>(användare.Where(s => s.Användarnamn.ToUpper().Contains(sök.ToUpper())));
			}

			return användare;
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
		/// <param name="id">id av användaren som ska tas bort</param>
		/// <returns>Sann om användaren har tagits bort och annars falsk</returns>
		public bool TaBortAnvändare(int id)
		{
			//Delete metoden i Databas returnerar sann eller falsk
			bool lyckades = databas.TaBortAnvändare(id);

			return lyckades;
		}

		/// <summary>
		/// UppdateraAnvändare ändrar om en användare i databasen med hjälp av
		/// Databas objekten.
		/// </summary>
		/// <param name="användare">Användare objektet som ska ändras</param>
		/// <returns>Sann om användaren ändrades och annars falsk</returns>
		public bool UppdateraAnvändare(Användare användare)
		{
			//Update metoden i Databas returnerar sann eller falsk
			bool lyckades = databas.UppdateraAnvändare(användare);

			return lyckades;
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
