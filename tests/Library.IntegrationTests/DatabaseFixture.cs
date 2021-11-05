using Npgsql;
using System;

namespace Library.IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {
        public NpgsqlConnection Db { get; private set; }

        public DatabaseFixture()
        {
            Db = new NpgsqlConnection("Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;Include Error Detail=true;");

            // ... initialize data in the test database ...
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }

        
    }
}