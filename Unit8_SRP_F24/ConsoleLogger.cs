using System;
using System.Security;
using Unit8_SRP_F24.Contracts;

namespace Unit8_SRP_F24;

public class ConsoleLogger : ILogger
{
    private readonly string logFile = "log.xml";

    public void LogMessage(string message, params object[] args)
    {
        string text = string.Format(message, args);
        Console.WriteLine(text);

        string type = "INFO";
        int colon = text.IndexOf(':');
        if (colon > 0 && (text.StartsWith("INFO") || text.StartsWith("WARN") || text.StartsWith("ERROR")))
        {
            type = text.Substring(0, colon);
            text = text.Substring(colon + 1).Trim();
        }

        using (StreamWriter logfile = File.AppendText(logFile))
        {
            logfile.WriteLine("<log><type>" + type + "</type><message>" + SecurityElement.Escape(text) + "</message></log>");
        }
    }
}
