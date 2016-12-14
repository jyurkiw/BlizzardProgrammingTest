using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlizzardProgrammingTest.Backend.Models
{
    public interface IModel
    {
        void ModelInit(string[] row);
        IDictionary<string, string> GetContractDict();
    }
}
