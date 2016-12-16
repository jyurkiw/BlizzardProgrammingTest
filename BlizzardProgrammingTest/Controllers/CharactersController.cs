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
            string username = RequestContext.Principal.Identity.Name;
            username = username.Substring(username.LastIndexOf('\\') + 1);

            if (id.CompareTo(username) == 0)
            {
                List<IDictionary<string, string>> characterList = DBObject.GetCharacterList(id);

                //return characterList;
                return Ok(characterList);
            }
            else
            {
                return Unauthorized();
            }
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
            string username = RequestContext.Principal.Identity.Name;
            username = username.Substring(username.LastIndexOf('\\') + 1);

            if (value.Owner.CompareTo(username) == 0)
            {
                DBObject.AddNewCharacter(value, username);

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Apply a character level token.
        /// </summary>
        /// <param name="id">The id of the character.</param>
        /// <returns>An Ok if the user owns the character.</returns>
        public IHttpActionResult Put(int id)
        {
            string username = RequestContext.Principal.Identity.Name;
            username = username.Substring(username.LastIndexOf('\\') + 1);

            if(DBObject.ApplyLevelToken(id, username))
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Delete a character.
        /// TODO: Pass in a username and check for character ownership.
        /// Username should be retrieved from the controller. Not the front-end.
        /// </summary>
        /// <param name="id">The ID of the character.</param>
        public IHttpActionResult Delete(int id)
        {
            string username = RequestContext.Principal.Identity.Name;
            username = username.Substring(username.LastIndexOf('\\') + 1);

            if(DBObject.DeleteCharacter(id, username))
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
