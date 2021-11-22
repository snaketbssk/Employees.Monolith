using System;

namespace Employees.Monolith.DataLayer.Models.Tables.Entities
{
    public abstract class GuidUserTable : BaseTable, IGuidUserTable
    {
        public Guid GuidUser { get; set; }
    }
}
