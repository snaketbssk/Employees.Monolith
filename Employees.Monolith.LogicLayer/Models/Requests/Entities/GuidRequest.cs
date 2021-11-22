using System;

namespace Employees.Monolith.LogicLayer.Models.Requests.Entities
{
    public class GuidRequest : IGuidRequest
    {
        public Guid Guid { get; set; }
    }
}
