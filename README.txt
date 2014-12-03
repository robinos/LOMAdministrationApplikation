LOMAdministraitonApplikation - Version 0.4 - 2014-12-01

För att köra:

1) I Databas klassen måste man ändrar path i connectionString till där
\LjusOchMiljoAB\LjusOchMiljoAB\App_Data\LOM_DB.mdf ligger.  Utan en full path
(t.ex med kod för att själv upptäcka där applikationen kördes), fick man inte
tilllåtelse att ändra i databasen.

2) För att få currencyTextBox i toolbox till Windows Forms måste man bygga om hela
solution.  Visual Studio borde automatiskt hitta den då.

3) Har man yttligare problem med databasen rekommenderas det att starta om Visual
Studio eller öppna databasen i projektet LjusOchMiljoAB först.

4) Kör.



Lösningsinformation

JRINCCustomControls projektet innehåller currencyTextBox vilket används i
LOMAdministrationApplikation projektet. Det är baserad på kod från
http://www.codeproject.com/Articles/248989/A-Currency-Masked-TextBox-from-TextBox-Class,
men ändrat totallt i koden för att få tre olika funktionaliteter.

1.) Med decimaltecken (default ",") och ett obligatoriskt decimalvärde (som 2
decimaler), accepterar textboxen nummer och decimaltecknen och ser till att den
alltid har x decimal där x är decimalvärdet.  ie. med decimalvärde 2, blir
12,1 -> 12,10 och 0 -> 0,00.  Vid för många decimaler blir det avhuggen, ie.
112,345 -> 112,34.  Nollar framför blir borttagen, ie. 00012 -> 12.00.  Det går
även att sätta ett tusentalstecken (default " ") och ett pengar tecken (default "kr"). 

2.) Utan decimaltecken men med ett obligatoriskt decimalvärde, acceptera textboxen
ett nummer som måste vara en viss storlek och får nollar framför för att se till
att det blir så, ie. med decimalvärde 5 blir 13 -> 00013. 

3.) Utan decimaltecken och utan obligatoriska decimalvärde blir det bara en vanliga
textbox.

**För att få currencyTextBox i toolbox till Windows Forms måste man bygga hela
solution.  Visual Studio borde automatiskt hitta den då.**

- currencyTextBox.cs - kod



LOMAdministrationApplikation projektet innehåller huvudprogrammet.
Enlig MVC (så nära som möjligt) - Produkt och Användare är modeller (data),
ProduktForm, AnvändareForm och HuvudApplikationForm är vyar och
AdministrationApplikation är huvudkontroller (som hanterar kommunikation
mellan data och vyn och innehåller Main).  Databas är en kontroller som har
hand om databasen och kommunicera med huvudkontroller.

- Produkt.cs (Modell)
Databehållaren för en produkt inläst från Produkt tabellen i databasen och för
ny data som ska skrivas till databasen. Den har egna Equals och HashCode metoder.
Egenskaper - ID, Namn, Typ, Färg, Bildfilnamn, Ritningsfilnamn, RefID,
Beskrivning, Monteringsbeskrivning
Metoder - Equals, Hashcode

- Användare.cs (Modell)
Databehållaren för en användare inläst från Anvandare tabellen i databasen och
för ny data som ska skrivas till databasen. Den har egna Equals och HashCode metoder.
Egenskaper - ID, Användarnamn, LösenordHash, Roll, Räknare, Låst 
Metoder - Equals, Hashcode

- HuvudApplikationForm.cs (Vy)
Om användare är inloggad som vanlig användare med sin Windows konto får användaren
tillgång till knapparna för att ta sig till ProduktForm eller AnvändareForm. Annars
(som Guest konto) kommer man inte vidare.
Se klassen för detaljer.

- ProduktForm.cs (Vy)
Visar upp produktinformation och tar in input från användaren. Form logiken hanteras
av ProduktForm och produkt logiken hanteras av AdministrationApplikation. 
ProduktForm testar indatan innan det skickas vidare.
Se klassen för detaljer.

- AnvändareForm.cs (Vy)
Visar upp användarinformation och tar in input från användaren. Form logiken hanteras
av AnvändareForm och användare logiken hanteras av AdministrationApplikation. 
AnvändareForm testar indatan innan det skickas vidare.
Se klassen för detaljer.

- AdministrationApplikation.cs (Main, Huvud-Kontroller)
Skickar till Databas klassen och får svar som ges till ProduktForm och AnvändareForm.
Se klassen för detaljer.

- Databas.cs (Kontroller)
Hanterar all kontakt med databasen.  Testar nyckelvärden innan
insättning/borttagning/uppdatering.
Se klassen för detaljer.

- LOM_DB.mdf

Produkt tabellen
ID (char(5)), Namn (Varchar(30)), Pris (Decimal(10,2)), Typ (Varchar(30)),
Farg (Varchar(30)), Bildfilnamn (Varchar(30)), Ritningsfilnamn (Varchar(30)),
RefID (char(5)), Beskrivning (Varchar(600)), Montering (Varchar(300))

Anvandare tabellen
ID (int), Anvandarnamn (Varchar(30)), LosenordHash (NVarchar(MAX)), Roll (Varchar(30)),
Raknare (int), Laste (bit) 


**UnitTestar måste omarbetas lite med alla ändringar som har kommit**

LOMAdministrationApplikationUnitTestar innehåller unit testar till
AdministrationApplikation.
De två klasser som testas är Produkt och AdministrationApplikation.  Databas går
inte att testa pga databaskopplingen som ge unit testar problem.  Det kan dock testas
genom AdministrationApplikation som använder den.  ProduktForm kan inte testas
med vanliga Unit Testar utan måste testas med en UI test som har inte gjorts än.

- testar_Produkt.cs
Testar skapande, tilldelning, och Equals metod i Produkt klassen.

- testar_AdministrationApplikation.cs
Testar skapande, tilldelning, och metoder som kallar på Databas och ger
vyar svar.
