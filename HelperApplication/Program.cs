using System;
using Microsoft.EntityFrameworkCore.SqlServer;
using SharedTypes.SocialTypes;
using SharedTypes.Tokens;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Driver.Linq;
using SharedTypes.Cryptography;

namespace HelperApplication
{
    class Program
    {
        private static IMongoCollection<User> collection;
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase("messengerdatabase");
            collection = dataBase.GetCollection<User>("Users");
            
            Console.WriteLine("Enter users amount:");
            List<User> usersToAdd = DatabaseGenerator(Convert.ToInt32(Console.ReadLine())).GetAwaiter().GetResult();
            collection.InsertManyAsync(usersToAdd).GetAwaiter().GetResult();
        }

        private static List<string> PreLoadNames()
        {
            List<string> names = new List<string>();
            using (StreamReader streamReader = new StreamReader("Names.txt"))
            {
                string x = "";
                while ((x = streamReader.ReadLine()) != null)
                {
                    names.Add(x);
                }
            }
            return names;
        }
        private static async Task<List<User>> DatabaseGenerator(int amount)
        {
            List<string> preLoadNames = PreLoadNames();
            HashSet<string> callnamesHashSet = new HashSet<string>();
            List<User> users = new List<User>();
            Random random = new Random();
            for (int x = 0; x < amount; x++)
            {
                string firstName = preLoadNames[random.Next(0, preLoadNames.Count())];
                User user = new User();
                string CallName = "@" + firstName;
                user.Contacts = new List<Contact>();
                CheckAvailability(CallName);
                void CheckAvailability(string callname)
                {
                    bool successfull = callnamesHashSet.Add(callname);
                    if (!successfull)
                    {
                        callname = firstName + DateTime.Now.Millisecond * random.Next(0, 10);
                        CheckAvailability(callname);
                    }
                    else
                    {
                        CallName = callname;
                    }
                    
                }

                user.CallName = CallName;
                user.Contact = new Contact()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = firstName,
                    CallName = CallName
                };
                user.FireBaseToken = new FireBaseToken();
                user.Password = random.GetRandomHexadecimal(36).CreateHash(true, user.CallName);
                users.Add(user);
            }
            User masteruser = new User()
            {
                CallName = "@Master",
                Password = "12345".CreateHash(true, "@Master"),
                Contact = new Contact()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Fedir " + "Katushonok"
                },
                Contacts = new List<Contact>(),
                FireBaseToken = new FireBaseToken(),
            };
            users.Add(masteruser);
            return users;
        }
        
    }

   
}
