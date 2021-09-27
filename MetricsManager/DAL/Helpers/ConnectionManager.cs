using System.Data.SQLite;

namespace MetricsManager.DAL
{
    public class ConnectionManager
    {
        public const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        //"Data Source=metrics.db; Version=3;";
        //"Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        public SQLiteConnection GetOpenedConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            return connection.OpenAndReturn();
        }
    }
}