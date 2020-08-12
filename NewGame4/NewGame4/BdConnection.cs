using System;
using System.IO;
using MySql.Data.MySqlClient;
using NewGame4.Utilities;

namespace NewGame4
{
    public class BdConnection
    {
        public MySqlConnection Connection;
        public void Connect()
        {
            var data = JsonLoader.Load(@"Resources\ServerConfig.json");
            var bdConfig = data.GetNode("BD");
            var pathDb = $"server={bdConfig.GetString("host")};userid={bdConfig.GetString("username")};password={bdConfig.GetString("password")};database={bdConfig.GetString("database")}"; 
            
            Connection = new MySqlConnection(pathDb);
            Connection.Open();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connection successful\n");
        }

        public void CloseConnect()
        {
            Connection.Close();
        }
    }
}