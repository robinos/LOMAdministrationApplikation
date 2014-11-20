using System;
using System.Data.SqlClient; //för Microsoft SQL databas
using System.Data; //för DataSet, DataRow, etc.
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //för MessageBox
using LOMAdministrationApplikation; //för Produkt klassen

namespace LOMAdministrationApplikation
{
	/*
	 * (Kontroller)
	 * Databasklassen hanterar all kontakt med databasen.  Första versionen av
	 * connectionString (instansvariabel) hittade själv databasmappen, men kunde
	 * användas bara för läsning.  Den fick inte tillåtelse till ändringar.
	 * För att ändra databasen krävs det att man ger exakta pathen (som det är nu).
	 * 
	 * ÖppnaKopplingen och StängKopplingen - öppnar/stäng kopplingen till databasen
	 * LäsProdukter - läsar produkter från databasen till Dictionary produkter
	 * InsättProdukt - skriver produkt till databasen och uppdatera Dictionary produkter
	 * UppdateraProdukt - tar bort en produkt från databasen och uppdatera Dictionary produkter
	 * TaBortProdukt - ändra en produkt från databasen och uppdatera Dictionary produkter
	 * ExisterandeID - kollar om en ID redan existerar i databasen
	 * 
	 * Version: 1.0
	 * 2014-10-27
	 * Robin Osborne
	 */
	public class Databas
	{
		//instansvariabler
		//private string connectionString = @"Data Source=(LocalDB)\v11.0;" + "AttachDbFilename=" + Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\TestDatabase.mdf;" + "Integrated Security=True;";
		private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\Eliyat\Documents\Visual Studio 2013\Projects\LjusOchMiljoAB\LjusOchMiljoAB\App_Data\LOM_DB.mdf;" + "Integrated Security=True;";		
		private Dictionary<string, Produkt> produkter;
		private SqlConnection kopplingen;

		/*
		 * Konstruktör för Databasklassen.
		 * En tom Dictionary av produkter, med en index av en id-sträng, skapas.
		 */
		public Databas() 
		{
			//initialisera Dictionary produkter
			this.produkter = new Dictionary<string, Produkt>();
			//initialisera kopplingen till databasen (öppnas inte än)
			kopplingen = new SqlConnection(connectionString);
		}

		//get till Produkter
		public Dictionary<string, Produkt> Produkter
		{
			get
			{
				return produkter;
			}
		}

		/*
		 * ÖppnaKopplingen öppnar kopplingen till databasen.
		 * Om det lyckas, returneras sann.  Om det misslyckas visas en meddelande
		 * för användaren och falsk returneras.
		 * 
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		private bool ÖppnaKopplingen()
		{
			try
			{
				//Öppna kopplingen och skickar tillbaka sann
				kopplingen.Open();
				return true;
			}
			catch (SqlException ex)
			{
				//Öppnades inte.  Skickar tillbaka falsk
				MessageBox.Show("Kan inte koppla till databasen.  Kontakt administratören. " + ex.Message);
				return false;
			}
		}


		/*
		 * StängKopplingen stänger kopplingen till databasen.
		 * Om det lyckas, returneras sann.  Om det misslyckas visas en meddelande
		 * för användaren och falsk returneras.
		 * 
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		private bool StängKopplingen()		
		{
			try
			{
				//Stänger koppling och skickar tillbaka sann
				kopplingen.Close();
				return true;
			}
			catch (SqlException ex)
			{
				//Stängdes inte.  Skickar tillbaka falsk
				MessageBox.Show("Databasen stängdes inte ner! "+ex.Message);
				return false;
			}
		}


		/*
		 * ProduktLasare är en metod för att läsa in värden från databasen till
		 * en bibliotek object.
		 * 
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		public bool LäsaProdukter()
		{
			//Öppna databasen
			bool lyckades = ÖppnaKopplingen();

			//Om man inte kunde öppna databas, sluta och returnera falsk
			if (!lyckades) return false;

			//DataSet är en behållare/mellansteg för inläst databas-data (kan
			//innehålla flera tabeller)
			DataSet dataSet = new DataSet();

			//En ny DataAdapter skapas med ett select sql command redan inbyggt
			//(DataAdapter används sedan för att fylla en DataSet) 
			SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Produkt", kopplingen);

			//Fill metoden på DataAdapter används för att faktiskt utföra fyllning
			//av Datasetet ds från tabellen Produkter
			dataAdapter.Fill(dataSet, "Produkt");

			//Om där finns inga tabeller, returnera falsk
			if (dataSet.Tables.Count == 0)
				return false;

			//Om där finns inga rader i första tabellen, returnera falsk
			var table = dataSet.Tables[0];
			if (table.Rows.Count == 0)
				return false;

			//Om där finns ingen ID kolumnen, returnera falsk
			if (!table.Columns.Contains("ID"))
				return false;

			//Om där finns ingenting i ID kolumnen, returnera falsk
			var row = dataSet.Tables[0].Rows[0];
			if (row.IsNull("ID"))
				return false;

			//Temporär Produkt variabel
			Produkt produktTemp;

			//Loopa genom varje rad i Produkter tabellen (Tables[0] för att Produkter
			//är den första tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Läser värdena från en rad till Produkt objektet
				produktTemp = new Produkt();
				produktTemp.ID = dataRow["ID"].ToString();
				produktTemp.Namn = dataRow["Namn"].ToString();
				produktTemp.Pris = decimal.Parse(dataRow["Pris"].ToString());
				produktTemp.Typ = dataRow["Typ"].ToString();
				produktTemp.Farg = dataRow["Farg"].ToString();
				produktTemp.Bildfilnamn = dataRow["Bildfilnamn"].ToString();
				produktTemp.Ritningsfilnamn = dataRow["Ritningsfilnamn"].ToString();
				produktTemp.RefID = dataRow["RefID"].ToString();
				produktTemp.Beskrivning = dataRow["Beskrivning"].ToString();
				produktTemp.Monteringsbeskrivning = dataRow["Montering"].ToString();

				//Sätt Produkt objekt i Dictionary produkter med id som nyckel
				//(om en id redan finns i produkter, uppdatera bara värden)
				if (!produkter.ContainsKey(produktTemp.ID))
					produkter.Add(produktTemp.ID, produktTemp);
				else
					produkter[produktTemp.ID] = produktTemp;
			}

			//Stäng databasen
			StängKopplingen();

			return lyckades;
		}


		/* 
		 * InsättProdukt är en metod för insättning av värden i databasen.
		 * Den kollar om id finns redan innan med en insert.  Finns det redan,
		 * så görs ingenting.
		 * 
		 * In - Produkt som ska läggas till databasen
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		public bool InsättProdukt(Produkt produkt)
		{
			bool success = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om test datan med id==tempId är inte redan i tabellen, sätt i
			//det, annars gör ingenting (för att undervika en unik id krash)
			if (!ExisterandeID(produkt.ID))
			{
				success = true;
				//SqlCommand föredra allt i en lång sträng
				String sCommandString = "INSERT INTO Produkt (ID, Namn, Pris, Typ, Farg, Bildfilnamn, Ritningsfilnamn, RefID, Beskrivning, Montering) VALUES ('" + produkt.ID + "', '" + produkt.Namn + "', '" + produkt.Pris + "', '" + produkt.Typ + "', '" + produkt.Farg + "', '" + produkt.Bildfilnamn + "', '" + produkt.Ritningsfilnamn + "', '" + produkt.RefID + "', '" + produkt.Beskrivning + "', '" + produkt.Monteringsbeskrivning + "')";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();

				produkter.Add(produkt.ID, produkt);
			}

			//Stäng databasen
			StängKopplingen();

			return success;
		}


		/*
		 * UppdateraProdukt är en metod för updatering av en befintlig värde
		 * i databasen. Den kollar redan att id faktiskt finns innan uppdatering.
		 * Finns det inte, så görs ingenting. 
		 * Uppdateringen kunde göras i en stor kommando, men uppbryten är det både
		 * lättare att läsa och bra om man vill ändra koden för att bara uppdatera
		 * det fältet som ändrades.
		 * 
		 * In - Produkt som ska uppdateras i databasen
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		public bool UppdateraProdukt(Produkt produkt)
		{
			bool lyckades = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==produkt.ID är redan i tabellen, uppdatera den,
			//annars gör ingenting
			if (ExisterandeID(produkt.ID))
			{
				lyckades = true;

				String sCommandString;
				//SqlCommand command;
				SqlCommand kommando;

				//uppdatera namn
				sCommandString = "UPDATE Produkt SET Namn='" + produkt.Namn + "' WHERE ID='" + produkt.ID +"'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera pris
				sCommandString = "UPDATE Produkt SET Pris='" + produkt.Pris + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera typ
				sCommandString = "UPDATE Produkt SET Typ='" + produkt.Typ + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera farg
				sCommandString = "UPDATE Produkt SET Farg='" + produkt.Farg + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera bildfilnamn
				sCommandString = "UPDATE Produkt SET Bildfilnamn='" + produkt.Bildfilnamn + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera ritningsfilnamn
				sCommandString = "UPDATE Produkt SET Ritningsfilnamn='" + produkt.Ritningsfilnamn + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera refID
				sCommandString = "UPDATE Produkt SET RefID='" + produkt.RefID + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera beskrivning
				sCommandString = "UPDATE Produkt SET Beskrivning='" + produkt.Beskrivning + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera monteringsbeskrivning
				sCommandString = "UPDATE Produkt SET Montering='" + produkt.Monteringsbeskrivning + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				produkter[produkt.ID] = produkt;
			}

			//Stäng databasen
			StängKopplingen();

			return lyckades;
		}


		/*
		 * TaBortProdukt är en Metod för att ta bort ett befintligt värde i databasen.
		 * Den kollar redan att id faktiskt finns innan den tas bort. Finns det inte
		 * så görs ingenting. 
		 * 
		 * In - id (sträng) av produkten som ska tas bort
		 * Ut - sann eller falsk för om det lyckades eller inte 
		 */
		public bool TaBortProdukt(string id)
		{
			bool lyckades = false;

			//Öppna databasen och om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==produkt.ID är redan i tabellen, tar bort den,
			//annars gör ingenting
			if (ExisterandeID(id))
			{
				lyckades = true;
				//Tar bort produkten
				string sCommandString = "DELETE FROM Produkt WHERE ID='" + id + "'";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();

				produkter.Remove(id);
			}

			//Stäng databasen
			StängKopplingen();

			return lyckades;
		}


		/*
		 * ExisterandeID är en metod som testar om en produkt ID finns redan i
		 * databasen.
		 * Man kunde bara kolla i Dictionary produkter istället för att läsa om
		 * databasen men det förutsätter att LäsaProdukter har körts innan.
		 * Som det är nu är det oberoende av det.
		 * 
		 * In - id (sträng) av produkten
		 * Ut - sann eller falsk för om den existerar eller inte
		 */
		public bool ExisterandeID(string id)
		{
			//DataSet är en behållare/mellansteg för inläst databas-data (kan innehålla flera tabeller)
			DataSet dataSet = new DataSet();

			//En ny DataAdapter skapas med ett select sql command redan inbyggt
			//(DataAdapter används sedan för att fylla en DataSet) 
			SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Produkt", kopplingen);

			//Fill metoden på DataAdapter används för att faktiskt utföra fyllning
			//av Datasetet ds från tabellen Produkter
			dataAdapter.Fill(dataSet, "Produkter");

			//Om där finns inga tabeller, returnera falsk
			if (dataSet.Tables.Count == 0)
				return false;

			//Om där finns inga rader i första tabellen, returnera falsk
			var table = dataSet.Tables[0];
			if (table.Rows.Count == 0)
				return false;

			//Om där finns ingen ID kolumnen, returnera falsk
			if (!table.Columns.Contains("ID"))
				return false;

			//Om där finns ingenting i ID kolumnen, returnera falsk
			var row = dataSet.Tables[0].Rows[0];
			if (row.IsNull("ID"))
				return false;

			//Bool för att markera att någonting redan existerar i tabellen, från början false 
			bool bExisterar = false;

			//Loopa genom varje rad i Produkter tabellen (Tables[0] för att Produkter
			//är den första och enda tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Om field "id" i en DataRow är lika med den nya tempId, så finns den redan 
				if (dataRow["ID"].ToString() == id)
					bExisterar = true; //bExisterar true innebär att testId redan finns i tabellen
			}

			return bExisterar;
		}

	}
}
