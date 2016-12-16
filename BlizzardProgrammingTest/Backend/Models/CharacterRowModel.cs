using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlizzardProgrammingTest.Backend;

namespace BlizzardProgrammingTest.Backend.Models
{
    /// <summary>
    /// Model of a character row from a character DB query.
    /// Contains all relevant character selection data.
    /// One character per row.
    /// </summary>
    public class CharacterRowModel : IModel
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public string Faction { get; set; }
        public bool Deleted { get; set; }

        public CharacterRowModel()
        {
            Deleted = false;
        }
        public CharacterRowModel(string[] row)
        {
            ModelInit(row);
        }

        /// <summary>
        /// Initialize the row object from the passed string array.
        /// String array should come from the DB. In a real environment
        /// would be a DataRow object or, more likely, the method would be
        /// replaced entirely with EF.
        /// </summary>
        /// <param name="row">The data to initialize the model with.</param>
        public void ModelInit(string[] row)
        {
            if(row.Length == BackendConstants.QueryProperties.CharacterRowLength)
            {
                Id = int.Parse(row[BackendConstants.CharacterQueryIndicies.Id]);
                Owner = row[BackendConstants.CharacterQueryIndicies.Owner];
                Name = row[BackendConstants.CharacterQueryIndicies.Name];
                Level = int.Parse(row[BackendConstants.CharacterQueryIndicies.Level]);
                Race = row[BackendConstants.CharacterQueryIndicies.Race];
                Class = row[BackendConstants.CharacterQueryIndicies.Class];
                Faction = row[BackendConstants.CharacterQueryIndicies.Faction];
                Deleted = bool.Parse(row[BackendConstants.CharacterQueryIndicies.Deleted]);
            }
        }
        
        /// <summary>
        /// Translate the model into a dictionary object that satisfies the
        /// front-end data contract.
        /// </summary>
        /// <returns>A dictionary with the model's data.</returns>
        public IDictionary<string, string> GetContractDict()
        {
            Dictionary<string, string> contract = new Dictionary<string, string>();
            contract.Add(BackendConstants.CharacterFieldNames.Id, Id.ToString());
            contract.Add(BackendConstants.CharacterFieldNames.Name, Name);
            contract.Add(BackendConstants.CharacterFieldNames.Level, Level.ToString());
            contract.Add(BackendConstants.CharacterFieldNames.Race, Race);
            contract.Add(BackendConstants.CharacterFieldNames.Class, Class);
            contract.Add(BackendConstants.CharacterFieldNames.Faction, Faction);
            contract.Add(BackendConstants.CharacterFieldNames.Deleted, Deleted.ToString());

            return contract;
        }

        /// <summary>
        /// Convert the model into a string.
        /// This method is used to save the data to file whenever a character is added or deleted.
        /// The output string is a valid JSON array in DB order.
        /// </summary>
        /// <returns>The model in a string.</returns>
        public override string ToString()
        {
            return string.Format("    [ \"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\" ]", Id.ToString(), Owner, Name, Level.ToString(), Race, Class, Faction, Deleted);
        }
    }
}