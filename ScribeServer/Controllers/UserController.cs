using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ScribeServer.Models;

namespace ScribeServer.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// ROUTE: api/user
        /// Used to get information on a currently logged in user via jwt token.
        /// </summary>
        /// <returns>Returns User claims according to the OpenID token settings.
        /// Returns 401 for a non-logged in user.</returns>
        [HttpGet]
        [Authorize]
        public string Get()
        {
            var sub = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub").Value.ToString());
            var user = Database.GlobalDatabase.Users.FirstOrDefault(u => u.AuthorId == sub);
            return user.Role.ToString();
        }
    }
}
