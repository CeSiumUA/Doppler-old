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
            ClaimsIdentity authenticatedUser = await Checkidentity(username, password, firebasetoken);
            if(authenticatedUser == null)
            {
                return BadRequest("Invalid username or password!");
            }
            DateTime dateTime = DateTime.Now;

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(AuthenticationToken.Issuer, 
                AuthenticationToken.Audience, 
                authenticatedUser.Claims, 
                dateTime, 
                dateTime.AddMinutes(AuthenticationToken.TTL), 
                new SigningCredentials(AuthenticationToken.GetEncryptionKey(Properties.Resources.SecurityKey), 
                SecurityAlgorithms.HmacSha256));
            string encodedToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            (string Token, string CallName) payload = (encodedToken, authenticatedUser.Name);
            return new JsonResult(payload);
        }
        //TODO Update Firebase Token
        private async Task<ClaimsIdentity> Checkidentity(string userName, string Password, string FireBaseToken)
        {
            Console.WriteLine("Authentication: Searching for a user...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            User usr = null;
            usr = MongoDbContext.UsersCollection.AsQueryable().Where(x => x.CallName == userName).FirstOrDefault();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            if(usr != null)
            {
                if (usr.Password.CompareToNormalPassword(Password))
                {
                    List<Claim> claims = new List<Claim>()
                    {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, usr.CallName)
                    };
                    usr.FireBaseToken.Token = FireBaseToken;
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }
            return null;

        }
    }
}
