using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplHelpers;
using System.Collections.Generic;
namespace SimplHelpers.Test
{
    [TestClass]
    public class CollectionsHelpersTest
    {
        [TestMethod]
        public void SerializeToDictionary()
        {
            var enumDictionary = CollectionsHelpers.EnumToDictionary(CurrencyEnum.EUR);
            Assert.AreEqual(enumDictionary.Count, 2, "All enum values didn't serialize");
            string eur;
            Assert.IsTrue(enumDictionary.TryGetValue(978, out eur), "Values didn't serialize correctly");
        }

        public enum CurrencyEnum 
        { 
            EUR = 978,
            USD = 840,
            USaD = 840
        }
    }
}
