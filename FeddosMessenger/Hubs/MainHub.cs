using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace FeddosMessenger.Hubs
{
    [Authorize]
    public class MainHub:Hub
    {
        
        public async Task GetNewChats(string test)
        {
            Console.WriteLine("Getting chats");
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected! " + Context.ConnectionId);
            await base.OnConnectedAsync();
        }
    }
}