using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Spectre.Console;

class Program
{

    static async Task Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=nsdatabase;Uid=root;Pwd=;";
        string url = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,rain&timezone=auto";
        HttpClient Client = new HttpClient();


        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string selectStation = "";
            string stationName = "";
            bool stationSelected = false;

            try
            {
                conn.Open();
                string query = "SELECT * FROM netherlands_train_stations;";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                while (!stationSelected)
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Kies uit op welk station je staat.\n");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["name"].ToString());
                        }
                    }

                    Console.WriteLine();
                    selectStation = Console.ReadLine();
                    Console.Clear();

                    using (MySqlCommand checkCmd = new MySqlCommand(query, conn))

                    using (MySqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stationName = reader["name"].ToString();

                            if (selectStation.ToLower() == stationName.ToLower())
                            {
                                Console.WriteLine("Geselecteerd station: " + reader["name"].ToString());
                                stationSelected = true;
                                reader.Close();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                            }
                        }
                    }

                    if (!stationSelected)
                    {
                        Console.Clear();
                        Console.WriteLine("Error: Station " + selectStation + " bestaat niet, probeer het opnieuw.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            DateTime lastShownTime = DateTime.MinValue;

            while (stationSelected)
            {
                //Console.WriteLine("test");
                string messageQuery = "SELECT * FROM berichten WHERE gekeurd = \"goedGekeurd\" AND station = \"" + stationName + "\" ORDER BY gemaakt DESC LIMIT 3;";

                MySqlCommand cmd = new MySqlCommand(messageQuery, conn);

                bool newMessagesFound = false;
                DateTime newestBerichtTime = lastShownTime;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    var berichten = new List<(string naam, string kleinBericht, DateTime gemaakt)>();

                    while (reader.Read())
                    {
                        DateTime gemaakt = Convert.ToDateTime(reader["gemaakt"]);

                        if (gemaakt > lastShownTime)
                        {
                            newMessagesFound = true;
                        }

                        if (gemaakt > newestBerichtTime)
                        {
                            newestBerichtTime = gemaakt;
                        }

                        berichten.Add((
                            reader["naam"].ToString(),
                            reader["kleinBericht"].ToString(),
                            gemaakt
                        ));
                    }

                    if (newMessagesFound)
                    {
                        Console.Clear();
                        Console.WriteLine("Station: " + selectStation);
                        DateTime nu = DateTime.Now;
                        Console.WriteLine("Datum: " + nu.ToShortDateString() + " Tijd: " + nu.ToShortTimeString());
                        Console.WriteLine(" ");

                        try
                        {
                            HttpResponseMessage response = await Client.GetAsync(url);
                            response.EnsureSuccessStatusCode();

                            string json = await response.Content.ReadAsStringAsync();

                            using (JsonDocument doc = JsonDocument.Parse(json))
                            {
                                var root = doc.RootElement;

                                if (root.TryGetProperty("current", out JsonElement current))
                                {
                                    float temperature = current.GetProperty("temperature_2m").GetSingle();
                                    float rain = current.GetProperty("rain").GetSingle();

                                    Console.WriteLine($"Temperatuur: {temperature} °C");
                                    Console.WriteLine($"Neerslag: {rain} mm");
                                }
                                else
                                {
                                    Console.WriteLine("Geen 'current' weerdata gevonden.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error fetching weather data: {ex.Message}");
                        }

                        Console.WriteLine(" ");
                        Console.WriteLine("\nMeningen van station: " + selectStation + "\n");

                        foreach (var bericht in berichten)
                        {
                            Console.WriteLine("Naam: " + bericht.naam);
                            Console.WriteLine("Klein bericht: " + bericht.kleinBericht);
                            Console.WriteLine("Datum: " + bericht.gemaakt);
                            Console.WriteLine();
                        }

                        lastShownTime = newestBerichtTime;
                    }
                }

                Thread.Sleep(5000);
            }
        }
    }
}
