using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Doppler.API.Social;
using Doppler.REST.Models.Authentication;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Repository;
using Doppler.REST.Models.Social;
using Doppler.REST.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Doppler.Tests
{
    [TestClass]
    public class AuthenticationServicesTest
    {
        private IRepository repository;
        private ICryptographyProvider cryptographyProvider;
        [TestInitialize]
        public void Init()
        {
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(repository => repository.GetDopplerUserWithPassword("fedir").Result)
                .Returns(new DopplerUser()
                {
                    Email = "fedir.katushonok@gmail.com",
                    Id = 5,
                    Login = "f.katushonok",
                    Password = new Password()
                    {
                        PasswordHash = "ZQUlvPg9Yg7egPewtix14rfPCdayJmAXTHG62hIn754=",
                        PasswordSalt = "7yvfk1I4B6O8brt1EL3/HA==",
                        Iterations = 100000
                    }
                });
            this.repository = repoMock.Object;
            var cryptoMock = new Mock<ICryptographyProvider>();
        }
        [TestMethod]
        [DataRow("fedir", "12345")]
        public void AuthenticationServiceLogin_Test(string login, string password)
        {
            //TODO
            //AuthenticationService authenticationService = new AuthenticationService(repository, cryptographyProvider, null);
            //var user = authenticationService.Authenticate(login, password).Result;
        }
    }
}
