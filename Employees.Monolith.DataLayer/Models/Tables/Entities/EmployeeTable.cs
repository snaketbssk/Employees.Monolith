using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Employees.Monolith.DataLayer.Models.Tables.Entities
{
    public class EmployeeTable : BaseTable
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PatronymicName { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        [JsonIgnore]
        public int DepartmentId { get; set; }
        [JsonIgnore]
        public DepartmentTable Department { get; set; }
        public ICollection<SkillTable> Skills { get; set; }
    }
}
