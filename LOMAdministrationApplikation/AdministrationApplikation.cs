using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOMAdministrationApplikation
{
	/*
	 * (Kontroller - Main)
	 * AdministrationApplikation har hand om all kommunikation mellan Databas och
	 * ProduktForm och AnvändareForm.  
	 * 
	 * Main - Kör programmet (startar ProduktApplikationForm)
	 * LäsaFrånDatabas - läser in data från databasen (använder Databas produktDatabas)
	 * LäggTillProdukt - lägg till en produkt i databasen (använder Databas produktDatabas)
	 * TaBortProdukt - ta bort en produkt från databasen (använder Databas produktDatabas)
	 * UppdateraProdukt - uppdatera en produkt i databasen (använder Databas produktDatabas)
	 * SammaProdukter - kollar att Dictionary produkter är samma i ProduktApplikation och Databas
	 * 
	 * Version: 1.0
	 * 2014-10-27
	 * Robin Osborne
	 */
	public class AdministrationApplikation
	{
		//instansvariabler
		//Referens till Databas 
		private Databas produktDatabas = null;
		//Dictionary av produkter
		private Dictionary<string, Produkt> produkter;

		/*
		 * Konstruktör för ProduktApplikation
		 */
		public AdministrationApplikation()
		{
			//initialisera produktDatabas
			produktDatabas = new Databas();
			//initialiser Dictionary av produkter (samma referens som
			//produkter i produktDatabas)
			produkter = produktDatabas.Produkter;
		}

		//Get/Set till Dictionary produkter
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
		/// Main startar och kör huvudprogrammet.  Den initialiserar en instans av
		/// ProduktApplikation och läser från databasen.  Om det lyckas, startas
		/// ProduktApplikationForm (UI) och programmet börjar för användaren, annars
		/// stängs det ner.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//Med produktApplikation undviker man att behöver ha statisk metoder
			//och kan skickar som referens till ProduktApplikationForm
			AdministrationApplikation produktApplikation = new AdministrationApplikation();

			//Läsa in data från databasen. Om det lyckas, körs applikationen
			if (produktApplikation.LäsaFrånDatabas())
			{
				//Visar Form
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//ProduktApplikationForm objekten görs i samband med körning
				//för att man behöver inte i nuläge en referens till den
				Application.Run(new ProduktForm(produktApplikation));
			}
		}

		/*
		 * LäsaFrånDatabas läser in data från databasen och tillsätter Dictionary
		 * produkter med datan som läsas in.
		 * 
		 * ut - sann returneras om det lyckades, annars falsk
		 */
		public bool LäsaFrånDatabas()
		{
			bool lyckades = false;

			//Om ProduktLasare metoden i Databas returnerar sann, lyckades
			//blir sann
			if (produktDatabas.LäsaProdukter())
			{
				lyckades = true;
			}

			return lyckades;
		}

		/*
		 * LaggTillProdukt är en metod för att lägga till en produkt till databasen.
		 * 
		 * in - produkten som ska läggas till databasen 
		 * ut - sann returneras om det lyckades, annars falsk
		 */
		public bool LäggTillProdukt(Produkt produkt)
		{
			//InsättProdukt metoden i Databas returnerar sann eller falsk
			bool lyckades = produktDatabas.InsättProdukt(produkt);

			return lyckades;
		}

		/*
		 * TaBortProdukt är en metod för att ta bort en produkt från databasen
		 * och uppdaterar Dictionary produkter.
		 * 
		 * in - id(sträng) av produkten som ska tas bort från databasen 
		 * ut - sann returneras om det lyckades, annars falsk
		 */
		public bool TaBortProdukt(string id)
		{
			//Delete metoden i Databas returnerar sann eller falsk
			bool success = produktDatabas.TaBortProdukt(id);

			//Databasen läsas om och Produkt sätts till den nya innehåll
			produktDatabas.LäsaProdukter();

			return success;
		}

		/*
		 * UppdateraProdukt är en metod för att ändra en produkt i databasen.
		 * 
		 * in - produkten som ska uppdateras i databasen 
		 * ut - sann returneras om det lyckades, annars falsk
		 */
		public bool UppdateraProdukt(Produkt produkt)
		{
			//Update metoden i Databas returnerar sann eller falsk
			bool success = produktDatabas.UppdateraProdukt(produkt);

			//Databasen läsas om och Produkt sätts till den nya innehåll
			produktDatabas.LäsaProdukter();

			return success;
		}

		/*
		 * SammaProdukter testar att Dictionary Produkter är samma som den
		 * i Databas klassen.  Används för test.
		 * Det borde vara en referens till samma objekt så en enklare
		 * Equals (från objekt) kan användas.
		 * 
		 * ut - sann returneras om Dictionary objekten är samma, annars falsk
		 */
		public bool SammaProdukter()
		{
			return(produktDatabas.Produkter.Equals(produkter));
		}
	}
}
