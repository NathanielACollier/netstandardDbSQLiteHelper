using System.Data.SQLite;
using System.Collections.Generic;
using System;

namespace netstandardDbSQLiteHelper{
        public static class Utility
    {
        public static string GetConnectionString(string filePath)
        {
            return $"Data Source={filePath}; datetimeFormat=CurrentCulture;";
        }


        public static void useSQLiteCommand(string connectionString,
                                string commandText,
                                Action<SQLiteCommand> funcToRunOnCommand,
                                Dictionary<string,object> parameters = null)
        {
            using (var sqlconn = new SQLiteConnection(connectionString))
            {
                var cmd = new SQLiteCommand(commandText, sqlconn);

                if( parameters != null && parameters.Count > 0)
                {
                    foreach(var e in parameters)
                    {
                        cmd.Parameters.Add(new SQLiteParameter(e.Key, e.Value));
                    }
                }

                funcToRunOnCommand(cmd);
            }
        }


        


    }
}