using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using BlizzardProgrammingTest.Backend.Models;

namespace BlizzardProgrammingTest.Backend
{
    public class DBObject
    {
        private static DBObject _instance = null;

        private string raceClassQueryPath;
        private string characterQueryPath;

        private static DBObject Instance {
            get
            {
                if(_instance == null)
                {
                    _instance = new DBObject();
                }

                return _instance;
            }
        }

        public List<RaceClassRowModel> raceClassTable;
        public List<CharacterRowModel> characterTable;

        private static object key = new object();

        public DBObject()
        {
            raceClassTable = new List<RaceClassRowModel>();
            characterTable = new List<CharacterRowModel>();

            raceClassQueryPath = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "bin\\Data\\raceClassJoinQuery.json");
            characterQueryPath = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "bin\\Data\\charactersQuery.json");

            // Load table data from files.
                
            // Load Race/Class data
            List<string[]> raceClassDataSet = JsonConvert.DeserializeObject<List<string[]>>(System.IO.File.ReadAllText(raceClassQueryPath));
            foreach(string[] row in raceClassDataSet)
            {
                raceClassTable.Add(new RaceClassRowModel(row));
            }

            // Load character data
            List<string[]> characterDataSet = JsonConvert.DeserializeObject<List<string[]>>(System.IO.File.ReadAllText(characterQueryPath));
            foreach (string[] row in characterDataSet)
            {
                characterTable.Add(new CharacterRowModel(row));
            }
        }

        private void SaveDataToFile()
        {
            lock (key)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(characterQueryPath, false);
                file.WriteLine("[");

                // Save character data.
                for (int i = 0; i < DBObject.Instance.characterTable.Count; i++)
                {
                    string line = DBObject.Instance.characterTable[i].ToString();
                    if (i < DBObject.Instance.characterTable.Count - 1)
                    {
                        line += ",";
                    }

                    file.WriteLine(line);
                }

                file.WriteLine("]");

                file.Close();
            }
        }

        // ORM Methods
        public static List<IDictionary<string, string>> GetCharacterList(string username)
        {
            lock(key)
            {
                System.Diagnostics.Debug.WriteLine("getting character list");
                List<IDictionary<string, string>> characterList = (
                from character in DBObject.Instance.characterTable
                where character.Owner == username
                select character.GetContractDict()
                ).ToList();

                System.Diagnostics.Debug.WriteLine("done getting character list");
                return characterList;
            }
        }

        public static Dictionary<string, Dictionary<string, List<string>>> GetRaceClass(string username)
        {
            Dictionary<string, Dictionary<string, List<string>>> contract = new Dictionary<string, Dictionary<string, List<string>>>();


            List<RaceClassRowModel> rows = DBObject.Instance.raceClassTable;
            if (!GetUserPermForDeathknights(username))
            {
                rows = rows.Where(r => r.Class != BackendConstants.QueryProperties.DeathKnightClassName).ToList();
            }

            foreach(RaceClassRowModel row in rows)
            {
                if (!contract.ContainsKey(row.Faction))
                {
                    contract.Add(row.Faction, new Dictionary<string, List<string>>());
                }

                if (!contract[row.Faction].ContainsKey(row.Race))
                {
                    contract[row.Faction].Add(row.Race, new List<string>());
                }

                contract[row.Faction][row.Race].Add(row.Class);
            }

            return contract;
        }

        private static bool GetUserPermForDeathknights(string username)
        {
            return (from row in DBObject.Instance.characterTable where row.Level >= 55 select true).Count() > 0;
        }

        public static void AddNewCharacter(CharacterRowModel character)
        {
            lock(key)
            {
                System.Diagnostics.Debug.WriteLine("adding new character");
                int newId = 1;

                if (DBObject.Instance.characterTable.Count > 0)
                {
                    newId = DBObject.Instance.characterTable.Select(c => c.Id).Max() + 1;
                }

                character.Id = newId;

                DBObject.Instance.characterTable.Add(character);
                System.Diagnostics.Debug.WriteLine("done adding new character");

                DBObject.Instance.SaveDataToFile();
            }
        }

        public static void DeleteCharacter(int id)
        {
            lock (key)
            {
                CharacterRowModel character = DBObject.Instance.characterTable.Where(c => c.Id == id).FirstOrDefault();

                if (character != null)
                {
                    DBObject.Instance.characterTable.Remove(character);
                }

                DBObject.Instance.SaveDataToFile();
            }
        }
    }
}