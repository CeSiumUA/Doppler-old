using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeddosMessengerApp
{
    class CommunicationHub
    {
        public static HubConnection hubConnection;
        public static void InitiateHub()
        {
            hubConnection = new HubConnectionBuilder().
        }
    }
}
