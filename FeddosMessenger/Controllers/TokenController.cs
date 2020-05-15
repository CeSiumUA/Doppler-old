using FeddosMessenger.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using SharedTypes.Cryptography;
using SharedTypes.SocialTypes;
using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MessagePack;

namespace FeddosMessenger.Controllers
{
    public class TokenController:ControllerBase
    {
        //TODO
        //private DataBaseContext _dataBaseContext;
        public TokenController()
        {

        }

        [HttpPost("/auth")]
        public async Task<IActionResult> Token(string username, string password, string firebasetoken)
        {
            (ClaimsIdentity ClaimsIdentity, Contact Contact) authenticatedUser = await Checkidentity(username, password, firebasetoken);
            if(authenticatedUser.ClaimsIdentity == null)
            {
                return BadRequest("Invalid username or password!");
            }
            DateTime dateTime = DateTime.Now;

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(AuthenticationToken.Issuer, 
                AuthenticationToken.Audience, 
                authenticatedUser.ClaimsIdentity.Claims, 
                dateTime, 
                dateTime.AddMinutes(AuthenticationToken.TTL), 
                new SigningCredentials(AuthenticationToken.GetEncryptionKey(Properties.Resources.SecurityKey), 
                SecurityAlgorithms.HmacSha256));
            string encodedToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            //(string Token, string CallName, Contact Contact) payload = (encodedToken, authenticatedUser.ClaimsIdentity.Name, authenticatedUser.Contact);
            PayLoad payLoad = new PayLoad()
            {
                Token = encodedToken,
                CallName = authenticatedUser.ClaimsIdentity.Name,
                Contact = authenticatedUser.Contact
            };
            return new JsonResult(payLoad);
        }
        //TODO Update Firebase Token
        private async Task<(ClaimsIdentity, Contact)> Checkidentity(string userName, string Password, string FireBaseToken)
        {
            Console.WriteLine("Authentication: Searching for a user...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            User usr = null;
            usr = MongoDbContext.UsersCollection.AsQueryable().Where(x => x.CallName == userName).FirstOrDefault();
            stopwatch.Stop();
            Console.WriteLine("Search performed in: " + stopwatch.ElapsedMilliseconds + " ms");
            if(usr != null)
            {
                if (usr.Password.CompareToNormalPassword(Password))
                {
                    List<Claim> claims = new List<Claim>()
                    {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, usr.CallName)
                    };
                    if (FireBaseToken != usr.FireBaseToken.Token)
                    {
                        Stopwatch stopwatch1 = new Stopwatch();
                        stopwatch1.Restart();
                        var filter = Builders<User>.Filter.Eq(x => x.Id, usr.Id);
                        var update = Builders<User>.Update.Set(x => x.FireBaseToken.Token, FireBaseToken);
                        await MongoDbContext.UsersCollection.UpdateOneAsync(filter, update);
                        stopwatch1.Stop();
                        Console.WriteLine("FireBase token update performed, operation took: " + stopwatch1.ElapsedMilliseconds + " ms, for User: " + usr.CallName);
                    }
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    return (claimsIdentity, usr.Contact);
                }
            }
            return (null, null);

        }
    }
}
