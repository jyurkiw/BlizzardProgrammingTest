using BlizzardProgrammingTest.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlizzardProgrammingTest.Controllers
{
    /// <summary>
    /// Get and undelete characters.
    /// </summary>
    public class DeletedCharactersController : ApiController
    {
        // GET: api/DeletedCharacters/5
        public IHttpActionResult Get(string id)
        {
            string username = RequestContext.Principal.Identity.Name;
            username = username.Substring(username.LastIndexOf('\\') + 1);

            if (id.CompareTo(username) == 0)
            {
                List<IDictionary<string, string>> characterList = DBObject.GetDeletedCharacterList(id);

                //return characterList;
                return Ok(characterList);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: api/DeletedCharacters
        public IHttpActionResult Post(int id)
        {
            string username = RequestContext.Principal.Identity.Name;
            username = username.Substring(username.LastIndexOf('\\') + 1);

            if (DBObject.UndeleteCharacter(id, username))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
