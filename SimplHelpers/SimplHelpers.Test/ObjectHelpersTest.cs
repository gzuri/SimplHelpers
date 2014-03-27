using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SimplHelpers.Test
{
    [TestClass]
    public class ObjectHelpersTest
    {
        [TestMethod]
        public void TestObjectCopy()
        {
            var source = new TestClass1
            {
                Name = "Foo",
                SomeID = 1,
                LongVariable = 12353242,
                IgnoreVariable = 1,
                ListString = new List<string> { "Foo", "Bla" }
            };

            var destination = new TestClass1{IgnoreVariable = 2};


            destination = ObjectHelpers.CopyObject(source, destination, true, "IgnoreVariable");

            Assert.IsNotNull(destination);
            Assert.AreEqual(destination.IgnoreVariable, 2);
        }


        public class TestClass1 
        {
            public string Name { get; set; }
            public int SomeID { get; set; }
            public long LongVariable { get; set; }
            public int IgnoreVariable { get; set; }
            public List<string> ListString { get; set; }
        }
    }
}
