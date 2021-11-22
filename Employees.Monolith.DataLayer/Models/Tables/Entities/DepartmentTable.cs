using System.Collections.Generic;

namespace Employees.Monolith.DataLayer.Models.Tables.Entities
{
    public class DepartmentTable : BaseTable
    {
        public string Title { get; set; }
        public ICollection<EmployeeTable> Employees { get; set; }
    }
}
