using System;
using MySql.Data.MySqlClient;

namespace NewGame4.Configs
{
    public class DBMySQLUtils
    {
            public static MySqlConnection
                GetDbConnection(string host, int port, string database, string username, string password)
            {
                // Connection String.
                String connString = "Server=" + host + ";Database=" + database
                                    + ";port=" + port + ";User Id=" + username + ";password=" + password;
 
                MySqlConnection conn = new MySqlConnection(connString);
 
                return conn;
            }
            
    }
}