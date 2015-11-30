using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimplHelpers.Test
{
    [TestClass]
    public class IpHelpersTest
    {
        [TestMethod]
        public void CalculateIpNumericValue_HappyPath()
        {
            var stringAddress = "88.217.190.78";
            var ipAddress  = IPAddress.Parse(stringAddress);

            var numericAddress = IpHelpers.CalculateNumericValue(ipAddress);

            Assert.AreEqual(numericAddress, 1490665038);
        }

        [TestMethod]
        public void CalculateIpNumericValue_ExceedsMaxIntValue()
        {
            var stringAddress = "176.4.0.0";
            var ipAddress = IPAddress.Parse(stringAddress);

            var numericAddress = IpHelpers.CalculateNumericValue(ipAddress);

            Assert.AreEqual(numericAddress, 2953052160);
        }

    }
}
