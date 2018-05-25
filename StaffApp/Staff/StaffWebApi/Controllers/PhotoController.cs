using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaffWebApi.Controllers
{
    [EnableCors("all")]
    [Route("[controller]")]
    public class PhotoController : Controller
    {
        // GET Photo/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var i = (id % 3)+1;
            //return Redirect($"~/Photos/{i}.jpg");
            return File($"~/Photos/{i}.jpg", "image/jpeg");
        }
    }
}
