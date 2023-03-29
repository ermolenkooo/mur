using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using BLL.Models;
using BLL;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace webserver.Controllers
{
    public class AccountController : Controller
    {
        private Identity myIdentity;

        [HttpPost("/token")]
        public IActionResult Token(string username, string password, string role)
        {
            myIdentity = new Identity();
            var identity = myIdentity.GetIdentity(username, password, role);
            if (identity == null)
                return BadRequest(new { errorText = "Invalid username or password." });

            var response = new
            {
                access_token = myIdentity.CreateToken(),
                username = identity.Name
            };
            
            return Json(response);
        }
    }
}
