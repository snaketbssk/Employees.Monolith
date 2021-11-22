using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Creaters;
using System.ComponentModel.DataAnnotations;

namespace Employees.Monolith.Api.Controllers.DepartmentControllers.Requests.Entities
{
    public class PutDepartmentRequest : ICreater<DepartmentTable>
    {
        [StringLength(32)]
        public string Title { get; set; }

        public DepartmentTable Create()
        {
            var result = new DepartmentTable
            {
                Title = Title
            };
            return result;
        }

        public DepartmentTable Update(DepartmentTable result)
        {
            result.Title = Title;
            return result;
        }
    }
}
