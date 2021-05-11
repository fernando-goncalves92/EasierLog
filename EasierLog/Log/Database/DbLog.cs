using System.Threading.Tasks;

namespace EasierLog
{
    internal class DbLog : ILog
    {
        private const string ConnectionStringPrefix = "Provider";
        private const string OleDbOracle1 = "OraOLEDB.Oracle";
        private const string OleDbOracle2 = "msdaora";
        private const string OleDbSQLServer1 = "MSOLEDBSQL";
        private const string OleDbSQLServer2 = "SQLOLEDB";
        private const string OleDbMySQL = "MySQLProv";
        private const string OleDbPostgre = "PostgreSQL OLE DB Provider";
        private const string MongoDB = "mongodb://";

        private readonly IDbms _databaseLog = null;

        private static DbLog _instance;

        public static DbLog Instance
        {
            get
            {
                if (_instance == null)                
                    _instance = new DbLog();

                return _instance;
            }
        }

        public DbLog()
        {
            if (Settings.ConnectionString.Contains(OleDbSQLServer1) || Settings.ConnectionString.Contains(OleDbSQLServer2))
                _databaseLog = new SQLServer();
            else if (Settings.ConnectionString.Contains(OleDbOracle1) || Settings.ConnectionString.Contains(OleDbOracle2))
                _databaseLog = new Oracle();
            else if (Settings.ConnectionString.Contains(OleDbMySQL))
                _databaseLog = new MySQL();
            else if (Settings.ConnectionString.Contains(OleDbPostgre))
                _databaseLog = new Postgre();
            else if (Settings.ConnectionString.Contains(MongoDB))
                _databaseLog = new MongoDB();
            else
                ConsoleHelper.Write("Connection string format invalid! Check out github project instructions: https://github.com/fernando-goncalves92/EasierLog");
        }

        public async Task Log(string system, string module, string version, string user, object info, string infoDescription, LogLevel logLevel)
        {
            await _databaseLog.Save(system, module, version, user, info, infoDescription, logLevel);
        }
    }
}
