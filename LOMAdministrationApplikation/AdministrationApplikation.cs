using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOMAdministrationApplikation.Controllers;
using LOMAdministrationApplikation.Models;
using LOMAdministrationApplikation.Views;

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
		private Databas databas = null;
		//Dictionary av produkter
		private Dictionary<string, Produkt> produkter;
		//Dictionary av användare
		private Dictionary<string, Användare> allaAnvändare;

		/*
		 * Konstruktör för ProduktApplikation
		 */
		public AdministrationApplikation()
		{
			//initialisera produktDatabas
			databas = new Databas();
			//initialiser Dictionary av produkter (samma referens som
			//produkter i produktDatabas)
			produkter = databas.Produkter;
			allaAnvändare = databas.AllaAnvändare;
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

		//Get/Set till Dictionary allaAnvändare
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
			AdministrationApplikation administrationApplikation = new AdministrationApplikation();

			//Läsa in data från databasen. Om det lyckas, körs applikationen
			if (administrationApplikation.LäsaFrånDatabas())
			{
				//Visar Form
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//ProduktApplikationForm objekten görs i samband med körning
				//för att man behöver inte i nuläge en referens till den
				//Application.Run(new ProduktForm(produktApplikation));
				Application.Run(new HuvudApplikationForm(administrationApplikation));
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
			if (databas.LäsaProdukter() && databas.LäsaAnvändare())
			{
				lyckades = true;
			}
			else
			{
				//MessageBox.Show("Det gick inte att läsa från en eller fler databas tabeller!");
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
			bool lyckades = databas.InsättProdukt(produkt);

			return lyckades;
		}

		/*
		 * LaggTillAnvändare är en metod för att lägga till en användare till databasen.
		 * 
		 * in - användaren som ska läggas till databasen 
		 * ut - sann returneras om det lyckades, annars falsk
		 */
		public bool LäggTillAnvändare(Användare användare)
		{
			//InsättProdukt metoden i Databas returnerar sann eller falsk
			bool lyckades = databas.InsättAnvändare(användare);

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
			bool success = databas.TaBortProdukt(id);

			//Databasen läsas om och Produkt sätts till den nya innehåll
			databas.LäsaProdukter();

			return success;
		}

		/*
		 * TaBortAnvändare är en metod för att ta bort en produkt från databasen
		 * och uppdaterar Dictionary produkter.
		 * 
		 * in - id(sträng) av användaren som ska tas bort från databasen 
		 * ut - sann returneras om det lyckades, annars falsk
		 */
		public bool TaBortAnvändare(int id, string användarnamn)
		{
			//Delete metoden i Databas returnerar sann eller falsk
			bool success = databas.TaBortAnvändare(id, användarnamn);

			//Databasen läsas om och AllaAnvändare sätts till den nya innehåll
			databas.LäsaAnvändare();

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
			bool success = databas.UppdateraProdukt(produkt);

			//Databasen läsas om och Produkt sätts till den nya innehåll
			databas.LäsaProdukter();

			return success;
		}

		/*
		 * UppdateraAnvändare är en metod för att ändra en användare i databasen.
		 * 
		 * in - användaren som ska uppdateras i databasen 
		 * ut - sann returneras om det lyckades, annars falsk
		 */
		public bool UppdateraAnvändare(Användare användare)
		{
			//Update metoden i Databas returnerar sann eller falsk
			bool success = databas.UppdateraAnvändare(användare);

			//Databasen läsas om och AllaAnvändare sätts till den nya innehåll
			databas.LäsaAnvändare();

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
			return(databas.Produkter.Equals(produkter));
		}

		/*
		 * SammaAnvändare testar att Dictionary AllaAnvändare är samma som den
		 * i Databas klassen.  Används för test.
		 * Det borde vara en referens till samma objekt så en enklare
		 * Equals (från objekt) kan användas.
		 * 
		 * ut - sann returneras om Dictionary objekten är samma, annars falsk
		 */
		public bool SammaAnvändare()
		{
			return (databas.AllaAnvändare.Equals(allaAnvändare));
		}
	}
}
