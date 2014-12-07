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
	/// <summary>
	/// (Kontroller)
	/// 
	/// Databas klassen hanterar allt kontakt med databasen.  Det finns två
	/// tabeller i databasen.  Produkt tabellen innehåller alla produkter och
	/// Anvandare tabellen innehåller alla användare som få tillåtelse att logga
	/// in till webbapplikationen.
	/// 
	/// -Egenskaper-
	/// ProduktLista - get - lista av alla produkter i databasen
	/// AnvändarLista - get - lista av alla användare i databasen
	/// HögstaAnvändareID - get - högsta id för alla användare i databasen/listan
	/// 
	/// -Konstruktör-
	/// Databas - initialisera produkt och användare listor och kopplingen
	/// 
	/// -Metoder-
	/// ÖppnaKopplingen och StängKopplingen - öppnar/stäng kopplingen till
	///		databasen
	/// HämtaDataSet - hämtar en DataSet med all data i angiven tabell
	/// TestaDataSetKolumn - Testar en kolumn i en DataSet för att se om den
	///		existera
	///	TestaKolumnData - Testar att en kolumn har data i någon rad
	/// DefaultProdukt skapar en default produkt för Produkt tabellen för om
	///		den är helt tom
	/// LäsProdukter - läsar produkter från databasen till produktListan
	/// InsättProdukt - skriver produkt till databasen och uppdatera
	///		produktListan
	/// UppdateraProdukt - tar bort en produkt från databasen och uppdatera
	///		produktListan
	///	TaBortProdukt - ändra en produkt från databasen och uppdatera
	///		produktListan
	///	ExisterandeProdukt - kollar om en produkt redan existerar i databasen
	///	HämtaProduktMedID - hämtar en produkt från listan som har angiven id
	/// DefaultAnvändare skapar en default användare för Anvandare tabellen för
	///		om den är helt tom
	/// LäsAnvändare - läsar användare från databasen till användarListan
	/// InsättAnvändare - skriver användare till databasen och uppdatera
	///		användarListan
	/// UppdateraAnvändare - tar bort en användare från databasen och uppdatera
	///		användarListan
	///	TaBortAnvändare - ändra en användare från databasen och uppdatera
	///		användarListan
	///	ExisterandeAnvändare - kollar om en användare redan existerar i databasen
	///	HämtaAnvändareMedID - hämtar en användare från listan som har angiven id
	/// 
	/// Version: 0.5
	/// 2014-12-07
	/// Grupp 2
	/// </summary>
	public class Databas
	{
		//instansvariabler
        //kopplingssträngen till databasen
		//private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\BIlbo\Source\Repos\LjusOchMiljoAB\LjusOchMiljoAB\App_Data\LOM_DB.mdf;" + "Integrated Security=True;";		
		private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + @"C:\Users\Eliyat\Documents\Visual Studio 2013\Projects\LjusOchMiljoAB\LjusOchMiljoAB\App_Data\LOM_DB.mdf;" + "Integrated Security=True;";	
		//lista för att hålla alla produkter från Produkttabellen
		private List<Produkt> produktLista;
		//lista för att hålla alla användare från Användartabellen
		private List<Användare> användarLista;
		//kopplingen till databasen
		SqlConnection kopplingen;
		//Högsta ID värde för alla användare
		private int högstaAnvändareID = 0;

		/// <summary>
		/// Get egenskap för listan av alla produkter i databasen
		/// </summary>
		public List<Produkt> ProduktLista
		{
			get { return produktLista; }
		}

		/// <summary>
		/// Get egenskap för listan av alla ánvändare i databasen
		/// </summary>
		public List<Användare> AnvändarLista
		{
			get { return användarLista; }
		}

		/// <summary>
		/// Get och Set egenskap för högsta ID värde av en användare
		/// </summary>
		public int HögstaAnvändareID
		{
			get { return högstaAnvändareID; }
		}

		/// <summary>
		/// Konstruktör för Databas initialisera listor av produkter och
		/// användare och kopplingen till databasen.
		/// </summary>
		public Databas()
		{
			//initialisera en lista av produkter
			this.produktLista = new List<Produkt>();
			//initialisera en lista av användare
			this.användarLista = new List<Användare>();
			//initialisera kopplingen till databasen (öppnas inte än)
			kopplingen = new SqlConnection(connectionString);
		}

		/// <summary>
		/// ÖppnaKopplingen öppnar kopplingen till databasen. Om det lyckas
		/// returneras sann. Om det misslyckas visas en meddelande för
		/// användaren och falsk returneras.
		/// </summary>
		/// <returns>sann om det lyckades och annars falsk</returns>
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
				MessageBox.Show("Databasen var redan öppen eller kan inte öppnas av andra skäll. " + ex.Message);
				//Öppnades inte.  Skickar tillbaka falsk
				return false;
			}
		}

		/// <summary>
		/// StängKopplingen stänger kopplingen till databasen. Om det lyckas
		/// returneras sann. Om det misslyckas visas en meddelande för
		/// användaren och falsk returneras.
		/// </summary>
		/// <returns>sann om det lyckades och annars falsk</returns>
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

		/// <summary>
		/// HämtaDataSet hämtar datan från databasen till en data set med
		/// hjälp av en Adapter objekt och angivna kommando och tabell.
		/// </summary>
		/// <param name="kommando">SELECT kommandot som sträng</param>
		/// <param name="tabell">Namn av tabellen som ska läsas</param> 
		/// <returns>integer värdet för totalla sidor</returns>
		public DataSet HämtaDataSet(string kommando, string tabell)
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

			//Fria upp minnet för adaptern
			dataAdapter.Dispose();

			return dataSet;
		}

		/// <summary>
		/// TestaDataSetKolumn testar en kolumn på en data set för att
		/// försäkra att den existera.
		/// </summary>
		/// <param name="dataSet">DataSet som ska testas</param>
		/// <param name="kolumn">Namn av kolumnen</param> 
		/// <returns>sann och kolumnen existera och annars falsk</returns>
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

			return true;
		}

		/// <summary>
		/// TestaKolumnData testar en kolumn på en data set för att
		/// försäkra att den innehåller data.
		/// </summary>
		/// <param name="dataSet">DataSet som ska testas</param>
		/// <param name="kolumn">Namn av kolumnen</param> 
		/// <returns>sann och datan existera och annars falsk</returns>
		public bool TestaKolumnData(DataSet dataSet, string kolumn)
		{
			//Om där finns ingen data i kolumnen returnera falsk
			var row = dataSet.Tables[0].Rows[0];
			if (row.IsNull(kolumn))
				return false;

			return true;
		}

		/// <summary>
		/// DefaultProdukt skapar en default produkt för Produkt
		/// tabellen för om den är helt tom.
		/// </summary>
		/// <returns>en Produkt med default värden</returns>
		public Produkt DefaultProdukt()
		{
			Produkt produkt = new Produkt();
			string id = "00000";

			produkt.ID = id;
			produkt.Namn = id;
			produkt.Pris = 0.00m;
			produkt.Färg = id;
			produkt.Typ = id;
			produkt.Bildfilnamn = id;
			produkt.Ritningsfilnamn = id;
			produkt.RefID = id;
			produkt.Monteringsbeskrivning = id;
			produkt.Beskrivning = id;

			return produkt;
		}

		/// <summary>
		/// LäsaProdukter är en metod för att läsa in värden från Produkt
		/// tabellen till en lista av produkter.
		/// </summary>
		/// <returns>sann om man kunde läsa produkter och annars falsk</returns>
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
			DataSet dataSet = HämtaDataSet(kommando, tabellnamn);

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

			//Om där finns ingen data i databasen i nyckelkolumnen sätt
			//i en default produkt
			if (!TestaKolumnData(dataSet, kolumnID))
			{
				StängKopplingen();
				InsättProdukt(DefaultProdukt());
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

				//Sätt Produkt objekt i Lista produktLista
				//(om en id redan finns i produkttLista ersätt produkten)
				if (!ExisterandeProdukt(produktTemp.ID))
					produktLista.Add(produktTemp);
				else
				{
					produktLista.Remove(HämtaProduktMedID(produktTemp.ID));
					produktLista.Add(produktTemp);
				}
			}

			//Stäng databasen
			StängKopplingen();

			return lyckades;
		}

		/// <summary>
		/// InsättProdukt är en metod för insättning av en produkt i databasen.
		/// Den kollar om id finns redan i listan innan insättning.
		/// Om produkten redan finns gör metoden ingenting  och falsk returneras.
		/// </summary>
		/// <param name="produkt">Produkten som ska läggas till</param>
		/// <returns>sann om produkten lades till och annars falsk</returns>
		public bool InsättProdukt(Produkt produkt)
		{
			bool lyckades = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om test datan med id==tempId är inte redan i tabellen, sätt i
			//det, annars gör ingenting (för att undervika en unik id krash)
			if (!ExisterandeProdukt(produkt.ID))
			{
				lyckades = true;
				//SqlCommand föredra allt i en lång sträng
				String kommandSträng = "INSERT INTO Produkt (ID, Namn, Pris, Typ, Farg, Bildfilnamn, Ritningsfilnamn, RefID, Beskrivning, Montering) VALUES ('" + produkt.ID + "', '" + produkt.Namn + "', " + " TRY_PARSE('" + produkt.Pris + "' AS DECIMAL(10, 2) using 'sv-SE'), '" + produkt.Typ + "', '" + produkt.Färg + "', '" + produkt.Bildfilnamn + "', '" + produkt.Ritningsfilnamn + "', '" + produkt.RefID + "', '" + produkt.Beskrivning + "', '" + produkt.Monteringsbeskrivning + "')";
				SqlCommand kommando = new SqlCommand(kommandSträng, kopplingen);
				kommando.ExecuteNonQuery();
			}

			//Stäng databasen
			StängKopplingen();

			if(lyckades)
			{
				//Läsa om för att sätta om listan (man kan inte vara säkert att
				//det verkligen har skrivits till databasen)
				lyckades = LäsaProdukter();
			}

			return lyckades;
		}

		/// <summary>
		/// UppdateraProdukt är en metod för uppdatering av en befintlig produkt
		/// i databasen. Den kollar redan att id faktiskt finns innan uppdatering.
		/// Om produkten inte redan finns gör metoden ingenting och falsk returneras.
		/// </summary>
		/// <param name="produkt">Produkten som ska uppdateras</param>
		/// <returns>sann om produkten uppdaterades och annars falsk</returns>
		public bool UppdateraProdukt(Produkt produkt)
		{
			bool lyckades = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om id redan finns i produktListan
			if (ExisterandeProdukt(produkt.ID))
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
			}

			produktLista.OrderBy(n => n.Namn);

			//Stäng databasen
			StängKopplingen();

			if(lyckades)
			{
				//Läsa om för att sätta om listan (man kan inte vara säkert att
				//det verkligen har uppdaterats i databasen)
				lyckades = LäsaProdukter();
			}

			return lyckades;
		}

		/// <summary>
		/// TaBortProdukt är en metod för att ta bort en befintlig produkt
		/// i databasen. Den kollar att id redan finns innan uppdatering.
		/// Om produkten inte redan finns gör metoden ingenting  och falsk
		/// returneras.
		/// </summary>
		/// <param name="produkt">Produkten som ska tas bort</param>
		/// <returns>sann om produkten har tagits bort och annars falsk</returns>
		public bool TaBortProdukt(string id)
		{
			bool lyckades = false;

			//Öppna databasen och om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==produkt.ID är redan i tabellen, tar bort den,
			//annars gör ingenting
			if (ExisterandeProdukt(id))
			{
				lyckades = true;
				//Tar bort produkten
				string sCommandString = "DELETE FROM Produkt WHERE ID='" + id + "'";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();
			}

			//Stäng databasen
			StängKopplingen();

			if(lyckades)
			{
				//Läsa om för att sätta om listan (man kan inte vara säkert att
				//det verkligen har tagits bort från databasen)
				produktLista.Remove(HämtaProduktMedID(id));
				lyckades = LäsaProdukter();
			}

			return lyckades;
		}

		/// <summary>
		/// ExisterandeProdukt är en metod som testar om en produkt ID finns
		/// redan i produktListan.  
		/// </summary>
		/// <param name="id">id för en möjlig produkt</param>
		/// <returns>sann om produkten med id-värdet existera och annars falsk</returns>
		public bool ExisterandeProdukt(string id)
		{
			//Bool för att markera att någonting redan existerar i tabellen, från början false 
			bool bExisterar = false;

			foreach (Produkt produkt in produktLista)
			{
				if (produkt.ID.Equals(id))
					bExisterar = true;
			}

			return bExisterar;
		}

		/// <summary>
		/// HämtaProduktMedID är en metod som hämtar en produkt från listan
		/// med angiven id eller null om ingen hittades. 
		/// </summary>
		/// <param name="id">id för en möjlig produkt</param>
		/// <returns>Produkt objektet om den hittades och annars null</returns>
		public Produkt HämtaProduktMedID(string id)
		{
			Produkt tempProdukt = null;

			foreach (Produkt produkt in produktLista)
			{
				if (produkt.ID.Equals(id))
					tempProdukt = produkt;
			}

			return tempProdukt;
		}

		/// <summary>
		/// DefaultAnvändare skapar en default användare för Anvandare
		/// tabellen för om den är helt tom.
		/// </summary>
		/// <returns>en Användare med default värden</returns>
		public Användare DefaultAnvändare()
		{
			Användare användare = new Användare();
			int id = 0;

			användare.ID = id;
			användare.Användarnamn = "" + id;
			användare.LösenordHash = "" + id;
			användare.Roll = "" + id;
			användare.Räknare = id;
			användare.Låst = false;

			return användare;
		}

		/// <summary>
		/// LäsaAnvändare är en metod för att läsa in värden från Anvandare
		/// tabellen till en lista av användare.
		/// </summary>
		/// <returns>sann om man kunde läsa användare och annars falsk</returns>
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
			DataSet dataSet = HämtaDataSet(kommando, tabellnamn);

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

			//Om där finns ingen data i databasen i nyckelkolumnen sätt
			//i en default användare
			if (!TestaKolumnData(dataSet, kolumnID))
			{
				StängKopplingen();
				InsättAnvändare(DefaultAnvändare());
			}

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
				användareTemp.Låst = bool.Parse(dataRow["Laste"].ToString());

				//Sätt Användare objekt i Lista användarLista
				//(om en id redan finns i användarLista ersätt användaren)
				if (!ExisterandeAnvändare(användareTemp.ID))
					användarLista.Add(användareTemp);
				else
				{
					användarLista.Remove(HämtaAnvändareMedID(användareTemp.ID));
					användarLista.Add(användareTemp);
				}

				if (användareTemp.ID >= högstaAnvändareID)
					högstaAnvändareID = användareTemp.ID;
			}

			//Stäng databasen
			StängKopplingen();

			return lyckades;
		}

		/// <summary>
		/// InsättAnvändare är en metod för insättning av en användare i databasen.
		/// Den kollar om id finns redan i listan innan insättning.
		/// Om användaren redan finns gör metoden ingenting och falsk returneras.
		/// </summary>
		/// <param name="användare">Användare som ska läggas till</param>
		/// <returns>sann om användaren lades till och annars falsk</returns>
		public bool InsättAnvändare(Användare användare)
		{
			bool lyckades = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om test datan med id==tempId är inte redan i tabellen, sätt i
			//det, annars gör ingenting (för att undervika en unik id krash)
			if (!ExisterandeAnvändare(användare.ID))
			{
				lyckades = true;
				//SqlCommand föredra allt i en lång sträng
				String sCommandString = "INSERT INTO Anvandare (ID, Anvandarnamn, LosenordHash, Roll, Raknare, Laste) VALUES ('" + användare.ID + "', '" + användare.Användarnamn + "', '" + användare.LösenordHash + "', '" + användare.Roll + "', '" + användare.Räknare + "', '" + användare.Låst + "')";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();
			}

			//Stäng databasen
			StängKopplingen();

			if(lyckades)
			{
				//Läsa om för att sätta om listan (man kan inte vara säkert att
				//det verkligen blev tilläggd i databasen)
				lyckades = LäsaAnvändare();
			}

			return lyckades;
		}

		/// <summary>
		/// UppdateraAnvändare är en metod för uppdatering av en befintlig användare
		/// i databasen. Den kollar redan att id faktiskt finns innan uppdatering.
		/// Om användaren inte redan finns gör metoden ingenting och falsk returneras.
		/// </summary>
		/// <param name="användare">Användare som ska uppdateras</param>
		/// <returns>sann om användaren uppdaterades och annars falsk</returns>
		public bool UppdateraAnvändare(Användare användare)
		{
			bool lyckades = false;

			//Öppna databasen, om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==användare.ID är redan i tabellen, uppdatera den,
			//annars gör ingenting
			if (ExisterandeAnvändare(användare.ID))
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
				sCommandString = "UPDATE Anvandare SET Laste='" + användare.Låst + "' WHERE ID='" + användare.ID + "'";
				kommando = new SqlCommand(sCommandString, kopplingen);
				kommando.ExecuteNonQuery();
			}

			//Stäng databasen
			StängKopplingen();

			if (lyckades)
			{
				//Läsa om för att sätta om listan (man kan inte vara säkert att
				//det verkligen blev uppdaterad i databasen)
				lyckades = LäsaAnvändare();
			}

			return lyckades;
		}

		/// <summary>
		/// TaBortAnvändare är en metod för att ta bort en befintlig användare
		/// i databasen. Den kollar att id redan finns innan den tas bort.
		/// Om användaren inte redan finns gör metoden ingenting  och falsk
		/// returneras.
		/// </summary>
		/// <param name="användare">Användare som ska tas bort</param>
		/// <returns>sann om användaren har tagits bort och annars falsk</returns>
		public bool TaBortAnvändare(int id)
		{
			bool lyckades = false;

			//Öppna databasen och om man inte lyckas returnera falsk
			if (!ÖppnaKopplingen()) return false;

			//Om ID==användare.ID är redan i tabellen, tar bort den,
			//annars gör ingenting
			if (ExisterandeAnvändare(id))
			{
				lyckades = true;
				//Tar bort användaren
				string sCommandString = "DELETE FROM Anvandare WHERE ID='" + id + "'";
				SqlCommand command = new SqlCommand(sCommandString, kopplingen);
				command.ExecuteNonQuery();
			}

			//Stäng databasen
			StängKopplingen();

			if (lyckades)
			{
				//Läsa om för att sätta om listan (man kan inte vara säkert att
				//det verkligen har tagits bort från databasen)
				användarLista.Remove(HämtaAnvändareMedID(id));
				lyckades = LäsaAnvändare();
			}

			return lyckades;
		}

		/// <summary>
		/// ExisterandeAnvändare är en metod som testar om en användare ID finns
		/// redan i användarListan.  
		/// </summary>
		/// <param name="id">id för en möjlig användare</param>
		/// <returns>sann om användaren med id-värdet existera och annars falsk</returns>
		public bool ExisterandeAnvändare(int id)
		{
			//Bool för att markera att någonting redan existerar i tabellen, från början false 
			bool bExisterar = false;

			foreach (Användare användare in användarLista)
			{
				if (användare.ID.Equals(id))
					bExisterar = true;
			}

			return bExisterar;
		}

		/// <summary>
		/// HämtaAnvändareMedID är en metod som hämtar en användare från listan
		/// med angiven id eller null om ingen hittades. 
		/// </summary>
		/// <param name="id">id för en möjlig användare</param>
		/// <returns>Användare objektet om den hittades och annars null</returns>
		public Användare HämtaAnvändareMedID(int id)
		{
			//Bool för att markera att någonting redan existerar i tabellen, från början false 
			Användare tempAnvändare = null;

			foreach (Användare användare in användarLista)
			{
				if (användare.ID.Equals(id))
					tempAnvändare = användare;
			}

			return tempAnvändare;
		}

		/*
		 * ExisterandeAnvändarnamn är en metod som testar om en produkt ID finns redan i
		 * databasen.
		 * Man kunde bara kolla i Dictionary produkter istället för att läsa om
		 * databasen men det förutsätter att LäsaAnvändare har körts innan.
		 * Som det är nu är det oberoende av det.
		 * 
		 * In - id (int)
		 * Ut - sann eller falsk för om den existerar eller inte
		 */
		/*public bool ExisterandeAnvändare(int id)
		{
			string kommando = "SELECT * FROM Anvandare";
			string tabellnamn = "Anvandare";

			//Hämta dataset för produkter
			//DataSet är en behållare/mellansteg för inläst databas-data (kan innehålla flera tabeller)
			DataSet dataSet = HämtaDataSet(kommando, tabellnamn);

			//Bool för att markera att någonting redan existerar i tabellen, från början false 
			bool bExisterar = false;

			//Loopa genom varje rad i Produkter tabellen (Tables[0] för att Produkter
			//är den första och enda tabellen i DataSet ds)
			foreach (DataRow dataRow in dataSet.Tables[0].Rows)
			{
				//Om field "ID" i en DataRow är lika med id så finns den redan 
				if (int.Parse(dataRow["ID"].ToString()) == id)
					bExisterar = true; //bExisterar true innebär att testId redan finns i tabellen
			}

			return bExisterar;
		}*/
	}
}
