using FeddosMessenger.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedTypes.SocialTypes;
using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FeddosMessenger.Controllers
{
    public class TokenController:ControllerBase
    {
        public TokenController(DataBaseContext dataBaseContext)
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

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(AuthenticationToken.Issuer, AuthenticationToken.Audience, authenticatedUser.Claims, dateTime, dateTime.AddMinutes(AuthenticationToken.TTL), new SigningCredentials(AuthenticationToken.GetEncryptionKey(), SecurityAlgorithms.HmacSha256));
            string encodedToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            (string Token, string CallName) payload = (encodedToken, authenticatedUser.Name);
            return new JsonResult(payload);
        }
        //TODO
        private async Task<ClaimsIdentity> Checkidentity(string userName, string Password, string FireBaseToken)
        {
            User usr = null;
            using(DataBaseContext dbc = new DataBaseContext())
            {
                usr = await dbc.Users.Where(x => x.CallName == userName && x.Password == Password).FirstOrDefaultAsync();
                if(usr != null)
                {
                    usr.FireBaseToken = new FireBaseToken() { Token = FireBaseToken };
                }
                await dbc.SaveChangesAsync();
            }
            if(usr != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, usr.CallName)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;

        }
    }
}
