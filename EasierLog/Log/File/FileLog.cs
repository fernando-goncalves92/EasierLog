﻿using System;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasierLog
{
    internal class FileLog : ILog
    {   
        private string _fileOfDay => $"{Settings.DirectoryToStoreLog}\\{DateTime.Now:yyyyMMdd}.xml";

        private static FileLog _instance;
        public static FileLog Instance
        {
            get 
            {
                if (_instance == null)
                    _instance = new FileLog();

                return _instance; 
            }            
        }

        public async Task Log(string system, string module, string version, string user, object info, string infoDescription, LogLevel logLevel)
        {
            await Save(system, module, version, user, info, infoDescription, logLevel);
        }

        private async Task Save(string system, string module, string version, string user, object info, string infoDescription, LogLevel level)
        {
            await FileHelper.DeletePastFilesAccorddingSettings();

            await Task.Run(() =>
            {
                if (FileHelper.CreateDirectoryIfNotExists(Settings.DirectoryToStoreLog))
                {
                    using (StreamWriter streamWriter = File.AppendText(_fileOfDay))
                    {
                        using (XmlTextWriter xmlWriter = new XmlTextWriter(streamWriter))
                        {
                            xmlWriter.WriteStartElement("Log");
                            xmlWriter.WriteElementString("Type", level.ToString());
                            xmlWriter.WriteElementString("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            xmlWriter.WriteElementString("System", system);
                            xmlWriter.WriteElementString("Module", module);
                            xmlWriter.WriteElementString("Version", version);
                            xmlWriter.WriteElementString("User", user);
                            xmlWriter.WriteElementString("Info", JsonConvert.SerializeObject(info));
                            xmlWriter.WriteElementString("InfoDescription", infoDescription);
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteWhitespace("\n");
                        }
                    }
                }
            });
        }
    }
}