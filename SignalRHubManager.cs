using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace signalrtest
{
    public class SignalRHubManager
    {
        public string HubUrl { get; set; }
        private HubConnection _hubConnection;
        public SignalRHubManager(string hubUrl)
        {
            HubUrl = hubUrl;
            _hubConnection = new HubConnectionBuilder().WithUrl(HubUrl).WithAutomaticReconnect().Build();
        }
        public void AddMessageHandler<T>(string message, Action<T> messageHandler)
        {
            _hubConnection.On<T>(message, messageHandler);

        }
        public async Task Connect()
        {
            await _hubConnection.StartAsync();

        }
        public async Task Close()
        {
            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();
        }
        public async static Task<SignalRHubManager> CreateConnection<T>(string hubUrl, string message, Action<T> messageHandler)
        {
            var hubConnection = new SignalRHubManager(hubUrl);
            hubConnection.AddMessageHandler(message, messageHandler);
            await hubConnection.Connect();

            return hubConnection;
        }

    }
}
