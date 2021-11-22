using System;

namespace Employees.Monolith.DataLayer.Models.Tables
{
    public interface ICreatedAtTable
    {
        DateTime? CreatedAt { get; set; }
    }
}
