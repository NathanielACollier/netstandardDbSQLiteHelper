using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;

namespace netstandardDbSQLiteHelper{
        public static class Utility
    {

        
        public static string GetConnectionString(string filePath)
        {
            return ""+new SqliteConnectionStringBuilder{
                DataSource = !string.IsNullOrWhiteSpace(filePath) ? filePath : ":memory:"   
            };
        }


        public static void useSQLiteCommand(string connectionString,
                                string commandText,
                                Action<SqliteCommand> funcToRunOnCommand,
                                Dictionary<string,object> parameters = null)
        {
            using (var sqlconn = new SqliteConnection(connectionString))
            {
                var cmd = new SqliteCommand(commandText, sqlconn);

                if( parameters != null && parameters.Count > 0)
                {
                    foreach(var e in parameters)
                    {
                        cmd.Parameters.Add(new SqliteParameter(e.Key, e.Value));
                    }
                }

                funcToRunOnCommand(cmd);
            }
        }


        


    }
}