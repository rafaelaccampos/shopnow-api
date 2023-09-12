using Microsoft.Data.SqlClient;

namespace ShopNow.IntegrationTests.Setup
{
    public class DatabaseCreator
    {
        public void CreateDatabase(string connectionString)
        {
            var connectionBuilder = new SqlConnectionStringBuilder(connectionString);
            var database = connectionBuilder.InitialCatalog;
            connectionBuilder.InitialCatalog = "master";

            var connection = new SqlConnectionStringBuilder(connectionBuilder.ConnectionString) { InitialCatalog = "master" };

            connection.Execute($@"IF NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'{database}')
                              CREATE DATABASE [{database}]");
        }
    }
}
