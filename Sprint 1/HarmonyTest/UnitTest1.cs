using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Harmony;
using Harmony.Controllers;

namespace HarmonyTest
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public bool GenerateVenueList_IsEmpty_ReturnsTrue()
        {
            UsersController Controller = new UsersController();

            return true;
        }
        [TestMethod]
        public void Total_Add10to10ShouldEqual20()
        {
            int x = 10;
            int y = x + 10;
            Assert.AreEqual(x + 10, y);
        }


    }
}
