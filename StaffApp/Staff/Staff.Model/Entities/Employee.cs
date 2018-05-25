using System;
using System.Collections.Generic;
using System.Text;
using Staff.Model.Interfaces;

namespace Staff.Model.Entities
{
    public class Employee : IEntityWithKey<int>
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
        public string MobilePhone { get; set; }


        #region EmployeeWork
        public string WorkPhone { get; set; }

        public string Position { get; set; }
        public string Department { get; set; }

        public string Resume { get; set; }
        #endregion

        public AppUser User { get; set; }

        public int EntityKey => EmployeeId;
    }
}
