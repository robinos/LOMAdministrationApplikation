using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOMAdministrationApplikation.Models
{
	/// <summary>
	/// (Kontroller)
	/// 
	/// MockDatabas är en ersättning för Databas klassen när enhetstester
	/// körs.
	/// 
	/// -Egenskaper-
	/// ProduktLista - get - lista av alla produkter
	/// AnvändarLista - get - lista av alla användare
	/// HögstaAnvändareID - get - högsta id för alla användare i användarListan
	/// 
	/// -Konstruktor-
	/// MockDatabas - initialisera produkt och användare listor
	/// 
	/// -Metoder-
	/// DefaultProdukt - skapar en default produkt för produktListan
	/// LäsProdukter - läsar produkter (spegling)
	/// InsättProdukt - skriver en produkt till produktListan
	/// UppdateraProdukt - ändra en produkt i produktListan
	///	TaBortProdukt - tar bort en produkt från produktListan
	///	ExisterandeProdukt - kollar om en produkt redan existerar i produktListan
	///	HämtaProduktMedID - hämtar en produkt från produktListan som har angiven id
	/// DefaultAnvändare - skapar en default användare för användarListan
	/// LäsAnvändare - läsar användare från databasen till användarListan
	/// InsättAnvändare - skriver en användare till användarListan
	/// UppdateraAnvändare - ändra en användare i användarListan
	///	TaBortAnvändare - ta bort en användare i användarListan
	///	ExisterandeAnvändare - kollar om en användare redan existerar i användarListan
	///	HämtaAnvändareMedID - hämtar en användare från användarListan som har
	///		angiven id
	/// 
	/// Version: 0.5
	/// 2014-12-07
	/// Grupp 2
	/// </summary>
	public class MockDatabas : IDatabas
	{
		//lista för att hålla alla produkter
		private List<Produkt> produktLista;
		//lista för att hålla alla användare
		private List<Användare> användarLista;
		//Högsta ID värde för alla användare
		private int högstaAnvändareID = 0;

		/// <summary>
		/// Get egenskap för listan av alla produkter
		/// </summary>
		public List<Produkt> ProduktLista
		{
			get { return produktLista; }
		}

		/// <summary>
		/// Get egenskap för listan av alla ánvändare
		/// </summary>
		public List<Användare> AnvändarLista
		{
			get { return användarLista; }
		}

		/// <summary>
		/// Get och Set egenskap för högsta ID värde av en användare
		/// </summary>
		public int HögstaAnvändareID
		{
			get { return högstaAnvändareID; }
		}

		/// <summary>
		/// Konstruktör för MockDatabas initialisera listor av produkter och
		/// användare.
		/// </summary>
		public MockDatabas()
		{
			//initialisera en lista av produkter
			this.produktLista = new List<Produkt>();
			//initialisera en lista av användare
			this.användarLista = new List<Användare>();
		}

		/// <summary>
		/// DefaultProdukt skapar en default produkt för produktListan
		/// om den är helt tom.
		/// </summary>
		/// <param name="id">id sträng för en produkt</param>
		/// <returns>en Produkt med default värden</returns>
		public Produkt DefaultProdukt(string id)
		{
			Produkt produkt = new Produkt();

			produkt.ID = id;
			produkt.Namn = id;
			produkt.Pris = 0.00m;
			produkt.Färg = id;
			produkt.Typ = id;
			produkt.Bildfilnamn = id;
			produkt.Ritningsfilnamn = id;
			produkt.RefID = id;
			produkt.Monteringsbeskrivning = id;
			produkt.Beskrivning = id;

			return produkt;
		}

		/// <summary>
		/// LäsaProdukter är en metod som speglar metoden för att läsa in
		/// alla värden. Om listan är tom skapas en default produkt.
		/// </summary>
		/// <returns>sann</returns>
		public bool LäsaProdukter()
		{
			bool lyckades = true;

			if (produktLista.Count <= 0)
				produktLista.Add(DefaultProdukt("0000000000"));

			return lyckades;
		}

		/// <summary>
		/// InsättProdukt är en metod för insättning av en produkt.
		/// Den kollar om id finns redan i listan innan insättning.
		/// Om produkten redan finns gör metoden ingenting och falsk returneras.
		/// </summary>
		/// <param name="produkt">Produkten som ska läggas till</param>
		/// <returns>sann om produkten lades till och annars falsk</returns>
		public bool InsättProdukt(Produkt produkt)
		{
			bool lyckades = false;

			if (!ExisterandeProdukt(produkt.ID))
			{
				produktLista.Add(produkt);
				lyckades = true;
			}

			return lyckades;
		}

		/// <summary>
		/// UppdateraProdukt är en metod för uppdatering av en befintlig produkt
		/// Den kollar redan att id faktiskt finns innan uppdatering. Om produkten
		/// inte redan finns gör metoden ingenting och falsk returneras.
		/// </summary>
		/// <param name="produkt">Produkten som ska uppdateras</param>
		/// <returns>sann om produkten uppdaterades och annars falsk</returns>
		public bool UppdateraProdukt(Produkt produkt)
		{
			bool lyckades = false;

			if (ExisterandeProdukt(produkt.ID))
			{
				produktLista.Remove(HämtaProduktMedID(produkt.ID));
				produktLista.Add(produkt);
				lyckades = true;
			}

			return lyckades;
		}

		/// <summary>
		/// TaBortProdukt är en metod för att ta bort en befintlig produkt.
		/// Den kollar om en id redan finns innan uppdatering. Om produkten
		/// inte redan finns gör metoden ingenting  och falsk returneras.
		/// </summary>
		/// <param name="produkt">Produkten som ska tas bort</param>
		/// <returns>sann om produkten har tagits bort och annars falsk</returns>
		public bool TaBortProdukt(string id)
		{
			bool lyckades = false;

			if (ExisterandeProdukt(id))
			{
				produktLista.Remove(HämtaProduktMedID(id));
				lyckades = true;
			}

			return lyckades;
		}

		/// <summary>
		/// ExisterandeProdukt är en metod som testar om en produkt ID finns
		/// redan i produktListan.  
		/// </summary>
		/// <param name="id">id för en möjlig produkt</param>
		/// <returns>sann om produkten med id-värdet existera och annars falsk</returns>
		public bool ExisterandeProdukt(string id)
		{
			bool bExisterar = false;

			foreach (Produkt produkt in produktLista)
			{
				if (produkt.ID.Equals(id))
					bExisterar = true;
			}

			return bExisterar;
		}

		/// <summary>
		/// HämtaProduktMedID är en metod som hämtar en produkt från listan
		/// med angiven id eller null om ingen hittades. 
		/// </summary>
		/// <param name="id">id för en möjlig produkt</param>
		/// <returns>Produkt objektet om den hittades och annars null</returns>
		public Produkt HämtaProduktMedID(string id)
		{
			Produkt tempProdukt = null;

			foreach (Produkt produkt in produktLista)
			{
				if (produkt.ID.Equals(id))
					tempProdukt = produkt;
			}

			return tempProdukt;
		}

		/// <summary>
		/// DefaultAnvändare skapar en default användare för användarLista
		/// om den är helt tom.
		/// </summary>
		/// <returns>en Användare med default värden</returns>
		public Användare DefaultAnvändare(int id)
		{
			Användare användare = new Användare();

			användare.ID = id;
			användare.Användarnamn = "" + id;
			användare.LösenordHash = "" + id;
			användare.Roll = "" + id;
			användare.Räknare = id;
			användare.Låst = false;

			return användare;
		}

		/// <summary>
		/// LäsaAnvändare är en metod som speglar metoden för att läsa in
		/// alla värden. Om listan är tom skapas en default användare.
		/// </summary>
		/// <returns>sann</returns>
		public bool LäsaAnvändare()
		{
			bool lyckades = true;

			if (användarLista.Count <= 0)
				användarLista.Add(DefaultAnvändare(0));

			return lyckades;
		}

		/// <summary>
		/// InsättAnvändare är en metod för insättning av en användare.
		/// Den kollar om id finns redan i listan innan insättning.
		/// Om användaren redan finns gör metoden ingenting och falsk returneras.
		/// </summary>
		/// <param name="användare">Användare som ska läggas till</param>
		/// <returns>sann om användaren lades till och annars falsk</returns>
		public bool InsättAnvändare(Användare användare)
		{
			bool lyckades = false;

			if (!ExisterandeAnvändare(användare.ID))
			{
				användarLista.Add(användare);
				lyckades = true;
			}

			return lyckades;
		}

		/// <summary>
		/// UppdateraAnvändare är en metod för uppdatering av en befintlig
		/// användare. Den kollar redan att id faktiskt finns innan uppdatering.
		/// Om användaren inte redan finns gör metoden ingenting och falsk
		/// returneras.
		/// </summary>
		/// <param name="användare">Användare som ska uppdateras</param>
		/// <returns>sann om användaren uppdaterades och annars falsk</returns>
		public bool UppdateraAnvändare(Användare användare)
		{
			bool lyckades = false;

			if (ExisterandeAnvändare(användare.ID))
			{
				användarLista.Remove(HämtaAnvändareMedID(användare.ID));
				användarLista.Add(användare);
				lyckades = true;
			}

			return lyckades;
		}

		/// <summary>
		/// TaBortAnvändare är en metod för att ta bort en befintlig användare.
		/// Den kollar att id redan finns innan den tas bort. Om användaren
		/// inte redan finns gör metoden ingenting och falsk returneras.
		/// </summary>
		/// <param name="användare">Användare som ska tas bort</param>
		/// <returns>sann om användaren har tagits bort och annars falsk</returns>
		public bool TaBortAnvändare(int id)
		{
			bool lyckades = false;

			if (ExisterandeAnvändare(id))
			{
				användarLista.Remove(HämtaAnvändareMedID(id));
				lyckades = true;
			}

			return lyckades;
		}

		/// <summary>
		/// ExisterandeAnvändare är en metod som testar om en användare ID finns
		/// redan i användarListan.  
		/// </summary>
		/// <param name="id">id för en möjlig användare</param>
		/// <returns>sann om användaren med id-värdet existera och annars falsk</returns>
		public bool ExisterandeAnvändare(int id)
		{
			bool bExisterar = false;

			foreach (Användare användare in användarLista)
			{
				if (användare.ID.Equals(id))
					bExisterar = true;
			}

			return bExisterar;
		}

		/// <summary>
		/// HämtaAnvändareMedID är en metod som hämtar en användare från listan
		/// med angiven id eller null om ingen hittades. 
		/// </summary>
		/// <param name="id">id för en möjlig användare</param>
		/// <returns>Användare objektet om den hittades och annars null</returns>
		public Användare HämtaAnvändareMedID(int id)
		{
			Användare tempAnvändare = null;

			foreach (Användare användare in användarLista)
			{
				if (användare.ID.Equals(id))
					tempAnvändare = användare;
			}

			return tempAnvändare;
		}
	}
}
