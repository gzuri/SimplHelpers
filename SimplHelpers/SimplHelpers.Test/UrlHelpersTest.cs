using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimplHelpers.Test
{
    [TestClass]
    public class UrlHelpersTest
    {
        [TestMethod]
        public void GetQueryParamsFromUri_AbsoluteUrl1()
        {
            var queryParams = UrlHelper.GetQueryParamsFromUri("?name=goran&nickname=dex");
            Assert.AreEqual(queryParams.Count, 2);
        }

        [TestMethod]
        public void GetQueryParamsFromUri_AbsoluteUrl2()
        {
            var queryParams = UrlHelper.GetQueryParamsFromUri("?");
            Assert.AreEqual(queryParams.Count, 0);
        }

        [TestMethod]
        public void GetQueryParamsFromUri_AbsoluteUrl3()
        {
            var queryParams = UrlHelper.GetQueryParamsFromUri("?name&nickname=dex");

            Assert.AreEqual(queryParams.Count, 2);
        }
    }
}
