using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOMAdministrationApplikation.Models
{
	public class Användare
	{
		//instansvariabler
		private int id;
		private string användarnamn;
		private string lösenordhash;
		private string roll;
		private int räknare;
		private bool låste;

		//SQL int
		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		//30 karaktär max
		public string Användarnamn
		{
			get { return användarnamn; }
			set { användarnamn = value; }
		}

		//with Crypto
		public string LösenordHash
		{
			get { return lösenordhash; }
			set { lösenordhash = value; }
		}

		//30 karaktär max
		public string Roll
		{
			get { return roll; }
			set { roll = value; }
		}

		//SQL int
		public int Räknare
		{
			get { return räknare; }
			set { räknare = value; }
		}

		//SQL bit
		public bool Låste
		{
			get { return låste; }
			set { låste = value; }
		}

		/*
		 * Det är viktigt att en produkt har EXAKT samma instansvariabler för att
		 * vara lika.
		 * Equals metoden är överskriven från Object versionen.
		 * 
		 * In - Objektet som man jämför med
		 * Ut - sann eller falsk om lika
		 */
		public override bool Equals(Object obj)
		{
			//Kolla efter null värden och jämför typer
			if (obj == null || GetType() != obj.GetType())
				return false;

			Användare otherAnvändare = (Användare)obj;
			return ((id == otherAnvändare.ID) && (användarnamn == otherAnvändare.Användarnamn)
				&& (lösenordhash == otherAnvändare.LösenordHash) && (roll == otherAnvändare.Roll)
				&& (räknare == otherAnvändare.Räknare) && (låste == otherAnvändare.Låste));
		}

		/*
		 * Då man överskriver Equals metoden måste man även gör det med GetHashCode
		 * för det används i vissa algoritm med andra standardklasser.  Lika objekt
		 * måste ha samma hash code.
		 * GetHashCode metoden är överskriven från Object versionen.
		 * 
		 * Ut - hash code värde
		 */
		public override int GetHashCode()
		{
			return (id.GetHashCode() ^ användarnamn.GetHashCode() ^ lösenordhash.GetHashCode()
				^ roll.GetHashCode() ^ räknare.GetHashCode() ^ låste.GetHashCode());
		}
	}
}
