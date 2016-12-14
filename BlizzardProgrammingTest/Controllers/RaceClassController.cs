using BlizzardProgrammingTest.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlizzardProgrammingTest.Controllers
{
    /// <summary>
    /// GET for Race/Class data.
    /// </summary>
    public class RaceClassController : ApiController
    {
        /// <summary>
        /// Get data necessary to populate the create character page.
        /// </summary>
        /// <param name="id">The numeric id of the character to delete.</param>
        /// <returns>Race/Class data.</returns>
        public IHttpActionResult Get(string id)
        {
            Dictionary<string, Dictionary<string, List<string>>> raceClasses = DBObject.GetRaceClass(id);

            return Ok(raceClasses);
        }
    }
}
