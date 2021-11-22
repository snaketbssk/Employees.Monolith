using Employees.Monolith.DataLayer.Models.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Monolith.LogicLayer.Models.Creaters
{
    public interface IEmployeeTableCreater : ICreater<EmployeeTable>
    {
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        decimal Salary { get; set; }
        Guid GuidDepartment { get; set; }
        List<Guid> GuidSkills { get; set; }
    }
}
