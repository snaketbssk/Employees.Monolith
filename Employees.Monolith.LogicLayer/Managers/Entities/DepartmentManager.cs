using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using System.Threading.Tasks;

namespace Employees.Monolith.LogicLayer.Managers.Entities
{
    public class DepartmentManager : Manager<DatabaseContext>
    {
        public DepartmentManager(DatabaseContext context) : base(context)
        {
        }
    }
}
