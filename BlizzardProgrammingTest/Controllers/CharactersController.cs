using BlizzardProgrammingTest.Backend;
using BlizzardProgrammingTest.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BlizzardProgrammingTest.Controllers
{
    public class CharactersController : ApiController
    {
        // GET: api/Characters
        //public IEnumerable<IDictionary<string, string>> Get(string id)
        public IHttpActionResult Get(string id)
        {
            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(id);

            //return characterList;
            return Ok(characterList);
        }

        // POST: api/Characters
        public IHttpActionResult Post([FromBody]CharacterRowModel value)
        {
            value.Level = 1;
            DBObject.AddNewCharacter(value);

            return Ok();
        }

        // DELETE: api/Characters/5
        public void Delete(int id)
        {
            DBObject.DeleteCharacter(id);
        }
    }
}
