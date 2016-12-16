using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using BlizzardProgrammingTest.Backend.Models;
using System.Threading.Tasks;

namespace BlizzardProgrammingTest.Backend
{
    /// <summary>
    /// Database interaction layer. In this case, the "DB" is just some
    /// JSON files on the disk.
    /// </summary>
    public class DBObject : IDisposable
    {
        private static DBObject _instance = null;

        private string raceClassQueryPath;
        private string characterQueryPath;

        private bool isDisposed = false;

        /// <summary>
        /// Singleton implementation because file-access is expensive and I only want to do it once.
        /// Static variables like singletons in a web application like ASP.NET live at the server-application
        /// level rather than the connection level. So, one singleton instance will service every single
        /// connection.
        /// </summary>
        public static DBObject Instance {
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

        private static object fileWriteKey = new object();

        /// <summary>
        /// On construction, load data from the "DB" (files).
        /// Data is limited so in this case we will just cache the data.
        /// Caching will happen in a singleton because we don't need to
        /// get complicated, and the dataset is extremely small.
        /// </summary>
        private DBObject()
        {
            lock (fileWriteKey)
            {
                raceClassTable = new List<RaceClassRowModel>();
                characterTable = new List<CharacterRowModel>();

                raceClassQueryPath = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, BackendConstants.QueryProperties.RaceClassQueryPath);
                characterQueryPath = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, BackendConstants.QueryProperties.CharacterPath);

                // Load table data from files.
                // Load Race/Class data
                List<string[]> raceClassDataSet = JsonConvert.DeserializeObject<List<string[]>>(System.IO.File.ReadAllText(raceClassQueryPath));
                foreach (string[] row in raceClassDataSet)
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
        }

        /// <summary>
        /// On construction, load data from the passed lists.
        /// This constructor has been included for testing purposes.
        /// Normal program flow should utalize the data files.
        /// For release code, this constructor should be in an #if DEBUG
        /// block so that it will not be compiled in.
        /// </summary>
        /// <param name="raceData">Test race data.</param>
        /// <param name="characterData">Test character data.</param>
        public DBObject(List<RaceClassRowModel> raceData, List<CharacterRowModel> characterData)
        {
            raceClassTable = raceData;
            characterTable = characterData;
            _instance = this;
        }

        /// <summary>
        /// Dispose the DBObject.
        /// Write character list data to file.
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                DBObject.Instance.SaveCharacterDataToFile();
                isDisposed = true;
            }
        }

        ~DBObject()
        {
            Dispose();
        }

        /// <summary>
        /// Write the current character data to file.
        /// This overwrites the current data and doesn't use any kind of
        /// backing-up mechanism due to time constraints, otherwise
        /// we would use some kind of versioning and curation method
        /// to miminize potential data loss.
        /// Of course, if this were a real application, we'd be using a database
        /// anyway.
        /// </summary>
        private Task SaveCharacterDataToFile()
        {
            return Task.Run(() =>
            {
                lock (fileWriteKey)
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
            });
        }

        // ORM Methods
        /// <summary>
        /// Get the user's characters. Requires a valid username.
        /// </summary>
        /// <param name="username">A username.</param>
        /// <returns>The user's character list.</returns>
        public static List<IDictionary<string, string>> GetCharacterList(string username)
        {
            List<IDictionary<string, string>> characterList = (
                from character in DBObject.Instance.characterTable
                where character.Owner == username
                where !character.Deleted
                select character.GetContractDict()
            ).ToList();
                
            return characterList;
        }

        /// <summary>
        /// Get the user's deleted characters. Requires a valid username.
        /// </summary>
        /// <param name="username">A username.</param>
        /// <returns>The user's deleted character list.</returns>
        public static List<IDictionary<string, string>> GetDeletedCharacterList(string username)
        {
            List<IDictionary<string, string>> characterList = (
                from character in DBObject.Instance.characterTable
                where character.Owner == username
                where character.Deleted
                select character.GetContractDict()
            ).ToList();

            return characterList;
        }

        /// <summary>
        /// Get application race/class data.
        /// Race/class data is loaded from an external data source for simplification of
        /// curation and modification.
        /// Race/class data is stored in row-form. This method converts it to the contracted
        /// hierarchical form in O(n) time. 
        /// The username is used to filter restricted class access (Death Knights, etc) from
        /// the dataset.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>Contracted race/class data.</returns>
        public static Dictionary<string, Dictionary<string, List<string>>> GetRaceClass(string username)
        {
            Dictionary<string, Dictionary<string, List<string>>> contract = new Dictionary<string, Dictionary<string, List<string>>>();

            List<RaceClassRowModel> rows = DBObject.Instance.raceClassTable;
            if (!GetUserPermForDeathknights(username))
            {
                rows = rows.Where(r => r.Class != BackendConstants.QueryProperties.DeathKnightClassName).ToList();
            }

            // Create the hierarctical dataset in a single pass over the data.
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

        /// <summary>
        /// Check if the passed user is qualified to create a deathknight character.
        /// The requirement data is stored as a constant, but really should be in the database.
        /// However, it's staying a constant due to time constraints.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <returns>True if the user can make a deathknight.</returns>
        public static bool GetUserPermForDeathknights(string username)
        {
            return (from row in DBObject.Instance.characterTable where row.Level >= BackendConstants.QueryProperties.DeathKnightMinLevelReq where !row.Deleted select true).Count() > 0;
        }

        /// <summary>
        /// Add a new character to the character data set.
        /// </summary>
        /// <param name="character">The character data.</param>
        public static bool AddNewCharacter(CharacterRowModel character, string username)
        {
            // Check username against owner
            if (character.Owner.CompareTo(username) != 0)
            {
                return false;
            }

            // Check for invalid faction, class, or race selection.
            if (!ValidateCharacter(character))
            {
                return false;
            }

            // No death knights if you don't have a toon at 55 yet.
            if (character.Class.CompareTo(BackendConstants.QueryProperties.DeathKnightClassName) == 0 && !GetUserPermForDeathknights(character.Owner))
            {
                return false;
            } // Check for valid deathknight toons and set level as apporpriate (DKs start at level 55).
            else if (character.Class.CompareTo(BackendConstants.QueryProperties.DeathKnightClassName) == 0)
            {
                character.Level = BackendConstants.QueryProperties.DeathKnightMinLevelReq;
            }
            
            int newId = 1;

            if (DBObject.Instance.characterTable.Count > 0)
            {
                newId = DBObject.Instance.characterTable.Select(c => c.Id).Max() + 1;
            }

            character.Id = newId;

            DBObject.Instance.characterTable.Add(character);

            DBObject.Instance.SaveCharacterDataToFile();

            return true;
        }

        /// <summary>
        /// Validate the passed character's faction + race + class combination against
        /// the current race class query dataset.
        /// </summary>
        /// <param name="character">The character to validate.</param>
        /// <returns>True if the combination is valid.</returns>
        private static bool ValidateCharacter(CharacterRowModel character)
        {
            return DBObject.Instance.raceClassTable.Where(r => r.Faction == character.Faction && r.Race == character.Race && r.Class == character.Class).Count() >= 1;
        }

        public static bool ApplyLevelToken(int id, string username)
        {
            CharacterRowModel character = DBObject.Instance.characterTable.Where(c => c.Id == id).FirstOrDefault();

            if (character != null && character.Owner.CompareTo(username) == 0)
            {
                int characterIndex = DBObject.Instance.characterTable.IndexOf(character);
                DBObject.Instance.characterTable[characterIndex].Level = BackendConstants.QueryProperties.LevelTokenLevel;
                DBObject.Instance.SaveCharacterDataToFile();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a character by ID.
        /// TODO: Pass in a username and check for character ownership.
        /// Username should be retrieved from the controller. Not the front-end.
        /// </summary>
        /// <param name="id">The character ID to delete.</param>
        public static bool DeleteCharacter(int id, string username)
        {
            CharacterRowModel character = DBObject.Instance.characterTable.Where(c => c.Id == id).FirstOrDefault();

            if (character != null && character.Owner.CompareTo(username) == 0)
            {
                int characterIndex = DBObject.Instance.characterTable.IndexOf(character);
                DBObject.Instance.characterTable[characterIndex].Deleted = true;
                DBObject.Instance.SaveCharacterDataToFile();

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Undelete a character by ID.
        /// Username should be retrieved from the controller. Not the front-end.
        /// </summary>
        /// <param name="id">The character ID to undelete.</param>
        public static bool UndeleteCharacter(int id, string username)
        {
            CharacterRowModel character = DBObject.Instance.characterTable.Where(c => c.Id == id).FirstOrDefault();

            if (character != null && character.Owner.CompareTo(username) == 0)
            {
                int characterIndex = DBObject.Instance.characterTable.IndexOf(character);
                DBObject.Instance.characterTable[characterIndex].Deleted = false;
                DBObject.Instance.SaveCharacterDataToFile();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}