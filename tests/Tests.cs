using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void CreatePopulateAndReadFromTestTable()
        {
            var db = new netstandardDbSQLiteHelper.Database(@"C:\users\ncollier\desktop\test.db");

            db.Command(@"
                drop table if exists test;
                CREATE TABLE test(
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    timestamp datetime  DEFAULT CURRENT_TIMESTAMP,
                    val1 varchar(50),
                    val2 varchar(50),
                    val3 varchar(50)
                );
            ");

            db.Command(@"
                INSERT INTO test (val1,val2,val3)
                VALUES(@v1, @v2, @v3)
            ", new System.Collections.Generic.Dictionary<string, object>
            {
                {"@v1", "7" },
                {"@v2", "Happy Birthday!" },
                {"@v3", "8" }
            });

            var dt = db.Query(@"
                select *
                from test
            ");

            
            Assert.IsTrue(string.Equals(dt[0]["val2"] as string, "Happy Birthday!", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(string.Equals(dt[0]["val3"], "8"));

        }
    }
}