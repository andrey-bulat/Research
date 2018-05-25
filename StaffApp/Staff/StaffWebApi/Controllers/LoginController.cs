using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staff.WebApi.Model;
using StaffWebApi.Model;

namespace StaffWebApi.Controllers
{
    [EnableCors("all")]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly StaffService _staffService;

        public LoginController(StaffService staffService)
        {
            _staffService = staffService;
        }

        [Produces("application/json")]
        [HttpPost("[action]")]
        public async Task<ActionResult> SignIn([FromBody] LoginInfo value)
        {
            var user = _staffService.Login(value.Login, value.Password);
            if (user == null)
            {
                return new UnauthorizedResult();
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("FullName", user.DisplayName),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(24)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                        new ClaimsPrincipal(claimsIdentity),
                                        authProperties);

                return new JsonResult(user);
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> SignOut([FromBody] UserInfo user)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new OkResult();
        }
    }
}