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
            RaceClassRowModel humWar = new RaceClassRowModel();
            humWar.Id = 1;
            humWar.Faction = "alliance";
            humWar.Race = "Human";
            humWar.Class = "Warrior";

            RaceClassRowModel humMag = new RaceClassRowModel();
            humWar.Id = 1;
            humWar.Faction = "alliance";
            humWar.Race = "Human";
            humWar.Class = "Mage";

            RaceClassRowModel humDk = new RaceClassRowModel();
            humDk.Id = 1;
            humDk.Faction = "alliance";
            humDk.Race = "Human";
            humDk.Class = "Death Knight";

            List<RaceClassRowModel> raceClassData = new List<RaceClassRowModel>();
            raceClassData.Add(humWar);
            raceClassData.Add(humMag);
            raceClassData.Add(humDk);

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
            DBObject dbo = new DBObject(raceClassData, characterData);

            DBObject.DeleteCharacter(dkToon.Id + 1, dkToon.Owner);

            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(dkToon.Owner);

            Assert.AreEqual(characterList.Count, 1);
        }

        [TestMethod]
        public void TestDeleteCharacterIdExist()
        {
            RaceClassRowModel humWar = new RaceClassRowModel();
            humWar.Id = 1;
            humWar.Faction = "alliance";
            humWar.Race = "Human";
            humWar.Class = "Warrior";

            RaceClassRowModel humMag = new RaceClassRowModel();
            humWar.Id = 1;
            humWar.Faction = "alliance";
            humWar.Race = "Human";
            humWar.Class = "Mage";

            RaceClassRowModel humDk = new RaceClassRowModel();
            humDk.Id = 1;
            humDk.Faction = "alliance";
            humDk.Race = "Human";
            humDk.Class = "Death Knight";

            List<RaceClassRowModel> raceClassData = new List<RaceClassRowModel>();
            raceClassData.Add(humWar);
            raceClassData.Add(humMag);
            raceClassData.Add(humDk);

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
            DBObject dbo = new DBObject(raceClassData, characterData);

            DBObject.DeleteCharacter(dkToon.Id, dkToon.Owner);

            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(dkToon.Owner);

            Assert.AreEqual(characterList.Count, 0);
        }
    }
}
