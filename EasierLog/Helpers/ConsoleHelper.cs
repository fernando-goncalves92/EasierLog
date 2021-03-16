using System;
using System.Diagnostics;

namespace EasierLog
{
    internal static class ConsoleHelper
    {
        public static void Write(Exception exception, string description)
        {
            Debug.WriteLine($"****************************************************************************************");
            Debug.WriteLine($"Description: {description}");
            Debug.WriteLine($"----------------------------------------------------------------------------------------");
            Debug.WriteLine($"Exception Type: {exception.GetType().Name}");
            Debug.WriteLine($"----------------------------------------------------------------------------------------");
            Debug.WriteLine($"Exception Message: {exception.Message}");
            Debug.WriteLine("*****************************************************************************************");
        }

        public static void Write(string message)
        {
            Debug.WriteLine($"****************************************************************************************");
            Debug.WriteLine($"{message}");            
            Debug.WriteLine("*****************************************************************************************");
        }
    }
}
