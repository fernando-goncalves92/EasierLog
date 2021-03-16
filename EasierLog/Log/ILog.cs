using System.Threading.Tasks;

namespace EasierLog
{
    internal interface ILog
    {
        Task Log(string system, string module, string version, string user, object info, string infoDescription, LogLevel logLevel);
    }
}
