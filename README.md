# 11S2P2_diakolimpia_projektmunka

Ez az alkalmazás egy konzolos felületet biztosít a Diákolimpia labdarúgó adatbázisának kezeléséhez és statisztikáinak lekérdezéséhez. MySQL adatbázist használ a háttérben, és a MySqlConnector könyvtáron keresztül csatlakozik hozzá.

Fő funkciók
Az alkalmazás főmenüjéből elérhető modulok:

Statisztikák – Legjobb góllövők, csapatátlagok, legtöbb gólt szerző csapatok lekérdezése.

Szűrők – Mérkőzések keresése dátum, csapat vagy játékos, illetve eseménytípus alapján (pl. sárga/piros lap).

Játékosok – Keresés diákigazolvány-szám alapján.

Rangsorok/Helyezések – Csoportbeli helyezések megtekintése.

Kilépés – A program bezárása.

Követelmények
.NET Framework vagy .NET Core futtatókörnyezet

MySQL adatbázis, amely a következő sémát tartalmazza:

Csapat, Jatekos, Merkozes, Esemeny táblák

Csatlakozási adatok a connectionFootballDB változóban:

pgsql
Másolás
Szerkesztés
server=localhost;database=diakolimpia_labdarugas;user=root;password=;
Használat
Fordítsd le és futtasd a projektet egy C# fejlesztőkörnyezetben (pl. Visual Studio vagy dotnet run CLI).

A program induláskor egy üdvözlő üzenetet jelenít meg.

A főmenüben a megfelelő szám beírásával navigálhatsz az almodulokhoz.

A lekérdezések eredményeit a konzolon jeleníti meg, kivéve a hibaüzeneteket, melyek MessageBox formában is megjelenhetnek.
