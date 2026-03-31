using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TK;


namespace UnitTestProject1
{
    [TestClass]
    public class RailwayMathTests
    {
        [TestMethod]
        public void Calculate_Kupe_100km_2tickets()
        {
            decimal result = RailwayMath.CalculateTotalPrice(100, 2, 1.1m);
            Assert.AreEqual(1760m, result);
        }

        [TestMethod]
        public void Calculate_Platzkart_100km_2tickets()
        {
            decimal result = RailwayMath.CalculateTotalPrice(100, 2, 1.0m);
            Assert.AreEqual(1600m, result);
        }

        [TestMethod]
        public void Calculate_Lux_200km_1ticket()
        {
            decimal result = RailwayMath.CalculateTotalPrice(200, 1, 1.3m);
            Assert.AreEqual(2080m, result);
        }

        [TestMethod]
        public void Calculate_ZeroDistance_ShouldThrow()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                RailwayMath.CalculateTotalPrice(0, 1, 1.0m));
        }

        [TestMethod]
        public void Calculate_ZeroTickets_ShouldThrow()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                RailwayMath.CalculateTotalPrice(100, 0, 1.0m));
        }
    }
}