using MySql.Data.MySqlClient;

internal class Program
{
    private static void Main(string[] args)
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
                    Console.WriteLine("Kies op welk station je staat.");
                    string selectStation = Console.ReadLine();
                    Console.Clear();

                    while (reader.Read())
                    {
                        string stationName = reader["name"].ToString();
     
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }
    }
}