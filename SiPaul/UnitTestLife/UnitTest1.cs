using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiPaul;

namespace UnitTestLife
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var life = new LifeManager(1, 1);
            bool[,] cell = life.Step();
            Assert.AreEqual(false, cell[0, 0]);
        }

        [TestMethod]
        public void TestStayAlive()
        {
             Assert.AreEqual(true, LifeManager.CheckAlive(true, 3));
        }

        [TestMethod]
        public void TestDies()
        {
            Assert.AreEqual(false, LifeManager.CheckAlive(true, 4));
        }

        [TestMethod]
        public void TestBlock()
        {
            var life = new LifeManager(2, 2);
            bool[,] cell = life.Step();
            cell[0, 0] = true;
            cell[0, 1] = true;
            cell[1, 0] = true;
            cell[1, 1] = true;
            bool[,] cellafterstep = life.Step();
            Assert.AreEqual(true, cellafterstep[1, 1]);
        }

        [TestMethod]
        public void TestLine()
        {
            var life = new LifeManager(3, 3);
            bool[,] cell = life.Step();
            cell[0, 1] = true;
            cell[1, 1] = true;
            cell[2, 1] = true;
            bool[,] cellafterstep = life.Step();
            Assert.AreEqual(true, cellafterstep[1, 0]);
            Assert.AreEqual(true, cellafterstep[1, 1]);
            Assert.AreEqual(true, cellafterstep[1, 2]);
            bool[,] cellafterstep2 = life.Step();
            Assert.AreEqual(true, cellafterstep2[0, 1]);
            Assert.AreEqual(true, cellafterstep2[1, 1]);
            Assert.AreEqual(true, cellafterstep2[2, 1]);
        }
    }
}
