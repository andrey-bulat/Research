using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Position { get; set; }
        public string Department { get; set; }

        public string Resume { get; set; }
    }
}
