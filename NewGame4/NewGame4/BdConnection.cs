using System;
using MySql.Data.MySqlClient;

namespace NewGame4
{
    public class BdConnection
    {
        public MySqlConnection Connection;
        private string _pathDb = @"server=139.162.166.31;userid=biromiro_test;password=b{xVtmW_N*Rf;database=biromiro_zakaz";

        public void Connect()
        {
            Connection = new MySqlConnection(_pathDb);
            Connection.Open();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nConnection successful\n");
        }

        public void CloseConnect()
        {
            Connection.Close();
        }
    }
}