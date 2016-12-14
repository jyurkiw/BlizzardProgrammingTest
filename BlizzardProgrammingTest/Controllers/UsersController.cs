using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlizzardProgrammingTest.Controllers
{
    public class UsersController : ApiController
    {
        // GET: api/Users
        /// <summary>
        /// Get the users's username from the request context. The application has annon access turned off
        /// and Windows auth turned on, so we'll be making this user-specific at the windows account level.
        /// We'll call it the latest in anti-cheat-something'er'other. It's probably not the best idea for
        /// a game, but we're not going to worry about that.
        /// </summary>
        /// <returns>A basic user data object containing the current username.</returns>
        public IDictionary<string, string> Get()
        {
            string name = RequestContext.Principal.Identity.Name;
            return new Dictionary<string, string> { { "username", name.Substring(name.LastIndexOf('\\') + 1) } };
        }
    }
}
