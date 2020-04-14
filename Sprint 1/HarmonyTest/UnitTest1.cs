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
    }
}
