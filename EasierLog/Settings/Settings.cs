using System;
using System.Configuration;
using System.Collections.Specialized;

namespace EasierLog
{
    internal class Settings 
    {          
        public static LogDestination DestinationLog { get; private set; }
        public static DatabaseConventionPattern DatabaseConventionPatternForTableAndColumns { get; private set; }
        public static string ConnectionString { get; private set; }
        public static string DatabaseTableToStoreLog { get; private set; }        
        public static string DirectoryToStoreLog { get; private set; }        
        public static int DaysToKeepLogFiles { get; private set; }
        public static bool SaveLogFileInCaseOfDatabaseLogFail { get; private set; }
        public static bool ConfigLoaded { get; private set; }

        public static void LoadAppSettings()
        {
            try
            {   
                var easyLoggerSettings = ConfigurationManager.GetSection("EasierLog") as NameValueCollection;
                
                if (easyLoggerSettings != null)
                {
                    if (Enum.TryParse<LogDestination>(easyLoggerSettings["destinationLog"], out var destinationLog))
                        DestinationLog = destinationLog;

                    if (Enum.TryParse<DatabaseConventionPattern>(easyLoggerSettings["databaseConventionPatternForTableAndColumns"], out var databaseConventionPatternForTableAndColumns))
                        DatabaseConventionPatternForTableAndColumns = databaseConventionPatternForTableAndColumns;

                    ConnectionString = easyLoggerSettings["connectionString"];
                    DatabaseTableToStoreLog = easyLoggerSettings["databaseTableToStoreLog"];                    
                    DirectoryToStoreLog = easyLoggerSettings["directoryToStoreLog"];

                    if (int.TryParse(easyLoggerSettings["daysToKeepLogFiles"], out var daysToKeepLogFiles))
                        DaysToKeepLogFiles = daysToKeepLogFiles;
                    
                    if (bool.TryParse(easyLoggerSettings["saveLogFileInCaseOfDatabaseLogFail"], out var saveLogFileInCaseOfDatabaseLogFail))
                        SaveLogFileInCaseOfDatabaseLogFail = saveLogFileInCaseOfDatabaseLogFail;
                }

                ConfigLoaded = true;
            }
            catch (Exception error)
            {
                ConsoleHelper.Write(error, "Error trying to load the app.confg ");
            }
        }
    }
}
