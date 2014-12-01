using System;
using System.Collections.Generic; //Dictionary klass
using System.Management; //Behövs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOMAdministrationApplikation;
using LOMAdministrationApplikation.Models;

namespace LOMAdministrationApplikationUnitTestar
{
	/// <summary>
	/// Unittestar för AdministrationApplikation.
	/// *Viktigt: Det här använder ingen mock men skriver och tar bort
	/// från databasen med indirekt hjälp av Databas klassen!
	/// </summary>
	[TestClass]
	public class testar_AdministrationApplikation
	{
		AdministrationApplikation administrationApplikation;
		Produkt produkt1;

		/*
		 * Initialisation mellan avancerade testar. 
		 */
		public void initialise()
		{
			administrationApplikation = new AdministrationApplikation();
			produkt1 = new Produkt();

			administrationApplikation.LäsaFrånDatabas();

			produkt1.ID = "99999";
			produkt1.Namn = "Test Namn";
			produkt1.Pris = 10.00m;
			produkt1.Typ = "Test Typ";
			produkt1.Färg = "Test Farg";
			produkt1.Bildfilnamn = "Test Bildfilnamn";
			produkt1.Ritningsfilnamn = "Test Ritningsfilnamn";
			produkt1.RefID = "88888";
			produkt1.Beskrivning = "Test Beskrivning";
			produkt1.Monteringsbeskrivning = "Test Monteringsbeskrivning";
		}

		/*
		 * Testar att det går att skapa en AdministrationApplikation objekt.
		 */
		[TestMethod]
		public void test_SkapaNyProduktApplikationObjekt()
		{
			//Skapar Produkt objekt
			AdministrationApplikation produktApplikationTest = new AdministrationApplikation();
			Assert.IsNotNull(produktApplikationTest);
		}

		/*
		 * Testar att det går att läsa från databas utan problem och får en
		 * Dictionary produkter som utdata från Databas klassen.
		 */
		[TestMethod]
		public void test_LasaFranDatabas()
		{
			//Skapar Produkt objekt
			AdministrationApplikation produktApplikationTest = new AdministrationApplikation();
			Assert.IsTrue(produktApplikationTest.LäsaFrånDatabas());
			Assert.IsNotNull(produktApplikationTest.ProduktLista);
		}

		/*
		 * Testar att det går att läsa från databas utan problem och får en
		 * Dictionary produkter som utdata från Databas klassen som faktiskt
		 * innehåller data och är inte bara tom.  Testan även kollar att
		 * Dictionary i ProduktApplikation blir samma som den i Databas klassen. 
		 * Testen kommer att vara falsk med en tom databas.
		 */
		[TestMethod]
		public void test_LasaFranDatabasInteTom()
		{
			initialise();
			Assert.IsTrue(administrationApplikation.ProduktLista.Count > 0);
		}

		/*
		 * Testar LaggTillProdukt och TaBortProdukt i ProduktApplikation
		 */
		[TestMethod]
		public void test_LaggTillOchTarBortProdukt()
		{
			initialise();
			
			//Lägg till och testar att den blev tillagd
			Assert.IsTrue(administrationApplikation.LäggTillProdukt(produkt1));
			administrationApplikation.LäsaFrånDatabas();
			Assert.IsTrue(TestaAttIDExistera(produkt1.ID, administrationApplikation.ProduktLista));

			//Tar bort och testar att den är borta
			Assert.IsTrue(administrationApplikation.TaBortProdukt(produkt1.ID));
			administrationApplikation.LäsaFrånDatabas();
			//Assert.IsTrue(!TestaAttIDExistera(produkt1.ID, administrationApplikation.ProduktLista));
		}

		/*
		 * Testar att LaggTillProdukt i ProduktApplikation inte lägger till vid
		 * existerande id.
		 */
		[TestMethod]
		public void test_InteLaggTillEnExisterande()
		{
			initialise();

			//Lägg till
			administrationApplikation.LäggTillProdukt(produkt1);

			//Lägg till och testar att den blev INTE tillagd
			Assert.IsTrue(!administrationApplikation.LäggTillProdukt(produkt1));

			//Ta bort
			administrationApplikation.TaBortProdukt(produkt1.ID);
		}

		/*
		 * Testar att TaBortProdukt i ProduktApplikation inte ta bort om den inte
		 * finns.
		 */
		[TestMethod]
		public void test_InteTaBortIckeExisterande()
		{
			initialise();

			//Tar bort och testar att ingenting hände
			Assert.IsTrue(!administrationApplikation.TaBortProdukt(produkt1.ID));
		}

		/*
		 * Testar UppdateraProdukt i ProduktApplikation
		 */
		[TestMethod]
		public void test_UppdateraProdukt()
		{
			initialise();

			Assert.IsTrue(administrationApplikation.LäggTillProdukt(produkt1));

			produkt1.Färg = "Farg";
			Assert.IsTrue(administrationApplikation.UppdateraProdukt(produkt1));
			administrationApplikation.LäsaFrånDatabas();
			Assert.IsTrue(produkt1.Equals(HittaProdukt(produkt1.ID, administrationApplikation.ProduktLista)));

			Assert.IsTrue(administrationApplikation.TaBortProdukt(produkt1.ID));
		}

		/*
		 * Testar att UppdateraProdukt i ProduktApplikation inte ändrar om den inte
		 * finns.
		 */
		[TestMethod]
		public void test_UpdateraIckeExisterande()
		{
			initialise();

			//Updatera och testar att uppdatering mislyckades
			produkt1.Färg = "Farg";
			Assert.IsTrue(!administrationApplikation.UppdateraProdukt(produkt1));
			administrationApplikation.LäsaFrånDatabas();
			Assert.IsTrue(!TestaAttIDExistera(produkt1.ID, administrationApplikation.ProduktLista));
		}

		/*
		 * Hjälpmetod för att testa att en id existera i listan produkter.
		 */
		private bool TestaAttIDExistera(string id, List<Produkt> produkter)
		{
			bool existera = false;

			foreach (Produkt produkt in produkter)
			{
				if (id.Equals(produkt.ID)) existera = true;
			}

			return existera;
		}

		/*
		 * Hjälpmetod returnera produkten från listan i som matchar angiven id.
		 */
		private Produkt HittaProdukt(string id, List<Produkt> produkter)
		{
			Produkt produkt = null;

			//Söker efter namnet från comboboxen i produktsamlingen.  Detta gör att
			//namn måste vara unik.
			foreach (Produkt tempProdukt in produkter)
			{
				if (tempProdukt.ID.Equals(id))
				{
					produkt = tempProdukt;
				}
			}

			return produkt;
		}
	}
}
