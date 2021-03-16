using System.Threading.Tasks;

namespace EasierLog
{
    public static class EasierLogger
    {
        /// <summary>
        /// Generates information log
        /// </summary>
        /// <param name="system">System where log was triggered</param>
        /// <param name="module">Module where log was triggered</param>
        /// <param name="version">System version</param>
        /// <param name="user">User who was using the system when the log was triggered</param>
        /// <param name="info">The information that needs to be logged. Usually the entire exception.</param>
        /// <param name="infoDescription">Description about logged information</param>
        /// <returns></returns>
        public static Task Info(string system, string module, string version, string user, object info, string infoDescription)
        {
            return Log(system, module, version, user, info, infoDescription, LogLevel.Information);
        }

        /// <summary>
        /// Generates warning log
        /// </summary>
        /// <param name="system">System where log was triggered.</param>
        /// <param name="module">Module where log was triggered.</param>
        /// <param name="version">System version.</param>
        /// <param name="user">User who was using the system when the log was triggered.</param>
        /// <param name="info">The information that needs to be logged. Usually the entire exception.</param>
        /// <param name="infoDescription">Description about logged information.</param>
        /// <returns></returns>
        public static Task Warning(string system, string module, string version, string user, object info, string infoDescription)
        {
            return Log(system, module, version, user, info, infoDescription, LogLevel.Warning);
        }

        /// <summary>
        /// Generates error log type
        /// </summary>
        /// <param name="system">System where log was triggered.</param>
        /// <param name="module">Module where log was triggered.</param>
        /// <param name="version">System version.</param>
        /// <param name="user">User who was using the system when the log was triggered.</param>
        /// <param name="info">The information that needs to be logged. Usually the entire exception.</param>
        /// <param name="infoDescription">Description about logged information.</param>
        /// <returns></returns>
        public static Task Error(string system, string module, string version, string user, object info, string infoDescription)
        {
            Warning(system, module, version, user, null, infoDescription);

            return Log(system, module, version, user, info, infoDescription, LogLevel.Error);
        }

        /// <summary>
        /// Generates a simple log information
        /// </summary>
        /// <param name="info">The information to be logged.</param>
        /// <returns></returns>
        public static Task Trace(string info)
        {
            return Log("-", "-", "-", "-", info, "Trace", LogLevel.Information);
        }

        private static Task Log(string system, string module, string version, string user, object info, string infoDescription, LogLevel logLevel)
        {
            Task result = null;

            if (!Settings.ConfigLoaded)
            {
                Settings.LoadAppSettings();
            }

            if (Settings.ConfigLoaded)
            {
                switch (Settings.DestinationLog)
                {
                    default:
                    case LogDestination.File:
                        {
                            result = Task.Run(() => FileLog.Instance.Log(system, module, version, user, info, infoDescription, logLevel));

                            result.ContinueWith(task =>
                            {
                                ConsoleHelper.Write(task.Exception.InnerException, "Error trying to save log in file");
                            },
                            TaskContinuationOptions.OnlyOnFaulted);

                            break;
                        }
                    case LogDestination.Database:
                        {
                            result = Task.Run(() => DbLog.Instance.Log(system, module, version, user, info, infoDescription, logLevel));

                            result.ContinueWith(task =>
                            {
                                if (Settings.SaveLogFileInCaseOfDatabaseLogFail)
                                {
                                    result = Task.Run(async () => await FileLog.Instance.Log(system, module, version, user, info, infoDescription, logLevel));

                                    result.ContinueWith(taskFile =>
                                    {
                                        ConsoleHelper.Write(taskFile.Exception.InnerException, "Error trying to save log in database and file");
                                    },
                                    TaskContinuationOptions.OnlyOnFaulted);
                                }
                                else                                
                                    ConsoleHelper.Write(task.Exception.InnerException, "Error trying to save log in database");
                            },
                            TaskContinuationOptions.OnlyOnFaulted);

                            break;
                        }
                }
            }            

            return result;
        }
    }
}
