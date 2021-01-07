using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor
{
    class DatabaseContext
    {
        private static DatabaseContext instance = null;
        private static readonly object padlock = new object();
       
        private static SqliteConnectionStringBuilder connectionString;

        DatabaseContext()
        {
            connectionString = new SqliteConnectionStringBuilder();
            connectionString.DataSource = "./UserData.db";
        }

        public static DatabaseContext Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new DatabaseContext();
                }
                return instance;
            }
        }

        public SqliteConnectionStringBuilder GetConnectionString { get { return connectionString; } }
    }
}
