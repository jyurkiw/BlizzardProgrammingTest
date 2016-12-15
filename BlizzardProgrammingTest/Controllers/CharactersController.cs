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
    /// <summary>
    /// Get, Create, and Delete characters.
    /// </summary>
    public class CharactersController : ApiController
    {
        /// <summary>
        /// Get the user's character list.
        /// TODO: Remove user controller.
        /// Use windows auth.
        /// </summary>
        /// <param name="id">The username.</param>
        /// <returns>The user's characters.</returns>
        public IHttpActionResult Get(string id)
        {
            List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(id);

            //return characterList;
            return Ok(characterList);
        }

        /// <summary>
        /// POST a new character.
        /// New characters automatically start at level 1 (Death Knights are handled in the AddNewCharacter method).
        /// </summary>
        /// <param name="value">The character data.</param>
        /// <returns>A standard HTTP response.</returns>
        public IHttpActionResult Post([FromBody]CharacterRowModel value)
        {
            value.Level = 1;
            DBObject.AddNewCharacter(value);

            return Ok();
        }

        /// <summary>
        /// Delete a character.
        /// TODO: Pass in a username and check for character ownership.
        /// Username should be retrieved from the controller. Not the front-end.
        /// </summary>
        /// <param name="id">The ID of the character.</param>
        public void Delete(int id)
        {
            DBObject.DeleteCharacter(id);
        }
    }
}
