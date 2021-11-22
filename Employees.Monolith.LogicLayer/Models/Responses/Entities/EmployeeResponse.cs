using Employees.Monolith.DataLayer.Models.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Monolith.LogicLayer.Models.Responses.Entities
{
    public class EmployeeResponse
    {
        public EmployeeTable Employee { get; set; }
        public Guid GuidDepartment { get; set; }
        public EmployeeResponse(EmployeeTable employee, Guid guidDepartment)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            GuidDepartment = guidDepartment;
        }
    }
}
