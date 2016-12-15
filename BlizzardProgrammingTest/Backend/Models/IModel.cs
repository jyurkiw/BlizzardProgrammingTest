using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlizzardProgrammingTest.Backend.Models
{
    /// <summary>
    /// A standard model interface.
    /// Makes up a data contract that should allow for
    /// initialization from the DB, and conversion
    /// into front-end friendly contract format.
    /// </summary>
    public interface IModel
    {
        void ModelInit(string[] row);
        IDictionary<string, string> GetContractDict();
    }
}
