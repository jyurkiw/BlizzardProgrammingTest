using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlizzardProgrammingTest.Backend;

namespace BlizzardProgrammingTest.Backend.Models
{
    public class CharacterRowModel : IModel
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public string Faction { get; set; }

        public CharacterRowModel() { }
        public CharacterRowModel(string[] row)
        {
            ModelInit(row);
        }

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
            }
        }
        
        public IDictionary<string, string> GetContractDict()
        {
            Dictionary<string, string> contract = new Dictionary<string, string>();
            contract.Add(BackendConstants.CharacterFieldNames.Id, Id.ToString());
            contract.Add(BackendConstants.CharacterFieldNames.Name, Name);
            contract.Add(BackendConstants.CharacterFieldNames.Level, Level.ToString());
            contract.Add(BackendConstants.CharacterFieldNames.Race, Race);
            contract.Add(BackendConstants.CharacterFieldNames.Class, Class);
            contract.Add(BackendConstants.CharacterFieldNames.Faction, Faction);

            return contract;
        }

        public override string ToString()
        {
            return string.Format("    [ \"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\" ]", Id.ToString(), Owner, Name, Level.ToString(), Race, Class, Faction);
        }
    }
}