using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void CreatePopulateAndReadFromTestTable()
        {
            var db = new netstandardDbSQLiteHelper.Database(@"~/Desktop/temp/test.db");

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



        [TestMethod]
        public void TestInMemory(){
            var db = new netstandardDbSQLiteHelper.Database(null);

            db.Command(@"
                create table if not exists m1(
                    p1 varchar(50) not null,
                    p2 int not null,
                    p3 varchar(51) not null
                )
            ");

            for( int r = 0; r < 50; ++r ){
                
                db.Command(@"
                    insert into m1(p1,p2,p3)
                    values(:p1,:p2,:p3)
                ", new Dictionary<string, object>{
                    {":p1",""},
                    {":p2",-1},
                    {":p3",""}
                });
            }


            var results = db.Query(@"
                select *
                from m1
            ");

            Assert.IsTrue(results.Count > 40);

        }
    }
}