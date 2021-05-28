using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doppler.REST.Services
{
    public class HubClientsMappingService
    {
        private SortedDictionary<string, string> clients { get; set; }

        public HubClientsMappingService()
        {
            this.clients = new SortedDictionary<string, string>();
        }

        public string Get(string phoneNumber)
        {
            if (this.clients.ContainsKey(phoneNumber))
            {
                return clients[phoneNumber];
            }

            return null;
        }

        public void Set(string phoneNumber, string connectionId)
        {
            if (clients.ContainsKey(phoneNumber))
            {
                this.clients[phoneNumber] = connectionId;
            }
            else
            {
                this.clients.Add(phoneNumber, connectionId);
            }
        }

        public void Remove(string phoneNumber)
        {
            if (clients.ContainsKey(phoneNumber))
            {
                this.clients.Remove(phoneNumber);
            }
        }
    }
}
