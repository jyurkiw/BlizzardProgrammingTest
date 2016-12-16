using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlizzardProgrammingTest.Backend;
using BlizzardProgrammingTest.Backend.Models;
using System.Collections.Generic;

namespace BlizzardProgrammingTest.Test
{
    [TestClass]
    public class GetUserPermForDeathknightsTests
    {
        [TestMethod]
        public void TestGetUserPermForDeathknightsL1()
        {
            CharacterRowModel dkToon = new CharacterRowModel();
            dkToon.Class = "Warrior";
            dkToon.Faction = "alliance";
            dkToon.Id = 1;
            dkToon.Level = 1;
            dkToon.Name = "Wally";
            dkToon.Owner = "Masterson";
            dkToon.Race = "Human";

            List<CharacterRowModel> characterData = new List<CharacterRowModel>();
            characterData.Add(dkToon);
            DBObject dbo = new DBObject(null, characterData);

            Assert.IsFalse(DBObject.GetUserPermForDeathknights(dkToon.Owner));
        }

        [TestMethod]
        public void TestGetUserPermForDeathknightsL54()
        {
            CharacterRowModel dkToon = new CharacterRowModel();
            dkToon.Class = "Warrior";
            dkToon.Faction = "alliance";
            dkToon.Id = 1;
            dkToon.Level = 54;
            dkToon.Name = "Wally";
            dkToon.Owner = "Masterson";
            dkToon.Race = "Human";

            List<CharacterRowModel> characterData = new List<CharacterRowModel>();
            characterData.Add(dkToon);
            DBObject dbo = new DBObject(null, characterData);

            Assert.IsFalse(DBObject.GetUserPermForDeathknights(dkToon.Owner));
        }

        [TestMethod]
        public void TestGetUserPermForDeathknightsL55()
        {
            CharacterRowModel dkToon = new CharacterRowModel();
            dkToon.Class = "Warrior";
            dkToon.Faction = "alliance";
            dkToon.Id = 1;
            dkToon.Level = 55;
            dkToon.Name = "Wally";
            dkToon.Owner = "Masterson";
            dkToon.Race = "Human";

            List<CharacterRowModel> characterData = new List<CharacterRowModel>();
            characterData.Add(dkToon);
            DBObject dbo = new DBObject(null, characterData);

            Assert.IsTrue(DBObject.GetUserPermForDeathknights(dkToon.Owner));
        }

        [TestMethod]
        public void TestGetUserPermForDeathknightsL100()
        {
            CharacterRowModel dkToon = new CharacterRowModel();
            dkToon.Class = "Warrior";
            dkToon.Faction = "alliance";
            dkToon.Id = 1;
            dkToon.Level = 100;
            dkToon.Name = "Wally";
            dkToon.Owner = "Masterson";
            dkToon.Race = "Human";

            List<CharacterRowModel> characterData = new List<CharacterRowModel>();
            characterData.Add(dkToon);
            DBObject dbo = new DBObject(null, characterData);

            Assert.IsTrue(DBObject.GetUserPermForDeathknights(dkToon.Owner));
        }
    }
}
