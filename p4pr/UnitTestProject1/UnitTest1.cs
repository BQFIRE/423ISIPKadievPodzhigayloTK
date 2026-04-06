using Microsoft.VisualStudio.TestTools.UnitTesting;
using p4pr;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Page1()
        {
            var page = new Page1();

            Assert.IsNotNull(page);
        }

        [TestMethod]
        public void Test_Page2()
        {
            var page = new Page2();

            Assert.IsNotNull(page);
        }

        [TestMethod]
        public void Test_Page3()
        {
            var page = new Page3();

            Assert.IsNotNull(page);
        }
    }
}