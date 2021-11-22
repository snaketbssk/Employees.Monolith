using Employees.Monolith.DataLayer.Models.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Monolith.LogicLayer.Models.Responses.Entities
{
    public class DepartmentResponse
    {
        public DepartmentTable Department { get; set; }
        public decimal AvarageSalary { get; set; }
        public int CountEmployees { get; set; }
        public DepartmentResponse(DepartmentTable department, decimal avarageSalary, int countEmployees)
        {
            Department = department ?? throw new ArgumentNullException(nameof(department));
            AvarageSalary = avarageSalary;
            CountEmployees = countEmployees;
        }
    }
}
