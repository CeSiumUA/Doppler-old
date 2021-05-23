using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.REST.Models.Authentication;
using Doppler.REST.Models.Cryptography;

namespace Doppler.REST.Models.Social
{
    public class DopplerUser : User
    {
        [JsonIgnore]
        public Password Password { get; set; }
        [JsonIgnore]
        public JwtToken RefreshToken { get; set; }
        public string RefreshTokenToken { get; set; }
        public static DopplerUser InitializeNewUser(RegisterUserModel registerUserModel, ICryptographyProvider cryptographyProvider)
        {
            return InitializeNewUser(registerUserModel.Email, registerUserModel.Login,
                registerUserModel.PhoneNumber, registerUserModel.Name, registerUserModel.Password, cryptographyProvider);
        }
        public static DopplerUser InitializeNewUser(string email, string login, string phoneNumber, string name, string password, ICryptographyProvider cryptographyProvider)
        {
            DopplerUser dopplerUser = new DopplerUser();
            dopplerUser.Email = email;
            dopplerUser.Login = login;
            dopplerUser.PhoneNumber = phoneNumber;
            dopplerUser.Name = name;
            dopplerUser.Password = cryptographyProvider.HashPassword(password);
            return dopplerUser;
        }
    }
}
