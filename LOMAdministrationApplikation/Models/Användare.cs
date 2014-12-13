using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOMAdministrationApplikation.Models
{
	/// <summary>
	/// (Model - Användare)
	/// 
	/// Användare klassen är en behållare för information inläst från en
	/// rad av Anvandare tabellen i databasen.  Den även används för att
	/// sedan skriva nya rader till tabellen.
	/// 
	/// -Egenskaper-
	/// ID - get/set - int användare id som är kolumnen ID i tabellen
	/// Användarnamn - get/set - sträng användarnamn som är kolumnen
	///		Anvandarnamn i tabellen
	/// LösenordHash - get/set - sträng hash för lösenordet som är
	///		kolumnen LosenordHash i tabellen
	/// Roll - get/set - sträng roll som är kolumnen Roll i tabellen
	///		(bara kund används i nuläget)
	/// Räknare - get/set - sträng räknare som är kolumnen Räknare i tabellen
	///		(representerar antal misslyckade inloggningar)
	/// Låst - get/set - boolean låst som är kolumnen Låste i tabellen
	///		(om sann användaren kan inte logga in)
	/// 
	/// -Metoder-
	/// Equals - Override på Equals metoden där alla variabler är viktiga
	///		(är tänkt att användas för att se om andringar har gjorts)
	/// GetHashCode - Override på GetHashCode metoden där alla variabler
	///		är viktiga (*måste finnas för ändringar på Equals metoden)
	/// 
	/// Version: 0.5
	/// 2014-12-07
	/// Grupp 2
	/// </summary>
	public class Användare
	{
		//instansvariabler
		private int id;
		private string användarnamn;
		private string lösenordhash;
		private string roll;
		private int räknare;
		private bool låst;

		/// <summary>
		/// Get/Set egenskap för integer användare id.  I Anvandare tabellen
		/// blir den en SQL integer som heter ID.  Den är även tabellnyckeln.
		/// </summary>
		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng användarnamn.  I Anvandare tabellen
		/// blir den en 30 karaktär varchar som heter Anvandarnamn (utan ä).
		/// Anvandarnamn är unikt i tabellen.
		/// </summary>
		public string Användarnamn
		{
			get { return användarnamn; }
			set { användarnamn = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng lösenordhash.  I Anvandare tabellen
		/// blir den en maxlängd nvarchar som heter LosenordHash (utan ö).
		/// Hashen görs med System.Web.Helpers biblioteket.
		/// </summary>
		public string LösenordHash
		{
			get { return lösenordhash; }
			set { lösenordhash = value; }
		}

		/// <summary>
		/// Get/Set egenskap för sträng roll.  I Anvandare tabellen
		/// blir den en 30 karaktär varchar som heter Roll och
		/// representerar användare roll (just nu används bara "kund").
		/// </summary>
		public string Roll
		{
			get { return roll; }
			set { roll = value; }
		}

		/// <summary>
		/// Get/Set egenskap för integer räknare.  I Anvandare tabellen
		/// blir den en SQL integer som heter Raknare (utan ä).
		/// </summary>
		public int Räknare
		{
			get { return räknare; }
			set { räknare = value; }
		}

		/// <summary>
		/// Get/Set egenskap för boolean låst.  I Anvandare tabellen
		/// blir den en SQL bit som heter Last (utan å).
		/// </summary>
		public bool Låst
		{
			get { return låst; }
			set { låst = value; }
		}

		/// <summary>
		/// Equals jämför en Användare objekt med en annan med hjälp av alla
		/// variabler (för att se om någonting har ändrats).  Den överskriver
		/// Object klassens version och därför måste ser ut på samma sätt.
		/// </summary>
		/// <param name="obj">Object som Användare objektet ska jämföras med</param> 
		/// <returns>sann om de räknas som lika och annars falsk</returns>
		public override bool Equals(Object obj)
		{
			//Kolla efter null värden och jämför typer
			if (obj == null || GetType() != obj.GetType())
				return false;

			//Om ovan är sann jämför alla variabler
			Användare otherAnvändare = (Användare)obj;
			return ((id == otherAnvändare.ID) && (användarnamn == otherAnvändare.Användarnamn)
				&& (lösenordhash == otherAnvändare.LösenordHash) && (roll == otherAnvändare.Roll)
				&& (räknare == otherAnvändare.Räknare) && (låst == otherAnvändare.Låst));
		}

		/// <summary>
		/// GetHashCode skapar en hash kod från alla variabler som används för att
		/// bestämma om två användare är lika.  Den borde vara unik och bara lika
		/// om Användare objekten skulle räknas som lika.
		/// </summary>
		/// <returns>en integer hash värde som borde bara vara lika om Användare
		///		objekten räknas som lika med alla variabler</returns>
		public override int GetHashCode()
		{
			return (id.GetHashCode() ^ användarnamn.GetHashCode() ^ lösenordhash.GetHashCode()
				^ roll.GetHashCode() ^ räknare.GetHashCode() ^ låst.GetHashCode());
		}
	}
}
