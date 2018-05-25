using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Staff.WebApi.Model;
using StaffWebApi.Model;

namespace Staff.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("all")]
    public class StaffDataController : Controller
    {
        private readonly StaffService _staffService;

        public StaffDataController(StaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("[action]/{pageNum}/{pageSize}")]
        public StaffList GetPage(int pageNum, int pageSize)
        {
            return _staffService.GetPage(pageSize<10?10: pageSize, pageNum < 0 ? 0 : pageNum);
        }

        [HttpGet("[action]/{id}")]
        public Employee GetEmployee(int id)
        {
            return _staffService.GetEmployee(id);
        }

        [HttpGet("[action]")]
        public int GetEmployeesCount()
        {
            return _staffService.GetEmployeesCount();
        }
    }
}
