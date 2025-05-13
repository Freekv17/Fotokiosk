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
                
                

                while (stationSelected == false)
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    Console.WriteLine("Kies uit op welk station je staat.\n");
                    string selectStation = Console.ReadLine();
                    Console.Clear();
                    while (reader.Read())
                    {
                        string stationName = reader["name"].ToString();

                        if (selectStation.ToLower() == stationName.ToLower())
                        {
                            Console.Clear();
                            Console.WriteLine("Geselecteerd station: " + stationName);
                            stationSelected = true;
                            System.Threading.Thread.Sleep(1000);
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
            if (stationSelected == true)
            {
                Console.WriteLine("Vul uw naam in\n");
                string name = Console.ReadLine();
                if (name == null)
                {
                    name = "Anoniem";
                }
                Console.Clear();
                Console.WriteLine("Schrijf een klein berichtje over het station\n");
                string shortMessage = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Schrijf je mening over het station");
                string longMessage = Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
