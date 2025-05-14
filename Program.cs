using Google.Protobuf;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Threading;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=nsdatabase;Uid=root;Pwd=;";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string selectStation = "";
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
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (selectStation.Equals(reader["name"].ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                Console.Clear();
                                Console.WriteLine("Geselecteerd station: " + reader["name"].ToString());
                                stationSelected = true;
                                selectStation = reader["name"].ToString();
                                Thread.Sleep(1000);
                                reader.Close();
                                Console.Clear();
                                break;
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

            while (stationSelected == true)
            {
                bool isRepeating = false;
                Console.WriteLine("Vul uw naam in\n");
                string name = Console.ReadLine().ToString();
                isRepeating = true;
                while (isRepeating)
                {
                    if (name == null)
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
                            command.Parameters.AddWithValue("@station", selectStation);

                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine("Bedankt voor uw bericht!");
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
