using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Spectre.Console;
internal class Program
{

    private static void Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=nsdatabase;Uid=root;Pwd=;";
        string stationName = "";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            bool stationSelected = false;
            try
            {
                conn.Open();
                string query = "SELECT * FROM netherlands_train_stations;";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                while (!stationSelected)
                {
                    List<string> stations = new List<string>();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stations.Add(reader["name"].ToString());
                        }
                    }

                    Console.WriteLine("Kies uit op welk station je staat.\n");

                    stationName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Welk [green]station[/] is dit?")
                            .PageSize(10)
                            .MoreChoicesText("[blue](Beweeg naar boven en beneden om meer stations te laten zien)[/]")
                            .AddChoices(stations)
                    );

                    // Confirm selection
                    Console.Clear();
                    Console.WriteLine("Geselecteerd station: " + stationName);
                    Thread.Sleep(1000);
                    Console.Clear();

                    stationSelected = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }

        // Connecteer met enquete antwoorden database
        using (MySqlConnection connShow = new MySqlConnection(connectionString))
        {
            try
            {
                bool isRunning = true;
                while (isRunning)
                {
                    //TODO: FIX OUDE BERICHTEN NIET GEUPDATE TE WORDEN NA KEURING
                    connShow.Open();
                    string queryBerichten = "SELECT * FROM berichten WHERE gekeurd = \"nietGekeurd\" AND station = \"" + stationName + "\";";
                    MySqlCommand cmd = new MySqlCommand(queryBerichten, connShow);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();

                    if (!reader.HasRows)
                    {
                        connShow.Close();
                        Console.WriteLine("Geen resultaten gevonden");

                        Console.WriteLine("\nDruk op enter om opnieuw te zoeken");
                        Console.WriteLine("Druk op [X] om programma af te sluiten");

                        string userInput = Console.ReadLine();
                        Console.Clear();

                        if (userInput.ToUpper() == "X")
                        {
                            // Programma wordt afgesloten
                            isRunning = false;
                            reader.Close();
                            connShow.Close();
                            break;
                        }
                    }
                    else
                    {
                        // Data ophalen en in variabelen zetten
                        string id = reader["id"].ToString();
                        string enqueteName = reader["naam"].ToString();
                        string kleinBericht = reader["kleinBericht"].ToString();
                        string grootBericht = reader["grootBericht"].ToString();
                        string gemaakt = reader["gemaakt"].ToString();


                        bool foutGekeurd = true;
                        while (foutGekeurd)
                        {
                            // Overzicht maken
                            Console.WriteLine("Id: " + id);
                            Console.WriteLine("Naam: " + enqueteName);
                            Console.WriteLine("Klein bericht: " + kleinBericht);
                            Console.WriteLine("Groot bericht: " + grootBericht);
                            Console.WriteLine("Gemaakt: " + gemaakt);    

                            Console.WriteLine("\nDruk [1] om goed te keuren");
                            Console.WriteLine("Druk [2] om af te keuren");
                            Console.WriteLine("\n\nDruk [X] om programma af te sluiten");

                            string userInput = Console.ReadLine();
                            Console.Clear();

                            bool updateQueryNeeded = false;

                            string keuring = "";

                            if (userInput == "1")
                            {
                                // Bericht is goedgekeurd
                                foutGekeurd = false;
                                keuring = "goedGekeurd";
                                updateQueryNeeded = true;
                                connShow.Close();
                            }
                            else if (userInput == "2")
                            {
                                // Bericht is afgekeurd
                                foutGekeurd = false;
                                keuring = "afgekeurd";
                                updateQueryNeeded = true;
                                connShow.Close();
                            }
                            else if (userInput.ToUpper() == "X")
                            {
                                // Programma wordt afgesloten
                                foutGekeurd = false;
                                isRunning = false;
                                reader.Close();
                                connShow.Close();
                                break;
                            }
                            else
                            {
                                // Gebruiker heeft foutieve keuze gemaakt
                                Console.WriteLine("Error: Ingevoerde waarde is geen optie, probeer het opnieuw");
                                Console.ReadLine();
                                Console.Clear();
                            }

                            if (updateQueryNeeded)
                            {
                                using (MySqlConnection connUpdate = new MySqlConnection(connectionString))
                                {
                                    try
                                    {
                                        connUpdate.Open();
                                        string queryInsert = "UPDATE berichten SET gekeurd = \"" + keuring + "\" WHERE id = \"" + id + "\";";
                                        MySqlCommand cmdInsert = new MySqlCommand(queryInsert, connUpdate);
                                        cmdInsert.ExecuteNonQuery();
                                        Console.WriteLine("Bericht is succesvol " + keuring);
                                        Console.WriteLine("Druk op enter om door te gaan");
                                        Console.ReadLine();
                                        Console.Clear();

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
