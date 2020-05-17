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
        public MainHub()
        {
            
        }
        
        public async Task GetNewChats()
        {
            Console.WriteLine("Getting chats");
        }
    }
}