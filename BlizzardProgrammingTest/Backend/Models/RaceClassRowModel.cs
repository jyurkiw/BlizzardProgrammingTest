using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlizzardProgrammingTest.Backend;

namespace BlizzardProgrammingTest.Backend.Models
{
    public class RaceClassRowModel : IModel
    {
        public int Id { get; set; }
        public string Faction { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }

        public RaceClassRowModel() { }
        public RaceClassRowModel(string[] row)
        {
            ModelInit(row);
        }

        public void ModelInit(string[] row)
        {
            if (row.Length == BackendConstants.QueryProperties.RaceClassRowLength)
            {
                Id = int.Parse(row[BackendConstants.RaceClassQueryIndicies.Id]);
                Faction = row[BackendConstants.RaceClassQueryIndicies.Faction];
                Race = row[BackendConstants.RaceClassQueryIndicies.Race];
                Class = row[BackendConstants.RaceClassQueryIndicies.Class];
            }
        }

        // Disabled because more data contraction is necessary
        public IDictionary<string, string> GetContractDict()
        {
            throw new NotImplementedException("This method is invalid for this class.");
        }
    }
}