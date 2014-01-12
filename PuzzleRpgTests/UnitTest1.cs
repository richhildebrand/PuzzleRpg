using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace PuzzleRpgTests
{
    [TestClass]
    public class UnitTest1
   { 
        [TestMethod]
        public void TestMethod1()
        {
            var expected = true;
            var actual = true;
            Assert.AreEqual(expected, actual);
        }
    }
}
