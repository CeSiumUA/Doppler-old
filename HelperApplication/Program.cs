using System;
using Microsoft.EntityFrameworkCore.SqlServer;
using FeddosMessenger;
using SharedTypes.SocialTypes;
using SharedTypes.Tokens;

namespace HelperApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using(FeddosMessenger.Database.DataBaseContext dbc = new FeddosMessenger.Database.DataBaseContext())
            {
                InsertToDb();
                async void InsertToDb()
                {
                    Console.WriteLine("Insert new? y/n");
                    string dcs = Console.ReadLine().ToUpper();
                    if(dcs == "Y")
                    {
                        Console.WriteLine("Enter CallName:");
                        User user = new User();
                        user.CallName = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        user.Password = Console.ReadLine();
                        user.Contact = new Contact();
                        Console.WriteLine("Enter FirstName:");
                        user.Contact.FirstName = Console.ReadLine();
                        Console.WriteLine("Enter SecondName:");
                        user.Contact.SecondName = Console.ReadLine();
                        user.FireBaseToken = new FireBaseToken();
                        await dbc.Users.AddAsync(user);
                        Console.WriteLine("----");
                        Console.WriteLine("Wrote to Db!");
                        Console.WriteLine("----");
                        InsertToDb();
                    }
                    if(dcs == "N")
                    {
                        SaveChanges();
                    }
                }
                void SaveChanges()
                {
                    Console.WriteLine(dbc.SaveChanges());
                }
            }
        }
    }
}
