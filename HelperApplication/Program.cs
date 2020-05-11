using System;
using Microsoft.EntityFrameworkCore.SqlServer;
using FeddosMessenger;
using SharedTypes.SocialTypes;
using SharedTypes.Tokens;
using Microsoft.EntityFrameworkCore;
using FeddosMessenger.Database;

namespace HelperApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
           
            using (privateDbHandler dbc = new privateDbHandler())
            {
                InsertToDb();
                async void InsertToDb()
                {
                    Console.WriteLine("Insert new? y/n");
                    string dcs = Console.ReadLine().ToUpper();
                    if (dcs == "Y")
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
                    if (dcs == "N")
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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                
            }

            Console.WriteLine("Type something to exit...");
            Console.ReadLine();
        }
    }

    public class privateDbHandler:DbContext
    {
        public DbSet<User> Users { get; set; }
        public privateDbHandler()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(FeddosMessenger.Properties.Resources.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
