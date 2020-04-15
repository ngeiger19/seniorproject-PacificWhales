using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Harmony;
using Harmony.Controllers;
using Harmony.Models;
using System.Collections.Generic;
using System.Linq;

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
        public void CityQuery_EmptyList_ReturnsEmptyList()
        {
            HomeController controller = new HomeController();
            IEnumerable<User> users = Enumerable.Empty<User>();
            
            IEnumerable<User> result = controller.CityQuery(users, "Salem");
            Assert.AreEqual(users.Count(), result.Count());
        }
    }
}
