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
    