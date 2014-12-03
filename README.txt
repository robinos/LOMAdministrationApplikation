LOMAdministraitonApplikation - Version 0.4 - 2014-12-01

F�r att k�ra:

1) I Databas klassen m�ste man �ndrar path i connectionString till d�r
\LjusOchMiljoAB\LjusOchMiljoAB\App_Data\LOM_DB.mdf ligger.  Utan en full path
(t.ex med kod f�r att sj�lv uppt�cka d�r applikationen k�rdes), fick man inte
tilll�telse att �ndra i databasen.

2) F�r att f� currencyTextBox i toolbox till Windows Forms m�ste man bygga om hela
solution.  Visual Studio borde automatiskt hitta den d�.

3) Har man yttligare problem med databasen rekommenderas det att starta om Visual
Studio eller �ppna databasen i projektet LjusOchMiljoAB f�rst.

4) K�r.



L�sningsinformation

JRINCCustomControls projektet inneh�ller currencyTextBox vilket anv�nds i
LOMAdministrationApplikation projektet. Det �r baserad p� kod fr�n
http://www.codeproject.com/Articles/248989/A-Currency-Masked-TextBox-from-TextBox-Class,
men �ndrat totallt i koden f�r att f� tre olika funktionaliteter.

1.) Med decimaltecken (default ",") och ett obligatoriskt decimalv�rde (som 2
decimaler), accepterar textboxen nummer och decimaltecknen och ser till att den
alltid har x decimal d�r x �r decimalv�rdet.  ie. med decimalv�rde 2, blir
12,1 -> 12,10 och 0 -> 0,00.  Vid f�r m�nga decimaler blir det avhuggen, ie.
112,345 -> 112,34.  Nollar framf�r blir borttagen, ie. 00012 -> 12.00.  Det g�r
�ven att s�tta ett tusentalstecken (default " ") och ett pengar tecken (default "kr"). 

2.) Utan decimaltecken men med ett obligatoriskt decimalv�rde, acceptera textboxen
ett nummer som m�ste vara en viss storlek och f�r nollar framf�r f�r att se till
att det blir s�, ie. med decimalv�rde 5 blir 13 -> 00013. 

3.) Utan decimaltecken och utan obligatoriska decimalv�rde blir det bara en vanliga
textbox.

**F�r att f� currencyTextBox i toolbox till Windows Forms m�ste man bygga hela
solution.  Visual Studio borde automatiskt hitta den d�.**

- currencyTextBox.cs - kod



LOMAdministrationApplikation projektet inneh�ller huvudprogrammet.
Enlig MVC (s� n�ra som m�jligt) - Produkt och Anv�ndare �r modeller (data),
ProduktForm, Anv�ndareForm och HuvudApplikationForm �r vyar och
AdministrationApplikation �r huvudkontroller (som hanterar kommunikation
mellan data och vyn och inneh�ller Main).  Databas �r en kontroller som har
hand om databasen och kommunicera med huvudkontroller.

- Produkt.cs (Modell)
Databeh�llaren f�r en produkt inl�st fr�n Produkt tabellen i databasen och f�r
ny data som ska skrivas till databasen. Den har egna Equals och HashCode metoder.
Egenskaper - ID, Namn, Typ, F�rg, Bildfilnamn, Ritningsfilnamn, RefID,
Beskrivning, Monteringsbeskrivning
Metoder - Equals, Hashcode

- Anv�ndare.cs (Modell)
Databeh�llaren f�r en anv�ndare inl�st fr�n Anvandare tabellen i databasen och
f�r ny data som ska skrivas till databasen. Den har egna Equals och HashCode metoder.
Egenskaper - ID, Anv�ndarnamn, L�senordHash, Roll, R�knare, L�st 
Metoder - Equals, Hashcode

- HuvudApplikationForm.cs (Vy)
Om anv�ndare �r inloggad som vanlig anv�ndare med sin Windows konto f�r anv�ndaren
tillg�ng till knapparna f�r att ta sig till ProduktForm eller Anv�ndareForm. Annars
(som Guest konto) kommer man inte vidare.
Se klassen f�r detaljer.

- ProduktForm.cs (Vy)
Visar upp produktinformation och tar in input fr�n anv�ndaren. Form logiken hanteras
av ProduktForm och produkt logiken hanteras av AdministrationApplikation. 
ProduktForm testar indatan innan det skickas vidare.
Se klassen f�r detaljer.

- Anv�ndareForm.cs (Vy)
Visar upp anv�ndarinformation och tar in input fr�n anv�ndaren. Form logiken hanteras
av Anv�ndareForm och anv�ndare logiken hanteras av AdministrationApplikation. 
Anv�ndareForm testar indatan innan det skickas vidare.
Se klassen f�r detaljer.

- AdministrationApplikation.cs (Main, Huvud-Kontroller)
Skickar till Databas klassen och f�r svar som ges till ProduktForm och Anv�ndareForm.
Se klassen f�r detaljer.

- Databas.cs (Kontroller)
Hanterar all kontakt med databasen.  Testar nyckelv�rden innan
ins�ttning/borttagning/uppdatering.
Se klassen f�r detaljer.

- LOM_DB.mdf

Produkt tabellen
ID (char(5)), Namn (Varchar(30)), Pris (Decimal(10,2)), Typ (Varchar(30)),
Farg (Varchar(30)), Bildfilnamn (Varchar(30)), Ritningsfilnamn (Varchar(30)),
RefID (char(5)), Beskrivning (Varchar(600)), Montering (Varchar(300))

Anvandare tabellen
ID (int), Anvandarnamn (Varchar(30)), LosenordHash (NVarchar(MAX)), Roll (Varchar(30)),
Raknare (int), Laste (bit) 


**UnitTestar m�ste omarbetas lite med alla �ndringar som har kommit**

LOMAdministrationApplikationUnitTestar inneh�ller unit testar till
AdministrationApplikation.
De tv� klasser som testas �r Produkt och AdministrationApplikation.  Databas g�r
inte att testa pga databaskopplingen som ge unit testar problem.  Det kan dock testas
genom AdministrationApplikation som anv�nder den.  ProduktForm kan inte testas
med vanliga Unit Testar utan m�ste testas med en UI test som har inte gjorts �n.

- testar_Produkt.cs
Testar skapande, tilldelning, och Equals metod i Produkt klassen.

- testar_AdministrationApplikation.cs
Testar skapande, tilldelning, och metoder som kallar p� Databas och ger
vyar svar.
