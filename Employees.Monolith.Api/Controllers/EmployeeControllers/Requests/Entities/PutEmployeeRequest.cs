using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Creaters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.EmployeeControllers.Requests.Entities
{
    public class PutEmployeeRequest : IEmployeeTableCreater
    {
        [StringLength(32)]
        public string FirstName { get; set; }
        [StringLength(32)]
        public string MiddleName { get; set; }
        [StringLength(32)]
        public string LastName { get; set; }
        [StringLength(32)]
        public string PatronymicName { get; set; }
        [Range(typeof(decimal), "1", "9999999999")]
        public decimal Salary { get; set; }
        [RegularExpression(RegularExpressionConstant.PHONE)]
        public string Phone { get; set; }
        public Guid GuidDepartment { get; set; }
        public List<Guid> GuidSkills { get; set; }
        public EmployeeTable Create()
        {
            var result = new EmployeeTable
            {
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                PatronymicName = PatronymicName,
                Salary = Salary,
                Phone = Phone
            };
            return result;
        }

        public EmployeeTable Update(EmployeeTable result)
        {
            result.FirstName = FirstName;
            result.MiddleName = MiddleName;
            result.LastName = LastName;
            result.PatronymicName = PatronymicName;
            result.Salary = Salary;
            result.Phone = Phone;
            return result;
        }
    }
}
