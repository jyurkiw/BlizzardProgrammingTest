using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlizzardProgrammingTest.Backend.Models;
using BlizzardProgrammingTest.Backend;
using System.Collections.Generic;
using System.Linq;

namespace BlizzardProgrammingTest.Test
{
    [TestClass]
    public class AddNewCharacterTests
    {
        [TestMethod]
        public void CharIsDKNoQualify()
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

            CharacterRowModel dkToon2 = new CharacterRowModel();
            dkToon2.Class = "Death Knight";
            dkToon2.Faction = "alliance";
            dkToon2.Id = 1;
            dkToon2.Level = 1;
            dkToon2.Name = "Wally";
            dkToon2.Owner = "Masterson";
            dkToon2.Race = "Human";

            DBObject.AddNewCharacter(dkToon2);

            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(dkToon2.Owner);

            Assert.AreEqual(characterList.Count, 1);
        }

        [TestMethod]
        public void CharIsDKYesQualify()
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

            CharacterRowModel dkToon2 = new CharacterRowModel();
            dkToon2.Class = "Death Knight";
            dkToon2.Faction = "alliance";
            dkToon2.Id = 1;
            dkToon2.Level = 1;
            dkToon2.Name = "Wally";
            dkToon2.Owner = "Masterson";
            dkToon2.Race = "Human";

            DBObject.AddNewCharacter(dkToon2);

            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(dkToon2.Owner);

            Assert.AreEqual(characterList.Count, 2);
            int toon2Level = int.Parse(characterList.Where(c => c["class"].CompareTo(dkToon2.Class) == 0).Select(c => c["level"]).FirstOrDefault());
            Assert.AreEqual(toon2Level, 55);
        }

        [TestMethod]
        public void CharIsNotDK()
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

            CharacterRowModel dkToon2 = new CharacterRowModel();
            dkToon2.Class = "Mage";
            dkToon2.Faction = "alliance";
            dkToon2.Id = 1;
            dkToon2.Level = 1;
            dkToon2.Name = "Wally";
            dkToon2.Owner = "Masterson";
            dkToon2.Race = "Human";

            DBObject.AddNewCharacter(dkToon2);

            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(dkToon2.Owner);

            Assert.AreEqual(characterList.Count, 2);
            int toon2Level = int.Parse(characterList.Where(c => c["class"].CompareTo(dkToon2.Class) == 0).Select(c => c["level"]).FirstOrDefault());
            Assert.AreEqual(toon2Level, dkToon2.Level);
        }
    }
}
