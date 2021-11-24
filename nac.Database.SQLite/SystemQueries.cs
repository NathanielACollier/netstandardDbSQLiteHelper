using System.Collections.Generic;
using System;
using System.Linq;

namespace nac.Database.SQLite
{
    public static class SystemQueries
    {
        public static bool doesTableExist(Database db, string tableName)
        {
            // .net doesn't have sqlite 3.3 that can do 'drop table if exists <table_name>' so need to check for table manually
            // -- https://stackoverflow.com/questions/1601151/how-do-i-check-in-sqlite-whether-a-table-exists?noredirect=1&lq=1
            var t = db.Query(@"
                SELECT name 
                FROM sqlite_master 
                WHERE type='table' AND name= @table_name
            ",new Dictionary<string, object>
            {
                { "@table_name", tableName }
            });

            return t.Count() > 0;
        }
    }

}