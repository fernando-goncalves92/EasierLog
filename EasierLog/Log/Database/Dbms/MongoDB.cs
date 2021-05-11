using MongoDB.Driver;
using System.Threading.Tasks;

namespace EasierLog
{
    internal class MongoDB : IDbms
    {
        private readonly IMongoDatabase _db;
        private readonly string _collection;

        public MongoDB()
        {
            var mongoUrl = new MongoUrl(Settings.ConnectionString);            
            
            _db = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);            
            
            _collection = Settings.DatabaseTableToStoreLog;
        }

        public Task CreateTableIfNotExists() { return null; }

        public async Task Save(string system, string module, string version, string user, object info, string infoDescription, LogLevel level)
        {
            var log = new LogObjectStructure()
            {
                System = system,
                Module = module,
                Version = version,
                User = user,
                Info = info,
                InfoDescription = infoDescription,
                Level = level
            };

            await _db.GetCollection<LogObjectStructure>(_collection).InsertOneAsync(log);
        }
    }
}
