using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor
{
    class Database
    {
        private static Database instance = null;
        private static readonly object padlock = new object();
       
        private static SqliteConnectionStringBuilder connectionString;

        Database()
        {
            connectionString = new SqliteConnectionStringBuilder();
            connectionString.DataSource = "UserData.db";

            //using (var connection = new SqliteConnection(connectionString.ConnectionString))
            //{
            //    connection.Open();

            //    var createTableCmd = connection.CreateCommand();
            //    createTableCmd.CommandText = "CREATE TABLE Users (email VARCHAR(40), password VARCHAR(18))";
            //    createTableCmd.ExecuteNonQuery();
            //}
        }

        public static Database Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new Database();
                }
                return instance;
            }
        }

        public SqliteConnectionStringBuilder GetConnectionString { get { return connectionString; } }
    }
}
