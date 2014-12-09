using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOMAdministrationApplikation.Models
{
	public interface IDatabas
	{
		List<Produkt> ProduktLista { get; }
		List<Användare> AnvändarLista { get; }
		int HögstaAnvändareID { get; }

		Produkt DefaultProdukt(string id);
		bool LäsaProdukter();
		bool InsättProdukt(Produkt produkt);
		bool UppdateraProdukt(Produkt produkt);
		bool TaBortProdukt(string id);
		bool ExisterandeProdukt(string id);
		Produkt HämtaProduktMedID(string id);
		Användare DefaultAnvändare(int id);
		bool LäsaAnvändare();
		bool InsättAnvändare(Användare användare);
		bool UppdateraAnvändare(Användare användare);
		bool TaBortAnvändare(int id);
		bool ExisterandeAnvändare(int id);
		Användare HämtaAnvändareMedID(int id);
	}
}
