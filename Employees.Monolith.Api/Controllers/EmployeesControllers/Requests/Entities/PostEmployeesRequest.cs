using System;
using System.Collections.Generic;

namespace Employees.Monolith.Api.Controllers.EmployeesControllers.Requests.Entities
{
    public class PostEmployeesRequest
    {
        public List<Guid> GuidDepartments { get; set; }
        public List<Guid> GuidSkills { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
    }
}
