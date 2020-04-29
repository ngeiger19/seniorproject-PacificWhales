using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Harmony;
using Harmony.Controllers;
using Harmony.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HarmonyTest
{

    [TestClass]
    public class UnitTest1
    {

        public string statesJSON = "[{\"name\": \"Alabama\",\"abbreviation\": \"AL\"},{\"name\": \"Alaska\",\"abbreviation\": \"AK\"},{\"name\": \"American Samoa\",\"abbreviation\": \"AS\"},{\"name\": \"Arizona\",\"abbreviation\": \"AZ\"}]";

        [TestMethod]
        public void ParseJsonStringOfStatesToSelectList_Success_CountEqualTo4()
        {
            AccountController Controller = new AccountController();
            List<SelectListItem> stateList = Controller.ParseJsonStringOfStatesToSelectList(statesJSON);
            int expectedResult = 4;
            Assert.AreEqual(expectedResult,  stateList.Count());
        }

        [TestMethod]
        public bool GenerateVenueList_IsEmpty_ReturnsTrue()
        {
            UsersController Controller = new UsersController();
            
            return true;
        }
<<<<<<< HEAD
        [TestMethod]
        public void Total_Add10to10ShouldEqual20()
        {
            int x = 10;
            int y = x + 10;
            Assert.AreEqual(x + 10, y);
        }


=======

        [TestMethod]
        public void CityQuery_EmptyList_ReturnsEmptyList()
        {
            HomeController controller = new HomeController();
            IEnumerable<User> users = Enumerable.Empty<User>();
            
            IEnumerable<User> result = controller.CityQuery(users, "Salem");
            Assert.AreEqual(users.Count(), result.Count());
        }
>>>>>>> 8a832d019db39fa7ce981cdbc4a91825a8404ae3
    }
}
