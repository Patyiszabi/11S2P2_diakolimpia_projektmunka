using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows.Forms;

namespace diakolimpia_consoleapp_11S2P2
{

    internal class Program
    {
        #region Connection To Databases
        public static string connectionFootballDB = "server=localhost;database=diakolimpia_labdarugas;user=root;password=;";
        public static string connectionArcheryDB = "server=localhost;database=íjászat;user=root;password=;";
        public static string connectionHandballDB = "server=localhost;database=kézilabda;user=root;password=;"
        public static string connectionVolleyballDB = "server=localhost;database=röpi2;user=root;password=;"
        #endregion

        static void Main(string[] args)
        {
            #region Main Menu and Application
            bool running = true;
            MessageBox.Show("Üdvözöljük a Diákolimpia programban!");

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Diákolimpia Főmenü ===");
                Console.WriteLine("1. Statisztikák");
                Console.WriteLine("2. Szűrők");
                Console.WriteLine("3. Játékosok");
                Console.WriteLine("4. Rangsorok/Helyezések");
                Console.WriteLine("5. Kilépés");
                Console.Write("\nVálasszon egy opciót (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowStatisticsMenu();
                        break;
                    case "2":
                        ShowMatchesMenu();
                        break;
                    case "3":
                        ShowPlayersMenu();
                        break;
                    case "4":
                        ShowTeamsMenu();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Köszönjük, hogy alkalmazásunkat használta!");
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás! Kérjük, adjon meg egy számot (1-5).");
                        Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                        Console.ReadLine();
                        break;
                }
            }
            #endregion

            if (!running)
            {
                MessageBox.Show("Köszönjük szépen, hogy alkalmazásunkat használta!");
            }

        }

        #region Submenus

        static void ShowStatisticsMenu()
        {
            bool backToMainMenu = false;
            while (!backToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("=== Statisztikák ===");
                Console.WriteLine("1. Legtöbb gólt szerző játékosok");
                Console.WriteLine("2. Átlagos gólok csapatonként");
                Console.WriteLine("3. Legtöbb gólt szerző csapatok");
                Console.WriteLine("4. Vissza a főmenübe");
                Console.Write("\nVálasszon egy opciót (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayersGoalList();
                        break;
                    case "2":
                        averageGoalsPerTeam();
                        break;
                    case "3":
                        mostGoalsTeam();
                        break;
                    case "4":
                        backToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás! Kérjük, adjon meg egy számot (1-4).");
                        Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                        Console.ReadLine();
                        break;
                }
                if (!backToMainMenu)
                {
                    Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                    Console.ReadLine();
                }
            }
        }

        static void ShowMatchesMenu()
        {
            bool backToMainMenu = false;
            while (!backToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("=== Szűrők ===");
                Console.WriteLine("1. Szűrési funkciók");
                Console.WriteLine("2. Vissza a főmenübe");
                Console.Write("\nVálasszon egy opciót (1-2: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowFilteringOptionsMenu();
                        break;
                    case "2":
                        backToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás! Kérjük, adjon meg egy számot (1-2).");
                        Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                        Console.ReadLine();
                        break;
                }
                if (!backToMainMenu)
                {
                    Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                    Console.ReadLine();
                }
            }
        }

        static void ShowPlayersMenu()
        {
            bool backToMainMenu = false;
            while (!backToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("=== Játékosok ===");
                Console.WriteLine("1. Játékos keresése diákigazolvány alapján");
                Console.WriteLine("2. Vissza a főmenübe");
                Console.Write("\nVálasszon egy opciót (1-2): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SearchPlayerViaID();
                        break;
                    case "2":
                        backToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás! Kérjük, adjon meg egy számot (1-2).");
                        Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                        Console.ReadLine();
                        break;
                }
                if (!backToMainMenu)
                {
                    Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                    Console.ReadLine();
                }
            }
        }

        static void ShowTeamsMenu()
        {
            bool backToMainMenu = false;
            while (!backToMainMenu)
            {
                Console.Clear();
                Console.WriteLine("=== Rangsorok/Helyezések ===");
                Console.WriteLine("1. Csoportok rangsora");
                Console.WriteLine("2. Vissza a főmenübe");
                Console.Write("\nVálasszon egy opciót (1-2): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GroupStandings();
                        break;
                    case "2":
                        backToMainMenu = true;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás! Kérjük, adjon meg egy számot (1-2).");
                        Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                        Console.ReadLine();
                        break;
                }
                if (!backToMainMenu)
                {
                    Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                    Console.ReadLine();
                }
            }
        }

        static void ShowFilteringOptionsMenu()
        {
            bool backToMatchesMenu = false;
            while (!backToMatchesMenu)
            {
                Console.Clear();
                Console.WriteLine("=== Szűrési funkciók ===");
                Console.WriteLine("1. Mérkőzések szűrése dátum-intervallum alapján");
                Console.WriteLine("2. Mérkőzések szűrése csapat/játékos alapján");
                Console.WriteLine("3. Események szűrése piros/sárga lapok alapján");
                Console.WriteLine("4. Vissza a Szűrési menübe");
                Console.Write("\nVálasszon egy opciót (1-4): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        MatchFilterByDateRange();
                        break;
                    case "2":
                        MatchFilterByTeamOrPlayer();
                        break;
                    case "3":
                        MatchFilterByEventType();
                        break;
                    case "4":
                        backToMatchesMenu = true;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás! Kérjük, adjon meg egy számot (1-4).");
                        Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                        Console.ReadLine();
                        break;
                }
                if (!backToMatchesMenu)
                {
                    Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                    Console.ReadLine();
                }
            }
        }

        #endregion

        #region Methods for Statistics

        static void PlayersGoalList()
        {
            Console.Clear();
            Console.WriteLine("=== Legtöbb gólt szerző játékosok ===");
            Console.Write("\nMennyi játékost szeretne kilistázni?: ");
            int limit = 0;
            if (!int.TryParse(Console.ReadLine(), out limit) || limit <= 0)
            {
                Console.WriteLine("Érvénytelen bemenet! Kérjük, adjon meg egy pozitív egész számot.");
                Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                Console.ReadLine();
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT j.nev, c.nev AS csapat_nev, COUNT(e.id) AS golok_szama
                        FROM Esemeny e
                        JOIN Jatekos j ON e.jatekos_id = j.id
                        JOIN Csapat c ON j.csapat_id = c.id
                        WHERE e.tipus = 'gól'
                        GROUP BY j.id
                        ORDER BY golok_szama DESC
                        LIMIT @limit";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@limit", limit);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();
                            int helyezes = 1;
                            while (reader.Read())
                            {
                                string playerName = reader.GetString("nev");
                                string teamName = reader.GetString("csapat_nev");
                                int goals = reader.GetInt32("golok_szama");
                                Console.WriteLine($"{helyezes}. {playerName} - {teamName} - {goals} gól");
                                helyezes++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);

                }
            }
        }

        static void averageGoalsPerTeam()
        {
            Console.Clear();
            Console.WriteLine("=== Átlagos gólok csapatonként ===");
            Console.Write("\nMennyi csapatot szeretne kilistázni?: ");
            int limit = 0;
            if (!int.TryParse(Console.ReadLine(), out limit) || limit <= 0)
            {
                Console.WriteLine("Érvénytelen bemenet! Kérjük, adjon meg egy pozitív egész számot.");
                Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                Console.ReadLine();
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT cs.nev AS csapat_nev,
                        ROUND(AVG(IF(m.csapat1_id = cs.id, m.csapat1_pont, m.csapat2_pont)), 2) AS atlagos_golok
                        FROM Csapat cs
                        JOIN Merkozes m ON (m.csapat1_id = cs.id OR m.csapat2_id = cs.id)
                        GROUP BY cs.id
                        ORDER BY atlagos_golok DESC
                        LIMIT @limit";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@limit", limit);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();
                            int helyezes = 1;
                            while (reader.Read())
                            {
                                string teamName = reader.GetString("csapat_nev");
                                double averageGoals = reader.GetDouble("atlagos_golok");
                                Console.WriteLine($"{helyezes}. {teamName} - {averageGoals} átlagos gól");
                                helyezes++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);
                }
            }
        }

        static void mostGoalsTeam()
        {
            Console.Clear();
            Console.WriteLine("=== Legtöbb gólt szerző csapatok ===");
            Console.Write("\nMennyi csapatot szeretne kilistázni?: ");
            int limit = 0;
            if (!int.TryParse(Console.ReadLine(), out limit) || limit <= 0)
            {
                Console.WriteLine("Érvénytelen bemenet! Kérjük, adjon meg egy pozitív egész számot.");
                Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                Console.ReadLine();
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT cs.nev AS csapat_nev,
                        SUM(IF(m.csapat1_id = cs.id, m.csapat1_pont, m.csapat2_pont)) AS osszes_gol
                        FROM Csapat cs
                        JOIN Merkozes m ON (m.csapat1_id = cs.id OR m.csapat2_id = cs.id)
                        GROUP BY cs.id
                        ORDER BY osszes_gol DESC
                        LIMIT @limit";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@limit", limit);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();
                            int helyezes = 1;
                            while (reader.Read())
                            {
                                string teamName = reader.GetString("csapat_nev");
                                int totalGoals = reader.GetInt32("osszes_gol");
                                Console.WriteLine($"{helyezes}. {teamName} - {totalGoals} összes gól");
                                helyezes++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);
                }
            }
        }

        #endregion

        #region Methods for Matches

        static void MatchFilterByDateRange()
        {
            Console.Clear();
            Console.WriteLine("=== Mérkőzések szűrése dátum-intervallum alapján ===");
            Console.Write("Adja meg a kezdő dátumot (yyyy-MM-dd): ");
            string startDateString = Console.ReadLine();
            Console.Write("Adja meg a záró dátumot (yyyy-MM-dd): ");
            string endDateString = Console.ReadLine();

            if (DateTime.TryParse(startDateString, out DateTime startDate) && DateTime.TryParse(endDateString, out DateTime endDate))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
                {
                    try
                    {
                        connection.Open();

                        string query = @"
                            SELECT m.id, m.datum, m.helyszin, c1.nev AS csapat1, c2.nev AS csapat2
                            FROM Merkozes m
                            JOIN Csapat c1 ON m.csapat1_id = c1.id
                            JOIN Csapat c2 ON m.csapat2_id = c2.id
                            WHERE m.datum BETWEEN @startDate AND @endDate
                            ORDER BY m.datum";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@startDate", startDate);
                            command.Parameters.AddWithValue("@endDate", endDate);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                Console.WriteLine();
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32("id");
                                    DateTime datum = reader.GetDateTime("datum");
                                    string helyszin = reader.GetString("helyszin");
                                    string csapat1 = reader.GetString("csapat1");
                                    string csapat2 = reader.GetString("csapat2");
                                    Console.WriteLine($"{id}. {datum.ToString("yyyy-MM-dd")} - {helyszin} - {csapat1} vs {csapat2}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hiba történt: " + ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Érvénytelen dátumformátum! Kérjük, adjon meg érvényes dátumokat.");
                Console.WriteLine("Nyomjon Entert a folytatáshoz...");
                Console.ReadLine();
            }
        }

        static void MatchFilterByTeamOrPlayer()
        {
            Console.Clear();
            Console.WriteLine("=== Mérkőzések szűrése csapat/játékos alapján ===");
            Console.WriteLine("1. Csapat alapján");
            Console.WriteLine("2. Játékos alapján");
            Console.Write("\nVálasszon egy opciót (1-2): ");

            string choice = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
            {
                try
                {
                    connection.Open();
                    string query;
                    MySqlCommand command;

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Adja meg a csapat nevét: ");
                            string teamName = Console.ReadLine();

                            query = @"
                                SELECT m.id, m.datum, m.helyszin, c1.nev AS csapat1, c2.nev AS csapat2
                                FROM Merkozes m
                                JOIN Csapat c1 ON m.csapat1_id = c1.id
                                JOIN Csapat c2 ON m.csapat2_id = c2.id
                                WHERE c1.nev LIKE @teamName OR c2.nev LIKE @teamName
                                ORDER BY m.datum";

                            command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@teamName", $"%{teamName}%");
                            break;

                        case "2":
                            Console.Write("Adja meg a játékos nevét: ");
                            string playerName = Console.ReadLine();

                            query = @"
                                SELECT m.id, m.datum, m.helyszin, c1.nev AS csapat1, c2.nev AS csapat2
                                FROM Merkozes m
                                JOIN Csapat c1 ON m.csapat1_id = c1.id
                                JOIN Csapat c2 ON m.csapat2_id = c2.id
                                JOIN Jatekos j ON (j.csapat_id = c1.id OR j.csapat_id = c2.id)
                                WHERE j.nev LIKE @playerName
                                ORDER BY m.datum";

                            command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@playerName", $"%{playerName}%");
                            break;

                        default:
                            Console.WriteLine("Érvénytelen választás!");
                            return;
                    }

                    using (command)
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            DateTime datum = reader.GetDateTime("datum");
                            string helyszin = reader.GetString("helyszin");
                            string csapat1 = reader.GetString("csapat1");
                            string csapat2 = reader.GetString("csapat2");
                            Console.WriteLine($"{id}. {datum.ToString("yyyy-MM-dd")} - {helyszin} - {csapat1} vs {csapat2}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);
                }
            }
        }

        static void MatchFilterByEventType()
        {
            Console.Clear();
            Console.WriteLine("=== Események szűrése piros/sárga lapok alapján ===");
            Console.WriteLine("1. Piros lapok");
            Console.WriteLine("2. Sárga lapok");
            Console.Write("\nVálasszon egy opciót (1-2): ");

            string choice = Console.ReadLine();
            string eventType;

            switch (choice)
            {
                case "1":
                    eventType = "piros";
                    break;
                case "2":
                    eventType = "sárga";
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás!");
                    return;
            }


            using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT e.id, m.datum, j.nev AS jatekos_nev, c.nev AS csapat_nev, e.megjegyzes
                        FROM Esemeny e
                        JOIN Merkozes m ON e.merkozes_id = m.id
                        JOIN Jatekos j ON e.jatekos_id = j.id
                        JOIN Csapat c ON j.csapat_id = c.id
                        WHERE e.tipus = @eventType
                        ORDER BY m.datum";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventType", eventType);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine();
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                DateTime date = reader.GetDateTime("datum");
                                string playerName = reader.GetString("jatekos_nev");
                                string teamName = reader.GetString("csapat_nev");
                                string round = reader.GetString("megjegyzes");
                                Console.WriteLine($"{id}. {date.ToString("yyyy-MM-dd")} - {playerName} ({teamName}) - {round}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);
                }
            }
        }

        #endregion

        #region Methods for IDSearch

        static void SearchPlayerViaID()
        {
            Console.Clear();
            Console.WriteLine("=== Játékos keresése diákigazolvány szám alapján ===");
            Console.Write("\nÍrja be a diákigazolvány számot: ");
            string studentId = Console.ReadLine();
            bool played = false;

            if (string.IsNullOrWhiteSpace(studentId))
            {
                Console.WriteLine("Érvénytelen diákigazolvány szám!");
                Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                Console.ReadLine();
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT j.nev, j.mezszam, j.poszt, cs.nev AS csapat_nev, j.diakigazolvany_azonositoszam
                        FROM Jatekos j
                        JOIN Csapat cs ON j.csapat_id = cs.id
                        WHERE j.diakigazolvany_azonositoszam = @studentId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", studentId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("\nNem található játékos ezzel a diákigazolvány számmal.");
                                Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                                Console.ReadLine();
                                return;
                            }

                            while (reader.Read())
                            {
                                string playerName = reader.GetString("nev");
                                int jerseynumber = reader.GetInt32("mezszam");
                                string position = reader.GetString("poszt");
                                string teamName = reader.GetString("csapat_nev");
                                string studentIdNumber = reader.GetString("diakigazolvany_azonositoszam");

                                Console.WriteLine($"\nJátékos: {playerName}");
                                Console.WriteLine($"Mezszám: {jerseynumber}");
                                Console.WriteLine($"Pozíció: {position}");
                                Console.WriteLine($"Csapat: {teamName}");
                                Console.WriteLine($"Diákigazolványszám: {studentIdNumber}");
                            }
                        }
                    }

                    Console.WriteLine("\nA játékos meccsei:");

                    string matchesQuery = @"
                        SELECT DISTINCT m.id, m.datum, m.helyszin, c1.nev AS csapat1, c2.nev AS csapat2
                        FROM Merkozes m
                        JOIN Csapat c1 ON m.csapat1_id = c1.id
                        JOIN Csapat c2 ON m.csapat2_id = c2.id
                        JOIN Jatekos j ON (j.csapat_id = m.csapat1_id OR j.csapat_id = m.csapat2_id)
                        WHERE j.diakigazolvany_azonositoszam = @studentId
                        ORDER BY m.datum";

                    using (MySqlCommand command = new MySqlCommand(matchesQuery, connection))
                    {
                        command.Parameters.AddWithValue("@studentId", studentId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int matchID = reader.GetInt32("id");
                                DateTime matchDate = reader.GetDateTime("datum");
                                string matchPlace = reader.GetString("helyszin");
                                string team1 = reader.GetString("csapat1");
                                string team2 = reader.GetString("csapat2");
                                played = true;

                                Console.WriteLine($"\nMérkőzés ID: {matchID}");
                                Console.WriteLine($"Dátum: {matchDate:yyyy-MM-dd}");
                                Console.WriteLine($"Helyszín: {matchPlace}");
                                Console.WriteLine($"Csapatok: {team1} vs {team2}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);
                }
            }

            if (!played)
            {
                Console.WriteLine("\nA játékos nem játszott egyetlen mérkőzésen sem.");
            }

            Console.WriteLine("\nNyomjon Entert a főmenühöz való visszatéréshez...");
            Console.ReadLine();
        }
        #endregion

        #region Methods for Group Standings

        static void GroupStandings()
        {
            Console.Clear();
            Console.WriteLine("=== Csoportok rangsora ===");

            using (MySqlConnection connection = new MySqlConnection(connectionFootballDB))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT cs.nev AS csapat_nev,
                               csop.nev AS csoport,
                               SUM(CASE
                                   WHEN m.csapat1_id = cs.id AND m.csapat1_pont > m.csapat2_pont THEN 3
                                   WHEN m.csapat2_id = cs.id AND m.csapat2_pont > m.csapat1_pont THEN 3
                                   WHEN (m.csapat1_id = cs.id OR m.csapat2_id = cs.id) AND m.csapat1_pont = m.csapat2_pont THEN 1
                                   ELSE 0
                               END) AS pontok,
                               SUM(CASE WHEN m.csapat1_id = cs.id THEN m.csapat1_pont
                                   WHEN m.csapat2_id = cs.id THEN m.csapat2_pont ELSE 0 END) -
                               SUM(CASE WHEN m.csapat1_id = cs.id THEN m.csapat2_pont
                                   WHEN m.csapat2_id = cs.id THEN m.csapat1_pont ELSE 0 END) AS golkulonbseg,
                               COUNT(DISTINCT m.id) AS lejatszott_meccsek,
                               ROW_NUMBER() OVER(PARTITION BY csop.nev ORDER BY SUM(CASE
                                   WHEN m.csapat1_id = cs.id AND m.csapat1_pont > m.csapat2_pont THEN 3
                                   WHEN m.csapat2_id = cs.id AND m.csapat2_pont > m.csapat1_pont THEN 3
                                   WHEN (m.csapat1_id = cs.id OR m.csapat2_id = cs.id) AND m.csapat1_pont = m.csapat2_pont THEN 1
                                   ELSE 0
                               END) DESC, SUM(CASE WHEN m.csapat1_id = cs.id THEN m.csapat1_pont
                                   WHEN m.csapat2_id = cs.id THEN m.csapat2_pont ELSE 0 END) -
                               SUM(CASE WHEN m.csapat1_id = cs.id THEN m.csapat2_pont
                                   WHEN m.csapat2_id = cs.id THEN m.csapat1_pont ELSE 0 END) DESC) AS rangsor_a_csoportban
                        FROM Csapat cs
                        JOIN Csoport csop ON cs.csoport_id = csop.id
                        JOIN Merkozes m ON m.csapat1_id = cs.id OR m.csapat2_id = cs.id
                        WHERE cs.sportag_id = (SELECT id FROM Sportag WHERE nev = @sportName)
                        GROUP BY cs.id, csop.nev
                        ORDER BY csop.nev, pontok DESC, golkulonbseg DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sportName", "Labdarúgás");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("\nNem található adat a csapatok rangsoráról.");
                                Console.WriteLine("\nNyomjon Entert a folytatáshoz...");
                                Console.ReadLine();
                                return;
                            }

                            Console.WriteLine();
                            Console.WriteLine("| {0,-10} | {1,-25} | {2,6} | {3,10} | {4,18} |",
                                "Csoport", "Csapatnév", "Pontok", "Gólkülönbség", "Lejátszott meccsek");
                            Console.WriteLine(new string('-', 85));

                            string currentGroup = "";
                            bool isFirstGroup = true;

                            while (reader.Read())
                            {
                                string teamName = reader.GetString("csapat_nev");
                                string groupName = reader.GetString("csoport");
                                int points = reader.GetInt32("pontok");
                                int goalDifference = reader.GetInt32("golkulonbseg");
                                int matchesPlayed = reader.GetInt32("lejatszott_meccsek");
                                int rankInGroup = reader.GetInt32("rangsor_a_csoportban");

                                if (groupName != currentGroup)
                                {
                                    if (!isFirstGroup)
                                    {
                                        Console.WriteLine(new string('-', 85));
                                    }
                                    currentGroup = groupName;
                                    Console.WriteLine();
                                    Console.WriteLine($"=== {currentGroup} ===");
                                    isFirstGroup = false;
                                }


                                if (rankInGroup <= 2)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                else
                                {
                                    Console.ResetColor();
                                }


                                string formattedGoalDiff = goalDifference > 0 ? $"+{goalDifference}" : goalDifference.ToString();

                                Console.WriteLine("| {0,-10} | {1,-25} | {2,6} | {3,10} | {4,18} |",
                                    groupName, teamName, points, formattedGoalDiff, matchesPlayed);
                            }
                            Console.ResetColor();
                            Console.WriteLine(new string('-', 85));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hiba történt: " + ex.Message);
                }
            }

            Console.WriteLine("\nNyomjon Entert a főmenühöz való visszatéréshez...");
            Console.ReadLine();
        }
        #endregion

    }
}

