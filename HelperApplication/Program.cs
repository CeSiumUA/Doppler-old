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
using System.Drawing;

namespace HelperApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateDB();
        }
        private static void GenerateDB()
        {
            Console.WriteLine("Enter users amount:");
            List<User> usersToAdd = DatabaseGenerator(Convert.ToInt32(Console.ReadLine())).GetAwaiter().GetResult();
            using (DBCNT dBCNT = new DBCNT())
            {
                dBCNT.Users.AddRange(usersToAdd);
                dBCNT.SaveChanges();
            }
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
        private static async void UpdateImages()
        {
            Random random = new Random();
            Image[] images = Directory.GetFiles("png", "*.png", SearchOption.AllDirectories).Select(x => { return Image.FromFile(x); }).ToArray();
            using (DBCNT dBCNT = new DBCNT())
            {
                List<User> users = await dBCNT.Users.ToListAsync();
                for(int x = 0; x < users.Count; x++)
                {
                    User usr = users[x];
                    usr.Contact.Icon = new SharedTypes.Media.EntityIcon()
                    {
                        Icon = images[random.Next(0, images.Length - 1)].ConvertImageToByteArray()
                    };
                    users[x] = usr;
                }
                dBCNT.UpdateRange(users);
                await dBCNT.SaveChangesAsync();
            }
        }
        private static async Task<List<User>> DatabaseGenerator(int amount)
        {
            List<string> preLoadNames = PreLoadNames();
            HashSet<string> callnamesHashSet = new HashSet<string>();
            List<User> users = new List<User>();
            Random random = new Random();
            Image[] images = Directory.GetFiles("png", "*.png", SearchOption.AllDirectories).Select(x => { return Image.FromFile(x); }).ToArray();
            for (int x = 0; x < amount; x++)
            {
                string firstName = preLoadNames[random.Next(0, preLoadNames.Count())];
                User user = new User();
                string CallName = "@" + firstName;
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
                    Id = Guid.NewGuid(),
                    Name = firstName,
                    CallName = CallName
                };
                user.Contact.Icon = new SharedTypes.Media.EntityIcon()
                {
                    Icon = images[random.Next(0, images.Length - 1)].ConvertImageToByteArray()
                };
                user.FireBaseToken = new FireBaseToken();
                user.Password = random.GetRandomHexadecimal(36).CreateHash(true, user.CallName);
                users.Add(user);
            }
            string clnm = "@Master";
            User masteruser = new User()
            {
                
                CallName = clnm,
                Password = "12345".CreateHash(true, "@Master"),
                Contact = new Contact()
                {
                    Id = Guid.NewGuid(),
                    Name = "Fedir " + "Katushonok",
                    CallName = clnm,
                    Icon = new SharedTypes.Media.EntityIcon()
                    {
                        Icon = images[random.Next(0, images.Length - 1)].ConvertImageToByteArray()
                    }
                },
                FireBaseToken = new FireBaseToken(),
            };
            users.Add(masteruser);
            return users;
        }
        
    }

    
}
