using Employees.Monolith.Configurations.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace Employees.Monolith.DataLayer.Models.Contexts.Entities
{
    public class FactoryContext : IDesignTimeDbContextFactory<DatabaseContext>
    {
        DatabaseContext IDesignTimeDbContextFactory<DatabaseContext>.CreateDbContext(string[] args)
        {
            AppConfiguration.Set(args);
            var result = DatabaseContext.GetContext();
            return result;
        }
    }
}
