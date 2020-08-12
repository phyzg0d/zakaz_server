using MySql.Data.MySqlClient;

namespace NewGame4.Configs
{
    public class DBUtils
    {
        public static MySqlConnection GetDbConnection( )
        {
            string host = "139.162.166.31";
            int port = 3306;
            string database = "biromiro_zakaz";
            string username = "biromiro_test";
            string password = "b{xVtmW_N*Rf";
 
            return DBMySQLUtils.GetDbConnection(host, port, database, username, password);
        }
    }
}