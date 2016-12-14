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

        /// <summary>
        /// Initialize the row object from the passed string array.
        /// String array should come from the DB. In a real environment
        /// would be a DataRow object or, more likely, the method would be
        /// replaced entirely with EF.
        /// </summary>
        /// <param name="row">The data to initialize the model with.</param>
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

        /// <summary>
        /// Disabled because more data contraction is necessary.
        /// The total data contract for this information in the front end is an aggregation
        /// of the entire table. Not a single row.
        /// Additionally, the race/class data is hierarctical, and does not fit into a
        /// row-based format.
        /// </summary>
        /// <returns>Throws an exception.</returns>
        public IDictionary<string, string> GetContractDict()
        {
            throw new NotImplementedException("This method is invalid for this class.");
        }
    }
}