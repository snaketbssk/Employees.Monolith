using System;

namespace Employees.Monolith.DataLayer.Models.Tables.Entities
{
    public class PermissionTable : ConnectionUserTable, IUpdateAtTable
    {
        public DateTime UpdateAt { get; set; }
    }
}
