using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    public class ConnectionManager
    {
        public const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public SQLiteConnection GetOpenedConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            return connection.OpenAndReturn();
        }
    }
}