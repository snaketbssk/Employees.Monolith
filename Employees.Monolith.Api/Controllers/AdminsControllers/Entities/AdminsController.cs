using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.DataLayer.Models.Contexts.Entities;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Monolith.Api.Controllers.AdminsControllers.Entities
{
    [ApiController]
    [Route(RouteConstant.CONTROLLER)]
    public class AdminsController : ControllerBase
    {
        private readonly ILogger<AdminsController> _logger;
        private readonly DatabaseContext _context;
        public AdminsController(ILogger<AdminsController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// [ADMINISTRATORS] GetUsersAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserTable>), 200)]
        [Authorize(AuthenticationSchemes = SchemeConstant.VALIDATE_X_TOKEN, Roles = GroupConstant.ADMINISTRATORS)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _context.Users.Where(v => v.Role == RoleUserTable.Admin).ToListAsync();
            return Ok(result);
        }
    }
}
