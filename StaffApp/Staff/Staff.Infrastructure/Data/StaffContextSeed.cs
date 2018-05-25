using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Staff.Model.Entities;

namespace Staff.Infrastructure.Data
{
    public static class StaffContextSeed
    {
        private static readonly string[] namesM = new[] { "Иван", "Олег", "Федор", "Святослав", "Всеволод" };
        private static readonly string[] namesF = new[] { "Ольга", "Анна", "Наталья", "Светлана", "Раиса" };
        private static readonly string[] positions = new[] { "специалист", "посыльный", "бухгалтер" };
        private static readonly string[] departments = new[] { "Маркетинг", "Поставки", "Бухгалтерия" };
        private static readonly string[] resume = new[] { "Хороший человек", "Редиска", "Так себе" };

        private static Random _rnd = new Random();

        private static DateTime GetRndBirthday()
        {
            return new DateTime(_rnd.Next(1970, 1990), _rnd.Next(1, 12), _rnd.Next(1, 28));
        }

        private static ICollection<Employee> GetRndEmploees()
        {
            var male = new Employee
            {
                FirstName = namesM[_rnd.Next(0, 4)],
                SecondName = namesM[_rnd.Next(0, 4)] + "ович",
                LastName = namesM[_rnd.Next(0, 4)] + "ов",
                Birthday = GetRndBirthday(),
                MobilePhone = "+380 (67) 000 " + _rnd.Next(1, 9999).ToString("00 00"),

                WorkPhone = "+380 (67) 999 00 " + _rnd.Next(1, 99).ToString("00"),
                Position = positions[_rnd.Next(0, 2)],
                Department = departments[_rnd.Next(0, 2)],
                Resume = resume[_rnd.Next(0, 2)]
            };

            var female = new Employee
            {
                FirstName = namesF[_rnd.Next(0, 4)],
                SecondName = namesM[_rnd.Next(0, 4)] + "овна",
                LastName = namesM[_rnd.Next(0, 4)] + "ова",
                Birthday = GetRndBirthday(),
                MobilePhone = "+380 (67) 000 " + _rnd.Next(1, 9999).ToString("00 00"),

                WorkPhone = "+380 (67) 999 00 " + _rnd.Next(1, 99).ToString("00"),
                Position = positions[_rnd.Next(0, 2)],
                Department = departments[_rnd.Next(0, 2)],
                Resume = resume[_rnd.Next(0, 2)]
            };

            return new[] { male, female };
        }

        public static void EnsureSeedData(this StaffContext context
            , ILoggerFactory loggerFactory = null)
        {
            //try
            {
                if (!context.Employees.Any())
                {
                    var num = 0;
                    for (var i = 1; i < 40; i++)
                    {
                        var range = GetRndEmploees();
                        foreach (var employee in range)
                        {
                            num++;
                            employee.User = new AppUser
                            {
                                Login = $"user{num}@company.com",
                                Password = $"user{num}",
                            };
                            context.Employees.Add(employee);
                        }
                    }

                    context.SaveChanges();
                }
            }
            //catch (Exception ex)
            //{
            //    if (loggerFactory != null)
            //    {
            //        var log = loggerFactory.CreateLogger("staff seed");
            //        log.LogError(ex.Message);
            //    }

            //    throw;
            //}
        }
    }
}
