# Sten, Sax, Påse - C# Konsolapplikation

Detta är ett interaktivt konsolspel för det klassiska spelet "Sten, Sax, Påse", utvecklat i C# och .NET som en del av en projektuppgift vid Mittuniversitetet. Spelet demonstrerar grundläggande objektorienterad programmering (OOP), säker inmatningsvalidering och datalagring.

## Funktioner
* **Spela mot datorn:** En människa spelar mot en slumpstyrd dator i en loop till dess att någon når 3 poäng.
* **Visuell presentation:** Spelets val (sten, sax eller påse) illustreras med ASCII-konst, och utskrifterna är färgkodade (grön för vinst, röd för förlust, gul för oavgjort).
* **Datapersistens (JSON):** Vinster sparas kontinuerligt ner till en lokal fil (`stats.json`), så att matchhistoriken överlever även när programmet stängs ned.
* **Påskägg:** Inkluderar ett inbyggt påskägg som triggas när spelaren når ett visst antal sparade historiska vinster.
* **Felsäker:** Hanterar felaktig inmatning från användaren utan att programmet kraschar, tack vare inbyggd TryParse-validering.

## Struktur (Single Responsibility Principle)
Projektet är uppdelat i flera filer för att separera ansvar:
* `Program.cs` - Huvudprogrammet som sköter loophantering och användarinteraktion.
* `Game.cs` - Innehåller själva spellogiken och avgör vinnaren baserat på val.
* `Player.cs` - Representerar en spelare (namn och poäng).
* `Choice.cs` - En enum som definierar de giltiga dragen.
* `GameStats.cs` - Datamodell för att spara och läsa in JSON-historiken.
* `GameUI.cs` - Hanterar uteslutande presentation, färgkodning och utskrift av ASCII-konst.

## Förutsättningar
För att kunna bygga och köra detta projekt behöver du ha .NET SDK installerat på din dator.
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (eller senare)

## Kom igång

1. **Klona repot:**
   ```bash
   git clone [https://github.com/runegranath/Projekt-C-.git](https://github.com/runegranath/Projekt-C-.git)

Författare:
Rune Granath
Student vid Webbutvecklingsprogrammet (120 hp), Mittuniversitetet.
