namespace DBAccess.DatabaseConnectors
{
    public interface IDatabaseConnector
    {
        public string? GetDatabaseVersion(string? host, string databaseName, string databasePort);
        public string? ExecuteSQL(string SQLQuery);
        public string? GetTables();

    }
}