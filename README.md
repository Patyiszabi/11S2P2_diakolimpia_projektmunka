# 11S2P2_diakolimpia_projektmunka

⚽ Diákolimpia Labdarúgó Konzolos Alkalmazás
Ez az alkalmazás egy 🖥️ konzolos felületet biztosít a 🎓 Diákolimpia labdarúgó adatbázisának kezeléséhez és statisztikáinak lekérdezéséhez. 🗄️ MySQL adatbázist használ a háttérben, és a 📦 MySqlConnector könyvtáron keresztül csatlakozik hozzá.

🔧 Fő funkciók
Az alkalmazás főmenüjéből elérhető modulok:

📊 Statisztikák – Legjobb góllövők ⚽, csapatátlagok 📈, legtöbb gólt szerző csapatok 🥇 lekérdezése.

🔍 Szűrők – Mérkőzések keresése dátum 🗓️, csapat 🛡️ vagy játékos 🧍 alapján, illetve eseménytípus szerint (pl. 🟨 sárga / 🟥 piros lap).

🧑‍🎓 Játékosok – Keresés diákigazolvány-szám alapján 🆔.

🏆 Rangsorok / Helyezések – Csoportbeli helyezések 📊 megtekintése.

❌ Kilépés – A program bezárása 🚪.

🧾 Követelmények
⚙️ .NET Framework vagy .NET Core futtatókörnyezet

🗄️ MySQL adatbázis, amely a következő sémát tartalmazza:

🛡️ Csapat

🧍 Jatekos

🏟️ Merkozes

⚠️ Esemeny táblák


▶️ Használat
🛠️ Futtasd a projektet egy C# fejlesztőkörnyezetben (pl. Visual Studio 💻 vagy dotnet run CLI 💡).

👋 A program induláskor üdvözlő üzenetet jelenít meg.

📋 A főmenüben a megfelelő szám beírásával navigálhatsz az almodulokhoz.

📃 A lekérdezések eredményeit a konzolon jeleníti meg, kivéve a hibaüzeneteket, amelyek MessageBox formában is megjelenhetnek ⚠️.
