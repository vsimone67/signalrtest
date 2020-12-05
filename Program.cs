using System;
using System.Threading.Tasks;

namespace signalrtest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear(); // clear screen
            Console.WriteLine("Connecting to http://traefik.prod/mibhub");
            var mibHubConnection = await SignalRHubManager.CreateConnection<object>("http://traefik.prod/mibhub", "MibCompleted", (message) =>
            {
                WriteToConsole("Mib Received", ConsoleColor.DarkMagenta);
            });

            Console.WriteLine("Connecting to http://traefik.prod/facdecision");
            var facDecisionConnection = await SignalRHubManager.CreateConnection<object>("http://traefik.prod/facdecision", "FacDecisionMade", (message) =>
            {
                WriteToConsole("Fac Decision Made", ConsoleColor.DarkCyan);
            });

            Console.WriteLine("Connecting to http://traefik.prod/faccase");
            var facCaseConnection = await SignalRHubManager.CreateConnection<object>("http://traefik.prod/faccase", "FacCaseSubmitted", (message) =>
            {
                WriteToConsole("Fac Case Made", ConsoleColor.DarkYellow);
            });

            WriteToConsole("Listening for messages..", Console.ForegroundColor);
            WriteToConsole(string.Empty, Console.ForegroundColor); // skip space

            Console.ReadLine();
            WriteToConsole("Client is shutting down!", ConsoleColor.DarkRed);
            WriteToConsole("Closing Connections", ConsoleColor.Green);

            await mibHubConnection.Close();
            await facDecisionConnection.Close();
            await facCaseConnection.Close();

            WriteToConsole("All connections closed", ConsoleColor.Green);
        }

        static void WriteToConsole(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
