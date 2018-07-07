using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace netstandardDbSQLiteHelper
{
    public class Database
    {
        private string connectionString;

        public Database(string databaseFilePath)
        {
            this.connectionString = Utility.GetConnectionString(databaseFilePath);
        }

        public int Command(string commandText, Dictionary<string, object> parameters = null)
        {
            int result = -1;

            Utility.useSQLiteCommand(connectionString: connectionString,
                commandText: commandText,
                parameters: parameters,
                funcToRunOnCommand: (cmd) =>
                {
                    cmd.Connection.Open();
                    result = cmd.ExecuteNonQuery();
                });

            return result;
        }



        public List<Dictionary<string,object>> Query(string queryText, Dictionary<string, object> parameters = null)
        {
            var rows = new List<Dictionary<string,object>>();

            Utility.useSQLiteCommand(connectionString: connectionString,
                commandText: queryText,
                parameters: parameters,
                funcToRunOnCommand: (cmd) =>
                {
                    cmd.Connection.Open();
                    var reader = cmd.ExecuteReader();

                    while( reader.Read()){
                        var dict = Enumerable.Range(0, reader.FieldCount)
                                    .ToDictionary(reader.GetName, reader.GetValue);
                        
                        rows.Add(dict);
                    }
                    cmd.Connection.Close();
                });
            return rows;
        }

    }
}
