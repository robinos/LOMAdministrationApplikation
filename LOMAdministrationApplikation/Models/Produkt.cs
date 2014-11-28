using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

/// <summary>
/// (Modell - Produkt)
/// 
/// Produkt klassen är en behållare för information inläst från en
/// rad av Produkt tabellen i databasen.  Den även används för att
/// sedan skriva nya rader till tabellen.
/// 
/// -Egenskaper-
/// ID - get/set - sträng produkt id som är kolumnen ID i tabellen
/// Namn - get/set - sträng namn som är kolumnen Namn i tabellen
/// Pris - get/set - decimal pris som är kolumnen Pris i tabellen
/// Typ - get/set - sträng typ som är kolumnen Typ i tabellen
/// Färg - get/set - sträng färg som är kolumnen Farg i tabellen
/// Bildfilnamn - get/set - sträng bildfilnamn som är kolumnen Bildfilnamn
///		i tabellen
/// Ritningsfilnamn - get/set - sträng ritningsfilnamn som är kolumnen
///		Ritningsfilnamn i tabellen
/// RefID - get/set - sträng produkt refid som är kolumnen RefID i tabellen
/// Monteringsbeskrivning - get/set - sträng monteringsbeskrivning som är
///		kolumnen Montering i tabellen
/// Beskrivning - get/set - sträng beskrivning som är kolumnen Beskrivning
///		i tabellen
/// 
/// -Metoder-
/// Equals - Override på Equals metoden där alla variabler är viktiga
///		(är tänkt att användas för att se om andringar har gjorts)
/// GetHashCode - Override på GetHashCode metoden där alla variabler
///		är viktiga (*måste finnas för ändringar på Equals metoden)
/// 
/// Version: 0.3
/// 2014-11-28
/// Grupp 2
/// </summary>
namespace LOMAdministrationApplikation.Models
{
	public class Produkt
	{
		//instansvariabler
		private string id;
		private string namn;
		private Decimal pris;
		private string typ;
		private string färg;
		private string bildfilnamn;
		private string ritningsfilnamn;
		private string refid;
		private string beskrivning;
		private string monteringsbeskrivning;

		/// <summary>
		/// Get/Set egenskap för sträng id.  I Produkt tabellen blir
		/// den en 5 karaktär char som representera ett 5 storlek
		/// numeriskt värde och heter ID. Den är även tabellnyckeln.
		/// </summary>
		public string ID
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng namn.  I Produkt tabellen
		/// blir den en 30 karaktär varchar som heter Namn och
		/// representerar produktnamn. Namn är även unikt i tabellen.
		/// </summary>
		public string Namn
		{
			get { return namn; }
			set { namn = value; }
		}

		/// <summary>
		/// Get/Set egenskap för decimal pris.  I Produkt tabellen
		/// heter den Pris och är en SQL Decimal(10,2) - 10 max storlek
		/// framför decimalen och 2 storlek efter decimalen.
		/// </summary>
		public Decimal Pris
		{
			get { return pris; }
			set { pris = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng typ.  I Produkt tabellen
		/// blir den en 30 karaktär varchar som heter Typ och
		/// representerar produktkategori.
		/// </summary>
		public string Typ
		{
			get { return typ; }
			set { typ = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng färg.  I Produkt tabellen
		/// blir den en 30 karaktär varchar som heter Farg (utan ä)
		/// och representerar produktfärgen.
		/// </summary>
		public string Färg
		{
			get { return färg; }
			set { färg = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng bildfilnamn.  I Produkt tabellen
		/// blir den en 30 karaktär varchar som heter Bildfilnamn och
		/// och representerar filnamn för relaterad bild.
		/// </summary>
		public string Bildfilnamn
		{
			get { return bildfilnamn; }
			set { bildfilnamn = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng ritningsfilnamn.  I Produkt tabellen
		/// blir den en 30 karaktär varchar som heter Ritningsfilnamn och
		/// och representerar filnamn för relaterad bild av ritningen.
		/// </summary>
		public string Ritningsfilnamn
		{
			get { return ritningsfilnamn; }
			set { ritningsfilnamn = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng refid.  I Produkt tabellen blir
		/// den en 5 karaktär char som representera ett 5 storlek
		/// numeriskt värde och heter RefID. Den är en referens till
		/// produkten den tillhör om produkten är kopplad till en annan
		/// som med vissa tillbehör.  Annars används egen ID.
		/// </summary>
		public string RefID
		{
			get { return refid; }
			set { refid = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng monteringsbeskrivning.  I
		/// Produkt tabellen blir den en 300 karaktär varchar som
		/// heter Montering och och representerar monteringsbeskrivning
		/// för en produkt.
		/// </summary>
		public string Monteringsbeskrivning
		{
			get { return monteringsbeskrivning; }
			set { monteringsbeskrivning = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng beskrivning.  I Produkt
		/// tabellen blir den en 600 karaktär varchar som heter
		/// Beskrivning och och representerar en beskrivning för en
		/// produkt.
		/// </summary>
		public string Beskrivning
		{
			get { return beskrivning; }
			set { beskrivning = value; }
		}

		/// <summary>
		/// Equals jämför en Produkt objekt med en annan med hjälp av alla
		/// variabler (för att se om någonting har ändrats).  Den överskriver
		/// Object klassens version och därför måste ser ut på samma sätt.
		/// </summary>
		/// <param name="obj">Object som Produkt objektet ska jämföras med</param> 
		/// <returns>sann om de räknas som lika och annars falsk</returns>
		public override bool Equals(Object obj)
		{
			//Kolla efter null värden och jämför typer
			if (obj == null || GetType() != obj.GetType())
				return false;

			//Om ovan är sann jämför alla variabler
			Produkt otherProdukt = (Produkt)obj;
			return ( (id == otherProdukt.ID) && (namn == otherProdukt.Namn)
				&& (pris == otherProdukt.Pris) && (typ == otherProdukt.Typ)
				&& (färg == otherProdukt.Färg) && (bildfilnamn == otherProdukt.Bildfilnamn)
				&& (ritningsfilnamn == otherProdukt.Ritningsfilnamn)
				&& (refid == otherProdukt.RefID) && (monteringsbeskrivning == otherProdukt.Monteringsbeskrivning)
				&& (beskrivning == otherProdukt.Beskrivning));
		}

		/// <summary>
		/// GetHashCode skapar en hash kod från alla variabler som används för att
		/// bestämma om två produkter är lika.  Den borde vara unik och bara lika
		/// om Produkt objekten skulle räknas som lika.
		/// </summary>
		/// <returns>en integer hash värde som borde bara vara lika om Produkt
		///		objekten räknas som lika med alla variabler</returns>
		public override int GetHashCode()
		{
			return ( id.GetHashCode() ^ namn.GetHashCode() ^ pris.GetHashCode() ^ typ.GetHashCode()
				^ färg.GetHashCode() ^ bildfilnamn.GetHashCode() ^ ritningsfilnamn.GetHashCode()
				^ refid.GetHashCode() ^ monteringsbeskrivning.GetHashCode()
				^ beskrivning.GetHashCode() );
		}		
	}
}
