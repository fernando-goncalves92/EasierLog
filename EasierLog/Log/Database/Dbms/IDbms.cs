using System.Threading.Tasks;

namespace EasierLog
{
    internal interface IDbms
    {
        Task CreateTableIfNotExists();
        Task Save(string system, string module, string version, string user, object info, string infoDescription, LogLevel level);
    }
}
