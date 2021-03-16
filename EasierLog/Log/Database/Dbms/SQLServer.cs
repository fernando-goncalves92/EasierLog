using System;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace EasierLog
{
    internal class SQLServer : IDbms
    {
        public async Task CreateTableIfNotExists()
        {
            using (OleDbConnection connection = new OleDbConnection(Settings.ConnectionString))
            {
                await connection.OpenAsync();

                using (OleDbCommand command = connection.CreateCommand())
                {
                    StringBuilder query = new StringBuilder();

                    query.Append($" if object_id({Settings.DatabaseTableToStoreLog.ToSqlString()}) is null ");
                    query.Append($"    create table [{Settings.DatabaseTableToStoreLog}] ");
                    query.Append($"    ( ");
                    query.Append($"       [{"id".ToConventionPattern()}] bigint identity(1,1) ");
                    query.Append($"      ,[{"system".ToConventionPattern()}] varchar(50) ");
                    query.Append($"      ,[{"module".ToConventionPattern()}] varchar(50) ");
                    query.Append($"      ,[{"version".ToConventionPattern()}] varchar(50) ");
                    query.Append($"      ,[{"user".ToConventionPattern()}] varchar(50) ");
                    query.Append($"      ,[{"date".ToConventionPattern()}] datetime ");
                    query.Append($"      ,[{"info".ToConventionPattern()}] varchar(max) ");
                    query.Append($"      ,[{"info_description".ToConventionPattern()}] varchar(100) ");
                    query.Append($"      ,[{"level".ToConventionPattern()}] varchar(50) ");
                    query.Append($"    ) ");

                    command.CommandText = query.ToString();

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task Save(string system, string module, string version, string user, object info, string infoDescription, LogLevel level)
        {
            using (OleDbConnection connection = new OleDbConnection(Settings.ConnectionString))
            {
                await CreateTableIfNotExists();

                await connection.OpenAsync();

                using (OleDbCommand command = connection.CreateCommand())
                {
                    StringBuilder query = new StringBuilder();

                    query.Append($" insert into [{Settings.DatabaseTableToStoreLog}] ");
                    query.Append($" ( ");
                    query.Append($"    [{"system".ToConventionPattern()}]");
                    query.Append($"   ,[{"module".ToConventionPattern()}]");
                    query.Append($"   ,[{"version".ToConventionPattern()}]");
                    query.Append($"   ,[{"user".ToConventionPattern()}]");
                    query.Append($"   ,[{"date".ToConventionPattern()}]");
                    query.Append($"   ,[{"info".ToConventionPattern()}]");
                    query.Append($"   ,[{"info_description".ToConventionPattern()}]");
                    query.Append($"   ,[{"level".ToConventionPattern()}]");
                    query.Append($" ) ");
                    query.Append($" values ");
                    query.Append($" ( ");
                    query.Append($"    {system.ToSqlString()} ");
                    query.Append($"   ,{module.ToSqlString()} ");
                    query.Append($"   ,{version.ToSqlString()} ");
                    query.Append($"   ,{user.ToSqlString()} ");
                    query.Append($"   ,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToSqlString()} ");
                    query.Append($"   ,{info.ToString().ToSqlString()} ");
                    query.Append($"   ,{infoDescription.ToSqlString()} ");
                    query.Append($"   ,{level.ToString().ToSqlString()} ");
                    query.Append($" ) ");

                    command.CommandText = query.ToString();

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
