using System;
using System.Collections.Generic;
using System.Text;
using Staff.Model.Interfaces;

namespace Staff.Model.Entities
{
    public class AppUser:IEntityWithKey<int>
    {
        public int LoginId { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public int EntityKey => LoginId;
    }
}
