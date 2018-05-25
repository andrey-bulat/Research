using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Staff.Infrastructure.Data;
using StaffWebApi.Model;

namespace Staff.WebApi.Model
{
    public class StaffService
    {
        private readonly StaffContext _context;
        public StaffService(StaffContext context)
        {
            _context = context;
        }

        public StaffList GetPage(int pageSize, int pageNum)
        {
            var staff = _context.Employees
                .Skip(pageNum * pageSize)
                .Take(pageSize)
                .OrderBy(o => o.EmployeeId)
                .Select(o => new StaffItem
                {
                    EmployeeId = o.EmployeeId,
                    LastName = o.LastName,
                    FirstName = o.FirstName,
                    Position = o.Position,
                    Department = o.Department,
                });

            var pagesCount = _context.Employees.Count();
            pagesCount = pagesCount == 0 ? 0 : (int)Math.Ceiling(((decimal)pagesCount) / pageSize);

            return new StaffList
            {
                PageNum = pageNum,
                PageSize = pageSize,
                StaffItems = staff.ToArray(),
                PagesCount = pagesCount,
            };
        }

        public int GetEmployeesCount()
        {
            return _context.Employees.Count();
        }

        public UserInfo Login(string login, string password)
        {
            return _context.Users
                .Where(o => o.Login == login && o.Password == password)
                .Select(u => new UserInfo
                {
                    UserId = u.EmployeeId,
                    DisplayName = u.Employee.FirstName + " " + u.Employee.LastName,
                    UserName = u.Login,
                })
                .FirstOrDefault();
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees
                .Select(o =>
                    new Employee
                    {
                        Birthday = o.Birthday.Date,
                        Department = o.Department,
                        EmployeeId = o.EmployeeId,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        MobilePhone = o.MobilePhone,
                        Position = o.Position,
                        Resume = o.Resume,
                        SecondName = o.SecondName,
                        WorkPhone = o.WorkPhone,
                    }
                )
                .FirstOrDefault(o => o.EmployeeId == id);
        }
    }
}
