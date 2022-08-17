using System.Data.Common;
using Npgsql;

namespace DBAccess.DatabaseConnectors
{
    public class PostgresConnector : IDatabaseConnector
    {
        private static string? dbUsername = Environment.GetEnvironmentVariable("DB_USERNAME");
        private static string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        private NpgsqlConnection? connection;

        public PostgresConnector(string? host, string? databaseName, string? databasePort)
        {
            host = (host == null) ? "localhost" : host;
            databaseName = (databaseName == null) ? "postgres" : databaseName;
            databasePort = (databasePort == null) ? "5432" : databasePort;

            var connectionString = buildConnectionString(host, dbUsername, dbPassword, databaseName, databasePort);
            try
            {
                ConnectToDatabase(connectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public string GetTables()
        {
            var sqlQuerry = "select * from pg_catalog.pg_tables where schemaname != 'pg_catalog' and schemaname != 'information_schema';";
            return ExecuteSQL(sqlQuerry);
        }
        public static string? GetUsername()
        {
            return dbUsername;
        }

        public string? GetDatabaseVersion(string? host, string databaseName, string databasePort)
        {

            var sqlQuery = "SELECT version()";

            return ExecuteSQL(sqlQuery);
        }

        public string ExecuteSQL(string SQLQuery)
        {
            using var command = new NpgsqlCommand(SQLQuery, connection);
            string? queryResult = "";
            try
            {
                if (command.ExecuteScalar() != null &&
                    connection != null)
                {
                    queryResult = command.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }


            return queryResult;
        }

        private void ConnectToDatabase(string connectionString)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        private string buildConnectionString(string host, string? username, string? password, string databaseName, string databasePort)
        {
            string usernameConstruct =
                username == null ? "" : $"Username={username};";
            string passwordConstruct =
                password == null ? "" : $"Password={password};";
            return $"Host={host};{usernameConstruct}{passwordConstruct}Database={databaseName};Port={databasePort}";
        }

    }
}