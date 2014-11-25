using System;
using System.Data.SqlClient; //för Microsoft SQL databas
using System.Data; //för DataSet, DataRow, etc.
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //för MessageBox
using LOMAdministrationApplikation.Models; //för Produkt och Användare klasser

namespace LOMAdministrationApplikation.Controllers
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
	 * Version: 0.2
	 * 2014-11-23
	 * Robin Osborne
	 */
	public class Databas
	{
		//instansvariabler
        //private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\BIlbo\Source\Repos\LjusOchMiljoAB\LjusOchMiljoAB\App_Data\LOM_DB.mdf;" + "Integrated Security=True;";		
		private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\Eliyat\Documents\Visual Studio 2013\Projects\LjusOchMiljoAB\LjusOchMiljoAB\App_Data\LOM_DB.mdf;" + "Integrated Security=True;";	
		private Dictionary<string, Produkt> produkter;
		private Dictionary<string, Användare> allaAnvändare;
		SqlConnection kopplingen;

		//För olika Produkt sidor
		private int totallaSidorProdukter = 1;
		private int produkterPerSida = 5;

		//För olika Användare sidor
		private int totallaSidorAnvändare = 1;
		private int användarePerSida = 5;

		/*
		 * Konstruktör för Databasklassen.
		 * En tom Dictionary av produkter, med en index av en id-sträng, skapas.
		 */
		public Databas() 
		{
			//initialisera Dictionary produkter
			this.produkter = new Dictionary<string, Produkt>();
			//initialisera Dictionary användare
			this.allaAnvändare = new Dictionary<string, Användare>();
			//initialisera kopplingen till databasen (öppnas inte än)
			kopplingen = new SqlConnection(connectionString);
		}

		//egenskap för Produkt dictionary
		public Dictionary<string, Produkt> Produkter
		{
			get
			{
				return produkter;
			}
		}

		//egenskap för Användare dictionary
		public Dictionary<string, Användare> AllaAnvändare
		{
			get
			{
				return allaAnvändare;
			}
		}

		//egenskap för totalla sidor av produkter
		public int TotallaSidorProdukter
		{
			get
			{
				return totallaSidorProdukter;
			}
		}

		//egenskap för antal produkter per sida
		public int ProdukterPerSida
		{
			get
			{
				return produkterPerSida;
			}
		}

		//egenskap för totalla sidor av användare
		public int TotallaSidorAnvändare
		{
			get
			{
				return totallaSidorAnvändare;
			}
		}

		//egenskap för antal användare per sida
		public int AnvändarePerSida
		{
			get
			{
				return användarePerSida;
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
			catch (InvalidOperationException ex)
			{
				//Öppnades inte.  Skickar tillbaka falsk
				MessageBox.Show("Databasen var redan öppen. " + ex.Message);
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
		 * HämtaDataSetProdukter hämtar datan från databasen till en data
		 * set mha en adapter.
		 * 
		 * in:	kommando: SELECT kommando man ska exekvera som sträng
		 *		tabell: tabellnamn man tar data ifrån som sträng
		 * ut: en DataSet enligt kommando på tabellen
		 */
		public DataSet HämtaDataSetProdukter(string kommando, string tabell)
		{
			//DataSet är en behållare/mellansteg för inläst databas-data (kan
			//innehålla flera tabeller)
			DataSet dataSet = new DataSet();

			//En ny DataAdapter skapas med ett select sql command redan inbyggt
			//(DataAdapter används sedan för att fylla en DataSet) 
			SqlDataAdapter dataAdapter = new SqlDataAdapter(kommando, kopplingen);

			//Fill metoden på DataAdapter används för att faktiskt utföra fyllning
			//av Datasetet ds från tabellen Produkt
			dataAdapter.Fill(dataSet, tabell);

			//Fria upp minnet för adapter
			dataAdapter.Dispose();

			return dataSet;
		}

		/*
		 * TestaDataSetKolumn testar en kolumn på en data set för att försäkra
		 * att den existera och innehåller data.
		 * 
		 * in:	dataSet: dataSet objekt man ska testa
		 *		kolumn: namnet på kolumnen man vill testa som sträng
		 * ut: sant och det lyckades och annars falskt
		 */
		public bool TestaDataSetKolumn(DataSet dataSet, string kolumn)
		{
			//Om där finns inga tabeller, returnera falsk
			if (dataSet.Tables.Count == 0)
				return false;

			//Om där finns inga rader i första tabellen returnera falsk
			var table = dataSet.Tables[0];
			if (table.Rows.Count == 0)
				return false;

			//Om där finns ingen kolumn med namnet returnera falsk
			if (!table.Columns.Contains(kolumn))
				return false;

			//Om där finns ingen data i kolumnen returnera falsk
			var row = dataSet.Tables[0].Rows[0];
			if (row.IsNull(kolumn))
				return false;

			return true;
		}

		/*
		 * LäsaProdukter är en metod för att läsa in värden från Produkt
		 * tabellen till en dictionary object.
		 * 
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		public bool LäsaProdukter()
		{
			bool lyckades = true;
			string kolumnID = "ID";
			string kolumnNamn = "Namn";
			string kommando = "SELECT * FROM Produkt";
			string tabellnamn = "Produkt";

			//Öppna databasen
			//Om man inte kunde öppna databas sluta och returnera falsk
			if (ÖppnaKopplingen() == false)
				return false;

			//Hämta dataset för produkter
			DataSet dataSet = HämtaDataSetProdukter(kommando, tabellnamn);

			//Testa att ID kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnID))
			{
				//Stäng databasen
				StängKopplingen();
				return false;
			}

			//Testa att Namn kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnNamn))
			{
				//Stäng databasen
				StängKopplingen();
				return false;
			}

			//Räknar hur många sidor av produkter det blir totallt
			totallaSidorProdukter = RäknarTotallaSidor(dataSet, tabellnamn, totallaSidorProdukter, produkterPerSida);

			//Temporär Produkt variabel
			Produkt produktTemp;

			//Loopa genom varje rad i Produkt tabellen (Tables[0] för att Produkter
			//är den enda tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Läser värdena från en rad till Produkt objektet
				produktTemp = new Produkt();
				produktTemp.ID = dataRow["ID"].ToString();
				produktTemp.Namn = dataRow["Namn"].ToString();
				produktTemp.Pris = Decimal.Parse(dataRow["Pris"].ToString());
				produktTemp.Typ = dataRow["Typ"].ToString();
				produktTemp.Färg = dataRow["Farg"].ToString();
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
		 * 
		 */
		public Dictionary<string, Produkt> HämtaSidaProdukter(int sida)
		{
			string kolumnID = "ID";
			string kolumnNamn = "Namn";

			Dictionary<string, Produkt> tempProdukter = new Dictionary<string, Produkt>();
			string tabellnamn = "Produkt";
			string kommando;

			if (sida == 1)
			{
				kommando = "Select TOP " + produkterPerSida + " * from Produkt ORDER BY ID";
			}
			else
			{
				int tidigareSidaProdukter = (sida - 1) * produkterPerSida;

				kommando = "Select TOP " + produkterPerSida + " * from Produkt WHERE ID NOT IN " +
					"(Select TOP " + tidigareSidaProdukter + " ID from Produkt ORDER BY ID)";
			}

			ÖppnaKopplingen();

			DataSet dataSet = HämtaDataSetProdukter(kommando, tabellnamn);

			//Testa att ID kolumnen existera
			//Om något är fel sluta och returnera null
			if (!TestaDataSetKolumn(dataSet, kolumnID))
			{
				//Stäng databasen
				StängKopplingen();
				return null;
			}

			//Testa att Namn kolumnen existera
			//Om något är fel sluta och returnera null
			if (!TestaDataSetKolumn(dataSet, kolumnNamn))
			{
				//Stäng databasen
				StängKopplingen();
				return null;
			}

			//Temporär Produkt variabel
			Produkt produktTemp;

			//Loopa genom varje rad i Produkt tabellen (Tables[0] för att Produkter
			//är den enda tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Läser värdena från en rad till Produkt objektet
				produktTemp = new Produkt();
				produktTemp.ID = dataRow["ID"].ToString();
				produktTemp.Namn = dataRow["Namn"].ToString();
				produktTemp.Pris = Decimal.Parse(dataRow["Pris"].ToString());
				produktTemp.Typ = dataRow["Typ"].ToString();
				produktTemp.Färg = dataRow["Farg"].ToString();
				produktTemp.Bildfilnamn = dataRow["Bildfilnamn"].ToString();
				produktTemp.Ritningsfilnamn = dataRow["Ritningsfilnamn"].ToString();
				produktTemp.RefID = dataRow["RefID"].ToString();
				produktTemp.Beskrivning = dataRow["Beskrivning"].ToString();
				produktTemp.Monteringsbeskrivning = dataRow["Montering"].ToString();

				//Sätt Produkt objekt i Dictionary produkter med id som nyckel
				//(om en id redan finns i produkter, uppdatera bara värden)
				if (!tempProdukter.ContainsKey(produktTemp.ID))
					tempProdukter.Add(produktTemp.ID, produktTemp);
				else
					tempProdukter[produktTemp.ID] = produktTemp;
			}

			//Stäng databasen
			StängKopplingen();

			produkter = tempProdukter;

			return tempProdukter;
		}

		/*
		 * LäsaAnvändare är en metod för att läsa in värden från databasen till
		 * en dictionary object.
		 * 
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		public bool LäsaAnvändare()
		{
			bool lyckades = true;
			string kolumnID = "ID";
			string kolumnNamn = "Anvandarnamn";
			string kommando = "SELECT * FROM Anvandare";
			string tabellnamn = "Anvandare";

			//Öppna databasen
			//Om man inte kunde öppna databas sluta och returnera falsk
			if (ÖppnaKopplingen() == false)
				return false;

			//Hämta dataset för användare
			DataSet dataSet = HämtaDataSetProdukter(kommando, tabellnamn);

			//Testa att ID kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnID))
			{
				//Stäng databasen
				StängKopplingen();
				return false;
			}

			//Testa att Namn kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnNamn))
			{
				//Stäng databasen
				StängKopplingen();
				return false;
			}

			//Räknar hur många sidor av användare det blir totallt
			totallaSidorAnvändare = RäknarTotallaSidor(dataSet, tabellnamn, totallaSidorAnvändare, användarePerSida);

			//Temporär Produkt variabel
			Användare användareTemp;

			//Loopa genom varje rad i Anvandare tabellen (Tables[0] för att Anvandare
			//är den enda tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Läser värdena från en rad till Produkt objektet
				användareTemp = new Användare();
				användareTemp.ID = int.Parse(dataRow["ID"].ToString());
				användareTemp.Användarnamn = dataRow["Anvandarnamn"].ToString();
				användareTemp.LösenordHash = dataRow["LosenordHash"].ToString();
				användareTemp.Roll = dataRow["Roll"].ToString();
				användareTemp.Räknare = int.Parse(dataRow["Raknare"].ToString());
				användareTemp.Låste = bool.Parse(dataRow["Laste"].ToString());

				//Sätt Användare objekt i Dictionary användare med användarnamn
				//som nyckel (om en användarnamn redan finns i användare, uppdatera
				//bara värden)
				if (!allaAnvändare.ContainsKey(användareTemp.Användarnamn))
					allaAnvändare.Add(användareTemp.Användarnamn, användareTemp);
				else
					allaAnvändare[användareTemp.Användarnamn] = användareTemp;
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
			bool lyckades = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om test datan med id==tempId är inte redan i tabellen, sätt i
			//det, annars gör ingenting (för att undervika en unik id krash)
			if (!ExisterandeProdukt(produkt.ID, produkt.Namn))
			{
				lyckades = true;
				//SqlCommand föredra allt i en lång sträng
				String sCommandString = "INSERT INTO Produkt (ID, Namn, Pris, Typ, Farg, Bildfilnamn, Ritningsfilnamn, RefID, Beskrivning, Montering) VALUES ('" + produkt.ID + "', '" + produkt.Namn + "', " + " TRY_PARSE('" + produkt.Pris + "' AS DECIMAL(10, 2) using 'sv-SE'), '" + produkt.Typ + "', '" + produkt.Färg + "', '" + produkt.Bildfilnamn + "', '" + produkt.Ritningsfilnamn + "', '" + produkt.RefID + "', '" + produkt.Beskrivning + "', '" + produkt.Monteringsbeskrivning + "')";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();

				produkter.Add(produkt.ID, produkt);
			}

			//Stäng databasen
			StängKopplingen();

			return lyckades;
		}

		/* 
		 * InsättAnvändare är en metod för insättning av värden i databasen.
		 * Den kollar om id finns redan innan med en insert.  Finns det redan,
		 * så görs ingenting.
		 * 
		 * In - Användare som ska läggas till databasen
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		public bool InsättAnvändare(Användare användare)
		{
			bool success = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om test datan med id==tempId är inte redan i tabellen, sätt i
			//det, annars gör ingenting (för att undervika en unik id krash)
			if (!ExisterandeAnvändare(användare.ID, användare.Användarnamn))
			{
				success = true;
				//SqlCommand föredra allt i en lång sträng
				String sCommandString = "INSERT INTO Anvandare (ID, Anvandarnamn, LosenordHash, Roll, Raknare, Laste) VALUES ('" + användare.ID + "', '" + användare.Användarnamn + "', '" + användare.LösenordHash + "', '" + användare.Roll + "', '" + användare.Räknare + "', '" + användare.Låste + "')";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();

				this.allaAnvändare.Add(användare.Användarnamn, användare);
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
			if (ExisterandeProdukt(produkt.ID, produkt.Namn))
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
				sCommandString = "UPDATE Produkt SET Pris=" + " TRY_PARSE('" + produkt.Pris + "' AS DECIMAL(10, 2) using 'sv-SE')" + " WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera typ
				sCommandString = "UPDATE Produkt SET Typ='" + produkt.Typ + "' WHERE ID='" + produkt.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera farg
				sCommandString = "UPDATE Produkt SET Farg='" + produkt.Färg + "' WHERE ID='" + produkt.ID + "'";
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
		 * UppdateraAnvändare är en metod för updatering av en befintlig värde
		 * i databasen. Den kollar redan att id faktiskt finns innan uppdatering.
		 * Finns det inte, så görs ingenting. 
		 * Uppdateringen kunde göras i en stor kommando, men uppbryten är det både
		 * lättare att läsa och bra om man vill ändra koden för att bara uppdatera
		 * det fältet som ändrades.
		 * 
		 * In - Användare som ska uppdateras i databasen
		 * Ut - sann eller falsk för om det lyckades eller inte
		 */
		public bool UppdateraAnvändare(Användare användare)
		{
			bool lyckades = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==användare.ID är redan i tabellen, uppdatera den,
			//annars gör ingenting
			if (ExisterandeAnvändare(användare.ID, användare.Användarnamn))
			{
				lyckades = true;

				String sCommandString;
				//SqlCommand command;
				SqlCommand kommando;

				//uppdatera användarnamn
				sCommandString = "UPDATE Anvandare SET Anvandarnamn='" + användare.Användarnamn + "' WHERE ID='" + användare.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera lösenordens hash
				sCommandString = "UPDATE Anvandare SET LosenordHash='" + användare.LösenordHash + "' WHERE ID='" + användare.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera roll
				sCommandString = "UPDATE Anvandare SET Roll='" + användare.Roll + "' WHERE ID='" + användare.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera räknare
				sCommandString = "UPDATE Anvandare SET Raknare='" + användare.Räknare + "' WHERE ID='" + användare.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				//uppdatera låste
				sCommandString = "UPDATE Anvandare SET Laste='" + användare.Låste + "' WHERE ID='" + användare.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();

				allaAnvändare[användare.Användarnamn] = användare;
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
		public bool TaBortProdukt(string id, string namn)
		{
			bool lyckades = false;

			//Öppna databasen och om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==produkt.ID är redan i tabellen, tar bort den,
			//annars gör ingenting
			if (ExisterandeProdukt(id, namn))
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
		 * TaBortAnvändare är en Metod för att ta bort ett befintligt värde i databasen.
		 * Den kollar redan att id faktiskt finns innan den tas bort. Finns det inte
		 * så görs ingenting. 
		 * 
		 * In - id (sträng) av användaren som ska tas bort
		 * Ut - sann eller falsk för om det lyckades eller inte 
		 */
		public bool TaBortAnvändare(int id, string användarnamn)
		{
			bool lyckades = false;

			//Öppna databasen och om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==användare.ID är redan i tabellen, tar bort den,
			//annars gör ingenting
			if (ExisterandeAnvändare(id, användarnamn))
			{
				lyckades = true;
				//Tar bort användaren
				string sCommandString = "DELETE FROM Anvandare WHERE ID='" + id + "'";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();

				allaAnvändare.Remove(användarnamn);
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
		public bool ExisterandeProdukt(string id, string namn)
		{
			string kolumnID = "ID";
			string kolumnNamn = "Namn";
			string kommando = "SELECT * FROM Produkt";
			string tabellnamn = "Produkt";

			//Hämta dataset för produkter
			//DataSet är en behållare/mellansteg för inläst databas-data (kan innehålla flera tabeller)
			DataSet dataSet = HämtaDataSetProdukter(kommando, tabellnamn);

			//Testa att ID kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnID))
			{
				return false;
			}

			//Testa att Namn kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnNamn))
			{
				return false;
			}

			//Bool för att markera att någonting redan existerar i tabellen, från början false 
			bool bExisterar = false;

			//Loopa genom varje rad i Produkter tabellen (Tables[0] för att Produkter
			//är den första och enda tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Om field "id" i en DataRow är lika med den nya tempId, så finns den redan 
				if (dataRow["ID"].ToString() == id && dataRow["Namn"].ToString().Equals(namn))
					bExisterar = true; //bExisterar true innebär att testId redan finns i tabellen
			}

			return bExisterar;
		}

		/*
		 * ExisterandeAnvändarnamn är en metod som testar om en produkt ID finns redan i
		 * databasen.
		 * Man kunde bara kolla i Dictionary produkter istället för att läsa om
		 * databasen men det förutsätter att LäsaAnvändare har körts innan.
		 * Som det är nu är det oberoende av det.
		 * 
		 * In - id (int) och användarnamn (sträng) av användaren
		 * Ut - sann eller falsk för om den existerar eller inte
		 */
		public bool ExisterandeAnvändare(int id, string användarnamn)
		{
			string kolumnID = "ID";
			string kolumnNamn = "Anvandarnamn";
			string kommando = "SELECT * FROM Anvandare";
			string tabellnamn = "Anvandare";

			//Hämta dataset för produkter
			//DataSet är en behållare/mellansteg för inläst databas-data (kan innehålla flera tabeller)
			DataSet dataSet = HämtaDataSetProdukter(kommando, tabellnamn);

			//Testa att ID kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnID))
			{
				return false;
			}

			//Testa att Namn kolumnen existera
			//Om något är fel sluta och returnera falsk
			if (!TestaDataSetKolumn(dataSet, kolumnNamn))
			{
				return false;
			}

			//Bool för att markera att någonting redan existerar i tabellen, från början false 
			bool bExisterar = false;

			//Loopa genom varje rad i Produkter tabellen (Tables[0] för att Produkter
			//är den första och enda tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Om field "ID" i en DataRow är lika med id så finns den redan 
				if (int.Parse(dataRow["ID"].ToString()) == id)
					bExisterar = true; //bExisterar true innebär att testId redan finns i tabellen
				//Om field "ID" i en DataRow är lika med användarnamn så finns den redan 
				if (dataRow["Anvandarnamn"].ToString() == användarnamn)
					bExisterar = true; //bExisterar true innebär att testId redan finns i tabellen
			}

			return bExisterar;
		}

		/*
		 * 
		 */
		private int RäknarTotallaSidor(DataSet dataSet, string tabellnamn, int totallaSidor, int elementPerSida)
		{
			int rowCount = dataSet.Tables[tabellnamn].Rows.Count;
			totallaSidor = rowCount / elementPerSida;
			// if any row left after calculated pages, add one more page 
			if (rowCount % elementPerSida > 0)
				totallaSidor += 1;
			return totallaSidor;
		}
	}
}
