using MySql.Data.MySqlClient;

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

                while (stationSelected == false)
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    Console.WriteLine("Kies op welk station je staat.");
                    string selectStation = Console.ReadLine();
                    Console.Clear();

                    while (reader.Read())
                    {
                        stationName = reader["name"].ToString();

                        if (selectStation.ToLower() == stationName.ToLower())
                        {
                            Console.WriteLine("Station geselecteerd: " + stationName);
                            stationSelected = true;
                            reader.Close();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }

                    if (!stationSelected)
                    {
                        Console.WriteLine("Error: Station " + selectStation + " bestaat niet, probeer het opnieuw.");
                        reader.Close();
                    }
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


                        bool foutGekeurd = true;
                        while (foutGekeurd)
                        {
                            // Overzicht maken
                            Console.WriteLine("Id: " + id);
                            Console.WriteLine("Naam: " + enqueteName);
                            Console.WriteLine("Klein bericht: " + kleinBericht);
                            Console.WriteLine("Groot bericht: " + grootBericht);

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
