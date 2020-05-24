using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DopplerServer.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SharedTypes.SocialTypes;

namespace DopplerServer.Hubs
{
    
    public class MainHub:Hub
    {
        DataBaseContext dataBaseContext;
        [Authorize]
        public async Task GetContacts(string pattern)
        {
            List<Contact> Contacts;

            Contacts = await dataBaseContext.Users.Where(x => x.Contact.CallName.ToUpper().Contains(pattern.ToUpper()) || x.Contact.Name.ToUpper().Contains(pattern.ToUpper())).Select(x => x.Contact).ToListAsync();
            
            await Clients.Caller.SendAsync("ReceiveContacts", Contacts);
        }
        public MainHub(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
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