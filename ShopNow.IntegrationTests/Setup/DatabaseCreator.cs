using Microsoft.Data.SqlClient;

namespace ShopNow.IntegrationTests.Setup
{
    public static class DatabaseCreator
    {
        public static void CreateDatabase(string connectionString)
        {
            var connectionBuilder = new SqlConnectionStringBuilder(connectionString);
            var database = connectionBuilder.InitialCatalog;

            var connectionStringCompleted = new SqlConnectionStringBuilder(connectionBuilder.ConnectionString) { InitialCatalog = "master" }.ConnectionString;

            using var connection = new SqlConnection(connectionStringCompleted);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $@"IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{database}')
                              CREATE DATABASE [{database}]";
            command.ExecuteScalar();
        }
    }
}
