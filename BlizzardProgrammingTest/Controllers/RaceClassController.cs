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
    public class RaceClassController : ApiController
    {
        // GET: api/RaceClass
        public IHttpActionResult Get(string id)
        {
            Dictionary<string, Dictionary<string, List<string>>> raceClasses = DBObject.GetRaceClass(id);

            return Ok(raceClasses);
        }
    }
}
