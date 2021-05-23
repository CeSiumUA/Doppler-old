using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppler.REST.Models.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Doppler.Tests.Cryptography
{
    [TestClass]
    public class CryptographyHashTests
    {
        [TestMethod]
        [DataRow("12345Aa-")]
        public void PasswordCorrectHashCheck_Test(string password)
        {
            var pw = HashProvider.GenerateHash(password);
            var passwordMatch = HashProvider.CompareHash(password, pw);
            Assert.IsTrue(passwordMatch);
        }
        [TestMethod]
        [DataRow("12345Aa-", "12345Aa")]
        public void PasswordInCorrectHashCheck_Test(string originPassword, string inputPassword)
        {
            var pw = HashProvider.GenerateHash(originPassword);
            var passwordMatch = HashProvider.CompareHash(inputPassword, pw);
            Assert.IsFalse(passwordMatch);
        }
    }
}
