using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlizzardProgrammingTest.Backend.Models;
using System.Collections.Generic;
using BlizzardProgrammingTest.Backend;

namespace BlizzardProgrammingTest.Test
{
    [TestClass]
    public class DeleteCharacterTests
    {
        [TestMethod]
        public void TestDeleteCharacterIdNoExist()
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

            DBObject.DeleteCharacter(dkToon.Id + 1);

            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(dkToon.Owner);

            Assert.AreEqual(characterList.Count, 1);
        }

        [TestMethod]
        public void TestDeleteCharacterIdExist()
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

            DBObject.DeleteCharacter(dkToon.Id);

            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(dkToon.Owner);

            Assert.AreEqual(characterList.Count, 0);
        }
    }
}
