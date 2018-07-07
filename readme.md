+ Steps to create solution
    + mkdir netstandardDbSQLiteHelper
    + cd netstandardDbSQLiteHelper
    + dotnet new sln
    + mkdir netstandardDbSQLiteHelper
    + cd netstandardDbSQLiteHelper
    + dotnet new classlib
    + cd ..
    + dotnet sln add .\netstandardDbSQLiteHelper\netstandardDbSQLiteHelper.csproj
    + mkdir tests
    + cd tests
    + dotnet new mstest
    + cd ..
    + dotnet sln add .\tests\tests.csproj
    + git init
    + git remote add origin https://github.com/NathanielACollier/netstandardDbSQLiteHelper
    + git push -u origin master

+ Steps to getting started with SQLite code in dotnet core
    + cd .\netstandardDbSQLiteHelper\
    + dotnet add package Microsoft.Data.SQLite --version 2.1.0
    
+ Getting the tests project to work
    + cd .\tests\
    + dotnet add reference ..\netstandardDbSQLiteHelper\netstandardDbSQLiteHelper.csproj
    + Copy the Microsoft.Data.SQLite package reference to the tests.csproj
    + dotnet restore
    