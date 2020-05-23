using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FeddosMessenger.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SharedTypes.SocialTypes;

namespace FeddosMessenger.Hubs
{
    
    public class MainHub:Hub
    {
        [Authorize]
        public async Task GetContacts(string pattern)
        {
            List<Contact> Contacts;
            if (!string.IsNullOrEmpty(pattern))
            {
                Contacts = await MongoDbContext.UsersCollection.AsQueryable().Where(x => x.CallName.Contains(pattern) || x.Contact.Name.Contains(pattern)).Select(x => x.Contact).ToListAsync();
            }
            else
            {
                Contacts = await MongoDbContext.UsersCollection.AsQueryable().Select(x => x.Contact).ToListAsync();
            }
            await Clients.Caller.SendAsync("ReceiveContacts", Contacts);
        }
        [Authorize]
        public async Task CheckAuthorization()
        {
            await Clients.Caller.SendAsync("CheckAuthResult", true);
        }
        [Authorize]
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected! " + Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        
    }
}