using System;

namespace Employees.Monolith.DataLayer.Models.Tables
{
    public interface IGuidTable
    {
        Guid? Guid { get; set; }
    }
}
