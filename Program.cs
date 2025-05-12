using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=nsdatabase;Uid=root;Pwd=;";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            bool stationSelected = false;
            try
            {
                conn.Open();
                string query = "SELECT * FROM netherlands_train_stations;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (stationSelected == false)
                {
                    Console.WriteLine("Kies uit op welk station je staat.");
                    string selectStation = Console.ReadLine();
                    Console.Clear();

                    while (reader.Read())
                    {
                        string stationName = reader["name"].ToString();
                        //Console.WriteLine(stationName);


                        // Optional comparison
                        if (selectStation.ToLower() == stationName.ToLower())
                        {
                            Console.WriteLine("Station selected: " + stationName);
                            stationSelected = true;
                            reader.Close();
                            conn.Close();
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            while (stationSelected)
            {
                //Console.WriteLine("");
            }
        }
    }
}
