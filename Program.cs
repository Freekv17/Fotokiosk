using Spectre.Console;
using Spectre.Console.Cli;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=nsdatabase;Uid=root;Pwd=;";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string stationName = "";
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
        while (stationSelected == true)
            {
                bool isRepeating = false;
                Console.WriteLine("Vul uw naam in");
                AnsiConsole.Markup("[bold][grey]Vul niks in om anoniem te zijn.[/][/]\n\n");
                string name = Console.ReadLine().ToString();
                isRepeating = true;
                while (isRepeating)
                {
                    if (name == "")
                    {
                        name = "Anoniem";
                    }
                    Console.Clear();

                    Console.WriteLine("Schrijf een klein berichtje over het station\n");
                    string shortMessage = Console.ReadLine().ToString();
                    Console.Clear();
                    while (shortMessage == "")
                    {
                        Console.WriteLine("Schrijf een klein berichtje over het station\n");
                        shortMessage = Console.ReadLine().ToString();
                        Console.Clear();
                    }

                    Console.WriteLine("Schrijf je mening over het station\n");
                    string longMessage = Console.ReadLine().ToString();
                    Console.Clear();
                    while (longMessage == "")
                    {
                        Console.WriteLine("Schrijf je mening over het station\n");
                        longMessage = Console.ReadLine().ToString();
                        Console.Clear();
                    }

                    Console.WriteLine("Enter om te bevestigen of type annuleer om te stoppen\n");
                    string confirmation = Console.ReadLine().ToString();
                    if (confirmation.ToLower() == "annuleer")
                    {
                        Console.Clear();
                        isRepeating = false;
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO berichten (naam, kleinBericht, grootBericht, station) VALUES (@name, @shortMessage, @longMessage, @station)";

                        using (MySqlCommand command = new MySqlCommand(insertQuery, conn))
                        {
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@shortMessage", shortMessage);
                            command.Parameters.AddWithValue("@longMessage", longMessage);
                            command.Parameters.AddWithValue("@station", stationName);

                            int rowsAffected = command.ExecuteNonQuery();
                            AnsiConsole.Markup("[green]Bedankt voor uw bericht![/]");
                            System.Threading.Thread.Sleep(1000);
                            Console.Clear();
                            isRepeating = false;
                        }
                    }
                }
            }
            conn.Close();
        }
    }
}
