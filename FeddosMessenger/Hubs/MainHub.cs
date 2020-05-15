using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace FeddosMessenger.Hubs
{
    [Authorize]
    public class MainHub:Hub
    {
        public MainHub(HttpContext context)
        {
            
        }
        
        
    }
}