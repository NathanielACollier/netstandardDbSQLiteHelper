using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Data;

namespace netstandardDbSQLiteHelper
{
    public class Database
    {
        private string connectionString;

        public Database(string databaseFilePath)
        {
            if(string.IsNullOrWhiteSpace(databaseFilePath))
            {
                throw new Exception("Database file path is empty");
            }

            if(!System.IO.File.Exists(databaseFilePath))
            {
                SQLiteConnection.CreateFile(databaseFilePath);
            }

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



        public DataTable Query(string queryText, Dictionary<string, object> parameters = null)
        {
            var dt = new DataTable();

            Utility.useSQLiteCommand(connectionString: connectionString,
                commandText: queryText,
                parameters: parameters,
                funcToRunOnCommand: (cmd) =>
                {
                    var da = new SQLiteDataAdapter(cmd);

                    da.Fill(dt);
                });

            return dt;
        }

    }
}
