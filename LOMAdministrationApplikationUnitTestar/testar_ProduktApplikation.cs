using System;
using System.Collections.Generic; //Dictionary klass
using System.Management; //Behövs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOMAdministrationApplikation;

namespace LOMAdministrationApplikationUnitTestar
{
	/// <summary>
	/// Unittestar för AdministrationApplikation
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

			produkt1.ID = "10000";
			produkt1.Namn = "Test Namn";
			produkt1.Pris = 10.00m;
			produkt1.Typ = "Test Typ";
			produkt1.Färg = "Test Farg";
			produkt1.Bildfilnamn = "Test Bildfilnamn";
			produkt1.Ritningsfilnamn = "Test Ritningsfilnamn";
			produkt1.RefID = "20000";
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
			Assert.IsNotNull(produktApplikationTest.Produkter);
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
			Assert.IsTrue(administrationApplikation.Produkter.Count > 0);
			Assert.IsTrue(administrationApplikation.SammaProdukter());
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
			Assert.IsTrue(TestaAttIDExistera(produkt1.ID, administrationApplikation.Produkter));

			//Tar bort och testar att den är borta
			Assert.IsTrue(administrationApplikation.TaBortProdukt(produkt1.ID));
			administrationApplikation.LäsaFrånDatabas();
			Assert.IsTrue(!TestaAttIDExistera(produkt1.ID, administrationApplikation.Produkter));
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
			Assert.IsTrue(produkt1.Equals(HittaProdukt(produkt1.ID, administrationApplikation.Produkter)));

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
			Assert.IsTrue(!TestaAttIDExistera(produkt1.ID, administrationApplikation.Produkter));
		}

		/*
		 * Hjälpmetod för att testa att en id existera i Dictionary produkter
		 * i ProduktApplikation.
		 */
		private bool TestaAttIDExistera(string id, Dictionary<string,Produkt> produkter)
		{
			bool existera = false;

			foreach (Produkt produkt in produkter.Values)
			{
				if (id.Equals(produkt.ID)) existera = true;
			}

			return existera;
		}

		/*
		 * Hjälpmetod returnera produkten från Dictionary produkter i
		 * ProduktApplikation som matchar id angiven.
		 */
		private Produkt HittaProdukt(string id, Dictionary<string, Produkt> produkter)
		{
			Produkt produkt = new Produkt();

			//Söker efter namnet från comboboxen i produktsamlingen.  Detta gör att
			//namn måste vara unik.
			foreach (Produkt tempProdukt in produkter.Values)
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
