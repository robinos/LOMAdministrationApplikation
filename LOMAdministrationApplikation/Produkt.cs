using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * (Modell)
 * ProduktApplikation testar inte data innan insättning.
 * All datatestning tas hand om i ProduktApplikationForm och Databas
 * klasserna.
 * 
 * Equals - Överskriven metoden för hur .Equals vet om produkter är lika (alla
 *		variabler måste vara lika)
 * GetHashCode - Man måste överskriva även GetHashCode då man ändrar Equals metoden 
 * 
 * Version: 1.0
 * 2014-10-27
 * Robin Osborne
 */
namespace LOMAdministrationApplikation
{
	public class Produkt
	{
		//instansvariabler
		private string id;
		private string namn;
		private decimal pris;
		private string typ;
		private string farg;
		private string bildfilnamn;
		private string ritningsfilnamn;
		private string refid;
		private string beskrivning;
		private string monteringsbeskrivning;

		/*
		 * Constructor för Produktklassen
		 */
		public Produkt() { }

		//5 karaktär max (numerisk-värde som sträng)
		public string ID
		{
			get { return id; }
			set { id = value; }
		}

		//30 karaktär max
		public string Namn
		{
			get { return namn; }
			set { namn = value; }
		}

		//SQL Decimal(10,2)
		public decimal Pris
		{
			get { return pris; }
			set { pris = value; }
		}

		//30 karaktär max
		public string Typ
		{
			get { return typ; }
			set { typ = value; }
		}

		//30 karaktär max
		public string Farg
		{
			get { return farg; }
			set { farg = value; }
		}

		//30 karaktär max
		public string Bildfilnamn
		{
			get { return bildfilnamn; }
			set { bildfilnamn = value; }
		}

		//30 karaktär max
		public string Ritningsfilnamn
		{
			get { return ritningsfilnamn; }
			set { ritningsfilnamn = value; }
		}

		//5 karaktär max (numerisk-värde som sträng)
		public string RefID
		{
			get { return refid; }
			set { refid = value; }
		}

		//300 karaktär max
		public string Monteringsbeskrivning
		{
			get { return monteringsbeskrivning; }
			set { monteringsbeskrivning = value; }
		}

		//600 karaktär max
		public string Beskrivning
		{
			get { return beskrivning; }
			set { beskrivning = value; }
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

			Produkt otherProdukt = (Produkt)obj;
			return ( (id == otherProdukt.ID) && (namn == otherProdukt.Namn)
				&& (pris == otherProdukt.Pris) && (typ == otherProdukt.Typ)
				&& (farg == otherProdukt.Farg) && (bildfilnamn == otherProdukt.Bildfilnamn)
				&& (ritningsfilnamn == otherProdukt.Ritningsfilnamn)
				&& (refid == otherProdukt.RefID) && (monteringsbeskrivning == otherProdukt.Monteringsbeskrivning)
				&& (beskrivning == otherProdukt.Beskrivning));
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
			return ( id.GetHashCode() ^ namn.GetHashCode() ^ pris.GetHashCode() ^ typ.GetHashCode()
				^ farg.GetHashCode() ^ bildfilnamn.GetHashCode() ^ ritningsfilnamn.GetHashCode()
				^ refid.GetHashCode() ^ monteringsbeskrivning.GetHashCode()
				^ beskrivning.GetHashCode() );
		}		
	}
}
