using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;
using System.IO;

namespace netstandardDbSQLiteHelper{
        public static class Utility
    {

        
        public static string GetConnectionString(string filePath)
        {
            filePath = ensureDatabaseFileExistsIfNotBlank(filePath);
            return ""+new SqliteConnectionStringBuilder{
                DataSource = !string.IsNullOrWhiteSpace(filePath) ? filePath : ":memory:"   
            };
        }


        static public string ensureDatabaseFileExistsIfNotBlank(string databaseFileName)
        {
            if(!string.IsNullOrWhiteSpace(databaseFileName) &&
                !System.IO.File.Exists(databaseFileName)
                )
            {
                if(databaseFileName.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
                {
                    databaseFileName = databaseFileName.Substring(2); // take out the first two chars
                    string homeFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    databaseFileName = System.IO.Path.Combine(homeFolder, databaseFileName);
                }
                FileStream fs = File.Create(databaseFileName);
                fs.Close();
            }

            return databaseFileName;

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