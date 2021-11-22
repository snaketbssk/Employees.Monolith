using System;

namespace Employees.Monolith.DataLayer.Models.Tables
{
    public interface IExpiredAtTable
    {
        DateTime ExpiredAt { get; set; }
    }
}
