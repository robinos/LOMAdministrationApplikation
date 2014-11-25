using System;
using System.Management; //Behövs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LOMAdministrationApplikation.Models;

namespace LOMAdministrationApplikationUnitTestar
{
	/// <summary>
	/// Unittestar för Produkt.  Skapelse, tilldelning, och Equals metoden (och
	/// indirekt HashCode metoden) testas. Produkt gör inga testar på att värdena
	/// ska vara giltiga, så det testas inte.  
	/// </summary>
	[TestClass]
	public class testar_Produkt
	{
		private Produkt produkt1;
		private Produkt produkt2;

		/*
		 * Initialisation mellan avancerade testar.
		 */
		public void initialise()
		{
			produkt1 = new Produkt();
			produkt2 = new Produkt();

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

			produkt2.ID = "10000";
			produkt2.Namn = "Test Namn";
			produkt2.Pris = 10.00m;
			produkt2.Typ = "Test Typ";
			produkt2.Färg = "Test Farg";
			produkt2.Bildfilnamn = "Test Bildfilnamn";
			produkt2.Ritningsfilnamn = "Test Ritningsfilnamn";
			produkt2.RefID = "20000";
			produkt2.Beskrivning = "Test Beskrivning";
			produkt2.Monteringsbeskrivning = "Test Monteringsbeskrivning";
		}

		/*
		 * Testar att det går att skapa en ny Produkt objekt.
		 */
		[TestMethod]
		public void test_SkapaNyProduktObjekt()
		{
			Produkt produkt = new Produkt();
			//Skapar Produkt objekt
			Assert.IsNotNull(produkt);
		}

		/*
		 * Testar att det går att tilldela värden till en produkt objekt.
		 */
		[TestMethod]
		public void test_SetGetValidVariabler()
		{
			initialise();
			Assert.AreEqual(produkt1.ID, "10000");
			Assert.AreEqual(produkt1.Namn, "Test Namn");
			Assert.AreEqual(produkt1.Pris, 10.00m);
			Assert.AreEqual(produkt1.Typ, "Test Typ");
			Assert.AreEqual(produkt1.Färg, "Test Farg");
			Assert.AreEqual(produkt1.Bildfilnamn, "Test Bildfilnamn");
			Assert.AreEqual(produkt1.Ritningsfilnamn, "Test Ritningsfilnamn");
			Assert.AreEqual(produkt1.RefID, "20000");
			Assert.AreEqual(produkt1.Beskrivning, "Test Beskrivning");
			Assert.AreEqual(produkt1.Monteringsbeskrivning, "Test Monteringsbeskrivning");
		}

		/*
		 * Testar att produkt1 och produkt2 anses lika av Produkts Equals metod.
		 */
		[TestMethod]
		public void test_ArProduktLika()
		{
			initialise();
			Assert.IsTrue(produkt1.Equals(produkt2));
		}

		/*
		 * Olika ID ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_ID()
		{
			initialise();
			produkt2.ID = "20000";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika Namn ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_Namn()
		{
			initialise();
			produkt2.Namn = "Bob 20000";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika Typ ger inte lika 
		 */
		[TestMethod]
		public void test_ArProduktOlika_Typ()
		{
			initialise();
			produkt2.Typ = "utomhus armatur";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika Farg ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_Farg()
		{
			initialise();
			produkt2.Färg = "grön-blå";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika Bildfilnamn ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_Bildfilnamn()
		{
			initialise();
			produkt2.Bildfilnamn = "20000bild";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika Ritsningsfilnamn ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_Ritningsfilnamn()
		{
			initialise();
			produkt2.Ritningsfilnamn = "20000ritning";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika RefID ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_RefID()
		{
			initialise();
			produkt2.RefID = "30000";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika Beskrivningar ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_Beskrivning()
		{
			initialise();
			produkt2.Beskrivning = "Fantastiska lampa!";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}

		/*
		 * Olika Monteringsbeskrivningar ger inte lika
		 */
		[TestMethod]
		public void test_ArProduktOlika_Monteringsbeskrivning()
		{
			initialise();
			produkt2.Monteringsbeskrivning = "Sätt del A på basen enligt figur 3 på ritningen.";
			Assert.IsTrue(!produkt1.Equals(produkt2));
		}
	}
}
