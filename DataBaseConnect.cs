using System;
using System.Data.SqlClient;

namespace GroceryOrderingManagementSystem
{
    public sealed class DatabaseHelper
    {
        //=====================================================
        // DATABASE CONFIGURATION
        //=====================================================
        private const string DefaultConnectionString =
            @"Server=MANNY-EMMANUEL-;
              Database=master;
              Trusted_Connection=True;
              TrustServerCertificate=True;";

        private readonly string connectionString;

        public DatabaseHelper()
        {
            // A deployment can override the development connection without
            // recompiling the application.
            connectionString =
                Environment.GetEnvironmentVariable("GROCERY_DB_CONNECTION")
                ?? DefaultConnectionString;
        }

        //=====================================================
        // APPLICATION DATABASE CONNECTION
        //=====================================================
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        //=====================================================
        // MASTER CONNECTION FOR BACKUP AND RESTORE
        //=====================================================
        public SqlConnection GetMasterConnection()
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(connectionString)
                {
                    InitialCatalog = "master"
                };

            return new SqlConnection(builder.ConnectionString);
        }

        public string DatabaseName
        {
            get
            {
                SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder(connectionString);

                return string.IsNullOrWhiteSpace(builder.InitialCatalog)
                    ? "GroceryOrderingManagementSystem"
                    : builder.InitialCatalog;
            }
        }
    }
}
